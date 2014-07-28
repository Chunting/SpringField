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
using MSRA.SpringField.Components;

namespace MSRA.SpringField.Application.Controls
{
    public partial class CollegeSelector : System.Web.UI.UserControl
    {
        private string collegeName;
        private string validationGroup;

        protected void Page_Load(object sender, EventArgs e)
        {
            rfvCollegeName.ValidationGroup = validationGroup;
            if (!IsPostBack)
            {
                //List<string> universityList = XmlUtility.GetUniversityData();
                List<string> universityList = StaticData.CollegeNameList;
                //List<string> result = StaticData.CollegeNameList;
                ddlCollegeList.DataSource = universityList;                
                ddlCollegeList.DataBind();                
                tbCollegeName.Text = collegeName;
                if (!IsInList(collegeName))
                {
                    ddlCollegeList.Items.Insert(0, new ListItem("All", ""));
                    ddlCollegeList.Items.FindByText("All").Selected = true;
                    tbCollegeName.Enabled = false;
                    //tbCollegeName.Text = collegeName;
                    //ddlCollegeList.SelectedIndex = universityList.IndexOf(StaticData.OTHER_COLLEGE);
                }
                else
                {
                    tbCollegeName.Enabled = false;
                    tbCollegeName.Text = collegeName;
                    ddlCollegeList.SelectedIndex = universityList.IndexOf("----" + collegeName);
                }

                //ddlCollegeList.DataSource = universityList;
                //ddlCollegeList.DataBind();
                //ddlCollegeList.SelectedIndex = universityList.IndexOf(StaticData.OTHER_COLLEGE);
                //tbCollegeName.Enabled = true;
            }
        }

        public string CollegeName
        {
            set { collegeName = value; }
            get { return tbCollegeName.Text; }
        }

        public string ValidationGroup
        {
            get { return validationGroup; }
            set { validationGroup = value; }
        }

        protected void ddlCollegeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //List<string> universityList = XmlUtility.GetUniversityData();
            List<string> universityList = StaticData.CollegeNameList;
            if (ddlCollegeList.SelectedItem.Text.Equals(StaticData.OTHER_COLLEGE))
            {
                //other college
                tbCollegeName.Enabled = true;
                tbCollegeName.Text = string.Empty;
            }
            else
            {
                string university = universityList[(ddlCollegeList.SelectedIndex - 1) >= 0 ? (ddlCollegeList.SelectedIndex - 1) : 0];
                tbCollegeName.Enabled = false;
                if (university.IndexOf("----") == -1)
                {
                    tbCollegeName.Text = "";
                    return;
                }
                university = university.Replace("----", "");
                tbCollegeName.Text = university;
            }
        }

        private bool IsInList(string collegeName)
        {
            //List<string> universityList = XmlUtility.GetUniversityData();
            List<string> universityList = StaticData.CollegeNameList;
            if (universityList.IndexOf("----" + collegeName) == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}