<%@ Page Title="" Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" CodeBehind="PAKeyinToolForm.aspx.cs" Inherits="MSRA.SpringField.Application.Modules.Temp.PAKeyinToolForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" style="width:100%">
<tr>
    <td>&nbsp;</td>
</tr>
<tr>
    <td><h4>Imported Applicants</h4></td>
</tr>
<tr>
    <td>&nbsp;</td>
</tr>
<tr>
    <td>
        <table>
            <tr>
                <td>Intern Name: </td>
                <td><asp:TextBox runat="server" ID="txtInternName"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Email: </td>
                <td><asp:TextBox runat="server" ID="txtEmail"></asp:TextBox></td>
            </tr>            
            <tr>
                <td colspan="2"><asp:Button ID="btnSearch" Text="Filter" runat="server" OnClick="btnSearch_Click" /></td>
            </tr>
        </table>
    </td>
</tr>
<tr>
    <td>
        <asp:GridView Width="100%" ID="gvApplicantList" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" RowStyle-Height="25"
            onpageindexchanging="gvApplicantList_PageIndexChanging">
        <Columns>
            <asp:HyperLinkField HeaderStyle-HorizontalAlign="Left" HeaderText="Name" 
            DataTextField="InternName" DataNavigateUrlFormatString="~/MentorPA.aspx?paid={0}"
             DataNavigateUrlFields="id" />
             <asp:TemplateField>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "CheckOutDate", "{0:yyyy-MM-dd}")%>
                </ItemTemplate>
                <HeaderTemplate>Check-out Date</HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Application Status">
                <ItemTemplate>
                <%# (MSRA.SpringField.Components.Enumerations.ApplicationStatusEnum)int.Parse(DataBinder.Eval(Container.DataItem, "Status").ToString()) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Chinese Name" DataField="NameInChinese" />
            <asp:BoundField HeaderText="MentorName" DataField="MentorName" />
            <asp:TemplateField>
                <ItemTemplate>
                    <%#  MSRA.SpringField.Components.CheckInFormResourceManager.IdToText("Groups", int.Parse(DataBinder.Eval(Container.DataItem, "GroupId").ToString().Trim()))%>
                </ItemTemplate>
                <HeaderTemplate>
                    Group</HeaderTemplate>
                     <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
        </Columns>
        </asp:GridView>
    </td>
</tr>
</table>
</asp:Content>
