using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ResumeCollector.ResumeHelper;
using ResumeCollector.CustomExceptions;
using ResumeCollector.Lib;
using System.IO;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components.Configuration;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components;

namespace ResumeCollector.ResumeHelper
{
    public class ResumeUploaderHandler : IPropertyHandler
    {
        #region IPropertyHandler Members

        public void PopulateProperty(System.Reflection.PropertyInfo propertyInfo, PropertyStruct propertyStruct, ResumeScheme resumeScheme, object parentObj)
        {
            bool isUpdate;
            Document resume = null;
            ApplicantEduBackground aeb = parentObj as ApplicantEduBackground;

            if (aeb.Resume.DocId == 0 || aeb.Resume == null)
            {
                isUpdate = false;
                resume = new Document();
            }
            else
            {
                isUpdate = true;
                resume = aeb.Resume;
            }

            string resumeName = resumeScheme.GetTextBoxValue(1, propertyStruct.MappingName);

            if(string.IsNullOrEmpty(resumeName))
            {
                throw new PropertyIsEmptyException(String.Format("{0}{1}", !String.IsNullOrEmpty(propertyStruct.DataValue) ? String.Format("{0} - ", propertyStruct.DataValue) : "", propertyStruct.PropertyName));
            }

            string curPath = CollectionTask.CurrentDirectory + "\\" + resumeName;
            string originalName = Path.GetFileName(curPath);
            string ext = Path.GetExtension(curPath).ToLower();
            SiteConfiguration siteConfig = SiteConfiguration.GetConfig();
            FileInfo resumeFile = null;
            //bool fFound = false;
            if (File.Exists(curPath))
            {
                resumeFile = new FileInfo(curPath);
                //fFound = true;
            //}
            //else
            //{
            //    DirectoryInfo di = new DirectoryInfo(CollectionTask.CurrentDirectory);
            //    if (null != di)
            //    {
            //        // Check resume in doc format
            //        if (di.GetFiles("*.doc").Length > 0)
            //        {
            //            resumeFile = di.GetFiles("*.doc")[0];
            //            fFound = true;
            //        }
            //        // Check resume in docx format
            //        else if (di.GetFiles("*.docx").Length > 0)
            //        {
            //            resumeFile = di.GetFiles("*.docx")[0];
            //            fFound = true;
            //        }
            //        // Check resume in pdf format
            //        else if (di.GetFiles("*.pdf").Length > 0)
            //        {
            //            resumeFile = di.GetFiles("*.pdf")[0];
            //            fFound = true;
            //        }
                    
            //    }
            //}

            //// Final action upon the resume existance
            //if (fFound)
            //{
                resume.ApplicantId = aeb.ApplicantId;
                resume.OriginalName = resumeFile.Name;
                resume.DocType = DocumentEnum.CV;
                resume.SaveName = aeb.ApplicantId.ToString() + "_cv" + resumeFile.Extension;

                aeb.ResumeExt = resumeFile.Extension;
                aeb.ResumeImage = GlobalHelper.GetFileImage(resumeFile.FullName);

                //rename the file and copy to the dest folder
                string destPath = Path.Combine(siteConfig.SiteAttributes["docPath"], resume.SaveName);
                resumeFile.CopyTo(destPath, true);

                if (!isUpdate)
                {
                    resume.Insert();
                }
                else
                {
                    resume.Update();
                }
                aeb.Resume = resume;
 
            }
            else
            {
                throw new ResumeInvailidException();
            }
        }

        #endregion
    }
}
