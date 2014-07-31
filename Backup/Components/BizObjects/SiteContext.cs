/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		SiteContext.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to wrap HttpContext for current request
 
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

namespace MSRA.SpringField.Components.BizObjects
{
    public class SiteContext
    {
        private HttpContext currentContext;

        public SiteContext()
        {
            currentContext = HttpContext.Current;
        }

        public static SiteContext Current
        {
            get {
                if (HttpContext.Current.Items["CurrentSiteContext"] == null)
                {
                    HttpContext.Current.Items.Add("CurrentSiteContext", new SiteContext());
                }
                return HttpContext.Current.Items["CurrentSiteContext"] as SiteContext;
            }
        }

        public SiteUser CurrentUser
        {
            get { return SiteUser.Current; }
        }

        public HttpContext Context
        {
            get { return currentContext; }
        }
    }
}
