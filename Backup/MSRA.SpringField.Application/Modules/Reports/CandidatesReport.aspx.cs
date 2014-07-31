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
using System.Text;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Application
{
    public partial class CandidatesReport : System.Web.UI.Page
    {
        //private string strFilter;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["StartDate"].Length > 0)
                {
                    tbStartApplyDate.Text = DateTime.Parse(Request.QueryString["StartDate"]).ToString("yyyy-MM-dd");
                }

                if (Request.QueryString["EndDate"].Length > 0)
                {
                    tbEndApplyDate.Text = DateTime.Parse(Request.QueryString["EndDate"]).ToString("yyyy-MM-dd");
                }

                tbStartApplyDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbStartApplyDate.ClientID + ",'yyyy-mm-dd');");
                tbEndApplyDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEndApplyDate.ClientID + ",'yyyy-mm-dd');");
                tbStartPreferredStartDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbStartPreferredStartDate.ClientID + ",'yyyy-mm-dd');");
                tbEndPreferredStartDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEndPreferredStartDate.ClientID + ",'yyyy-mm-dd');");
                tbStartPreferredEndDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbStartPreferredEndDate.ClientID + ",'yyyy-mm-dd');");
                tbEndPreferredEndDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEndPreferredEndDate.ClientID + ",'yyyy-mm-dd');");

                /*
                 * Add by Yuanqin,
                 * 2011.2.22
                 * Add the data of ddlOffline 
                 */
                string[] OfflineArray = EnumHelper.GetEnumStrings(typeof(IsOfflineEnum));
                for (int i = 0; i < OfflineArray.Length; i++)
                {
                    ddlOffline.Items.Add(new ListItem(OfflineArray[i], i.ToString()));
                }
                ListItem all = new ListItem("All", string.Empty);
                ddlOffline.Items.Insert(0, all);

                BindSourcingDLL();
                BindGroupDLL();
                ShowAdditionalColum();
                BindData();
                InitializeVailableColumns();
            }
        }
        private void BindGroupDLL()
        {
            ddlInterestedGroup.DataSource = StaticData.GroupList;
            ddlInterestedGroup.DataBind();
            ListItem allGroup = new ListItem("All", string.Empty);
            ddlInterestedGroup.Items.Insert(0, allGroup);
            allGroup.Selected = true;
        }
        private void BindSourcingDLL()
        {
            XmlDocument doc = new XmlDocument();
            string fileName = Server.MapPath("~/InfoSourceMapping.xml");
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(fileName, settings);
            doc.Load(reader);

            XmlNode categoryNode = doc.SelectSingleNode("/Info/Category[@ID='Job_Board']");
            foreach (XmlNode node in categoryNode.ChildNodes)
            {
                ddlSourcing.Items.Add(node.Attributes["text"].Value);
            }

            ListItem allSourcing = new ListItem("All", string.Empty);
            ddlSourcing.Items.Insert(0, allSourcing);
            allSourcing.Selected = true;
        }

        private string GetFilter()
        {
            StringBuilder sb = new StringBuilder();

            if (!String.IsNullOrEmpty(tbStartApplyDate.Text.Trim()))
            {
                sb.Append(" (ApplicationDate > '");
                sb.Append(Convert.ToDateTime(tbStartApplyDate.Text.Trim()).AddSeconds(-1).ToString());
                sb.Append("') AND");
            }
            if (!String.IsNullOrEmpty(tbEndApplyDate.Text.Trim()))
            {
                sb.Append(" (ApplicationDate < '");
                sb.Append(Convert.ToDateTime(tbEndApplyDate.Text.Trim()).AddDays(1).ToString());
                sb.Append("') AND");
            }
            if (!String.IsNullOrEmpty(tbStartPreferredStartDate.Text.Trim()))
            {
                sb.Append(" (PreferredStartDate > '");
                sb.Append(Convert.ToDateTime(tbStartPreferredStartDate.Text.Trim()).AddSeconds(-1).ToString());
                sb.Append("') AND");
            }
            if (!String.IsNullOrEmpty(tbEndPreferredStartDate.Text.Trim()))
            {
                sb.Append(" (PreferredStartDate < '");
                sb.Append(Convert.ToDateTime(tbEndPreferredStartDate.Text.Trim()).AddDays(1).ToString());
                sb.Append("') AND");
            }
            if (ddlInterestedGroup.SelectedItem.Text != "All")
            {
                if (ddlInterestedGroup.SelectedItem.Text == "Other")
                {
                    sb.Append(" (InterestedGroup like '%");
                    sb.Append(tbOtherGroup.Text.Trim());
                    sb.Append("%') AND");
                }
                else
                {
                    sb.Append(" (InterestedGroup like '%");
                    //Modify by Yuanqin,2011.2.15
                    //sb.Append(ddlInterestedGroup.SelectedItem.Text);
                    sb.Append(ddlInterestedGroup.SelectedItem.Text.Replace(@"'", "''"));
                    sb.Append("%') AND");
                }
            }
            if (ddlSourcing.SelectedItem.Text != "All")
            {
                if (ddlSourcing.SelectedItem.Text == "Other")
                {
                    sb.Append(" (JobInfoSource NOT IN (");
                    for (int i = 1; i < ddlSourcing.Items.Count - 1; i++)
                    {
                        if (i == 1)
                            sb.Append("'");
                        else
                            sb.Append(",'");
                        sb.Append(ddlSourcing.Items[i].Text);
                        sb.Append("'");
                    }
                    sb.Append(")) AND");
                }
                else
                {
                    //'Microsoft Student Technical Club','Microsoft Studnet Technical Club','Student Technical Club'
                    if (ddlSourcing.SelectedItem.Text.Equals("Microsoft Student Technical Club"))
                    {
                        sb.Append(" ((JobInfoSource = '");
                        sb.Append(ddlSourcing.SelectedItem.Text);
                        sb.Append("') OR (JobInfoSource = 'Microsoft Studnet Technical Club') OR (JobInfoSource = 'Student Technical Club')) AND ");
                    }
                    else
                    {
                        sb.Append(" (JobInfoSource = '");
                        sb.Append(ddlSourcing.SelectedItem.Text);
                        sb.Append("') AND");
                    }
                }
            }

            /*
             * Add by Yuanqin,
             * 2011.2.22
             * Add the filter of ddlOffline 
             */
            if (ddlOffline.SelectedItem.Text != "All")
            {
                sb.Append(" (IsOffline = '");
                sb.Append(Convert.ToInt32(ddlOffline.SelectedValue));
                sb.Append("') AND");
            }

            /*
             * Add by Yuanqin,
             * 2011.4.26
             * Add preferEndDate, InterestedAreas, Status
             */
            if (!String.IsNullOrEmpty(tbStartPreferredEndDate.Text.Trim()))
            {
                sb.Append(" (SecondaryStartDate > '");
                sb.Append(Convert.ToDateTime(tbStartPreferredEndDate.Text.Trim()).AddSeconds(-1).ToString());
                sb.Append("') AND");
            }
            if (!String.IsNullOrEmpty(tbEndPreferredEndDate.Text.Trim()))
            {
                sb.Append(" (SecondaryStartDate < '");
                sb.Append(Convert.ToDateTime(tbEndPreferredEndDate.Text.Trim()).AddDays(1).ToString());
                sb.Append("') AND");
            }
            if (!String.IsNullOrEmpty(tbArea.Text.Trim()))
            {
                sb.Append(" (InterestedAreas like '%");
                sb.Append(tbArea.Text.Trim());
                sb.Append("%') AND");
            }
            if (ddlStatus.SelectedItem.Text != "All")
            {
                Int32 status = Convert.ToInt32(ddlStatus.SelectedValue);
                if (status == 1)//Available
                {
                    sb.Append(" (Status = '");
                    sb.Append(Convert.ToInt32(ApplicationStatusEnum.Available));
                    sb.Append("') AND");
                }
                else if (status == 2)//Waiting For Interview Feedback
                {
                    sb.Append(" (Status = '");
                    sb.Append(Convert.ToInt32(InterviewStatusEnum.WaitingForInterviewFeedback));
                    sb.Append("') AND");
                }
                else if (status == 3)//Wating For Mentor Decision
                {
                    sb.Append(" (Status = '");
                    sb.Append(Convert.ToInt32(InterviewStatusEnum.WaitingForMentorDecision));
                    sb.Append("') AND");
                }
                else if (status == 4)//Waiting For Group Manager Decision
                {
                    sb.Append(" (Status = '");
                    sb.Append(Convert.ToInt32(InterviewStatusEnum.WaitingForGroupManagerDecision));
                    sb.Append("') AND");
                }
                else if (status == 5)//Hired
                {
                    sb.Append(" (Status = '");
                    sb.Append(Convert.ToInt32(ApplicationStatusEnum.Hired));
                    sb.Append("') AND");
                }
                else if (status == 6)//QualifiedButNotMatched
                {
                    sb.Append(" (Status = '");
                    sb.Append(Convert.ToInt32(ApplicationStatusEnum.QualifiedButNotMatched));
                    sb.Append("') AND");
                }
                else if (status == 7)//Rejected
                {
                    sb.Append(" (Status = '");
                    sb.Append(Convert.ToInt32(ApplicationStatusEnum.Rejected));
                    sb.Append("') AND");
                }
                else if (status == 8)//Decline Offer
                {
                    sb.Append(" (Status = '");
                    sb.Append(Convert.ToInt32(ApplicationStatusEnum.OfferDeclined));
                    sb.Append("') AND");
                }
                else if (status == 9)//On Board
                {
                    sb.Append(" (Status = '");
                    sb.Append(Convert.ToInt32(ApplicationStatusEnum.OnBoard));
                    sb.Append("') AND");
                }
            }

            string strFilter = sb.ToString();
            if (strFilter.Length > 4)
                return strFilter.Substring(0, strFilter.Length - 4);
            else
                return "";
        }
        private void InitializeVailableColumns()
        {
            List<string> AvailableColums = new List<string>();
            AvailableColums.Add("Enroll Date in University");
            AvailableColums.Add("Email");
            AvailableColums.Add("Phone");
            AvailableColums.Add("Nationality");
            AvailableColums.Add("Apply Date");
            AvailableColums.Add("Graduated Date");
            AvailableColums.Add("Advisor Name");
            AvailableColums.Add("Preferred Start Date");
            AvailableColums.Add("Preferred End Date");
            AvailableColums.Add("Interested Group");
            AvailableColums.Add("Interested Areas");
            AvailableColums.Add("Type");
            AvailableColums.Add("Source");
            AvailableColums.Add("Channel");
            AvailableColums.Add("Source Detail");
            AvailableColums.Add("Preferred Position");
            AvailableColums.Add("Participated Program");
            AvailableColums.Add("Last PA");
            //AvailableColums.Add("Referral Person");
            AvailableColums.Add("Resume");

            foreach (string col in AvailableColums)
            {
                lbAvailablecolumns.Items.Add(col);
            }
        }
        protected void gvCandidates_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCandidates.PageIndex = e.NewPageIndex;
            ShowAdditionalColum();
            BindData(ddlOrderBy.SelectedValue);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //ddlOrderBy.SelectedIndex = 3;
        }

        private void ShowAdditionalColum()
        {
            //int colCount = gvCandidates.Columns.Count;

            //gvCandidates.Columns
            for (int i = 0; i < lbCurrentcolumns.Items.Count; i++)
            {
                TemplateField tf = new TemplateField();
                //tf.SortExpression
                tf.AccessibleHeaderText = lbCurrentcolumns.Items[i].Text;
                tf.HeaderText = lbCurrentcolumns.Items[i].Text;
                tf.ItemTemplate = new CandidateReportColumn(lbCurrentcolumns.Items[i].Text);
                gvCandidates.Columns.Add(tf);
            }
            //for (int j = 7; j < colCount; j++)
            //{
            //    gvCandidates.Columns.RemoveAt(7);
            //}
        }
        private void BindData(string sortExpression)
        {
            /*
             * Modify for dumplicate applicant, Add tbResume and status
             * Author: Yuanqin
             * Date: 2011.3.9
             */ 
            //DataSet dsApplicant = Applicant.GetAllApplicants();
            DataView dvApplicant;
            if (tbResume.Text.Trim().Length == 0)
            {
                dvApplicant = new DataView(Applicant.GetAllApplicantsWithoutPermissionFilter().Tables[0]);
            }
            else
            {
                dvApplicant = new DataView(Applicant.GetApplicantsByResume(tbResume.Text.Trim()).Tables[0]);
            }
 
            dvApplicant.RowFilter = GetFilter();
            dvApplicant.Sort = sortExpression;
            gvCandidates.DataSource = dvApplicant;
            gvCandidates.DataBind();
            lbCount.Text = dvApplicant.Count.ToString();

            if (tbStartApplyDate.Text.Trim().Length == 0 && tbEndApplyDate.Text.Trim().Length == 0)
            {
                lbTimeSpan.Text = "";
            }
            else
            {
                lbTimeSpan.Text = "Date From ";
                if (!String.IsNullOrEmpty(tbStartApplyDate.Text.Trim()))
                {
                    lbTimeSpan.Text += tbStartApplyDate.Text;

                    if (!String.IsNullOrEmpty(tbEndApplyDate.Text.Trim()))
                    {
                        lbTimeSpan.Text += " To ";
                        lbTimeSpan.Text += tbEndApplyDate.Text;
                    }
                    else
                    {
                        lbTimeSpan.Text = "Later than " + tbStartApplyDate.Text;
                    }
                }
                else
                {
                    lbTimeSpan.Text = "Earlier than " + tbEndApplyDate.Text;
                }
            }
        }
        private void BindData()
        {
            BindData("ApplicationDate Desc");
        }
        protected string ParseEnglishName(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            StringBuilder sb = new StringBuilder();            
            //sb.Append("<a href='../../ShowApplication.aspx?applicant="); 
            sb.Append("<a href='http://msra-spfield/springfield/ShowApplication.aspx?applicant=");          
            sb.Append(dr["ApplicantId"].ToString());
            sb.Append("' target='_self'>"); //sb.Append("&contrid=item_lnkApplicants' target='_self'>");         
            sb.Append(dr["FirstName"].ToString());
            sb.Append(" ");
            sb.Append(dr["LastName"].ToString().ToUpper());
            sb.Append("</a>");

            return sb.ToString();
        }
        protected string ParseStatus(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            String status = StaticData.AppStatusDict[(ApplicationStatusEnum)dr["Status"]];
            if (status.ToLower() == "rejected")
            {
                status = "Available";
            }
            /*
             * Add by Yuanqin, 2011.5.5
             */ 
            if (status.ToLower() == "qualifiedbutnotmatched")
            {
                status = "Available";
            }

            return status;
        }
        protected string ParseDegree(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            return StaticData.DegreeList[Convert.ToInt32(dr["Degree"])] + dr["YearOfStudy"].ToString();
        }
        protected string ParseLastAction(object dataItem)
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
                        NewStatus = "";
                        break;
                }
                return NewStatus;
            }
            else
            {
                return status;
            }
        }
        //protected void gvCandidates_Sorting(object sender, GridViewSortEventArgs e)
        //{        
        //    string sortExpression = "";
        //    //gvCandidates.SortExpression
        //    if (ViewState["sortExpression"] != null)
        //    {//读取已存在排序表达式
        //        sortExpression = ViewState["sortExpression"].ToString();
        //        if (sortExpression == e.SortExpression + " Desc")
        //        {//上次排序表达式为当前列名用为降序时,则按升序排序
        //            sortExpression = e.SortExpression + " Asc";
        //        }
        //        else
        //        {//与上次排序不是同一列名,或上次为升序排序,则改为降序
        //            sortExpression = e.SortExpression + " Desc";
        //        }
        //    }
        //    else
        //    {//第一次默认为降序排列
        //        sortExpression = e.SortExpression + " Desc";
        //    }
        //    //保存排序表达式至viewstate中
        //    ViewState["sortExpression"] = sortExpression;
        //    BindData(sortExpression);//用新的表达式重新绑定gridview
        //}
        //private void bindResults(string sortExpression)
        //{
        //    //hidBegin.Value = txtBeginDate.Value.Trim();
        //    //hidEnd.Value = txtEndDate.Value.Trim();
        //    //hidName.Value = txtName.Text.Trim();
        //    //hidDept.Value = txtDeptNo.Value;

        //    m_hr_StaffTimeSheet sheet = new m_hr_StaffTimeSheet();
        //    //获取dataset
        //    DataSet ds = sheet.GetResultByDeptnoNameDate(txtDeptNo.Value, txtName.Text.Trim(), txtBeginDate.Value, txtEndDate.Value);
        //    //转换为dataview
        //    DataView dv = ds.Tables[0].DefaultView;
        //    if (sortExpression != null && sortExpression != "")
        //    {//排序表达式不为空,则将dataview按sortExpression排序
        //        dv.Sort = sortExpression;
        //    }
        //    else
        //    {
        //        ViewState["sortExpression"] = null;
        //    }

        //    gvResults.DataSource = dv;//绑定已排序dataview
        //    gvResults.DataBind();
        //}
        protected void btnAddColumn_Click(object sender, EventArgs e)
        {
            lbCurrentcolumns.Items.Add(lbAvailablecolumns.SelectedItem);
            lbAvailablecolumns.Items.Remove(lbAvailablecolumns.SelectedItem);
            lbCurrentcolumns.SelectedIndex = lbCurrentcolumns.Items.Count - 1;
        }
        protected void btnRemoveColumn_Click(object sender, EventArgs e)
        {
            lbAvailablecolumns.Items.Add(lbCurrentcolumns.SelectedItem);
            lbCurrentcolumns.Items.Remove(lbCurrentcolumns.SelectedItem);
            lbAvailablecolumns.SelectedIndex = lbAvailablecolumns.Items.Count - 1;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ShowAdditionalColum();
            BindData(ddlOrderBy.SelectedValue);
        }
        protected void ddlInterestedGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlInterestedGroup.SelectedItem.Text == "Other")
            {
                lbOtherGroup.Visible = true;
                tbOtherGroup.Visible = true;
            }
            else
            {
                lbOtherGroup.Visible = false;
                tbOtherGroup.Visible = false;
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            //Form.Controls.Add(gvCandidates);
            this.gvCandidates.AllowPaging = false;
            ShowAdditionalColum();
            BindData(ddlOrderBy.SelectedValue);

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=CandidateReport_" + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/excel";
            Response.Charset = "utf-8";
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
            Response.ContentEncoding = Encoding.UTF8;//.GetEncoding("UTF-8");
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvCandidates.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
    public class CandidateReportColumn : ITemplate
    {

        private string colname;

        public CandidateReportColumn(string cname)
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
            switch (colname)
            {
                case "Advisor Name":
                    l.Text = ((DataRowView)container.DataItem)["AdvisorFirstName"].ToString() + " " + ((DataRowView)container.DataItem)["AdvisorLastName"].ToString().ToUpper();
                    break;
                case "Preferred Start Date":
                    l.Text = ParseDatetime(((DataRowView)container.DataItem)["PreferredStartDate"].ToString());
                    //+ "--"
                    //+ ParseDatetime(((DataRowView)container.DataItem)["SecondaryStartDate"].ToString());      
                    break;
                case "Preferred End Date":
                    l.Text = ParseDatetime(((DataRowView)container.DataItem)["SecondaryStartDate"].ToString());
                    //+ "--"
                    //+ ParseDatetime(((DataRowView)container.DataItem)["SecondaryStartDate"].ToString());      
                    break;
                case "Interested Group":
                    l.Text = ParseInterestedGroup(container.DataItem);
                    break;
                case "Type":
                    l.Text = ParseInternshipType(container.DataItem);
                    break;
                case "Source":
                    l.Text = ParseSourcing(container.DataItem);
                    break;
                case "Channel":
                    l.Text = ParseChannel(container.DataItem);
                    break;
                case "Source Detail":
                    l.Text = ParseSourceDetail(container.DataItem);
                    break;
                case "Preferred Position":
                    l.Text = ParsePreferredPosition(container.DataItem);
                    break;
                case "Participated Program":
                    l.Text = ParseParticipatedProgram(container.DataItem);
                    break;
                case "Last PA":
                    l.Text = ParseLastPA(container.DataItem);
                    break;
                    //case "Referral Person":
                    //    l.Text = ParseReferralPerson(container.DataItem);
                    //break;
                case "Resume":
                    l.Text = ParseResume(container.DataItem);
                    break;
                case "Enroll Date in University":
                    l.Text = ParseDatetime(((DataRowView)container.DataItem)["EnrollDate"].ToString());
                    break;
                case "Email":
                    l.Text = ((DataRowView)container.DataItem)["Email"].ToString();
                    break;
                case "Phone":
                    l.Text = ((DataRowView)container.DataItem)["PhoneNumber"].ToString();
                    break;
                case "Nationality":
                    l.Text = ((DataRowView)container.DataItem)["Nationality"].ToString();
                    break;
                case "Apply Date":
                    l.Text = ParseDatetime(((DataRowView)container.DataItem)["ApplicationDate"].ToString());
                    break;
                case "Graduated Date":
                    l.Text = ParseDatetime(((DataRowView)container.DataItem)["GraduatedDate"].ToString());
                    break;
                case "Interested Areas":
                    l.Text = ((DataRowView)container.DataItem)["InterestedAreas"].ToString();
                    break;
                default:
                    l.Text = "";
                    break;
            }
        }

        protected string ParseDatetime(string strDatetime)
        {
            if (!String.IsNullOrEmpty(strDatetime))
            {
                return Convert.ToDateTime(strDatetime).ToString("yyyy-MM-dd");
            }
            else
                return "";
        }
        protected string ParseLastPA(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            String LastPA = "";
            Guid guidApplicantID = (Guid)dr["ApplicantId"];
            if (guidApplicantID == Guid.Empty)
                return LastPA;
            DataSet dsPA = PerformanceAssessment.GetPerformanceAssessmentByApplicantId(guidApplicantID);
            if (dsPA.Tables[0].Rows.Count > 0 && Convert.ToInt32(dsPA.Tables[0].Rows[0]["OverrallEvaluation"].ToString()) > 0)
                LastPA = PAResourceManager.IdToText("PerformanceLevel", Convert.ToInt32(dsPA.Tables[0].Rows[0]["OverrallEvaluation"].ToString()));
            return LastPA;
        }
        protected string ParseInterestedGroup(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            String InterestedGroup = dr["InterestedGroup"].ToString();
            InterestedGroup = InterestedGroup.Replace(";", "<br/>");
            InterestedGroup = "<span style='text-overflow:ellipsis;overflow:hidden;width:150px'>" + InterestedGroup + "</span>";
            if (InterestedGroup.Length > 5)
                return InterestedGroup.Substring(0, InterestedGroup.Length - "<br/>".Length);
            else
                return InterestedGroup;
        }
        protected string ParseInternshipType(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            int InternshipType = Int32.Parse(dr["InternshipType"].ToString());
            if ((InternshipTypeEnum)InternshipType == InternshipTypeEnum.FullTime)
            {
                return "Full Time";
            }
            else if ((InternshipTypeEnum)InternshipType == InternshipTypeEnum.PartTime)
            {
                return "Part Time";
            }
            else
            {
                return "Unknown";
            }
        }
        protected string ParseSourcing(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            return dr["JobInfoSource"].ToString();
        }
        protected string ParseChannel(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            return dr["JobInfoChannel"].ToString();
        }
        protected string ParseSourceDetail(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            return dr["JobInfoDetail"].ToString();
        }
        protected string ParsePreferredPosition(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            string strPosition = dr["PreferredPosition"].ToString();
            if (!String.IsNullOrEmpty(strPosition))
            {
                int PreferredPosition = Int32.Parse(strPosition);
                string tmp = EnumHelper.EnumToString((PositionTypeEnum)PreferredPosition);
                if ((PositionTypeEnum)PreferredPosition == PositionTypeEnum.EngineeringIntern)
                {
                    return "Engineering Intern";
                }
                else if ((PositionTypeEnum)PreferredPosition == PositionTypeEnum.ResearchIntern)
                {
                    return "Research Intern";
                }
                else
                {
                    return "Unknown";
                }
            }
            else
            {
                return "";
            }
        }
        protected string ParseParticipatedProgram(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            return dr["SpecialProgram"].ToString().Replace("None of the above;", "");
        }
        protected string ParseResume(object dataItem)
        {
            DataRowView dr = (DataRowView)dataItem;
            string strResumeID = dr["ResumeId"].ToString();
            int ResumeID = -1;
            if (!String.IsNullOrEmpty(strResumeID))
            {
                ResumeID = Int16.Parse(strResumeID);
            }
            else
            {
                return "";
            }
            Document resume = Document.GetDocumentById(ResumeID);
            if (resume != null && resume.DocId > 0)
            {
                StringBuilder resumeLnk = new StringBuilder();
                resumeLnk.Append("<a href=\"");
                resumeLnk.Append(SiteConfiguration.GetConfig().SiteAttributes["docUrl"]);
                resumeLnk.Append(resume.SaveName);
                resumeLnk.Append("\" target=\"_blank\">View</a>");
                return resumeLnk.ToString();
            }
            else
            {
                return "";
            }
        }
        //protected string ParseReferralPerson(object dataItem)
        //{
        //    DataRowView dr = (DataRowView)dataItem;
        //    if (dr["JobInfoSource"].ToString().Trim() == "Referral")
        //    {
        //        if (dr["JobInfoChannel"].ToString().Trim() == "MS Employee")
        //        {
        //            return dr["JobInfoDetail"].ToString().Trim() + "[MS Employee]";
        //        }
        //        else if (dr["JobInfoChannel"].ToString().Trim() == "MS Intern")
        //        {
        //            return dr["JobInfoDetail"].ToString().Trim() + "[MS Intern]";
        //        }
        //        else
        //        {
        //            return dr["JobInfoDetail"].ToString().Trim();
        //        }
        //    }

        //    return "";
        //}
    }
}