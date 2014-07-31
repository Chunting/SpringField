<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="MSRA.SpringField.Application.Controls.Controls_HiringReport" Codebehind="HiringReport.ascx.cs" %>
<asp:GridView CssClass="applicants_table" ID="gvReport" runat="server" AutoGenerateColumns="false">
    <Columns>
        <%--<asp:BoundField DataField="GroupName" HeaderText="Group Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>--%>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate><%# Eval("ShortName") %></ItemTemplate>
            <HeaderTemplate>Group Name</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
            <ItemTemplate><%# Eval("Hired Cadt #") %></ItemTemplate>
            <HeaderTemplate>Hired Cadt #</HeaderTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView><asp:Button ID="btngvReportExportExcel" runat="server" Text="Export to Excel" OnClick="btngvReportExportExcel_Click"/>
<br />
<asp:GridView CssClass="applicants_table" ID="gvDetailedReport" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="gvDetailedReport_PageIndexChanging">
    <Columns>
        <asp:TemplateField AccessibleHeaderText="Name" HeaderText="Name">
            <ItemTemplate>
                <asp:HyperLink ID="hlApplication" runat="server" NavigateUrl='<%# Eval("ApplicantId", "~/ShowApplication.aspx?applicant={0}") %>'
                    Target="_blank" Text='<%# Eval("FirstName") + " " + Eval("LastName").ToString().ToUpper() %>'></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField AccessibleHeaderText="NameInChinese" DataField="NameInChinese" HeaderText="Chinese Name" />
        <asp:TemplateField AccessibleHeaderText="Group" HeaderText="Group">
            <ItemTemplate>
                <%# ParseGroup(Container.DataItem)%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField AccessibleHeaderText="MentorAlias" HeaderText="Mentor Alias">
            <ItemTemplate>
                <%# Eval("MentorAlias")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField AccessibleHeaderText="ApplyDate" HeaderText="Apply Date">
            <ItemTemplate>
                <%# ParseApplyDate(Container.DataItem)%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField AccessibleHeaderText="Hiredon" HeaderText="Hired on">
            <ItemTemplate>
                <%# ParseManagerDecisionDate(Container.DataItem)%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField AccessibleHeaderText="Sourcing" HeaderText="Sourcing">
            <ItemTemplate>
                <%# ParseSourcing(Container.DataItem)%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField AccessibleHeaderText="Channel" HeaderText="Channel">
            <ItemTemplate>
                <%# ParseChannel(Container.DataItem)%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField AccessibleHeaderText="University" HeaderText="University">
            <ItemTemplate>
                <%# Eval("HighestEducationalInstitution")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField AccessibleHeaderText="Major" HeaderText="Major">
            <ItemTemplate>
                <%# Eval("Major")%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

