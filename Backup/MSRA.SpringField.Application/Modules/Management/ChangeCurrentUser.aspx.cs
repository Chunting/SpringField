using System;
using System.Xml;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MSRA.SpringField.Components.BizObjects;

namespace MSRA.SpringField.Application.Management
{
    public partial class Management_ChangeCurrentUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = SiteUser.Current.Alias;
        }
        protected void SetCurrentUserAlias(string alias)
        {
            string strPath = Context.Server.MapPath("../web.config");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strPath);

            XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xmlDoc.NameTable);
            xmlnsManager.AddNamespace("nc", "http://schemas.microsoft.com/.NetConfiguration/v2.0");

            XmlNodeList nodeList = xmlDoc.SelectNodes("/nc:configuration/nc:connectionStrings/nc:add", xmlnsManager);
            foreach (XmlNode node in nodeList)
            {
                if (node.Attributes["name"].Value == "current_user_test")
                {
                    node.Attributes["connectionString"].Value = alias;
                }
            }
            xmlDoc.Save(strPath);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (RadioButton1.Checked)
            {
                SetCurrentUserAlias("fareast\\xcui");
            }
            else if (RadioButton2.Checked)
            {
                SetCurrentUserAlias("fareast\\bainguo");
            }
            else if (RadioButton3.Checked)
            {
                SetCurrentUserAlias("mslpa\\v-yuanwu");  //modify by Yuanqin 2011.1.5
            }
            else if (RadioButton4.Checked)
            {
                SetCurrentUserAlias(TextBox1.Text);
            }
        }
    }
}