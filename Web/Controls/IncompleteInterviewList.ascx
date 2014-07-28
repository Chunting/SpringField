<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IncompleteInterviewList.ascx.cs"
    Inherits="Controls_IncompleteInterviewList" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="panAll" runat="server">
<div id="incomplete_interview">
    <div class="panel_title_expand">
        Incomplete Interview List
    </div>
    <div class="panel_content">
        <asp:DataList ID="dlIncompleteFeedback" runat="server" RepeatLayout="Flow" OnItemCommand="dlIncompleteFeedback_ItemCommand">
            <ItemTemplate>
                <table class="applicants_table">
                    <tr>
                        <td style="width: 30%;">
                            Due Date:
                        </td>
                        <td style="width: 70%">
                            <%# DataBinder.Eval(Container.DataItem, "DueDate") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Interviewer:
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "InterviewerAlias") %>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:HyperLink ID="lnkChangeInterviewer" runat="server" Target="_blank" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "FeedbackId", "~/ChangeInterviewer.aspx?feedback={0}") %>'>Change Interviewer</asp:HyperLink>&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbtnCancelInterview" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "FeedbackId") %>'
                                CommandName="CancelInterview">Cancel Interview</asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Panel1" runat="server" Style="display: none">
            <asp:Panel ID="panInfo" runat="server" CssClass="modalPopup">
                <div>
                    <p>Do you want to cancel this interview?</p>
                    <p style="text-align: center;">
                        <asp:Button ID="OkButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "FeedbackId") %>' runat="server" Text="OK" OnCommand="OkButton_Click" />
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" />
                    </p>
                </div>
            </asp:Panel>
        </asp:Panel>
              <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server"
            TargetControlID="lbtnCancelInterview" 
            PopupControlID="Panel1"
            BackgroundCssClass="modalBackground"
            CancelControlID="CancelButton"
            DropShadow="true"
            Drag="true"
            PopupDragHandleControlID="panInfo"/>
            </ItemTemplate>
        </asp:DataList>
    </div>
</div>

</asp:Panel>