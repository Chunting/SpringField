using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using MSRA.SpringField.Components;

namespace MSRA.SpringField.Application.Controls
{
    public partial class InfoSourceGenerator : System.Web.UI.UserControl
    {
        private XmlDocument xml;
        private const string CATEGORY = "Category";
        private const string SOURCE = "Source";
        private const string CHANNEL = "Channel";
        private const string DETAIL = "Detail";
        private const string CONTROL = "control";
        private const string NAME = "name";
        private const string TEXT = "text";
        private const string DDL = "DropDownList";
        private const string TB = "TextBox";
        private const string NONE = "None";

        protected void Page_Load(object sender, EventArgs e)
        {
            xml = new XmlDocument();

            string fileName = Server.MapPath("~/InfoSourceMapping.xml");
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(fileName, settings);
            xml.Load(reader);

            if (!IsPostBack)
            {
                ddlCategory.DataSource = StaticData.InfoCategory;
                ddlCategory.DataBind();
                ddlCategory.Items[0].Selected = true;
                Category = StaticData.InfoCategory[0];
                BindSourceList();
                ddlSource.Items[0].Selected = true;
                Source = ddlSource.SelectedItem.Text;
                BindChannelPanel();
                if (ddlChannel.Visible == true)
                {
                    ddlChannel.Items[0].Selected = true;
                    tbChannel.Text = ddlChannel.SelectedItem.Text;
                }
            }
        }

        #region GetNodeMethod
        private XmlNode GetCategoryNode(string category)
        {
            XmlNode categoryNode = null;
            foreach (XmlNode node in xml.DocumentElement.ChildNodes)
            {
                if (node.Name == CATEGORY && node.Attributes[TEXT].Value == category)
                {
                    categoryNode = node;
                    break;
                }
            }
            if (categoryNode == null)
            {
                throw new Exception("can't find category node");
            }
            return categoryNode;
        }

        private XmlNode GetSourceNode(string category, string source)
        {
            XmlNode categoryNode = GetCategoryNode(category);
            XmlNode sourceNode = null;

            foreach (XmlNode node in categoryNode.ChildNodes)
            {
                if (node.Name == SOURCE && node.Attributes[TEXT].Value == source)
                {
                    sourceNode = node;
                    break;
                }
            }

            if (sourceNode == null)
            {
                throw new Exception("can't find source node");
            }
            return sourceNode;
        }

        private XmlNode GetChannelNode(string category, string source, string channel)
        {
            XmlNode sourceNode = GetSourceNode(category, source);
            XmlNode channelNode = null;

            foreach (XmlNode node in sourceNode.ChildNodes)
            {
                if (node.Name == CHANNEL && node.Attributes[TEXT].Value == channel)
                {
                    channelNode = node;
                    break;
                }
            }

            if (channelNode == null)
            {
                throw new Exception("can't find channel node");
            }
            return channelNode;
        }
        #endregion

        #region GetListMethod
        private List<string> GetSourceList(string category)
        {
            XmlNode categoryNode = GetCategoryNode(category);
            List<string> list = new List<string>();
            foreach (XmlNode node in categoryNode.ChildNodes)
            {
                list.Add(node.Attributes[TEXT].Value);
            }
            return list;
        }

        private List<string> GetChannelList(string category, string source)
        {
            XmlNode sourceNode = GetSourceNode(category, source);
            List<string> list = new List<string>();
            foreach (XmlNode node in sourceNode.ChildNodes)
            {
                list.Add(node.Attributes[TEXT].Value);
            }
            return list;
        }
        #endregion

        #region Helper Method
        private void AddNewLine(Control parent)
        {
            parent.Controls.Add(new LiteralControl("<br />"));
        }
        #endregion

        #region Bind Method
        private void BindSourceList()
        {
            ddlSource.Items.Clear();
            ddlSource.DataSource = GetSourceList(Category);
            ddlSource.DataBind();
            ddlSource.AutoPostBack = true;
            ddlSource.SelectedIndexChanged += new EventHandler(ddlSource_SelectedIndexChanged);
        }

        private void BindChannelPanel()
        {
            XmlNode sourceNode = GetSourceNode(Category, Source);
            if (sourceNode.Attributes[CONTROL].Value == DDL)
            {
                ddlChannel.Visible = true;
                //tbChannel.Visible = false;
                tbChannel.Enabled = false;
                ddlChannel.Items.Clear();
                ddlChannel.DataSource = GetChannelList(Category, Source);
                ddlChannel.DataBind();
                ddlChannel.AutoPostBack = true;
                ddlChannel.SelectedIndexChanged += new EventHandler(ddlChannel_SelectedIndexChanged);
                ddlChannel.Items[0].Selected = true;
                tbChannel.Text = ddlChannel.SelectedItem.Text;
            }
            else
            {
                ddlChannel.Visible = false;
                tbChannel.Enabled = true;
                tbChannel.Text = string.Empty;
            }
        }
        #endregion

        #region Event Handler
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Category = ddlCategory.SelectedItem.Text;
            BindSourceList();
            Source = ddlSource.SelectedItem.Text;
            BindChannelPanel();
        }

        protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            Source = ddlSource.SelectedItem.Text;

            BindChannelPanel();
        }

        protected void ddlChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbChannel.Text = ddlChannel.SelectedItem.Text;
        }
        #endregion

        #region Properties
        public string Category
        {
            get { return ViewState[CATEGORY].ToString(); }
            set { ViewState[CATEGORY] = value; }
        }

        public string Source
        {
            get { return ViewState[SOURCE].ToString(); }
            set { ViewState[SOURCE] = value; }
        }

        //public string Channel
        //{
        //    get { return ViewState[CHANNEL].ToString(); }
        //    set { ViewState[CHANNEL] = value; }
        //}

        //public string Detail
        //{
        //    get { return ViewState[DETAIL].ToString(); }
        //    set { ViewState[DETAIL] = value; }
        //}

        public string Channel
        {
            get { return tbChannel.Text; }
        }

        public string Detail
        {
            get { return tbDetail.Text; }
        }
        #endregion
    }
}