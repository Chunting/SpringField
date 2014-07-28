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
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components;

namespace MSRA.SpringField.Application.Keyin
{
    public partial class InfoSourceGenerator : System.Web.UI.UserControl
    {
        private XmlDocument xml;
        private static readonly string CATEGORY = "Category";
        private static readonly string SOURCE = "Source";
        private static readonly string CHANNEL = "Channel";
        //    private static readonly string DETAIL = "Detail";
        private static readonly string CONTROL = "control";
        //    private static readonly string NAME = "name";
        private static readonly string TEXT = "text";
        private static readonly string DDL = "DropDownList";
        private static readonly string TB = "TextBox";
        //    private static readonly string NONE = "None";
        private static readonly String CURRENTID = "CurrentID";

        //private int GetDDLSelectedIndex(DropDownList ddl, string selectText)
        //{
        //    for (int i = 0; i < ddl.Items.Count; i++)
        //    {
        //        if (ddl.Items[i].Text == selectText)
        //        {
        //            // found
        //            return i;
        //        }
        //    }

        //    // not found
        //    return -1;

        //}

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
                ApplicantRelatedInfo ari = ApplicantRelatedInfo.GetApplicantRelatedInfoById(CurrentID);

                ddlCategory.DataSource = StaticData.InfoCategory;
                ddlCategory.DataBind();
                ddlCategory.Items[0].Selected = true;
                Category = StaticData.InfoCategory[0];

                BindSourceList();
                if (ari != null && ari.JobInfoSource != string.Empty)
                {
                    Source = ari.JobInfoSource;
                }
                else
                {
                    Source = ddlSource.Items[0].Value;
                }
                // Set selected source
                if (ari != null)
                {
                    ddlSource.SelectedValue = ari.JobInfoSource;
                }

                BindChannelPanel();
                tbChannel.Visible = false;
                if (ddlChannel.Visible == true)
                {
                    // Set selected channel if we can
                    if (ari != null)
                    {
                        ddlChannel.SelectedValue = ari.JobInfoChannel;
                    }
                    BindDetailPanel();
                }
                // Set selected channel to textbox
                if (ari != null)
                {
                    tbChannel.Text = ari.JobInfoChannel;
                    tbDetail.Text = ari.JobInfoDetail;
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
            //if (categoryNode == null)
            //{
            //    throw new Exception("can't find category node");
            //}
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
                throw new Exception("can't find channel node" + channel);
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
            //ddlSource.AutoPostBack = true;
            // ddlSource.SelectedIndexChanged += new EventHandler(ddlSource_SelectedIndexChanged);
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

                lbChannelDescription.Visible = false;
            }
            else if (sourceNode.Attributes[CONTROL].Value == TB)
            {
                ddlChannel.Visible = false;
                tbChannel.Visible = true;
                tbChannel.Enabled = true;
                tbChannel.Text = string.Empty;

                lbChannelDescription.Visible = true;
                if (GetChannelList(Category, Source).Count > 0)
                {
                    lbChannelDescription.Text = GetChannelList(Category, Source)[0];
                }
                else
                {
                    lbChannelDescription.Text = "";
                }
            }
            else
            {
                ddlChannel.Visible = false;
                lbChannelDescription.Visible = false;
                tbChannel.Visible = false;
                tbChannel.Enabled = false;
                tbChannel.Text = string.Empty;
            }
        }

        private void BindDetailPanel()
        {
            lbDetailDescription.Visible = false;
            tbDetail.Visible = false;
            if (Channel != string.Empty)
            {
                XmlNode channelNode = GetChannelNode(Category, Source, Channel);
                if (channelNode.Attributes[CONTROL].Value == TB)
                {
                    XmlNode detailNode = channelNode.SelectSingleNode("/Info/Category[@text = '" + Category + "']/Source[@text = '" + Source + "']/Channel[@text = '" + Channel + "']/Detail[1]");
                    if (detailNode == null) throw new Exception("detail node is null");
                    tbDetail.Visible = true;
                    lbDetailDescription.Text = detailNode.Attributes[TEXT].Value;
                    lbDetailDescription.Visible = true;
                }
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
            BindDetailPanel();
        }

        protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            Source = ddlSource.SelectedItem.Text;

            BindChannelPanel();
            BindDetailPanel();
        }

        protected void ddlChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbDetailDescription.Visible = false;
            tbChannel.Text = ddlChannel.SelectedItem.Text;
            BindDetailPanel();
        }
        #endregion

        #region Properties
        public string Category
        {
            get { return ViewState[CATEGORY].ToString(); }
            set
            {
                ViewState[CATEGORY] = value;
            }
        }

        public string Source
        {
            get { return ViewState[SOURCE].ToString(); }
            set
            {
                ViewState[SOURCE] = value;
            }
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
            set { tbChannel.Text = value; }
        }

        public string Detail
        {
            get { return tbDetail.Text; }
            set { tbDetail.Text = value; }
        }

        public Guid CurrentID
        {
            get
            {
                if (ViewState[CURRENTID] != null)
                {
                    return new Guid(ViewState[CURRENTID].ToString());
                }
                else
                {
                    return Guid.Empty;
                }
            }
            set { ViewState[CURRENTID] = value; }
        }
        #endregion
    }
}
