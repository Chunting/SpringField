using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ResumeCollector.ResumeHelper;
using ResumeCollector.CustomExceptions;
using MSRA.SpringField.Components.BizObjects;

namespace ResumeCollector.ResumeHelper
{
    public class JobInfoChannelHandler : IPropertyHandler
    {
        #region IPropertyHandler Members

        private const string ITEM = "Item";

        public void PopulateProperty(System.Reflection.PropertyInfo propertyInfo, PropertyStruct propertyStruct, ResumeScheme resumeScheme, object parentObj)
        {
            ApplicantRelatedInfo ari = parentObj as ApplicantRelatedInfo;

            switch(ari.JobInfoSource)
            {
                //Referral
                //case StaticData.InfoSource[5]:
                case "Referral":
                    PopulateChannel(propertyInfo, propertyStruct, resumeScheme, parentObj, "Referral");
                    break;
                //Job Fair
                //case StaticData.InfoSource[9]:
                //case "Job Fair":
                //    PopulateChannel(propertyInfo, propertyStruct, resumeScheme, parentObj, 8);
                //    break;
                //BBS
                //case StaticData.InfoSource[1]:
                //case "BBS":
                //    PopulateChannel(propertyInfo, propertyStruct, resumeScheme, parentObj, 1);
                //    break;
                //Website
                //case StaticData.InfoSource[0]:
                case "Web Site":
                    PopulateChannel(propertyInfo, propertyStruct, resumeScheme, parentObj, "Web Site");
                    break;
                //Campus Event
                ////case StaticData.InfoSource[12]:
                //case "Campus Event":
                //    PopulateChannel(propertyInfo, propertyStruct, resumeScheme, parentObj, 6);
                //    break;
                //Contest
                //case StaticData.InfoSource[8]:
                //case "Contest":
                //    PopulateChannel(propertyInfo, propertyStruct, resumeScheme, parentObj, 7);
                //    break;
                //Other
                //case StaticData.InfoSource[20]:
                case "Promotion":
                    PopulateChannel(propertyInfo, propertyStruct, resumeScheme, parentObj, "Promotion");
                    break;
                case "Talent Program":
                    PopulateChannel(propertyInfo, propertyStruct, resumeScheme, parentObj, "Talent Program");
                    break;
                default:
                    propertyInfo.SetValue(parentObj, "",null);
                    break;
                //throw new PropertyIsEmptyException(String.Format("{0}{1}", String.IsNullOrEmpty(propertyStruct.DataValue)? String.Format("{0} - ", propertyStruct.DataValue):"", propertyStruct.PropertyName));
            }
        }

        private void PopulateChannel(System.Reflection.PropertyInfo propertyInfo, PropertyStruct propertyStruct, ResumeScheme resumeScheme, object parentObj, string infoSource)
        {
            string itemPath = propertyStruct.XPath + "/" + ITEM;
            XmlNode curItem;
            PropertyStruct curStruct;
            IPropertyHandler curHandler;
            itemPath += "[@value='" + infoSource + "']";
            //itemPath += "[@value='" + StaticData.InfoSource[index] + "']";
            curItem = ResumeMapping.GetMappingNode(itemPath);
            curStruct = new PropertyStruct(curItem, string.Empty);
            curHandler = PropertyHandlerFactory.GetHandler(curStruct.Control);
            curHandler.PopulateProperty(propertyInfo, curStruct, resumeScheme, parentObj);
        }

        #endregion
    }
}
