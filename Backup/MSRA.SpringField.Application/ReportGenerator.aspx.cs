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

namespace MSRA.SpringField.Application
{
    public partial class ReportGenerator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSelectStartDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbStartTime.ClientID + ",'yyyy-mm-dd');");
            btnSelectEndDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEndTime.ClientID + ",'yyyy-mm-dd');");
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            switch (ddlReportType.SelectedIndex)
            {
                case 0:
                    Response.Redirect(String.Format("~/Modules/Reports/CandidatesReport.aspx?StartDate={0}&EndDate={1}", tbStartTime.Text, tbEndTime.Text));
                    break;
                case 1:
                    Response.Redirect(String.Format("~/Modules/Reports/SourcingReport_New.aspx?StartDate={0}&EndDate={1}", tbStartTime.Text, tbEndTime.Text));
                    break;
                case 2:
                    Response.Redirect(String.Format("~/Modules/Reports/HireReport_New.aspx?StartDate={0}&EndDate={1}", tbStartTime.Text, tbEndTime.Text));
                    break;
                case 3:
                    Response.Redirect(String.Format("~/Modules/Reports/PAReport.aspx?BeginDate={0}&EndDate={1}", tbStartTime.Text, tbEndTime.Text));
                    break;
                case 4:
                    Response.Redirect(String.Format("~/Modules/Reports/PAReportForHR.aspx?BeginDate={0}&EndDate={1}", tbStartTime.Text, tbEndTime.Text));
                    break;
                default:
                    Response.Redirect(String.Format("~/Modules/Reports/CandidatesReport.aspx?StartDate={0}&EndDate={1}", tbStartTime.Text, tbEndTime.Text));
                    break;
            }
        }
    }
}