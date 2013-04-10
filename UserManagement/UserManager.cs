using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Net;
using System.Net.Mail;
using System.Web.Security;
using System.IO;

namespace ScoreBored.UserManagement
{
    public class UserManager
    {
        public static void SendActivateEmail(MembershipUser user)
        {
            Database db = DatabaseFactory.CreateDatabase("cnGrammit");
            
            //Create an activation DB record with a unique GUID that 
            //Use that as a request variable to the activation page
            Guid activationGuid = Guid.NewGuid();

            DbCommand dbCommand = db.GetStoredProcCommand("usp_InsertUSER_ACTIVATION");
            db.AddInParameter(dbCommand, "@userID", DbType.Guid, user.ProviderUserKey);
            db.AddInParameter(dbCommand, "@GUID", DbType.Guid, activationGuid);
            db.AddOutParameter(dbCommand, "@ID", DbType.Int32, 2);
            db.ExecuteNonQuery(dbCommand);


            StringBuilder bodyMsg = new StringBuilder();

            bodyMsg.Append("So close... <br />  in order to complete enrollment with ScoreBored.net, you'll need to confirm your account.");
            bodyMsg.AppendFormat("<br />UserName: {0}", user.UserName);
            bodyMsg.Append("<br />Password Question: " + user.PasswordQuestion);


            bodyMsg.Append("<br />Click this link to activate your account: <a href=\"");
            bodyMsg.Append(HttpRuntime.Cache.Get("basePath").ToString());

            bodyMsg.Append("Login.aspx?a=" + HttpUtility.UrlEncode(activationGuid.ToString()) + "\">ACTIVATE</a>");

            MailGen mail = new MailGen();
            mail.SendMessage(user.Email, "info@scorebored.net", "ScoreBored Account Activation", bodyMsg.ToString());
        }

    }
}
