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

namespace ResumeCollector.ResumeHelper
{
    public class PaperUploaderHandler : IPropertyHandler
    {
        #region IPropertyHandler Members

        private const string ITEM = "Item";
        private const string MAPPING = "mapping";

        public void PopulateProperty(System.Reflection.PropertyInfo propertyInfo, PropertyStruct propertyStruct, ResumeScheme resumeScheme, object parentObj)
        {
            bool isUpdateA, isUpdateB;
            Document paperA = null;
            Document paperB = null;
            ApplicantEduBackground aeb = parentObj as ApplicantEduBackground;

            if (aeb.Papers[0].DocId == 0 || aeb.Papers[0] == null)
            {
                isUpdateA = false;
                paperA = new Document();
            }
            else
            {
                isUpdateA = true;
                paperA = aeb.Papers[0];
            }

            if (aeb.Papers[1].DocId == 0 || aeb.Papers[1] == null)
            {
                isUpdateB = false;
                paperB = new Document();
            }
            else
            {
                isUpdateB = true;
                paperB = aeb.Papers[1];
            }

            string itemPath = propertyStruct.XPath + "/" + ITEM;
            XmlNodeList itemList = ResumeMapping.GetMappingNodeList(itemPath);

            XmlNode paperANode = itemList[0];
            XmlNode paperBNode = itemList[1];

            string paperAName = resumeScheme.GetTextBoxValue(1, paperANode.Attributes[MAPPING].Value);
            string paperBName = resumeScheme.GetTextBoxValue(1, paperBNode.Attributes[MAPPING].Value);

            if (!string.IsNullOrEmpty(paperAName))
            {
                UploadPaper(aeb, paperA, paperAName, "_paper1", isUpdateA);
                aeb.Papers[0] = paperA;
            }

            if (!string.IsNullOrEmpty(paperBName))
            {
                UploadPaper(aeb, paperB, paperBName, "_paper2", isUpdateB);
                aeb.Papers[1] = paperB;
            }
        }

        private void UploadPaper(ApplicantEduBackground aeb, Document paper, string paperName, string addition, bool isUpdate)
        {
            string curPath = CollectionTask.CurrentDirectory + "\\" + paperName;

            if (File.Exists(curPath))
            {
                string originalName = Path.GetFileName(curPath);
                string ext = Path.GetExtension(curPath).ToLower();
                SiteConfiguration siteConfig = SiteConfiguration.GetConfig();

                paper.ApplicantId = aeb.ApplicantId;
                paper.OriginalName = originalName;
                paper.DocType = DocumentEnum.Paper;
                paper.SaveName = aeb.ApplicantId.ToString() + addition + ext;

                //rename the file and copy to the dest folder
                FileInfo curPaper = new FileInfo(curPath);
                string destPath = Path.Combine(siteConfig.SiteAttributes["docPath"], paper.SaveName);
                curPaper.CopyTo(destPath, true);

                if (!isUpdate)
                {
                    paper.Insert();
                }
                else
                {
                    paper.Update();
                }
            }
            else
            {
                throw new PpaerInvailidException();
            }
        }

        #endregion
    }
}
