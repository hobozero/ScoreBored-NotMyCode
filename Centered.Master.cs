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
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ScoreBoard
{
    public partial class Centered : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.User.Identity.IsAuthenticated)
                nav_signup.Visible = false;
        }


        protected void Login1_LoggingIn1(object sender, LoginCancelEventArgs e)
        {
            System.Web.UI.WebControls.Login login1 = sender as System.Web.UI.WebControls.Login;
            MembershipUser mUser = Membership.GetUser(login1.UserName);

            if (mUser != null && !mUser.IsApproved)
            {
                //remind user they need to activate thier account

                Database db = DatabaseFactory.CreateDatabase("cnGrammit");

                login1.FailureText = "Almost there!  You must <a href='/login.aspx'>ACTIVATE </a> your account first.";

                login1.FindControl("UserNameLabel").Visible =
                    login1.FindControl("UserName").Visible =
                    login1.FindControl("PasswordLabel").Visible =
                    login1.FindControl("Password").Visible =
                    login1.FindControl("LoginLinkButton").Visible =
                    false;

            }
        }

        protected void Login1_LoggedIn(object sender, EventArgs e)
        {

            if (Request["ReturnUrl"] == null)
            {
                System.Web.UI.WebControls.Login login = sender as System.Web.UI.WebControls.Login;

                //MembershipUser mUser = Membership.GetUser(login.UserName);
                //if (Roles.IsUserInRole(login.UserName, "User"))
                //    Response.Redirect("/Users");

            }
        }


        protected void Login1_LoginError(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Login login1 = sender as System.Web.UI.WebControls.Login;
            login1.FailureText = "FAIL.  Please try again or <a href='" +
                this.ResolveUrl("~/RecoverPassword.aspx") + "'>reset</a> your password";
        }
    }
}
