<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="MSRA.SpringField.Application.Controls.Controls_InterviewFeedback" Codebehind="InterviewFeedback.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<div>
    <div id="ch_title" class="panel_title_expand">
        Interview Feedback
    </div>
    <div id="ch_content" class="panel_content">
        <table class="applicants_table" width="100%">
            <tr>
                <td>
                    Suggestion:
                </td>
                <td>
                    <asp:DropDownList ID="ddlSuggestion" runat="server" Width="180">
                        <asp:ListItem Selected="true" Text="Suggest to Reject" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Suggest to Hire" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 323px">
                    Feedback:
                </td>
                <td style="height: 323px">
                    <asp:TextBox ID="tbFeedbackContent" runat="server" TextMode="MultiLine" Width="400"
                        Height="300" CssClass="required_input"></asp:TextBox><br />
                    <asp:Label ID="lbMessage" runat="server" Text="" ForeColor="red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnAddFeedback" Width="120" runat="server" Text="Submit Feedback" OnClick="btnAddFeedback_Click" CausesValidation="true" />&nbsp;&nbsp;
                    <input type="button" style="width:120" onclick="window.open('ChangeInterviewer.aspx?feedback=<%=FeedbackId %>','','height=700,width=760,status=no,toolbar=no,menubar=no,location=no,resizable=no,scrollbars=yes')" value="Change Interviewer" />&nbsp;&nbsp;
                    <input type="button" style="width:120" value="Close" onclick="window.close();" />&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="Panel1" runat="server" Style="display: none">
            <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup">
                <div>
                    <p>
                        you want to submit your feedback about "<asp:Literal ID="litHint" runat="server"></asp:Literal>", do you confirm to do this?</p>
                    <p style="text-align: center;">
                        <asp:Button ID="OkButton" runat="server" Text="OK" OnClick="OkButton_Click" />
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" />
                    </p>
                </div>
            </asp:Panel>
        </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server"
            TargetControlID="btnAddFeedback"
            PopupControlID="Panel1"
            BackgroundCssClass="modalBackground"
            CancelControlID="CancelButton"
            DropShadow="true" 
            Drag="true"
            PopupDragHandleControlID="Panel3"/>
</div>
