using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Springfield.Components;
using System.Text.RegularExpressions;

public partial class _Default : System.Web.UI.Page 
{
    public Guid ApplicantId
    {
        get
        {
            if (Session["ApplicantId"] != null)
            {
                Guid Id =  new Guid(Session["ApplicantId"].ToString());
                return Id;
            }
            else
            {
                //Guid TestApplicant = new Guid("2d83ed78-444a-4041-ae7b-c0d03bbb986d");// for Test 9b35c9cb-048e-4b07-8663-8900fc037dd5
                //return TestApplicant;
                return Guid.Empty;
            }
        }
        set
        {
            Session["ApplicantId"] = value;
        }
    }
    public string Mode
    {
        get
        {
            if (Session["Mode"] != null)
                return Session["Mode"].ToString();
            else
                return "Unkown";
        }
        set
        {
            Session["Mode"] = value;
        }
    }
    public string Department
    {
        get
        {
            if (Session["Department"] != null)
                return Session["Department"].ToString();
            else
                return String.Empty;
        }
        set
        {
            Session["Department"] = value;
        }
    }
    public Guid PerformanceAssessmentId
    {
        get
        {
            if (Session["PerformanceAssessmentId"] != null)
            {
                Guid Id = new Guid(Session["PerformanceAssessmentId"].ToString());
                return Id;
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
            this.tbCheckinDate.Attributes.Add("readonly", "true");
            this.btnCheckoutDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckoutDate.ClientID + ",'mm/dd/yyyy');");
            this.tbCheckoutDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckoutDate.ClientID + ",'mm/dd/yyyy');");
            this.tbCheckoutDate.Attributes.Add("readonly", "true");
            this.btnGraduationDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbGraduationDate.ClientID + ",'mm/dd/yyyy');");
            this.tbGraduationDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbGraduationDate.ClientID + ",'mm/dd/yyyy');");
            this.tbGraduationDate.Attributes.Add("readonly", "true");

            FillDropdownList();
            BindUncompletedPAList();
            
            if (gvUncompletedPA.Rows.Count == 0)
            {
                FilApplicantBasicInfo();
                divPADetail.Visible = true;
                divUncompletedPAlist.Visible = false;

                String[] Group = new string[] { "MSRA", "STC" };
                bool[] Visible = new bool[] { true, false };
                SetControlVisible(Group, Visible, this.Page);
                Department = "MSRA";
            }
            

        }
    }

    #region Uncompleted PA gridview related functions
    protected void BindUncompletedPAList()
    {
        DataSet dsUncompletedPA = PerformanceAssessment.GetUncompletedPAbyApplicantId(ApplicantId, DateTime.Now.AddMonths(-1));
        gvUncompletedPA.DataSource = dsUncompletedPA.Tables[0].DefaultView;
        gvUncompletedPA.DataBind();
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
    protected string ParseCheckinDate(string CheckinDate)
    {
        try
        {
            DateTime dtCheckinDate = Convert.ToDateTime(CheckinDate);
            if (dtCheckinDate != Convert.ToDateTime("9999-12-29 0:00:00"))
                return dtCheckinDate.ToString("MM/dd/yyyy");
            else
                return "N/A";
        }
        catch
        {
            return "N/A";
        }
    }
    protected string ParseCheckoutDate(string CheckoutDate)
    {
        try
        {
            DateTime dtCheckoutDate = Convert.ToDateTime(CheckoutDate);
            if (dtCheckoutDate != Convert.ToDateTime("9999-12-30 0:00:00"))
                return dtCheckoutDate.ToString("MM/dd/yyyy");
            else
                return "N/A";
        }
        catch
        {
            return "N/A";
        }
    }
    protected void gvUncompletedPA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "completePA":
                PerformanceAssessmentId = new Guid(e.CommandArgument.ToString());
                //btnCancel.Visible = false;
                Mode = "Edit";
                btnAddPA.Visible = true;
                FillPAInfo();
                break;
            case "deletePA":
                Guid DeletePAId = new Guid(e.CommandArgument.ToString());
                if (DeletePAId == PerformanceAssessmentId)
                {
                    JSUtility.Alert(this.Page, "You are editing this performance assessment, so you can not delete it!");
                    return;
                }
                bool isSuccess = PerformanceAssessment.DeletePerformanceAssessmentbyId(DeletePAId);
                if (isSuccess)
                    JSUtility.Alert(this.Page, "Successful Deleted!");
                else
                    JSUtility.Alert(this.Page, "Failed to delete!");
                BindUncompletedPAList();
                break;
        }
    }
    protected void btnAddPA_Click(object sender, EventArgs e)
    {
        Mode = "Insert";
        FilApplicantBasicInfo();
        divPADetail.Visible = true;
        btnAddPA.Visible = false;
        btnCancel.Visible = true;

        String[] Group = new string[] { "MSRA", "STC" };
        bool[] Visible = new bool[] { true, false };
        SetControlVisible(Group, Visible, this.Page);
        Department = "MSRA";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //PerformanceAssessment.DeletePerformanceAssessmentbyId(PerformanceAssessmentId);
        divPADetail.Visible = false;
        btnAddPA.Visible = true;
        PerformanceAssessmentId = Guid.Empty;
        Mode = "Unknown";
        //BindUncompletedPAList();
    }
    #endregion

    private void FillDropdownList()
    {
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
    private void FillPAInfo()
    {
        PerformanceAssessment CurrentPA = PerformanceAssessment.GetPerformanceAssessmentById(PerformanceAssessmentId);

        divPADetail.Visible = true;

        lbName.Text = CurrentPA.InternName.Trim();
        tbPhone.Text = CurrentPA.InternPhone.Trim();
        tbEmail.Text = CurrentPA.InternEmail.Trim();
        if(CurrentPA.GroupId > 0)
            ddlGroup.SelectedValue = CurrentPA.GroupId.ToString();
        ddlProject.SelectedValue = CurrentPA.ProjectId.ToString();
        if(CurrentPA.Discipline > 0)
            ddlDiscipline_STC.SelectedValue = CurrentPA.Discipline.ToString();
        if (!String.IsNullOrEmpty(CurrentPA.Pipeline.Trim()))
            ddlPipeline_STC.SelectedValue = CurrentPA.Pipeline.Trim();
        ddlInternPosition.SelectedValue = CurrentPA.InternPosition.ToString();

        if (CurrentPA.CheckInDate != Convert.ToDateTime("9999-12-29 0:00:00"))
            tbCheckinDate.Text = CurrentPA.CheckInDate.ToString("MM/dd/yyyy");
        if (CurrentPA.CheckOutDate != Convert.ToDateTime("9999-12-30 0:00:00"))
            tbCheckoutDate.Text = CurrentPA.CheckOutDate.ToString("MM/dd/yyyy");
        if (CurrentPA.GraduationDate != Convert.ToDateTime("9999-12-30 0:00:00"))
            tbGraduationDate.Text = CurrentPA.GraduationDate.ToString("MM/dd/yyyy");
        tbMentorAlias.Text = CurrentPA.MentorAlias.Trim();
        tbMentorName.Text = CurrentPA.MentorName.Trim();
        if (CurrentPA.GroupMgrId != Guid.Empty)
            tbGroupManagerAlias.Text = SiteUser.GetAliasByUserId(CurrentPA.GroupMgrId);
        if(CurrentPA.Department > 0)
            ddlDepartment.SelectedValue = CurrentPA.Department.ToString();
        if (ddlDepartment.SelectedItem.Text.Trim() == "STC")
        {
            String[] Group = new string[] { "STC", "MSRA" };
            bool[] Visible = new bool[] { true, false };
            SetControlVisible(Group, Visible, this.Page);
            Department = "STC";
        }
        else
        {
            String[] Group = new string[] { "MSRA", "STC" };
            bool[] Visible = new bool[] { true, false };
            SetControlVisible(Group, Visible, this.Page);
            Department = "MSRA";
        }
    }
    private void FilApplicantBasicInfo()
    {
        if (ApplicantId == Guid.Empty || ApplicantId == null)
            return;
        DataSet dsPABasicInfo = PerformanceAssessment.GetPABasicInfoByApplicantId(ApplicantId);
        DataTable tb = dsPABasicInfo.Tables[0];

        lbName.Text = tb.Rows[0]["InternName"].ToString();
        tbPhone.Text = tb.Rows[0]["InternPhone"].ToString();
        tbEmail.Text = tb.Rows[0]["InternEmail"].ToString();
        //tbGroup.Text = CheckInFormResourceManager.IdToText("Groups", Convert.ToInt32(tb.Rows[0]["GroupId"]));
        if(!String.IsNullOrEmpty(tb.Rows[0]["GroupId"].ToString()))
            ddlGroup.SelectedValue = tb.Rows[0]["GroupId"].ToString();
        if (!String.IsNullOrEmpty(tb.Rows[0]["ProjectId"].ToString()))
            ddlProject.SelectedValue = tb.Rows[0]["ProjectId"].ToString();
        if (!String.IsNullOrEmpty(tb.Rows[0]["Discipline"].ToString()))
            ddlDiscipline_STC.SelectedValue = tb.Rows[0]["Discipline"].ToString();
        if (!String.IsNullOrEmpty(tb.Rows[0]["InternPosition"].ToString()))
            ddlInternPosition.SelectedValue = tb.Rows[0]["InternPosition"].ToString();

        if (!String.IsNullOrEmpty(tb.Rows[0]["CheckInDate"].ToString())) 
            tbCheckinDate.Text = Convert.ToDateTime(tb.Rows[0]["CheckInDate"]).ToString("MM/dd/yyyy");
        if (!String.IsNullOrEmpty(tb.Rows[0]["CheckOutDate"].ToString())) 
            tbCheckoutDate.Text = Convert.ToDateTime(tb.Rows[0]["CheckOutDate"]).ToString("MM/dd/yyyy");
        if (!String.IsNullOrEmpty(tb.Rows[0]["GraduatedDate"].ToString()))
            tbGraduationDate.Text = Convert.ToDateTime(tb.Rows[0]["GraduatedDate"]).ToString("MM/dd/yyyy");  
        tbMentorAlias.Text = tb.Rows[0]["MentorAlias"].ToString();
        if (!String.IsNullOrEmpty(tb.Rows[0]["GroupMgrId"].ToString()))
        {
            Guid MgrId = new Guid(tb.Rows[0]["GroupMgrId"].ToString());
            if(MgrId != Guid.Empty)
                tbGroupManagerAlias.Text = SiteUser.GetAliasByUserId(MgrId);
        }

        //PerformanceAssessment pa = new PerformanceAssessment();
        //if (!String.IsNullOrEmpty(tb.Rows[0]["GroupId"].ToString()))
        //    pa.GroupId = Convert.ToInt32(tb.Rows[0]["GroupId"]);
        //if (!String.IsNullOrEmpty(tb.Rows[0]["Discipline"].ToString()))
        //    pa.Discipline = Convert.ToInt32(tb.Rows[0]["Discipline"]);
        //if (!String.IsNullOrEmpty(tb.Rows[0]["ProjectId"].ToString()))
        //    pa.ProjectId = Convert.ToInt32(tb.Rows[0]["ProjectId"]);
        //if (!String.IsNullOrEmpty(tb.Rows[0]["InternPosition"].ToString())) 
        //    pa.InternPosition = Convert.ToInt32(tb.Rows[0]["InternPosition"]);
        //pa.MentorAlias = tb.Rows[0]["MentorAlias"].ToString();
        //if (!String.IsNullOrEmpty(tb.Rows[0]["CheckInDate"].ToString()))
        //    pa.CheckInDate = Convert.ToDateTime(tb.Rows[0]["CheckInDate"]);
        //if (!String.IsNullOrEmpty(tb.Rows[0]["CheckOutDate"].ToString()))
        //    pa.CheckOutDate = Convert.ToDateTime(tb.Rows[0]["CheckOutDate"]);
        //if (!String.IsNullOrEmpty(tb.Rows[0]["GraduatedDate"].ToString()))
        //    pa.GraduationDate = Convert.ToDateTime(tb.Rows[0]["GraduatedDate"]);      
        //pa.InternName = tb.Rows[0]["InternName"].ToString();
        //pa.InternPhone = tb.Rows[0]["InternPhone"].ToString();
        //pa.InternEmail = tb.Rows[0]["InternEmail"].ToString();
        //if (!String.IsNullOrEmpty(tb.Rows[0]["GroupMgrId"].ToString()))
        //    pa.GroupMgrId = (Guid)tb.Rows[0]["GroupMgrId"];
        //pa.ApplicantId = ApplicantId;
        //PerformanceAssessmentId = pa.Insert();
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (!StaticData.FTEDict.ContainsKey(tbMentorAlias.Text.ToLower().Trim()))
        {
            lbAliasNotice.Text = "This Alias isn't correct, please Input again!";
            return;
        }
        if (!StaticData.FTEDict.ContainsKey(tbGroupManagerAlias.Text.ToLower().Trim()))
        {
            lbGroupManagerAlias.Text = "This Alias isn't correct, please Input again!";
            return;
        }

        PerformanceAssessment pa;
        if (Mode == "Edit")
        {
            pa = PerformanceAssessment.GetPerformanceAssessmentById(PerformanceAssessmentId);
        }
        else //insert
        {
            pa = new PerformanceAssessment();
        }
        pa.ApplicantId = ApplicantId;
        pa.InternName = lbName.Text.Trim();
        //pa.InternChineseName = lbName.Text.Trim();
        pa.InternPhone = tbPhone.Text.Trim();
        pa.InternEmail = tbEmail.Text.Trim();
        pa.GroupId = Convert.ToInt32(ddlGroup.SelectedValue);
        pa.Department = Convert.ToInt32(ddlDepartment.SelectedValue);
        pa.ProjectId = Convert.ToInt32(ddlProject.SelectedValue);
        pa.GroupMgrId = new Guid(SiteUser.GetUserIdByAlias(tbGroupManagerAlias.Text.ToLower().Trim()));
        pa.InternPosition = Convert.ToInt32(ddlInternPosition.SelectedValue);
        pa.CheckInDate = Convert.ToDateTime(tbCheckinDate.Text.Trim());
        pa.CheckOutDate = Convert.ToDateTime(tbCheckoutDate.Text.Trim());
        pa.MentorAlias = tbMentorAlias.Text.ToLower().Trim();
        pa.MentorName = tbMentorName.Text.Trim();
        pa.GraduationDate = Convert.ToDateTime(tbGraduationDate.Text.Trim());
        if (ddlDepartment.SelectedValue == PAResourceManager.TextToId("Department","STC").ToString())
        {
            pa.Pipeline = ddlPipeline_STC.SelectedValue;
            pa.Discipline = Convert.ToInt32(ddlDiscipline_STC.SelectedValue);
        }
        if (Mode == "Edit")
        {
            pa.Update();
        }
        else //insert
        {
            PerformanceAssessmentId = pa.Insert();
        }
        
        Response.Redirect("InputPA.aspx");
    }

    protected void SetControlVisible(string[] Group, bool[] Visible,Control control)
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
            Regex regex = new Regex(@"[\w\d_]+_" + Group[i],RegexOptions.IgnoreCase);
            if(!String.IsNullOrEmpty(control.ID) && regex.IsMatch(control.ID))
                control.Visible = Visible[i];
        }

        return;
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        String[] Group = new string[] { "MSRA", "STC" };
        bool[] Visible;
        switch(PAResourceManager.IdToText("Department",Convert.ToInt32(ddlDepartment.SelectedValue)))
        {
            case "MSRA":
                Visible = new bool[] { true, false };
                SetControlVisible(Group, Visible, this.Page);
                Department = "MSRA";
                break;
            case "STC":
                Group = new string[] { "MSRA", "STC" };
                Visible = new bool[] { false, true };
                SetControlVisible(Group, Visible, this.Page);
                Department = "STC";
                break;
        }
        
    }
}
