using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace JLT.Common.Utility
{
    public static class SendEmail
    {
        /// <summary>
        /// Sends an e-mail message using the designated SMTP mail server.
        /// </summary>
        /// <param name="subject">The subject of the message being sent.</param>
        /// <param name="messageBody">The message body.</param>
        /// <param name="fromAddress">The sender's e-mail address.</param>
        /// <param name="toAddress">The recipient's e-mail address (separate multiple e-mail addresses
        /// with a semi-colon).</param>
        /// <param name="ccAddress">The address(es) to be CC'd (separate multiple e-mail addresses with
        /// a semi-colon).</param>
        /// <remarks>You must set the SMTP server within this method prior to calling.</remarks>
        /// <example>
        /// <code>
        ///   // Send a quick e-mail message
        ///   SendEmail.SendMessage("This is a Test", 
        ///                         "This is a test message...",
        ///                         "noboday@nowhere.com",
        ///                         "somebody@somewhere.com", 
        ///                         "ccme@somewhere.com");
        /// </code>
        /// </example>
        public static void SendMessage(string subject, string messageBody, string fromAddress, string toAddress, string ccAddress, string bccAddress, bool IsHtmlFormat)
        {
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();

            // Set the sender's address
            message.From = new MailAddress(fromAddress);

            // Allow multiple "To" addresses to be separated by a semi-colon
            if (toAddress.Trim().Length > 0)
            {
                foreach (string addr in toAddress.Split(';'))
                {
                    message.To.Add(new MailAddress(addr));
                }
            }

            // Allow multiple "Cc" addresses to be separated by a semi-colon
            if (ccAddress.Trim().Length > 0)
            {
                foreach (string addr in ccAddress.Split(';'))
                {
                    message.CC.Add(new MailAddress(addr));
                }
            }

            if (bccAddress.Trim().Length > 0)
            {
                foreach (string addr in bccAddress.Split(';'))
                {
                    message.Bcc.Add(new MailAddress(addr));
                }
            }
            // Set the subject and message body text
            //message.Attachments.Add(new Attachment(
            message.Subject = subject;
            message.Body = messageBody;
            if (IsHtmlFormat == true) { message.IsBodyHtml = true; }
            // TODO: *** Modify for your SMTP server ***
            // Set the SMTP server to be used to send the message
            //client.Host = System.Configuration.ConfigurationManager.(

            // Send the e-mail message
            client.Send(message);
            message.Dispose();
        }
        public static void SendMessage(string subject, string messageBody, string fromAddress, string toAddress,
            string ccAddress, string bccAddress, string strAttachmentPath, bool IsHtmlFormat)
        {
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();

            // Set the sender's address
            message.From = new MailAddress(fromAddress);

            // Allow multiple "To" addresses to be separated by a semi-colon
            if (toAddress.Trim().Length > 0)
            {
                foreach (string addr in toAddress.Split(';'))
                {
                    message.To.Add(new MailAddress(addr));
                }
            }

            // Allow multiple "Cc" addresses to be separated by a semi-colon
            if (ccAddress.Trim().Length > 0)
            {
                foreach (string addr in ccAddress.Split(';'))
                {
                    message.CC.Add(new MailAddress(addr));
                }
            }

            if (bccAddress.Trim().Length > 0)
            {
                foreach (string addr in bccAddress.Split(';'))
                {
                    message.Bcc.Add(new MailAddress(addr));
                }
            }
            // Set the subject and message body text
            if(!String.IsNullOrWhiteSpace(strAttachmentPath)) message.Attachments.Add(new Attachment(strAttachmentPath));
            message.Subject = subject;
            message.Body = messageBody;
            if (IsHtmlFormat == true) { message.IsBodyHtml = true; }
            // TODO: *** Modify for your SMTP server ***
            // Set the SMTP server to be used to send the message
            //client.Host = System.Configuration.ConfigurationManager.(

            // Send the e-mail message
            client.Send(message);
            if (!String.IsNullOrWhiteSpace(strAttachmentPath)) message.Attachments.Dispose();
            message.Dispose();
        }
    }
}
