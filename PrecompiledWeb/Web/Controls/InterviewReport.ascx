<%@ control language="C#" autoeventwireup="true" inherits="Controls_InterviewReport, App_Web_eecyfkj6" %>
<asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="gvReport_PageIndexChanging">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Mentor Alias")%>
            </ItemTemplate>
            <HeaderTemplate>
                Mentor Alias</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Interviewed Cadt #")%>
            </ItemTemplate>
            <HeaderTemplate>
                Interviewed Cadt #</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Cadt # of Waiting for interview feedback")%>
            </ItemTemplate>
            <HeaderTemplate>
                Cadt # of Waiting for interview feedback</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Cadt # of Waiting for mentor's decision")%>
            </ItemTemplate>
            <HeaderTemplate>
                Cadt # of Waiting for mentor's decision</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Cadt # of waiting for Group Manager's approval")%>
            </ItemTemplate>
            <HeaderTemplate>
                Cadt # of waiting for Group Manager's approval</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Hired #")%>
            </ItemTemplate>
            <HeaderTemplate>
                Hired #</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Reject #")%>
            </ItemTemplate>
            <HeaderTemplate>
                Reject #</HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <%# Eval("Decline #")%>
            </ItemTemplate>
            <HeaderTemplate>
                Decline #</HeaderTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
        There is no candidate fit your conditions!
    </EmptyDataTemplate>
</asp:GridView><asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel" OnClick="btnExportExcel_Click" />
<br />
<br />
<asp:GridView ID="gvDetailCandidates" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="gvDetailCandidates_PageIndexChanging">
    <Columns>
        <asp:TemplateField AccessibleHeaderText="Name" HeaderText="Name">
            <HeaderStyle CssClass="name_style" />
            <ItemTemplate>
                <asp:HyperLink ID="hlApplication" runat="server" NavigateUrl='<%# Eval("ApplicantId", "~/ShowApplication.aspx?applicant={0}") %>'
                    Target="_blank" Text='<%# Eval("FirstName") + " " + Eval("LastName").ToString().ToUpper() %>'></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField AccessibleHeaderText="NameInChinese" DataField="NameInChinese" HeaderText="Chinese Name" />
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
        <asp:BoundField AccessibleHeaderText="Major" DataField="Major" HeaderText="Major">
            <HeaderStyle CssClass="major_style" />
        </asp:BoundField>
        <asp:BoundField AccessibleHeaderText="InterestedGroup" HeaderText="Interested Group"
            DataField="InterestedGroup" />
        <asp:TemplateField AccessibleHeaderText="Status" HeaderText="Status">
            <HeaderStyle CssClass="status_style" />
            <ItemTemplate>
                <%# ParseStatus(Container.DataItem)%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField AccessibleHeaderText="HiringManager" HeaderText="Hiring Manager">
            <HeaderStyle CssClass="status_style" />
            <ItemTemplate>
                <%# ParseHireManager(Container.DataItem)%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField AccessibleHeaderText="CompleteDate" HeaderText="Apply Date">
            <HeaderStyle CssClass="date_style" />
            <ItemTemplate>
                <%# ParseApplyDate(Container.DataItem)%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField AccessibleHeaderText="CurrentStatus" HeaderText="Last Action"
            Visible="false">
            <HeaderStyle CssClass="interview_style" />
            <ItemTemplate>
                <%# ParseInterviewStatus(Container.DataItem) %>
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
        <asp:TemplateField AccessibleHeaderText="Last Action Date" HeaderText="Last Action Date">
            <ItemTemplate>
                <%# ParseLastActionDate(Container.DataItem) %>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
        There is no candidate fit your conditions!
    </EmptyDataTemplate>
</asp:GridView><asp:Button ID="btngvDetailCandidatesExportExcel" Visible="false" runat="server" Text="Export to Excel" OnClick="btngvDetailCandidatesExportExcel_Click"/>
