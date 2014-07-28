<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckInFormView.ascx.cs" Inherits="Controls_CheckInFormView" %>
    <div>
        <div class="panel_title_expand">MSRA New Intern On-board Request</div>
        <div class="panel_content">
        <table class="applicants_table" style="table-layout: fixed;">
        <tr>
       <td width="30%">Intern's group</td>
       <td width="70%">
        <asp:Label ID="lblGroup" runat="server" Text="Label"></asp:Label>
        </td>
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
        <tr>
        <td>Preffered check-in date
        </td>
        <td>
        <asp:Label ID="lblPreferCheckInDay" runat="server" Text="Label"></asp:Label><br />
        </td>
        </tr>
        <tr>
        <td>
        Preferred last working day (at least three months)</td>
        <td>
        <asp:Label ID="lblPreferLastWorkingDay" runat="server" Text="Label"></asp:Label></td>
        <tr>
        <td>
        Has he/she got the approval letter from his/her advisor/university?
        </td>
        <td>
        <asp:Label ID="lblAdvisorApproval" runat="server" Text="Label"></asp:Label>
        </td>
        </tr>
        <%--
        <tr>
            <td>
            
        Enroll date in university</td>
            <td>
           
        <asp:Label ID="lblEnrollDate" runat="server" Text="Label"></asp:Label> </td>
        </tr>
        <tr>
            <td>
                Graduation date in university
            </td>
            <td>  
                <asp:Label ID="lblGraduateDate" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        --%>
         <tr>
       <td>Any special requirement?</td>
       <td><asp:Label ID="lblComments" runat="server" Text="Label"></asp:Label></td>
       </tr>
       </table>
       </div>
</div>