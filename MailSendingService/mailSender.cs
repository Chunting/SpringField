/// Author: Jiansong Zhang
/// Date: 2006-04-29
/// Description: Automatic mail sender for springfield project.
///              (1) Read mail from data base
///              (2) Send mail by smtp protocol
///              (3) Update the status of every sent mail
///              Run independently, in task schedule 

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Configuration;
using System.Threading;
using System.IO;

namespace Springfield.ExtendModule.EmailModule
{
    class mailSender
    {
        //private const string FILE_NAME = "mailSender.log";
        private static readonly string FILE_NAME = ConfigurationManager.AppSettings["logFileName"];
        private static readonly string SENDER_ADDRESS = ConfigurationManager.AppSettings["senderAddress"];
        private static readonly string SENDER_NAME = ConfigurationManager.AppSettings["senderName"];
        private static readonly string SENDER_PASSWORD = ConfigurationManager.AppSettings["senderPassword"];
        private static readonly string SENDER_DOMAIN = ConfigurationManager.AppSettings["senderDomain"];
        private static readonly string MAIL_SERVER = ConfigurationManager.AppSettings["mailServer"];

        /// <summary>
        /// Hourly check unsent mail
        /// </summary>
        public static void unsentCheck(Object stateInfo)
        {
            Log("[ INFO ] - Begin to check unsent mails and try to send them");

            try
            {
                //TODO 这里数据查询要修改，升级到3.5才能用linq

                /// Set up connection to data base
                string connectionString = ConfigurationManager.AppSettings["connectionString"];
                DataSet mailSet = new DataSet();
                SqlConnection mailConnection = new SqlConnection(connectionString);
                SqlCommand unsentMailCommand = new SqlCommand("SELECT * FROM dbo.sf_Email WHERE dbo.sf_Email.IsSend = 0",
                                                              mailConnection);
                SqlDataAdapter mailAdapter = new SqlDataAdapter(unsentMailCommand);
                SqlCommandBuilder builder = new SqlCommandBuilder(mailAdapter);

                /// Get mail from data base
                mailConnection.Open();
                mailAdapter.Fill(mailSet);
                //mailConnection.Close();

                ///// Send mail
                //sendMail(mailSet);
                if (mailSet != null)
                {
                    DataRow[] dataRows=new DataRow[1];
                    foreach (DataRow rowIndex in mailSet.Tables[0].Rows)
                    {
                        MailMessage mailMessage = InitMail(rowIndex);
                        if (mailMessage != null)
                        {
                            if (sendMailbySmtp(mailMessage))//如果发送成功
                            {
                                rowIndex["IsSend"] = 1;// The mail has been sent
                                dataRows[0] = rowIndex;
                                mailAdapter.Update(dataRows);

                                Log("[ INFO ] - Mail  id = " + rowIndex["EmailId"] + " has been sent, Sender: " + SENDER_ADDRESS + "; Receiver: " + mailMessage.To[0].ToString());
                                mailMessage.Dispose();
                            }
                        }
                    }
                }
                
                ///// Update the status of sent mail
                ////mailConnection.Open();
                //mailAdapter.Update(mailSet);
                mailConnection.Close();
            }
            catch(Exception ex)
            {
                Log(ex.Message);
                return;
            }

            Log("[ INFO ] - Unsent mail check finish.");

        }

        # region send unsent mail

        /// <summary>
        ///   Check every unsent mail, try to send it
        /// </summary>
        /// <param name="mailSet"></param>
        public static void sendMail(DataSet mailSet)
        {
            if (mailSet == null)
            {
                return;
            }
            foreach (DataRow rowIndex in mailSet.Tables[0].Rows)
            {
                //sendMailbySmtp(rowIndex);
                MailMessage mailMessage = InitMail(rowIndex);
                if (mailMessage != null)
                {
                    if (sendMailbySmtp(mailMessage))//如果发送成功
                    {
                        rowIndex["IsSend"] = 1;// The mail has been sent
                                
                        //Log("[ INFO ] - A mail has been sent, Sender: " + SENDER_ADDRESS + "; Receiver: " + mailMessage.To[0].ToString());
                        Log("[ INFO ] - Mail  id = " + rowIndex["EmailId"] + " has been sent, Sender: " + SENDER_ADDRESS + "; Receiver: " + mailMessage.To[0].ToString());
                        
                        mailMessage.Dispose();//释放资源
                    }
                }
            }
        }
        /// <summary>
        /// Initializate a email  from DataRow to MailMessage
        /// by Mingming Lou 2012.07.04
        /// </summary>
        /// <returns></returns>
        public static MailMessage InitMail(DataRow mailbody)
        {
            MailMessage message;

            try
            {
                MailAddress from = new MailAddress(SENDER_ADDRESS);

                message = new MailMessage();
                message.From = from;

                string tofromDB = mailbody["To"].ToString();
                string[] toAddresses = tofromDB.Split(';');
                foreach (string toAddress in toAddresses)
                {
                    message.To.Add(toAddress);
                }

                string ccfromDB = mailbody["Cc"].ToString();
                if (ccfromDB != null && ccfromDB.Length > 0)
                {
                    string[] ccAddresses = ccfromDB.Split(';');
                    foreach (string ccAddress in ccAddresses)
                    {
                        message.CC.Add(ccAddress);
                    }
                }

                string bccfromDB = mailbody["Bcc"].ToString();
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
                Log(string.Format("[ ERROR ] - Invalid mail address found! Error message is {0}", ex.Message));
                return null;
            }

            message.Subject = mailbody["Subject"].ToString();
            message.Body = mailbody["body"].ToString();
            // message.BodyEncoding = System.Text.Encoding.Unicode;
            message.Priority.Equals(mailbody["Priority"].ToString());
            message.IsBodyHtml = true;

            return message;
        }

        public static bool sendMailbySmtp(MailMessage mailMessage)
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
                Log("[ ERROR ] - Error when sending mail! - [MESSAGE: " + ex.Message + "]");
                return false;
            }
            return true;

        }

        /// <summary>
        ///   Try to send a mail by smtp
        /// </summary>
        /// <param name="mailbody"></param>
        public static void sendMailbySmtp_old(DataRow mailbody)
        {
            MailMessage message;

            #region Initialize a mail
            try
            {
                MailAddress from = new MailAddress(SENDER_ADDRESS);
                
                message = new MailMessage();
                message.From = from;

                string tofromDB = mailbody["To"].ToString();
                string [] toAddresses = tofromDB.Split(';');
                foreach (string toAddress in toAddresses)
                {
                    message.To.Add(toAddress);
                }

                string ccfromDB = mailbody["Cc"].ToString();
                if (ccfromDB != null && ccfromDB.Length > 0)
                {
                    string [] ccAddresses = ccfromDB.Split(';');
                    foreach (string ccAddress in ccAddresses)
                    {
                        message.CC.Add(ccAddress);
                    }
                }
                
                string bccfromDB = mailbody["Bcc"].ToString();
                if (bccfromDB != null && bccfromDB.Length > 0)
                {
                    string [] bccAddresses = bccfromDB.Split(';');
                    foreach (string bccAddress in bccAddresses)
                    {
                        message.Bcc.Add(bccAddress);
                    }
                }
            }
            catch(Exception ex)
            {
                Log(string.Format("[ ERROR ] - Invalid mail address found! Error message is {0}", ex.Message));
                return;
            }

            message.Subject = mailbody["Subject"].ToString();
            message.Body = mailbody["body"].ToString();
            // message.BodyEncoding = System.Text.Encoding.Unicode;
            message.Priority.Equals(mailbody["Priority"].ToString());
            message.IsBodyHtml = true;

            // port? credential?

            #endregion


            SmtpClient client = new SmtpClient(MAIL_SERVER);

            client.Credentials = new System.Net.NetworkCredential(
                SENDER_NAME,
                SENDER_PASSWORD,
                SENDER_DOMAIN);
            


            /// Try to send the mail, if the mail has been sent correctly, modify the "IsSend" to 1. Otherwise, return directly 
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Log("[ ERROR ] - Error when sending mail! - [MESSAGE: " + ex.Message + "]");
                return;
            }
            mailbody["IsSend"] = 1;            // The mail has been sent
            Log("[ INFO ] - A mail has been sent, Sender: " + SENDER_ADDRESS + "; Receiver: " + message.To[0].ToString());
            message.Dispose();
            return;
        }

        #endregion

        /// <summary>
        /// Daily check unfinished interview
        /// </summary>
        public static void interviewCheck(Object stateInfo)
        {
            Log("[ INFO ] - Begin to check unfinished interview whose dute date is before today, send reminding mail to interviewer and hiring manager.");

            try
            {
                string connectionString = ConfigurationManager.AppSettings["connectionString"];
                DataSet remindSet = new DataSet();
                SqlConnection remindConnection = new SqlConnection(connectionString);
                SqlCommand remindCommand = new SqlCommand("dbo.sf_GetUncompleteFeedback",
                                                          remindConnection);
                remindCommand.CommandType = CommandType.StoredProcedure;
                remindCommand.Parameters.Add(new SqlParameter("@DueDate", DateTime.Now));
                SqlDataAdapter remindAdapter = new SqlDataAdapter(remindCommand);

                /// Get Interviewer ID and Hiring Manager ID from database
                remindConnection.Open();
                remindAdapter.Fill(remindSet);
                remindConnection.Close();

                if (remindSet == null)
                {
                    return;
                }

                /// Get the reminding mail template from database
                DataSet mailTemplate = new DataSet();
                SqlConnection templateConnection = new SqlConnection(connectionString);
                SqlCommand templateCommand = new SqlCommand("SELECT * FROM dbo.sf_EmailTemplate WHERE dbo.sf_EmailTemplate.EmailType = 13",
                                                            templateConnection);
                SqlDataAdapter templateAdapter = new SqlDataAdapter(templateCommand);

                /// Get Interviewer ID and Hiring Manager ID from database
                templateConnection.Open();
                templateAdapter.Fill(mailTemplate);
                templateConnection.Close();

                /// Send reminding mail
                sendRemindMail(remindSet, mailTemplate);
            }
            catch(Exception ex)
            {
                Log(string.Format("[ ERROR ] - {0}", ex.Message));
                return;
            }

            Log("[ INFO ] - Interview check finish.");
        }

        #region send reminding mail

        /// <summary>
        /// Send reminding mail
        /// </summary>
        /// <param name="remindSet"></param>
        public static void sendRemindMail(DataSet remindSet, DataSet mailTemplate)
        {
            foreach (DataRow rowIndex in remindSet.Tables[0].Rows)
            {
                Log("[ INFO ] - A reminder email will be sent.");
                string mailbody = mailTemplate.Tables[0].Rows[0]["EmailTemplate"].ToString();
                string mailSubject = mailTemplate.Tables[0].Rows[0]["EmailSubject"].ToString();

                mailbody = mailbody.Replace("//Date//", DateTime.Now.ToShortDateString());
                mailbody = mailbody.Replace("//DueDate//", rowIndex["DueDate"].ToString());
                string interviewerAlias = rowIndex["InterviewerAlias"].ToString();
                string hiringManager = rowIndex["UserName"].ToString();
                string[] hiringManagerAlias = hiringManager.Split('\\');
                mailbody = mailbody.Replace("//applicantid//", rowIndex["ApplicantId"].ToString());
                mailbody = mailbody.Replace("//Applicant Name//", rowIndex["FirstName"].ToString() + " " + rowIndex["LastName"].ToString());

                mailSubject = mailSubject.Replace("//DueDate//", rowIndex["DueDate"].ToString());
                mailSubject = mailSubject.Replace("//Date//", DateTime.Now.ToShortDateString());
                mailSubject = mailSubject.Replace("//applicantid//", rowIndex["ApplicantId"].ToString());
                mailSubject = mailSubject.Replace("//Applicant Name//", rowIndex["FirstName"].ToString() + " " + rowIndex["LastName"].ToString());

                sendRemindMailbySmtp(interviewerAlias, hiringManagerAlias[1], mailSubject, mailbody);
            }
        }

        /// <summary>
        /// Send reminding mail by smtp protocol
        /// </summary>
        /// <param name="alias"></param>
        public static void sendRemindMailbySmtp(string InterviewerAlias, string HiringManagerAlias, string mailSubject, string mailbody)
        {
            MailMessage message;

            try
            {
                /// Initialize a mail
                string mailExt = ConfigurationManager.AppSettings["mailExt"];
                MailAddress from = new MailAddress(SENDER_ADDRESS);
                MailAddress to = new MailAddress(InterviewerAlias + mailExt);
                message = new MailMessage(from, to);
                message.CC.Add(HiringManagerAlias + mailExt);
                string internRecruiter = ConfigurationManager.AppSettings["internRecruiter"];
                message.CC.Add(internRecruiter);
            }
            catch(Exception e)
            {
                Log(string.Format("[ ERROR ] - Invalid mail address found! Error message was {0}", e.Message));
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

            /// Try to send the mail, if the mail has been sent correctly, modify the "IsSend" to 1. Otherwise, return directly 
            try
            {
                client.Send(message);
            }
            catch (Exception e)
            {
                Log(string.Format("[ ERROR ] - Error when sending reminding mail! Error message was {0}", e.Message));
                return;
            }
            Log(string.Format("[ INFO ] - A reminding mail has been sent! Sender: {0}; Receiver: {1}", 
                SENDER_ADDRESS, message.To[0].ToString()));
        }

        #endregion

        /// <summary>
        /// log message to "mailSender.log"
        /// </summary>
        /// <param name="logmessage"></param>
        public static void Log(string logmessage)
        {
            StreamWriter sw = null;
            int logFileSize = 5;

            try
            {
                logFileSize = Convert.ToInt32(ConfigurationManager.AppSettings["logFileSize"]);
            }
            catch
            {
                logFileSize = 5;
            }

            try
            {
                if (!File.Exists(FILE_NAME))
                {
                    sw = File.CreateText(FILE_NAME);
                }
                else
                {
                    FileInfo logFile = new FileInfo(FILE_NAME);
                    //LMM fixed : didn't use logFileSize before,just a constant
                    //5 * 1024 * 1024
                    if (logFile.Length >= (logFileSize * logFileSize*1024*1024))
                    {
                        //delete the log file larger than 5M
                        //LMM 竟然是把文件直接删除，木有备份或删除最旧的一段什么呢 留个 TODO 以后再改把
                        logFile.Delete();
                        sw = File.CreateText(FILE_NAME);
                    }
                    else
                    {
                        sw = File.AppendText(FILE_NAME);
                    }
                }
                sw.WriteLine("{0}:\t{1}", DateTime.Now.ToString(), logmessage);
                sw.Flush();
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
}
