using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Net.Mail;
using ScoreBored.UserManagement;
using MySql.Data.MySqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ScoreBoard
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Create an activation DB record with a unique GUID that 
                //Use that as a request variable to the activation page
                //IF this is an activation
                if (Request["a"] != null)
                {
                    try
                    {
                        Guid actGuid = new Guid(Request["a"].ToUpper().ToString());
                        string sUserGuid = string.Empty;


                        Database db = DatabaseFactory.CreateDatabase("cnGrammit");
                        try
                        {
                            sUserGuid = db.ExecuteScalar("spActivateUser", new object[1] { actGuid }).ToString();
                        }
                        catch
                        {

                        }
                        Guid userGuid = new Guid(sUserGuid);
                        MembershipUser user = Membership.GetUser(userGuid);
                        if (user != null)
                        {
                            user.IsApproved = true;
                            Membership.UpdateUser(user);

                            this.Login1.LoginButtonText = "Please log in to complete activation.";
                        }

                        
                        //MySql Implementation
                        //try
                        //{

                        //    string cnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["LocalMySqlServer"].ToString();
                        //    using (MySqlConnection cnx = new MySqlConnection(cnString))
                        //    {
                        //        string cmdText = "usp_ActivateUser";
                        //        using (MySqlCommand cmd = new MySqlCommand(cmdText, cnx))
                        //        {
                        //            cmd.CommandType = CommandType.StoredProcedure;
                        //            cmd.Parameters.Add(new MySqlParameter("?GUID", actGuid.ToString()));
                        //            cnx.Open();
                        //            sUserGuid = cmd.ExecuteScalar().ToString();
                        //            cnx.Close();
                        //        }
                        //    }
                        //}
                        //catch
                        //{

                        //}
                        //int userId = 0;
                        //if (int.TryParse(sUserGuid, out userId))
                        //{
                        //    MembershipUser user = Membership.GetUser(userId);
                        //    if (user != null)
                        //    {
                        //        user.IsApproved = true;
                        //        Membership.UpdateUser(user);

                        //        this.Login1.LoginButtonText = "Please log in to complete activation.";
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        this.Login1.TitleText = "Error Activating Account: <br />" + ex.Message;
                    }
                }
            }
        }

        protected void Login1_LoggingIn1(object sender, LoginCancelEventArgs e)
        {
            System.Web.UI.WebControls.Login login = sender as System.Web.UI.WebControls.Login;
            MembershipUser mUser = Membership.GetUser(login.UserName);

            if (mUser != null && !mUser.IsApproved)
            {
                //remind user they need to activate thier account
                //show email box
                //update email address, then resend activation
                activateBox.Visible = true;

                lblEmail.Text = mUser.Email;

                UserManager.SendActivateEmail(mUser);

            }
        }

        protected void Login1_LoggedIn(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Login login = sender as System.Web.UI.WebControls.Login;
            MembershipUser mUser = Membership.GetUser(login.UserName);

            if (mUser != null)
            {

                if (Request["ReturnUrl"] == null)
                {
                    if (Roles.IsUserInRole(login.UserName, "User"))
                        Response.Redirect("/users");
                }

            }
        }
    }
}
