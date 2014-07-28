using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Threading;
using System.IO;
namespace SF_Mail
{
    public class Logger
    {
        private static readonly string FILE_NAME = ConfigurationManager.AppSettings["logFileName"];
        private static readonly int LOG_FILE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["logFileSize"]);

        /// <summary>
        /// log message to "mailSender.log"
        /// </summary>
        /// <param name="logmessage"></param>
        public static void Log(string logMessage)
        {
            StreamWriter sw = null;
            try
            {
                if (!File.Exists(FILE_NAME))//不存在就新建
                {
                    sw = File.CreateText(FILE_NAME);
                }
                else
                {
                    FileInfo logFile = new FileInfo(FILE_NAME);
                    //LMM fixed : didn't use logFileSize before,just a constant
                    //5 * 1024 * 1024
                    if (logFile.Length >= (LOG_FILE_SIZE * 1024 * 1024))
                    {
                        //delete the log file larger than 5M
                        //LMM: 竟然是把文件直接删除，木有备份或删除最旧的一段什么呢 留个 TODO 以后再改把
                        logFile.Delete();
                        sw = File.CreateText(FILE_NAME);
                    }
                    else
                    {
                        sw = File.AppendText(FILE_NAME);
                    }
                }
                sw.WriteLine("{0}:\t{1}", DateTime.Now.ToString(), logMessage);
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
