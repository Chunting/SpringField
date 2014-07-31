<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="MSRA.SpringField.Application.ReportGenerator" MasterPageFile="~/SpringfieldMaster.master" Codebehind="ReportGenerator.aspx.cs" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="cntReportGenerator" runat="server">
   
     <div id="notionbar" style="background-color:#FFFFE1;border:solid 1px #FEC951;height:50;vertical-align:middle;margin:5 0;padding:3 0;text-align:left">
            <table style="width:100%;height:100%">
                <tr>
                    <td align="left" valign="middle">
                       <ul>
                        <li>Choose start date, end date, and Report Type, click "Generate", then get your report.
                        </li>
                        <li>If you don't select the date, Springfield will generate the last 3 months' report.
                        </li>
                        <li>All reports will be cached in 20 minutes, after that you have to generate report
                            again. </li>
                    </ul>  
                    </td>
                </tr>
            </table>                
            </div>
    
    <div style="width: 100%;">
        <div id="content_title" class="panel_title_expand">
            Report Generator
        </div>
        <div id="content_content" class="panel_content">
            <table class="applicants_table">
                <tr> 
                    <td class="bold_font" width="20%">
                        Start Date:
                    </td>
                    <td>
                        <asp:TextBox ID="tbStartTime" runat="server"></asp:TextBox>
                        <input type="button" value="Select" id="btnSelectStartDate" name="btnSelectStartDate"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="bold_font">
                        End Date:
                    </td>
                    <td>
                        <asp:TextBox ID="tbEndTime" runat="server"></asp:TextBox>
                        <input type="button" value="Select" id="btnSelectEndDate" name="btnSelectEndDate"
                            runat="server" />
                        <asp:CompareValidator ID="cmpDate" runat="server" ControlToValidate="tbEndTime" ControlToCompare="tbStartTime"
                            Operator="GreaterThan" SetFocusOnError="true" ErrorMessage="End date must be greater than start date!"
                            Type="Date" /></td>
                </tr>
                <tr>
                    <td>
                        Report Type:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReportType" runat="server">
                            <asp:ListItem Text="New Candidates Report"></asp:ListItem>
                            <asp:ListItem Text="Sourcing Report"></asp:ListItem>
                            <asp:ListItem Text="Hiring Report"></asp:ListItem>
                            <asp:ListItem Text="PA Report"></asp:ListItem>
                            <asp:ListItem Text="PA Report for HR"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
