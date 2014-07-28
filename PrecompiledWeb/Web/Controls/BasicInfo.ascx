<%@ control language="C#" autoeventwireup="true" inherits="Controls_BasicInfo, App_Web_eecyfkj6" %>

<div>
<div id="ch_title" class="panel_title_expand">
    Applicant Basic Information
</div>
<div id="ch_content" class="panel_content">

<table class="applicants_table" style="text-align:left;">
<tr>
    <td width="15%">
        Applicant Name:
    </td>
    <td width="25%">
        <asp:HyperLink ID="lnkApplicantName" runat="server" Target="_blank"></asp:HyperLink>
    </td>
    <td width="10%">
        Degree:
    </td>
    <td>
        <asp:Label ID="lbDegree" runat="server" Text=""></asp:Label>
    </td>
</tr>
<tr>
    <td>
        Interest Area:
    </td>
    <td>
        <asp:Label ID="lbArea" runat="server" Text=""></asp:Label>
    </td>
    <td>
        School:
    </td>
    <td>
        <asp:Label ID="lbSchool" runat="server" Text=""></asp:Label>
    </td>

</tr>
<tr>
    <td>
        Major:
    </td>
    <td>
        <asp:Label ID="lbMajor" runat="server" Text=""></asp:Label>
    </td>
    <td>
        Resume:
    </td>
    <td>
        <asp:HyperLink ID="lnkResume" runat="server"></asp:HyperLink>
    </td>
</tr>
<tr>
    <td>
        Paper:
    </td>
    <td>
        <asp:HyperLink ID="lnkPaperA" runat="server"></asp:HyperLink>
        <asp:HyperLink ID="lnkPaperB" runat="server"></asp:HyperLink>
    </td>
    <td>
    Apply Date:
    </td>
    <td>
        <asp:Label ID="lbApplyDate" runat="server" Text=""></asp:Label>
    </td>
</tr>
<tr>
    <td>
        Interview Status:
    </td>
    <td>
        <asp:Label ID="lbApplicant" runat="server" ForeColor="red" Text="Not Started"></asp:Label>
    </td>
    <td colspan="1">
        <asp:Label ID="lbNewHirePackage" runat="server" Text="New Hire Package"></asp:Label>
    </td>
    <td colspan="1">
        <asp:HyperLink ID="hlNewHirePackage" Text="Not Available" runat="server"></asp:HyperLink>
    </td>

   <%-- <td>
    Need to do:
    </td>
    <td>
        <asp:Label ID="lbNeedToDo" runat="server" Text="" ForeColor="red"></asp:Label>
    </td>--%>
</tr>
<tr id="trPAURL" runat="server" visible="false">
<td>Performance Assessment Web<br /> Address for Intern:</td>
<td colspan="3"><asp:Label ID="lbPAURL" runat="server"></asp:Label></td>
</tr>
</table>

</div>
</div>