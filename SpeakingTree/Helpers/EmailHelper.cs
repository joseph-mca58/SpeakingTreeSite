using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net.Mail;
//using EASendMail;
using System.Net;

namespace SpeakingTree.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        string fromAddress = string.Empty; // email id from which the mails will be sent
        string emailUserSubject = string.Empty; // subject line for the emails sent to user
        string emailBodyUserMessage = string.Empty; // email body to be sent to user after processing is completed

        public EmailHelper()
        {
            fromAddress = ConfigurationManager.AppSettings["fromAddress"];

            emailUserSubject = ConfigurationManager.AppSettings["emailUserSubject"];

            emailBodyUserMessage = ConfigurationManager.AppSettings["emailBodyUserMessageTemplate"];
        }

        public void SendEmailToUser(string userEmail, string userId, string password, bool success, string instructions, string failureReason)
        {
            emailUserSubject = ConfigurationManager.AppSettings["emailUserSubject"];
            emailBodyUserMessage = Convert.ToString(ConfigurationManager.AppSettings["emailBody"]);
            
            if (success)
            {
                emailBodyUserMessage = emailBodyUserMessage.Replace("#status", "Successfully Completed");

                emailBodyUserMessage = emailBodyUserMessage.Replace("#userId", userId);
                emailBodyUserMessage = emailBodyUserMessage.Replace("#password", password);
            }
            else
            {
                emailBodyUserMessage = emailBodyUserMessage.Replace("#userId", "");
                emailBodyUserMessage = emailBodyUserMessage.Replace("#password", "");

                emailBodyUserMessage = emailBodyUserMessage.Replace("#status", "Failed");
                emailBodyUserMessage = emailBodyUserMessage + "\n Please contact Speaking Tree.";
                emailBodyUserMessage = emailBodyUserMessage + "\n " + failureReason;
                string[] icsEmailAdd = userEmail.Split(';');
                SendEmail(emailUserSubject, emailBodyUserMessage, fromAddress, icsEmailAdd);// to send failure email to DEV team

            }
            SendEmail(emailUserSubject, emailBodyUserMessage, fromAddress, new string[] { userEmail });
        }

        public static void SendEmail(string eSubject, string eBody, string fromAddress, string[] toAddresses)
        {
            try
            {
                //MailMessage objMsessage = new MailMessage();
                //MailAddress fromMailAddress = new MailAddress(fromAddress);
                //foreach (string toAddress in toAddresses)
                //{
                //    objMsessage.To.Add(toAddress);
                //}
                //objMsessage.IsBodyHtml = true;
                //objMsessage.From = fromMailAddress;
                //objMsessage.Subject = eSubject;
                //objMsessage.Body = eBody;
                //using (var smtpClient = new SmtpClient())
                //{
                //    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //    smtpClient.UseDefaultCredentials = true;
                //    smtpClient.Host = ConfigurationManager.AppSettings["SMTPHostServer"];
                //    smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPort"].ToString());
                //    smtpClient.Send(objMsessage);
                //}
                //ESendemail
                //SmtpMail oMail = new SmtpMail("TryIt");

                //// Your gmail email address
                //oMail.From = "aadi.swd@gmail.com";

                //// Set recipient email address
                //oMail.To = "aadi.swd@gmail.com";

                //// Set email subject
                //oMail.Subject = "test email from gmail account";

                //// Set email body
                //oMail.TextBody = "this is a test email sent from c# project with gmail.";

                //// Gmail SMTP server address
                //SmtpServer oServer = new SmtpServer("smtp.gmail.com");

                //// Gmail user authentication
                //// For example: your email is "gmailid@gmail.com", then the user should be the same
                //oServer.User = "aadi.swd@gmail.com";
                //oServer.Password = "Adipwd4gmail";

                //// If you want to use direct SSL 465 port,
                //// please add this line, otherwise TLS will be used.
                //// oServer.Port = 465;

                //// set 587 TLS port;
                //oServer.Port = 587;

                //// detect SSL/TLS automatically
                //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                ////Console.WriteLine("start to send email over SSL ...");

                //SmtpClient oSmtp = new SmtpClient();
                //oSmtp.SendMail(oServer, oMail);

                ////Console.WriteLine("email was sent successfully!");
                ///

                GMailer.GmailUsername = "aadi.swd@gmail.com";
                GMailer.GmailPassword = "Adipwd4gmail.com";

                GMailer mailer = new GMailer();
                mailer.ToEmail = "aadi.swd@gmail.com";
                mailer.Subject = "Verify your email id";
                mailer.Body = "Thanks for Registering your account.<br> please verify your email id by clicking the link <br> ";//<a href='youraccount.com/verifycode=12323232'>verify</a>
                mailer.IsHtml = true;
                mailer.Send();
            }
            catch (Exception ex)
            {
                //GSCAFolderRename.ElmahHelper.ElmahHelper.LogError(ex);
                //GSCAFolderRename.ElmahHelper.ElmahHelper.LogError(ex);
            }
        }
    }

    public class GMailer
    {
        public static string GmailUsername { get; set; }
        public static string GmailPassword { get; set; }
        public static string GmailHost { get; set; }
        public static int GmailPort { get; set; }
        public static bool GmailSSL { get; set; }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }

        static GMailer()
        {
            GmailHost = "smtp.gmail.com";
            GmailPort = 25; // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
            GmailSSL = true;
        }

        public void Send()
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = GmailHost;
            smtp.Port = GmailPort;
            smtp.EnableSsl = GmailSSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(GmailUsername, GmailPassword);

            using (var message = new MailMessage(GmailUsername, ToEmail))
            {
                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = IsHtml;
                smtp.Send(message);
            }
        }
    }
}