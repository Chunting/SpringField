<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Applicants.aspx.cs" Inherits="Default" MasterPageFile="~/SpringfieldMaster.master"%>

<%@ Register Src="Controls/ApplicantsList.ascx" TagName="ApplicantsList" TagPrefix="uc1" %>
<%@ Register TagPrefix="SFUserControl" TagName="CollegeSelector" Src="~/Controls/CollegeSelector.ascx" %>
<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="mainContent" runat="server">
<%--<ul>
    <li>
    You can click on the "Filter" bar to expand the panel, and set your preferred filters to search candidates.
    </li>
    <li>
    If you want to start the recruiting process, please click "Schedule Interview" icons.
    </li>
    <li>
    You can click "Add Favorite" icon to add current applicant to your favorite list.
    </li>
</ul>--%>
<div id="filter">
   <div id="filter_title" class="panel_title_expand" onclick="ChangeStyle(this,'filter_content')">
    Filter Condition
    </div>
    <div id="filter_content" class="panel_content" style="display:block;">
    <table class="applicants_table">
        <tr>
            <td class="bold_font">
            Status:
            </td>
            <td>         
            <asp:DropDownList ID="ddlStatus" runat="server" Width="100%">
                <asp:ListItem Text="All" Value=""></asp:ListItem>
                <asp:ListItem Text="Available" Value="1" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Waiting For Interview Feedback" Value="2"></asp:ListItem>
                <asp:ListItem Text="Waiting For Mentor Decision" Value="3"></asp:ListItem>
                <asp:ListItem Text="Waiting For Group Manager Decision" Value="4"></asp:ListItem>
                <asp:ListItem Text="Hired" Value="5"></asp:ListItem>
                <asp:ListItem Text="Rejected" Value="6"></asp:ListItem>
                <asp:ListItem Text="Offer Declined" Value="7"></asp:ListItem>
                <asp:ListItem Text="On Board" Value="8"></asp:ListItem>
            </asp:DropDownList>
            </td>
            <td class="bold_font">
                Major Category:
            </td>
            <td>
                <asp:DropDownList ID="ddlMajor" runat="server" Width="100%">
                    <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Computer & EE Related" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Automation Related" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Math Related" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Physical Related" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Liberal Related" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Other" Value="5"></asp:ListItem>
                </asp:DropDownList>
            </td>
         </tr>
         <tr>
            <td class="bold_font" style="height: 33px">
              Degree:
            </td>
            <td style="height: 33px">
              <asp:DropDownList ID="ddlDegree" runat="server" Width="100%">
                <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
                <asp:ListItem Text="BS" Value="0"></asp:ListItem>
                <asp:ListItem Text="MS" Value="1"></asp:ListItem>
                <asp:ListItem Text="PHD" Value="2"></asp:ListItem>
                <asp:ListItem Text="MBA" Value="3"></asp:ListItem>
                <asp:ListItem Text="Double BS" Value="4"></asp:ListItem>
                <asp:ListItem Text="MS + PhD" Value="5"></asp:ListItem>
                <asp:ListItem Text="Diploma" Value="6"></asp:ListItem>
                <asp:ListItem Text="Other" Value="7"></asp:ListItem>
              </asp:DropDownList>
            </td>
            <td class="bold_font" style="height: 33px">
              Interested Group:
            </td>
            <td>
              <asp:DropDownList ID="ddlGroup" runat="server" Width="100%" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true">
              </asp:DropDownList>
              <div runat="server" id="div_OtherGorup" visible="false">
              Please specify:<asp:TextBox ID="tb_OtherGorup" runat="server"></asp:TextBox>
              </div>
            </td>
          </tr>
        <tr>
            <td class="bold_font">
                Interested Area:
            </td>
            <td>
                <asp:TextBox ID="tbArea" runat="server"></asp:TextBox>
            </td>
            <td class="bold_font">Applicant Name:</td>
            <td><asp:TextBox ID="tbName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="bold_font">
                Search in Resume:
            </td>
            <td colspan="3">
                <asp:TextBox ID="tbResume" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td class="bold_font" style="height: 69px">University:</td>
        <td colspan="3" style="height: 69px"><SFUserControl:CollegeSelector ID="cs" runat="server" ValidationGroup="Applicant" /></td>
        </tr>
        
        <tr>
            <td colspan="4" style="text-align: center;">
                <asp:Button ID="btnApplyFilter" runat="server" Text="Apply Filter" OnClick="btnApplyFilter_Click" />
            </td>
        </tr>
    </table>
    </div>
</div>
<hr class="split_line" />
    <uc1:ApplicantsList ID="ApplicantsList1" runat="server" OnPagerClickChanged="ApplicantsList1_PagerClick" ListType="applicantlist" />

</asp:Content>