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
using MSRA.SpringField.Components.Enumerations;

public partial class InputPA : System.Web.UI.Page
{
    //private bool IsSaved = false;
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
        if (ApplicantId != null && ApplicantId != Guid.Empty)
        {
            this.ApptID.Text = ApplicantId.ToString();
        }
        else
        {
            ApptID.Text = "No Applicant ID";
            return;
        }
        if (!IsPostBack)
        {
            ddlPaperStatus.DataSource = PAResourceManager.GetTypeDisplayItems("PaperStatus");
            ddlPaperStatus.DataBind();
            if (Session["Department"]!= null && Session["Department"].ToString() != "MSRA")
            {
                this.divPublication.Visible = false;
            }
            else
            {
                gvPublicationBindData();
            }
            PerformanceAssessment pa = PerformanceAssessment.GetPerformanceAssessmentById(PerformanceAssessmentId);
            if (pa.IsApproved != 5)
            {
                btnSummit.Enabled = false;
                btnSave.Enabled = false;
                tbObjective.Enabled = false;
                tbSelfEvaluation.Enabled = false;
                tbStengthsAndWeaknesses.Enabled = false;
                ddlPaperStatus.Enabled = false;
                tbNewPublication.Enabled = false;
                tbnSummitNewPublication.Enabled = false;
            }
            tbObjective.Text = pa.Objective;
            tbSelfEvaluation.Text = pa.SelfEvaluation;
            tbStengthsAndWeaknesses.Text = pa.StrengthsAndWeaknesses;
        }

        //gvPublicationBindData();
    }
    protected void btnSummit_Click(object sender, EventArgs e)
    {

        //btnSummit.Enabled = false;
        btnSummit.Text = "Pending";
        PerformanceAssessment pa = PerformanceAssessment.GetPerformanceAssessmentById(PerformanceAssessmentId);
        PerformanceAssessmentId = pa.Id;
        pa.Objective = tbObjective.Text;
        pa.SelfEvaluation = tbSelfEvaluation.Text;
        pa.StrengthsAndWeaknesses = tbStengthsAndWeaknesses.Text;
        pa.IsApproved = 4;
        bool IsUpdated = pa.Update();
        if (!IsUpdated)
        {
            Response.Write("<script>alert('Failed to Summit Performance Assessment, please input again!');</script>");
            return;
        }


        //send email ·¢ÓÊ¼þ£¡
        MailHelper mailHelper = new MailHelper();
        mailHelper.AddPerformanceAssessmentVariables(pa.Id);
        mailHelper.SendMail(MailType.PAReminder);
        Response.Write("<script>alert('Performance Assessment successfully summited!'); window.close();</script>");

        btnSummit.Text = "Submit";
        btnSummit.Enabled = false;

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = false;
        btnSave.Text = "Saving";
        PerformanceAssessment pa = PerformanceAssessment.GetPerformanceAssessmentById(PerformanceAssessmentId);
        PerformanceAssessmentId = pa.Id;
        pa.Objective = tbObjective.Text;
        pa.SelfEvaluation = tbSelfEvaluation.Text;
        pa.StrengthsAndWeaknesses = tbStengthsAndWeaknesses.Text;
        pa.IsApproved = 5;
        bool IsUpdated = pa.Update();
        if (!IsUpdated)
        {
            Response.Write("<script>alert('Failed to Save Performance Assessment, please input again!');</script>");
            lbInfo.Text = "Failed, please retry";
            return;
        }
        btnSave.Text = "Saved";
        lbInfo.Text = "PA saved, please check before submit!";
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
