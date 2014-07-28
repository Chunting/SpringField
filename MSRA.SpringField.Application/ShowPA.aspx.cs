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
using MSRA.SpringField.Application.Controls;
using MSRA.SpringField.Components.BizObjects;
using MSRA.SpringField.Components;
using MSRA.SpringField.Components.Configuration;
using MSRA.SpringField.Components.Enumerations;
using MSRA.SpringField.Application.Config.Schemas;
using System.Linq;

namespace MSRA.SpringField.Application
{
    public partial class ShowPA : System.Web.UI.Page
    {
        private Guid applicantId = Guid.Empty;
        //private Interview interview;
        private Interview interview
        {
            set
            {
                ViewState["showapplicant_interview"] = value;
            }
            get
            {
                return ViewState["showapplicant_interview"] as Interview;
            }
        }
        private Applicant curApplicant
        {
            set
            {
                ViewState["showapplicant_curApplicant"] = value;
            }
            get
            {
                return ViewState["showapplicant_curApplicant"] as Applicant;
            }
        }
        private readonly string DocPath = SiteConfiguration.GetConfig().SiteAttributes["docUrl"];

        protected void Page_Init(object sender, EventArgs e)
        {
            //权限验证
            if (!SiteUser.Current.IsInRole(RoleType.InternRecruiter))
            {
                Response.End();
            }

            if (Request["applicant"] == null)
            {
                JSUtility.Alert(this, "Invalid Parameter!");
                JSUtility.CloseWindow(this);
                return;
            }
            else
            {
                try
                {
                    //不能删
                    applicantId = new Guid(Convert.ToString(Request.QueryString["applicant"]));
                }
                catch
                {
                    JSUtility.Alert(this, "Invalid parameter!");
                    JSUtility.CloseWindow(this);
                    return;
                }
            }


            //if (Request["test"] != null)
            //    ucPerformanceAssessment.ApplicantId = "2d83ed78-444a-4041-ae7b-c0d03bbb986d";
            //else
            ucPerformanceAssessment.ApplicantId = applicantId.ToString();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected string GetName()
        {

            Guid appId = new Guid(Request.Params["applicant"].ToString());//Request.QueryString["applicant"]

            SpringFieldDataContext ctx = new SpringFieldDataContext();

            sf_PerformanceAssessment pa =
                ctx.sf_PerformanceAssessments.FirstOrDefault<sf_PerformanceAssessment>(p => p.ApplicantId == appId);
            if (pa != null)
            {
                return pa.InternName +"-"+ pa.InternPhone;
            }
            return "NoName";
        }
    }
}
