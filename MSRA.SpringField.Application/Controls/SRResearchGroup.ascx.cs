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
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.BizObjects;

namespace MSRA.SpringField.Application.Controls
{
    public partial class Controls_SRResearchGroup : System.Web.UI.UserControl
    {
        private List<string> FullNameGroupList = StaticData.GroupList;
        private ArrayList ShortNameGroupList = CheckInFormResourceManager.GetTypeDisplayItems("Groups");
        private DataView AllCandidates;
        public DateTime m_StartDate, m_EndDate;

        protected void Page_Load(object sender, EventArgs e)
        {
            FullNameGroupList.Remove("Other");

            AllCandidates = new DataView(CashedApplicantInfo.GetAllApplcantsforSourcingReport(m_StartDate, m_EndDate).Tables[0]);

            DataTable tbGroup = new DataTable();
            tbGroup.Columns.Add("Group", typeof(string));
            tbGroup.Columns.Add("Cadt #", typeof(string));
            foreach (string groupName in FullNameGroupList)
            {
                tbGroup.Rows.Add(groupName, "0");
            }
            tbGroup.Rows.Add("Candidate Number", "0");

            //string strApplicationDateFilter = "(ApplicationDate >= '" + m_StartDate.ToString() + "' AND ApplicationDate <= '" + m_EndDate.AddDays(1).ToString() + "') ";
            string strFilter;
            string Key;

            for (int i = 0; i < FullNameGroupList.Count; i++)
            {
                strFilter = " (InterestedGroup like '%" + tbGroup.Rows[i]["Group"].ToString().Replace("\'", "\'\'") + "%')";
                Key = "Group_Cadt_" + i.ToString() + "_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
                if (SiteCache.Get(Key) == null)
                {
                    SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
                }
                AllCandidates.RowFilter = strFilter;
                tbGroup.Rows[i]["Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());
            }
            strFilter = "";
            Key = "Group_Cadt_CandidateNumber_" + m_StartDate.ToShortDateString() + "_" + m_EndDate.ToShortDateString();
            if (SiteCache.Get(Key) == null)
            {
                SiteCache.Insert(Key, strFilter, SiteCache.DefaultExpiration);
            }
            AllCandidates.RowFilter = strFilter;
            tbGroup.Rows[tbGroup.Rows.Count - 1]["Cadt #"] = String.Format("<a href='ShowCandidates.aspx?type=source&StartDate=" + m_StartDate.ToString("MM/dd/yyyy") + "&EndDate=" + m_EndDate.ToString("MM/dd/yyyy") + "&key=" + Key + "'>{0}</a>", AllCandidates.Count.ToString());

            gvGroupCadt.DataSource = tbGroup.DefaultView;
            gvGroupCadt.DataBind();
        }

        public void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename= Sourcing Report - by Group" + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/excel";
            Response.Charset = "utf-8";
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
            Response.ContentEncoding = Encoding.UTF8;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvGroupCadt.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}