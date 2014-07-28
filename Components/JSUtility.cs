using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace MSRA.SpringField.Components
{
    public static class JSUtility
    {
        private static readonly Type cstype = typeof(Page);

        public static void Alert(Page page, string text)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"JavaScript\">");
            sb.Append("\n<!--");
            sb.Append("\nwindow.alert(\"");
            sb.Append(text);
            sb.Append("\");");
            sb.Append("\n//-->");
            sb.Append("\n</script>");
            //page.RegisterClientScriptBlock("sf_alert", sb.ToString());
            if (!page.ClientScript.IsClientScriptBlockRegistered(cstype, "sf_alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(cstype, "sf_alert", sb.ToString(), false);
            }
        }

        public static void CloseWindow(Page page)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"JavaScript\">");
            sb.Append("\n<!--");
            sb.Append("\nwindow.opener =null;window.close();");
            sb.Append("\n//-->");
            sb.Append("\n</script>");
            //page.RegisterClientScriptBlock("sf_close_window", sb.ToString());
            if (!page.ClientScript.IsClientScriptBlockRegistered(cstype, "sf_close_window"))
            {
                page.ClientScript.RegisterClientScriptBlock(cstype, "sf_close_window", sb.ToString(), false);
            }
        }

        public static void OpenNewWindow(Page page, string url, string pattern)
        {
            if (pattern == null || pattern == string.Empty)
            { 
                pattern = "height=700,width=760,status=no,toolbar=no,menubar=no,location=no,resizable=no,scrollbars=yes";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"JavaScript\">");
            sb.Append("\n");
            sb.Append("\nwindow.open('");
            sb.Append(url);
            sb.Append("',null,'");
            sb.Append(pattern);
            sb.Append("');");
            sb.Append("\n");
            sb.Append("\n</script>");
            //page.RegisterClientScriptBlock("sf_open_window", sb.ToString());
            if (!page.ClientScript.IsClientScriptBlockRegistered(cstype, "sf_open_window"))
            {
                page.ClientScript.RegisterClientScriptBlock(cstype, "sf_open_window", sb.ToString(), false);
            }
        }

        public static void RedirectOpenerLocation(Page page)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"JavaScript\">");
            sb.Append("\n<!--");
            sb.Append("\nif (window.opener && !window.opener.closed)window.opener.location.href=window.opener.location.href;");
            sb.Append("\n//-->");
            sb.Append("\n</script>");
            //page.RegisterClientScriptBlock("sf_redirection", sb.ToString());
            if (!page.ClientScript.IsClientScriptBlockRegistered(cstype, "sf_redirection"))
            {
                page.ClientScript.RegisterClientScriptBlock(cstype, "sf_redirection", sb.ToString(), false);
            }
        }
        public static void RedirectSelf(Page page)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"JavaScript\">");
            sb.Append("\n");
            sb.Append("\nwindow.location.href=window.location.href;");
            sb.Append("\n");
            sb.Append("\n</script>");
            //page.RegisterClientScriptBlock("sf_redirection", sb.ToString());
            if (!page.ClientScript.IsClientScriptBlockRegistered(cstype, "sf_redirection"))
            {
                page.ClientScript.RegisterClientScriptBlock(cstype, "sf_redirection", sb.ToString(), false);
            }
        }
        public static void RedirectLocation(Page page, string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"JavaScript\">");
            sb.Append("\n<!--");
            sb.Append("\nlocation.href='");
            sb.Append(url);
            sb.Append("';");
            sb.Append("\n//-->");
            sb.Append("\n</script>");
            //page.RegisterClientScriptBlock("sf_redirection", sb.ToString());
            if (!page.ClientScript.IsClientScriptBlockRegistered(cstype, "sf_redirection"))
            {
                page.ClientScript.RegisterClientScriptBlock(cstype, "sf_redirection", sb.ToString(), false);
            }
        }

        public static void ComfirmSelection(Page page, string firstConfirm, string secondConfirm, string scriptName, System.Web.UI.WebControls.DropDownList ddl)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"JavaScript\">");
            sb.Append("\n<!--");
            sb.Append("\nfunction ");
            sb.Append(scriptName);
            sb.Append("()\n{\n  var ddl = document.getElementById('");
            sb.Append(ddl.UniqueID);
            sb.Append("');\n  if(ddl.selectedIndex == 0)\n  {\n  return confirm('");
            sb.Append(firstConfirm);
            sb.Append("');\n  }\n  else\n  {\n  return confirm('");
            sb.Append(secondConfirm);
            sb.Append("');\n  }\n");
            sb.Append("}\n//-->");
            sb.Append("\n</script>");

            if (!page.ClientScript.IsClientScriptBlockRegistered(cstype, scriptName))
            {
                page.ClientScript.RegisterClientScriptBlock(cstype, scriptName, sb.ToString(), false);
            }
        }
    }
}
