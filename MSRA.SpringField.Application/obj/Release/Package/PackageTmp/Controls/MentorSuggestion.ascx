<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="MSRA.SpringField.Application.Controls.Controls_MentorSuggestion" Codebehind="MentorSuggestion.ascx.cs" %>
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
        <table class="applicants_table" width="100%">
            <tr>
                <td style="width: 30%;">
                    Suggestion:
                </td>
                <td style="width: 424px;padding:3 3" valign="middle">
                    <asp:DropDownList ID="ddlHMSuggestion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlHMSuggestion_SelectedIndexChanged">
                        <asp:ListItem Text="Qualified But Not Matched" Value="2" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Final Reject" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Decided to Hire" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Decline Offer" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
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
                    Feedback:
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
                <td colspan="2" align="center">
                    <div style="width: 100%;margin:2 2;padding:3 3;vertical-align:middle; text-align:center">
                        <asp:Button Width="120" ID="btnHMApproval" runat="server" Text=" Submit Request " OnClick="btnHMApproval_Click" CausesValidation="false" OnClientClick="return ConfirmMentorSuggestion('Applicant');" />&nbsp;&nbsp;
                        <asp:Button Width="120" ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="Preview" ValidationGroup="Applicant" />&nbsp;&nbsp;
                        <asp:Button Width="120" ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       
                    </div>
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