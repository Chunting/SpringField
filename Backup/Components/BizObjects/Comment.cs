/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		Comment.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to store the comment related with a Applicant

Remarks:
        This class should be used out of the Springfield.Components assembly, so it should be declared as an public class.
             
Environment:
		Class Library/.NET Framework 2.0 

Project:
        Springfield.Components for MSRA Intern Application Tracking System (MIATS), codename: Springfield;
        MSRA/MS^2.3/Compass Team

Revision History:			
		21/Aug/2006 Created by Yuan Chen

*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Components.BizObjects
{
    public class Comment
    {
        #region Private members
        private int m_CommentId;
        private DateTime m_CommentTime;
        private Guid m_Commenter;
        private string m_CommenterAlias;
        private string m_CommentContent;
        private Guid m_ApplicantId;
        #endregion

        #region Constructor
        public Comment()
        {
            m_CommentId = 0;
            m_CommentTime = DateTime.Now;
            m_Commenter = SiteUser.Current.SiteUserId;
            m_CommenterAlias = SiteUser.Current.FullName;
            m_CommentContent = string.Empty;
        }
        #endregion

        #region Public Methods
        public void Insert()
        { 
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.InsertComment(this);
        }

        public void Update()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.UpdateComment(this);
        }
        #endregion

        #region Static Methods
        public static void DeleteCommentById(int id)
        { 
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.DeleteComment(id);
        }

        public static Comment GetCommentById(int id)
        { 
            //string cacheKey = "comment_" + id.ToString();
            //if(SiteCache.Get(cacheKey) == null)
            //{
            //    IDataProvider dp = DataProviderFactory.GetDataProvider();
            //    SiteCache.Insert(cacheKey, dp.GetCommentById(id), SiteCache.DefaultExpiration);
            //}
            //return SiteCache.Get(cacheKey) as Comment;
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            Comment comment  = dp.GetCommentById(id);
            return comment;
        }

        public static DataSet GetCommentsForApplicant(Guid applicantId)
        {
            //string cacheKey = "comment_" + applicantId.ToString();
            //if (SiteCache.Get(cacheKey) == null)
            //{
            //    IDataProvider dp = DataProviderFactory.GetDataProvider();
            //    SiteCache.Insert(cacheKey, dp.GetCommentForApplicant(applicantId), SiteCache.DefaultExpiration);
            //}
            //return SiteCache.Get(cacheKey) as DataSet;
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            DataSet ds = dp.GetCommentForApplicant(applicantId);
            return ds;
        }
        #endregion

        #region Properties
        public DateTime CommentTime
        {
            get { return m_CommentTime; }
            set { m_CommentTime = value; }
        }

        public Guid Commenter
        {
            get { return m_Commenter; }
            set { m_Commenter = value; }
        }

        public string CommenterFullName
        {
            get { return m_CommenterAlias; }
            set { m_CommenterAlias = ((value == null) ? string.Empty : value); }
        }

        public Guid ApplicantId
        {
            get { return m_ApplicantId; }
            set { m_ApplicantId = value; }
        }

        public string CommentContent
        {
            get { return m_CommentContent; }
            set { m_CommentContent = ((value == null) ? string.Empty : value); }
        }

        public int CommentId
        {
            get{ return m_CommentId; }
            set{ m_CommentId = value; }
        }
        #endregion
    }
}
