<%@ Control Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Keyin.InfoSourceGenerator" Codebehind="InfoSourceGenerator.ascx.cs" %>
<table class="applicants_table">

<asp:Panel ID="pnlCategory" runat="server" Visible="false">
<tr>
    <td width="10%">
    Category:
    </td>
    <td>
    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
    </td>
</tr>
</asp:Panel>
<asp:UpdatePanel ID="up1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
<ContentTemplate>
<asp:Panel ID="pnlSource" runat="server">
<tr>
    <td>
    Source:
    </td>
    <td>
    <asp:DropDownList ID="ddlSource" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSource_SelectedIndexChanged"></asp:DropDownList>
    </td>
</tr>
</asp:Panel>
<asp:Panel ID="pnlChannel" runat="server" >
<tr>
    <td>
    Channel:
    </td>
    <td>
    <asp:Label ID="lbChannelDescription" runat="server" Text="" Visible="false"></asp:Label>
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
        <asp:Label ID="lbDetailDescription" runat="server" Text=""></asp:Label>
        <asp:TextBox ID="tbDetail" runat="server" Visible="true"></asp:TextBox>
    </td>
</tr>
</asp:Panel>
</ContentTemplate>

</asp:UpdatePanel>
</table>