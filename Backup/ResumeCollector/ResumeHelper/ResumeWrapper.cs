using System;
using System.Collections.Generic;
using System.Text;
using ResumeCollector.Lib;
using System.Reflection;
using System.Web.Security;
using System.Xml;
using ResumeCollector.CustomExceptions;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.BizObjects;
using ResumeCollector.Exception;

namespace ResumeCollector.ResumeHelper
{
    public class ResumeWrapper
    {
        private int m_CurrentStep;
        private ApplicantBasicInfo m_BasicInfo;
        private ApplicantEduBackground m_EduBackground;
        private ApplicantRelatedInfo m_RelatedInfo;
        private ResumeScheme m_Scheme;
        private bool m_IsUpdate;
        private string m_UserEmail;
        private string m_Username;
        private MembershipUser m_Applicant;
        private Guid m_CurId;

        private const int FULL_STEPS = 3;
        private const string EMAIL_PATH = "/Applicant/ApplicantBasicInfo/Email";
        private const string FIRST_NAME = "/Applicant/ApplicantBasicInfo/FirstName";
        private const string LAST_NAME = "/Applicant/ApplicantBasicInfo/LastName";

        public event EventHandler Step;

        public string ApplicantEmail
        {
            get { return m_UserEmail; }
        }

        public string ApplicantName
        {
            get { return m_Username; }
        }

        public ResumeWrapper(string fileName, string password)
        {
            m_IsUpdate = false;
            m_CurrentStep = 0;
            m_Scheme = new ResumeScheme(fileName, password);
            m_Scheme.Load();
        }

        private string GetApplicantEmail()
        {
            XmlNode emailNode = ResumeMapping.GetMappingNode(EMAIL_PATH);
            PropertyStruct property = new PropertyStruct(emailNode, EMAIL_PATH);
            string email = m_Scheme.GetTextBoxValue(1, property.MappingName);
            if (string.IsNullOrEmpty(email))
            {
                throw new PropertyIsEmptyException("Email");
            }
            else if (!GlobalHelper.ValidateEmail(email))
            {
                throw new PropertyIsInvalidException("Email");
            }
            return email;
        }

        private string GetUsername()
        {
            XmlNode firstnameNode = ResumeMapping.GetMappingNode(FIRST_NAME);
            XmlNode lastnameNode = ResumeMapping.GetMappingNode(LAST_NAME);

            PropertyStruct firstname = new PropertyStruct(firstnameNode, FIRST_NAME);
            PropertyStruct lastname = new PropertyStruct(lastnameNode, LAST_NAME);

            string fname = m_Scheme.GetTextBoxValue(1, lastname.MappingName);
            string lname = m_Scheme.GetTextBoxValue(1, firstname.MappingName);

            if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname))
            {
                throw new PropertyIsEmptyException("LastName + FirstName");
            }

            return lname + " " + fname;
        }

        public void ProcessResume()
        {
            //judge whether applicant is exist
            try
            {
                m_UserEmail = GetApplicantEmail();
            }
            catch
            {
                throw;
            }
            finally
            {
                try
                {
                    m_Username = GetUsername();
                }
                catch
                {
                    throw new PropertyIsEmptyException("UserName");
                }
            }
            m_Applicant = Membership.GetUser(m_UserEmail);
            if (m_Applicant == null)
            {
                m_IsUpdate = false;
                Membership.CreateUser(m_UserEmail, GlobalHelper.PasswordGenerator(7, true, true, true, false, false), m_UserEmail);
            }
            else
            {
                m_IsUpdate = true;
            }

            m_CurId = SiteUser.GetIdByFullName(m_UserEmail);

            try
            {
                PopulateApplicantBasicInfo();
                StepNext();
                PopulateApplicantEduBackground();
                StepNext();
                PopulateApplicantRelatedInfo();
                StepNext();
            }
            catch (System.Exception ex)
            {
                try
                {
                    if (!m_IsUpdate)
                    {
                        Membership.DeleteUser(m_UserEmail, true);
                        ApplicantBasicInfo.DeleteApplicantBasicInfoById(m_CurId);
                        ApplicantEduBackground.DeleteApplicantEduBackgroundById(m_CurId);
                        ApplicantRelatedInfo.DeleteApplicantRelatedInfoById(m_CurId);
                    }
                }
                finally
                {
                    throw ex;
                }
            }
            
            //if (m_IsUpdate)
            //{
            //    m_BasicInfo.Update();
            //    m_EduBackground.Update();
            //    m_RelatedInfo.Update();
            //}
            //else
            //{
            //    m_BasicInfo.Insert();
            //    m_EduBackground.Insert();
            //    m_RelatedInfo.Insert();
            //}
        }

        private void PopulateApplicantBasicInfo()
        {
            bool isUpdate = true;
            m_BasicInfo = ApplicantBasicInfo.GetApplicantBasicInfoById(m_CurId);
            if(m_BasicInfo == null)
            {
                m_BasicInfo = new ApplicantBasicInfo();
                m_BasicInfo.ApplicantId = m_CurId;
                isUpdate = false;
            }
            m_BasicInfo.ApplicationTime = DateTime.Now;
            List<PropertyStruct> propertyList = ResumeMapping.GetBasicInfoPropertyList();
            Type curType = typeof(ApplicantBasicInfo);
            PropertyInfo propertyInfo;
            IPropertyHandler curHandler;

            try
            {
                foreach (PropertyStruct property in propertyList)
                {
                    propertyInfo = curType.GetProperty(property.PropertyName);
                    curHandler = PropertyHandlerFactory.GetHandler(property.Control);
                    curHandler.PopulateProperty(propertyInfo, property, m_Scheme, m_BasicInfo);
                }
            }
            catch (SchemaFormatException)
            {
                throw new SchemaFormatException("Be sure the application form(xls file) and the mapping file have the same schema.");
            }

            if (isUpdate)
            {
                m_BasicInfo.Update();
            }
            else
            {
                m_BasicInfo.Insert();
            }
        }

        private void PopulateApplicantEduBackground()
        {
            bool isUpdate = true;
            m_EduBackground = ApplicantEduBackground.GetApplicantEduBackgroundById(m_CurId);
            if(m_EduBackground == null)
            {
                m_EduBackground = new ApplicantEduBackground();
                m_EduBackground.ApplicantId = m_CurId;
                isUpdate = false;
            }
            List<PropertyStruct> propertyList = ResumeMapping.GetEduBackgroundPropertyList();
            Type curType = typeof(ApplicantEduBackground);
            PropertyInfo propertyInfo;
            IPropertyHandler curHandler;

            try
            {
                foreach (PropertyStruct property in propertyList)
                {
                    propertyInfo = curType.GetProperty(property.PropertyName);
                    curHandler = PropertyHandlerFactory.GetHandler(property.Control);
                    curHandler.PopulateProperty(propertyInfo, property, m_Scheme, m_EduBackground);
                }
            }
            catch (SchemaFormatException)
            {
                throw new SchemaFormatException("Be sure the application form(xls file) and the mapping file have the same schema.");
            }

            if (isUpdate)
            {
                m_EduBackground.Update();
            }
            else
            {
                m_EduBackground.Insert();
            }
        }

        private void PopulateApplicantRelatedInfo()
        {
            bool isUpdate = true;
            m_RelatedInfo = ApplicantRelatedInfo.GetApplicantRelatedInfoById(m_CurId);
            if(m_RelatedInfo == null)
            {
                m_RelatedInfo = new ApplicantRelatedInfo();
                m_RelatedInfo.ApplicantId = m_CurId;
                isUpdate = false;
            }
            List<PropertyStruct> propertyList = ResumeMapping.GetRelatedInfoPropertyList();
            Type curType = typeof(ApplicantRelatedInfo);
            PropertyInfo propertyInfo;
            IPropertyHandler curHandler;

            //special populate
            m_RelatedInfo.JobInfoCategory = "Job Board"; //job board

            try
            {
                foreach (PropertyStruct property in propertyList)
                {
                    propertyInfo = curType.GetProperty(property.PropertyName);
                    curHandler = PropertyHandlerFactory.GetHandler(property.Control);
                    curHandler.PopulateProperty(propertyInfo, property, m_Scheme, m_RelatedInfo);
                }
            }
            catch (SchemaFormatException)
            {
                throw new SchemaFormatException("Be sure the application form(xls file) and the mapping file have the same schema.");
            }

            if (isUpdate)
            {
                m_RelatedInfo.Update();
            }
            else
            {
                m_RelatedInfo.Insert();
            }
        }

        private void StepNext()
        {
            m_CurrentStep++;
            OnStep();
        }

        protected void OnStep()
        {
            if (Step != null)
            {
                Step.Invoke(this, EventArgs.Empty);
            }
        }

        public void Release()
        {
            m_Scheme.Release();
            m_CurrentStep = 0;
        }
    }
}
