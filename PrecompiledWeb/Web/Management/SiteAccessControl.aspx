<%@ page language="C#" masterpagefile="~/SpringfieldMaster.master" autoeventwireup="true" inherits="Management_SiteAccessControl, App_Web_7bnxe2jf" title="SpringField" %>
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

