using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.IO;
using Microsoft.Office.Interop.Outlook;

namespace ResumeCollector.Lib
{
    public static class EmailSender
    {
        /// <summary>
        /// Send mail by local outlook application. This method use outlook COM to fetch the settings of outlook and send the mail.
        /// @Author: Yin.P
        /// @Date: 2009-11-05
        /// </summary>
        /// <param name="to">Destination</param>
        /// <param name="cc">Copy</param>
        /// <param name="subject">Mail Title</param>                                                                      
        /// <param name="body">Mail Content</param>
        /// <param name="attachments">Files that attached to the mail</param>
        public static void SendEmailByLocalOutlook(string to, string cc, string subject, string body, string[] attachments)
        {
            Application ol = new Application();
            MailItem item = ol.CreateItem(0) as MailItem;
            if (cc != null && cc.Length > 0)
            {
                item.CC = cc;
            }

            for (int i = 0; i < attachments.Length; i++)
            {
                FileInfo info = new FileInfo(attachments[i]);
                item.Attachments.Add(attachments[i], OlAttachmentType.olByValue, 1, info.Name);
            }
            
            item.To = to;
            item.Subject = subject;
            item.Body = body;
            item.Send();
        }

        public static void SendEmail(string from, string to, string subject, string body, List<string> attachments)
        {
            MailAddress fromAddress = new MailAddress(from);
            MailMessage msg = new MailMessage();
            msg.From = fromAddress;

            string[] toAddressList = to.Split(new char[] { ';' });
            foreach (string toAddress in toAddressList)
            { 
                if(!string.IsNullOrEmpty(toAddress.Trim()))
                {
                    msg.To.Add(new MailAddress(toAddress));
                }
            }

            msg.Subject = subject;
            msg.Body = body;
            msg.Priority = MailPriority.High;

            if (attachments != null)
            {
                foreach (string filename in attachments)
                {
                    if(File.Exists(filename))
                    {
                        msg.Attachments.Add(new System.Net.Mail.Attachment(filename));
                    }
                }
            }

            SmtpClient client = new SmtpClient();
            client.Send(msg);
        }
    }
}
