<%@ Control Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Controls.Controls_CheckInFormView" Codebehind="CheckInFormView.ascx.cs" %>
<div>
    <div class="panel_title_expand">MSRA New Intern On-board Request</div>
    <div class="panel_content">
        <table class="applicants_table" style="table-layout: fixed;">
            <tr>
                <td Style="width: 20%">Intern's group</td>
                <td Style="width: 80%">
                    <asp:Label ID="lblGroup" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Intern's project</td>
                <td>
                    <asp:Label ID="lblProject" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Intern's position</td>
                <td>
                    <asp:Label ID="lblPosition" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>His/Her Intern Type</td>
                <td>
                    <asp:Label ID="lblInternType" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>His/her Mentor</td>
                <td>
                    <asp:Label ID="lblMentor" runat="server" Text="Label"></asp:Label> (email alias)
                </td>
            </tr>
            <tr>
                <td>Preffered check-in date</td>
                <td>
                    <asp:Label ID="lblPreferCheckInDay" runat="server" Text="Label"></asp:Label><br />
                </td>
            </tr>
            <tr>
                <td>Preferred last working day (at least three months)</td>
                <td>
                    <asp:Label ID="lblPreferLastWorkingDay" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Has he/she got the approval letter from his/her advisor/university?</td>
                <td>
                    <asp:Label ID="lblAdvisorApproval" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Any special requirement?</td>
                <td><asp:Label ID="lblComments" runat="server" Text="Label"></asp:Label></td>
            </tr>
       </table>
    </div>
</div>