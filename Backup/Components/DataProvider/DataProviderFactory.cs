/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		DataProviderFactory.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to generate DataProvider instance due to the configuration in Web.Config file
 
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
using System.Reflection;

namespace MSRA.SpringField.Components.Configuration
{
    public static class DataProviderFactory
    {
        public static IDataProvider GetDataProvider()
        {
            //read configuration in Web.Config <springfiled> configsection
            SiteConfiguration config = SiteConfiguration.GetConfig();
            DataProviderConfiguration dpConfig = config.DataProviders[config.DefaultDataProvider] as DataProviderConfiguration;
            Type dataProviderType = null;

            //is cache exists
            if (SiteCache.Get("DataProvider") == null)
            {
                try
                {
                    //load Type from the assembly
                    dataProviderType = Type.GetType(dpConfig.TypeName);
                    Type[] paramType = new Type[1];
                    paramType[0] = typeof(DataProviderConfiguration);

                    //insert to cache 
                    SiteCache.InsertMax("DataProvider", dataProviderType.GetConstructor(paramType));
                }
                catch {
                    throw new Exception("unable to load DataProvider");
                }
            }

            //use parameter to get DataProvider instance
            object[] paramValue = new object[1];
            paramValue[0] = dpConfig;
            return ((ConstructorInfo)SiteCache.Get("DataProvider")).Invoke(paramValue) as IDataProvider;
        }
    }
}
