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
using Springfield.Components;
using Springfield.Components.Configuration;
using System.IO;
using System.Text;

public partial class PAReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.btnBeginDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbBeginDate.ClientID + ",'yyyy-mm-dd');");
            this.tbBeginDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbBeginDate.ClientID + ",'yyyy-mm-dd');");
            //this.tbBeginDate.Attributes.Add("readonly", "true");
            this.btnEndDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEndDate.ClientID + ",'yyyy-mm-dd');");
            this.tbEndDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEndDate.ClientID + ",'yyyy-mm-dd');");
            //this.tbEndDate.Attributes.Add("readonly", "true");
            this.btnCheckOutBeginDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckOutBeginDate.ClientID + ",'yyyy-mm-dd');");
            this.tbCheckOutBeginDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckOutBeginDate.ClientID + ",'yyyy-mm-dd');");
            //this.tbCheckOutBeginDate.Attributes.Add("readonly", "true");
            this.btnCheckOutEndDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckOutEndDate.ClientID + ",'yyyy-mm-dd');");
            this.tbCheckOutEndDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckOutEndDate.ClientID + ",'yyyy-mm-dd');");
            //this.tbCheckOutEndDate.Attributes.Add("readonly", "true");

            //tbBeginDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            //tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            bool isFillDate = false;
            try
            {
                if (Request.Params["BeginDate"] != null)
                {
                    tbCheckOutBeginDate.Text = Convert.ToDateTime(Request.Params["BeginDate"].ToString()).ToString("yyyy-MM-dd");
                    isFillDate = true;
                }
            }
            catch
            {
                tbCheckOutBeginDate.Text = "";
            }
            try
            {
                if (Request.Params["EndDate"] != null)
                {
                    tbCheckOutEndDate.Text = Convert.ToDateTime(Request.Params["EndDate"].ToString()).ToString("yyyy-MM-dd");
                    isFillDate = true;
                }
            }
            catch
            {
                tbCheckOutEndDate.Text = "";
            }

            if (isFillDate == false)
            {
                tbCheckOutBeginDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                tbCheckOutEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            //tbCheckOutBeginDate.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            //tbCheckOutEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            ddlGroup.DataSource = CheckInFormResourceManager.GetTypeDataSet("Groups").Tables[0];
            ddlGroup.DataValueField = "id";
            ddlGroup.DataTextField = "name";
            ddlGroup.DataBind();
            ListItem AllGroupItem = new ListItem("All", "0");
            ddlGroup.Items.Insert(0, AllGroupItem);

            ListItem AllStatusItem = new ListItem("All", "0");
            ddlStatus.Items.Add(AllStatusItem);
            ListItem AvailableItem = new ListItem(ApplicationStatusEnum.Available.ToString(), ((int)ApplicationStatusEnum.Available).ToString());
            ddlStatus.Items.Add(AvailableItem);
            ListItem OnBoardItem = new ListItem(ApplicationStatusEnum.OnBoard.ToString(), ((int)ApplicationStatusEnum.OnBoard).ToString());
            ddlStatus.Items.Add(OnBoardItem);
            ddlStatus.SelectedValue = ((int)ApplicationStatusEnum.OnBoard).ToString();

            BindData(GetFilter());
        }
    }

    #region gvPA related functions
    private void BindData(string strFilter)
    {
        DataSet dsPA = PerformanceAssessment.GetPerformanceAssessment();
        DataTable PATable = dsPA.Tables[0];
        PATable.Columns.Add("ApplicationStatus", typeof(string));
        for (int i = 0; i < PATable.Rows.Count; i++)
        {
            PATable.Rows[i]["ApplicationStatus"] = GetStatusByApplicantId(PATable.Rows[i]["ApplicantId"].ToString());
        }

        DataView dvPA = new DataView(PATable);
        
        dvPA.RowFilter = strFilter;
        dvPA.Sort = "InsertDate DESC";

        gvPAReport.DataSource = dvPA;
        gvPAReport.DataBind();
        lbCount.Text = dvPA.Count.ToString();
    }
    protected string GetStatusByApplicantId(string ApplicantId)
    {
        string strStatus = "N/A";
        if (String.IsNullOrEmpty(ApplicantId))
            return strStatus;
        Guid guidApplicantId = new Guid(ApplicantId);
        if (guidApplicantId == Guid.Empty)
            return strStatus;
        ApplicantBasicInfo abi = ApplicantBasicInfo.GetApplicantBasicInfoById(guidApplicantId);
        if (abi != null)
        {
            strStatus = abi.Status.ToString();
        }
        return strStatus;
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
    protected string GetPerformance(string Performance)
    {
        string strPerformance = "N/A";
        try
        {
            strPerformance = PAResourceManager.IdToText("PerformanceLevel", Convert.ToInt32(Performance));
        }
        catch
        {
        }

        return strPerformance;
    }
    protected string GetApplicantLink(string ID)
    {
        return "~/ShowApplication.aspx?applicant=" + ID;
    }
    protected string GetViewPALink(string ApplicantID, string PAID)
    {
        return "~/ShowApplication.aspx?applicant=" + ApplicantID + "&tab=2&PAID=" + PAID;
    }
    protected string GetDeadline(string InternPAtime)
    {
        string strDeadline = String.Empty;
        DateTime dtInternPAtime = DateTime.MinValue;
        try
        {
            dtInternPAtime = Convert.ToDateTime(InternPAtime);
        }
        catch
        {
            return strDeadline;
        }

        SiteConfiguration config = SiteConfiguration.GetConfig();
        int Timeslice = Convert.ToInt32(config.SiteAttributes["MentorPADays"]);
        strDeadline = dtInternPAtime.AddDays(Timeslice).ToString("yyyy-MM-dd");
        return strDeadline;
    }
    protected string ParseDate(string date)
    {
        string retureDate = "N/A";
        if(Convert.ToDateTime(date) != Convert.ToDateTime("9999-12-30")
            && Convert.ToDateTime(date) != Convert.ToDateTime("9999-12-29"))
            retureDate = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
        return retureDate;
    }
    protected void gvPAReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "CheckOut":
                Guid guidApplicantId = new Guid(e.CommandArgument.ToString());
                if (guidApplicantId == Guid.Empty)
                    break;
                ApplicantBasicInfo abi = ApplicantBasicInfo.GetApplicantBasicInfoById(guidApplicantId);
                abi.Status = ApplicationStatusEnum.Available;
                abi.Update();                
                BindData(GetFilter());
                break;
        }
    }
    protected void gvPAReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPAReport.PageIndex = e.NewPageIndex;
        BindData(GetFilter());
    }
    #endregion

    #region Private Functions
    private string GetFilter()
    {
        string strFilter = String.Empty;
        if (!isNullOrEmpty(tbCandidateName.Text.Trim()))
        {
            strFilter += "(InternName like '%";
            strFilter += tbCandidateName.Text.Trim();
            strFilter += "%') AND ";
        }
        if (!isNullOrEmpty(tbMentorAlias.Text.Trim()))
        {
            strFilter += "(MentorAlias = '";
            strFilter += tbMentorAlias.Text.Trim();
            strFilter += "') AND ";
        }
        if (!isNullOrEmpty(tbBeginDate.Text.Trim()))
        {
            strFilter += "(ModifyDate > '";
            strFilter += Convert.ToDateTime(tbBeginDate.Text.Trim()).ToString();
            strFilter += "') AND ";
        }
        if (!isNullOrEmpty(tbEndDate.Text.Trim()))
        {
            strFilter += "(ModifyDate < '";
            strFilter += Convert.ToDateTime(tbEndDate.Text.Trim()).AddDays(1).ToString();
            strFilter += "') AND ";
        }
        if (!isNullOrEmpty(tbCheckOutBeginDate.Text.Trim()))
        {
            strFilter += "(CheckOutDate > '";
            strFilter += Convert.ToDateTime(tbCheckOutBeginDate.Text.Trim()).ToString();
            strFilter += "') AND ";
        }
        if (!isNullOrEmpty(tbCheckOutEndDate.Text.Trim()))
        {
            strFilter += "(CheckOutDate < '";
            strFilter += Convert.ToDateTime(tbCheckOutEndDate.Text.Trim()).AddDays(1).ToString();
            strFilter += "') AND ";
        }
        if (ddlGroup.SelectedIndex > 0)
        {
            strFilter += "(GroupId = ";
            strFilter += ddlGroup.SelectedValue.ToString();
            strFilter += ") AND ";
        } 
        if (ddlStatus.SelectedIndex > 0)
        {
            strFilter += "(ApplicationStatus = '";
            strFilter += ddlStatus.SelectedItem.Text;
            strFilter += "') AND ";
        }  
        if (strFilter.Length > 4)
            strFilter = strFilter.Substring(0, strFilter.Length - 4);
        return strFilter;
    }
    private bool isNullOrEmpty(string str)
    {
        if (str != "" && !String.IsNullOrEmpty(str))
            return false;
        else
            return true;
    }
    #endregion

    #region Event Handler
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strFilter = GetFilter();
        BindData(strFilter);
    }    
    protected void btnCheckOutSelected_Click(object sender, EventArgs e)
    {
        if (Request.Form["cb_ischeck"] != null)
        {
            string[] idArr = Request.Form["cb_ischeck"].ToString().Split(',');
            ApplicantBasicInfo abi;
            Guid curId;
            foreach (string idStr in idArr)
            {
                curId = new Guid(idStr.Trim());
                abi = ApplicantBasicInfo.GetApplicantBasicInfoById(curId);
                abi.Status = ApplicationStatusEnum.Available;
                abi.Update();
            }
            BindData(GetFilter());
            JSUtility.Alert(this.Page, "All selected interns' status have been changed to \"Available\"!");
        }
        else
        {
            JSUtility.Alert(this.Page, "No intern has been selected!");
        }
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        gvPAReport.AllowPaging = false;
        gvPAReport.Columns.RemoveAt(0);
        gvPAReport.Columns.RemoveAt(gvPAReport.Columns.Count - 1);
        BindData(GetFilter());
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=PA Report" + DateTime.Now.ToString() + ".xls");
        Response.ContentType = "application/excel";
        Response.Charset = "utf-8";
        Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
        Response.ContentEncoding = Encoding.UTF8;
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gvPAReport.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    #endregion
}
