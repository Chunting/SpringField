/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		SiteUser.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to store current site user
 
Remarks:
        This class should be used out of the Springfield.Components assembly, so it should be declared as an public class.
             
Environment:
		Class Library/.NET Framework 2.0 

Project:
        Springfield.Components for MSRA Intern Application Tracking System (MIATS), codename: Springfield;
        MSRA/MS^2.3/Compass Team

Revision History:			
		25/Apr/2006 Created by Yuan Chen
        09/June/2009 Add GetUserIdByAlias Method by Yi Shao

*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Components.BizObjects
{
    public class SiteUser
    {
        public const string EMPLOYEE_MAIL_EXTENTION = "@microsoft.com";
        public const string INTERN_MAIL_EXTENTION = "@msrchina.research.microsoft.com";

        private HttpContext curContext;
        private string alias;
        private string domainName;
        private string emailAddress;
        private string fullName;
        private Guid siteUserId;
        private string m_RealName;
        private string m_GroupName;
        private MembershipUser m_RawUser;

        public SiteUser()
        {
            //set anonymous properties
            curContext = HttpContext.Current;
            alias = "AnonymousUser";
            domainName = "Deny";
            fullName = "Deny\\AnonymousUser";
            emailAddress = string.Empty;
        }

        public SiteUser(string full_name)
        {
            fullName = full_name.ToLower();
            curContext = HttpContext.Current;
            string[] nameArr = fullName.Split('\\');

            if (nameArr.Length == 2)
            {
                domainName = nameArr[0];
                alias = nameArr[1];
            }
            else
            {
                throw new Exception(FullName + ", where are you from?");
            }

            string[] roles = Roles.GetRolesForUser(fullName);

            //first logon then create a user for him
            if (roles == null || roles.Length == 0)
            {
                if (StaticData.FTEDict.ContainsKey(alias))
                {
                    Roles.AddUserToRole(fullName, RoleType.DefaultUser.ToString());//默认用户
                }
                else
                {
                    Roles.AddUserToRole(fullName, RoleType.AnonymousUser.ToString());//匿名
                }
            }

            InitCommentInfo();

            siteUserId = SiteUser.GetIdByFullName(fullName);

            switch (domainName.ToLower())
            { 
                case "MSLPA":
                    emailAddress = alias + INTERN_MAIL_EXTENTION;
                    break;
                case "fareast":
                    emailAddress = alias + EMPLOYEE_MAIL_EXTENTION;
                    break;

                //we assume all other users have employee mail extention. 
                default:
                    emailAddress = alias + EMPLOYEE_MAIL_EXTENTION;
                    break;
                    //throw new Exception("unrecognize domain name");
            }
        }

        public static SiteUser Current
        {
            get {
                if (HttpContext.Current.Items["CurrentUser"] == null)
                {
                    HttpContext.Current.Items.Add("CurrentUser", new SiteUser());//匿名用户
                }
                //SiteUser sUser = new SiteUser("fareast\\msratfs1");
                //HttpContext.Current.Items["CurrentUser"] = sUser;
                return HttpContext.Current.Items["CurrentUser"] as SiteUser;
            }
        }

        public static Guid GetIdByFullName(string fullName)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetUserIdByFullName(fullName);
        }

        public static string GetAliasByUserId(Guid userId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            string userFullName = dp.GetFullNameByUserId(userId);
            string[] userNameArr = userFullName.Split('\\');
            if (userNameArr.Length > 1)
            {
                return userNameArr[1];
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Added by Yi Shao at 2009-6-9
        /// </summary>
        /// <param name="Alias"></param>
        /// <returns></returns>
        public static string GetUserIdByAlias(string Alias)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetUserIdByAlias(Alias);
        }

        public static string[] SplitFullName(string fullName)
        {
            string[] userNameArr = fullName.Split('\\');
            return userNameArr;
        }

        public string[] GetRoles()
        {
            return Roles.GetRolesForUser(fullName);
        }

        public bool IsInRole(string roleType)
        {
            return Roles.IsUserInRole(fullName, roleType);
        }

        public bool IsInRole(RoleType roleType)
        {
            return Roles.IsUserInRole(fullName, roleType.ToString());
        }

        public static string[] SplitComment(string comment)
        {
            string[] strArr = comment.Split('|');
            return strArr;
        }

        private void InitCommentInfo()
        {
            m_RawUser = Membership.GetUser(fullName);
            if (m_RawUser == null)
            {
                //should we create membership for each people?
            }
            else
            {
                string[] strArr = SplitComment(m_RawUser.Comment);
                if (strArr.Length == 2)
                {
                    m_RealName = String.IsNullOrEmpty(strArr[0]) ? fullName : strArr[0];
                    m_GroupName = String.IsNullOrEmpty(strArr[1]) ? "N/A" : strArr[1];
                }
                else
                {
                    m_RealName = fullName;
                    m_GroupName = "N/A";
                }
            }
        }

        public void UpdateCommentInfo()
        {
            if (m_RawUser == null)
            {
                m_RawUser = Membership.CreateUser(fullName, GlobalHelper.PasswordGenerator(7, true, true, true, false, false), emailAddress);
            }
            m_RawUser.Comment = string.Format("{0}|{1}", m_RealName, m_GroupName);
            Membership.UpdateUser(m_RawUser);
        }

        public string Alias
        {
            get { return alias; }
            //set { alias = vlaue; }
        }

        public string DomainName
        {
            get { return domainName; }
            //set { domainName = value; }
        }

        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        public string FullName
        {
            get { return fullName; }
        }

        public Guid SiteUserId
        {
            get { return siteUserId; }
        }

        public MembershipUser RawUser
        {
            get { return m_RawUser; }
        }

        public string RealName
        {
            get { return m_RealName; }
            set {
                if (!String.IsNullOrEmpty(value))
                {
                    m_RealName = value;
                }
                else
                {
                    m_RealName = fullName;
                }
            }
        }

        public string GroupName
        {
            get { return m_GroupName; }
            set {
                if (!String.IsNullOrEmpty(value))
                {
                    m_GroupName = value;
                }
                else
                {
                    m_GroupName = "N/A";
                }
            }
        }
    }
}
