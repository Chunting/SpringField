<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MentorResult.ascx.cs"
    Inherits="Controls_MentorResult" %>
<%@ Register Src="CheckInFormEdit.ascx" TagName="CheckInFormEdit" TagPrefix="uc2" %>
<%@ Register Src="CheckInFormView.ascx" TagName="CheckInFormView" TagPrefix="uc1" %>

<div id="hm_result">
    <div class="panel_title_expand">
        Mentor's Result
    </div>
    <div class="panel_content">
        <table class="applicants_table" style="table-layout: fixed;">
            <tr>
                <td style="width: 30%;">
                    Time:
                </td>
                <td style="width: 70%;">
                    <asp:Label ID="lbHMDecisionTime" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%;">
                    Alias:
                </td>
                <td style="width: 70%;">
                    <asp:Label ID="lbHMAlias" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Suggestion:
                </td>
                <td>
                    <asp:Label ID="lbHMSuggestion" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr id="tr_ApprovalEmail" runat="server" visible="false">
                <td>
                    Approval Email:
                </td>
                <td>
                <a href="#" id="link_download" runat="server">Download</a> 
                </td>
            </tr>
            <tr>
                <td>
                    Comment:
                </td>
                <td style="line-height: 150%;">
                  <div style="max-height: 500px; overflow: auto;">
                    <asp:Label ID="lbHMComment" runat="server" Text=""></asp:Label>
                  </div>
                </td>
            </tr>
        </table>
    </div>
</div>
<uc1:CheckInFormView id="CheckInFormView1" runat="server">
</uc1:CheckInFormView>
<uc2:CheckInFormEdit ID="CheckInFormEdit1" runat="server" />
<asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit" Width="73px" />
<asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" ValidationGroup="Applicant"/>
<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
