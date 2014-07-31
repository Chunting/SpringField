using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components;
using MSRA.SpringField.Application.Config.Schemas;
using System.Linq;
using MSRA.SpringField.Components.Enumerations;
using System.Web.Security;

namespace MSRA.SpringField.Application
{
    public partial class MentorPA : System.Web.UI.Page
    {
        private PerformanceAssessment pa;
        public Guid PerformanceAssessmentId
        {
            get
            {
                if (Session["PerformanceAssessmentId"] != null)
                {
                    Guid Id = new Guid(Session["PerformanceAssessmentId"].ToString());
                    return Id;
                }
                else if (Request.Params["PAID"] != null)
                {
                    return new Guid(Request.Params["PAID"].ToString());
                }
                else
                {
                    return Guid.Empty;
                }
            }
            set
            {
                Session["PerformanceAssessmentId"] = value;
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

        /// <summary>
        /// this is the first time mentor summit this PA or edit this PA after the first suumit
        /// Value: summit, edit
        /// </summary>
        public string Mode
        {
            get
            {
                if (Session["Mode"] != null)
                {
                    return Session["Mode"].ToString();
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                Session["Mode"] = value;
            }
        }

        public bool isEditBasicInfo//whether modify basic infomation
        {
            get
            {
                if (Session["isEditBasicInfo"] != null)
                {
                    return (bool)Session["isEditBasicInfo"];
                }
                else
                {
                    return false;
                }
            }
            set
            {
                Session["isEditBasicInfo"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //是否显示complete by ur按钮            
            if (Roles.IsUserInRole(SiteUser.Current.FullName, RoleType.InternRecruiter.ToString()))
            {
                btnSummitByUR.Visible = true;
            }

            SpringFieldDataContext ctx = new SpringFieldDataContext();
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.Params["PAId"]))
                {
                    PerformanceAssessmentId = new Guid(Request.Params["PAId"]);
                }

                this.btnCheckinDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckinDate.ClientID + ",'mm/dd/yyyy');");
                this.tbCheckinDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckinDate.ClientID + ",'mm/dd/yyyy');");
                this.tbCheckinDate.Attributes.Add("readonly", "true");
                this.btnCheckoutDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckoutDate.ClientID + ",'mm/dd/yyyy');");
                this.tbCheckoutDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckoutDate.ClientID + ",'mm/dd/yyyy');");
                this.tbCheckoutDate.Attributes.Add("readonly", "true");
                this.btnExpectedOnsiteInterviewDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbExpectedOnsiteInterviewDate.ClientID + ",'mm/dd/yyyy');");
                this.tbExpectedOnsiteInterviewDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbExpectedOnsiteInterviewDate.ClientID + ",'mm/dd/yyyy');");
                this.tbExpectedOnsiteInterviewDate.Attributes.Add("readonly", "true");
                this.btnGraduationDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbGraduationDate.ClientID + ",'mm/dd/yyyy');");
                this.tbGraduationDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbGraduationDate.ClientID + ",'mm/dd/yyyy');");
                this.tbGraduationDate.Attributes.Add("readonly", "true");

                FillDropdownList();
                BindRadioButtonList();
                BindPA();
                gvPublicationBindData();

                /*
                 * Charge if it should display the information of STC.
                 * @Author: Yin.P
                 * @Date: 2009-12-08
                 */
                #region Charge if it should display the information of STC.
                sf_PerformanceAssessment paObj =
                    ctx.sf_PerformanceAssessments.FirstOrDefault<sf_PerformanceAssessment>(p => p.id.Equals(PerformanceAssessmentId));

                if (paObj != null && paObj.Department != null)
                {
                    //string groupName = CheckInFormResourceManager.IdToText("Groups", paObj.GroupId.Value);
                    string departmenName = MSRA.SpringField.Components.PAResourceManager.IdToText("Department", paObj.Department.Value);
                    String[] Group = new string[] { "MSRA", "STC" };
                    if ("STC".Equals(departmenName) ||
                        MSRA.SpringField.Components.CheckInFormResourceManager.IdToText("Groups", pa.GroupId).Equals("STC"))
                    {
                        this.ddlDepartment.Items.FindByText("STC").Selected = true;
                        this.ddlDepartment.Items.FindByText("MSRA").Selected = false;
                        Group = new string[] { "MSRA", "STC" };
                        bool[] Visible = new bool[] { false, true };
                        SetControlVisible(Group, Visible, this.Page);
                    }
                    else
                    {
                        this.ddlDepartment.Items.FindByText("MSRA").Selected = true;
                        this.ddlDepartment.Items.FindByText("STC").Selected = false;
                        bool[] Visible = new bool[] { true, false };
                        SetControlVisible(Group, Visible, this.Page);
                    }
                }
                #endregion
            }
        }

        private void BindInternPA()
        {
            pa = PerformanceAssessment.GetPerformanceAssessmentById(PerformanceAssessmentId);

            ddlDepartment.SelectedValue = pa.Department.ToString();
            this.tbObjective.Text = pa.Objective;
            this.tbSelfEvaluation.Text = pa.SelfEvaluation;
            this.tbInternSW.Text = pa.StrengthsAndWeaknesses;
            lbName.Text = pa.InternName;
            tbPhone.Text = pa.InternPhone;
            tbEmail.Text = pa.InternEmail;
            ddlGroup.SelectedValue = pa.GroupId.ToString();
            tbCheckinDate.Text = pa.CheckInDate.ToString("MM/dd/yyyy");
            tbCheckoutDate.Text = pa.CheckOutDate.ToString("MM/dd/yyyy");
            tbGraduationDate.Text = pa.GraduationDate.ToString("MM/dd/yyyy");
            tbMentorName.Text = pa.MentorName;
            ApplicantId = pa.ApplicantId;

            ddlProject.SelectedValue = pa.ProjectId.ToString();
            ddlInternPosition.SelectedValue = pa.InternPosition.ToString();
            if (pa.GroupMgrId != Guid.Empty)
                tbGroupManagerAlias.Text = SiteUser.GetAliasByUserId(pa.GroupMgrId);

            ddlDiscipline_STC.SelectedValue = int.Parse(pa.Discipline.ToString()) == 0 ? "1" : pa.Discipline.ToString();

            if (!String.IsNullOrEmpty(pa.Pipeline.Trim()))
                ddlPipeline_STC.SelectedValue = pa.Pipeline.Trim();


        }

        private void BindPA()
        {
            BindInternPA();

            String[] Group = new string[] { "MSRA", "STC" };
            bool[] Visible;
            switch (PAResourceManager.IdToText("Department", Convert.ToInt32(ddlDepartment.SelectedValue)))
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

            //if OverrallEvaluation's value is beteween 1 and RATIO_SIZE-1，then we suppose mentor has summit PA
            const int RATIO_SIZE = 7;
            if (pa.OverrallEvaluation > 0 && pa.OverrallEvaluation < RATIO_SIZE)
            {
                Mode = "edit";

                if (pa.CodingSkill > 0 && pa.CodingSkill < RATIO_SIZE)
                    rblCodingskill.SelectedValue = pa.CodingSkill.ToString();

                if (pa.AnalyticalSkill > 0 && pa.AnalyticalSkill < RATIO_SIZE)
                    rblAnalyticalSkill.SelectedValue = pa.AnalyticalSkill.ToString();

                if (pa.ProblemSolving > 0 && pa.ProblemSolving < RATIO_SIZE)
                    rblProblemSolving.SelectedValue = pa.ProblemSolving.ToString();

                if (pa.Innovation > 0 && pa.Innovation < RATIO_SIZE)
                    rblInnovation.SelectedValue = pa.Innovation.ToString();

                if (pa.DrivingForResults > 0 && pa.DrivingForResults < RATIO_SIZE)
                    rblDrivingforResults.SelectedValue = pa.DrivingForResults.ToString();

                if (pa.DealingWithAmbiguity > 0 && pa.DealingWithAmbiguity < RATIO_SIZE)
                    rblDealingwithAmbiguity.SelectedValue = pa.DealingWithAmbiguity.ToString();

                if (pa.QuickOnLearing > 0 && pa.QuickOnLearing < RATIO_SIZE)
                    rblQuickonLearning.SelectedValue = pa.QuickOnLearing.ToString();

                if (pa.English > 0 && pa.English < RATIO_SIZE)
                    rblEnglish.SelectedValue = pa.English.ToString();

                if (pa.Communication > 0 && pa.Communication < RATIO_SIZE)
                    rblCommunicationSkills.SelectedValue = pa.Communication.ToString();

                if (pa.TeamWork > 0 && pa.TeamWork < RATIO_SIZE)
                    rblTeamWork.SelectedValue = pa.TeamWork.ToString();

                if (pa.Attitude > 0 && pa.Attitude < RATIO_SIZE)
                    rblAttitude.SelectedValue = pa.Attitude.ToString();

                if (pa.IntegrityHonesty > 0 && pa.IntegrityHonesty < RATIO_SIZE)
                    rblIntegrityandHonesty.SelectedValue = pa.IntegrityHonesty.ToString();

                if (pa.OpenRespectful > 0 && pa.OpenRespectful < RATIO_SIZE)
                    rblOpenandRespectful.SelectedValue = pa.OpenRespectful.ToString();

                if (pa.BigChallenges > 0 && pa.BigChallenges < RATIO_SIZE)
                    rblBigChallenges.SelectedValue = pa.BigChallenges.ToString();

                if (pa.Passion > 0 && pa.Passion < RATIO_SIZE)
                    rblPassion.SelectedValue = pa.Passion.ToString();

                if (pa.Accountable > 0 && pa.Accountable < RATIO_SIZE)
                    rblAccountable.SelectedValue = pa.Accountable.ToString();

                if (pa.SelfCritical > 0 && pa.SelfCritical < RATIO_SIZE)
                    rblSelfCritical.SelectedValue = pa.SelfCritical.ToString();

                if (pa.OverrallEvaluation > 0 && pa.OverrallEvaluation < RATIO_SIZE)
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
                tbComments.Text = pa.MentorComments;
                tbStrength.Text = pa.MentorStrength;
                tbWeakness.Text = pa.MentorWeakness;

            }
            else
            {
                Mode = "Submit";
            }
        }

        protected void btnEditInternPA_Click(object sender, EventArgs e)
        {
            ChangetoEdit();
            btnEditInternPA.Visible = false;
            isEditBasicInfo = true;
            this.btnSaveEdittingInternPA.Visible = true;
            this.btnCancelEdittingInternPA.Visible = true;
        }

        private void gvPublicationBindData()
        {
            DataSet dsPabulication = new DataSet();
            dsPabulication = InternPublication.GetInternPublicationByPAId(PerformanceAssessmentId);

            gvPublication.DataSource = dsPabulication.Tables[0].DefaultView;
            gvPublication.DataBind();
        }
        protected string StatusIdToString(string Id)
        {
            return PAResourceManager.IdToText("PaperStatus", Convert.ToInt32(Id));
        }

        protected void btnCancelEdittingInternPA_Click(object sender, EventArgs e)
        {
            BindInternPA();
            ChangetoView();
            this.btnEditInternPA.Visible = true;
            this.btnSaveEdittingInternPA.Visible = false;
            this.btnCancelEdittingInternPA.Visible = false;
            isEditBasicInfo = false;
        }

        protected void btnSaveEdittingInternPA_Click(object sender, EventArgs e)
        {
            PerformanceAssessment PA = PerformanceAssessment.GetPerformanceAssessmentById(PerformanceAssessmentId);

            if (isEditBasicInfo)
            {
                PA.InternName = lbName.Text;//###
                PA.InternPhone = tbPhone.Text;
                PA.InternEmail = tbEmail.Text;
                PA.GroupId = Convert.ToInt32(ddlGroup.SelectedValue);
                PA.CheckInDate = Convert.ToDateTime(tbCheckinDate.Text);
                PA.CheckOutDate = Convert.ToDateTime(tbCheckoutDate.Text);
                PA.GraduationDate = Convert.ToDateTime(tbGraduationDate.Text);
                PA.MentorName = tbMentorName.Text.Trim();
                PA.Objective = tbObjective.Text;
                PA.SelfEvaluation = tbSelfEvaluation.Text;
                PA.StrengthsAndWeaknesses = tbInternSW.Text;

                PA.ProjectId = Convert.ToInt32(ddlProject.SelectedValue);
                PA.InternPosition = Convert.ToInt32(ddlInternPosition.SelectedValue);

                //TODO: The mentor's alias is needed, but the full names of mentors were filled in the review forms
                if (SiteUser.GetUserIdByAlias(tbGroupManagerAlias.Text.Trim()).Length == 0)
                {
                    PA.GroupMgrId = new Guid();
                }
                else
                {
                    PA.GroupMgrId = new Guid(SiteUser.GetUserIdByAlias(tbGroupManagerAlias.Text.Trim()));
                }

                if (ddlDepartment.SelectedValue == PAResourceManager.TextToId("Department", "STC").ToString())
                {
                    PA.Discipline = Convert.ToInt32(ddlDiscipline_STC.SelectedValue);
                    PA.Pipeline = ddlPipeline_STC.SelectedValue;
                }
            }

            PA.Update();

            ChangetoView();
            this.btnEditInternPA.Visible = true;
            this.btnSaveEdittingInternPA.Visible = false;
            this.btnCancelEdittingInternPA.Visible = false;
            isEditBasicInfo = false;

        }

        //改变最上面基本信息的状态
        private void ChangetoEdit()
        {
            lbName.Enabled = true;//##
            tbEmail.Enabled = true;
            tbPhone.Enabled = true;
            ddlGroup.Enabled = true;
            tbCheckinDate.Enabled = true;
            tbCheckoutDate.Enabled = true;
            tbGraduationDate.Enabled = true;
            tbMentorName.Enabled = true;
            tbObjective.Enabled = true;
            tbSelfEvaluation.Enabled = true;
            tbInternSW.Enabled = true;
            btnCheckinDate.Visible = true;
            btnCheckoutDate.Visible = true;
            btnGraduationDate.Visible = true;
            ddlProject.Enabled = true;
            ddlInternPosition.Enabled = true;
            tbGroupManagerAlias.Enabled = true;
            ddlDiscipline_STC.Enabled = true;
            ddlPipeline_STC.Enabled = true;
        }
        private void ChangetoView()
        {
            lbName.Enabled = false;
            tbEmail.Enabled = false;
            tbPhone.Enabled = false;
            ddlGroup.Enabled = false;
            tbCheckinDate.Enabled = false;
            tbCheckoutDate.Enabled = false;
            tbGraduationDate.Enabled = false;
            tbMentorName.Enabled = false;
            tbObjective.Enabled = false;
            tbSelfEvaluation.Enabled = false;
            tbInternSW.Enabled = false;
            btnCheckinDate.Visible = false;
            btnCheckoutDate.Visible = false;
            btnGraduationDate.Visible = false;
            ddlProject.Enabled = false;
            ddlInternPosition.Enabled = false;
            tbGroupManagerAlias.Enabled = false;
            ddlDiscipline_STC.Enabled = false;
            ddlPipeline_STC.Enabled = false;
        }
        private void FillDropdownList()
        {
            ddlPaperStatus.DataSource = PAResourceManager.GetTypeDisplayItems("PaperStatus");
            ddlPaperStatus.DataBind();
            ddlGroup.DataSource = CheckInFormResourceManager.GetTypeDataSet("Groups").Tables[0];
            ddlGroup.DataValueField = "id";
            ddlGroup.DataTextField = "name";
            ddlGroup.DataBind();
            ddlDiscipline_STC.DataSource = CheckInFormResourceManager.GetTypeDataSet("Positions").Tables[0];//the "Intern's position" in "New Intern On Board Request" correspond to "Displine" in PA
            ddlDiscipline_STC.DataValueField = "id";
            ddlDiscipline_STC.DataTextField = "name";
            ddlDiscipline_STC.DataBind();
            ddlPipeline_STC.DataSource = PAResourceManager.GetTypeDataSet("ProjectBasedorFTEPipeline").Tables[0];
            ddlPipeline_STC.DataValueField = "id";
            ddlPipeline_STC.DataTextField = "name";
            ddlPipeline_STC.DataBind();
            ddlInternPosition.DataSource = PAResourceManager.GetTypeDataSet("InternPosition").Tables[0];//the "Intern Type" in "New Intern On Board Request" correspond to "Intern Position" in PA
            ddlInternPosition.DataValueField = "id";
            ddlInternPosition.DataTextField = "name";
            ddlInternPosition.DataBind();
            ddlProject.DataSource = CheckInFormResourceManager.GetTypeDataSet("Projects").Tables[0];
            ddlProject.DataValueField = "id";
            ddlProject.DataTextField = "name";
            ddlProject.DataBind();
            ddlDepartment.DataSource = PAResourceManager.GetTypeDataSet("Department").Tables[0];
            ddlDepartment.DataValueField = "id";
            ddlDepartment.DataTextField = "name";
            ddlDepartment.DataBind();
        }

        protected DataTable GetPerformanceLevelDataSource()
        {
            DataTable SourceTable = new DataTable();
            SourceTable = PAResourceManager.GetTypeDataSet("PerformanceLevel").Tables[0];

            return SourceTable;

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


        // 最底端的submit按钮
        protected void btnSummit_Click(object sender, EventArgs e)
        {
            //改变一下按钮的内容，表示已经点击了submit
            btnSummit.Enabled = false;
            btnSummit.Text = "Pending";

            PerformanceAssessment PA = PerformanceAssessment.GetPerformanceAssessmentById(PerformanceAssessmentId);

            if (isEditBasicInfo)
            {
                PA.InternPhone = tbPhone.Text;
                PA.InternEmail = tbEmail.Text;
                PA.GroupId = Convert.ToInt32(ddlGroup.SelectedValue);
                PA.CheckInDate = Convert.ToDateTime(tbCheckinDate.Text);
                PA.CheckOutDate = Convert.ToDateTime(tbCheckoutDate.Text);
                PA.GraduationDate = Convert.ToDateTime(tbGraduationDate.Text);
                PA.MentorName = tbMentorName.Text.Trim();

                PA.Objective = tbObjective.Text;
                PA.SelfEvaluation = tbSelfEvaluation.Text;
                PA.StrengthsAndWeaknesses = tbInternSW.Text;

                PA.ProjectId = Convert.ToInt32(ddlProject.SelectedValue);
                PA.InternPosition = Convert.ToInt32(ddlInternPosition.SelectedValue);

                //TODO: The mentor's alias is needed, but the full names of mentors were filled in the review forms
                if (SiteUser.GetUserIdByAlias(tbGroupManagerAlias.Text.Trim()).Length == 0)
                {
                    PA.GroupMgrId = new Guid();
                }
                else
                {
                    PA.GroupMgrId = new Guid(SiteUser.GetUserIdByAlias(tbGroupManagerAlias.Text.Trim()));
                }

                if (ddlDepartment.SelectedValue == PAResourceManager.TextToId("Department", "STC").ToString())
                {
                    PA.Discipline = Convert.ToInt32(ddlDiscipline_STC.SelectedValue);
                    PA.Pipeline = ddlPipeline_STC.SelectedValue;
                }
            }
            //start
            PA.Department = Convert.ToInt32(ddlDepartment.SelectedValue);//最顶端的MSRA/STC
            PA.CodingSkill = Convert.ToInt32(rblCodingskill.SelectedValue);
            PA.AnalyticalSkill = Convert.ToInt32(rblAnalyticalSkill.SelectedValue);
            PA.ProblemSolving = Convert.ToInt32(rblProblemSolving.SelectedValue);
            PA.Innovation = Convert.ToInt32(rblInnovation.SelectedValue);
            PA.DrivingForResults = Convert.ToInt32(rblDrivingforResults.SelectedValue);
            PA.DealingWithAmbiguity = Convert.ToInt32(rblDealingwithAmbiguity.SelectedValue);
            PA.QuickOnLearing = Convert.ToInt32(rblQuickonLearning.SelectedValue);
            PA.English = Convert.ToInt32(rblEnglish.SelectedValue);
            PA.Communication = Convert.ToInt32(rblCommunicationSkills.SelectedValue);
            PA.TeamWork = Convert.ToInt32(rblTeamWork.SelectedValue);
            PA.Attitude = Convert.ToInt32(rblAttitude.SelectedValue);
            PA.IntegrityHonesty = Convert.ToInt32(rblIntegrityandHonesty.SelectedValue);
            PA.OpenRespectful = Convert.ToInt32(rblOpenandRespectful.SelectedValue);
            PA.BigChallenges = Convert.ToInt32(rblBigChallenges.SelectedValue);
            PA.Passion = Convert.ToInt32(rblPassion.SelectedValue);
            PA.Accountable = Convert.ToInt32(rblAccountable.SelectedValue);
            PA.SelfCritical = Convert.ToInt32(rblSelfCritical.SelectedValue);
            PA.OverrallEvaluation = Convert.ToInt32(rblOverallEvaluation.SelectedValue);

            PA.MentorComments = tbComments.Text;
            PA.MentorStrength = tbStrength.Text;
            PA.MentorWeakness = tbWeakness.Text;
            //end

            if (ddlDepartment.SelectedValue == PAResourceManager.TextToId("Department", "STC").ToString() && rblHiredAsFTE_STC.SelectedIndex > -1)
                PA.HiredAsFTE = Convert.ToInt32(rblHiredAsFTE_STC.SelectedValue);
            if (ddlDepartment.SelectedValue == PAResourceManager.TextToId("Department", "MSRA").ToString() && rblHiredAsFTE_MSRA.SelectedIndex > -1)
                PA.HiredAsFTE = Convert.ToInt32(rblHiredAsFTE_MSRA.SelectedValue);
            if (!String.IsNullOrEmpty(rblOnsiteInterviewNow.SelectedValue))
                PA.OnsiteInterviewNow = Convert.ToInt32(rblOnsiteInterviewNow.SelectedValue);


            if (!String.IsNullOrEmpty(tbExpectedOnsiteInterviewDate.Text))
                PA.ExpectedOnsiteInterviewDate = Convert.ToDateTime(tbExpectedOnsiteInterviewDate.Text);
            if (!String.IsNullOrEmpty(tbExtendPeriod.Text))
                PA.ExtendPeriod = Convert.ToDecimal(tbExtendPeriod.Text.Trim());

            //check if the applicant is exist before updating.
            SpringFieldDataContext ctx = new SpringFieldDataContext();
            sf_ApplicantBasicInfo result =
                ctx.sf_ApplicantBasicInfos.FirstOrDefault<sf_ApplicantBasicInfo>(p => p.ApplicantId.Equals(PA.ApplicantId));
            if (result != null)
            {
                /*
                 * Add IsApproved = 0
                 * Author: Yuanqin
                 * Date: 2011.3.14
                 */
                PA.IsApproved = 1;//表示已经完成

                //IsApproved: 1=完成 4=Incomplete 0=Inprocess 9=Completed By UR

                PA.Update();

                if (Mode == "Submit")
                {
                    /*
                     * PA Approval
                     * Send mail to group manager after being approved
                     */
                    MailHelper mailHelper = new MailHelper();
                    mailHelper.AddPerformanceAssessmentVariables(PA.Id);
                    mailHelper.SendMail(MailType.PANotice);
                }
                string ViewingPAURL = "ShowApplication.aspx?applicant=" + PA.ApplicantId.ToString() + "&tab=2&PAID=" + PA.Id.ToString();
                this.Response.Write("<script   language=javascript>alert('Successfully submit this performance assessment!');window.location   ='" + ViewingPAURL + "';</script>");
            }
            else
            {
                JSUtility.Alert(this.Page, "Current applicant is not exist...");
            }

            btnSummit.Text = "Submit";
            btnSummit.Enabled = true;
        }
        // 最底端的submitByUR按钮
        protected void btnSummitByUR_Click(object sender, EventArgs e)
        {
            PerformanceAssessment PA = PerformanceAssessment.GetPerformanceAssessmentById(PerformanceAssessmentId);
            //IsApproved: 1=完成 4=Incomplete 0=Inprocess 9=Completed By UR
            PA.IsApproved = 9;//表示Completed By UR
            PA.Update();
            JSUtility.Alert(this.Page, "Approval Status has been changed to 'Completed By UR' ");
        }


        protected void gvPublication_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Guid PublicationId = new Guid(gvPublication.DataKeys[e.RowIndex]["PublicationId"].ToString());
            InternPublication CurrentPublication = InternPublication.GetInternPublicationById(PublicationId);
            CurrentPublication.Name = ((TextBox)gvPublication.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            DropDownList ddlStatus = (DropDownList)gvPublication.Rows[e.RowIndex].FindControl("ddlgvPaperStatus");
            CurrentPublication.CurrentStatus = PAResourceManager.TextToId("PaperStatus", ddlStatus.SelectedItem.Value);
            CurrentPublication.Update();
            gvPublication.EditIndex = -1;
            gvPublicationBindData();
        }

        protected void gvPublication_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPublication.EditIndex = e.NewEditIndex;
            gvPublicationBindData();
            ((TextBox)gvPublication.Rows[e.NewEditIndex].Cells[1].Controls[0]).Width = Unit.Parse("80%");
        }

        protected void gvPublication_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPublication.EditIndex = -1;
            gvPublicationBindData();
        }

        protected void gvPublication_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            InternPublication.DeleteInternPublicationById(new Guid(gvPublication.DataKeys[e.RowIndex]["PublicationId"].ToString()));
            gvPublicationBindData();
        }
        protected void btnAddPublication_Click(object sender, EventArgs e)
        {
            this.btnAddPublication.Visible = false;
            this.panelAddNewPublication.Visible = true;
        }
        protected void tbnSummitNewPublication_Click(object sender, EventArgs e)
        {
            InternPublication ip = new InternPublication();

            ip.PAId = PerformanceAssessmentId;
            ip.ApplicantId = ApplicantId;
            ip.CurrentStatus = PAResourceManager.TextToId("PaperStatus", ddlPaperStatus.SelectedValue);
            ip.Name = tbNewPublication.Text;

            Guid PublicationId = ip.Insert();
            gvPublicationBindData();
            this.btnAddPublication.Visible = true;
            this.panelAddNewPublication.Visible = false;
        }
        protected void btnCancelAddingPublication_Click(object sender, EventArgs e)
        {
            this.btnAddPublication.Visible = true;
            this.panelAddNewPublication.Visible = false;
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] Group = new string[] { "MSRA", "STC" };
            bool[] Visible;
            switch (PAResourceManager.IdToText("Department", Convert.ToInt32(ddlDepartment.SelectedValue)))
            {
                case "STC":
                    Group = new string[] { "MSRA", "STC" };
                    Visible = new bool[] { false, true };
                    SetControlVisible(Group, Visible, this.Page);
                    break;
                default:
                    Visible = new bool[] { true, false };
                    SetControlVisible(Group, Visible, this.Page);
                    break;
            }
        }

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
    }
}