/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		SiteConfigurationHandler.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class implement the System.Configuration.IConfigurationSectionHandler interface,
        and used to handle the site configuration in Web.Config file

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
using System.Text;
using System.Configuration;

namespace MSRA.SpringField.Components.Configuration
{
    /// <summary>
    /// class used by ASP.NET Configuration to load Springfield configurations
    /// </summary>
    public class SiteConfigurationHandler : System.Configuration.IConfigurationSectionHandler
    {

        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            SiteConfiguration config = new SiteConfiguration();
            config.LoadValueFromConfig(section);
            return config;
        }

        #endregion
    }
}
