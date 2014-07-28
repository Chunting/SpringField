using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ResumeCollector.ResumeHelper;
using ResumeCollector.CustomExceptions;
using MSRA.SpringField.Components.BizObjects;

namespace ResumeCollector.ResumeHelper
{
    public class JobInfoDetailHandler : IPropertyHandler
    {
        #region IPropertyHandler Members

        private const string ITEM = "Item";

        public void PopulateProperty(System.Reflection.PropertyInfo propertyInfo, PropertyStruct propertyStruct, ResumeScheme resumeScheme, object parentObj)
        {
            ApplicantRelatedInfo ari = parentObj as ApplicantRelatedInfo;

            switch (ari.JobInfoSource)
            {
                //Referral
                //case StaticData.InfoSource[5]:
                case "Referral":
                    PopulateDetail(propertyInfo, propertyStruct, resumeScheme, parentObj, "Referral");
                    break;
/*
                case "Web Site":
                    PopulateDetail(propertyInfo, propertyStruct, resumeScheme, parentObj, "Web Site");
                    break;
                case "Promotion":
                    PopulateDetail(propertyInfo, propertyStruct, resumeScheme, parentObj, "Promotion");
                    break;
                case "Talent Program":
                    PopulateDetail(propertyInfo, propertyStruct, resumeScheme, parentObj, "Talent Program");
                    break;
 * */
                //BBS
                //case StaticData.InfoSource[1]:
                //case "BBS":
                //    PopulateDetail(propertyInfo, propertyStruct, resumeScheme, parentObj, 1);
                //    break;
                //Website
                //case StaticData.InfoSource[0]:
                //case "Web Site":
                //    PopulateDetail(propertyInfo, propertyStruct, resumeScheme, parentObj, 0);
                //    break;
                default:
                    propertyInfo.SetValue(parentObj, "", null);
                    break;
                    //throw new PropertyIsEmptyException(propertyStruct.PropertyName);
            }
        }

        private void PopulateDetail(System.Reflection.PropertyInfo propertyInfo, PropertyStruct propertyStruct, ResumeScheme resumeScheme, object parentObj, string infoSource)
        {
            string itemPath = propertyStruct.XPath + "/" + ITEM;
            XmlNode curItem;
            PropertyStruct curStruct;
            //IPropertyHandler curHandler;

            itemPath += "[@value='" + infoSource + "']";
            curItem = ResumeMapping.GetMappingNode(itemPath);
            curStruct = new PropertyStruct(curItem, string.Empty);
            //curHandler = PropertyHandlerFactory.GetHandler(curStruct.Control);
            //curHandler.PopulateProperty(propertyInfo, curStruct, resumeScheme, parentObj);
            string valueObj = resumeScheme.GetTextBoxValue(1, curStruct.MappingName);
            if (string.IsNullOrEmpty(valueObj) && curStruct.Required)
            {
                throw new PropertyIsEmptyException(String.Format("{0}{1}", !String.IsNullOrEmpty(propertyStruct.DataValue) ? String.Format("{0} - ", propertyStruct.DataValue) : "", propertyStruct.PropertyName));
            }
            propertyInfo.SetValue(parentObj, valueObj, null);
        }
        #endregion
    }
}
