<%@ control language="C#" autoeventwireup="true" inherits="Controls_InterviewFeedback, App_Web_eecyfkj6" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<div>
    <div id="ch_title" class="panel_title_expand">
        Interview Feedback
    </div>
    <div id="ch_content" class="panel_content">
        <table class="applicants_table">
            <tr>
                <td>
                    Suggestion:
                </td>
                <td>
                    <asp:DropDownList ID="ddlSuggestion" runat="server">
                        <asp:ListItem Selected="true" Text="Suggest to Reject" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Suggest to Hire" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 323px">
                    Comment:
                </td>
                <td style="height: 323px">
                    <asp:TextBox ID="tbFeedbackContent" runat="server" TextMode="MultiLine" Width="400"
                        Height="300" CssClass="required_input"></asp:TextBox><br />
                    <asp:Label ID="lbMessage" runat="server" Text="" ForeColor="red"></asp:Label>
                    <%--<asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="tbFeedbackContent"
                        ErrorMessage="Comment is required"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <%--                <asp:Button ID="btnAddFeedback" runat="server" Text="Submit Feedback" OnClick="btnAddFeedback_Click" OnClientClick="return ConfirmFeedback();" CausesValidation="true" />
--%>
                    <asp:Button ID="btnAddFeedback" runat="server" Text="Submit Feedback" OnClick="btnAddFeedback_Click"
                        CausesValidation="true" />
                    <asp:Button ID="btnChangeInterviewer" runat="server" Text="Chanage Interviewer" OnClick="btnChangeInterviewer_Click"
                        CausesValidation="false" />
                    <input type="button" value="Close" onclick="window.close();" />
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
