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

public partial class CompletedPAList : System.Web.UI.Page
{
    private string Filter = "(MentorAlias = '" + SiteUser.Current.Alias.ToString() + "') AND (OverrallEvaluation > 0)"; 
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet dsPA = PerformanceAssessment.GetPerformanceAssessment();
        DataView dvPA = new DataView(dsPA.Tables[0]);
        dvPA.RowFilter = Filter;

        gvCompletedPA.DataSource = dvPA;
        gvCompletedPA.DataBind();
    }

    protected string GetApplicantLink(string ID)
    {
        return "~/ShowApplication.aspx?applicant=" + ID;
    }

    protected string GetViewLink(string ApplicantID, string PAID)
    {
        return "~/ShowApplication.aspx?applicant=" + ApplicantID + "&tab=2&PAID=" + PAID;
    }

    protected string GetEditLink(string ID)
    {
        return "~/MentorPA.aspx?PAId=" + ID;
    }

    protected string GetGroupNameByID(string ID)
    {
        string Group = "";
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
        string strPerformance = "";
        try
        {
            strPerformance = PAResourceManager.IdToText("PerformanceLevel", Convert.ToInt32(Performance));
        }
        catch
        {
        }

        return strPerformance;
    }
}
