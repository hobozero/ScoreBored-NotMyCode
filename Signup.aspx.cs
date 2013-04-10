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
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {
            MembershipUser mUser = null; ;

            //Add extra client data
            try
            {
                mUser = Membership.GetUser(CreateUserWizard1.UserName);
                object UserGUID = mUser.ProviderUserKey;

                if (!Roles.IsUserInRole(mUser.UserName, "User"))
                    Roles.AddUserToRole(mUser.UserName, "User");



                UserManager.SendActivateEmail(mUser);
            }
            catch (Exception ex){

                lblError.Text = "An error has occurred and your account could not be set up. " + ex.Message +
                     "<br /> Please click the back button to correct the problem  if possible or send an email to tech@scorebored.net so we can resolve the problem for you.";

                if (mUser != null)
                    Membership.DeleteUser(mUser.UserName, true);
                
            }
        }

      

        protected void CreateUserWizard1_CreatingUser(object sender, LoginCancelEventArgs e)
        {
            
            //***********************************************************************************************************************************************
            //TODO: REMOVE IN PRODUCTION
            //TextBox UserNameTextBox = (TextBox)UserNameWizardStep.ContentTemplateContainer.FindControl("UserName");
            //MembershipUser mUser = Membership.GetUser(UserNameTextBox.Text);
            //if (mUser != null)
            //{
            //    object UserGUID = mUser.ProviderUserKey;


            //    Membership.DeleteUser(mUser.UserName);
            //}
            //***********************************************************************************************************************************************

        }
    }
}
