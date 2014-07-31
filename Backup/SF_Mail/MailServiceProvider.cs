/// by Mingming Lou 2012.07.16

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;

namespace SF_Mail
{
    public class MailServiceProvider
    {
        private static readonly string SENDER_ADDRESS = ConfigurationManager.AppSettings["senderAddress"];
        private static readonly string SENDER_NAME = ConfigurationManager.AppSettings["senderName"];
        private static readonly string SENDER_PASSWORD = ConfigurationManager.AppSettings["senderPassword"];
        private static readonly string SENDER_DOMAIN = ConfigurationManager.AppSettings["senderDomain"];
        private static readonly string MAIL_SERVER = ConfigurationManager.AppSettings["mailServer"];
        private static readonly string MAIL_EXT = ConfigurationManager.AppSettings["mailExt"];
        private static readonly string INTERN_RECRUITER = ConfigurationManager.AppSettings["internRecruiter"];

        /// <summary>
        /// send mail by smtp protocol
        /// </summary>
        /// <param name="mailMessage">mail object</param>
        /// <returns></returns>
        private static bool sendMailbySmtp(MailMessage mailMessage)
        {
            SmtpClient client = new SmtpClient(MAIL_SERVER);

            client.Credentials = new System.Net.NetworkCredential(
                SENDER_NAME,
                SENDER_PASSWORD,
                SENDER_DOMAIN);
            // Try to send the mail, if the mail has been sent correctly, modify the "IsSend" to 1. Otherwise, return directly 
            try
            {
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Logger.Log("[ ERROR ] - Error when sending mail! - [MESSAGE: " + ex.Message + "]");
                return false;
            }
            return true;

        }

        # region send unsent mail

        /// <summary>
        ///   Check every unsent mail, try to send it
        /// </summary>
        public static void SendUnsentMails(Object stateInfo)//有这个用不到的参数(用于回调)才能加入定时的服务
        {
            MailDataClassesDataContext db = new MailDataClassesDataContext();
            var db_sf_Emails = db.sf_Emails;
            var unsentMails = from mails in db_sf_Emails
                              where mails.IsSend == 0
                              select mails;

            foreach (var mail in unsentMails)
            {
                SendMail(mail, db);
            }
        }

        /// <summary>
        ///  send just a mail
        /// </summary>
        public static bool SendMail(sf_Email mail, MailDataClassesDataContext db)
        {
            MailMessage mailMessage = InitMail(mail);
            if (mailMessage != null)
            {
                Logger.Log("[ INFO ] - Mail InitMail() successful , id = " + mail.EmailId);

                if (sendMailbySmtp(mailMessage))//如果发送成功
                {
                    // Set mail has been sent
                    mail.IsSend = 1;
                    db.SubmitChanges();

                    Logger.Log("[ INFO ] - Mail sent successful , id = " + mail.EmailId + " , Sender: " + SENDER_ADDRESS + "; Receiver: " + mailMessage.To[0].ToString());
                    mailMessage.Dispose();//释放资源
                    return true;
                }
                else
                {
                    Logger.Log("[ ERROR ] - Mail sent failed , id = " + mail.EmailId + " , Sender: " + SENDER_ADDRESS + "; Receiver: " + mailMessage.To[0].ToString());
                }

            }
            mailMessage.Dispose();//释放资源
            return false;
        }


        /// <summary>
        /// Initializate a email  from Linq DB to MailMessage
        /// </summary>
        /// <returns></returns>
        private static MailMessage InitMail(sf_Email mail)
        {
            MailMessage message = new MailMessage();

            try
            {
                //from
                MailAddress from = new MailAddress(SENDER_ADDRESS);
                message.From = from;

                //to
                string tofromDB = mail.To;//mailbody["To"].ToString();
                if (tofromDB == null && tofromDB.Length == 0)
                {
                    Logger.Log("[ ERROR ] - InitMail failed, no from, id = " + mail.EmailId);
                    return null;
                }
                string[] toAddresses = tofromDB.Split(';');
                foreach (string toAddress in toAddresses)
                {
                    message.To.Add(toAddress);
                }

                //cc
                string ccfromDB = mail.Cc;//mailbody["Cc"].ToString();
                if (ccfromDB != null && ccfromDB.Length > 0)
                {
                    string[] ccAddresses = ccfromDB.Split(';');
                    foreach (string ccAddress in ccAddresses)
                    {
                        message.CC.Add(ccAddress);
                    }
                }

                //bcc
                string bccfromDB = mail.Bcc;//mailbody["Bcc"].ToString();
                if (bccfromDB != null && bccfromDB.Length > 0)
                {
                    string[] bccAddresses = bccfromDB.Split(';');
                    foreach (string bccAddress in bccAddresses)
                    {
                        message.Bcc.Add(bccAddress);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("[ ERROR ] - Invalid mail address found! id = " + mail.EmailId + "Error message is" + ex.Message);
                return null;
            }

            message.Subject = mail.Subject;     // mailbody["Subject"].ToString();
            message.Body = mail.Body;           // mailbody["body"].ToString();
            // message.BodyEncoding = System.Text.Encoding.Unicode;
            message.Priority.Equals(mail.Priority);//mailbody["Priority"].ToString()
            message.IsBodyHtml = true;

            //LMM:2012-08-20附件插入图片这里需要特判，太麻烦了，放到MSRA.CN 用全局URL吧（改模版即可，不需要改这里）

            return message;
        }

        #endregion

        #region send reminding mail

        /// <summary>
        /// Daily check unfinished interview
        /// </summary>
        public static void interviewCheck(Object stateInfo)//有这个用不到的参数(用于回调)才能加入定时的服务
        {
            Logger.Log("[ INFO ] - Begin to check unfinished interview whose dute date is before today, send reminding mail to interviewer and hiring manager.");

            try
            {
                MailDataClassesDataContext db = new MailDataClassesDataContext();
                IEnumerable<sf_GetUncompleteFeedbackResult> uncompleteFeedbacks = db.sf_GetUncompleteFeedback(DateTime.Now);

                if (uncompleteFeedbacks == null)
                {
                    return;
                }
                var query_template = from mail_template in db.sf_EmailTemplates
                                     where mail_template.EmailType == 13
                                     select mail_template;
                sf_EmailTemplate template = query_template.Single();

                sendRemindMail(uncompleteFeedbacks, template);
            }
            catch (Exception ex)
            {
                Logger.Log(string.Format("[ ERROR ] - interviewCheck error {0}", ex.Message));
                return;
            }
            Logger.Log("[ INFO ] - Interview check finish.");
        }

        /// <summary>
        /// Send reminding mail
        /// </summary>
        /// <param name="remindSet"></param>
        private static void sendRemindMail(IEnumerable<sf_GetUncompleteFeedbackResult> uncompleteFeedbacks, sf_EmailTemplate mailTemplate)
        {
            foreach (var feedback in uncompleteFeedbacks)
            {
                Logger.Log("[ INFO ] - A reminder email will be sent. id=" + feedback.applicantid);
                string mailSubject = mailTemplate.EmailSubject;
                mailSubject = mailSubject.Replace("//DueDate//", feedback.DueDate.ToString());
                mailSubject = mailSubject.Replace("//Date//", DateTime.Now.ToShortDateString());
                mailSubject = mailSubject.Replace("//applicantid//", feedback.applicantid.ToString());
                mailSubject = mailSubject.Replace("//Applicant Name//", feedback.FirstName + " " + feedback.LastName);

                string mailbody = mailTemplate.EmailTemplate;
                mailbody = mailbody.Replace("//Date//", DateTime.Now.ToShortDateString());
                mailbody = mailbody.Replace("//DueDate//", feedback.DueDate.ToString());
                mailbody = mailbody.Replace("//applicantid//", feedback.applicantid.ToString());
                mailbody = mailbody.Replace("//Applicant Name//", feedback.FirstName + " " + feedback.LastName);

                string interviewerAlias = feedback.InterviewerAlias;

                string hiringManager = feedback.UserName;
                string[] hiringManagerAlias = hiringManager.Split('\\');

                sendRemindMailbySmtp(interviewerAlias, hiringManagerAlias[1], mailSubject, mailbody);
            }
        }

        /// <summary>
        /// Send reminding mail by smtp protocol
        /// </summary>
        /// <param name="alias"></param>
        private static void sendRemindMailbySmtp(string InterviewerAlias, string HiringManagerAlias, string mailSubject, string mailbody)
        {
            MailMessage message;

            try
            {
                /// Initialize a mail
                MailAddress from = new MailAddress(SENDER_ADDRESS);
                MailAddress to = new MailAddress(InterviewerAlias + MAIL_EXT);
                message = new MailMessage(from, to);
                message.CC.Add(HiringManagerAlias + MAIL_EXT);
                message.CC.Add(INTERN_RECRUITER);
            }
            catch (Exception e)
            {
                Logger.Log(string.Format("[ ERROR ] - sendRemindMailbySmtp() error ! Invalid mail address found! Error message was {0}", e.Message));
                return;
            }

            message.Subject = mailSubject;
            message.Body = mailbody.Replace("//Interviewer Name//", InterviewerAlias);
            message.IsBodyHtml = true;
            // message.BodyEncoding = System.Text.Encoding.Unicode;

            SmtpClient client = new SmtpClient(MAIL_SERVER);

            client.Credentials = new System.Net.NetworkCredential(
                SENDER_NAME,
                SENDER_PASSWORD,
                SENDER_DOMAIN);

            if (sendMailbySmtp(message))
            {
                Logger.Log(string.Format("[ INFO ] - A reminding mail has been sent! Sender: {0}; Receiver: {1}", SENDER_ADDRESS, message.To[0].ToString()));
            }
            else
            {
                Logger.Log("[ ERROR ] - sendRemindMailbySmtp() error ! Error when sending reminding mail!");
            }
        }

        #endregion
    }
}
