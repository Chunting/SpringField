using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Springfield.Components;
using Springfield.Components.Configuration;

public partial class PageApplicantEduBackground : System.Web.UI.Page
{
    bool isUpdate = false;
    Guid curId = Guid.Empty;
    ApplicantEduBackground aeb = null;
    MembershipUser curUser = null;
    string email;

    protected void Page_Load(object sender, EventArgs e)
    {
        email = Request.QueryString["email"].ToString();

        btnSelectDate2.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEnrollDate.ClientID + ",'mm/dd/yyyy');");
        btnSelectDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbGraduateDate.ClientID + ",'mm/dd/yyyy');");
        
        tbEnrollDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbEnrollDate.ClientID + ",'mm/dd/yyyy');");
        tbGraduateDate.Attributes.Add("onclick", "popUpCalendar(this,document.all." + tbGraduateDate.ClientID + ",'mm/dd/yyyy');");
        tbEnrollDate.Attributes.Add("readonly", "true");
        tbGraduateDate.Attributes.Add("readonly", "true");

        if (!IsPostBack)
        {
            //string[] colleges = EnumHelper.GetEnumStrings(typeof(CollegeEnum));
            //ddlCollege.DataSource = colleges;
            //ddlCollege.DataBind();
        }

        curUser = Membership.GetUser(Server.UrlDecode(email));
        curId = SiteUser.GetIdByFullName(curUser.UserName);
        aeb = ApplicantEduBackground.GetApplicantEduBackgroundById(curId);

        //education background exist
        if (aeb != null)
        {
            isUpdate = true;
            if (!IsPostBack)
            {
                PopulateEduBackground();
            }
        }
        else
        {
            if (!IsPostBack)
            {
                aeb = new ApplicantEduBackground();
            }
        }

        if (Session[email] == null)
        {
            Session[email] = aeb;
        }
        else
        {
            aeb = Session[email] as ApplicantEduBackground;
        }
    }

    protected void PopulateEduBackground()
    {
        tbAdvisorEmail.Text = aeb.InternAdvisor.Email;
        tbAdvisorFirstName.Text = aeb.InternAdvisor.FirstName;
        tbAdvisorLastName.Text = aeb.InternAdvisor.LastName;
        tbOrganization.Text = aeb.InternAdvisor.Organization;
        tbEnrollDate.Text = (aeb.EnrollDate == DateTime.MaxValue ? "" : aeb.EnrollDate.ToShortDateString());
        tbGraduateDate.Text = (aeb.GraduateDate == DateTime.MaxValue ? "" : aeb.GraduateDate.ToShortDateString());
        //ddlCollege.SelectedIndex = (int)aeb.HighestEduInstitution;
        cs.CollegeName = aeb.HighestEduInstitution;
        ddlDegree.SelectedIndex = (int)aeb.Degree;
        ddlGrade.SelectedIndex = (int)aeb.YearOfStudy - 1;
        ddlMajorCategory.SelectedIndex = (int)aeb.MajorCategory;
        tbMajor.Text = aeb.Major;
        //removed
        //ddlRank.SelectedIndex = (int)aeb.Rank;
        //ddlResearchApproach.SelectedIndex = (int)aeb.ResearchApproach;

        if (aeb.Resume != null && aeb.Resume.DocId != 0)
        {
            lbResume.Text = aeb.Resume.OriginalName;
        }

        if (aeb.Papers[0] != null && aeb.Papers[0].DocId != 0)
        {
            lbPaperA.Text = aeb.Papers[0].OriginalName;
        }

        if (aeb.Papers[1] != null && aeb.Papers[1].DocId != 0)
        {
            lbPaperB.Text = aeb.Papers[1].OriginalName;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string graduateDate = tbGraduateDate.Text.Trim();
        string strEnrollDate = tbEnrollDate.Text.Trim();
        DateTime theDate,enrollDate;

        if (string.IsNullOrEmpty(strEnrollDate))
        {
            lbMsg.Text = "Enroll Date error, please reinput!";
            return;
        }
        else
        {
            try
            {
                enrollDate = Convert.ToDateTime(strEnrollDate);
            }
            catch
            {
                lbMsg.Text = "Enroll Date error, please reinput!";
                return;
            }
        }

        if(string.IsNullOrEmpty(graduateDate))
        {
            lbMsg.Text = "Graduate Date error, please reinput!";
            return;
        }
        else
        {
            try
            {
                theDate = Convert.ToDateTime(graduateDate);
            }
            catch
            {
                lbMsg.Text = "Graduate Date error, please reinput!";
                return;
            }
        }
        try
        {
            if (isUpdate)
            {
                aeb.Degree = (DegreeEnum)ddlDegree.SelectedIndex;
                //aeb.Grade = ddlGrade.SelectedIndex;
                aeb.EnrollDate = enrollDate;
                aeb.GraduateDate = theDate;
                //aeb.HighestEduInstitution = (CollegeEnum)ddlCollege.SelectedIndex;
                aeb.HighestEduInstitution = cs.CollegeName;
                aeb.InternAdvisor.FirstName = GlobalHelper.ClearInput(tbAdvisorFirstName.Text.Trim(), 256, false);
                aeb.InternAdvisor.LastName = GlobalHelper.ClearInput(tbAdvisorLastName.Text.Trim(), 256, false);
                aeb.InternAdvisor.Email = GlobalHelper.ClearInput(tbAdvisorEmail.Text.Trim(), 256, false);
                aeb.InternAdvisor.Organization = GlobalHelper.ClearInput(tbOrganization.Text.Trim(), 256, false);
                aeb.Major = GlobalHelper.ClearInput(tbMajor.Text.Trim(), 256, false);
                aeb.MajorCategory = (MajorCategoryEnum)ddlMajorCategory.SelectedIndex;
                //removed
                //aeb.Rank = (RankEnum)ddlRank.SelectedIndex;
                //aeb.ResearchApproach = (ResearchApproachEnum)ddlResearchApproach.SelectedIndex;
                aeb.YearOfStudy = ddlGrade.SelectedIndex + 1;
                aeb.Update();
            }
            else
            {
                aeb.Degree = (DegreeEnum)ddlDegree.SelectedIndex;
                //aeb.Grade = ddlGrade.SelectedIndex;
                aeb.EnrollDate = enrollDate;
                aeb.GraduateDate = theDate;
                //aeb.HighestEduInstitution = (CollegeEnum)ddlCollege.SelectedIndex;
                aeb.HighestEduInstitution = cs.CollegeName;
                aeb.InternAdvisor.FirstName = GlobalHelper.ClearInput(tbAdvisorFirstName.Text.Trim(), 256, false);
                aeb.InternAdvisor.LastName = GlobalHelper.ClearInput(tbAdvisorLastName.Text.Trim(), 256, false);
                aeb.InternAdvisor.Email = GlobalHelper.ClearInput(tbAdvisorEmail.Text.Trim(), 256, false);
                aeb.InternAdvisor.Organization = GlobalHelper.ClearInput(tbOrganization.Text.Trim(), 256, false);
                aeb.Major = GlobalHelper.ClearInput(tbMajor.Text.Trim(), 256, false);
                aeb.MajorCategory = (MajorCategoryEnum)ddlMajorCategory.SelectedIndex;
                //removed                
                //aeb.Rank = (RankEnum)ddlRank.SelectedIndex;
                //aeb.ResearchApproach = (ResearchApproachEnum)ddlResearchApproach.SelectedIndex;
                aeb.YearOfStudy = ddlGrade.SelectedIndex + 1;

                aeb.ApplicantId = curId;
                aeb.Insert();
            }
        }
        finally
        {
            Session[email] = null;
        }

        btnSubmit.Visible = false;
        btnSubmit.Enabled = false;
        Server.Transfer(string.Format("ApplicantRelatedInfo.aspx?email={0}", Server.UrlEncode(email)));


        //lbMsg.Text = "Your data has up to date, Please go back to main page and complete the rest part of application!";

        ////check to change status?
        //if (Applicant.CheckComplete(curId))
        //{
        //    ApplicantBasicInfo.ChangeApplicantStatus(curId, ApplicationStatusEnum.ApplicationComplete);
        //    lbMsg.Text = "You have completed your application! A email will send to your register email address!";
        //    //send email
        //}
    }

    protected void btnAddResume_Click(object sender, EventArgs e)
    {
        Document curResume = UploadResume(curId);
        if (curResume == null)
        {
            lbMsg.Text = "Resume upload error!";
        }
        else
        {
            ApplicantBasicInfo abi = ApplicantBasicInfo.GetApplicantBasicInfoById(curId);
            abi.ApplicationTime = DateTime.Today;
            abi.Update();
            aeb.Resume = curResume;

            String resumeImagePath = SiteConfiguration.GetConfig().SiteAttributes["docPath"] + aeb.Resume.SaveName;
            Byte[] resumeImage = GlobalHelper.GetFileImage(resumeImagePath);

            aeb.ResumeImage = resumeImage;
            if (aeb.Resume.SaveName.IndexOf('.') == -1)
            {
                //default is ".doc"
                aeb.ResumeExt = ".doc";
            }
            else
            {
                string[] temp = aeb.Resume.SaveName.Split('.');
                aeb.ResumeExt = String.Format(".{0}", temp[temp.Length - 1]);
            }
        }
    }

    protected void btnPaperA_Click(object sender, EventArgs e)
    {
        Document curPaperA = UploadPaperA(curId);
        if(curPaperA == null)
        {
            lbMsg.Text = "PaperA upload error!";
        }
        else
        {
            aeb.Papers[0] = curPaperA;
        }
    }

    protected void btnPaperB_Click(object sender, EventArgs e)
    {
        aeb.Papers[1] = UploadPaperB(curId);
    }

    private Document UploadResume(Guid id)
    {
        Document curDoc = null;
        if (fuResume.HasFile)
        {
            string ext = System.IO.Path.GetExtension(fuResume.FileName).ToLower();
            if (ext == ".pdf" || ext == ".doc" || ext == ".docx" || ext == ".htm" || ext == ".html" || ext == ".rtf")
            {
                string newFileName = id.ToString() + "_cv" + ext;
                fuResume.SaveAs(SiteConfiguration.GetConfig().SiteAttributes["docPath"] + newFileName);

                if (aeb.Resume != null && aeb.Resume.DocId != 0)
                {
                    curDoc = aeb.Resume;                                      
                }
                else
                {
                    curDoc = new Document();
                }
                curDoc.SaveName = newFileName;
                curDoc.OriginalName = fuResume.FileName;
                curDoc.DocType = DocumentEnum.CV;
                curDoc.ApplicantId = id;

                if (aeb.Resume != null && aeb.Resume.DocId != 0)
                {
                    curDoc.Update();
                }
                else
                {
                    curDoc.Insert();
                }
                lbResume.Text = curDoc.OriginalName + " uploaded successfully!";
            }
            else
            {
                return null;
            }
        }
        return curDoc;
    }

    private Document UploadPaperA(Guid id)
    {
        Document curDoc = null;
        if (fuPaperA.HasFile)
        {
            string ext = System.IO.Path.GetExtension(fuPaperA.FileName).ToLower();
            if (ext == ".pdf" || ext == ".doc" || ext == ".docx" || ext == ".htm" || ext == ".html" || ext == ".rtf")
            {
                string newFileName = id.ToString() + "_paper1" + ext;
                fuPaperA.SaveAs(SiteConfiguration.GetConfig().SiteAttributes["docPath"] + newFileName);

                if (aeb.Papers[0] != null && aeb.Papers[0].DocId != 0)
                {
                    curDoc = aeb.Papers[0];
                }
                else
                {
                    curDoc = new Document();
                }
                curDoc.SaveName = newFileName;
                curDoc.OriginalName = fuPaperA.FileName;
                curDoc.DocType = DocumentEnum.Paper;
                curDoc.ApplicantId = id;

                if (aeb.Papers[0] != null && aeb.Papers[0].DocId != 0)
                {
                    curDoc.Update();
                }
                else
                {
                    curDoc.Insert();
                }
                lbPaperA.Text = curDoc.OriginalName + " uploaded successfully!";
            }
            else
            {
                return null;
            }
        }
        return curDoc;
    }

    private Document UploadPaperB(Guid id)
    {
        Document curDoc = null;
        if (fuPaperB.HasFile)
        {
            string ext = System.IO.Path.GetExtension(fuPaperB.FileName).ToLower();
            if (ext == ".pdf" || ext == ".doc" || ext == ".docx" || ext == ".htm" || ext == ".html" || ext == ".rtf")
            {
                string newFileName = id.ToString() + "_paper2" + ext;
                fuPaperB.SaveAs(SiteConfiguration.GetConfig().SiteAttributes["docPath"] + newFileName);

                if (aeb.Papers[1] != null && aeb.Papers[1].DocId != 0)
                {
                    curDoc = aeb.Papers[1];
                }
                else
                {
                    curDoc = new Document();
                }

                curDoc.SaveName = newFileName;
                curDoc.OriginalName = fuPaperB.FileName;
                curDoc.DocType = DocumentEnum.Paper;
                curDoc.ApplicantId = id;

                if (aeb.Papers[1] != null && aeb.Papers[1].DocId != 0)
                {
                    curDoc.Update();
                }
                else
                {
                    curDoc.Insert();
                }
                lbPaperB.Text = curDoc.OriginalName + " uploaded successfully!";
            }
            else
            {
                return null;
            }
        }
        return curDoc;
    }
}
