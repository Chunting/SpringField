using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Xml;
using MSRA.SpringField.Components;
using System.Linq;
using System.Xml.Linq;

namespace ResumeCollector.Lib
{
    public static class AppConfiguration
    {
        private static NameValueCollection m_Settings;

        private const string PROJECT_RESOURCE_FILE_NAME = "ProjectResource";
        private const string RESUME_MAPPING_FILE_NAME = "ResumeMapping";
        private const string LOG_LOCATION = "LogLocation";
        private const string RESUME_LOCATION = "ResumeLocation";
        private const string ARCHIVE_LOCATION = "ArchiveLocation";
        private const string FAILED_LOCATION = "FailedLocation";
        private const string RESUME_PWD = "ResumePassword";
        private const string CREATE_ARCHIVE_FOLDER = "CreateNewArchiveFolder";
        private const string CREATE_FAILED_FOLDER = "CreateNewFailedFolder";
        private const string DURATION_SPAN = "DurationSpan";
        private const string DURATION_NUM = "DurationNum";
        private const string START_TIME = "StartTime";
        private const string NOTIFIER = "Notifier";
        private const string RECEIVER = "Receiver";

        static AppConfiguration()
        {
            m_Settings = ConfigurationManager.AppSettings;
        }

        public static string ProjectResourceFileName
        {
            get { return m_Settings[PROJECT_RESOURCE_FILE_NAME]; }
        }

        public static string ResumeMappingFileName
        {
            get { return m_Settings[RESUME_MAPPING_FILE_NAME]; }
        }

        public static string ResumePassword
        {
            get { return m_Settings[RESUME_PWD]; }
        }

        public static string GetSettingValue(string key)
        {
            return m_Settings[key];
        }

        public static NameValueCollection Settings
        {
            get { return m_Settings; }
        }

        /// <summary>
        /// Use Linq to Xml update App.Config file to change the settings of the application
        /// @Author: Yin.P
        /// @Date: 2009-11-10
        /// </summary>
        /// <param name="key">the unique identity of setting</param>
        /// <param name="value">value of setting</param>
        public static void UpdateAppSettings(string key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                XElement doc = XElement.Load(config.FilePath);
                if (doc != null)
                {
                    XElement el = doc.Descendants("appSettings")
                        .Descendants<XElement>("add").FirstOrDefault<XElement>(p => p.Attribute("key").Value.Equals(key));
                    if (el.Attribute("value").Value.Equals(value) == false)
                    {
                        el.SetAttributeValue("value", value);
                        doc.Save(config.FilePath);
                    }

                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public static string LogLocation
        {
            get { return m_Settings[LOG_LOCATION]; }
            set { UpdateAppSettings(LOG_LOCATION, value); }
        }

        public static string ResumeLocation
        {
            get { return m_Settings[RESUME_LOCATION]; }
            set { UpdateAppSettings(RESUME_LOCATION, value); }
        }

        public static string ArchiveLocation
        {
            get { return m_Settings[ARCHIVE_LOCATION]; }
            set { UpdateAppSettings(ARCHIVE_LOCATION, value); }
        }

        public static string FailedLocation
        {
            get { return m_Settings[FAILED_LOCATION]; }
            set { UpdateAppSettings(FAILED_LOCATION, value); }
        }

        public static bool CreateArchiveFolder
        {
            get { return Convert.ToBoolean(m_Settings[CREATE_ARCHIVE_FOLDER]); }
            set { UpdateAppSettings(CREATE_ARCHIVE_FOLDER, value.ToString()); }
        }

        public static bool CreateFailedFolder
        {
            get { return Convert.ToBoolean(m_Settings[CREATE_FAILED_FOLDER]); }
            set { UpdateAppSettings(CREATE_FAILED_FOLDER, value.ToString()); }
        }

        public static DurationSpanEnum DurationSpan
        {
            get { return (DurationSpanEnum)EnumHelper.IntegerToEnum(typeof(DurationSpanEnum), Convert.ToInt32(m_Settings[DURATION_SPAN])); }
            set { UpdateAppSettings(DURATION_SPAN, EnumHelper.EnumToInteger(value).ToString()); }
        }

        public static int DurationNum
        {
            get { return Convert.ToInt32(m_Settings[DURATION_NUM]); }
            set { UpdateAppSettings(DURATION_NUM, Convert.ToString(value)); }
        }

        public static int StartTime
        { 
            get { return Convert.ToInt32(m_Settings[START_TIME]); }
            set { UpdateAppSettings(START_TIME, Convert.ToString(value)); }
        }

        public static string EmailNotifier
        {
            get { return m_Settings[NOTIFIER]; }
            set { UpdateAppSettings(NOTIFIER, value); }
        }

        public static string EmailReceiver
        {
            get { return m_Settings[RECEIVER]; }
            set { UpdateAppSettings(RECEIVER, value); }
        }
    }
}
