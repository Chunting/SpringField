using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using VBCtrl = Microsoft.Vbe.Interop.Forms;

namespace ResumeCollector.ResumeHelper
{
    public class ResumeScheme
    {
        private string m_Filename;
        private Excel.Application m_Application;
        private Excel.Workbook m_Workbook;
        private object m_Password;

        //mapping objects
        public static readonly global::System.Object MissingValue = global::System.Reflection.Missing.Value;
        public static readonly global::System.Object TrueValue = true;
        public static readonly global::System.Object FalseValue = false;

        public ResumeScheme(string fileName, string password)
        {
            if (File.Exists(fileName))
            {
                m_Filename = fileName;
            }
            else
            {
                throw new System.IO.FileNotFoundException("ResumeScheme can not load such file", fileName);
            }

            if (!String.IsNullOrEmpty(password))
            {
                m_Password = password;
            }
            else
            {
                m_Password = MissingValue;
            }

            try
            {
                m_Application = ExcelApplication.GetApplication();
            }
            catch (System.Exception)
            {
                throw new System.Exception("Please be sure the installation of office excel in your local machine.");
            }
        }

        public void Load()
        {
            m_Workbook = m_Application.Workbooks.Open(m_Filename, MissingValue, FalseValue,
                                                      MissingValue, m_Password, m_Password,
                                                      MissingValue, MissingValue, MissingValue,
                                                      TrueValue, MissingValue, MissingValue,
                                                      MissingValue, MissingValue, MissingValue);
        }

        public Excel.Sheets Worksheets
        {
            get { return m_Workbook.Worksheets as Excel.Sheets; }
        }

        public Excel.Worksheet GetWorksheet(object index)
        {
            return m_Workbook.Worksheets[index] as Excel.Worksheet;
        }

        public string FileName
        {
            get { return m_Filename; }
            set { m_Filename = value; }
        }

        public Excel.Workbook CurWorkbook
        {
            get { return m_Workbook; }
        }

        public void Release()
        {
            if (m_Workbook != null)
            {
                //m_Workbook.Save();
                m_Workbook.Close(false, MissingValue, false);
                Marshal.FinalReleaseComObject(m_Workbook);
                m_Workbook = null;
            }
        }

        public string GetTextBoxValue(object sheetIndex, string textBoxName)
        {
            Excel.Worksheet sheet = m_Workbook.Worksheets[sheetIndex] as Excel.Worksheet;
            Excel.OLEObject obj = sheet.OLEObjects(textBoxName) as Excel.OLEObject;
            VBCtrl.TextBox tb = obj.Object as VBCtrl.TextBox;
            return tb.Text == null ? "" : tb.Text.Trim();
        }

        public bool GetOptionButtonValue(object sheetIndex, string optionButtonName)
        {
            Excel.Worksheet sheet = m_Workbook.Worksheets[sheetIndex] as Excel.Worksheet;
            Excel.OLEObject obj = sheet.OLEObjects(optionButtonName) as Excel.OLEObject;
            VBCtrl.OptionButton ob = obj.Object as VBCtrl.OptionButton;
            return Convert.ToBoolean(ob.get_Value());
        }

        public bool GetCheckBoxValue(object sheetIndex, string checkBoxName)
        {
            Excel.Worksheet sheet = m_Workbook.Worksheets[sheetIndex] as Excel.Worksheet;
            Excel.OLEObject obj = sheet.OLEObjects(checkBoxName) as Excel.OLEObject;
            VBCtrl.CheckBox cb = obj.Object as VBCtrl.CheckBox;
            return Convert.ToBoolean(cb.get_Value());
        }

        public string GetComboBoxValue(object sheetIndex, string comboBoxName)
        {
            Excel.Worksheet sheet = m_Workbook.Worksheets[sheetIndex] as Excel.Worksheet;
            Excel.OLEObject obj = sheet.OLEObjects(comboBoxName) as Excel.OLEObject;
            VBCtrl.ComboBox cb = obj.Object as VBCtrl.ComboBox;
            return cb.Text == null ? "" : cb.Text.Trim() ;
        }

        public Excel.OLEObject GetOLEObject(object sheetIndex, string objectName)
        {
            Excel.Worksheet sheet = m_Workbook.Worksheets[sheetIndex] as Excel.Worksheet;
            Excel.OLEObject obj = sheet.OLEObjects(objectName) as Excel.OLEObject;
            
            return obj;
        }
    }
}
