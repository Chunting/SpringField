<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowCandidates.aspx.cs" Inherits="WeeklyCandidates" MasterPageFile="~/SpringfieldMaster.master" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="mainContent" runat="server">
<script language="javascript" type="text/javascript">
var menu = document.getElementById("menu");
menu.style.display = 'none';
</script>
<div style="width: 1200px;">
<h3><asp:Label ID="lbReportTitle" runat="server" Text="Report"></asp:Label></h3>
<h5><asp:Label ID="lbDateDuration" runat="server" Text="Date From {0} to {1}"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/ReportGenerator.aspx">Back to Reports</asp:HyperLink></h5>
<div id="content_title" class="panel_title_expand">
Applicants - <asp:Label ID="lbCount" runat="server" Text="0"></asp:Label>&nbsp;Records Found
</div>
<div id="content_content" class="panel_content">
    <asp:GridView ID="gvApplicants" runat="server" CssClass="applicants_table" AutoGenerateColumns="False" OnRowDataBound="gvApplicants_RowDataBound" OnRowCommand="gvApplicants_RowCommand">
                <Columns>
                <asp:TemplateField AccessibleHeaderText="Name" HeaderText="Name">
                    <HeaderStyle CssClass="name_style" />
                    <ItemTemplate>
                        <asp:HyperLink ID="hlApplication" runat="server" NavigateUrl='<%# Eval("ApplicantId", "ShowApplication.aspx?applicant={0}") %>'
                            Target="_blank" Text='<%# ((string)Eval("FirstName")) + " " + ((string)Eval("LastName")).ToUpper() %>'></asp:HyperLink>
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
                <asp:BoundField AccessibleHeaderText="Major" DataField="Major" HeaderText="Major" >
                    <HeaderStyle CssClass="major_style" />
                </asp:BoundField>
                    <asp:BoundField AccessibleHeaderText="InterestedGroup" HeaderText="Interested Group" DataField="InterestedGroup" />
                <asp:TemplateField AccessibleHeaderText="Status" HeaderText="Status">
                    <HeaderStyle CssClass="status_style" />
                    <ItemTemplate>
                        <%# ParseStatus(Container.DataItem)%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Mentor" HeaderText="Mentor">
                    <HeaderStyle CssClass="status_style" />
                    <ItemTemplate>
                        <%# ParseMentor(Container.DataItem)%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField AccessibleHeaderText="CompleteDate" HeaderText="Apply Date">
                    <HeaderStyle CssClass="date_style" />
                    <ItemTemplate>
                        <%# ParseDate(Container.DataItem) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="CurrentStatus" HeaderText="Last Action">
                    <HeaderStyle CssClass="interview_style" />
                    <ItemTemplate>
                        <%# ParseInterviewStatus(Container.DataItem) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Last Action Date" HeaderText="Last Action Date">
                    <ItemTemplate>
                        <%# ParseLastActionDate(Container.DataItem) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Operation" HeaderText="Action">
                    <HeaderStyle CssClass="operation_style" />
                    <ItemTemplate>
                        <a href="#" onclick="OpenNewWindow('ArrangeInterview.aspx','<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>');"><img src="ProUI/images/interview.jpg" class="img_icon" alt='Schedule Interview:<%# (string)Eval("FirstName") + " " + ((string)Eval("LastName")).ToUpper() %>' style="width: 16px; height: 16px;" /></a>&nbsp;&nbsp;&nbsp;<asp:ImageButton ImageUrl="~\ProUI\images\addfavorite.gif" AlternateText="Add Favorite" ID="btnAddFavorite" runat="server" CommandName="AddFavorite" style="border: 0px;height: 16px;width: 16px;"></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
            None Records Found
            </EmptyDataTemplate>
    </asp:GridView>
</div>
</div>
</asp:Content>
