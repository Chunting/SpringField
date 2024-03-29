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
using MSRA.ServerControls;
using System.Text;
public partial class Controls_ApplicantsList : System.Web.UI.UserControl
{
    public String ListType
    {
        set
        {
            ViewState["listtype"] = value;
        }
        get
        {
            if (ViewState["listtype"] == null)
            {
                return "applicantlist";
            }
            return ViewState["listtype"].ToString();
        }
    }
    public String ActiveTab
    {
        set
        {
            ViewState["ActiveTab"] = value;
        }
        get
        {
            if (ViewState["ActiveTab"] == null)
            {
                return "0";
            }
            return ViewState["ActiveTab"].ToString();
        }
    }
    public Object DataSource
    {
        set
        {
            //ChangeGridView();
            this.gvApplicants.DataSource = value;
        }

        get
        {
            return this.gvApplicants.DataSource;
        }
    }

    public Int32 TotalCount
    {
        set
        {
            lbCount.Text = value.ToString();
            PagerControl1.TotalCount = value;
        }
        get
        {
            return PagerControl1.TotalCount;
        }
    }

    public Int32 CurrentPage
    {
        set
        {
            PagerControl1.CurrentPage = value;
        }
        get
        {
            return PagerControl1.CurrentPage;
        }
    }

    public Int32 PageSize
    {
        set
        {
            PagerControl1.PageSize = value;
        }
        get
        {
            return PagerControl1.PageSize;
        }
    }


    private static readonly object EventClick = new object();
    public delegate void PagerClickChangedEventHandler(object sender, PagerEventArgs e);
    public event PagerClickChangedEventHandler PagerClickChanged
    {
        add
        {
            Events.AddHandler(EventClick, value);
        }
        remove
        {
            Events.RemoveHandler(EventClick, value);
        }
    }

    protected virtual void OnPagerClick(PagerEventArgs e)
    {
        PagerClickChangedEventHandler clickHandler = (PagerClickChangedEventHandler)Events[EventClick];
        if (clickHandler != null)
        {
            clickHandler(this, e);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            
            SetButtonStatus();
        }
    }

    protected override void OnInit(EventArgs e)
    {
        
        base.OnInit(e);
    }
    protected override void OnDataBinding(EventArgs e)
    {
        //ChangeGridView();
        base.OnDataBinding(e);
    }
    public void ChangeGridView()
    {
        TemplateField tfId = new TemplateField();
        tfId.AccessibleHeaderText = "Check";
        tfId.HeaderText = "Check";
        if (ListType == "favorite")
        {
            tfId.ItemTemplate = new FavoriteTemplateColumn("FavoriteId");
        }
        else
        {
            tfId.ItemTemplate = new FavoriteTemplateColumn("ApplicantId");
        }
        gvApplicants.Columns.Insert(0, tfId);
    }
    protected void btnMultiInterview_Click(object sender, EventArgs e)
    {
        if (Session["interviewees"] != null)
        {
            Session["interviewees"] = null;
        }

        if (Request.Form["cb_ischeck"] != null)
        {
            string[] idArr = Request.Form["cb_ischeck"].ToString().Split(',');
            Session["interviewees"] = idArr;
            JSUtility.OpenNewWindow(this.Page, "MultiInterview.aspx", string.Empty);
            PagerEventArgs pe = new PagerEventArgs(PagerControl1.CurrentPage - 1, PagerControl1.PageSize);
            OnPagerClick(pe);
        }
        else
        {
            JSUtility.Alert(this.Page, "None of applicant has been selected!");
        }
    }

    protected void btnMultiDelete_Click(object sender, EventArgs e)
    {
        if (Request.Form["cb_ischeck"] != null)
        {
            string[] idArr = Request.Form["cb_ischeck"].ToString().Split(',');
            foreach (string idStr in idArr)
            {
                Guid curId = new Guid(idStr.Trim());
                //delete the membership info
                ApplicantBasicInfo abi = ApplicantBasicInfo.GetApplicantBasicInfoById(curId);
                Membership.DeleteUser(abi.Email, true);
                //delete the applicant info
                Applicant.DeleteApplcantById(curId);
            }
            JSUtility.Alert(this.Page, "All selected items have been deleted permanently!");
            FirePagerEvent();
        }
        else
        {
            JSUtility.Alert(this.Page, "None of applicant has been deleted!");
        }
    }

    protected void btnMultiRefer_Click(object sender, EventArgs e)
    {
        if (Session["referrals"] != null)
        {
            Session["referrals"] = null;
        }

        if (Request.Form["cb_ischeck"] != null)
        {
            string[] idArr = Request.Form["cb_ischeck"].ToString().Split(',');
            Session["referrals"] = idArr;
            JSUtility.OpenNewWindow(this.Page, "MultiReferral.aspx", string.Empty);
        }
        else
        {
            JSUtility.Alert(this.Page, "None of applicant has been selected!");
        }
    }

    protected void btnDeleteFavorite_Click(object sender, EventArgs e)
    {
        if (Request.Form["cb_ischeck"] != null)
        {
            string[] idArr = Request.Form["cb_ischeck"].ToString().Split(',');
            foreach (string idStr in idArr)
            {
                //Favorite.DeleteFavoriteById(Convert.ToInt32(idStr));
                Favorite.DeleteFavorite(SiteUser.Current.SiteUserId, new Guid(idStr));
            }
            JSUtility.Alert(this.Page, "All selected items have been deleted permanently!");
            FirePagerEvent();
        }
        else
        {
            JSUtility.Alert(this.Page, "None of item has been deleted!");
        }
    }

    private void FirePagerEvent()
    {
        PagerEventArgs e = new PagerEventArgs(PagerControl1.CurrentPage - 1, PagerControl1.PageSize);
        OnPagerClick(e);
    }
    protected void gvApplicants_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ibFavorite = (ImageButton)e.Row.FindControl("btnAddFavorite");
            HyperLink hbApplicant = (HyperLink)e.Row.FindControl("hlApplication");
            if (ActiveTab != "0")
            {
                hbApplicant.NavigateUrl = hbApplicant.NavigateUrl + "&tab=" + ActiveTab;
            }
            if (ListType == "favorite")
            {
                ibFavorite.Visible = false;
            }
            TableCell curCell = e.Row.Cells[8];
            DataRowView curData = (DataRowView)e.Row.DataItem;
            string curArg = Convert.ToString(curData["ApplicantId"]);
            ImageButton btnAddFavorite = (ImageButton)curCell.FindControl("btnAddFavorite");

            if (btnAddFavorite != null)
            {
                btnAddFavorite.CommandArgument = curArg;
            }

            //Color the referral
            if (curData["ReferralType"] != DBNull.Value)
            {
                ReferralType referralType = (ReferralType)Convert.ToInt32(curData["ReferralType"]);
                if (referralType == ReferralType.KeyReferral)
                {
                    e.Row.BackColor = System.Drawing.Color.Honeydew;
                }
            }

            #region @Author: Liquid.P
            /**
             * Add a link button to the operation cell. 
             * The function of this button is to switch the additional row's display status.
             * 
             * @Author: Liquid.P
             * @Date: 2009-10-29
             **/
            HtmlAnchor link = e.Row.FindControl("switchbtn") as HtmlAnchor;
            if (link != null)
            {
                link.Attributes.Add("id", ((DataRowView)e.Row.DataItem)["ApplicantId"].ToString());
                link.Attributes.Add("href", "javascript:switchSummary('summary_" + (2 * e.Row.RowIndex + 1).ToString() + "')");
            }
            #endregion
        }
    }

    #region @Author: Liquid.P
    /// <summary>
    /// Change the looking of the table before displaying it.
    /// 
    /// @Author: Liquid.P
    /// @Date: 2009-10-29
    /// </summary>
    /// <param name="writer"></param>
    protected override void Render(HtmlTextWriter writer)
    {
        #region Add summary row for every candidates dynamically
        Table tbl = (Table)gvApplicants.Controls[0];
        int rowCount = gvApplicants.Rows.Count;
        for (int i = 1; i <= rowCount; i += 2)
        {
            GridViewRow newrow = new GridViewRow(i + 1, i + 1,
                DataControlRowType.DataRow, DataControlRowState.Normal);
            
            TableCell cell = new TableCell();
            cell.ColumnSpan = gvApplicants.Columns.Count - 2;
            cell.Text = (tbl.Rows[i].Cells[gvApplicants.Columns.Count - 1].
                FindControl("hidValue") as HtmlInputText).Value; //"Summary about specific candidate...";            
            cell.Attributes.Add("id",  "summary_" + i.ToString());
            tbl.Rows[i].Cells[0].RowSpan = 2;
            newrow.Cells.Add(cell);
            tbl.Controls.AddAt(i + 1, newrow);
            rowCount++;
        }

        gvApplicants.Columns[gvApplicants.Columns.Count - 1].Visible = false;
        #endregion

        base.Render(writer);
    }
    #endregion

    protected void gvApplicants_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "AddFavorite":
                {
                    Guid applicantId = new Guid(e.CommandArgument.ToString());
                    Guid ownerId = SiteUser.Current.SiteUserId;
                    if (Favorite.IsFavoriteExists(ownerId, applicantId))
                    {
                        JSUtility.Alert(this.Page, "Favorite item is exists!");
                    }
                    else
                    {
                        Favorite favorite = new Favorite();
                        favorite.ApplicantId = applicantId;
                        favorite.OwnerId = ownerId;
                        favorite.Insert();
                        JSUtility.Alert(this.Page, "Add To Favorite List Successfully!");
                    }
                    break;
                }
            case "DeleteApplicant":
                {
                    Guid applicantId = new Guid(e.CommandArgument.ToString());
                    Guid ownerId = SiteUser.Current.SiteUserId;
                    Applicant.DeleteApplcantById(applicantId);
                    JSUtility.Alert(this.Page, "Delete Application Successfully!");
                    break;
                }
            default:
                break;
        }
    }

    protected string ParseMentor(object dataItem)
    {
        DataRowView dr = (DataRowView)dataItem;
        string curInterviewId = Interview.GetRecentInterviewIdByApplicant(new Guid(dr["ApplicantId"].ToString()));
        string strHiringManagerAlias = "N/A";
        String status = StaticData.AppStatusDict[(ApplicationStatusEnum)dr["Status"]];

        if (status.ToLower() != "available" && status.ToLower() != "Key Referring" && curInterviewId != string.Empty)
        {
            Interview curInterview = Interview.GetInterviewById(Convert.ToInt32(curInterviewId));
            strHiringManagerAlias = SiteUser.GetAliasByUserId(curInterview.HiringManagerId);
        }
        return "<b>" + strHiringManagerAlias + "</b>";
    }

    protected string ParseStatus(object dataItem)
    {
        DataRowView dr = (DataRowView)dataItem;
        //return EnumHelper.EnumToString(EnumHelper.IntegerToEnum(typeof(ApplicationStatusEnum), Convert.ToInt32(dr["Status"])));
        String status = StaticData.AppStatusDict[(ApplicationStatusEnum)dr["Status"]];
        if(status.ToLower() == "rejected")
        {
            status = "Available";
        }
        
        return status;
    }

    protected string ParseInstitution(object dataItem)
    {
        DataRowView dr = (DataRowView)dataItem;
        return dr["HighestEducationalInstitution"] as string;
    }

    protected string ParseDegree(object dataItem)
    {
        DataRowView dr = (DataRowView)dataItem;
        return StaticData.DegreeList[Convert.ToInt32(dr["Degree"])];
    }

    protected string ParseDate(object dataItem)
    {
        DataRowView dr = (DataRowView)dataItem;
        return Convert.ToDateTime(dr["ApplicationDate"]).ToShortDateString();
    }

    protected string ParseLastPA(string ApplicantID)
    {
        String LastPA = "N/A";
        if(String.IsNullOrEmpty(ApplicantID))
            return LastPA;
        Guid guidApplicantID = new Guid(ApplicantID);
        if (guidApplicantID == Guid.Empty)
            return LastPA;
        DataSet dsPA = PerformanceAssessment.GetPerformanceAssessmentByApplicantId(guidApplicantID);
        if (dsPA.Tables[0].Rows.Count > 0 && Convert.ToInt32(dsPA.Tables[0].Rows[0]["OverrallEvaluation"].ToString()) > 0)
            LastPA = PAResourceManager.IdToText("PerformanceLevel", Convert.ToInt32(dsPA.Tables[0].Rows[0]["OverrallEvaluation"].ToString()));
        return LastPA;
    }

    protected string ParseInterviewStatus(object dataItem)
    {
        DataRowView dr = (DataRowView)dataItem;
        Guid curId = (Guid)dr["ApplicantId"];
        string status = Interview.GetRecentInterviewStatus(curId);
        string curInterviewId = Interview.GetRecentInterviewIdByApplicant(curId);
        if (status == "Waiting For Interview Feedback")
        {
            DataSet dsIncompleteFeedback = Feedback.GetIncompleteFeedbackByInterview(Convert.ToInt32(curInterviewId));
            DataTableReader drIncompleteFeedback = dsIncompleteFeedback.Tables[0].CreateDataReader();
            string strIncompleteInterviewers="";
            bool bFirstTime = true;
            while (drIncompleteFeedback.Read())
            {
                if (!bFirstTime) 
                { 
                    strIncompleteInterviewers += " "; 
                }
                else
                {
                    bFirstTime = false;
                }
                strIncompleteInterviewers += "<b>" + drIncompleteFeedback["InterviewerAlias"] + "</b>";
            }
            drIncompleteFeedback.Close();
            if (strIncompleteInterviewers == "")
            {
                strIncompleteInterviewers = "<b>N/A</b>";
            }
            return "Waiting For Interview (" + strIncompleteInterviewers + ") Feedback";
        }
        else if (status == "Waiting For GroupManager Decision")
        {
            Interview curInterview = Interview.GetInterviewById(Convert.ToInt32(curInterviewId));
            string strGrpMgrAlias = "N/A";
            if (curInterview.GroupManagerId != Guid.Empty)
            {
                strGrpMgrAlias = SiteUser.GetAliasByUserId(curInterview.GroupManagerId);
            }
            return "Waiting For GroupManager (<b>" + strGrpMgrAlias + "</b>) Decision";
        }
        else if (status == "Hired")
        {
            ApplicantBasicInfo abi = ApplicantBasicInfo.GetApplicantBasicInfoById(curId);
            string NewStatus;
            switch (abi.Status)
            {
                case ApplicationStatusEnum.Available:
                    NewStatus = "Check-out";
                    break;
                case ApplicationStatusEnum.InterviewinProcess:
                case ApplicationStatusEnum.Hired:
                    NewStatus = "Hired";
                    break;
                case ApplicationStatusEnum.OfferDeclined:
                    NewStatus = "Offer Declined";
                    break;
                case ApplicationStatusEnum.Rejected:
                    NewStatus = "Rejected";
                    break;
                case ApplicationStatusEnum.OnBoard:
                    NewStatus = "On Board";
                    break;
                default:
                    NewStatus = "N/A";
                    break;
            }
            return NewStatus;
        }
        else
        {
            return status;
        }
    }

    protected void PagerControl1_PagerClick(object sender, PagerEventArgs e)
    {
        PagerEventArgs pe = e;
        OnPagerClick(pe);
    }

    private void SetButtonStatus()
    {
        if (ListType == "applicantlist")
        {
            if (Roles.IsUserInRole(SiteUser.Current.FullName, RoleType.InternRecruiter.ToString()))
            {
                btnScheduleInterview.Visible = true;
                btnDeleteSelection.Visible = true;
                btnRecommend.Visible = true;
                btnMultiInterview.Visible = true;
                btnMultiDelete.Visible = true;
                btnMultiRefer.Visible = true;
                btnDeleteFavorite.Visible = false;
            }
            else
            {
                btnScheduleInterview.Visible = true;
                btnDeleteSelection.Visible = false;
                btnRecommend.Visible = true;
                btnMultiInterview.Visible = true;
                btnMultiDelete.Visible = false;
                btnMultiRefer.Visible = true;
                btnDeleteFavorite.Visible = false;
            }
        }
        else if (ListType == "favorite")// favorite list
        {
            btnScheduleInterview.Visible = false;
            btnDeleteSelection.Visible = false;
            btnRecommend.Visible = false;
            btnMultiInterview.Visible = false;
            btnMultiDelete.Visible = false;
            btnMultiRefer.Visible = false;
            btnDeleteFavorite.Visible = true;
        }
    }

    public class FavoriteTemplateColumn : ITemplate
    {

        private string colname;

        public FavoriteTemplateColumn(string cname)
        {

            colname = cname;

        }

        public void InstantiateIn(Control container)
        {
            LiteralControl l = new LiteralControl();
            l.DataBinding += new EventHandler(this.OnDataBinding);
            container.Controls.Add(l);
        }

        public void OnDataBinding(object sender, EventArgs e)
        {
            LiteralControl l = (LiteralControl)sender;

            GridViewRow container = (GridViewRow)l.NamingContainer;
            String strId = ((DataRowView)container.DataItem)[colname].ToString();

            StringBuilder sbCheckBox = new StringBuilder();
            sbCheckBox.Append("<input type=\"checkbox\" id=\"cb_ischeck\" name=\"cb_ischeck\" value=\"");
            sbCheckBox.Append(strId);
            sbCheckBox.Append("\"/>");
            l.Text = sbCheckBox.ToString();
        }

    }


}
