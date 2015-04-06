using Microsoft.AspNet.Identity;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace Qiyas.WebAdmin.Common.Email
{
    public class EmailManager
    {
        #region Send Email
        public static bool SendActivationEmail(string email, string activationCode, string username)
        {
            string tmplName = "TwoWayAuthentication.html", tmpl = string.Empty;
            string title;
            title = Resources.MainResource.TwoWayAuthTitle;
            

            try
            {
                tmpl = getEmailTemplate(tmplName);
            }
            catch
            {
                // error loading template file
                throw new Exception(string.Format(Resources.MainResource.EmailTemplateError, tmplName));
            }

            // replace ##xxx## with appropriate values
            tmpl = tmpl.Replace("##USERNAME##", username);
            tmpl = tmpl.Replace("##AUTHCODE##", activationCode);

            // send email 
            if (sendMail(tmpl, title, email))
                return true;

            return false;
        }

        public static string getEmailTemplate(string TemplateName)
        {
            string tmpl = string.Empty;
            // load template file
            StreamReader streamReader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/ContentData/EmailTemplates/" + TemplateName));
            tmpl = streamReader.ReadToEnd();
            streamReader.Close();
            return tmpl;
        }

        public static Task configSendGridasync(IdentityMessage message)
        {
            
            var myMessage = new SendGridMessage();
            myMessage.AddTo(message.Destination);
            myMessage.From = new System.Net.Mail.MailAddress(
                                "info@qiyas.sa", "Qiyas");
            myMessage.Subject = message.Subject;
            myMessage.Text = message.Body;
            myMessage.Html = message.Body;
            //myMessage.EnableTemplateEngine("b9f5bd23-2f6c-4ce6-9e42-e78ec282d984");
            var credentials = new NetworkCredential(
                       BusinessLogicLayer.Common.SendGridUserName,
                       BusinessLogicLayer.Common.SendGridPassword
                       );

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);
            try
            {
                if (transportWeb != null)
                {
                    return transportWeb.DeliverAsync(myMessage);
                }
                else
                {
                    return Task.FromResult(0);
                }
            }
            catch(Exception ex)
            {

            }
            return Task.FromResult(0);
            // Send the email.
            
        }

        public static bool SendEmailViaSendGrid(string Destination, string Subject, string Body, string FromEmail, string FromName)
        {
            
            var myMessage = new SendGridMessage();
            myMessage.AddTo(Destination);
            myMessage.From = new System.Net.Mail.MailAddress(
                                FromEmail, FromName);
            myMessage.Subject = Subject;
            myMessage.Text = Body;
            myMessage.Html = Body;
            //myMessage.EnableTemplateEngine("b9f5bd23-2f6c-4ce6-9e42-e78ec282d984");
            var credentials = new NetworkCredential(
                       BusinessLogicLayer.Common.SendGridUserName,
                       BusinessLogicLayer.Common.SendGridPassword
                       );

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);
            try
            {
                if (transportWeb != null)
                {
                    transportWeb.Deliver(myMessage);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
            // Send the email.

        }


        public static bool sendMail(string body, string subject, string toUser)
        {
            // mail parameters
            string fromEmail = "qiyasuser@gmail.com";
            if (BusinessLogicLayer.Common.FromEmail != null)
                fromEmail = BusinessLogicLayer.Common.FromEmail;

            string MailServer = "smtp.gmail.com";

            MailServer = BusinessLogicLayer.Common.MailServer;

            SmtpClient client = new SmtpClient(MailServer);
            NetworkCredential cred = new NetworkCredential(BusinessLogicLayer.Common.MailUser, BusinessLogicLayer.Common.MailPassword);
            client.EnableSsl = BusinessLogicLayer.Common.EnableSSL.ToString().ToLowerInvariant() == "true" ? true : false;
            // Add credentials if the SMTP server requires them.        
            if (!BusinessLogicLayer.Common.MailPort.ToString().Equals(""))
                client.Port = Convert.ToInt32(BusinessLogicLayer.Common.MailPort.ToString());


            client.UseDefaultCredentials = false;
            client.Credentials = cred;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage message = new MailMessage();
            message.From = new MailAddress(BusinessLogicLayer.Common.MailUser, fromEmail);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            //if (MyValidation.isEmail(toUser))
            message.To.Add(toUser);
            //message.Bcc.Add(toUser);
            // Create a message and set up the recipients.            


            try
            {
                // Thread t = new Thread();
                client.Send(message);
            }
            catch (Exception ex)
            {
                // error loading template file
                //throw new ErrorException(ex.Message);
                return false;
            }

            //}
            return true;
        }

        public static bool sendMail(string body, string subject, string toUser, Page page, string fileAttachment, MemoryStream stream)
        {
            // mail parameters
            string fromEmail = "qiyasuser@gmail.com";
            if (BusinessLogicLayer.Common.FromEmail != null)
                fromEmail = BusinessLogicLayer.Common.FromEmail.ToString();

            string MailServer = "smtp.gmail.com";
            MailServer = BusinessLogicLayer.Common.MailServer;

            SmtpClient client = new SmtpClient(MailServer);
            NetworkCredential cred = new NetworkCredential(BusinessLogicLayer.Common.FromEmail, BusinessLogicLayer.Common.MailPassword);
            client.EnableSsl = BusinessLogicLayer.Common.EnableSSL.ToString().ToLowerInvariant() == "true" ? true : false;
            // Add credentials if the SMTP server requires them.        
            if (!BusinessLogicLayer.Common.MailPort.ToString().Equals(""))
                client.Port = Convert.ToInt32(BusinessLogicLayer.Common.MailPort.ToString());


            client.UseDefaultCredentials = false;
            client.Credentials = cred;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage message = new MailMessage();
            message.From = new MailAddress(BusinessLogicLayer.Common.MailUser, fromEmail);
            message.Subject = subject;
            //message.Body = body;
            message.IsBodyHtml = true;

            //if (MyValidation.isEmail(toUser))
            message.To.Add(toUser);
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
            //LinkedResource logo = new LinkedResource(page.MapPath("~/images/bg-top_email.jpg"));
            LinkedResource logo = new LinkedResource(page.MapPath("~/images/bg-top_email.jpg"));
            logo.ContentId = "conferencelogo";
            htmlView.LinkedResources.Add(logo);
            message.AlternateViews.Add(htmlView);


            try
            {
                // Thread t = new Thread();
                client.Send(message);
            }
            catch (Exception ex)
            {
                // error loading template file
                //throw new ErrorException(ex.Message);
                return false;
            }

            return true;
        }


        public static  Task  SendEmailAsync(IdentityMessage message)
        {
            bool useSendGrid = false;
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["UseSendGrid"]))
            {
                useSendGrid = BusinessLogicLayer.Common.UseSendGrid;
            }
            if (!useSendGrid)
            {
                bool result = Common.Email.EmailManager.sendMail(message.Body, message.Subject, message.Destination);
                if(result)
                    return Task.FromResult(1);
                else
                    return Task.FromResult(0);
            }
            else
                return Common.Email.EmailManager.configSendGridasync(message);
        }


        public static bool sendMailWithError(string body, string subject, string toUser, out string errorMessage)
        {
            // mail parameters
            string fromEmail = "qiyasuser@gmail.com";
            fromEmail = BusinessLogicLayer.Common.FromEmail;

            string MailServer = "smtp.gmail.com";

            MailServer = BusinessLogicLayer.Common.MailServer;

            SmtpClient client = new SmtpClient(MailServer);
            NetworkCredential cred = new NetworkCredential(BusinessLogicLayer.Common.MailUser, BusinessLogicLayer.Common.MailPassword);
            client.EnableSsl = BusinessLogicLayer.Common.EnableSSL.ToLowerInvariant() == "true" ? true : false;
            // Add credentials if the SMTP server requires them.        
            if (!BusinessLogicLayer.Common.MailPort.ToString().Equals(""))
                client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["MailPort"].ToString());


            client.UseDefaultCredentials = false;
            client.Credentials = cred;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage message = new MailMessage();
            message.From = new MailAddress(BusinessLogicLayer.Common.MailUser, fromEmail);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            //if (MyValidation.isEmail(toUser))
            message.To.Add(toUser);
            //message.Bcc.Add(toUser);
            // Create a message and set up the recipients.            


            try
            {
                // Thread t = new Thread();
                client.Send(message);
                errorMessage = "";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + "<br />" + ex.InnerException;
                // error loading template file
                //throw new ErrorException(ex.Message);
                return false;
            }

            return true;
        }
        #endregion

        
    }
}