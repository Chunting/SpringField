/*************************************************************************************
 * Created by Yi Shao at 2009-06-14
 * Abstract: list all hired candidates in a gridview, user can change those candidats'
 * status to "On Board", "Offer Decline" or "Rejected" via this page. this page also 
 * contain search feature.
 * **********************************************************************************/

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
using Springfield.Components.Configuration;

public partial class HiredCandidates : System.Web.UI.Page
{
    public string FilterExpression
    {
        get
        {
            if (ViewState["filter"] != null)
            {
                return ViewState["filter"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        set { ViewState["filter"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            ddlGroup.DataSource = CheckInFormResourceManager.GetTypeDataSet("Groups").Tables[0];
            ddlGroup.DataValueField = "id";
            ddlGroup.DataTextField = "name";
            ddlGroup.DataBind();
            ListItem AllItem = new ListItem("All", "0");
            ddlGroup.Items.Insert(0, AllItem);           
        }
    }
    private void BindData()
    {
        DataView dv = new DataView(Applicant.GetHiredApplicants().Tables[0]);

        //Filter
        if (this.FilterExpression != string.Empty)
        {
            dv.RowFilter = this.FilterExpression;
        }

        gvHiredCandidates.DataSource = dv;
        gvHiredCandidates.DataBind();
        lbCount.Text = dv.Count.ToString();
    }
    
    #region transfer data for gridview
    protected string GetApplicantLink(string ID)
    {        
        return "~/ShowApplication.aspx?applicant=" + ID;
    }
    protected string GetApprovalEmailLink(string GMApprovalDocId, string MentorApprovalDocId)
    {
        Document docEmailApproval = new Document();
        SiteConfiguration config = SiteConfiguration.GetConfig();
        string docPath = config.SiteAttributes["docUrl"];
        string EmailApprovallink = "";
        if (GMApprovalDocId != "" && GMApprovalDocId != "0" && !String.IsNullOrEmpty(GMApprovalDocId))
        {
            docEmailApproval = Document.GetDocumentById(Convert.ToInt32(GMApprovalDocId));
            if(docEmailApproval.SaveName != "" && !String.IsNullOrEmpty(docEmailApproval.SaveName))
                EmailApprovallink = string.Format("<a href='{0}'>Download</a>", docPath + docEmailApproval.SaveName);
            return EmailApprovallink;
        }
        if (MentorApprovalDocId != "" && MentorApprovalDocId != "0" && !String.IsNullOrEmpty(MentorApprovalDocId))
        {
            docEmailApproval = Document.GetDocumentById(Convert.ToInt32(MentorApprovalDocId));
            if (docEmailApproval.SaveName != "" && !String.IsNullOrEmpty(docEmailApproval.SaveName))
                EmailApprovallink = string.Format("<a href='{0}'>Download</a>", docPath + docEmailApproval.SaveName);
            return EmailApprovallink;
        }

        return EmailApprovallink;
    }
    protected string GetGroupNameByID(string ID)
    {
        string Group = "";
        try
        {
            Group = CheckInFormResourceManager.IdToText("Groups", Convert.ToInt32(ID));
        }
        catch
        {
        }

        return Group;
    }
    protected string GetGMAliasByID(string ID)
    {
        string GMAlias = "";
        try
        {
            Guid id = new Guid(ID);
            GMAlias = SiteUser.GetAliasByUserId(id);
        }
        catch
        {
        }

        return GMAlias;
    }
    protected string GetDegree(string ID)
    {
        string Degree = "";
        try
        {
            Guid id = new Guid(ID);
            ApplicantEduBackground aeb = ApplicantEduBackground.GetApplicantEduBackgroundById(id);
            Degree = StaticData.DegreeList[(int)aeb.Degree] + aeb.YearOfStudy.ToString();
        }
        catch
        {
        }

        return Degree;
    }
    #endregion

    #region event handdler
    protected void btnOnBoardSelected_Click(object sender, EventArgs e)
    {
        if (Request.Form["cb_ischeck"] != null)
        {
            string[] idArr = Request.Form["cb_ischeck"].ToString().Split(',');
            ApplicantBasicInfo abi;
            Guid curId;
            foreach (string idStr in idArr)
            {
                curId = new Guid(idStr.Trim());
                abi = ApplicantBasicInfo.GetApplicantBasicInfoById(curId);
                abi.Status = ApplicationStatusEnum.OnBoard;
                abi.Update();
            }
            BindData();
            JSUtility.Alert(this.Page, "All selected candidates' status have been changed to \"On Board\"!");            
        }
        else
        {
            JSUtility.Alert(this.Page, "None of candidates has been selected!");
        }
    }
    protected void btnDeclineSelected_Click(object sender, EventArgs e)
    {
        if (Request.Form["cb_ischeck"] != null)
        {
            string[] idArr = Request.Form["cb_ischeck"].ToString().Split(',');
            ApplicantBasicInfo abi;
            Guid curId;
            Interview internview;
            string strInternviewId;
            foreach (string idStr in idArr)
            {
                curId = new Guid(idStr.Trim());
                abi = ApplicantBasicInfo.GetApplicantBasicInfoById(curId);
                abi.Status = ApplicationStatusEnum.OfferDeclined;
                abi.Update();
                strInternviewId = Interview.GetInterviewIdByApplicant(curId);
                internview = Interview.GetInterviewById(Convert.ToInt32(strInternviewId));
                internview.InterviewStatus = InterviewStatusEnum.OfferDeclined;
                internview.Update();
            }
            BindData();
            JSUtility.Alert(this.Page, "All selected candidates' status have been changed to \"Offer Decline\"!");
        }
        else
        {
            JSUtility.Alert(this.Page, "None of candidates has been selected!");
        }
    }
    protected void btnRejectedSelected_Click(object sender, EventArgs e)
    {
        if (Request.Form["cb_ischeck"] != null)
        {
            string[] idArr = Request.Form["cb_ischeck"].ToString().Split(',');
            ApplicantBasicInfo abi;
            Guid curId;
            Interview internview;
            string strInternviewId;
            foreach (string idStr in idArr)
            {
                curId = new Guid(idStr.Trim());
                abi = ApplicantBasicInfo.GetApplicantBasicInfoById(curId);
                abi.Status = ApplicationStatusEnum.Rejected;
                abi.Update();
                strInternviewId = Interview.GetInterviewIdByApplicant(curId);
                internview = Interview.GetInterviewById(Convert.ToInt32(strInternviewId));
                internview.InterviewStatus = InterviewStatusEnum.Rejected;
                internview.Update();
            }
            BindData();
            JSUtility.Alert(this.Page, "All selected candidates' status have been changed to \"Rejected\"!");
        }
        else
        {
            JSUtility.Alert(this.Page, "None of candidates has been selected!");
        }
    }
    protected void gvHiredCandidates_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ApplicantBasicInfo abi;
        Interview internview;
        Guid id;
        string strInternviewId;
        switch (e.CommandName)
        {
            case "OnBoard":
                id = new Guid(e.CommandArgument.ToString());
                abi = ApplicantBasicInfo.GetApplicantBasicInfoById(id);
                abi.Status = ApplicationStatusEnum.OnBoard;
                abi.Update();
                BindData();
                break;
            case "Decline":
                id = new Guid(e.CommandArgument.ToString());
                abi = ApplicantBasicInfo.GetApplicantBasicInfoById(id);
                abi.Status = ApplicationStatusEnum.OfferDeclined;
                abi.Update();
                strInternviewId = Interview.GetInterviewIdByApplicant(id);
                internview = Interview.GetInterviewById(Convert.ToInt32(strInternviewId));
                internview.InterviewStatus = InterviewStatusEnum.OfferDeclined;
                internview.Update();
                BindData();
                break;
            case "Reject":
                id = new Guid(e.CommandArgument.ToString());
                abi = ApplicantBasicInfo.GetApplicantBasicInfoById(id);
                abi.Status = ApplicationStatusEnum.Rejected;
                abi.Update();
                strInternviewId = Interview.GetInterviewIdByApplicant(id);
                internview = Interview.GetInterviewById(Convert.ToInt32(strInternviewId));
                internview.InterviewStatus = InterviewStatusEnum.Rejected;
                internview.Update();
                BindData();
                break;
        }
    }
    protected void gvHiredCandidates_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHiredCandidates.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strFilter = GetFilter();
        FilterExpression = strFilter;
        BindData();
    }
    #endregion

    #region private method
    private string GetFilter()
    {
        string strFilter = String.Empty;
        if (!isNullOrEmpty(tbCandidateName.Text.Trim()))
        {
            strFilter += "(FullName like '%";
            strFilter += tbCandidateName.Text.Trim();
            strFilter += "%') AND ";
        }
        if (!isNullOrEmpty(tbGMAlias.Text.Trim()))
        {
            strFilter += "(GroupManagerId = '";
            strFilter += SiteUser.GetUserIdByAlias(tbGMAlias.Text.Trim());
            strFilter += "') AND ";
        }
        if (!isNullOrEmpty(tbMentorAlias.Text.Trim()))
        {
            strFilter += "(MentorAlias = '";
            strFilter += tbMentorAlias.Text.Trim();
            strFilter += "') AND ";
        }
        if (ddlGroup.SelectedIndex > 0)
        {
            strFilter += "(GroupId = ";
            strFilter += ddlGroup.SelectedValue.ToString();
            strFilter += ") AND ";
        } 
        if (strFilter.Length > 4)
            strFilter = strFilter.Substring(0, strFilter.Length - 4);
        return strFilter;
    }
    private bool isNullOrEmpty(string str)
    {
        if (str != "" && !String.IsNullOrEmpty(str))
            return false;
        else
            return true;
    }
    #endregion
}
