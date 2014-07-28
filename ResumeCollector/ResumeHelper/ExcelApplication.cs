using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace ResumeCollector.ResumeHelper
{
    //singleton
    public static class ExcelApplication
    {
        private static Excel.Application curApplication;

        public static Excel.Application GetApplication()
        {
            if (null == curApplication)
            {
                curApplication = new Microsoft.Office.Interop.Excel.ApplicationClass();
            }
            return curApplication;
        }

        public static void Quit()
        {
            if (curApplication != null)
            {
                curApplication.Quit();
                Marshal.FinalReleaseComObject(curApplication);
                curApplication = null;
            }
        }
    }
}
