<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="MSRA.SpringField.Application.Controls.Controls_ManagerApproval" Codebehind="ManagerApproval.ascx.cs" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    <div id="Div3" style="width:100%">
    <div class="panel_title_expand">
        Group Manager Approval
    </div>
    <div class="panel_content">
        <table class="applicants_table" style="width:100%">
            <tr>
                <td style="width: 30%;">
                    Approval:
                </td>
                <td style="width: 70%;">
                    <asp:DropDownList Width="150" ID="ddlGMSuggestion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGMSuggestion_SelectedIndexChanged">
                        <asp:ListItem Text="Qualified But Not Matched" Value="2" Selected="True"></asp:ListItem> 
                        <asp:ListItem Text="Final Reject" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Approve to Hire" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Decline Offer" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Feedback:
                </td>
                <td>
                    <asp:TextBox ID="tbGMComment" runat="server" Columns="50" Rows="5" TextMode="MultiLine"
                        CssClass="required_input"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <%--<asp:Button ID="btnGMApproval" runat="server" Text="Approve" OnClick="btnGMApproval_Click" OnClientClick="return ConfirmApproval();" />--%>
                    <asp:Button ID="btnGMApproval" Width="120" CausesValidation="false" runat="server" Text="Submit" OnClick="btnGMApproval_Click" />&nbsp;&nbsp;
                    
                </td>
            </tr>
        </table>
    </div>
</div>
    <asp:Panel ID="Panel1" runat="server" Style="display: none">
            <asp:Panel ID="panInfo" runat="server" CssClass="modalPopup">
                <div>
                    <p><asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
                        <asp:Literal ID="litHint" runat="server"></asp:Literal></ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlGMSuggestion" EventName="SelectedIndexChanged" />
        </Triggers>
        </asp:UpdatePanel> Do you want to submit your decision?</p>
                    <p style="text-align: center;">
                        <asp:Button ID="OkButton" runat="server" Width="100" Text="OK" CausesValidation="false" OnClick="OkButton_Click" />
                        <asp:Button ID="CancelButton" runat="server" Width="100" Text="Cancel" />
                    </p>
                </div>
            </asp:Panel>
        </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server"
            TargetControlID="btnGMApproval"
            PopupControlID="Panel1"
            BackgroundCssClass="modalBackground"
            CancelControlID="CancelButton"
            DropShadow="true"
            Drag="true"
            PopupDragHandleControlID="panInfo"/>
