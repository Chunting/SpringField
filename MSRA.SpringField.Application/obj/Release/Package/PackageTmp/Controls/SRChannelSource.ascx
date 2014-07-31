<%@ Control Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Controls.Controls_SRChannelSource" Codebehind="SRChannelSource.ascx.cs" %>
<br />
<asp:GridView CssClass="applicants_table" ID="gvChannelSource" runat="server" AutoGenerateColumns="false" OnPreRender="gvChannelSource_PreRender" Width="100%">
    <Columns>
            <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Channel")%>
            </ItemTemplate>
            <HeaderTemplate>Channel</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <%# Eval("Cadt #") %>
            </ItemTemplate>
            <HeaderTemplate>Cadt #</HeaderTemplate>
        </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%# Eval("Interviewed Cadt #")%>
                    </ItemTemplate>
                    <HeaderTemplate>Interviewed Cadt #</HeaderTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <%# Eval("Hired #")%>
            </ItemTemplate>
            <HeaderTemplate>Hired #</HeaderTemplate>
        </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%# Eval("Rejected #")%>
                    </ItemTemplate>
                    <HeaderTemplate>Rejected #</HeaderTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%# Eval("QualifiedButNotMatched #")%>
                    </ItemTemplate>
                    <HeaderTemplate>QualifiedButNotMatched #</HeaderTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <%# Eval("Decline Offer #")%>
            </ItemTemplate>
            <HeaderTemplate>Decline Offer #</HeaderTemplate>
        </asp:TemplateField>
    </Columns>
    <HeaderStyle BackColor="#999999" ForeColor="white" />
</asp:GridView>
<br />