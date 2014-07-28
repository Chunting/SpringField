<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="MSRA.SpringField.Application.ShowApplication" MasterPageFile="~/SpringfieldMaster.master" EnableEventValidation="true" Codebehind="ShowApplication.aspx.cs" %>

<%@ Register Src="Controls/CheckoutSurvey.ascx" TagName="CheckoutSurvey"
    TagPrefix="uc11" %>
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
        if (activeTab != null) {
            activeTab.value = sender.get_activeTabIndex();
            if (document.getElementById('<%=approvaltools.ClientID %>') != null) {
                if (activeTab.value == 2) {
                    document.getElementById('<%=approvaltools.ClientID %>').style.display = "block";
                }
                else {
                    document.getElementById('<%=approvaltools.ClientID %>').style.display = "none";
                }
            }
        }
    }
    </script>
    
    <div>
        <Springfield:BasicInfo runat="server" ID="basicInfo" />
    </div>
    
    
    <div class="toolbar">
        <table style="height:100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="padding:0 10;height:30;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/schedule.png" ID="btnInterviewCurrent"
                        runat="server" AlternateText="Schedule Interview" OnClick="btnInterview_Click" ImageAlign="AbsMiddle" />
                        <label for="<%=btnInterviewCurrent.ClientID %>"><span style="cursor:hand">Schedule Interview</span></label>
                </td>
                <td>&nbsp;</td>
                <td style="padding:0 10;height:30;" 
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/addtofavorite.png"
                        ID="btnAddFavoriteCurrent" runat="server" AlternateText="Add to Favorite" OnClick="btnFavorite_Click" ImageAlign="AbsMiddle"/>
                         <label for="<%=btnAddFavoriteCurrent.ClientID %>"><span style="cursor:hand">Add To Favorite</span></label>
                </td>
                <td>&nbsp;</td>
                <td style="padding:0 10;height:30;" 
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">                    
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/forward.png"
                        ID="btnForward" runat="server" AlternateText="Forward to mentor" OnClick="btnForward_Click" ImageAlign="AbsMiddle"/>
                        <label for="<%=btnForward.ClientID %>"><span style="cursor:hand">Forward To Mentor</span></label>
                </td>
                <td>&nbsp;</td>
                <td style="padding:0 10;height:30;" 
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';" runat="server" id="btnEditApplicant">
                    <asp:ImageButton Width="28" Height="28" ID="btnEA" ImageUrl="~/Resource/Images/edit_applicant.png"
                        CssClass="img_icon" runat="server" AlternateText="Edit Applicant" Visible="true" OnClick="btnEditApplicant_Click"  ImageAlign="AbsMiddle" />
                        <label for="<%=btnEA.ClientID %>"><span style="cursor:hand;">Edit Applicant</span></label>
                </td>

                <td>&nbsp;</td>
                <!--
                 * Hire Action for incruiter 
                 * Author: Yuanqin
                 * Date: 2011.3.8
                 -->
                <td style="padding:0 10;height:30;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';" runat="server" id="btnOnBoardAction">
                    <asp:ImageButton Width="28" Height="28" ID="btnHA" ImageUrl="~/Resource/Images/approval.png"
                        CssClass="img_icon" runat="server" AlternateText="Hire Action" Visible="true" OnClick="btnOnBoardAction_Click" ImageAlign="AbsMiddle" />
                        <label for="<%=btnOnBoardAction.ClientID %>"><span style="cursor:hand;">Hired Action</span></label>
                </td>
                <td style="padding:0 10;height:30;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';" runat="server" id="Td1">
                    <asp:ImageButton Width="28" Height="28" ID="ImageButton1" ImageUrl="~/Resource/Images/save.png"
                        CssClass="img_icon" runat="server" AlternateText="Hire Action" Visible="true" OnClick="GoToShowPA" ImageAlign="AbsMiddle" />
                        <label><span style="cursor:hand;"><a style="color: black;" target=_blank href="<% Response.Write("ShowPA.aspx?applicant=" + Request["applicant"] + "&paid=" + Request.QueryString["paid"]); %>">Output PA</a></span></label>
                    <%--<asp:Button ID="Button1" runat="server" Text="Output PA" OnClick="GoToShowPA" />--%>
                </td>
            </tr>
        </table>        
    </div>
    
    <asp:HiddenField ID="activeTab" runat="server" />
    <ajaxToolkit:TabContainer runat="server" ID="Tabs"
        OnClientActiveTabChanged="ActiveTabChanged" ActiveTabIndex="3">
        <ajaxToolkit:TabPanel ID="tabBasicInfo" Height="20" Width="120" runat="server" HeaderText="Applicant's Information">
            <ContentTemplate>
                <uc1:ApplicantInfoPanel ID="ApplicantInfoPanel1" runat="server" />
                <div><Springfield:CommentList runat="server" ID="commentList" /></div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="tabInterviewHistory" Height="20" runat="server" HeaderText="Interview History">
            <ContentTemplate>
                <asp:DropDownList ID="ddlInterviews" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInterviews_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:PlaceHolder ID="phInterview" runat="server"></asp:PlaceHolder>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="tabPerformanceAssessment" Height="20" runat="server" HeaderText="Performance Assessment">
            <ContentTemplate>
                <asp:PlaceHolder ID="phPerformanceAssessment" runat="server">
                 <uc10:PerformanceAssessment ID="ucPerformanceAssessment" runat="server" />
                </asp:PlaceHolder>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel ID="tabCheckoutSurvey" Height="20" runat="server" HeaderText="Checkout Survey">
            <ContentTemplate>
                <asp:PlaceHolder ID="phCheckoutSurvey" runat="server">
                 <uc11:CheckoutSurvey ID="ucCheckoutSurvey" runat="server" />
                </asp:PlaceHolder>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
    <script type="text/javascript">
    
    if (document.getElementById("__tab_ctl00_mainPlaceHolder_Tabs_tabBasicInfo") != null)
    {
        document.getElementById("__tab_ctl00_mainPlaceHolder_Tabs_tabBasicInfo").style.height = "20";
    }

    if (document.getElementById("__tab_ctl00_mainPlaceHolder_Tabs_tabInterviewHistory") != null) 
    {
        document.getElementById("__tab_ctl00_mainPlaceHolder_Tabs_tabInterviewHistory").style.height = "20";
    }

    if (document.getElementById("__tab_ctl00_mainPlaceHolder_Tabs_tabPerformanceAssessment") != null) 
    {
        document.getElementById("__tab_ctl00_mainPlaceHolder_Tabs_tabPerformanceAssessment").style.height = "20";
    }
    if (document.getElementById("__tab_ctl00_mainPlaceHolder_Tabs_tabCheckoutSurvey") != null) {
        document.getElementById("__tab_ctl00_mainPlaceHolder_Tabs_tabCheckoutSurvey").style.height = "20";
    }           
</script>

   <%--<asp:Panel ID="Panel1" runat="server" Style="display: none">
        <asp:Panel ID="panInfo" runat="server" CssClass="modalPopup">
            <table style="width:100%;height:100%">
                <tr>
                    <td colspan="3"><p>Do you want to submit your approval?</p></td>
                </tr>
                <tr>
                <td align="center">
                    <table>
                        <tr>
                            <td><asp:Button ID="Pass" runat="server" Text="Pass" OnClick="Pass_Click" /></td>
                            <td><asp:Button ID="Reject" runat="server" Text="Reject" OnClick="Reject_Click" /></td>
                            <td><asp:Button ID="Invalid" runat="server" Text="Invalid" OnClick="Invalid_Click" /></td>
                            <td><asp:Button ID="CancelButton" runat="server" Text="Cancel" /></td>
                        </tr>
                    </table>
                </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>--%>
    
   <%-- <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server"
            TargetControlID="ImageButton1"
            PopupControlID="Panel1"
            BackgroundCssClass="modalBackground"
            CancelControlID="CancelButton"
            DropShadow="true"
            Drag="true"
            PopupDragHandleControlID="panInfo"/>--%>
            
<div class="toolbar" runat="server" id="approvaltools">
        <table style="height:100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="padding:0 10;height:30;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/schedule.png" ID="ImageButton2"
                        runat="server" AlternateText="Pass" OnClick="Pass_Click" ImageAlign="AbsMiddle" />
                        <label for="<%=ImageButton2.ClientID %>"><span style="cursor:hand">Pass</span></label>
                </td>
                <td>&nbsp;</td><td style="padding:0 10;height:30;" 
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/addtofavorite.png"
                        ID="ImageButton3" runat="server" AlternateText="Reject" OnClick="Reject_Click" ImageAlign="AbsMiddle"/>
                         <label for="<%=ImageButton3.ClientID %>"><span style="cursor:hand">Reject</span></label>
                </td>
                <td>&nbsp;</td><td style="padding:0 10;height:30;" 
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">                    
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/forward.png"
                        ID="ImageButton4" runat="server" AlternateText="Invalid" OnClick="Invalid_Click" ImageAlign="AbsMiddle"/>
                        <label for="<%=ImageButton4.ClientID %>"><span style="cursor:hand">Invalid</span></label>
                </td>
            </tr>
        </table>        
    </div>
</asp:Content>
