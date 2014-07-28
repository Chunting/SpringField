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

public partial class HireReport_New : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbStartDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbStartDate.ClientID + ",'yyyy-mm-dd');");
            tbEndDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEndDate.ClientID + ",'yyyy-mm-dd');");
            //tbStartDate.Attributes.Add("readonly", "true");
            //tbEndDate.Attributes.Add("readonly", "true");
            InitDate();
            if (Request["Group"] != null)
                ddlReportType.SelectedValue = "Group";
        }

        BindReport();
    }

    private void BindReport()
    {
        Control reportCon;
        switch (ddlReportType.SelectedValue)
        {
            case "Mentor":
                reportCon = LoadControl(@"~/Controls/InterviewReport.ascx");
                reportCon.ID = "InterviewReportCon";
                ((Controls_InterviewReport)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim());
                ((Controls_InterviewReport)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim());
                break;
            case "Group":
                reportCon = LoadControl(@"~/Controls/HiringReport.ascx");
                reportCon.ID = "HiringReportCon";
                ((Controls_HiringReport)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim());
                ((Controls_HiringReport)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim());
                break;
            default:
                reportCon = LoadControl(@"~/Controls/InterviewReport.ascx");
                reportCon.ID = "InterviewReportCon";
                ((Controls_InterviewReport)reportCon).m_StartDate = Convert.ToDateTime(tbStartDate.Text.Trim());
                ((Controls_InterviewReport)reportCon).m_EndDate = Convert.ToDateTime(tbEndDate.Text.Trim());
                break;
        }
        reportCon.EnableViewState = false;
        phReport.Controls.Add(reportCon);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lbDateSpan.Text = "Date From " + tbStartDate.Text + " To " + tbEndDate.Text;
        if (ddlReportType.SelectedValue == "Mentor")
        {
            this.phReport.FindControl("InterviewReportCon").FindControl("gvDetailCandidates").Visible = false;
            this.phReport.FindControl("InterviewReportCon").FindControl("btngvDetailCandidatesExportExcel").Visible = false;
        }
        else if (ddlReportType.SelectedValue == "Group")
        {
            this.phReport.FindControl("HiringReportCon").FindControl("gvDetailedReport").Visible = false;
            this.phReport.FindControl("HiringReportCon").FindControl("btngvDetailReportExportExcel").Visible = false;
        }
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
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
}
