<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnBoardReport.ascx.cs" Inherits="MSRA.SpringField.Application.Controls.Controls_OnBoardReport" %>
<asp:GridView CssClass="applicants_table" ID="gvReport" runat="server" AutoGenerateColumns="false">
    <Columns>
        <%--<asp:BoundField DataField="GroupName" HeaderText="Group Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"/>--%>
        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate><%# Eval("Way Of Incruit")%></ItemTemplate>
            <HeaderTemplate>Way Of Incruit</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
            <ItemTemplate><%# Eval("On Board Cadt #") %></ItemTemplate>
            <HeaderTemplate>On Board Cadt #</HeaderTemplate>
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
        <asp:TemplateField AccessibleHeaderText="ApplyDate" HeaderText="Apply Date">
            <ItemTemplate>
                <%# ParseApplyDate(Container.DataItem)%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField AccessibleHeaderText="Degree" HeaderText="Degree">
            <HeaderStyle CssClass="degree_style" />
            <ItemTemplate>
                <%# ParseDegree(Container.DataItem) %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField AccessibleHeaderText="Institution" HeaderText="Institution">
            <HeaderStyle CssClass="institution_style" />
            <ItemTemplate>
                <%# ParseInstitution(Container.DataItem)%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField AccessibleHeaderText="Major" DataField="Major" HeaderText="Major" />
        <asp:BoundField AccessibleHeaderText="InterestedGroup" HeaderText="Interested Group"
            DataField="InterestedGroup" />
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
    </Columns>
</asp:GridView>
