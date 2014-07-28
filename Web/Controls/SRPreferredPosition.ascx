<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SRPreferredPosition.ascx.cs" Inherits="Controls_SRPreferredPosition" %>

<br />
<br />
<asp:GridView ID="gvPreferredPosition" runat="server" AutoGenerateColumns="false">
    <Columns>
            <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Preferred Position")%>
            </ItemTemplate>
            <HeaderTemplate>Preferred Position</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Cadt #") %>
            </ItemTemplate>
            <HeaderTemplate>Cadt #</HeaderTemplate>
        </asp:TemplateField>
                <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Interviewed Cadt #")%>
            </ItemTemplate>
            <HeaderTemplate>Interviewed Cadt #</HeaderTemplate>
        </asp:TemplateField>
                <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Hired #")%>
            </ItemTemplate>
            <HeaderTemplate>Hired #</HeaderTemplate>
        </asp:TemplateField>
                <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Rejected #")%>
            </ItemTemplate>
            <HeaderTemplate>Rejected #</HeaderTemplate>
        </asp:TemplateField>
                <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Decline Offer #")%>
            </ItemTemplate>
            <HeaderTemplate>Decline Offer #</HeaderTemplate>
        </asp:TemplateField>
    </Columns>
    <HeaderStyle BackColor="#EAEAEA" />
</asp:GridView>
<br />
<asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel" OnClick="btnExportExcel_Click" />
