using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.PA_Intern.Config.Schemas;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using MSRA.SpringField.Components.Enumerations;

using MSRA.Springfield.Components.BizObjects;
/*
 * Intern checkout survey online
 * Add by Yuanqin,2011.5.27
 */

namespace MSRA.SpringField.PA_Intern
{
    public partial class InputSurvey : System.Web.UI.Page
    {
        public int InterviewId
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["InterviewId"]);
            }
        }

        public Guid ApplicantId
        {
            get
            {
                if (Session["ApplicantId"] != null)
                {
                    Guid Id = new Guid(Session["ApplicantId"].ToString());
                    return Id;
                }
                else
                {
                    return Guid.Empty;
                }
            }
            set
            {
                Session["ApplicantId"] = value;
            }
        }
        public Guid CheckoutSurveyId
        {
            get
            {
                if (Session["CheckoutSurveyId"] != null)
                {
                    Guid Id = new Guid(Session["CheckoutSurveyId"].ToString());
                    return Id;
                }
                else
                {
                    return Guid.Empty;
                }
            }
            set
            {
                Session["CheckoutSurveyId"] = value;
            }
        }

        public static bool existSurvery=false;
        //private Guid SurveyId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.Params["ApplicantId"]))
                {
                    ApplicantId = new Guid(Request.Params["ApplicantId"]);
                }

                this.btnCheckinDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckinDate.ClientID + ",'mm/dd/yyyy');");
                this.tbCheckinDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckinDate.ClientID + ",'mm/dd/yyyy');");
                //this.tbCheckinDate.Attributes.Add("readonly", "true");

                this.btnCheckoutDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckoutDate.ClientID + ",'mm/dd/yyyy');");
                this.tbCheckoutDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckoutDate.ClientID + ",'mm/dd/yyyy');");
                //this.tbCheckoutDate.Attributes.Add("readonly", "true");

                FillDropdownList();


                //----------Bin 2011-8-29------------------------------
                

                DataSet dsSurvey = CheckoutSurvey.GetCheckoutSurveyByApplicantId(ApplicantId);
                
                DataTable tbSurvey = dsSurvey.Tables[0];

                if (tbSurvey != null && tbSurvey.Rows.Count > 0)
                {
                    existSurvery = true;
                    CheckoutSurveyId = new Guid(tbSurvey.Rows[0]["id"].ToString());
                    //BindData
                    //CheckoutSurvey survey = CheckoutSurvey.GetCheckoutSurveyByApplicantId(ApplicantId);
                    //tbInternName.Text = survey.InternName;
                    lbInternName.Text = tbSurvey.Rows[0]["InternName"].ToString();

                    ddlGroup.SelectedValue = tbSurvey.Rows[0]["GroupId"].ToString();
                    ddlTimeSpan.SelectedValue = tbSurvey.Rows[0]["InternshipDuration"].ToString();
                    tbCheckinDate.Text = Convert.ToDateTime(tbSurvey.Rows[0]["CheckInDate"]).ToString("MM/dd/yyyy");
                    tbCheckoutDate.Text = Convert.ToDateTime(tbSurvey.Rows[0]["CheckOutDate"]).ToString("MM/dd/yyyy");

                    rblOverallView.SelectedValue = tbSurvey.Rows[0]["OverallView"].ToString();
                    tbOverallComments.Text = tbSurvey.Rows[0]["OverallComments"].ToString();

                    rblLikeWork.SelectedValue = tbSurvey.Rows[0]["LikeWork"].ToString();
                    rblBackground.SelectedValue = tbSurvey.Rows[0]["Background"].ToString();
                    rblWorkAmount.SelectedValue = tbSurvey.Rows[0]["WorkAmount"].ToString();
                    rblObjects.SelectedValue = tbSurvey.Rows[0]["Objects"].ToString();
                    rblDevelopmentSkill.SelectedValue = tbSurvey.Rows[0]["DevelopmentSkill"].ToString();
                    rblResearchSkill.SelectedValue = tbSurvey.Rows[0]["ResearchSkill"].ToString();
                    rblSDESkill.SelectedValue = tbSurvey.Rows[0]["SDESkill"].ToString();
                    rblProjectSkill.SelectedValue = tbSurvey.Rows[0]["ProjectSkill"].ToString();
                    rblDesignSkill.SelectedValue = tbSurvey.Rows[0]["DesignSkill"].ToString();
                    rblCommunicationSkill.SelectedValue = tbSurvey.Rows[0]["CommunicationSkill"].ToString();
                    rblTeamwork.SelectedValue = tbSurvey.Rows[0]["Teamwork"].ToString();
                    tbWorkComments.Text = tbSurvey.Rows[0]["WorkComments"].ToString();

                    rblMentorSetGoal.SelectedValue = tbSurvey.Rows[0]["MentorSetGoal"].ToString();
                    rblHelpFromMentor.SelectedValue = tbSurvey.Rows[0]["HelpFromMentor"].ToString();
                    rblMakeGoodUse.SelectedValue = tbSurvey.Rows[0]["MakeGoodUse"].ToString();
                    rblRespect.SelectedValue = tbSurvey.Rows[0]["Respect"].ToString();
                    tbMentorComments.Text = tbSurvey.Rows[0]["MentorComments"].ToString();

                    rblTrainingView.SelectedValue = tbSurvey.Rows[0]["TrainingView"].ToString();
                    rblTrainingSuitable.SelectedValue = tbSurvey.Rows[0]["TrainingSuitable"].ToString();
                    rblTrainingEssential.SelectedValue = tbSurvey.Rows[0]["TrainingEssential"].ToString();
                    rblActivityInterest.SelectedValue = tbSurvey.Rows[0]["ActivityInterest"].ToString();
                    tbTrainingComments.Text = tbSurvey.Rows[0]["TrainingComments"].ToString();

                    rblBalance.SelectedValue = tbSurvey.Rows[0]["Balance"].ToString();
                    rblWorkEnvironment.SelectedValue = tbSurvey.Rows[0]["WorkEnvironment"].ToString();
                    rblCompensation.SelectedValue = tbSurvey.Rows[0]["Compensation"].ToString();
                    rblSatisfaction.SelectedValue = tbSurvey.Rows[0]["Satisfaction"].ToString();
                    rblOnBoard.SelectedValue = tbSurvey.Rows[0]["OnBoard"].ToString();
                    rblAccommodation.SelectedValue = tbSurvey.Rows[0]["Accommodation"].ToString();
                    rblSalaryAndMeal.SelectedValue = tbSurvey.Rows[0]["SalaryAndMeal"].ToString();
                    rblReimbursement.SelectedValue = tbSurvey.Rows[0]["Reimbursement"].ToString();
                    rblITSupport.SelectedValue = tbSurvey.Rows[0]["ITSupport"].ToString();
                    rblDailySupport.SelectedValue = tbSurvey.Rows[0]["DailySupport"].ToString();
                    tbLifeComments.Text = tbSurvey.Rows[0]["LifeComments"].ToString();

                    rblLeading.SelectedValue = tbSurvey.Rows[0]["Leading"].ToString();
                    rblMSCulture.SelectedValue = tbSurvey.Rows[0]["MSCulture"].ToString();
                    rblReturnAsIntern.SelectedValue = tbSurvey.Rows[0]["ReturnAsIntern"].ToString();
                    rblJoinMS.SelectedValue = tbSurvey.Rows[0]["JoinMS"].ToString();
                    rblRecommend.SelectedValue = tbSurvey.Rows[0]["Recommend"].ToString();
                    tbMSRAComments.Text = tbSurvey.Rows[0]["MSRAComments"].ToString();
                    tbComments.Text = tbSurvey.Rows[0]["Comments"].ToString();
                }
                else
                {
                    existSurvery = false;
                    ApplicantBasicInfo m_basicinfo = ApplicantBasicInfo.GetApplicantBasicInfoById(ApplicantId);
                    lbInternName.Text = m_basicinfo.FirstName + " " + m_basicinfo.LastName;
                }
            }
        }

        private void FillDropdownList()
        {
            ddlGroup.DataSource = CheckInFormResourceManager.GetTypeDataSet("Groups").Tables[0];
            ddlGroup.DataValueField = "id";
            ddlGroup.DataTextField = "name";
            ddlGroup.DataBind();
        }

        protected void btnSummit_Click(object sender, EventArgs e)
        {
            btnSummit.Enabled = false;
            btnSummit.Text = "Pending";
            MSRA.Springfield.Components.BizObjects.CheckoutSurvey survey;

            DataSet dsSurvey = CheckoutSurvey.GetCheckoutSurveyByApplicantId(ApplicantId);

            DataTable tbSurvey = dsSurvey.Tables[0];

            if (tbSurvey != null && tbSurvey.Rows.Count > 0)
                existSurvery = true;
            else
                existSurvery = false;

            if (existSurvery)
            {
                survey = CheckoutSurvey.GetCheckoutSurveyById(CheckoutSurveyId);
            }
            else
            {
                survey = new MSRA.Springfield.Components.BizObjects.CheckoutSurvey();
            }

            survey.ApplicantId = ApplicantId;
            survey.InterviewId = InterviewId;
            //survey.InternName = tbInternName.Text.Trim();
            survey.InternName = lbInternName.Text;

            survey.GroupId = Convert.ToInt32(ddlGroup.SelectedValue);
            survey.InternshipDuration = Convert.ToInt32(ddlTimeSpan.SelectedValue);


            survey.CheckInDate = Convert.ToDateTime(tbCheckinDate.Text.Trim());
            survey.CheckOutDate = Convert.ToDateTime(tbCheckoutDate.Text.Trim());


            survey.OverallView = Convert.ToInt32(rblOverallView.SelectedValue.Trim());

            survey.OverallComments = tbOverallComments.Text.Trim();
            //if (!isNullOrEmpty(tbOverallComments.Text.Trim()))
            //{
            //    survey.OverallComments = tbOverallComments.Text.Trim();
            //}
            //else
            //{
            //   survey.OverallComments = null;
            //}

            survey.LikeWork = Convert.ToInt32(rblLikeWork.SelectedValue);
            survey.Background = Convert.ToInt32(rblBackground.SelectedValue);
            survey.WorkAmount = Convert.ToInt32(rblWorkAmount.SelectedValue);
            survey.Objects = Convert.ToInt32(rblObjects.SelectedValue);
            survey.DevelopmentSkill = Convert.ToInt32(rblDevelopmentSkill.SelectedValue);
            survey.ResearchSkill = Convert.ToInt32(rblResearchSkill.SelectedValue);
            survey.SDESkill = Convert.ToInt32(rblSDESkill.SelectedValue);
            survey.ProjectSkill = Convert.ToInt32(rblProjectSkill.SelectedValue);
            survey.DesignSkill = Convert.ToInt32(rblDesignSkill.SelectedValue);
            survey.CommunicationSkill = Convert.ToInt32(rblCommunicationSkill.SelectedValue);
            survey.Teamwork = Convert.ToInt32(rblTeamwork.SelectedValue);

            survey.WorkComments = tbWorkComments.Text.Trim();
            //if (!isNullOrEmpty(tbWorkComments.Text.Trim()))
            //{
            //    survey.WorkComments = tbWorkComments.Text.Trim();
            //}
            //else
            //{
            //    survey.WorkComments = null;
            //}

            survey.MentorSetGoal = Convert.ToInt32(rblMentorSetGoal.SelectedValue);
            survey.HelpFromMentor = Convert.ToInt32(rblHelpFromMentor.SelectedValue);
            survey.MakeGoodUse = Convert.ToInt32(rblMakeGoodUse.SelectedValue);
            survey.Respect = Convert.ToInt32(rblRespect.SelectedValue);

            survey.MentorComments = tbMentorComments.Text.Trim();
            //if (!isNullOrEmpty(tbMentorComments.Text.Trim()))
            //{
            //    survey.MentorComments = tbMentorComments.Text.Trim();
            //}
            //else
            //{
            //    survey.MentorComments = null;
            //}

            survey.TrainingView = Convert.ToInt32(rblTrainingView.SelectedValue);
            survey.TrainingSuitable = Convert.ToInt32(rblTrainingSuitable.SelectedValue);
            survey.TrainingEssential = Convert.ToInt32(rblTrainingEssential.SelectedValue);
            survey.ActivityInterest = Convert.ToInt32(rblActivityInterest.SelectedValue);

            survey.TrainingComments = tbTrainingComments.Text.Trim();
            //if (!isNullOrEmpty(tbTrainingComments.Text.Trim()))
            //{
            //    survey.TrainingComments = tbTrainingComments.Text.Trim();
            //}
            //else
            //{
            //    survey.TrainingComments = null;
            //}

            survey.Balance = Convert.ToInt32(rblBalance.SelectedValue);
            survey.WorkEnvironment = Convert.ToInt32(rblWorkEnvironment.SelectedValue);
            survey.Compensation = Convert.ToInt32(rblCompensation.SelectedValue);
            survey.Satisfaction = Convert.ToInt32(rblSatisfaction.SelectedValue);
            survey.OnBoard = Convert.ToInt32(rblOnBoard.SelectedValue);
            survey.Accommodation = Convert.ToInt32(rblAccommodation.SelectedValue);
            survey.SalaryAndMeal = Convert.ToInt32(rblSalaryAndMeal.SelectedValue);
            survey.Reimbursement = Convert.ToInt32(rblReimbursement.SelectedValue);
            survey.ITSupport = Convert.ToInt32(rblITSupport.SelectedValue);
            survey.DailySupport = Convert.ToInt32(rblDailySupport.SelectedValue);

            survey.LifeComments = tbLifeComments.Text.Trim();
            //if (!isNullOrEmpty(tbLifeComments.Text.Trim()))
            //{
            //    survey.LifeComments = tbLifeComments.Text.Trim();
            //}
            //else
            //{
            //    survey.LifeComments = null;
            //}

            survey.Leading = Convert.ToInt32(rblLeading.SelectedValue);
            survey.MSCulture = Convert.ToInt32(rblMSCulture.SelectedValue);
            survey.ReturnAsIntern = Convert.ToInt32(rblReturnAsIntern.SelectedValue);
            survey.JoinMS = Convert.ToInt32(rblJoinMS.SelectedValue);
            survey.Recommend = Convert.ToInt32(rblRecommend.SelectedValue);

            survey.MSRAComments = tbMSRAComments.Text.Trim();
            //if (!isNullOrEmpty(tbMSRAComments.Text.Trim()))
            //{
            //    survey.MSRAComments = tbMSRAComments.Text.Trim();
            //}
            //else
            //{
            //    survey.MSRAComments = null;
            //}

            survey.Comments = tbComments.Text.Trim();
            //if (!isNullOrEmpty(tbComments.Text.Trim()))
            //{
            //    survey.Comments = tbComments.Text.Trim();
            //}
            //else
            //{
            //    survey.Comments = null;
            //}

            //survey.InsertDate = DateTime.Now;

            if (existSurvery)
            {
                survey.Update();
            }
            else
            {
                CheckoutSurveyId = survey.Insert();
                survey.InsertDate = DateTime.Now;
            }


            bool IsUpdated = survey.Update();
            if (!IsUpdated)
            {
                Response.Write("<script>alert('Failed to Summit Checkout Survey, please input again!');</script>");
                return;
            }


            //send email 发的是type为21的check out survey report给mentor
            MailHelper mailHelper = new MailHelper();
            //mailHelper.AddCheckoutSurveyVariables(lbInternName.Text.Trim(), ApplicantId);
            mailHelper.AddCheckoutSurveyVariables(lbInternName.Text, ApplicantId);
            mailHelper.SendMail(MailType.CheckoutSurvey);
            Response.Write("<script>alert('Checkout Survey successfully summited!'); window.close();</script>");

            btnSummit.Text = "Submit";
            btnSummit.Enabled = true;

        }

        private bool isNullOrEmpty(string str)
        {
            if (str != "" && !String.IsNullOrEmpty(str))
                return false;
            else
                return true;
        }
    }
}
