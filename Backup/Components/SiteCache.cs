/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		SiteCache.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to wrap the HttpRuntime.Cache

Remarks:
        This class should be used out of the Springfield.Components assembly, so it should be declared as an public class.
             
Environment:
		Class Library/.NET Framework 2.0 

Project:
        Springfield.Components for MSRA Intern Application Tracking System (MIATS), codename: Springfield;
        MSRA/MS^2.3/Compass Team

Revision History:			
		27/Apr/2006 Created by Yuan Chen

*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Collections;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Components
{
    /// <summary>
    /// Class used to wrap the HttpRuntime.Cache
    /// </summary>
    public static class SiteCache
    {
        private static readonly Cache siteCache;

        static SiteCache()
        {
            HttpContext context = HttpContext.Current;
            if (context != null){
                siteCache = context.Cache;
            }
            else {
                siteCache = HttpRuntime.Cache;
            }
        }

        public static object Get(string key)
        {
            return siteCache.Get(key);
        }

        public static void Insert(string key, Object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        { 
            siteCache.Insert(key, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemoveCallback);
        }

        public static void Insert(string key, object value)
        { 
            siteCache.Insert(key,value);
        }

        public static void Insert(string key, object value, TimeSpan slidingExpiration)
        {
            siteCache.Insert(key, value, null, Cache.NoAbsoluteExpiration, slidingExpiration);
        }

        public static void Insert(string key, object value, CacheDependency dependencies)
        {
            siteCache.Insert(key, value, dependencies);
        }

        public static void Insert(string key, object value, string dependentFilePath)
        {
            CacheDependency dep = new CacheDependency(dependentFilePath);
            siteCache.Insert(key, value, dep);
        }

        public static void Insert(string key, object value, DateTime absoluteExpiration)
        {
            siteCache.Insert(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }

        public static void InsertMax(string key, object value)
        {
            siteCache.Insert(key, value, null, DateTime.MaxValue, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }

        public static void InsertMax(string key, object value, CacheItemRemovedCallback onRemoveCallback)
        {
            siteCache.Insert(key, value, null, DateTime.MaxValue, Cache.NoSlidingExpiration, CacheItemPriority.High, onRemoveCallback);
        }

        public static void Clear()
        {
            IDictionaryEnumerator cacheEnum = siteCache.GetEnumerator();
            ArrayList keyArray = new ArrayList();
            while (cacheEnum.MoveNext()){
                keyArray.Add(cacheEnum.Key);
            }
            foreach (string key in keyArray){
                siteCache.Remove(key);
            }
        }

        public static object Remove(string key)
        {
            return siteCache.Remove(key);
        }

        public static TimeSpan DefaultExpiration
        {
            get {
                string cacheKey = "DefaultExpiration";
                if (SiteCache.Get(cacheKey) == null)
                {
                    SiteConfiguration config = SiteConfiguration.GetConfig();
                    int defaultExpiration = Convert.ToInt32(config.SiteAttributes["cacheExpiration"]);
                    SiteCache.InsertMax(cacheKey, new TimeSpan(0, defaultExpiration, 0));
                }
                return (TimeSpan)SiteCache.Get(cacheKey);
            }
        }
    }
}
