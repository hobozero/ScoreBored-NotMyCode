using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net.Mail;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ScoreBoard
{
    public partial class RecoverPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Kludge to recover from Bug#189 The SMTP server requires a secure connection or the client was not authenticated. The server response was: 5.7.0 Must issue a STARTTLS command first.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PasswordRecovery1_SendingMail(object sender, MailMessageEventArgs e)
        {

            MailMessage mm = new MailMessage();

            mm.IsBodyHtml = true;
            mm.From = e.Message.From;

            mm.Subject = e.Message.Subject.ToString();

            mm.To.Add(e.Message.To[0]);

            mm.Body = e.Message.Body;
            SmtpClient smtp = new SmtpClient();
            smtp.EnableSsl = true;

            smtp.Send(mm);
            e.Cancel = true;
        }

        protected void SEndMAil(MembershipUser user, string password)
        {
            Dictionary<string, string> replacements = new Dictionary<string, string>(2);
            replacements.Add("<%UserName%>", user.UserName);
            replacements.Add("<%Password%>", password);

            MailMessage mm = PasswordRecovery1.MailDefinition.CreateMailMessage(user.Email, replacements, this);

            mm.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.EnableSsl = true;

            smtp.Send(mm);

        }

        protected void PasswordRecovery1_VerifyingUser(object sender, LoginCancelEventArgs e)
        {
            string userName = ((System.Web.UI.WebControls.PasswordRecovery)(sender)).UserName;
            MembershipUser user = Membership.GetUser(userName);

            if (user != null)
            {
                if (user.IsLockedOut)
                {
                    pnlLockedOut.Visible = true;
                    txtUserName.Text = userName;
                    lblSecQues.Text = user.PasswordQuestion;

                    e.Cancel = true;
                }
            }
        }

        protected void PasswordRecovery1_VerifyingAnswer(object sender, LoginCancelEventArgs e)
        {

        }

        protected void btnUnlock_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            MembershipUser user = Membership.GetUser(userName);
            string newPAssword = string.Empty;


            if (user != null)
            {
                try
                {
                    if (txtSecQuesAns.Text.Length > 0)
                    {
                        newPAssword = user.ResetPassword(txtSecQuesAns.Text);
                        SEndMAil(user, newPAssword);
                        lblPWError.Text = string.Empty;
                        lblPass.Text = "New password sent";
                    }
                    else
                    {
                        lblPWError.Text = "Security question incorrect.";
                    }
                }
                catch (MembershipPasswordException ex)
                {
                    if (ex.Message == "The user account has been locked out.")
                    {
                        user.UnlockUser();
                        if (txtSecQuesAns.Text.Length > 0)
                        {
                            try
                            {
                                newPAssword = user.ResetPassword(txtSecQuesAns.Text);
                                lblPWError.Text = string.Empty;
                                SEndMAil(user, newPAssword);
                                lblPass.Text = "New password sent.";
                            }
                            catch (MembershipPasswordException ex2)
                            {
                                ////MySql Implementation
                                //string cnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ToString();
                                //using (MySqlConnection cnx = new MySqlConnection(cnString))
                                //{
                                //    string cmdText = "update my_aspnet_Membership set IsLockedOut=1 where UserId = '" + user.ProviderUserKey.ToString() + "'";
                                //    using (MySqlCommand cmd = new MySqlCommand(cmdText, cnx))
                                //    {
                                //        cmd.CommandType = CommandType.Text;
                                //        cnx.Open();
                                //        cmd.ExecuteNonQuery();
                                //        cnx.Close();
                                //    }
                                //}

                                //Need to relock the user
                                Database db = DatabaseFactory.CreateDatabase("cnGrammit");
                                db.ExecuteNonQuery(CommandType.Text, "update aspnet_Membership set IsLockedOut=1 where UserId = '" + user.ProviderUserKey.ToString() + "'");

                                lblPWError.Text = "Security question incorrect.";


                                lblPWError.Text = "Security question incorrect.";
                            }
                        }
                    }
                    else
                    {
                        lblPWError.Text = "Security question incorrect.";
                    }
                }
            }
        }
    }
}
