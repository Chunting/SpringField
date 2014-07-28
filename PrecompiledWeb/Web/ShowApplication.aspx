<%@ page language="C#" autoeventwireup="true" inherits="ShowApplication, App_Web_wkngwpi-" masterpagefile="~/SpringfieldMaster.master" enableeventvalidation="true" %>

<%@ Register Src="Controls/PerformanceAssessment.ascx" TagName="PerformanceAssessment"
    TagPrefix="uc10" %>
<%@ Register Src="Controls/InterviewPanel.ascx" TagName="InterviewPanel" TagPrefix="uc9" %>
<%@ Register Src="Controls/ManagerApproval.ascx" TagName="ManagerApproval" TagPrefix="uc7" %>
<%@ Register Src="Controls/InterviewFeedback.ascx" TagName="InterviewFeedback" TagPrefix="uc8" %>
<%@ Register Src="Controls/MentorSuggestion.ascx" TagName="MentorSuggestion" TagPrefix="uc2" %>
<%@ Register Src="Controls/MentorResult.ascx" TagName="MentorResult" TagPrefix="uc3" %>
<%@ Register Src="Controls/ManagerResult.ascx" TagName="ManagerResult" TagPrefix="uc4" %>
<%@ Register Src="Controls/InterviewHistory.ascx" TagName="InterviewHistory" TagPrefix="uc5" %>
<%@ Register Src="Controls/IncompleteInterviewList.ascx" TagName="IncompleteInterviewList" TagPrefix="uc6" %>
<%@ Register Src="Controls/ApplicantInfoPanel.ascx" TagName="ApplicantInfoPanel" TagPrefix="uc1" %>
<%@ Register TagName="BasicInfo" TagPrefix="Springfield" Src="~/Controls/BasicInfo.ascx" %>
<%@ Register TagName="CommentList" TagPrefix="Springfield" Src="~/Controls/Comment.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="cntMain" runat="server" ContentPlaceHolderID="mainPlaceHolder">

    <script type="text/javascript">
    function ActiveTabChanged(sender, e) {
        var activeTab = $get('<%=activeTab.ClientID%>');
        activeTab.value = sender.get_activeTabIndex();
    }
    </script>

    <Springfield:BasicInfo runat="server" ID="basicInfo" />
    <h3>
    </h3>
    <div>
        <asp:ImageButton CssClass="img_icon" ImageUrl="~/ProUI/images/interview.jpg" ID="btnInterviewCurrent"
            runat="server" AlternateText="Schedule Interview" OnClick="btnInterview_Click" />
        &nbsp;&nbsp;&nbsp;<asp:ImageButton CssClass="img_icon" ImageUrl="~/ProUI/images/addfavorite.gif"
            ID="btnAddFavoriteCurrent" runat="server" AlternateText="Add to Favorite" OnClick="btnFavorite_Click" />
        &nbsp;&nbsp;&nbsp;<asp:ImageButton CssClass="img_icon" ImageUrl="~/ProUI/images/recommend.gif"
            ID="btnForward" runat="server" AlternateText="Forward to mentor" OnClick="btnForward_Click" />
        &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btnEditApplicant" ImageUrl="~/ProUI/images/edit.jpg"
            CssClass="img_icon" runat="server" AlternateText="Edit Applicant" Visible="false"
            OnClick="btnEditApplicant_Click" />
    </div>
    <asp:HiddenField ID="activeTab" runat="server" />
    <ajaxToolkit:TabContainer runat="server" ID="Tabs" OnClientActiveTabChanged="ActiveTabChanged">
        <ajaxToolkit:TabPanel ID="tabBasicInfo" runat="server" HeaderText="Applicant's Information">
            <ContentTemplate>
                <uc1:ApplicantInfoPanel ID="ApplicantInfoPanel1" runat="server" />
                <div>
                    <Springfield:CommentList runat="server" ID="commentList" />
                </div>
                <h3>
                </h3>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="tabInterviewHistory" runat="server" HeaderText="Interview History">
            <ContentTemplate>
                <asp:DropDownList ID="ddlInterviews" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInterviews_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:PlaceHolder ID="phInterview" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="tabPerformanceAssessment" runat="server" HeaderText="PerFormance Assessment">
            <ContentTemplate>
                <asp:PlaceHolder ID="phPerformanceAssessment" runat="server">
                 <uc10:PerformanceAssessment ID="ucPerformanceAssessment" runat="server" />
                </asp:PlaceHolder>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
   
</asp:Content>
