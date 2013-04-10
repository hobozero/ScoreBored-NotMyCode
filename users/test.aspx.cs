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
using MySql.Data.MySqlClient;
using System.Text;
using System.Net;
using System.Net.Mail;
using ScoreBored.UserManagement;


namespace ScoreBoard
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnComDummy_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpDown_Click(object sender, EventArgs e)
        {
            //MembershipUser mUser = Membership.GetUser();
            //UserManager.SendActivateEmail(mUser);

            MailGen mail = new MailGen();
            mail.SendMessage("amialive@hotmail.com", "info@scorebored.net", "Score Bored Account Activation", "test MSg");

            this.btnAddComment.Text = "sent";
        }
    }
}
