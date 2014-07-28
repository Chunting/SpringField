<%@ Page Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" CodeFile="SiteAccessControl.aspx.cs" Inherits="Management_SiteAccessControl" Title="SpringField" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
    <strong>
    Add User to role:<br />
    </strong>
    <br />
    Domain\Alias:
    <asp:TextBox ID="tb_alias" runat="server"></asp:TextBox><br />
    Roles:
    <asp:DropDownList ID="ddl_Roles" runat="server">
    </asp:DropDownList><br />
    <asp:Button ID="btn_AddRole" runat="server" OnClick="btn_AddRole_Click" Text="Add" /><br />
    <asp:Label ID="lb_msg" runat="server"></asp:Label>
</asp:Content>

