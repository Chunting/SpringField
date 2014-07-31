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
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components;
using MSRA.SpringField.Application.Config.Schemas;
using System.Linq;
using MSRA.SpringField.Components.BizObjects;

/*
 * CheckoutSurvey.ascx
 * Add by Yuanqin, 2011.6.4
 */ 

namespace MSRA.SpringField.Application.Controls
{
    public partial class CheckoutSurvey : System.Web.UI.UserControl
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
                BindSurveyList();

                // 下面这行如果运行，就会跳过权限，直接显示Survey Report,比如改成["paid"]神马的
                if (Request.Params["SurveyId"] != null)//always FALSE now
                {
                    Guid SurveyId = new Guid(Request.Params["SurveyId"].ToString());
                    BindSurveyDetail(SurveyId);//用相同方法在下面运行中
                }
            }
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

        protected string GetInternshipDurationByID(string ID)
        {
            string Duration = "";

            if (ID == "1")
            {
                Duration = "0 - 3M";
            }
            else if(ID == "2")
            {
                Duration = "4 - 6M";
            }
            else if (ID == "3")
            {
                Duration = "7 - 12M";
            }
            else if (ID == "4")
            {
                Duration = "One year above";
            }

            return Duration;
        }

        //这里对权限进行了限制
        private void BindSurveyList()
        {
            DataSet dsSurvey = MSRA.Springfield.Components.BizObjects.CheckoutSurvey.GetCheckoutSurvey();
            DataView dvSurvey = new DataView(dsSurvey.Tables[0]);
            string Filter = "(ApplicantId = '" + ApplicantId + "')";
            dvSurvey.Sort = "InsertDate DESC";
            dvSurvey.RowFilter = Filter;

            //上面的gv没有权限限制 Check-In Date	 Check-Out Date	 Group	 InternshipDuration	Action 
            gvSurveyList.DataSource = dvSurvey;
            gvSurveyList.DataBind();

            
            if (gvSurveyList.Rows.Count > 0
                && (SiteUser.Current.IsInRole(RoleType.GroupManager)
                        || SiteUser.Current.IsInRole(RoleType.OnBoardManager)
                        || SiteUser.Current.IsInRole(RoleType.InternRecruiter)
                        || SiteUser.Current.IsInRole(RoleType.DefaultUser)//为了让mentor们看checkoutSurvey，加了这一行，LMM:2012-08-08
                    )
                && Request.Params["SurveyId"] == null)//always TRUE   //前面注释内容直接放到右括号前面即可，似乎是没用的东西 LMM 2012-08-08
            {
                Guid firstSurveyId = new Guid(((Label)gvSurveyList.Rows[0].Cells[0].Controls[1]).Text);
                BindSurveyDetail(firstSurveyId);
            }
        }

        private void BindSurveyDetail(Guid SurveyId)
        {
            this.PanelPADetail.Visible = true;
            BindSurvey(SurveyId);
        }

        private void BindSurvey(Guid SurveyId)
        {
            MSRA.Springfield.Components.BizObjects.CheckoutSurvey survey = MSRA.Springfield.Components.BizObjects.CheckoutSurvey.GetCheckoutSurveyById(SurveyId);

            lbInternName.Text = survey.InternName;
            if(survey.GroupId > 0)
                lbGroup.Text = CheckInFormResourceManager.IdToText("Groups", survey.GroupId);
            lbInternshipDuration.Text = GetInternshipDurationByID(survey.InternshipDuration.ToString());
            lbCheckInDate.Text = survey.CheckInDate.ToString("yyyy-MM-dd");
            lbCheckOutDate.Text = survey.CheckOutDate.ToString("yyyy-MM-dd");
            rblOverallView.SelectedValue = survey.OverallView.ToString();
            if (String.IsNullOrEmpty(survey.OverallComments))
                lbOverallComments.Text = "None";
            else
                lbOverallComments.Text = survey.OverallComments.ToString();

            rblLikeWork.SelectedValue = survey.LikeWork.ToString();
            rblBackground.SelectedValue = survey.Background.ToString();
            rblWorkAmount.SelectedValue = survey.WorkAmount.ToString();
            rblObjects.SelectedValue = survey.Objects.ToString();
            rblDevelopmentSkill.SelectedValue = survey.DevelopmentSkill.ToString();
            rblResearchSkill.SelectedValue = survey.ResearchSkill.ToString();
            rblSDESkill.SelectedValue = survey.SDESkill.ToString();
            rblProjectSkill.SelectedValue = survey.ProjectSkill.ToString();
            rblDesignSkill.SelectedValue = survey.DesignSkill.ToString();

            rblCommunicationSkill.SelectedValue = survey.CommunicationSkill.ToString();
            rblTeamwork.SelectedValue = survey.Teamwork.ToString();
            if (String.IsNullOrEmpty(survey.WorkComments))
                lbWorkComments.Text = "None";
            else
                lbWorkComments.Text = survey.WorkComments.ToString();


            rblMentorSetGoal.SelectedValue = survey.MentorSetGoal.ToString();
            rblHelpFromMentor.SelectedValue = survey.HelpFromMentor.ToString();
            rblMakeGoodUse.SelectedValue = survey.MakeGoodUse.ToString();
            rblRespect.SelectedValue = survey.Respect.ToString();
            if (String.IsNullOrEmpty(survey.MentorComments))
                lbMentorComments.Text = "None";
            else
                lbMentorComments.Text = survey.MentorComments.ToString();

            rblTrainingView.SelectedValue = survey.TrainingView.ToString();
            rblTrainingSuitable.SelectedValue = survey.TrainingSuitable.ToString();
            rblTrainingEssential.SelectedValue = survey.TrainingEssential.ToString();
            rblActivityInterest.SelectedValue = survey.ActivityInterest.ToString();
            if (String.IsNullOrEmpty(survey.TrainingComments))
                lbTrainingComments.Text = "None";
            else
                lbTrainingComments.Text = survey.TrainingComments.ToString();

            rblBalance.SelectedValue = survey.Balance.ToString();
            rblWorkEnvironment.SelectedValue = survey.WorkEnvironment.ToString();
            rblCompensation.SelectedValue = survey.Compensation.ToString();
            rblSatisfaction.SelectedValue = survey.Satisfaction.ToString();
            rblOnBoard.SelectedValue = survey.OnBoard.ToString();
            rblAccommodation.SelectedValue = survey.Accommodation.ToString();
            rblSalaryAndMeal.SelectedValue = survey.SalaryAndMeal.ToString();
            rblReimbursement.SelectedValue = survey.Reimbursement.ToString();
            rblITSupport.SelectedValue = survey.ITSupport.ToString();
            rblDailySupport.SelectedValue = survey.DailySupport.ToString();
            if (String.IsNullOrEmpty(survey.LifeComments))
                lbLifeComments.Text = "None";
            else
                lbLifeComments.Text = survey.LifeComments.ToString();

            rblLeading.SelectedValue = survey.Leading.ToString();
            rblMSCulture.SelectedValue = survey.MSCulture.ToString();
            rblReturnAsIntern.SelectedValue = survey.ReturnAsIntern.ToString();
            rblJoinMS.SelectedValue = survey.JoinMS.ToString();
            rblRecommend.SelectedValue = survey.Recommend.ToString();

            if (String.IsNullOrEmpty(survey.MSRAComments))
                lbMSRAComments.Text = "None";
            else
                lbMSRAComments.Text = survey.MSRAComments.ToString();
            if (String.IsNullOrEmpty(survey.Comments))
                lbComments.Text = "None";
            else
                lbComments.Text = survey.Comments.ToString();
        }

        protected void gvSurveyList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "SurveyDetail":
                    Guid DetailSurveyId = new Guid(e.CommandArgument.ToString());
                    BindSurveyDetail(DetailSurveyId);
                    break;
                case "DeleteSurvey":
                    Guid DeleteSurveyId = new Guid(e.CommandArgument.ToString());
                    bool isSuccess = MSRA.Springfield.Components.BizObjects.CheckoutSurvey.DeleteCheckoutSurveybyId(DeleteSurveyId);
                    if (isSuccess)
                        JSUtility.Alert(this.Page, "Successful Deleted!");
                    else
                        JSUtility.Alert(this.Page, "Failed to delete!");
                    BindSurveyList();
                    break;
            }
        }
        protected void gvSurveyList_PreRender(object sender, EventArgs e)
        {
            if (SiteUser.Current.IsInRole(RoleType.InternRecruiter) || SiteUser.Current.IsInRole(RoleType.OnBoardManager))
            {
                foreach (GridViewRow row in gvSurveyList.Rows)
                {
                    ((LinkButton)row.FindControl("lnkbtnDetail")).Visible = true;
                    ((LinkButton)row.FindControl("lnkbtnDeletePA")).Visible = true;
                }
            }
            if (SiteUser.Current.IsInRole(RoleType.GroupManager))
            {
                foreach (GridViewRow row in gvSurveyList.Rows)
                {
                    ((LinkButton)row.FindControl("lnkbtnDetail")).Visible = true;
                }
            }
        }
    }
}