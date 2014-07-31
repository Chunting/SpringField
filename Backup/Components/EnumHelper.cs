/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		EnumHelper.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to do the convert between enumeration and string/integer

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

namespace MSRA.SpringField.Components
{
    /// <summary>
    /// Class use to do convert between enumeration and string/integer
    /// </summary>
    public static class EnumHelper
    { 
        /// <summary>
        /// Convert a integer value to enumeration
        /// </summary>
        /// <param name="enumType">Type of enum</param>
        /// <param name="enumValue">Integer value to be converted</param>
        /// <returns>Enum object</returns>
        public static object IntegerToEnum(System.Type enumType, int enumValue)
        {
            object enumObject = Enum.Parse(enumType,Enum.GetName(enumType,enumValue));
            return enumObject;
        }

        /// <summary>
        /// Convert a string to enumeration
        /// </summary>
        /// <param name="enumType">Type of enum</param>
        /// <param name="enumName">String that specific an member</param>
        /// <returns>Enum object</returns>
        public static object StringToEnum(System.Type enumType, string enumName)
        {
            object enumObject = Enum.Parse(enumType, enumName, false);
            return enumObject;
        }

        /// <summary>
        /// Get all enum integer values
        /// </summary>
        /// <param name="enumType">Type of enumeration</param>
        /// <returns>Array of integer values</returns>
        public static int[] GetEnumIntValues(System.Type enumType)
        {
            int len = Enum.GetValues(enumType).Length;
            int[] myArr = new int[len];
            Array.Copy(Enum.GetValues(enumType), myArr, len);
            return myArr;
        }

        /// <summary>
        /// Get all enum strings
        /// </summary>
        /// <param name="enumType">Type of enumeration</param>
        /// <returns>Array of string values</returns>
        public static string[] GetEnumStrings(System.Type enumType)
        {
            int len = Enum.GetNames(enumType).Length;
            string[] myArr = new string[len];
            Array.Copy(Enum.GetNames(enumType), myArr, len);
            return myArr;
        }

        /// <summary>
        /// Convert a enum member to a integer
        /// </summary>
        /// <param name="enumObject">Object of enum</param>
        /// <returns>Int value</returns>
        public static int EnumToInteger(object enumObject)
        {
            return Convert.ToInt32(enumObject);
        }

        /// <summary>
        /// Convert a enum member to a string
        /// </summary>
        /// <param name="enumObject">Object of enum</param>
        /// <returns>String of the member name</returns>
        public static string EnumToString(object enumObject)
        {
            return enumObject.ToString();
        }
    }
}