<%@ Page Title="" Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" CodeBehind="PaperReportForm.aspx.cs" Inherits="MSRA.SpringField.Application.Modules.Reports.PaperReportForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" runat="server">
  <script language="JavaScript" src="../../Resource/Scripts/popcalendar.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Resource/Scripts/master.js"></script>
    
<div>
<p style="font-size: 18px">Paper Report</p>
<table style="width:100%;border:solid 1px #888; padding-left:5px;padding-bottom:0px" cellpadding="0" cellspacing="0" border="0">
<tr style="line-height:35px;height:35px;">
        <td style="width:120px;border-bottom:solid 1px #888;border-right:solid 1px #888">Name: </td>
        <td align="left" style="border-bottom:solid 1px #888;">
            <asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr style="line-height:35px;height:35px;">
        <td style="border-bottom:solid 1px #888;border-right:solid 1px #888">Paper Status: </td>
        <td align="left" style="border-bottom:solid 1px #888">
        <asp:DropDownList ID="ddlPaperStatus" runat="server" AutoPostBack="False" Width="150px" 
        onselectedindexchanged="ddlApprovalStatus_SelectedIndexChanged">
            <asp:ListItem Selected="True" Text="All" Value="4"></asp:ListItem>
            <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
            <asp:ListItem Text="Unknown" Value="0"></asp:ListItem>
            <asp:ListItem Text="Submitted" Value="2"></asp:ListItem>
            <asp:ListItem Text="Published" Value="3"></asp:ListItem>            
        </asp:DropDownList>
        </td>
    </tr>
     <tr style="line-height:35px;height:35px;">
        <td style="border-bottom:solid 1px #888;border-right:solid 1px #888">PA Result: </td>
        <td align="left" style="border-bottom:solid 1px #888">
        <asp:DropDownList ID="ddlPAResult" runat="server" AutoPostBack="False" Width="150px">
            <asp:ListItem Selected="True" Text="All" Value="6"></asp:ListItem>
            <asp:ListItem Text="Very Bad" Value="1"></asp:ListItem>
            <asp:ListItem Text="Poor" Value="2"></asp:ListItem>
            <asp:ListItem Text="Fair" Value="3"></asp:ListItem>
            <asp:ListItem Text="Good" Value="4"></asp:ListItem>            
            <asp:ListItem Text="Excellent" Value="5"></asp:ListItem>
        </asp:DropDownList>
        </td>
    </tr>
    <tr style="line-height:35px;height:35px;">
        <td style="border-bottom:solid 1px #888;border-right:solid 1px #888">Checkout Date:</td>
        <td align="left" style="border-bottom:solid 1px #888">
        From: <asp:TextBox ID="dtFrom" runat="server" ReadOnly="false"></asp:TextBox>
        &nbsp;&nbsp;
        To: <asp:TextBox ID="dtTo" runat="server" ReadOnly="false"></asp:TextBox>
        <asp:CompareValidator 
                ID="cmpCheckoutDate" 
                runat="server" 
                ControlToValidate="dtTo"
                ControlToCompare="dtFrom" Operator="GreaterThan" SetFocusOnError="true"
                ErrorMessage="End date must be greater than start date!" Type="Date" Display="Dynamic" />
        </td>
    </tr>
    <tr style="line-height:35px;height:35px;">
        <td style="border-bottom:solid 1px #888;border-right:solid 1px #888">Checkin Date:</td>
        <td align="left" style="border-bottom:solid 1px #888">
        From: <asp:TextBox ID="dtCheckinFrom" runat="server" ReadOnly="false"></asp:TextBox>
        &nbsp;&nbsp;
        To: <asp:TextBox ID="dtCheckinTo" runat="server" ReadOnly="false"></asp:TextBox>
        <asp:CompareValidator 
                ID="CompareValidator1" 
                runat="server" 
                ControlToValidate="dtCheckinTo"
                ControlToCompare="dtCheckinFrom" Operator="GreaterThan" SetFocusOnError="true"
                ErrorMessage="End date must be greater than start date!" Type="Date" Display="Dynamic" />
        </td>
    </tr>
    
     <tr  style="line-height:35px;height:35px;">
        <td colspan="2" align="center" style="border-bottom:solid 0px #888">
            
            <div style="width:120px;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                     <asp:ImageButton Width="24" Height="24" CssClass="img_icon" ImageUrl="~/Resource/Images/filter.png" ID="btnFilter"
                            runat="server" AlternateText="Schedule Interview" OnClick="btnFind_Clicked" CausesValidation="true" ImageAlign="AbsMiddle" />
                            <label for="<%=btnFilter.ClientID %>"><span style="cursor:hand">Find</span></label>
                </div>
        </td>
    </tr>
</table>
<div style="width:100%;text-align:right;padding:3 0 3 0">
<table border="0" cellpadding="0" cellspacing="0" style="width:160px">
    <tr>
                    <td onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';"
                    style="padding:0 5;height:30;width:180px">
                    <asp:ImageButton Width="24" Height="24" ImageAlign="AbsMiddle" ImageUrl="~/Resource/Images/export_excel.png" 
                    ID="expExcel" runat="server" Font-Bold="true" OnClick="btnExportExcel_Click" />
                    <label style="cursor:hand" for="<%=expExcel.ClientID %>"><b>Export to Excel</b></label>                                        
                    </td>
    </tr>
</table>
</div>

    <asp:GridView ID="gvPapers" runat="server" AutoGenerateColumns="False" 
        CellPadding="3" GridLines="Vertical" Width="100%" AllowPaging="True" 
        onpageindexchanging="gvApprovingList_PageIndexChanging"  RowStyle-Height="40"
        onselectedindexchanging="gvApprovingList_SelectedIndexChanging" 
        PageSize="20" BackColor="White" BorderColor="#999999" BorderStyle="None" 
        BorderWidth="1px">
        <Columns>
            
           <asp:TemplateField ItemStyle-Wrap="false">
                <ItemTemplate>
                    <span title='<%#Eval("Name") %>'><%#Eval("InternName") %></span>
                </ItemTemplate>
                <HeaderTemplate>Paper Name</HeaderTemplate>
                     <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>            
            <asp:BoundField DataField="InternName" Visible="false"  ItemStyle-Wrap="false" 
                HeaderStyle-Wrap="false" HeaderText="Intern Name" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField>
                <ItemTemplate>
                    <%#  MSRA.SpringField.Components.CheckInFormResourceManager.IdToText("Groups",
                        int.Parse(DataBinder.Eval(Container.DataItem, "GroupId").ToString().Trim()))%>
                </ItemTemplate>
                <HeaderTemplate>
                    Group</HeaderTemplate>
                     <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:BoundField DataField="MentorAlias" HeaderText="Mentor Alias" HeaderStyle-HorizontalAlign="Left" 
                HeaderStyle-Wrap="false" >
<HeaderStyle Wrap="False"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="160px"  HeaderStyle-HorizontalAlign="Left"
                DataField="HighestEducationalInstitution" HeaderText="University" >            
<ItemStyle Width="160px"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="true">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "CheckInDate", "{0:yyyy-MM-dd}")%>
                </ItemTemplate>
                <HeaderTemplate>Checkin Date</HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField  ItemStyle-Wrap="false" HeaderStyle-Wrap="true">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "CheckOutDate", "{0:yyyy-MM-dd}")%>
                </ItemTemplate>
                <HeaderTemplate>Checkout Date</HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <%#  MSRA.SpringField.Components.PAResourceManager.IdToText("PaperStatus",
                                                int.Parse(DataBinder.Eval(Container.DataItem, "CurrentStatus").ToString().Trim()))%>
                </ItemTemplate>
                <HeaderTemplate>Paper Status</HeaderTemplate>
                     <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>            
            <asp:TemplateField  HeaderStyle-Wrap="false">
                <ItemTemplate>
                    <%#  MSRA.SpringField.Components.PAResourceManager.IdToText("PerformanceLevel",
                                                                        int.Parse(DataBinder.Eval(Container.DataItem, "OverrallEvaluation").ToString().Trim()))%>
                    
                </ItemTemplate>
                <HeaderTemplate>PA Result</HeaderTemplate>
                     <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            
            
        </Columns>
        <EmptyDataTemplate>
           <p style="font-size: 12px">
        There is not any publication at all...</p>
            </EmptyDataTemplate>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#9C969C" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
    </div>
    
    <div id="statusbar" style="background-color:#FFFFE1;border:solid 1px #FEC951;height:30;vertical-align:middle;margin:5 0;text-align:left">
            <table style="width:100%;height:100%">
                <tr>
                    <td align="left" valign="middle">
                        <asp:Label ID="lbMessage" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            </div>

</asp:Content>
