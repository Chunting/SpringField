<%@ Control Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Controls.Controls_InterviewPanel" Codebehind="InterviewPanel.ascx.cs" %>
<%@ Register Src="UploadingMentorSuggestion.ascx" TagName="UploadingMentorSuggestion"
    TagPrefix="uc9" %>
<%@ Register Src="UploadingManagerApproval.ascx" TagName="UploadingManagerApproval"
    TagPrefix="uc1" %>
<%@ Register Src="ManagerApproval.ascx" TagName="ManagerApproval" TagPrefix="uc7" %>
<%@ Register Src="InterviewFeedback.ascx" TagName="InterviewFeedback" TagPrefix="uc8" %>
<%@ Register Src="MentorSuggestion.ascx" TagName="MentorSuggestion" TagPrefix="uc2" %>
<%@ Register Src="MentorResult.ascx" TagName="MentorResult" TagPrefix="uc3" %>
<%@ Register Src="ManagerResult.ascx" TagName="ManagerResult" TagPrefix="uc4" %>
<%@ Register Src="InterviewHistory.ascx" TagName="InterviewHistory" TagPrefix="uc5" %>
<%@ Register Src="IncompleteInterviewList.ascx" TagName="IncompleteInterviewList"
    TagPrefix="uc6" %>
    
    <div style="border-style:solid; border-color:Red; border-width:2px;position:relative">
        <asp:Label ID="lbVersion" runat="server" Text="" ForeColor="red" Font-Bold="true"></asp:Label>
            
            <asp:Literal ID="litTips" runat="server"></asp:Literal>
                <asp:PlaceHolder ID="phInterview" runat="server"></asp:PlaceHolder>
                <asp:Panel ID="pMessage" runat="server" Visible="false">
                    <div id="ch_title" class="panel_title_expand">
                        Message
                    </div>
                    <div id="ch_content" class="panel_content">
                        <asp:Literal ID="litMessage" runat="server"></asp:Literal>
                    </div>
                </asp:Panel>
    </div>

