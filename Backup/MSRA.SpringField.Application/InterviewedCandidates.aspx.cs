/*****************************************************************************
 * Created by Yi Shao at 2009-06-10
 * Abstract:candidates list which contain students whose interview status is "waitting for feedback", 
 *          "waiting for mentor decision" or "waiting for Group Manager decision". this page also 
 *          support searching in the list
*****************************************************************************/
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
using MSRA.SpringField.Components.Enumerations;

namespace MSRA.SpringField.Application
{
    public partial class InterviewedCandidates : System.Web.UI.Page
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
                ddlGroup.DataSource = CheckInFormResourceManager.GetTypeDataSet("Groups").Tables[0].Select("display=true").CopyToDataTable();
                ddlGroup.DataValueField = "id";
                ddlGroup.DataTextField = "name";
                ddlGroup.DataBind();
                ListItem AllItem = new ListItem("All", "0");
                ddlGroup.Items.Insert(0, AllItem);
            }
        }

        protected void gvInternviewedCandidates_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInternviewedCandidates.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strFilter = GetFilter();
            FilterExpression = strFilter;
            BindData();
        }

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

        private void BindData()
        {
            DataView dv;
            switch (ddlStatus.SelectedValue)
            {
                case "FM":
                    DataTable tableWaitingForInterviewFeedback = Applicant.GetApplicantsByStatus(InterviewStatusEnum.WaitingForInterviewFeedback).Tables[0];
                    DataTable tableWaitingForMentorDecision = Applicant.GetApplicantsByStatus(InterviewStatusEnum.WaitingForMentorDecision).Tables[0];
                    tableWaitingForMentorDecision.Merge(tableWaitingForInterviewFeedback);
                    dv = new DataView(tableWaitingForInterviewFeedback);
                    break;
                case "GMA":
                    dv = new DataView(Applicant.GetApplicantsByStatus(InterviewStatusEnum.WaitingForGroupManagerDecision).Tables[0]);
                    break;
                default:
                    dv = new DataView(Applicant.GetApplicantsByStatus().Tables[0]);
                    break;
            }


            //Filter
            if (this.FilterExpression != string.Empty)
            {
                dv.RowFilter = this.FilterExpression;
            }

            gvInternviewedCandidates.DataSource = dv;
            gvInternviewedCandidates.DataBind();
            lbCount.Text = dv.Count.ToString();
        }

        protected string GetApplicantLink(string ID)
        {
            return "~/ShowApplication.aspx?applicant=" + ID;
        }
        protected string GetUploadLink(string ID)
        {
            return "~/ShowApplication.aspx?applicant=" + ID + "&tab=1";
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

        protected string GetStatus(string intStatus)
        {
            string strStatus = "";
            try
            {
                int status = Convert.ToInt32(intStatus);
                strStatus = ((InterviewStatusEnum)status).ToString();
            }
            catch
            {
            }

            return strStatus;
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
    }
}