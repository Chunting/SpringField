using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ResumeCollector.ResumeHelper;
using ResumeCollector.CustomExceptions;
using MSRA.SpringField.Components.BizObjects;

namespace ResumeCollector.ResumeHelper
{
    public class InternAdvisorObjectHandler : IPropertyHandler
    {

        #region IPropertyHandler Members

        private const string FIRST_NAME = "FirstName";
        private const string LAST_NAME = "LastName";
        private const string EMAIL = "Email";
        private const string ORG = "Organization";

        public void PopulateProperty(System.Reflection.PropertyInfo propertyInfo, PropertyStruct propertyStruct, ResumeScheme resumeScheme, object parentObj)
        {
            XmlNode firstnameNode, lastnameNode, emailNode, orgNode;
            PropertyStruct firstnameStruct, lastnameStruct, emailStruct, orgStruct;

            string firstnamePath = propertyStruct.XPath + "/" + FIRST_NAME;
            string lastnamePath = propertyStruct.XPath + "/" + LAST_NAME;
            string emailPath = propertyStruct.XPath + "/" + EMAIL;
            string orgPath = propertyStruct.XPath + "/" + ORG;

            firstnameNode = ResumeMapping.GetMappingNode(firstnamePath);
            lastnameNode = ResumeMapping.GetMappingNode(lastnamePath);
            emailNode = ResumeMapping.GetMappingNode(emailPath);
            orgNode = ResumeMapping.GetMappingNode(orgPath);

            firstnameStruct = new PropertyStruct(firstnameNode, string.Empty);
            lastnameStruct = new PropertyStruct(lastnameNode, string.Empty);
            emailStruct = new PropertyStruct(emailNode, string.Empty);
            orgStruct = new PropertyStruct(orgNode, string.Empty);

            Advisor curAdvisor = new Advisor();
            curAdvisor.FirstName = resumeScheme.GetTextBoxValue(1, firstnameStruct.MappingName);
            curAdvisor.LastName = resumeScheme.GetTextBoxValue(1, lastnameStruct.MappingName);
            curAdvisor.Email = resumeScheme.GetTextBoxValue(1, emailStruct.MappingName);
            curAdvisor.Organization = resumeScheme.GetTextBoxValue(1, orgStruct.MappingName);

            propertyInfo.SetValue(parentObj, curAdvisor, null);
        }

        #endregion
    }
}
