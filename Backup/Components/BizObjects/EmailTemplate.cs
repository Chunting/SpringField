/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2008 Microsoft Corporation

Module Name: 
		EmailTemplate.cs

Author:
        Sining Li(v-sinili@microsoft.com)  
        IEG team
 
Abstract:
        Class used to store an Email Template
 
Remarks:
        This class should be used out of the Springfield.Components assembly, so it should be declared as an public class.
             
Environment:
		Class Library/.NET Framework 2.0 

Project:
        Springfield.Components for MSRA Intern Application Tracking System (MIATS), codename: Springfield;
        IEG team

Revision History:			
		20/Aug/2006 Created by Sining Li

*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Components.BizObjects
{
    public class EmailTemplate
    {
        #region Private Members
        private MailType mailType;
        private string from;
        private string to;
        private string cc;
        private string bcc;
        private string subject;
        private string body;
        #endregion

        #region Constructors
        public EmailTemplate()
        {
            mailType = MailType.ApplicationComplete;
            from = string.Empty;
            to = string.Empty;
            cc = string.Empty;
            bcc = string.Empty;
            subject = string.Empty;
            body = string.Empty;
        }

        //public Email(int id)
        //{

        //} 
        #endregion

        #region Methods
        public void Update()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.UpdateEmailTemplate(this);
        }
        #endregion

        #region Static Helper Methods
        public static EmailTemplate GetEmailTemplateByType(MailType mailType)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetEmailTemplateByType(mailType);
        }
        #endregion

        #region Properties
        public MailType EmailType
        {
            get { return mailType; }
            set { mailType = value; }
        }

        public string From
        {
            get { return from; }
            set { from = ((value == null) ? string.Empty : value); }
        }

        public string To
        {
            get { return to; }
            set { to = ((value == null) ? string.Empty : value); }
        }

        public string CC
        {
            get { return cc; }
            set { cc = ((value == null) ? string.Empty : value); }
        }

        public string BCC
        {
            get { return bcc; }
            set { bcc = ((value == null) ? string.Empty : value); }
        }

        public string Subject
        {
            get { return subject; }
            set { subject = ((value == null) ? string.Empty : value); }
        }

        public string Body
        {
            get { return body; }
            set { body = ((value == null) ? string.Empty : value); }
        }
        #endregion
    }
}
