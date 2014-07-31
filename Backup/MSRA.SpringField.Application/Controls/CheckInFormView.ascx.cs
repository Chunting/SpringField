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
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.BizObjects;

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_CheckInFormView : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetCheckInForm(CheckInForm form)
        {
            if (form != null)
            {
                lblGroup.Text = CheckInFormResourceManager.IdToText("Groups", form.GroupId);
                lblProject.Text = CheckInFormResourceManager.IdToText("Projects", form.ProjectId);
                lblPosition.Text = CheckInFormResourceManager.IdToText("Positions", form.PositionId);
                lblInternType.Text = (form.InternTypeId == 1 ? "Full-time" : "Part-time");
                lblMentor.Text = form.MentorAlias;
                lblPreferCheckInDay.Text = form.PreferCheckInDate.ToShortDateString();
                lblPreferLastWorkingDay.Text = form.PreferLastWorkingDay.ToShortDateString();

                //removed
                //lblEnrollDate.Text = "month " + form.EnrollDate.Month + " in year " + form.EnrollDate.Year;
                //lblGraduateDate.Text = "month " + form.GraduateDate.Month + " in year " + form.GraduateDate.Year;
                //lblEnrollDate.Text = form.EnrollDate.ToShortDateString();
                //lblGraduateDate.Text = form.GraduateDate.ToShortDateString();

                lblAdvisorApproval.Text = (form.AdvisorApproved ?
                    "Yes - Please send a copy to MSRA intern support (Alias:msrainte)"
                    : "No - MSRA Intern Support Team will check with new students");
                lblComments.Text = GlobalHelper.FormatOutput(form.Comments);
            }
        }

        public void SetCheckInFormId(int id)
        {
            SetCheckInForm(CheckInForm.GetCheckInFormById(id));
        }

    }
}