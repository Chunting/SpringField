<%@ page language="C#" masterpagefile="~/SpringfieldMaster.master" autoeventwireup="true" inherits="HireReport_New, App_Web_wkngwpi-" %>

<%@ Register Src="Controls/HiringReport.ascx" TagName="HiringReport" TagPrefix="uc2" %>
<%@ Register Src="Controls/InterviewReport.ascx" TagName="InterviewReport" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="mainContent" runat="server">
    <div style="width: 100%">
        <div>
            <p style="font-size: 20px">
                Hiring Report</p>
            <p>
                <asp:Label ID="lbDateSpan" runat="server" Font-Bold="True"></asp:Label>
                &nbsp; &nbsp; &nbsp; &nbsp; <a href="Default.aspx" style="text-decoration: underline">
                    <b>Back to Home</b></a>&nbsp; &nbsp; &nbsp; &nbsp;<a href="reportgenerator.aspx"
                        style="text-decoration: underline"><b>Back to Reports</b></a>
            </p>
        </div>
        <div id="filter_title" class="panel_title_expand" onclick="ChangeStyle(this,'filter_content')">
            Filter Condition
        </div>
        <div id="filter_content" class="panel_content" style="display: block;">
            <table class="applicants_table">
                <tr>
                    <td>
                        <b>time frame</b></td>
                    <td>
                        Date from:
                        <asp:TextBox ID="tbStartDate" runat="server"></asp:TextBox>
                        &nbsp; &nbsp; &nbsp;<b>To : </b>
                        <asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox><br />
                        <asp:CompareValidator ID="cmpDate" runat="server" ControlToValidate="tbEndDate" ControlToCompare="tbStartDate"
                            Operator="GreaterThan" SetFocusOnError="true" ErrorMessage="End date must be greater than start date!"
                            Type="Date" Display="Dynamic" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Please select a report type:</b></td>
                    <td>
                        <asp:DropDownList ID="ddlReportType" runat="server">
                            <asp:ListItem Value="Mentor" Text="Interview Report By Mentor"></asp:ListItem>
                            <asp:ListItem Value="Group" Text="Hiring Report By Group"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" colspan="2">
                        <asp:Button ID="btnSubmit" Text="Generate" runat="server" OnClick="btnSubmit_Click" /></td>
                </tr>
            </table>
        </div>
        <hr class="split_line" />
        <div>
            <div id="content_title" class="panel_title_expand" onclick="ChangeStyle(this,'content_content')">
                Report
            </div>
            <div id="content_content" class="panel_content" style="display: block;">
                <asp:PlaceHolder ID="phReport" runat="server"></asp:PlaceHolder>
            </div>
        </div>
    </div>
</asp:Content>
