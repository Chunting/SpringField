/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		Email.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to work as an security module
 
Remarks:
        This class should be used out of the Springfield.Components assembly, so it should be declared as an public class.
             
Environment:
		Class Library/.NET Framework 2.0 

Project:
        Springfield.Components for MSRA Intern Application Tracking System (MIATS), codename: Springfield;
        MSRA/MS^2.3/Compass Team

Revision History:			
		25/Apr/2006 Created by Yuan Chen

*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Security;
using System.Security.Principal;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components.Enumerations;

namespace MSRA.SpringField.Components.HttpModule
{
    public class SiteSecurityModule : IHttpModule
    {
        #region IHttpModule Members

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.PostAuthenticateRequest += new EventHandler(context_PostAuthenticateRequest);//1
            context.PostAuthorizeRequest    += new EventHandler(context_PostAuthorizeRequest);//2
        }

        //2
        void context_PostAuthorizeRequest(object sender, EventArgs e)
        {
            if (SiteUser.Current.IsInRole(RoleType.AnonymousUser))
            {
                HttpContext.Current.Server.Transfer("~/AccessDeny.htm", false);
            }
        }


        //1
        void context_PostAuthenticateRequest(object sender, EventArgs e)
        {
            InitCurrentUser();
        }

        #endregion

        private void InitCurrentUser()
        {
            string fullName;
            IIdentity userIdentity = HttpContext.Current.User.Identity;
            WindowsIdentity winIdentity = userIdentity as WindowsIdentity;
            //for use
            fullName = winIdentity.Name;//获取用户的 Windows 登录名
            //for test
            if (System.Configuration.ConfigurationManager.AppSettings["enable_test_account"].ToString() == "true")
            {
                fullName = System.Configuration.ConfigurationManager.AppSettings["current_user_test"].ToString();//调试任意一个用户
            }
            string cacheKey = "user_" + fullName;
            if (SiteCache.Get(cacheKey) == null)//缓存里取不出就新建
            {
                SiteCache.Insert(cacheKey, new SiteUser(fullName), SiteCache.DefaultExpiration);
            }
            SiteUser curUser = SiteCache.Get(cacheKey) as SiteUser;
            //generate roles for current users
            GenericPrincipal curPrincipal = new GenericPrincipal(userIdentity, Roles.GetRolesForUser(fullName));
            HttpContext.Current.User = curPrincipal;

            HttpContext.Current.Items.Add("CurrentUser", curUser);
        }
    }
}
