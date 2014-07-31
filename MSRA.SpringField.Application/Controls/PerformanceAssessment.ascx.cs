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
using System.Text.RegularExpressions;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components;
using MSRA.SpringField.Application.Config.Schemas;
using System.Linq;

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_PerformanceAssessment : System.Web.UI.UserControl
    {
        public string ApplicantId
        {
            get
            {
                if (Session["ApplicantId"] != null)
                    return Session["ApplicantId"].ToString();
                else
                    return String.Empty;
            }
            set
            {
                Session["ApplicantId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRadioButtonList();
                BindPAList();

            
                if (Request.Params["PAID"] != null)//always TRUE
                {
                    Guid PAId = new Guid(Request.Params["PAID"].ToString());
                    BindPADetail(PAId);

                    /*
                     * Charge if it should display the information of STC.
                     * @Author: Yin.P
                     * @Date: 2009-12-08
                     */
                    #region Charge if it should display the information of STC.
                    SpringFieldDataContext ctx = new SpringFieldDataContext();
                    sf_PerformanceAssessment paObj =
                        ctx.sf_PerformanceAssessments.FirstOrDefault<sf_PerformanceAssessment>(p => p.id == PAId);
                    if (paObj != null && paObj.Department != null)
                    {                        
                        olFTE_STC.Visible = 
                            trPipelineDiscipline_STC.Visible = 
                            "STC".Equals(PAResourceManager.IdToText("Department", paObj.Department.Value));
                        olFTE_MSRA.Visible = !olFTE_STC.Visible;
                    }
                    #endregion
                }
            }
        }

        #region PA gridview related functions
        private void BindPAList()
        {
            DataSet dsPA = PerformanceAssessment.GetPerformanceAssessment();
            DataView dvPA = new DataView(dsPA.Tables[0]);
            string Filter = "(ApplicantId = '" + ApplicantId + "')";
            dvPA.Sort = "InsertDate DESC";
            dvPA.RowFilter = Filter;

            gvPAList.DataSource = dvPA;
            gvPAList.DataBind();//GridView，最上面一行Check-In Date信息等
            
            //权限验证
            if (gvPAList.Rows.Count > 0 //有这个人的PA信息
                && (SiteUser.Current.IsInRole(RoleType.GroupManager)
                    || SiteUser.Current.IsInRole(RoleType.OnBoardManager)
                    || SiteUser.Current.IsInRole(RoleType.InternRecruiter))//有权限
                && Request.Params["PAID"] == null)//always FALSE
            {
                Guid firstPAId = new Guid(((Label)gvPAList.Rows[0].Cells[0].Controls[1]).Text);
                BindPADetail(firstPAId);
            }
        }

        protected void gvPAList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "PAdetail":
                    Guid DetailPAId = new Guid(e.CommandArgument.ToString());
                    BindPADetail(DetailPAId);
                    break;
                case "deletePA":
                    Guid DeletePAId = new Guid(e.CommandArgument.ToString());
                    bool isSuccess = PerformanceAssessment.DeletePerformanceAssessmentbyId(DeletePAId);
                    if (isSuccess)
                        JSUtility.Alert(this.Page, "Successful Deleted!");
                    else
                        JSUtility.Alert(this.Page, "Failed to delete!");
                    BindPAList();
                    break;
            }
        }
        protected void gvPAList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Guid appId = new Guid(Request.Params["applicant"].ToString());
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SpringFieldDataContext ctx = new SpringFieldDataContext();
                
                sf_PerformanceAssessment pa = 
                    ctx.sf_PerformanceAssessments.FirstOrDefault<sf_PerformanceAssessment>(p => p.ApplicantId == appId);
                if (pa != null)
                {
                    HtmlAnchor anchor = e.Row.FindControl("lnkEditPA") as HtmlAnchor;
                    if (anchor != null)
                    {
                        if (pa.IsApproved == 1)
                        {
                            anchor.Visible = false;
                        }
                        else
                        {
                            anchor.Visible = true;
                        }
                    }
                }
            }
        }

        protected void gvPAList_PreRender(object sender, EventArgs e)
        {
            if (SiteUser.Current.IsInRole(RoleType.InternRecruiter) || SiteUser.Current.IsInRole(RoleType.OnBoardManager))
            {
                foreach (GridViewRow row in gvPAList.Rows)
                {
                    ((LinkButton)row.FindControl("lnkbtnDetail")).Visible = true;
                    row.FindControl("lnkEditPA").Visible = true;
                    ((LinkButton)row.FindControl("lnkbtnDeletePA")).Visible = true;
                }
            }
            if (SiteUser.Current.IsInRole(RoleType.GroupManager))
            {
                foreach (GridViewRow row in gvPAList.Rows)
                {
                    ((LinkButton)row.FindControl("lnkbtnDetail")).Visible = true;
                }
            }

            PerformanceAssessment pa = null;
            bool isShow = false;
            if (Request.QueryString["paid"] != null)
            {
                pa = PerformanceAssessment.GetPerformanceAssessmentById(new Guid(Request.QueryString["paid"].ToString()));
                if (pa != null)
                {
                    isShow = !(pa.IsApproved == 1);
                }
            }
            foreach (GridViewRow row in gvPAList.Rows)
            {
                if (((Label)row.FindControl("lbMentorAlias")).Text.Trim() == SiteUser.Current.Alias)
                {
                    ((LinkButton)row.FindControl("lnkbtnDetail")).Visible = true;
                    row.FindControl("lnkEditPA").Visible = isShow;
                    ((LinkButton)row.FindControl("lnkbtnDeletePA")).Visible = isShow;
                }
            }
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
        private void BindPADetail(Guid PAId)
        {
            this.PanelPADetail.Visible = true;
            gvPublicationBindData(PAId);//查询MSRA期间发的论文，girdview
            BindPA(PAId);
        }
        #endregion

        #region Detail PA related functions related functions
        /// <summary>
        /// 绑定PA单选框的内容
        /// </summary>
        /// <returns></returns>
        protected DataTable GetPerformanceLevelDataSource()
        {
            DataTable SourceTable = new DataTable();
            SourceTable = PAResourceManager.GetTypeDataSet("PerformanceLevel").Tables[0];

            return SourceTable;
        }
        private void gvPublicationBindData(Guid PAId)
        {
            DataSet dsPabulication = new DataSet();
            dsPabulication = InternPublication.GetInternPublicationByPAId(PAId);

            gvPublication.DataSource = dsPabulication.Tables[0].DefaultView;
            gvPublication.DataBind();
        }
        protected string StatusIdToString(string Id)
        {
            return PAResourceManager.IdToText("PaperStatus", Convert.ToInt32(Id));
        }
        private void BindPA(Guid PAId)
        {
            PerformanceAssessment pa = PerformanceAssessment.GetPerformanceAssessmentById(PAId);

            if (String.IsNullOrEmpty(pa.Objective))
                lbInternObjective.Text = "None";
            else
                lbInternObjective.Text = pa.Objective;

            if (String.IsNullOrEmpty(pa.SelfEvaluation))
                lbInternSelfEvaluation.Text = "None";
            else
                lbInternSelfEvaluation.Text = pa.SelfEvaluation;

            if (String.IsNullOrEmpty(pa.StrengthsAndWeaknesses))
                lbStrengthsWeaknesses.Text = "None";
            else
                lbStrengthsWeaknesses.Text = pa.StrengthsAndWeaknesses;

            lbInternName.Text = pa.InternName.Trim();
            lbInternPhone.Text = pa.InternPhone;
            lbInternEmail.Text = pa.InternEmail;
            if (pa.GroupId > 0)
                lbGroup.Text = CheckInFormResourceManager.IdToText("Groups", pa.GroupId);
            lbDate.Text = "From " + pa.CheckInDate.ToString("MM/dd/yyyy") + " To " + pa.CheckOutDate.ToString("MM/dd/yyyy");
            lbGraduationDate.Text = pa.GraduationDate.ToString("MM/dd/yyyy");
            lbMentorName.Text = pa.MentorName;
            lbMentorAlias.Text = pa.MentorAlias;
            //ApplicantId = pa.ApplicantId;
            if (pa.ProjectId > -1)
                lbProject.Text = CheckInFormResourceManager.IdToText("Projects", pa.ProjectId);
            if (pa.InternPosition > 0)
                lbInternPosition.Text = PAResourceManager.IdToText("InternPosition", pa.InternPosition);
            if (pa.GroupMgrId != Guid.Empty)
                lbGMAlias.Text = SiteUser.GetAliasByUserId(pa.GroupMgrId);
            if (pa.Discipline > 0)
                lbDiscipline_STC.Text = CheckInFormResourceManager.IdToText("Positions", pa.Discipline);
            if (!String.IsNullOrEmpty(pa.Pipeline.Trim()))
                lbPipeline_STC.Text = PAResourceManager.IdToText("ProjectBasedorFTEPipeline", Convert.ToInt32(pa.Pipeline.Trim()));


            if (pa.CodingSkill > 0 && pa.CodingSkill < 7)
                rblCodingskill.SelectedValue = pa.CodingSkill.ToString();

            if (pa.AnalyticalSkill > 0 && pa.AnalyticalSkill < 7)
                rblAnalyticalSkill.SelectedValue = pa.AnalyticalSkill.ToString();

            if (pa.ProblemSolving > 0 && pa.ProblemSolving < 7)
                rblProblemSolving.SelectedValue = pa.ProblemSolving.ToString();

            if (pa.Innovation > 0 && pa.Innovation < 7)
                rblInnovation.SelectedValue = pa.Innovation.ToString();

            if (pa.DrivingForResults > 0 && pa.DrivingForResults < 7)
                rblDrivingforResults.SelectedValue = pa.DrivingForResults.ToString();

            if (pa.DealingWithAmbiguity > 0 && pa.DealingWithAmbiguity < 7)
                rblDealingwithAmbiguity.SelectedValue = pa.DealingWithAmbiguity.ToString();

            if (pa.QuickOnLearing > 0 && pa.QuickOnLearing < 7)
                rblQuickonLearning.SelectedValue = pa.QuickOnLearing.ToString();

            if (pa.English > 0 && pa.English < 7)
                rblEnglish.SelectedValue = pa.English.ToString();

            if (pa.Communication > 0 && pa.Communication < 7)
                rblCommunicationSkills.SelectedValue = pa.Communication.ToString();

            if (pa.TeamWork > 0 && pa.TeamWork < 7)
                rblTeamWork.SelectedValue = pa.TeamWork.ToString();

            if (pa.Attitude > 0 && pa.Attitude < 7)
                rblAttitude.SelectedValue = pa.Attitude.ToString();

            if (pa.IntegrityHonesty > 0 && pa.IntegrityHonesty < 7)
                rblIntegrityandHonesty.SelectedValue = pa.IntegrityHonesty.ToString();

            if (pa.OpenRespectful > 0 && pa.OpenRespectful < 7)
                rblOpenandRespectful.SelectedValue = pa.OpenRespectful.ToString();

            if (pa.BigChallenges > 0 && pa.BigChallenges < 7)
                rblBigChallenges.SelectedValue = pa.BigChallenges.ToString();

            if (pa.Passion > 0 && pa.Passion < 7)
                rblPassion.SelectedValue = pa.Passion.ToString();

            if (pa.Accountable > 0 && pa.Accountable < 7)
                rblAccountable.SelectedValue = pa.Accountable.ToString();

            if (pa.SelfCritical > 0 && pa.SelfCritical < 7)
                rblSelfCritical.SelectedValue = pa.SelfCritical.ToString();

            if (pa.OverrallEvaluation > 0 && pa.OverrallEvaluation < 7)
                rblOverallEvaluation.SelectedValue = pa.OverrallEvaluation.ToString();

            if (pa.HiredAsFTE > 0 && pa.HiredAsFTE < 3)
            {
                rblHiredAsFTE_STC.SelectedValue = pa.HiredAsFTE.ToString();
                rblHiredAsFTE_MSRA.SelectedValue = pa.HiredAsFTE.ToString();
            }

            if (pa.HiredAsFTE == 3)
                rblHiredAsFTE_MSRA.SelectedValue = pa.HiredAsFTE.ToString();

            if (pa.OnsiteInterviewNow > 0 && pa.OnsiteInterviewNow < 3)
                rblOnsiteInterviewNow.SelectedValue = pa.OnsiteInterviewNow.ToString();

            tbExpectedOnsiteInterviewDate.Text = pa.ExpectedOnsiteInterviewDate.ToString("MM/dd/yyyy");
            tbExtendPeriod.Text = pa.ExtendPeriod.ToString();
            if (String.IsNullOrEmpty(pa.MentorComments))
                lbComments.Text = "None";
            else
                lbComments.Text = pa.MentorComments;
            if (String.IsNullOrEmpty(pa.MentorStrength))
                lbStrength.Text = "None";
            else
                lbStrength.Text = pa.MentorStrength;
            if (String.IsNullOrEmpty(pa.MentorWeakness))
                lbWeakness.Text = "None";
            else
                lbWeakness.Text = pa.MentorWeakness;

            String[] Group = new string[] { "MSRA", "STC" };
            bool[] Visible;
            if (pa.Department > 0 && pa.Department < 3)
            {
                switch (PAResourceManager.IdToText("Department", pa.Department))
                {
                    case "STC":
                        Visible = new bool[] { false, true };
                        SetControlVisible(Group, Visible, this.Page);
                        break;
                    default:
                        Visible = new bool[] { true, false };
                        SetControlVisible(Group, Visible, this.Page);
                        break;
                }
            }
            else
            {
                Visible = new bool[] { true, false };
                SetControlVisible(Group, Visible, this.Page);
            }
        }
        /// <summary>
        /// 用正则匹配所有\w\d的控件，设为true/false
        /// </summary>
        /// <param name="Group"></param>
        /// <param name="Visible"></param>
        /// <param name="control"></param>
        protected void SetControlVisible(string[] Group, bool[] Visible, Control control)
        {
            if (control.Controls.Count > 0)
            {
                foreach (Control ChildControl in control.Controls)
                {
                    SetControlVisible(Group, Visible, ChildControl);
                }
            }

            for (int i = 0; i < Group.Length; i++)
            {
                Regex regex = new Regex(@"[\w\d_]+_" + Group[i], RegexOptions.IgnoreCase);
                if (!String.IsNullOrEmpty(control.ID) && regex.IsMatch(control.ID))
                    control.Visible = Visible[i];
            }

            return;
        }
        private void BindRadioButtonList()
        {
            rblCodingskill.DataSource = GetPerformanceLevelDataSource();
            rblCodingskill.DataTextField = "name";
            rblCodingskill.DataValueField = "id";
            rblCodingskill.DataBind();

            rblAnalyticalSkill.DataSource = GetPerformanceLevelDataSource();
            rblAnalyticalSkill.DataTextField = "name";
            rblAnalyticalSkill.DataValueField = "id";
            rblAnalyticalSkill.DataBind();

            rblProblemSolving.DataSource = GetPerformanceLevelDataSource();
            rblProblemSolving.DataTextField = "name";
            rblProblemSolving.DataValueField = "id";
            rblProblemSolving.DataBind();

            rblInnovation.DataSource = GetPerformanceLevelDataSource();
            rblInnovation.DataTextField = "name";
            rblInnovation.DataValueField = "id";
            rblInnovation.DataBind();

            rblDrivingforResults.DataSource = GetPerformanceLevelDataSource();
            rblDrivingforResults.DataTextField = "name";
            rblDrivingforResults.DataValueField = "id";
            rblDrivingforResults.DataBind();

            rblDealingwithAmbiguity.DataSource = GetPerformanceLevelDataSource();
            rblDealingwithAmbiguity.DataTextField = "name";
            rblDealingwithAmbiguity.DataValueField = "id";
            rblDealingwithAmbiguity.DataBind();

            rblQuickonLearning.DataSource = GetPerformanceLevelDataSource();
            rblQuickonLearning.DataTextField = "name";
            rblQuickonLearning.DataValueField = "id";
            rblQuickonLearning.DataBind();

            rblEnglish.DataSource = GetPerformanceLevelDataSource();
            rblEnglish.DataTextField = "name";
            rblEnglish.DataValueField = "id";
            rblEnglish.DataBind();

            rblCommunicationSkills.DataSource = GetPerformanceLevelDataSource();
            rblCommunicationSkills.DataTextField = "name";
            rblCommunicationSkills.DataValueField = "id";
            rblCommunicationSkills.DataBind();

            rblTeamWork.DataSource = GetPerformanceLevelDataSource();
            rblTeamWork.DataTextField = "name";
            rblTeamWork.DataValueField = "id";
            rblTeamWork.DataBind();

            rblAttitude.DataSource = GetPerformanceLevelDataSource();
            rblAttitude.DataTextField = "name";
            rblAttitude.DataValueField = "id";
            rblAttitude.DataBind();

            rblIntegrityandHonesty.DataSource = GetPerformanceLevelDataSource();
            rblIntegrityandHonesty.DataTextField = "name";
            rblIntegrityandHonesty.DataValueField = "id";
            rblIntegrityandHonesty.DataBind();

            rblOpenandRespectful.DataSource = GetPerformanceLevelDataSource();
            rblOpenandRespectful.DataTextField = "name";
            rblOpenandRespectful.DataValueField = "id";
            rblOpenandRespectful.DataBind();

            rblBigChallenges.DataSource = GetPerformanceLevelDataSource();
            rblBigChallenges.DataTextField = "name";
            rblBigChallenges.DataValueField = "id";
            rblBigChallenges.DataBind();

            rblPassion.DataSource = GetPerformanceLevelDataSource();
            rblPassion.DataTextField = "name";
            rblPassion.DataValueField = "id";
            rblPassion.DataBind();

            rblAccountable.DataSource = GetPerformanceLevelDataSource();
            rblAccountable.DataTextField = "name";
            rblAccountable.DataValueField = "id";
            rblAccountable.DataBind();

            rblSelfCritical.DataSource = GetPerformanceLevelDataSource();
            rblSelfCritical.DataTextField = "name";
            rblSelfCritical.DataValueField = "id";
            rblSelfCritical.DataBind();

            rblOverallEvaluation.DataSource = GetPerformanceLevelDataSource();
            rblOverallEvaluation.DataTextField = "name";
            rblOverallEvaluation.DataValueField = "id";
            rblOverallEvaluation.DataBind();

            rblHiredAsFTE_STC.DataSource = PAResourceManager.GetTypeDataSet("HiredAsFTE").Tables[0];
            rblHiredAsFTE_STC.DataTextField = "name";
            rblHiredAsFTE_STC.DataValueField = "id";
            rblHiredAsFTE_STC.DataBind();

            rblHiredAsFTE_MSRA.DataSource = PAResourceManager.GetTypeDataSet("HiredAsFTE_MSRA").Tables[0];
            rblHiredAsFTE_MSRA.DataTextField = "name";
            rblHiredAsFTE_MSRA.DataValueField = "id";
            rblHiredAsFTE_MSRA.DataBind();

            rblOnsiteInterviewNow.DataSource = PAResourceManager.GetTypeDataSet("OnsiteInterviewNow").Tables[0];
            rblOnsiteInterviewNow.DataTextField = "name";
            rblOnsiteInterviewNow.DataValueField = "id";
            rblOnsiteInterviewNow.DataBind();
        }
        #endregion
    }
}