<%@ Page Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.HireReport_New" EnableEventValidation="false" Codebehind="HireReport_New.aspx.cs" %>

<%@ Register Src="~/Controls/HiringReport.ascx" TagName="HiringReport" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/InterviewReport.ascx" TagName="InterviewReport" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/OnBoardReport.ascx" TagName="OnBoardReport" TagPrefix="uc3" %>
<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="mainContent" runat="server">
<script language="JavaScript" src="../../Resource/Scripts/popcalendar.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Resource/Scripts/master.js"></script>
    <div style="width: 100%">
        
        <div>
            <p style="font-size: 20px">
                Hiring Report</p>
            
                <asp:Label ID="lbDateSpan" runat="server" Font-Bold="True"></asp:Label>
                
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
                    <asp:ImageButton Width="24" Height="24" ImageAlign="AbsMiddle" ImageUrl="~/Resource/Images/export_excel.png" ID="expExcel" runat="server" Font-Bold="true" OnClick="btnExportExcel_Click" />
                    <label style="cursor:hand" for="<%=expExcel.ClientID %>"><b>Export to Excel</b></label>                    
                    </td>
            </tr>
        </table>
        </div>
        </div> 
        
        <div id="filter_title" class="panel_title_expand" onclick="ChangeStyle(this,'filter_content')">
            Filter Condition
        </div>
        <div id="filter_content" class="panel_content" style="display: block;border-top:none;border-bottom:none">
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
                            <asp:ListItem Value="IsOffline" Text="On Board Report By IsOffline"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;height:30px" colspan="2" >
                        <div style="width:120px;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                     <asp:ImageButton Width="24" Height="24" CssClass="img_icon" ImageUrl="~/Resource/Images/generate.png" ID="btnSubmit"
                            runat="server" AlternateText="Generate" OnClick="btnSubmit_Click" 
                            CausesValidation="true" ImageAlign="AbsMiddle" />
                            <label for="<%=btnSubmit.ClientID %>"><span style="cursor:hand">Generate</span></label>
                </div></td>
                </tr>
            </table>
        </div>
       <%-- <hr class="split_line" />--%>
       <br />
        <div>
            <div id="content_title" class="panel_title_expand" onclick="ChangeStyle(this,'content_content')">
                Hiring Report
            </div>
            <div id="content_content" class="panel_content" style="display: block;">
                <asp:PlaceHolder ID="phReport" runat="server"></asp:PlaceHolder>
            </div>
        </div>
    </div>
</asp:Content>
