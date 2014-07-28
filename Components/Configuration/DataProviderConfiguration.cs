/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		DataProviderConfiguration.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to store the DataProvider configuration in Web.Config file

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
using System.Xml;
using System.Collections;
using System.Collections.Specialized;

namespace MSRA.SpringField.Components.Configuration
{
    /// <summary>
    /// class used to store a DataProvider's configurations
    /// </summary>
    public class DataProviderConfiguration
    {
        #region Private members
        private string name;
        private string providerType;
        private NameValueCollection providerAttributes = new NameValueCollection();
        #endregion

        #region Constructor
        public DataProviderConfiguration(XmlAttributeCollection attributeCollection)
        { 
            // Set the name of the provider
            name = attributeCollection["name"].Value;

            // Set the type of the provider
            providerType = attributeCollection["type"].Value;

            // Store all the attributes in the attributes bucket
            foreach (XmlAttribute attribute in attributeCollection){

                if ((attribute.Name != "name") && (attribute.Name != "type"))
                    providerAttributes.Add(attribute.Name, attribute.Value);
            }
        }
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
        }

        public string TypeName
        {
            get { return providerType; }
        }

        public NameValueCollection Attributes
        {
            get { return providerAttributes; }
        }
        #endregion
    }
}
