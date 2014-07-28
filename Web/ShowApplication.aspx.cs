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

public partial class ShowApplication : System.Web.UI.Page
{
    private Guid applicantId = Guid.Empty;
    //private Interview interview;
    private Interview interview
    {
        set
        {
            ViewState["showapplicant_interview"] = value;
        }
        get
        {
            return ViewState["showapplicant_interview"] as Interview;
        }
    }
    private Applicant curApplicant
    {
        set
        {
            ViewState["showapplicant_curApplicant"] = value;
        }
        get
        {
            return ViewState["showapplicant_curApplicant"] as Applicant;
        }
    }
    private readonly string DocPath = SiteConfiguration.GetConfig().SiteAttributes["docUrl"];

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Request["applicant"] == null)
        {
            JSUtility.Alert(this, "Invalid Parameter!");
            JSUtility.CloseWindow(this);
            return;
        }
        else
        {
            try
            {
                applicantId = new Guid(Convert.ToString(Request["applicant"]));
            }
            catch
            {
                JSUtility.Alert(this, "Invalid parameter!");
                JSUtility.CloseWindow(this);
                return;
            }
        }
        if (Request["tab"] != null)
        {
            if (Request["tab"].ToString() == "1")
            {
                Tabs.ActiveTab = Tabs.Tabs[1];
            }
            if (Request["tab"].ToString() == "2")
            {
                Tabs.ActiveTab = Tabs.Tabs[2];
            }
        }

        //if (Request["test"] != null)
        //    ucPerformanceAssessment.ApplicantId = "2d83ed78-444a-4041-ae7b-c0d03bbb986d";
        //else
            ucPerformanceAssessment.ApplicantId = applicantId.ToString();
        InitBasicInfo(applicantId);
        InitInterviewInfo(applicantId);
        InitCommentInfo(applicantId);
        SetButtonEnable();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(activeTab.Value))
            Tabs.ActiveTabIndex = int.Parse(activeTab.Value);
    }
    private void SetButtonEnable()
    {
        if (Roles.IsUserInRole(SiteUser.Current.FullName, RoleType.InternRecruiter.ToString()))
        {
            btnEditApplicant.Visible = true;
            btnAddFavoriteCurrent.Visible = true;
            btnInterviewCurrent.Visible = true;
            btnForward.Visible = true;
        }
        else
        {
            btnEditApplicant.Visible = false;
            btnAddFavoriteCurrent.Visible = true;
            btnInterviewCurrent.Visible = true;
            btnForward.Visible = true;
        }
    }
    protected void btnInterview_Click(object sender, EventArgs e)
    {
        JSUtility.OpenNewWindow(this, "ArrangeInterview.aspx?applicant=" + applicantId.ToString(), string.Empty);
        JSUtility.RedirectSelf(this);
    }
    protected void btnFavorite_Click(object sender, EventArgs e)
    {
        Guid ownerId = SiteUser.Current.SiteUserId;
        if (Favorite.IsFavoriteExists(ownerId, applicantId))
        {
            JSUtility.Alert(this, "Favorite item is exists!");
        }
        else
        {
            Favorite favorite = new Favorite();
            favorite.ApplicantId = applicantId;
            favorite.OwnerId = ownerId;
            favorite.Insert();
            JSUtility.Alert(this, "Add To Favorite List Successfully!");
        }
        JSUtility.RedirectSelf(this);
    }
    protected void btnEditApplicant_Click(object sender, EventArgs e)
    {
        ApplicantBasicInfo applicant = ApplicantBasicInfo.GetApplicantBasicInfoById(applicantId);
        Response.Redirect(SiteConfiguration.GetConfig().SiteAttributes["keyinSite"] + "ApplicantBasicInfo.aspx?email=" + applicant.Email);
    }

    private void InitBasicInfo(Guid applicantId)
    {
        basicInfo.ApplicantId = applicantId;
        ApplicantInfoPanel1.ApplicantID = applicantId;


    }

    private Controls_InterviewPanel GetInterviewPanel()
    {
        Controls_InterviewPanel InterviewPanel1 = null;
        if (phInterview.FindControl("InterviewPanel1") != null)
        {
            InterviewPanel1 = phInterview.FindControl("InterviewPanel1") as Controls_InterviewPanel;
        }else
        {
            InterviewPanel1 = (Controls_InterviewPanel)this.LoadControl("Controls/InterviewPanel.ascx");
            InterviewPanel1.ID = "InterviewPanel1";
            phInterview.Controls.Add(InterviewPanel1);
        }
        return InterviewPanel1;
    }
    private void InitInterviewInfo(Guid applicantId)
    {
        ApplicantRelatedInfo ari = ApplicantRelatedInfo.GetApplicantRelatedInfoById(applicantId);
        DataSet dsInterview = Interview.GetInterviewForApplicant(applicantId);

        if (dsInterview.Tables[0].Rows.Count > 0)
        {
            Controls_InterviewPanel InterviewPanel1 = GetInterviewPanel();


            for (Int32 i = 0; i < dsInterview.Tables[0].Rows.Count; i++)
            {
                string strInterviewName = "";
                if (i == dsInterview.Tables[0].Rows.Count - 1)
                {
                    strInterviewName = "Recent Interview";
                }
                else
                {
                    strInterviewName = "Interview Time " + (i + 1);
                }
                ddlInterviews.Items.Insert(0, new ListItem(strInterviewName,dsInterview.Tables[0].Rows[i]["interviewid"].ToString()+";"+(i+1)));
                /*
                Int32 interviewId = Convert.ToInt32(dsInterview.Tables[0].Rows[i]["interviewid"]);

                Controls_InterviewPanel uc = (Controls_InterviewPanel)this.LoadControl("Controls/InterviewPanel.ascx");
                uc.ApplicantId = applicantId;
                uc.InterviewId = interviewId;
                uc.InterviewTime = String.Format("Interview Time:{0}", i + 1);
                phInterview.Controls.Add(uc);
                 */
            }
            InterviewPanel1.ApplicantId = applicantId;
            InterviewPanel1.InterviewId = Convert.ToInt32(dsInterview.Tables[0].Rows[dsInterview.Tables[0].Rows.Count-1]["interviewid"]);
            InterviewPanel1.InterviewTime = String.Format("Interview Time:{0}", dsInterview.Tables[0].Rows.Count);
        }
        else
        {
            ddlInterviews.Visible = false;
        }
    }

    protected void btnForward_Click(object sender, ImageClickEventArgs e)
    {
        String url = String.Format("AddReferral.aspx?applicant={0}", applicantId);
        Response.Redirect(url);
    }

    private void InitCommentInfo(Guid applicantId)
    {
        commentList.ApplicantId = applicantId;
    }

    protected void ddlInterviews_SelectedIndexChanged(object sender, EventArgs e)
    {
        Controls_InterviewPanel InterviewPanel1 = GetInterviewPanel();
        InterviewPanel1.ApplicantId = applicantId;
        string[] strs = ddlInterviews.SelectedValue.ToString().Split(';');
        InterviewPanel1.InterviewId = Convert.ToInt32(strs[0]);
        InterviewPanel1.InterviewTime = String.Format("Interview Time:{0}", Convert.ToInt32(strs[1]));
        InterviewPanel1.ResetControls();
    }
}
