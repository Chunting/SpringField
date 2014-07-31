<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="MSRA.SpringField.Application.Controls.Controls_MentorResult" Codebehind="MentorResult.ascx.cs" %>
<%@ Register Src="CheckInFormEdit.ascx" TagName="CheckInFormEdit" TagPrefix="uc2" %>
<%@ Register Src="CheckInFormView.ascx" TagName="CheckInFormView" TagPrefix="uc1" %>

<div id="hm_result" style="margin:0 0 8 0">
    <div class="panel_title_expand">Mentor's Result</div>
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
                   HiringManager Alias:
                </td>
                <td style="width: 70%;">
                    <asp:Label ID="lbHMAlias" runat="server" Text=""></asp:Label>
                </td>
            </tr>
             <tr>
                <!--
                 * Modify Interview Process
                 * Author: Yin.P
                 * Date: 2010-1-5
                 -->
                <td style="width:30%">
                Mentor Alias:
                </td>
                <td style="width:70%">
                    <asp:Label ID="lblMentorAlias" runat="server"></asp:Label>
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
                    Feedback:
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
<uc1:CheckInFormView id="CheckInFormView1" runat="server"></uc1:CheckInFormView>
<uc2:CheckInFormEdit ID="CheckInFormEdit1" runat="server" />

<div style="text-align:center">
<table style="text-align:center;vertical-align:middle;padding:0 3 4 0;margin:0 2 4 0;">
    <tr>
        <td><asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit" ToolTip="Edit new intern onboard request" CausesValidation="false" Width="120px" /></td>
        <td><asp:Button ID="btnUpdate" runat="server" Text="Update" CausesValidation="false" ToolTip="Commit your modification of new intern onboard request" Width="120" OnClick="btnUpdate_Click" ValidationGroup="Applicant"/></td>
        <td><asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" Width="120" OnClick="btnCancel_Click" /></td>
    </tr>
</table>
</div>
<div style="height:20">&nbsp;</div>
