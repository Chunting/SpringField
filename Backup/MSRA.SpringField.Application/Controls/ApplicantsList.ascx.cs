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
using MSRA.SpringField.Controls;
using System.Text;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Application.Config.Schemas;
using System.Linq;
using System.Data.Linq;
using MSRA.SpringField.Foundation;

namespace MSRA.SpringField.Application.Controls
{
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
                Td1.Visible = true;
            }
            else
            {
                tfId.ItemTemplate = new FavoriteTemplateColumn("ApplicantId");
                Td1.Visible = false;
            }
            gvApplicants.Columns.Insert(0, tfId);
        }

        #region 中间图标按钮 单击事件
        //批量 安排面试
        protected void btnMultiInterview_Click(object sender, EventArgs e)
        {
            if (Session["interviewees"] != null)
            {
                Session["interviewees"] = null;
            }

            if (Request.Form["cb_ischeck"] != null)
            {
                string[] idArr = Request.Form["cb_ischeck"].ToString().Split(',');
                if (ListType == "favorite")
                {
                    SpringFieldDataContext ctx = new SpringFieldDataContext();
                    for (int i = 0; i < idArr.Length; i++)
                    {
                        sf_Favorite fav = ctx.sf_Favorites.FirstOrDefault<sf_Favorite>(
                            p => p.FavoriteId == int.Parse(idArr[i]));
                        idArr[i] = fav.ApplicantId.ToString();
                    }

                }
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

        //可能是批量从favourite中移除吧，不清楚
        protected void btnMultiRemove_Click(object sender, EventArgs e)
        {
            SpringFieldDataContext ctx = new SpringFieldDataContext();

            if (Request.Form["cb_ischeck"] != null)
            {
                string[] idArr = Request.Form["cb_ischeck"].ToString().Split(',');
                foreach (string idStr in idArr)
                {
                    var entity = from favorite in ctx.sf_Favorites
                                 where favorite.FavoriteId.Equals(idStr)
                                 select favorite;

                    if (entity != null)
                    {
                        ctx.sf_Favorites.DeleteOnSubmit(entity.FirstOrDefault<sf_Favorite>());
                    }

                    ctx.SubmitChanges();
                }
                JSUtility.Alert(this.Page, "All selected items have been removed from favorite list!");
                FirePagerEvent();
            }
            else
            {
                JSUtility.Alert(this.Page, "None of applicant has been removed!");
            }
        }

        //批量删除
        protected void btnMultiDelete_Click(object sender, EventArgs e)
        {
            if (Request.Form["cb_ischeck"] != null)
            {
                string[] idArr = Request.Form["cb_ischeck"].ToString().Split(',');
                foreach (string idStr in idArr)
                {
                    string id = idStr;
                    if (ListType == "favorite")
                    {
                        SpringFieldDataContext ctx = new SpringFieldDataContext();

                        sf_Favorite fav = ctx.sf_Favorites.FirstOrDefault<sf_Favorite>(p => p.FavoriteId == int.Parse(id));
                        if (fav != null)
                        {
                            id = fav.ApplicantId.ToString();
                        }
                    }
                    Guid curId = new Guid(id.Trim());
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

        //推荐给mentor
        protected void btnMultiRefer_Click(object sender, EventArgs e)
        {
            if (Session["referrals"] != null)
            {
                Session["referrals"] = null;
            }

            if (Request.Form["cb_ischeck"] != null)
            {
                string[] idArr = Request.Form["cb_ischeck"].ToString().Split(',');
                if (ListType == "favorite")
                {
                    SpringFieldDataContext ctx = new SpringFieldDataContext();
                    for (int i = 0; i < idArr.Length; i++)
                    {
                        sf_Favorite fav = ctx.sf_Favorites.FirstOrDefault<sf_Favorite>(
                            p => p.FavoriteId == int.Parse(idArr[i]));
                        idArr[i] = fav.ApplicantId.ToString();
                    }

                }
                Session["referrals"] = idArr;
                JSUtility.OpenNewWindow(this.Page, "MultiReferral.aspx", string.Empty);
            }
            else
            {
                JSUtility.Alert(this.Page, "None of applicant has been selected!");
            }
        }

        //加入最喜欢
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

        //------------------add by bin------------------------------
        //群发邮件
        protected void btnMultiSend_Click(object sender, EventArgs e)
        {
            if (Request.Form["cb_ischeck"] != null)
            {
                string[] idArr = Request.Form["cb_ischeck"].ToString().Split(',');

                foreach (string id in idArr)
                {
                    Guid appId = new Guid(id);
                    MailHelper mHelper = new MailHelper();
                    mHelper.AddNoticeApplicantVariables(appId);
                    mHelper.SendMail(MailType.NoticeApplicant);

                }

                Response.Write("<script>alert('Send notice emails successfully'); window.close();</script>");

            }
            else
            {
                JSUtility.Alert(this.Page, "None of applicant has been selectd!");
            }

        }
        #endregion

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
                HtmlGenericControl txtFav = (HtmlGenericControl)e.Row.FindControl("txtFav");
                HyperLink hbApplicant = (HyperLink)e.Row.FindControl("hlApplication");
                HtmlGenericControl divRemove = (HtmlGenericControl)e.Row.FindControl("divRemove");
                HtmlGenericControl lbl = (HtmlGenericControl)e.Row.FindControl("lbl");

                lbl.Attributes.Add("for", ibFavorite.ClientID);
                if (ActiveTab != "0")
                {
                    hbApplicant.NavigateUrl = hbApplicant.NavigateUrl + "&tab=" + ActiveTab;
                }

                if (ListType == "favorite")
                {
                    ibFavorite.Visible = false;
                    txtFav.Visible = false;
                    divRemove.Visible = true;

                    ImageButton ibRemove = e.Row.FindControl("ibRemove") as ImageButton;
                    if (ibRemove != null)
                    {
                        DataRowView dvRow = e.Row.DataItem as DataRowView;
                        if (dvRow != null)
                        {
                            ibRemove.CommandArgument = dvRow.Row.Field<int>("FavoriteId").ToString();
                        }
                    }
                }
                else
                {
                    divRemove.Visible = false;
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
                //HtmlAnchor link = e.Row.FindControl("switchbtn") as HtmlAnchor;
                //if (link != null)
                //{
                //    link.Attributes.Add("id", ((DataRowView)e.Row.DataItem)["ApplicantId"].ToString());
                //    link.Attributes.Add("href", "javascript:switchSummary('summary_" + (2 * e.Row.RowIndex + 1).ToString() + "')");
                //}
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
            //Table tbl = (Table)gvApplicants.Controls[0];            
            //int rowCount = gvApplicants.Rows.Count;
            //for (int i = 1; i <= rowCount; i += 2)
            //{
            //    GridViewRow newrow = new GridViewRow(i + 1, i + 1,
            //        DataControlRowType.DataRow, DataControlRowState.Normal);

            //    TableCell cell = new TableCell();
            //    cell.ColumnSpan = gvApplicants.Columns.Count - 2;
            //    cell.Text = (tbl.Rows[i].Cells[gvApplicants.Columns.Count - 1].
            //        FindControl("hidValue") as HtmlInputText).Value; //"Summary about specific candidate...";            
            //    cell.Attributes.Add("id", "summary_" + i.ToString());
            //    tbl.Rows[i].Cells[0].RowSpan = 2;
            //    newrow.Cells.Add(cell);
            //    tbl.Controls.AddAt(i + 1, newrow);
            //    rowCount++;
            //}

            gvApplicants.Columns[gvApplicants.Columns.Count - 1].Visible = false;
            #endregion

            base.Render(writer);
        }
        #endregion

        //Add To Favorite
        protected void btnFavorite_Click(object sender, EventArgs e)
        {
            if (Request.Form["cb_ischeck"] != null)
            {
                string[] idArr = Request.Form["cb_ischeck"].ToString().Split(',');
                foreach (string idStr in idArr)
                {
                    Guid ownerId = SiteUser.Current.SiteUserId;
                    if (Favorite.IsFavoriteExists(ownerId, new Guid(idStr)))
                    {
                        JSUtility.Alert(this.Page, "Favorite item is exists!");
                    }
                    else
                    {
                        Favorite favorite = new Favorite();
                        favorite.ApplicantId = new Guid(idStr);
                        favorite.OwnerId = ownerId;
                        favorite.Insert();
                        JSUtility.Alert(this.Page, "Add To Favorite List Successfully!");
                    }
                }
                JSUtility.Alert(this.Page, "All selected items have been deleted permanently!");
                FirePagerEvent();
            }
            else
            {
                JSUtility.Alert(this.Page, "None of applicant has been deleted!");
            }
        }

        // ?? 最后一列action
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
                case "Remove":
                    {
                        SpringFieldDataContext ctx = new SpringFieldDataContext();
                        int favoriteid = int.Parse(e.CommandArgument.ToString());
                        var item = from favorite in ctx.sf_Favorites
                                   where favorite.FavoriteId == favoriteid
                                   select favorite;
                        if (item != null)
                        {
                            ctx.sf_Favorites.DeleteOnSubmit(item.FirstOrDefault<sf_Favorite>());
                        }

                        ctx.SubmitChanges();

                        Response.Redirect("~/FavoritesList.aspx?mi=Applicants,Favorite Applicants&contrid=item_lnkFavoritesList");
                        break;
                    }
                default:
                    break;
            }
        }

        #region Parse 准备GridView每一列的数据
        protected string ParseMentor(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            string curInterviewId = Interview.GetRecentInterviewIdByApplicant(new Guid(dr["ApplicantId"].ToString()));
            string strHiringManagerAlias = "N/A";
            String status = StaticData.AppStatusDict[(ApplicationStatusEnum)dr["Status"]];

            if (status.ToLower() != "available" && status.ToLower() != "Key Referring" && curInterviewId != string.Empty)
            {
                Interview curInterview = Interview.GetInterviewById(Convert.ToInt32(curInterviewId));

                //InterviewStatus >= 2 means the mentor was fixed in checkinform
                if (((int)curInterview.InterviewStatus) >= 2)
                {
                    SpringFieldDataContext ctx = new SpringFieldDataContext();
                    sf_CheckInForm form =
                        ctx.sf_CheckInForms.FirstOrDefault<sf_CheckInForm>(p => p.FormId == curInterview.CheckInFormId);
                    if (form != null)
                    {
                        strHiringManagerAlias = form.MentorAlias;
                    }
                }
                else
                {
                    strHiringManagerAlias = SiteUser.GetAliasByUserId(curInterview.HiringManagerId);
                }
            }

            return "<b>" + strHiringManagerAlias + "</b>";
        }

        protected string ParseStatus(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            //return EnumHelper.EnumToString(EnumHelper.IntegerToEnum(typeof(ApplicationStatusEnum), Convert.ToInt32(dr["Status"])));
            String status = StaticData.AppStatusDict[(ApplicationStatusEnum)dr["Status"]];
            if (status.ToLower() == "rejected")
            {
                status = "Available";
            }
            /*
             * Add by Yuanqin, 2011.5.5
             * Add new status
             */
            if (status.ToLower() == "qualifiedbutnotmatched")
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
            if (dr["ApplicationDate"] == DBNull.Value)
            {
                return "";
            }
            else
            {
                return Convert.ToDateTime(dr["ApplicationDate"]).ToShortDateString();
            }
        }

        protected string ParseCheckBox(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            if (dr.Row.Table.Columns.Contains("FavoriteId") || ListType == "favorite")
            {
                return Convert.ToString(dr.Row.Field<int>("FavoriteId"));
            }
            return Convert.ToString(dr.Row.Field<Guid>("ApplicantId"));
        }

        protected string ParseLastPA(string ApplicantID)
        {
            String LastPA = "N/A";
            if (String.IsNullOrEmpty(ApplicantID))
                return LastPA;
            Guid guidApplicantID = new Guid(ApplicantID);
            if (guidApplicantID == Guid.Empty)
                return LastPA;
            DataSet dsPA = PerformanceAssessment.GetPerformanceAssessmentByApplicantId(guidApplicantID);
            if (dsPA.Tables[0].Rows.Count > 0 && dsPA.Tables[0].Rows[0]["OverrallEvaluation"].ParseInt() > 0)
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
                string strIncompleteInterviewers = "";
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
            //PagerEventArgs pe = e;
            OnPagerClick(e);
        }
        #endregion

        //设置中间行 图标按钮的状态
        private void SetButtonStatus()
        {
            if (ListType == "applicantlist")
            {
                if (Roles.IsUserInRole(SiteUser.Current.FullName, RoleType.InternRecruiter.ToString()))
                {
                    btnMultiInterview.Visible = true;
                    btnDeleteSelection.Visible = true;//可
                    //btnRecommend.Visible = true;
                    //btnMultiDelete.Visible = true;
                    btnMultiRefer.Visible = true;
                    //btnDeleteFavorite.Visible = false;
                    btnSendEmailSelection.Visible = true;//可
                }
                else
                {
                    btnMultiInterview.Visible = true;
                    btnDeleteSelection.Visible = false;//不可
                    //btnRecommend.Visible = true;
                    //btnMultiDelete.Visible = false;
                    btnMultiRefer.Visible = true;
                    //btnDeleteFavorite.Visible = false;
                    btnSendEmailSelection.Visible = false;//不可
                }
                Td1.Visible = false;

            }
            else if (ListType == "favorite")// favorite list 目前基本没人用
            {
                btnMultiInterview.Visible = true;

                //Modify by Yuanqin, 2011.3.2
                btnDeleteSelection.Visible = false;

                Td1.Visible = true;
                //btnRecommend.Visible = false;
                //btnMultiDelete.Visible = false;
                btnMultiRefer.Visible = true;
                btnFavorite.Visible = false;
                //btnDeleteFavorite.Visible = true;
                btnSendEmailSelection.Visible = false;
            }
        }
        
        // 最喜欢的模版（不知道是干什么用的）
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
}