<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InfoSourceGenerator.ascx.cs" Inherits="InfoSourceGenerator" %>
<table class="applicants_table">

<asp:Panel ID="pnlCategory" runat="server">
<tr>
    <td>
    Category:
    </td>
    <td>
    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
    </td>
</tr>
</asp:Panel>

<asp:Panel ID="pnlSource" runat="server">
<tr>
    <td>
    Source:
    </td>
    <td>
    <asp:DropDownList ID="ddlSource" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSource_SelectedIndexChanged"></asp:DropDownList>
    </td>
</tr>
</asp:Panel>

<asp:Panel ID="pnlChannel" runat="server">
<tr>
    <td>
    Channel:
    </td>
    <td>
    <asp:DropDownList ID="ddlChannel" runat="server" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ddlChannel_SelectedIndexChanged">
    </asp:DropDownList><br />
    <asp:TextBox ID="tbChannel" runat="server" Visible="true" Enabled="false"></asp:TextBox>
    </td>
</tr>
</asp:Panel>

<asp:Panel ID="pnlDetail" runat="server">
<tr>
    <td>
    Detail:
    </td>
    <td>
        <asp:TextBox ID="tbDetail" runat="server" Visible="true"></asp:TextBox>
    </td>
</tr>
</asp:Panel>

</table>