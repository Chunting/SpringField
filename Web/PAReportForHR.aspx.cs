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
using System.IO;
using System.Text;

public partial class PAReportForHR : System.Web.UI.Page
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
            bool isFillDate = false;
            try
            {
                if (Request.Params["BeginDate"] != null)
                {
                    tbBeginDate.Text = Convert.ToDateTime(Request.Params["BeginDate"].ToString()).ToString("yyyy-MM-dd");
                    isFillDate = true;
                }
            }
            catch
            {
                tbBeginDate.Text = "";
            }
            try
            {
                if (Request.Params["EndDate"] != null)
                {
                    tbEndDate.Text = Convert.ToDateTime(Request.Params["EndDate"].ToString()).ToString("yyyy-MM-dd");
                    isFillDate = true;
                }
            }
            catch
            {
                tbEndDate.Text = "";
            }

            if (isFillDate == false)
            {
                tbBeginDate.Text = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");
                tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }

            BindData(GetFilter());
        }
    }

    #region gvPA related functions
    private void BindData(string strFilter)
    {
        DataSet dsPA = PerformanceAssessment.GetPAReportForHR();
        DataView dvPA = new DataView(dsPA.Tables[0]);
        dvPA.RowFilter = strFilter;
        dvPA.Sort = "GroupId DESC";

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
    protected string GetViewPALink(string ID)
    {
        return "~/ShowApplication.aspx?applicant=" + ID + "&tab=2";
    }
    protected string GetPAZipLink(string ID)
    {
        return "~/PAPackage.ashx?PAId=" + ID;
    }
    protected string GetDegree(string ID)
    {
        string Degree = "N/A";
        try
        {
            Guid id = new Guid(ID);
            ApplicantEduBackground aeb = ApplicantEduBackground.GetApplicantEduBackgroundById(id);
            Degree = StaticData.DegreeList[(int)aeb.Degree] + aeb.YearOfStudy.ToString();
        }
        catch
        {
        }

        return Degree;
    }
    protected string ParseDate(string date)
    {
        string retureDate = "N/A";
        if (Convert.ToDateTime(date) != Convert.ToDateTime("9999-12-30")
            && Convert.ToDateTime(date) != Convert.ToDateTime("9999-12-29"))
            retureDate = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
        return retureDate;
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
            strFilter += "(GraduationDate > '";
            strFilter += Convert.ToDateTime(tbBeginDate.Text.Trim()).ToString();
            strFilter += "') AND ";
        }
        if (!isNullOrEmpty(tbEndDate.Text.Trim()))
        {
            strFilter += "(GraduationDate < '";
            strFilter += Convert.ToDateTime(tbEndDate.Text.Trim()).AddDays(1).ToString();
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
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        gvPAReport.AllowPaging = false;
        BindData(GetFilter());
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=PA Report-HR" + DateTime.Now.ToString() + ".xls");
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
