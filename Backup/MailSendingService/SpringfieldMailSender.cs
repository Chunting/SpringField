using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.Threading;

namespace Springfield.ExtendModule.EmailModule
{
    public partial class SpringfieldMailSender : ServiceBase
    {
        public SpringfieldMailSender()
        {
            InitializeComponent();
        }

        private Timer mailSenderTimer;

        private Timer interviewCheckTimer;

        protected override void OnStart(string[] args)
        {
            StartApp();
        }

        protected override void OnStop()
        {
            try
            {
                // stop the service
                mailSenderTimer.Change(0, 0);
                interviewCheckTimer.Change(0, 0);
                SF_Mail.Logger.Log("[ INFO ] - OnStop() --- mail sender is stopping...");
                //StartApp();
            }
            catch (Exception ex)
            {
                SF_Mail.Logger.Log("[ ERROR ] - OnStop() --- mail sender errors when stopping..." + ex.ToString());
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            SF_Mail.Logger.Log("[ INFO ] -  OnPause()");
        }
        protected override void OnContinue()
        {
            base.OnContinue();
            SF_Mail.Logger.Log("[ INFO ] -  OnContinue()");
        }
        protected override void OnShutdown()
        {
            OnStop();
            SF_Mail.Logger.Log("[ INFO ] -  OnShutdown()");
        }

        private void StartApp()
        {
            try
            {
                SF_Mail.Logger.Log("The mail sender start...");

                //------------下面都是关于unsent mail-----------------

                /// Timer for checking unsent mail
                TimeSpan unsentCheckTime = GetTimeSpan("checkUnsentMailInterval");//new TimeSpan(1, 0, 0);

                TimeSpan noDelay = new TimeSpan(0, 0, 0);
                TimerCallback unsentCallback = new TimerCallback(SF_Mail.MailServiceProvider.SendUnsentMails);//查未发邮件（传入要调用的方法）//(old)mailSender.unsentCheck
                mailSenderTimer = new Timer(unsentCallback, null, noDelay, unsentCheckTime);
                SF_Mail.Logger.Log("[ INFO ] -  Unsent mail checking timer start: interval = " + unsentCheckTime.ToString() + "  delay = " + noDelay.ToString());




                //------------下面都是关于查邮件-----------------

                /// Timer for checking unfinished interview
                TimeSpan interviewCheckTime = GetTimeSpan("checkUnfinishedInterviewInterval");//interviewCheckTime = new TimeSpan(1, 0, 0, 0);
                
                TimeSpan delay = GetTimeSpan("checkUnfinishedInterviewDelay");

                /// Check unfinished interview at 00:00 every day???????is something wrong?
                TimerCallback remindCallback = new TimerCallback(SF_Mail.MailServiceProvider.interviewCheck);//查面试（传入要调用的方法）//(old)mailSender.interviewCheck 
                interviewCheckTimer = new Timer(remindCallback, null, delay, interviewCheckTime);
                SF_Mail.Logger.Log("[ INFO ] -  Unfinished Interview checking timer start: interval = " + interviewCheckTime.ToString() + "  delay = " + delay.ToString());
            }
            catch (Exception er)
            {
                string ss = er.Message;
                SF_Mail.Logger.Log("[ ERROR ] - StartApp() --- mail sender errors when StartApp...");
            }
        }

        /// <summary>
        /// get timespan from config
        /// </summary>
        /// <param name="config">web.config or app.config</param>
        /// <returns></returns>
        private TimeSpan GetTimeSpan(string config)
        {
            int day, hour, minute, second;

            string appSetting = ConfigurationManager.AppSettings[config];
            if (appSetting == "")
            {
                //TODO

                //DateTime now = DateTime.Now;
                //DateTime tommorrow = new DateTime();
                //tommorrow = now + interviewCheckTime;
                //DateTime firstCheckTime = new DateTime(tommorrow.Year, tommorrow.Month, tommorrow.Day, 0, 0, 0);
                //delay = firstCheckTime - now;
            }
            string[] checkInterviewDelay = appSetting.Split(':');
            day = Convert.ToInt32(checkInterviewDelay[0]);
            hour = Convert.ToInt32(checkInterviewDelay[1]);
            minute = Convert.ToInt32(checkInterviewDelay[2]);
            second = Convert.ToInt32(checkInterviewDelay[3]);

            return new TimeSpan(day, hour, minute, second);
        }
    }
}
