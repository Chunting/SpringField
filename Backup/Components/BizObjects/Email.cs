/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		Email.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to store an Email
 
Remarks:
        This class should be used out of the Springfield.Components assembly, so it should be declared as an public class.
             
Environment:
		Class Library/.NET Framework 2.0 

Project:
        Springfield.Components for MSRA Intern Application Tracking System (MIATS), codename: Springfield;
        MSRA/MS^2.3/Compass Team

Revision History:			
		30/Apr/2006 Created by Yuan Chen

*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Components.BizObjects
{
    public class Email
    {
        #region Private Members
        private int emailId;
        private int priority;
        private EmailFormatEnum bodyFormat;
        private string from;
        private string to;
        private string cc;
        private string bcc;
        private string subject;
        private string body;
        private DateTime nextTryTime;
        private int numberOfTries;
        private Guid relatedUser;
        private bool isSend;
        #endregion

        #region Constructors
        public Email()
        {
            emailId = 0;
            priority = 0;
            from = string.Empty;
            to = string.Empty;
            cc = string.Empty;
            bcc = string.Empty;
            subject = string.Empty;
            body = string.Empty;
            nextTryTime = DateTime.Now;
            numberOfTries = 0;
            relatedUser = Guid.Empty;
            IsSend = false;
        }

        //public Email(int id)
        //{

        //} 
        #endregion

        #region Methods
        public void Insert()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            emailId = dp.InsertEmail(this);
        }

        public void Update()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.UpdateEmail(this);
        } 
        #endregion

        #region Static Helper Methods
        public static Email GetEmailById(int id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetEmailById(id);
        }

        public static void DeleteEmailById(int id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.DeleteEmail(id);
        }

        public static DataSet GetAllUnsentEmails()
        {
            return null;
        }

        public static void MarkEmailAsSend(int id)
        { 
        
        }
        #endregion

        #region Properties
        public int EmailId
        {
            get { return emailId; }
            set { emailId = value; }
        }

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public EmailFormatEnum BodyFormat
        {
            get { return bodyFormat; }
            set { bodyFormat = value; }
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

        public DateTime NextTryTime
        {
            get { return nextTryTime; }
            set { nextTryTime = value; }
        }

        public int NumberOfTries
        {
            get { return numberOfTries; }
            set { numberOfTries = value; }
        }

        public bool IsSend
        {
            get { return isSend; }
            set { isSend = value; }
        }

        public Guid RelatedUserId
        {
            get { return relatedUser; }
            set { relatedUser = value; }
        }
        #endregion
    }
}
