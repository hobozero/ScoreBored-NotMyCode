 using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using System.Net;
    using System.Net.Mail;
    using System.Web.Configuration;

namespace ScoreBored.UserManagement
{
   
  /// <summary>
    /// Summary description for MailGen
    /// </summary>
    public class MailGen
    {
        bool _isTest = false;
        public MailGen()
        {
            _isTest = (HttpContext.Current.Cache["basePath"].ToString().Contains("localhost"));
        }

        public void SendMessage(string toAddy, string fromAddy, string subject, string body)
        {
            NetworkCredential loginInfo = new NetworkCredential("tyler.ulrich@gmail.com", "uterus88");
            MailMessage msg = new MailMessage();
            SmtpClient client = null;

            if (_isTest) fromAddy = "tyler.ulrich@gmail.com";

            msg.From = new MailAddress(fromAddy);
            msg.To.Add(new MailAddress(toAddy));
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;
            msg.ReplyTo = new MailAddress("info@scorebored.net");
            if (_isTest)
            {
                //Under class="SmtpPermission" version="1"  Access="Connect"
                //Can only send on port 25.
                //Request for the permission of type 'System.Net.Mail.SmtpPermission, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089' failed.
                client = new SmtpClient("smtp.gmail.com", 25);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = loginInfo;
            }
            else
            {
                client = new SmtpClient(WebConfigurationManager.AppSettings["smtpServer"]);
                client.Credentials = new System.Net.NetworkCredential("info@scorebored.net", "uterus");
            }
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                string messge = "sending to: " + msg.To +" From " + msg.From + " Server: " + client.Host + " "+ ex.Message;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    messge += "  " + ex.Message;
                }

                throw new Exception(messge);
            }
        }
    }
}
