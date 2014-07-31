using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MSRA.SpringField.Application.Config.Schemas;
using System.Text;
using System.IO;

namespace MSRA.SpringField.Application.Modules.Reports
{
    public partial class PaperReportForm : System.Web.UI.Page
    {
        private SpringFieldDataContext ctx = new SpringFieldDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDataByCondition(false);
            }
            else
            {

            }

            this.dtFrom.Attributes.Add("onclick",
                "popUpCalendar(this,document.all." + dtFrom.ClientID + ",'yyyy-mm-dd');");
            this.dtTo.Attributes.Add("onclick",
                "popUpCalendar(this,document.all." + dtTo.ClientID + ",'yyyy-mm-dd');");

            this.dtCheckinFrom.Attributes.Add("onclick",
                "popUpCalendar(this,document.all." + dtCheckinFrom.ClientID + ",'yyyy-mm-dd');");
            this.dtCheckinTo.Attributes.Add("onclick",
                "popUpCalendar(this,document.all." + dtCheckinTo.ClientID + ",'yyyy-mm-dd');");
        }

        private void BindDataByCondition(bool isExport)
        {
            var result = new object();

            DateTime mdtFrom = Convert.ToDateTime("1/1/1753 12:00:00 AM"), mdtTo = Convert.ToDateTime("12/31/9999 11:59:59 PM");
            DateTime mdtCheckinFrom = Convert.ToDateTime("1/1/1753 12:00:00 AM"), mdtCheckinTo = Convert.ToDateTime("12/31/9999 11:59:59 PM");


            if (this.dtFrom.Text.Length != 0) mdtFrom = Convert.ToDateTime(dtFrom.Text);
            if (this.dtTo.Text.Length != 0) mdtTo = Convert.ToDateTime(dtTo.Text);
            if (this.dtCheckinFrom.Text.Length != 0) mdtCheckinFrom = Convert.ToDateTime(dtCheckinFrom.Text);
            if (this.dtCheckinTo.Text.Length != 0) mdtCheckinTo = Convert.ToDateTime(dtCheckinTo.Text);

            if (this.ddlPaperStatus.SelectedValue != "4")
            {
                if (this.ddlPAResult.SelectedValue != "6")
                {
                    result = from list in ctx.sf_InternPublications
                             join pa in ctx.sf_PerformanceAssessments on list.PAId equals pa.id
                             join e in ctx.sf_ApplicantEduBackgrounds on list.ApplicantId equals e.ApplicantId
                             where (pa.CheckOutDate > mdtFrom && pa.CheckOutDate < mdtTo)
                             && (pa.CheckInDate > mdtCheckinFrom && pa.CheckInDate < mdtCheckinTo)
                             && pa.InternName.Contains(this.txtName.Text)
                             && list.CurrentStatus == int.Parse(this.ddlPaperStatus.SelectedValue)
                             && pa.OverrallEvaluation == int.Parse(this.ddlPAResult.SelectedValue)
                             orderby pa.CheckInDate, pa.CheckOutDate descending
                             select new
                             {
                                 pa.InternName,
                                 pa.CheckOutDate,
                                 pa.CheckInDate,
                                 list.Name,
                                 list.CurrentStatus,
                                 pa.GroupId,
                                 e.HighestEducationalInstitution,
                                 pa.MentorAlias,
                                 pa.OverrallEvaluation
                             };
                }
                else
                {
                    result = from list in ctx.sf_InternPublications
                             join pa in ctx.sf_PerformanceAssessments on list.PAId equals pa.id
                             join e in ctx.sf_ApplicantEduBackgrounds on list.ApplicantId equals e.ApplicantId
                             where (pa.CheckOutDate > mdtFrom && pa.CheckOutDate < mdtTo)
                             && (pa.CheckInDate > mdtCheckinFrom && pa.CheckInDate < mdtCheckinTo)
                             && pa.InternName.Contains(this.txtName.Text)
                             && list.CurrentStatus == int.Parse(this.ddlPaperStatus.SelectedValue)
                             orderby pa.CheckInDate, pa.CheckOutDate descending
                             select new
                             {
                                 pa.InternName,
                                 pa.CheckOutDate,
                                 pa.CheckInDate,
                                 list.Name,
                                 list.CurrentStatus,
                                 pa.GroupId,
                                 e.HighestEducationalInstitution,
                                 pa.MentorAlias,
                                 pa.OverrallEvaluation
                             };
                }
               
            }
            else
            {
                if (this.ddlPAResult.SelectedValue != "6")
                {
                    result = from list in ctx.sf_InternPublications
                             join pa in ctx.sf_PerformanceAssessments on list.PAId equals pa.id
                             join e in ctx.sf_ApplicantEduBackgrounds on list.ApplicantId equals e.ApplicantId
                             where (pa.CheckOutDate > mdtFrom && pa.CheckOutDate < mdtTo)
                             && (pa.CheckInDate > mdtCheckinFrom && pa.CheckInDate < mdtCheckinTo)
                             && pa.InternName.Contains(this.txtName.Text)
                             && pa.OverrallEvaluation == int.Parse(ddlPAResult.SelectedValue)
                             orderby pa.CheckInDate, pa.CheckOutDate descending
                             select new
                             {
                                 pa.InternName,
                                 pa.CheckOutDate,
                                 pa.CheckInDate,
                                 list.Name,
                                 list.CurrentStatus,
                                 pa.GroupId,
                                 e.HighestEducationalInstitution,
                                 pa.MentorAlias,
                                 pa.OverrallEvaluation
                             };
                }
                else
                {
                    result = from list in ctx.sf_InternPublications
                             join pa in ctx.sf_PerformanceAssessments on list.PAId equals pa.id
                             join e in ctx.sf_ApplicantEduBackgrounds on list.ApplicantId equals e.ApplicantId
                             where (pa.CheckOutDate > mdtFrom && pa.CheckOutDate < mdtTo)
                             && (pa.CheckInDate > mdtCheckinFrom && pa.CheckInDate < mdtCheckinTo)
                             && pa.InternName.Contains(this.txtName.Text)
                             orderby pa.CheckInDate, pa.CheckOutDate descending
                             select new
                             {
                                 pa.InternName,
                                 pa.CheckOutDate,
                                 pa.CheckInDate,
                                 list.Name,
                                 list.CurrentStatus,
                                 pa.GroupId,
                                 e.HighestEducationalInstitution,
                                 pa.MentorAlias,
                                 pa.OverrallEvaluation
                             };
                }
            }

            int count = ((IQueryable)result).Cast<IQueryable>().Count();
            if (count > 1)
            {
                this.lbMessage.Text = count.ToString() + " records were found.";
            }
            else if (count == 1)
            {
                this.lbMessage.Text = count.ToString() + " records was found.";
            }
            else
            {
                this.lbMessage.Text = "no record was found.";
            }
            
            this.gvPapers.DataSource = result;
            if (isExport)
            {
                BoundField fName = new BoundField();
                fName.DataField = "Name";
                fName.HeaderText = "Paper Name";
                this.gvPapers.Columns.Insert(0,fName);
                this.gvPapers.DataBind();
            }
            this.gvPapers.DataBind();
        }

        private void InitUI()
        {

        }

        protected void gvApprovingList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void gvApprovingList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPapers.PageIndex = e.NewPageIndex;
            BindDataByCondition(false);
        }

        protected void ddlApprovalStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataByCondition(false);
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            //Form.Controls.Add(gvCandidates);
            this.gvPapers.AllowPaging = false;
            //ShowAdditionalColum();
            //BindData(ddlOrderBy.SelectedValue);
           
            BindDataByCondition(true);

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=PaperReport_" + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/excel";
            Response.Charset = "utf-8";
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
            Response.ContentEncoding = Encoding.UTF8;//.GetEncoding("UTF-8");
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            
            this.gvPapers.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        protected void btnFind_Clicked(object sender, EventArgs e)
        {
            BindDataByCondition(false);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }
    }
}
