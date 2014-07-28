using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MSRA.SpringField.Application.Config.Schemas;
using System.Data;

namespace MSRA.SpringField.Application.Modules.Temp
{
    public partial class PAKeyinToolForm : System.Web.UI.Page
    {
        private SpringFieldDataContext ctx = new SpringFieldDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) 
            {
                BindGrid();
            }
        }

        public void BindGrid()
        {
            var dataSet = from applicant in ctx.sf_ApplicantBasicInfos
                          join pa in ctx.sf_PerformanceAssessments on applicant.ApplicantId.ToString() equals pa.ApplicantId.ToString()
                          where pa.OverrallEvaluation == null
                          select new { applicant.ApplicantId , applicant.FirstName, applicant.LastName, applicant.NameInChinese, 
                              pa.id,pa.GroupId, pa.InternName,pa.MentorName, pa.CheckOutDate, applicant.Status};

            this.gvApplicantList.DataSource = dataSet;
            this.gvApplicantList.DataBind();
        }

        protected void gvApplicantList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvApplicantList.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        public void btnSearch_Click(object sender, EventArgs e)
        {
            var dataSet = from applicant in ctx.sf_ApplicantBasicInfos
                          join pa in ctx.sf_PerformanceAssessments on applicant.ApplicantId.ToString() equals pa.ApplicantId.ToString()
                          where pa.OverrallEvaluation == null && 
                            (pa.InternName.Equals(this.txtInternName.Text) || pa.InternEmail.Equals(this.txtEmail.Text))
                          select new
                          {
                              applicant.ApplicantId,
                              applicant.FirstName,
                              applicant.LastName,
                              applicant.NameInChinese,
                              pa.id,
                              pa.GroupId,
                              pa.InternName,
                              pa.MentorName,
                              pa.CheckOutDate,
                              applicant.Status
                          };

            this.gvApplicantList.DataSource = dataSet;
            this.gvApplicantList.DataBind();
        }
    }
}
