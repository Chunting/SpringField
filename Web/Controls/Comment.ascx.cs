using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Springfield.Components;

public partial class Controls_Comment : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected string ParseCommentContent(object dataItem)
    {
        DataRowView drv = (DataRowView)dataItem;
        return GlobalHelper.FormatOutput(Convert.ToString(drv["CommentContent"]));
    }

    public void BindData()
    {
        if (ApplicantId == null || ApplicantId == Guid.Empty)
        {
            JSUtility.Alert(this.Page, "Invalid parameter!");
        }
        else
        {
            gvComments.DataSource = Comment.GetCommentsForApplicant(ApplicantId);
            gvComments.DataBind();
        }
    }

    protected void btnAddComment_Click(object sender, EventArgs e)
    {
        Comment curComment = new Comment();
        //curComment.ApplicantId = m_ApplicantId;
        curComment.ApplicantId = ApplicantId;
        curComment.CommentContent = tbNewComment.Text;
        curComment.Insert();
        //SiteCache.Remove("comment_" + m_ApplicantId.ToString());
        JSUtility.Alert(this.Page, "Comment add successfully!");
        BindData();
    }

    public Guid ApplicantId
    {
        get { Guid g = new Guid(ViewState["comment_applicantid"].ToString()); return g; }
        set { ViewState["comment_applicantid"] = value; }
    }

    protected void gvComments_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComments.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvComments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TableCell curCell = e.Row.Cells[0];
            DataRowView drv = (DataRowView)e.Row.DataItem;

            string curArg = Convert.ToString(drv["CommentId"]);
            ImageButton deleteButton = (ImageButton)curCell.FindControl("btnDeleteComment");
            deleteButton.CommandArgument = curArg;

            if(SiteUser.Current.IsInRole(RoleType.InternRecruiter.ToString()))
            {
                deleteButton.Visible = true;
            }
        }
    }

    protected void gvComments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        { 
            case "DeleteComment":
                Comment.DeleteCommentById(Convert.ToInt32(e.CommandArgument));
                //SiteCache.Remove("comment_" + ApplicantId.ToString());
                BindData();
                JSUtility.Alert(this.Page, "Comment deleted successfully!");
                break;
            default:
                break;
        }
    }
}
