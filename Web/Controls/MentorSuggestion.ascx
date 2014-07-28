<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MentorSuggestion.ascx.cs"
    Inherits="Controls_MentorSuggestion" %>
<%@ Register Src="CheckInFormView.ascx" TagName="CheckInFormView" TagPrefix="uc2" %>
<%@ Register Src="CheckInFormEdit.ascx" TagName="CheckInFormEdit" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<script language="javascript">
function OpenArrangeInterview(applicantId)
{
    window.open('ArrangeInterview.aspx?applicant='+applicantId,null,'height=700,width=760,status=no,toolbar=no,menubar=no,location=no,resizable=no,scrollbars=yes');
}

</script>

<div id="Div2">
    <div class="panel_title_expand">
        Mentor Suggestion
    </div>
    <div class="panel_content">
        <table class="applicants_table">
            <tr>
                <td style="width: 30%;">
                    Suggestion:
                </td>
                <td style="width: 424px;">
                    <asp:DropDownList ID="ddlHMSuggestion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlHMSuggestion_SelectedIndexChanged">
                        <asp:ListItem Text="Final Reject" Value="-1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Decided to Hire" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Decline Offer" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <input type="button" value="Schedule New Interview" id="btnScheduleInterview" name="btnScheduleInterview"
                        runat="server" />
                </td>
            </tr>
            <asp:PlaceHolder ID="Panel2" runat="server">
                <tr>
                    <td>
                        Group Manager:
                    </td>
                    <td style="width: 424px">
                        <asp:DropDownList ID="ddlGroupManager" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </asp:PlaceHolder>
            <%--<asp:Panel ID="panGroup" runat="server" Visible="false">
                    <tr>
                        <td>
                            Group Manager:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGroupManager" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    </asp:Panel>    --%>
            <tr>
                <td>
                    Comment:
                </td>
                <td style="width: 424px">
                    <asp:TextBox ID="tbHMComment" runat="server" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <asp:PlaceHolder ID="Panel3" runat="server">
                <tr>
                    <td colspan="2">
                        <uc1:CheckInFormEdit ID="CheckInFormEdit1" runat="server" />
                        <uc2:CheckInFormView ID="CheckInFormView1" runat="server" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td colspan="2">
                    <div style="width: 50%; float:left">
                        <asp:Button ID="btnHMApproval" runat="server" Text=" Submit Request " OnClick="btnHMApproval_Click"
                            CausesValidation="false" OnClientClick="return ConfirmMentorSuggestion('Applicant');" />
                        <%--<asp:Button ID="btnHMApproval" runat="server" Text="Submit" OnClick="btnHMApproval_Click" CausesValidation="true"/>&nbsp;--%>
                    </div>
                    <div style="width: 50%; text-align:right">
                        <asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="Preview"
                            ValidationGroup="Applicant" />
                        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" />&nbsp;</div>
                </td>
            </tr>
        </table>
    </div>
</div>
<%--
<asp:Panel ID="Panel1" runat="server" Style="display: none; position:absolute;">
            <asp:Panel ID="panInfo" runat="server" CssClass="modalPopup">
                <div>
                    <p>
                    <asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
                        <asp:Literal ID="litHint" runat="server"></asp:Literal></ContentTemplate>

        </asp:UpdatePanel>Do you want to submit your decision?</p>
                    <p style="text-align: center;">
                        <asp:Button ID="OkButton" runat="server" Text="OK" OnClick="OkButton_Click" ValidationGroup="Applicant" />
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" />
                    </p>
                </div>
            </asp:Panel>
        </asp:Panel>
        
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server"
            TargetControlID="btnHMApproval"
            PopupControlID="Panel1"
            BackgroundCssClass="modalBackground"
            CancelControlID="CancelButton"
            DropShadow="true"
            Drag="true"
            PopupDragHandleControlID="panInfo" />
--%>
