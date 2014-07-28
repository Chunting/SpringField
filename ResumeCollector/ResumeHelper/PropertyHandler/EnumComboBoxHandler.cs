using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ResumeCollector.ResumeHelper;
using ResumeCollector.CustomExceptions;
using MSRA.SpringField.Components;

namespace ResumeCollector.ResumeHelper
{
    public class EnumComboBoxHandler : IPropertyHandler
    {
        #region IPropertyHandler ≥…‘±

        private const string ITEM = "Item";
        private const string VALUE = "value";
        private const string MAPPING = "mapping";

        public void PopulateProperty(System.Reflection.PropertyInfo propertyInfo, PropertyStruct propertyStruct, ResumeScheme resumeScheme, object parentObj)
        {
            XmlNodeList itemList;

            string itemPath = propertyStruct.XPath + "/" + ITEM;

            itemList = ResumeMapping.GetMappingNodeList(itemPath);
            string comboxValue = resumeScheme.GetComboBoxValue(1, propertyStruct.MappingName);
            foreach (XmlNode node in itemList)
            {
                if (node.Attributes[MAPPING].Value.Trim() == comboxValue)
                {
                    propertyInfo.SetValue(parentObj, EnumHelper.StringToEnum(Type.GetType(propertyStruct.DataType), node.Attributes[VALUE].Value), null);
                    return;
                }
            }
            // code runs here means the applicant miss the rank field in the applyform
            // We will with it with Others
            
            if ( !propertyStruct.Required )
            {
                string defaultValue = propertyStruct.Attributes["defaultValue"];
                if (defaultValue == null)
                {
                    throw new System.Exception("No default attribute for not required property [" + propertyStruct.PropertyName + "]");
                }
                else
                {
                    try
                    {
                        propertyInfo.SetValue(parentObj, EnumHelper.StringToEnum(Type.GetType(propertyStruct.DataType), defaultValue), null);
                    }
                    catch
                    {
                        throw new System.Exception("Default value for property [" + propertyStruct.PropertyName + "] is invalid!");
                    }
                }
            }else
            {
                throw new System.Exception("Required property [" + propertyStruct.PropertyName + "]");
            }
        }

        #endregion
    }
}
