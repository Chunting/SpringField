/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		SiteConfiguration.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to store the site configuration in Web.Config file

Remarks:
        This class should be used out of the Springfield.Components assembly, so it should be declared as an public class.
             
Environment:
		Class Library/.NET Framework 2.0 

Project:
        Springfield.Components for MSRA Intern Application Tracking System (MIATS), codename: Springfield;
        MSRA/MS^2.3/Compass Team

Revision History:			
		24/Apr/2006 Created by Yuan Chen

*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Xml;

namespace MSRA.SpringField.Components.Configuration
{
    /// <summary>
    /// class used to store the Springfield configurations
    /// </summary>
    public class SiteConfiguration
    {
        #region Private members
        private Hashtable dataProviders = new Hashtable();
        private string defaultDataProvider;
        private string defaultLanguage;
        private string smtpServer;
        private string mailMessageQueue;
        private string searchService;
        private string siteUrl;
        private NameValueCollection siteAttributes;
        #endregion

        #region Helper method to load configuration from Web.Config file
        /// <summary>
        /// method used to load configurations from Web.Config
        /// </summary>
        /// <param name="section">XmlNode of the configuration section to parse</param>
        internal void LoadValueFromConfig(System.Xml.XmlNode section)
        {
            XmlAttributeCollection attributeCollection = section.Attributes;

            try
            {
                defaultDataProvider = attributeCollection["defaultDataProvider"].Value;
            }
            catch
            {
                defaultDataProvider = "MSRA.SpringField.Foundation.SqlDataProvider, MSRA.SpringField.Foundation";
            }

            try
            {
                defaultLanguage = attributeCollection["defaultLanguage"].Value;
            }
            catch
            {
                defaultLanguage = "en-us";
            }

            try
            {
                smtpServer = attributeCollection["smtpServer"].Value;
            }
            catch
            {
                throw new Exception("smtp server config error");
            }

            try
            {
                mailMessageQueue = attributeCollection["mailMessageQueue"].Value;
            }
            catch
            {
                throw new Exception("mail message queue config error");
            }

            try
            {
                searchService = attributeCollection["searchService"].Value;
            }
            catch
            {
                throw new Exception("search service config error");
            }

            try
            {
                siteUrl = attributeCollection["siteUrl"].Value;
            }
            catch
            {
                throw new Exception("site url config error");
            }

            siteAttributes = new NameValueCollection();

            foreach (XmlAttribute attribute in attributeCollection)
            {
                siteAttributes.Add(attribute.Name, attribute.Value);
            }

            //get child nodes
            foreach (XmlNode node in section.ChildNodes)
            {
                switch (node.Name)
                {
                    case "dataProviders":
                        GetDataProviders(node, dataProviders);
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// generate data providers value
        /// </summary>
        /// <param name="node">XmlNode which store the data providers configuration</param>
        /// <param name="table">Hashtable to store the DataProviderConfiguration</param>
        private void GetDataProviders(XmlNode node, Hashtable table)
        {
            foreach (XmlNode provider in node)
            {
                switch (provider.Name)
                {
                    case "add":
                        table.Add(provider.Attributes["name"].Value, new DataProviderConfiguration(provider.Attributes));
                        break;

                    case "remove":
                        table.Remove(provider.Attributes["name"].Value);
                        break;

                    case "clear":
                        table.Clear();
                        break;

                    default:
                        break;
                }
            }
        } 
        #endregion

        #region Static GetConfig method
        /// <summary>
        /// method to get site configuration
        /// </summary>
        /// <returns>SiteConfiguration</returns>
        public static SiteConfiguration GetConfig()
        {
            if(SiteCache.Get("SiteConfiguration") == null)
            {
                SiteConfiguration config = ConfigurationManager.GetSection("springfield/settings") as SiteConfiguration;
                SiteCache.InsertMax("SiteConfiguration", config);
            }
            return SiteCache.Get("SiteConfiguration") as SiteConfiguration;
        }
        #endregion

        #region Properties
        public string DefaultDataProvider 
        { 
            get { 
                return defaultDataProvider; 
            } 
        }

        public string DefaultLanguage
        {
            get {
                return defaultLanguage;
            }
        }

        public string SmtpServer
        {
            get {
                return smtpServer;
            }
        }

        public string MailMessageQueue
        {
            get {
                return mailMessageQueue;
            }
        }

        public string SearchService
        {
            get {
                return searchService;
            }
        }

        public Hashtable DataProviders
        {
            get {
                return dataProviders;
            }
        }

        public string SiteUrl
        {
            get {
                return siteUrl;
            }
        }

        public NameValueCollection SiteAttributes
        {
            get {
                return siteAttributes;
            }
        }
        #endregion
    }
}
