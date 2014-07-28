<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SRResearchGroup.ascx.cs" Inherits="Controls_SRResearchGroup" %>
<br />
<br />
<asp:GridView ID="gvGroupCadt" runat="server" AutoGenerateColumns="false">
    <Columns>
            <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Group")%>
            </ItemTemplate>
            <HeaderTemplate>Group</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Cadt #") %>
            </ItemTemplate>
            <HeaderTemplate>Cadt #</HeaderTemplate>
        </asp:TemplateField>
    </Columns>
    <HeaderStyle BackColor="#EAEAEA" />
</asp:GridView>
<br />
<asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel" OnClick="btnExportExcel_Click" />