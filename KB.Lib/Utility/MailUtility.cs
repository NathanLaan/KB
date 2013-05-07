using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace KB.Lib.Utility
{
    public sealed class MailUtility
    {

        public void SendMail(string from, string to, string subject, string message, string smtpServer = "localhost")
        {
            try
            {
                MailMessage mailMessage = new MailMessage(from, to, subject, message);
                SmtpClient smtpClient = new SmtpClient(smtpServer);
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Send(mailMessage);
            }
            catch
            {
                //
                // TODO: Logging
                //
            }
        }

    }
}
