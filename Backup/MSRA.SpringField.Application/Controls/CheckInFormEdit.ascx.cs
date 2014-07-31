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
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components;

namespace MSRA.SpringField.Application.Controls
{
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

            btnCheckInDay.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckInDay.ClientID + ",'mm/dd/yyyy');");
            btnLastWorkingDay.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbLastWorkingDay.ClientID + ",'mm/dd/yyyy');");
            tbCheckInDay.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbCheckInDay.ClientID + ",'mm/dd/yyyy');");
            tbLastWorkingDay.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbLastWorkingDay.ClientID + ",'mm/dd/yyyy');");
            tbCheckInDay.Attributes.Add("readonly", "true");
            tbLastWorkingDay.Attributes.Add("readonly", "true");
        }

        /*
         * Modify Interview Process
         * Author: Yin.P
         * Date: 2010-1-5
         */
        public string MentorAlias
        {
            get { return this.tbMentor.Text; }
            set { this.tbMentor.Text = value; }
        }

        protected void Repeater1_ItemDataBound(object sender,
                                                   RepeaterItemEventArgs e)
        {
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
            RadioButton rdo = (RadioButton)e.Item.FindControl("RadioButton3");
            if (rdo != null)
            {
                string script =
                   "SetUniqueRadioButton('Repeater3.*Position',this)";
                rdo.Attributes.Add("onclick", script);
            }
        }

        protected void GraduateDate_TextChanged(object sender, EventArgs e)
        {

        }
        protected void tbEnrollDate_TextChanged(object sender, EventArgs e)
        {

        }
        protected void btnCheckInDay_Click(object sender, EventArgs e)
        {

        }

        private void SelectDropDown(DropDownList ddl, string text)
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
            SetRadioButtons(Repeater1, "RadioButton1", CheckInFormResourceManager.IdToText("Groups", form.GroupId));
            SetRadioButtons(Repeater2, "RadioButton2", CheckInFormResourceManager.IdToText("Projects", form.ProjectId));
            SetRadioButtons(Repeater3, "RadioButton3", CheckInFormResourceManager.IdToText("Positions", form.PositionId));
            rdInternType1.Checked = (form.InternTypeId == 1);
            rdInternType2.Checked = (form.InternTypeId == 2);
            tbMentor.Text = form.MentorAlias;
            tbCheckInDay.Text = form.PreferCheckInDate.ToString("MM/dd/yyyy");//.ToShortDateString();
            tbLastWorkingDay.Text = form.PreferLastWorkingDay.ToString("MM/dd/yyyy");//.ToShortDateString();
            rdAdvisor1.Checked = form.AdvisorApproved;
            rdAdvisor2.Checked = !form.AdvisorApproved;
            tbComments.Text = form.Comments;
        }

        private void SetRadioButtons(Repeater Repeater1, string name, string text)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                RadioButton radioButton = item.FindControl(name) as RadioButton;
                if (radioButton.Text == text)
                {
                    radioButton.Checked = true;
                }
                else
                {
                    radioButton.Checked = false;
                }
            }
        }

        public CheckInForm GetCheckInForm()
        {
            CheckInForm form = new CheckInForm();

            //Group
            foreach (RepeaterItem item in Repeater1.Items)
            {
                RadioButton radioButton = item.FindControl("RadioButton1") as RadioButton;
                if (radioButton.Checked)
                {
                    form.GroupId = CheckInFormResourceManager.TextToId("Groups", radioButton.Text);
                }
            }

            foreach (RepeaterItem item in Repeater2.Items)
            {
                string s = item.ItemType.ToString();
                RadioButton radioButton = item.FindControl("RadioButton2") as RadioButton;
                if (radioButton.Checked)
                {
                    form.ProjectId = CheckInFormResourceManager.TextToId("Projects", radioButton.Text);
                }
            }

            //Position
            foreach (RepeaterItem item in Repeater3.Items)
            {
                string s = item.ItemType.ToString();
                RadioButton radioButton = item.FindControl("RadioButton3") as RadioButton;
                if (radioButton.Checked)
                {
                    form.PositionId = CheckInFormResourceManager.TextToId("Positions", radioButton.Text);
                }
            }

            //Intern Type
            for (int i = 1; i < 3; i++)
            {
                RadioButton radioButton = FindControl("rdInternType" + i) as RadioButton;
                if (radioButton.Checked)
                {
                    form.InternTypeId = i;
                }
            }

            //Mentor
            form.MentorAlias = tbMentor.Text.Trim();

            //Preferred check-in date
            form.PreferCheckInDate = Convert.ToDateTime(tbCheckInDay.Text);
            form.PreferLastWorkingDay = Convert.ToDateTime(tbLastWorkingDay.Text);

            form.AdvisorApproved = rdAdvisor1.Checked;
            form.Comments = tbComments.Text;
            return form;
        }

        private bool IsChoosed(Repeater Repeater1, string name)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                RadioButton radioButton = item.FindControl(name) as RadioButton;
                if (radioButton.Checked)
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
}