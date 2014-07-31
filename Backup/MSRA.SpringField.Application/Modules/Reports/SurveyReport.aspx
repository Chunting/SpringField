<%@ Page Title="" Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" CodeBehind="SurveyReport.aspx.cs" Inherits="MSRA.SpringField.Application.SurveyReport1" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="mainContent" runat="server">

    <script language="JavaScript" src="../../Resource/Scripts/popcalendar.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Resource/Scripts/master.js"></script>
    <asp:Label Visible="false" ID="lbDateSpan" runat="server" Font-Bold="True"></asp:Label>
    <div class="toolbar"> 
        <table style="height:100%" cellpadding="0" cellspacing="0">
            <tr>
                <td onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';" 
                    style="padding:0 10;height:30;"><a href="../../Default.aspx" style="text-decoration: none">
                    <img src="../../Resource/Images/backhome.png" width="24" height="24" align="absmiddle" />
                    <b>Back to Home</b></a></td>
                <td onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';"
                    style="padding:0 10;height:30;">
                    <asp:ImageButton Width="24" Height="24" ImageAlign="AbsMiddle" ImageUrl="~/Resource/Images/export_excel.png" OnClick="Export_Click" ID="expExcel" runat="server" Font-Bold="true"/>
                    <label style="cursor:hand" for="<%=expExcel.ClientID %>"><b>Export to Excel</b></label>                    
                    </td>
            </tr>
        </table>
        </div>       
    
    <div id="filter_title" class="panel_title_expand" onclick="ChangeStyle(this,'filter_content')">Filter Condition</div>
    <div id="filter_content" class="panel_content" style="display: block;border-left:solid 1px #999;border-right:solid 1px #999">
    <table style="width:100%">
        <tr>
            <td align="right">Checkout Date: </td>
            <td>
                <asp:TextBox ID="tbStartDate" runat="server"></asp:TextBox>&nbsp;&nbsp;To
                <asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox><br/>
                <asp:CompareValidator ID="cmpDate" runat="server" ControlToValidate="tbEndDate" ControlToCompare="tbStartDate"
        Operator="GreaterThan" SetFocusOnError="true" ErrorMessage="End date must be greater than start date!"
        Type="Date" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td align="right">Group: </td>
            <td colspan ="3">
                <asp:DropDownList ID="ddlGroup" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">InternshipDuration: </td>
            <td colspan ="3">
                <asp:DropDownList ID="ddlTimeSpan" runat="server" Width="">
                    <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="0 - 3M" Value="1"></asp:ListItem>
                    <asp:ListItem Text="4 - 6M" Value="2"></asp:ListItem>
                    <asp:ListItem Text="7 - 12M" Value="3"></asp:ListItem>
                    <asp:ListItem Text="One year above" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </td>   
        </tr>
      <%--  <tr>
            <td align="right" style="width:150px; height: 29px;">Report Type:</td>
            <td colspan="3" style="height: 29px">
                    <asp:DropDownList ID="ddlReportType" runat="server">
                        <asp:ListItem Text="Survey Report - by OverView" Value="OverAllView"></asp:ListItem>
                        <asp:ListItem Text="Survey Report - by Group" Value="Group"></asp:ListItem>
                        <asp:ListItem Text="Survey Report - by Duration" Value="Duration"></asp:ListItem>
                    </asp:DropDownList>
            </td>
        </tr>--%>
        <tr>
            <td colspan="2" align="center" style="height:30px">
                <div style="width:120px;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                     <asp:ImageButton Width="24" Height="24" CssClass="img_icon" ImageUrl="~/Resource/Images/generate.png" ID="btnSubmit"
                            runat="server" AlternateText="Generate" OnClick="btnSubmit_Click" 
                            CausesValidation="true" ImageAlign="AbsMiddle" />
                            <label for="<%=btnSubmit.ClientID %>"><span style="cursor:hand">Generate</span></label>
                </div> 
           </td>
        </tr>
    </table>
    </div>
    
    <div>
    <br />
        <b><asp:HyperLink ID="lnkSurveyComments" Target="_blank" NavigateUrl="~/Modules/Reports/AllCommentsReport.aspx?" runat="server" style="text-decoration:underline;">
        <span id="item_lnkSurveyComments">All Comments</span></asp:HyperLink></b>
    </div>
    
    <asp:PlaceHolder ID="phReport" runat="server"></asp:PlaceHolder>
    
    <br />
    
</asp:Content>
