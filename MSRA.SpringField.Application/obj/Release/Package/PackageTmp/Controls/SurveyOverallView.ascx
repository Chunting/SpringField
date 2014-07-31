<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SurveyOverallView.ascx.cs" Inherits="MSRA.SpringField.Application.Controls.SurveyOverallView" %>
<br />
<div id="content_title" class="panel_title_expand" ">
            Applicants -
            <asp:Label ID="lbCount" runat="server" Text="0"></asp:Label>&nbsp;Records Found
</div>
<asp:GridView CssClass="applicants_table" ID="gvSurveyOverallView" runat="server" AutoGenerateColumns="false" OnPreRender="gvSurveyOverallView_PreRender" Width="100%">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("SurveyTitle")%>
            </ItemTemplate>
            <HeaderTemplate>SurveyTitle</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <%# Eval("Strongly agree #")%>
            </ItemTemplate>
            <HeaderTemplate>Strongly agree #</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <%# Eval("Somewhat agree #")%>
            </ItemTemplate>
            <HeaderTemplate>Somewhat agree #</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <%# Eval("Neither agree nor disagree #")%>
            </ItemTemplate>
            <HeaderTemplate>Neither agree nor disagree #</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <%# Eval("Somewhat disagree #")%>
            </ItemTemplate>
            <HeaderTemplate>Somewhat disagree #</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <%# Eval("Strongly disagree #")%>
            </ItemTemplate>
            <HeaderTemplate>Strongly disagree #</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
            <ItemTemplate>
                <%# Eval("N/A #")%>
            </ItemTemplate>
            <HeaderTemplate>N/A #</HeaderTemplate>
        </asp:TemplateField>               
    </Columns>
    <HeaderStyle BackColor="#999999" ForeColor="white" />
</asp:GridView>
<br />
