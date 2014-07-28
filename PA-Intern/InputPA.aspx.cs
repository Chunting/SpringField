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

public partial class InputPA : System.Web.UI.Page
{
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlPaperStatus.DataSource = PAResourceManager.GetTypeDisplayItems("PaperStatus");
            ddlPaperStatus.DataBind();
            if (Session["Department"].ToString() != "MSRA")
            {
                this.divPublication.Visible = false;
            }
            else
            {
                gvPublicationBindData();
            }
            PerformanceAssessment pa = PerformanceAssessment.GetPerformanceAssessmentById(PerformanceAssessmentId);
            tbObjective.Text = pa.Objective;
            tbSelfEvaluation.Text = pa.SelfEvaluation;
            tbStengthsAndWeaknesses.Text = pa.StrengthsAndWeaknesses;
        }

        //gvPublicationBindData();
    }
    protected void btnSummit_Click(object sender, EventArgs e)
    {
        PerformanceAssessment pa = PerformanceAssessment.GetPerformanceAssessmentById(PerformanceAssessmentId);
        pa.Objective = tbObjective.Text;
        pa.SelfEvaluation = tbSelfEvaluation.Text;
        pa.StrengthsAndWeaknesses = tbStengthsAndWeaknesses.Text;
        bool IsUpdated = pa.Update();
        if (!IsUpdated)
        {
            Response.Write("<script>alert('Failed to Summit Performance Assessment, please input again!');</script>");
            return;
        }
        //send email 
        MailHelper mailHelper = new MailHelper();
        mailHelper.AddPerformanceAssessmentVariables(pa.Id);
        mailHelper.SendMail(MailType.PAReminder);
        Response.Write("<script>alert('Performance Assessment successfully summited!'); window.close();</script>");
    }

    #region gvPublication related functions
    protected void gvPublication_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Guid PublicationId = new Guid(gvPublication.DataKeys[e.RowIndex]["PublicationId"].ToString());
        InternPublication CurrentPublication = InternPublication.GetInternPublicationById(PublicationId);
        CurrentPublication.Name = ((TextBox)gvPublication.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
        DropDownList ddlStatus = (DropDownList)gvPublication.Rows[e.RowIndex].FindControl("ddlgvPaperStatus");
        CurrentPublication.CurrentStatus = PAResourceManager.TextToId("PaperStatus",ddlStatus.SelectedItem.Value);
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

    protected void tbnSummitNewPublication_Click(object sender, EventArgs e)
    {
        InternPublication ip = new InternPublication();

        ip.PAId = PerformanceAssessmentId;
        ip.ApplicantId = ApplicantId;
        ip.CurrentStatus = PAResourceManager.TextToId("PaperStatus", ddlPaperStatus.SelectedValue);
        ip.Name = tbNewPublication.Text;

        Guid PublicationId = ip.Insert();
        gvPublicationBindData();
    }
    #endregion
}
