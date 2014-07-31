using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ResumeCollector.CustomExceptions;

namespace ResumeCollector.ResumeHelper
{
    public class StringCheckBoxGroupHandler : IPropertyHandler
    {
        #region IPropertyHandler Members

        private const string ITEM = "Item";

        public void PopulateProperty(System.Reflection.PropertyInfo propertyInfo, PropertyStruct propertyStruct, ResumeScheme resumeScheme, object parentObj)
        {
            XmlNodeList itemList;
            PropertyStruct curProperty;
            StringBuilder sb = new StringBuilder();

            string itemPath = propertyStruct.XPath + "/" + ITEM;
            itemList = ResumeMapping.GetMappingNodeList(itemPath);
            foreach (XmlNode node in itemList)
            {
                curProperty = new PropertyStruct(node, string.Empty);
                if (resumeScheme.GetCheckBoxValue(1, curProperty.MappingName))
                {
                    //Choosed other
                    if (curProperty.DataValue == "Other")
                    {
                        string otherValue = "Other";
                        //If have a child element named "Other", get the text from it
                        if (node.ChildNodes.Count > 0 && node.ChildNodes[0].Name=="Other")
                        {
                            curProperty = new PropertyStruct(node.ChildNodes[0], string.Empty);
                            otherValue = resumeScheme.GetTextBoxValue(1, curProperty.MappingName);
                        }
                        sb.Append(otherValue);
                        sb.Append(";");
                    }
                    else
                    {
                        sb.Append(curProperty.DataValue);
                        sb.Append(";");
                    }
                }
            }

            string valueObj = sb.ToString();

            if (propertyStruct.Required && String.IsNullOrEmpty(valueObj))
            {
                throw new PropertyIsEmptyException(String.Format("{0}{1}", !String.IsNullOrEmpty(propertyStruct.DataValue) ? String.Format("{0} - ", propertyStruct.DataValue) : "", propertyStruct.PropertyName));
            }

            propertyInfo.SetValue(parentObj, Convert.ChangeType(valueObj, Type.GetType(propertyStruct.DataType)), null);
        }

        #endregion
    }
}
