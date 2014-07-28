using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ResumeCollector.ResumeHelper;
using ResumeCollector.CustomExceptions;
using MSRA.SpringField.Components;

namespace ResumeCollector.ResumeHelper
{
    public class OptionGroupHandler : IPropertyHandler
    {
        #region IPropertyHandler Members

        private const string ITEM = "Item";

        public void PopulateProperty(System.Reflection.PropertyInfo propertyInfo, PropertyStruct propertyStruct, ResumeScheme resumeScheme, object parentObj)
        {
            XmlNodeList itemList;
            PropertyStruct curProperty;

            string itemPath = propertyStruct.XPath + "/" + ITEM;
            itemList = ResumeMapping.GetMappingNodeList(itemPath);
            foreach (XmlNode node in itemList)
            {
                curProperty = new PropertyStruct(node, string.Empty);
                if (resumeScheme.GetOptionButtonValue(1, curProperty.MappingName))
                {
                    propertyInfo.SetValue(parentObj, EnumHelper.StringToEnum(Type.GetType(propertyStruct.DataType), curProperty.DataValue), null);
                    return;
                }
            }
            if (propertyStruct.Required)
            {
                throw new PropertyIsEmptyException(String.Format("{0}{1}", !String.IsNullOrEmpty(propertyStruct.DataValue) ? String.Format("{0} - ", propertyStruct.DataValue) : "", propertyStruct.PropertyName));
            }
            else
            {
                try
                {
                    string defaultValue = propertyStruct.Attributes["defaultValue"];
                    if (defaultValue != null)
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
                    else
                    {
                        throw new System.Exception("No default attribute for not required property [" + propertyStruct.PropertyName + "]");
                    }
                    
                }
                catch (System.Exception e)
                {
                    throw new PropertyIsEmptyException(String.Format("{0}{1}", !String.IsNullOrEmpty(propertyStruct.DataValue) ? String.Format("{0} - ", propertyStruct.DataValue) : "", propertyStruct.PropertyName));
                }
            }
        }

        #endregion
    }
}
