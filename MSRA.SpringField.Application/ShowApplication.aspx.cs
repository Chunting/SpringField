/*
TO DO :
 bug:没有对查询的用户进行限制，只要能打开master，并且有guid就可以看到任何人的信息，尽管PA list里面只要他自己的实习生
*/

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
using MSRA.SpringField.Application.Controls;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Configuration;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Application.Config.Schemas;
using System.Linq;

namespace MSRA.SpringField.Application
{
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
                    applicantId = new Guid(Convert.ToString(Request.QueryString["applicant"]));
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
                if (Request["tab"].ToString() == "3")
                {
                    Tabs.ActiveTab = Tabs.Tabs[3];
                }
            }
            else
            {
                Tabs.ActiveTab = Tabs.Tabs[0];
            }

            //if (Request["test"] != null)
            //    ucPerformanceAssessment.ApplicantId = "2d83ed78-444a-4041-ae7b-c0d03bbb986d";
            //else
            ucPerformanceAssessment.ApplicantId = applicantId.ToString();
            InitBasicInfo(applicantId);
            InitInterviewInfo(applicantId);
            InitCommentInfo(applicantId);
            SetButtonEnable();

            //Add by Yuanqin, 2011.6.6
            ucCheckoutSurvey.ApplicantId = applicantId.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Tabs.ActiveTabIndex == 2)
            {
                approvaltools.Visible = true;
            }
            else
            {
                approvaltools.Visible = false;
            }

            if (!string.IsNullOrEmpty(activeTab.Value))
            {
                Tabs.ActiveTabIndex = int.Parse(activeTab.Value);
            }

            //面试次数的下拉列表
            if (string.IsNullOrEmpty(ddlInterviews.SelectedValue) == false)
            {
                string[] strs = ddlInterviews.SelectedValue.ToString().Split(';');
                Session["InterviewId"] = Convert.ToInt32(strs[0]);
            }

            /*
             * PA Approval
             * Author: Yin.P
             * Date: 2010-1-5
             */
            if (!Page.IsPostBack)
            {
                SetButtonEnable();
            }
        }

        private void SetButtonEnable()
        {
            /*
                 * TODO: PA Approval
                 * Author: Yin.P
                 * Date: 2010-1-5
                 */
            if (Request.QueryString["paid"] != null)
            {
                SpringFieldDataContext ctx = new SpringFieldDataContext();

                sf_PerformanceAssessment pa = ctx.sf_PerformanceAssessments.FirstOrDefault<sf_PerformanceAssessment>(
                        p => p.id.Equals(Request.QueryString["paid"])
                    );

                approvaltools.Visible = ((pa.IsApproved == 0 || pa.IsApproved == null)
                    && SiteUser.Current.IsInRole(RoleType.InternRecruiter));
            }
            else
            {
                approvaltools.Visible = false;
            }

            if (Roles.IsUserInRole(SiteUser.Current.FullName, RoleType.InternRecruiter.ToString()))
            {
                btnEditApplicant.Visible = true;
                btnAddFavoriteCurrent.Visible = true;
                btnInterviewCurrent.Visible = true;
                btnForward.Visible = true;
                /*
                 * Add by Yuanqin
                 * Date:2011.3.11
                 * 
                 */
                if (ApplicantBasicInfo.GetApplicantBasicInfoById(applicantId).Status != ApplicationStatusEnum.InterviewinProcess)
                {
                    btnOnBoardAction.Visible = true;
                }
                else
                {
                    btnOnBoardAction.Visible = false;
                }
            }
            else
            {
                btnEditApplicant.Visible = false;
                btnAddFavoriteCurrent.Visible = true;
                btnInterviewCurrent.Visible = true;
                btnForward.Visible = true;
                btnOnBoardAction.Visible = false;
                //btnOnBoardAction.Visible = true;
            }
        }
        //图标按钮Schedule Interview
        protected void btnInterview_Click(object sender, EventArgs e)
        {
            JSUtility.OpenNewWindow(this, "ArrangeInterview.aspx?applicant=" + applicantId.ToString(), string.Empty);
            JSUtility.RedirectSelf(this);
        }
        //图标按钮Add To Favourite
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
        //图标按钮Edit Applicant
        protected void btnEditApplicant_Click(object sender, EventArgs e)
        {
            ApplicantBasicInfo applicant = ApplicantBasicInfo.GetApplicantBasicInfoById(applicantId);
            Response.Redirect(SiteConfiguration.GetConfig().SiteAttributes["keyinSite"] + "ApplicantBasicInfo.aspx?email=" + applicant.Email);
        }
        //图标按钮 Forward To Mentor
        protected void btnForward_Click(object sender, ImageClickEventArgs e)
        {
            String url = String.Format("AddReferral.aspx?applicant={0}", applicantId);
            Response.Redirect(url);
        }
        //图标按钮OutputPA
        protected void GoToShowPA(object sender, EventArgs e)
        {
            Response.Redirect("~/ShowPA.aspx?applicant=" + applicantId + "&paid=" + Request.QueryString["paid"]);
        }

        /*
         * Hire Action for incruiter 
         * Author: Yuanqin
         * Date: 2011.3.8
         */
        //图标按钮Onboard
        protected void btnOnBoardAction_Click(object sender, EventArgs e)
        {
            Response.Redirect("OnBoardAction.aspx?applicant=" + applicantId.ToString());
        }

        //tab0 基本信息
        private void InitBasicInfo(Guid applicantId)
        {
            basicInfo.ApplicantId = applicantId;
            ApplicantInfoPanel1.ApplicantID = applicantId;
        }

        #region tab1 面试信息

        //初始化 <asp:PlaceHolder ID="phInterview" runat="server"></asp:PlaceHolder>
        private Controls_InterviewPanel GetInterviewPanel()
        {
            Controls_InterviewPanel InterviewPanel1 = null;
            if (phInterview.FindControl("InterviewPanel1") != null)
            {
                InterviewPanel1 = phInterview.FindControl("InterviewPanel1") as Controls_InterviewPanel;
            }
            else
            {
                InterviewPanel1 = (Controls_InterviewPanel)this.LoadControl("Controls/InterviewPanel.ascx");
                InterviewPanel1.ID = "InterviewPanel1";
                phInterview.Controls.Add(InterviewPanel1);
            }
            return InterviewPanel1;
        }

        //初始化面试信息的下拉框
        private void InitInterviewInfo(Guid applicantId)
        {
            ApplicantRelatedInfo ari = ApplicantRelatedInfo.GetApplicantRelatedInfoById(applicantId);
            DataSet dsInterview = Interview.GetInterviewForApplicant(applicantId);

            if (dsInterview.Tables[0].Rows.Count > 0)//如果有面试记录
            {
                Controls_InterviewPanel InterviewPanel1 = GetInterviewPanel();//获得上面加载的Panel


                for (Int32 i = 0; i < dsInterview.Tables[0].Rows.Count; i++)
                {
                    string strInterviewName = "";//下拉列表里面的文字
                    if (i == dsInterview.Tables[0].Rows.Count - 1)
                    {
                        strInterviewName = "Recent Interview";
                    }
                    else
                    {
                        strInterviewName = "Interview Time " + (i + 1);
                    }
                    ddlInterviews.Items.Insert(0, new ListItem(strInterviewName, dsInterview.Tables[0].Rows[i]["interviewid"].ToString() + ";" + (i + 1)));
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
                InterviewPanel1.InterviewId = Convert.ToInt32(dsInterview.Tables[0].Rows[dsInterview.Tables[0].Rows.Count - 1]["interviewid"]);
                InterviewPanel1.InterviewTime = String.Format("Interview Time:{0}", dsInterview.Tables[0].Rows.Count);
            }
            else//如果没有面试记录
            {
                ddlInterviews.Visible = false;
            }
        }

        //选择了一个面试信息查看
        protected void ddlInterviews_SelectedIndexChanged(object sender, EventArgs e)
        {
            Controls_InterviewPanel InterviewPanel1 = GetInterviewPanel();
            InterviewPanel1.ApplicantId = applicantId;
            string[] strs = ddlInterviews.SelectedValue.ToString().Split(';');
            InterviewPanel1.InterviewId = Convert.ToInt32(strs[0]);
            InterviewPanel1.InterviewTime = String.Format("Interview Time:{0}", Convert.ToInt32(strs[1]));
            InterviewPanel1.ResetControls();
            Session["InterviewId"] = InterviewPanel1.InterviewId;
        }
        #endregion



        private void InitCommentInfo(Guid applicantId)
        {
            //commentList.ApplicantId = applicantId;
        }



        protected void ibComment_Click(object sender, EventArgs e)
        {
            if (this.applicantId == null)
            {
                this.applicantId = new Guid(Request.QueryString["applicant"].ToString());
            }

            Response.Redirect("AddNote.aspx?applicant=" + this.applicantId);
        }

        protected void Invalid_Click(object sender, EventArgs e)
        {
            SpringFieldDataContext ctx = new SpringFieldDataContext();
            string PAID = Request.QueryString["paid"];
            if (PAID != null)
            {
                sf_PerformanceAssessment pa =
                    ctx.sf_PerformanceAssessments.FirstOrDefault<sf_PerformanceAssessment>(p => p.id.Equals(PAID));
                if (pa != null)
                {
                    pa.IsApproved = 3;  //IsApproved is 3 means INVALID.
                    ctx.SubmitChanges();

                    if (pa.GroupMgrId != null && pa.GroupMgrId != Guid.Empty)
                    {
                        JSUtility.Alert(this, "Performance Assessment has been set invalid,and a mail was sent to Group Manager");
                    }
                }
            }
        }

        /*
         * PA Approval
         * Author: Yin.P
         * Date: 2010-1-15
         */
        protected void Pass_Click(object sender, EventArgs e)
        {
            SpringFieldDataContext ctx = new SpringFieldDataContext();
            string PAID = Request.QueryString["paid"];
            if (PAID != null)
            {
                sf_PerformanceAssessment pa =
                    ctx.sf_PerformanceAssessments.FirstOrDefault<sf_PerformanceAssessment>(p => p.id.Equals(PAID));
                if (pa != null)
                {
                    pa.IsApproved = 1;
                    ctx.SubmitChanges();

                    if (pa.GroupMgrId != null && pa.GroupMgrId != Guid.Empty)
                    {
                        //Send mail to group manager
                        MailHelper mailHelper = new MailHelper();
                        mailHelper.AddPerformanceAssessmentVariables(pa.id);
                        mailHelper.SendMail(MailType.PANotice);

                        JSUtility.Alert(this, "Performance Assessment has been approved,and a mail was sent to Group Manager");
                    }
                    else
                    {
                        JSUtility.Alert(this, "Performance Assessment has been approved");
                    }
                }
            }
        }

        /*
         * PA Approval
         * Author: Yin.P
         * Date: 2010-1-15
         */
        protected void Reject_Click(object sender, EventArgs e)
        {
            SpringFieldDataContext ctx = new SpringFieldDataContext();
            string PAID = Request.QueryString["paid"];
            if (PAID != null)
            {
                sf_PerformanceAssessment pa =
                    ctx.sf_PerformanceAssessments.FirstOrDefault<sf_PerformanceAssessment>(p => p.id.Equals(PAID));
                if (pa != null)
                {
                    pa.IsApproved = 2;  //IsApproved is 2 means REJECT.
                    ctx.SubmitChanges();

                    if (pa.GroupMgrId != null && pa.GroupMgrId != Guid.Empty)
                    {
                        //Send mail to group manager
                        MailHelper mailHelper = new MailHelper();
                        mailHelper.AddPAApprovalRejectedVariables(new Guid(PAID));
                        mailHelper.SendMail(MailType.PAApprovalRejected);

                        JSUtility.Alert(this,
                            "The approval of current performance assessment was rejected. A mail has been sent to relevant mentor.");
                    }
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            /*
             * PA Approval
             * Author: Yin.P
             * Date: 2010-1-5
             */
            SetButtonEnable();
            base.OnPreRender(e);
        }

    }
}