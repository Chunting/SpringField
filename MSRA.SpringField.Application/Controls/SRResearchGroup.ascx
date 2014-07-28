<%@ Control Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Controls.Controls_SRResearchGroup" Codebehind="SRResearchGroup.ascx.cs" %>
<br />
<asp:GridView CssClass="applicants_table" ID="gvGroupCadt" runat="server" AutoGenerateColumns="false" Width="100%">
    <Columns>
            <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Group")%>
            </ItemTemplate>
            <HeaderTemplate>Group</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <%# Eval("Cadt #") %>
            </ItemTemplate>
            <HeaderTemplate>Cadt #</HeaderTemplate>
        </asp:TemplateField>
    </Columns>
    <HeaderStyle BackColor="#EAEAEA" />
</asp:GridView>
<br />