using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components;

using System.Data.Linq;
/*
 * Checkout Survey Comments Report
 * Add by Yuanqin, 2011.6.7
 */

namespace MSRA.SpringField.Application
{
    public partial class AllCommentsReport : System.Web.UI.Page
    {
        public DateTime m_StartDate, m_EndDate;
        public Int32 GroupId;
        public Int32 Duration;
        //private string m_CacheKey;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request["StartDate"] != null && Request["EndDate"] != null)
            //{
            //    try
            //    {
            //        m_StartDate = Convert.ToDateTime(Request["StartDate"]);
            //        m_EndDate = Convert.ToDateTime(Request["EndDate"]).AddDays(1).AddSeconds(-1);
            //        GroupId = Convert.ToInt32(Request["GroupId"]);
            //        Duration = Convert.ToInt32(Request["Duration"]);
            //        //m_CacheKey = Convert.ToString(Request["key"]);
            //    }
            //    catch
            //    {
            //        JSUtility.Alert(this, "Invalid Parameters!");
            //        JSUtility.CloseWindow(this);
            //        return;
            //    }
            //}
           

            if (!IsPostBack)
            {
                if (Request["StartDate"] != null && Request["EndDate"] != null)
                {
                    try
                    {
                        m_StartDate = Convert.ToDateTime(Request["StartDate"]);
                        m_EndDate = Convert.ToDateTime(Request["EndDate"]).AddDays(1).AddSeconds(-1);
                        GroupId = Convert.ToInt32(Request["GroupId"]);
                        Duration = Convert.ToInt32(Request["Duration"]);
                        //m_CacheKey = Convert.ToString(Request["key"]);
                    }
                    catch
                    {
                        JSUtility.Alert(this, "Invalid Parameters!");
                        JSUtility.CloseWindow(this);
                        return;
                    }
                }

                tbStartDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbStartDate.ClientID + ",'yyyy-mm-dd');");
                tbEndDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEndDate.ClientID + ",'yyyy-mm-dd');");
                tbStartDate.Attributes.Add("readonly", "true");
                tbEndDate.Attributes.Add("readonly", "true");
                InitDate();

                //Bind group list
                ddlGroup.DataSource = CheckInFormResourceManager.GetTypeDataSet("Groups").Tables[0];
                ddlGroup.DataValueField = "id";
                ddlGroup.DataTextField = "name";
                ddlGroup.DataBind();
                ListItem allGroup = new ListItem("All", string.Empty);
                ddlGroup.Items.Insert(0, allGroup);
                allGroup.Selected = true;

                ddlGroup.SelectedValue = GroupId.ToString();
                ddlTimeSpan.SelectedIndex = Duration;
                //BindData(GetFilter());
                if (Request["type"].ToLower() == "Detail")
                {
                    BindData(GetFilterForDetail());
                }
                else
                {
                    BindData(GetFilter());
                }
            }
        }

        private void BindData(string strFilter)
        {
            DataSet dsSurvey = MSRA.Springfield.Components.BizObjects.CheckoutSurvey.GetSurveyReport();
            DataView dvSurvey = new DataView(dsSurvey.Tables[0]);
            dvSurvey.RowFilter = strFilter;
            dvSurvey.Sort = "CheckOutDate DESC";
          
            gvSurveyReport.DataSource = dvSurvey;
            gvSurveyReport.DataBind();
            lbCount.Text = dvSurvey.Count.ToString();
        }
        protected string GetGroupNameByID(string ID)
        {
            string Group = "N/A";
            try
            {
                Group = CheckInFormResourceManager.IdToText("Groups", Convert.ToInt32(ID));
            }
            catch
            {
            }

            return Group;
        }
        protected string GetDurationByID(string ID)
        {
            string duration = "";
            if (ID == "1")
            {
                duration = "0 - 3M";
            }
            else if (ID == "2")
            {
                duration = "4 - 6M";
            }
            else if (ID == "3")
            {
                duration = "7 - 12M";
            }
            else if (ID == "4")
            {
                duration = "One year above";
            }
            else if (ID == "")
            {
                duration = "";
            }

            return duration;
        }
        protected string GetViewSurveyLink(string ID)
        {
            return "http://msra-spfield/springfield/ShowApplication.aspx?applicant=" + ID + "&tab=3";
        }
        protected void gvSurveyReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSurveyReport.PageIndex = e.NewPageIndex;
            BindData(GetFilter());
        }

        #region Private Functions
        private string GetFilter()
        {
            string strFilter = String.Empty;

            if (!isNullOrEmpty(tbStartDate.Text.Trim()))
            {
                strFilter += "(CheckOutDate > '";
                strFilter += Convert.ToDateTime(tbStartDate.Text.Trim()).ToString();
                strFilter += "') and ";
            }
            if (!isNullOrEmpty(tbEndDate.Text.Trim()))
            {
                strFilter += "(CheckOutDate < '";
                strFilter += Convert.ToDateTime(tbEndDate.Text.Trim()).ToString();
                strFilter += "') and ";
            }
            if (!isNullOrEmpty(ddlGroup.SelectedValue.Trim()))
            {
                strFilter += "(GroupId = '";
                strFilter += ddlGroup.SelectedValue.Trim();
                strFilter += "') and ";
            }
            if (!isNullOrEmpty(ddlTimeSpan.SelectedValue.Trim()) && ddlTimeSpan.SelectedIndex!=0)
            {
                strFilter += "(InternshipDuration = '";
                strFilter += ddlTimeSpan.SelectedValue.Trim();
                strFilter += "') and ";
            }

            strFilter += "(len(OverallComments) <>0 or len(WorkComments) <>0 or len(MentorComments) <>0 ";
            strFilter += "or len(TrainingComments) <>0 or len(LifeComments) <>0 or len(MSRAComments) <>0 ";
            strFilter += "or len(Comments) <>0 )";

            //if (strFilter.Length > 4)
            //    strFilter = strFilter.Substring(0, strFilter.Length - 4);
            return strFilter;
        }

        private bool isNullOrEmpty(string str)
        {
            if (str != "" && !String.IsNullOrEmpty(str))
                return false;
            else
                return true;
        }
        private string GetFilterForDetail()
        {
            string strFilter = String.Empty;

            if (m_StartDate.ToString() != null)
            {
                strFilter += "(CheckOutDate > '";
                strFilter += m_StartDate.ToString();
                strFilter += "') and ";
            }
            if (m_EndDate.ToString() != null)
            {
                strFilter += "(CheckOutDate < '";
                strFilter += m_EndDate.ToString();
                strFilter += "') and ";
            }
            if (GroupId.ToString() != "0")
            {
                strFilter += "(GroupId = '";
                strFilter += GroupId.ToString();
                strFilter += "') and ";
            }
            if (Duration.ToString() != "0")
            {
                strFilter += "(InternshipDuration = '";
                strFilter += Duration.ToString();
                strFilter += "') and ";
            }

            return strFilter;
        }
        private void InitDate()
        {
            if (Request["StartDate"] != null && Request["EndDate"] != null)
            {
                try
                {
                    tbStartDate.Text = Convert.ToDateTime(Request["StartDate"]).ToString("yyyy-MM-dd");
                    tbEndDate.Text = Convert.ToDateTime(Request["EndDate"]).ToString("yyyy-MM-dd");
                }
                catch
                {
                    tbStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                    tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                tbStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            lbDateSpan.Text = "Date From " + tbStartDate.Text + " To " + tbEndDate.Text;
        }
        #endregion

        #region Event Handler
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string strFilter = GetFilter();
            BindData(strFilter);
        }
        protected void Export_Click(object sender, EventArgs e)
        {
            gvSurveyReport.AllowPaging = false;
            BindData(GetFilter());
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=PA Report-HR" + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/excel";
            Response.Charset = "utf-8";
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
            Response.ContentEncoding = Encoding.UTF8;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvSurveyReport.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion
    }
}
