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

public partial class CheckInFormEdit : System.Web.UI.UserControl
{
    protected void Page_Init(object sender, EventArgs e)
    {
        Repeater1.DataSource = CheckInFormResourceManager.GetTypeDisplayItems("Groups");
        Repeater1.DataBind();
        Repeater2.DataSource = CheckInFormResourceManager.GetTypeDisplayItems("Projects");
        Repeater2.DataBind();
        Repeater3.DataSource = CheckInFormResourceManager.GetTypeDisplayItems("Positions");
        Repeater3.DataBind();

        
        //for (int yr = 1990; yr <= 2050; yr++)
        //{
        //    ddlEnrollYear.Items.Add(yr.ToString());
        //    ddlGraduateYear.Items.Add(yr.ToString());
        //}
        //for (int mon = 1; mon <= 12; mon++)
        //{
        //    ddlEnrollMonth.Items.Add(mon.ToString());
        //    ddlGraduateMonth.Items.Add(mon.ToString());
        //}
        btnCheckInDay.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckInDay.ClientID + ",'mm/dd/yyyy');");
        btnLastWorkingDay.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbLastWorkingDay.ClientID + ",'mm/dd/yyyy');");
        tbCheckInDay.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckInDay.ClientID + ",'mm/dd/yyyy');");
        tbLastWorkingDay.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbLastWorkingDay.ClientID + ",'mm/dd/yyyy');");
        //btnEnrollDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEnrollDate.ClientID + ",'mm/dd/yyyy');");
        //btnGraduateDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbGraduateDate.ClientID + ",'mm/dd/yyyy');");

        //tbEnrollDate.Attributes.Add("readonly", "true");
        tbCheckInDay.Attributes.Add("readonly", "true");
        tbLastWorkingDay.Attributes.Add("readonly", "true");
        //tbGraduateDate.Attributes.Add("readonly", "true");
    }
    /*
    protected ArrayList GetGroups()
    {
        ArrayList list = new ArrayList();
        list.Add("WSM");
        list.Add("System");
        list.Add("NLC");
        list.Add("Speech");
        list.Add("IG");
        list.Add("IM");
        list.Add("MCom");
        list.Add("W&N");
        list.Add("VC");
        list.Add("PDC");
        list.Add("ML");
        list.Add("UI");
        list.Add("DIT");
        list.Add("CID");
        list.Add("ID");
        list.Add("Theory");
        list.Add("IEG");
        list.Add("STC");
        list.Add("UR");
        list.Add("PR");
        return list;
    }

    protected ArrayList GetSpecialProjects()
    {
        ArrayList list = new ArrayList();
        list.Add("Hon's Project");
        list.Add("Point Source Project");
        list.Add("IG\\Separate");
        list.Add("Sigma Project");
        list.Add("UR Project");
        return list;
    }
     */
    protected void Repeater1_ItemDataBound(object sender,
                                               RepeaterItemEventArgs e)
    {
        /*if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType
           != ListItemType.AlternatingItem)
            return;
         */
        RadioButton rdo = (RadioButton)e.Item.FindControl("RadioButton1");
        if (rdo != null)
        {
            string script =
               "SetUniqueRadioButton('Repeater1.*group',this)";
            rdo.Attributes.Add("onclick", script);
        }
    }

    protected void Repeater2_ItemDataBound(object sender,
                                           RepeaterItemEventArgs e)
    {
        /*if (e.Item.ItemType != ListItemType.Item 
            && e.Item.ItemType != ListItemType.AlternatingItem)
            return;*/
        RadioButton rdo = (RadioButton)e.Item.FindControl("RadioButton2");

        if (rdo != null)
        {
            if (CheckInFormResourceManager.TextToId("Projects", rdo.Text) == 0)
            {
                rdo.Checked = true;
            }
            string script =
               "SetUniqueRadioButton('Repeater2.*SpecialProject',this)";
            rdo.Attributes.Add("onclick", script);
        }
    }

    protected void Repeater3_ItemDataBound(object sender,
                                        RepeaterItemEventArgs e)
    {
        /*if (e.Item.ItemType != ListItemType.Item 
            && e.Item.ItemType != ListItemType.AlternatingItem)
            return;*/
        RadioButton rdo = (RadioButton)e.Item.FindControl("RadioButton3");
        if (rdo != null)
        {
            string script =
               "SetUniqueRadioButton('Repeater3.*Position',this)";
            rdo.Attributes.Add("onclick", script);
        }
    }
    /*
    protected void Submit_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        
        //Group
        foreach (RepeaterItem item in Repeater1.Items)
        {
            RadioButton radioButton = item.FindControl("RadioButton1") as RadioButton;
            if (radioButton.Checked)
            {
                Label1.Text += radioButton.Text + "<br>";
            }
        }

        //Special Project
        RadioButton radioButton2 = Repeater2.Controls[0].FindControl("RadioButton2") as RadioButton;
        if (radioButton2.Checked)
        {
            Label1.Text += radioButton2.Text + "<br>";
        }

        
        foreach (RepeaterItem item in Repeater2.Items)
        {
            string s = item.ItemType.ToString();
            RadioButton radioButton = item.FindControl("RadioButton2") as RadioButton;
            if (radioButton.Checked)
            {
                Label1.Text += radioButton.Text + "<br>";
            }
        }

        //Position
        for (int i = 1; i < 8; i++)
        {
            RadioButton radioButton = FindControl("rdPosition" + i) as RadioButton;
            if (radioButton.Checked)
            {
                Label1.Text += radioButton.Text + "<br>";
            }
        }

        //Intern Type
        for (int i = 1; i < 3; i++)
        {
            RadioButton radioButton = FindControl("rdInternType" + i) as RadioButton;
            if (radioButton.Checked)
            {
                Label1.Text += radioButton.Text + "<br>";
            }
        }

        //Mentor
        Label1.Text += tbMentor.Text + "<br>";

        //Preferred check-in date
        Label1.Text += tbCheckInDay.Text + "<br>";

        //Preferred last working day
        Label1.Text += tbLastWorkingDay.Text + "<br>";

        //Approval letter
        for (int i = 1; i < 3; i++)
        {
            RadioButton radioButton = FindControl("rdAdvisor" + i) as RadioButton;
            if (radioButton.Checked)
            {
                Label1.Text += radioButton.Text + "<br>";
            }
        }

        //Enroll/graduation date
        Label1.Text += tbEnrollDate.Text + "<br>";
        Label1.Text += tbGraduateDate.Text + "<br>";

        //Comments
        Label1.Text += tbComments.Text + "<br>";
    }
    */
    protected void GraduateDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void tbEnrollDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnCheckInDay_Click(object sender, EventArgs e)
    {

    }

    private void SelectDropDown( DropDownList ddl, string text)
    {
        for (int idx = 0; idx < ddl.Items.Count; idx++)
        {
            if (ddl.Items[idx].Text == text)
            {
                ddl.SelectedIndex = idx;
            }
        }
    }
    public void SetCheckInForm(CheckInForm form)
    {
        SetRadioButtons(Repeater1, "RadioButton1", CheckInFormResourceManager.IdToText("Groups",form.GroupId));
        SetRadioButtons(Repeater2, "RadioButton2", CheckInFormResourceManager.IdToText("Projects", form.ProjectId));
        SetRadioButtons(Repeater3, "RadioButton3", CheckInFormResourceManager.IdToText("Positions", form.PositionId));
        rdInternType1.Checked = (form.InternTypeId == 1);
        rdInternType2.Checked = (form.InternTypeId == 2);
        tbMentor.Text = form.MentorAlias;
        tbCheckInDay.Text = form.PreferCheckInDate.ToString("MM/dd/yyyy");//.ToShortDateString();
        tbLastWorkingDay.Text = form.PreferLastWorkingDay.ToString("MM/dd/yyyy");//.ToShortDateString();
        rdAdvisor1.Checked = form.AdvisorApproved;
        rdAdvisor2.Checked = !form.AdvisorApproved;
        
        //removed
        //SelectDropDown(ddlEnrollMonth, form.EnrollDate.Month.ToString());
        //SelectDropDown(ddlEnrollYear, form.EnrollDate.Year.ToString());
        //SelectDropDown(ddlGraduateMonth, form.GraduateDate.Month.ToString());
        //SelectDropDown(ddlGraduateYear, form.GraduateDate.Year.ToString());

        //tbEnrollDate.Text = form.EnrollDate.ToShortDateString();
        //tbGraduateDate.Text = form.GraduateDate.ToShortDateString();
        tbComments.Text = form.Comments;
    }

    private void SetRadioButtons(Repeater Repeater1,string name, string text)
    {
        foreach (RepeaterItem item in Repeater1.Items)
        {
            RadioButton radioButton = item.FindControl(name) as RadioButton;
            if (radioButton.Text == text)
            {
                radioButton.Checked = true;
            }else
            {
                radioButton.Checked = false;
            }
        }
    }

    public CheckInForm GetCheckInForm()
    {
        CheckInForm form = new CheckInForm();
        //Label1.Text = "";

        //Group
        foreach (RepeaterItem item in Repeater1.Items)
        {
            RadioButton radioButton = item.FindControl("RadioButton1") as RadioButton;
            if (radioButton.Checked)
            {
                //Label1.Text += radioButton.Text + "<br>";
                form.GroupId = CheckInFormResourceManager.TextToId("Groups",radioButton.Text);
            }
        }

        //Special Project
        /*RadioButton radioButton2 = Repeater2.Controls[0].FindControl("RadioButton2") as RadioButton;
        if (radioButton2.Checked)
        {
            Label1.Text += radioButton2.Text + "<br>";
        }*/


        foreach (RepeaterItem item in Repeater2.Items)
        {
            string s = item.ItemType.ToString();
            RadioButton radioButton = item.FindControl("RadioButton2") as RadioButton;
            if (radioButton.Checked)
            {
                //Label1.Text += radioButton.Text + "<br>";
                form.ProjectId = CheckInFormResourceManager.TextToId("Projects",radioButton.Text);
            }
        }

        //Position
        foreach (RepeaterItem item in Repeater3.Items)
        {
            string s = item.ItemType.ToString();
            RadioButton radioButton = item.FindControl("RadioButton3") as RadioButton;
            if (radioButton.Checked)
            {
                //Label1.Text += radioButton.Text + "<br>";
                form.PositionId = CheckInFormResourceManager.TextToId("Positions", radioButton.Text);
            }
        }

        //Intern Type
        for (int i = 1; i < 3; i++)
        {
            RadioButton radioButton = FindControl("rdInternType" + i) as RadioButton;
            if (radioButton.Checked)
            {
                //Label1.Text += radioButton.Text + "<br>";
                form.InternTypeId = i;
            }
        }

        //Mentor
        //Label1.Text += tbMentor.Text + "<br>";
        form.MentorAlias = tbMentor.Text.Trim();

        //Preferred check-in date
        //Label1.Text += tbCheckInDay.Text + "<br>";
        form.PreferCheckInDate = Convert.ToDateTime(tbCheckInDay.Text);

        //Preferred last working day
        //Label1.Text += tbLastWorkingDay.Text + "<br>";
        form.PreferLastWorkingDay = Convert.ToDateTime(tbLastWorkingDay.Text);

        //Approval letter
        //for (int i = 1; i < 3; i++)
        //{
        //    RadioButton radioButton = FindControl("rdAdvisor" + i) as RadioButton;
        //    if (radioButton.Checked)
        //    {
        //        //Label1.Text += radioButton.Text + "<br>";
        //        form.AdvisorApproval = i;
        //    }
        //}
        form.AdvisorApproved = rdAdvisor1.Checked;

        //Enroll/graduation date
        //Label1.Text += tbEnrollDate.Text + "<br>";
        //Label1.Text += tbGraduateDate.Text + "<br>";
        //form.EnrollDate = Convert.ToDateTime(tbEnrollDate.Text);
        //form.GraduateDate = Convert.ToDateTime(tbGraduateDate.Text);

        //form.EnrollDate = new DateTime(Convert.ToInt32(ddlEnrollYear.SelectedItem.Text), Convert.ToInt32(ddlEnrollMonth.SelectedItem.Text),1);
        //form.GraduateDate = new DateTime(Convert.ToInt32(ddlGraduateYear.SelectedItem.Text), Convert.ToInt32(ddlGraduateMonth.SelectedItem.Text),1);

        //Comments
        //Label1.Text += tbComments.Text + "<br>";
        form.Comments = tbComments.Text;
        return form;
    }

    private bool IsChoosed(Repeater Repeater1, string name)
    {
        foreach (RepeaterItem item in Repeater1.Items)
        {
            RadioButton radioButton = item.FindControl(name) as RadioButton;
            if (radioButton.Checked )
            {
                return true;
            }
        }
        return false;
    }
    protected void ServerValidateGroups(object source, ServerValidateEventArgs args)
    {
        args.IsValid = IsChoosed(Repeater1, "RadioButton1");
    }
    protected void ServerValidatePositions(object source, ServerValidateEventArgs args)
    {
        args.IsValid = IsChoosed(Repeater3, "RadioButton3");
    }
    protected void ServerValidateProjects(object source, ServerValidateEventArgs args)
    {
        args.IsValid = IsChoosed(Repeater2, "RadioButton2");
    }

    protected void ServerValidateInternType(object source, ServerValidateEventArgs args)
    {
        args.IsValid = rdInternType1.Checked || rdInternType2.Checked;
    }

    protected void ServerValidateAdvisor(object source, ServerValidateEventArgs args)
    {
        args.IsValid = rdAdvisor1.Checked || rdAdvisor2.Checked;
    }

    protected void CustomValidator5_ServerValidate(object source, ServerValidateEventArgs args)
    {
        int c = 1;
        int d = 2;
        d += c;
        args.IsValid = false;
    }
}
