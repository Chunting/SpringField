/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		Document.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to store document information for Applicant
 
Remarks:
        This class should be used out of the Springfield.Components assembly, so it should be declared as an public class.
             
Environment:
		Class Library/.NET Framework 2.0 

Project:
        Springfield.Components for MSRA Intern Application Tracking System (MIATS), codename: Springfield;
        MSRA/MS^2.3/Compass Team

Revision History:			
		30/Apr/2006 Created by Yuan Chen
         1/May/2006 Modified by Yuan Chen -- Add Methods and Static Helper Methods

*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Components.BizObjects
{
    [Serializable]
    public class Document
    {
        #region Private Members
        private int docId;
        private DocumentEnum docType;
        private string originalName;
        private Guid applicantId;
        private string saveName;
        #endregion

        #region Constructors
        public Document()
        {
            docId = 0;
            originalName = string.Empty;
            applicantId = Guid.Empty;
            saveName = string.Empty;
        }

        //public Document(int id)
        //{

        //} 
        #endregion

        #region Methods
        public void Insert()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            docId = dp.InsertDocument(this);
        }

        public void Update()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.UpdateDocument(this);
        } 
        #endregion

        #region Static Helper Methods
        public static void DeleteDocumentById(int id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.DeleteDocument(id);
        }

        public static Document GetDocumentById(int id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetDocumentById(id);
        } 
        #endregion

        #region Properties
        public int DocId
        {
            get { return docId; }
            set { docId = value; }
        }

        public DocumentEnum DocType
        {
            get { return docType; }
            set { docType = value; }
        }

        public string OriginalName
        {
            get { return originalName; }
            set { originalName = ((value == null) ? string.Empty : value); }
        }

        public Guid ApplicantId
        {
            get { return applicantId; }
            set { applicantId = value; }
        }

        public string SaveName
        {
            get { return saveName; }
            set { saveName = ((value == null) ? string.Empty : value); }
        }
        #endregion
    }
}
