using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ResumeCollector.CustomExceptions;
using MSRA.SpringField.Components;

namespace ResumeCollector.ResumeHelper
{
    public class CollegeSelectorHandler : IPropertyHandler
    {
        #region IPropertyHandler Members

        private const string SELECTOR = "List";
        private const string OTHER = "Other";

        public void PopulateProperty(System.Reflection.PropertyInfo propertyInfo, PropertyStruct propertyStruct, ResumeScheme resumeScheme, object parentObj)
        {
            XmlNode selector;
            XmlNode other;

            PropertyStruct selectorProperty;
            PropertyStruct otherProperty;

            StringBuilder selectorPath = new StringBuilder(ResumeMapping.EDU_BACKGROUND_PATH);
            selectorPath.Append("/");
            selectorPath.Append(propertyStruct.PropertyName);
            selectorPath.Append("/");
            selectorPath.Append(SELECTOR);
            string selectorPathStr = selectorPath.ToString();

            StringBuilder otherPath = new StringBuilder(ResumeMapping.EDU_BACKGROUND_PATH);
            otherPath.Append("/");
            otherPath.Append(propertyStruct.PropertyName);
            otherPath.Append("/");
            otherPath.Append(OTHER);
            string otherPathStr = otherPath.ToString();

            selector = ResumeMapping.GetMappingNode(selectorPathStr);
            other = ResumeMapping.GetMappingNode(otherPathStr);

            selectorProperty = new PropertyStruct(selector, string.Empty);
            otherProperty = new PropertyStruct(other, string.Empty);

            string selectorValue = resumeScheme.GetComboBoxValue(1, selectorProperty.MappingName);
                        
            if ((selectorValue == StaticData.OTHER_COLLEGE) || University.isInContinentList(selectorValue) || University.isInCountryList(selectorValue))
            {
                string otherValue = resumeScheme.GetTextBoxValue(1, otherProperty.MappingName);
                if (string.IsNullOrEmpty(otherValue))
                {
                    throw new PropertyIsEmptyException(String.Format("{0}{1}", !String.IsNullOrEmpty(propertyStruct.DataValue)? String.Format("{0} - ", propertyStruct.DataValue):"", propertyStruct.PropertyName));
                }
                propertyInfo.SetValue(parentObj, otherValue, null);
            }
            else
            {
                if (string.IsNullOrEmpty(selectorValue))
                {
                    throw new PropertyIsEmptyException(String.Format("{0}{1}", !String.IsNullOrEmpty(propertyStruct.DataValue) ? String.Format("{0} - ", propertyStruct.DataValue) : "", propertyStruct.PropertyName));
                }
                propertyInfo.SetValue(parentObj, selectorValue, null);
            }
        }

        #endregion
    }
}
