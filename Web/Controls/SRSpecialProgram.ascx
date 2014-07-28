<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SRSpecialProgram.ascx.cs"
    Inherits="Controls_SRSpecialProgram" %>
<br />
<br />
<asp:GridView ID="gvSpecialProgram" runat="server" AutoGenerateColumns="false">
    <Columns>
            <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Special Program") %>
            </ItemTemplate>
            <HeaderTemplate>Special Program</HeaderTemplate>
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
