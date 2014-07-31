<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.WeeklyCandidates" MasterPageFile="~/SpringfieldMaster.master" Codebehind="ShowCandidates.aspx.cs" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="mainContent" runat="server">
<script language="JavaScript" src="../../Resource/Scripts/popcalendar.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Resource/Scripts/master.js"></script>
</script>


<div style="width: 100%;padding:5 3;margin:5 5">
<h3><asp:Label ID="lbReportTitle" runat="server" Text="Report"></asp:Label></h3>
<h5><asp:Label ID="lbDateDuration" runat="server" Text="Date From {0} to {1}"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<%--<asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/ReportGenerator.aspx">Back to Reports</asp:HyperLink>--%></h5>
<div id="content_title" class="panel_title_expand">
Applicants - <asp:Label ID="lbCount" runat="server" Text="0"></asp:Label>&nbsp;Records Found
</div>
<div id="content_content" class="panel_content">
<div style="width:100%">
    <asp:GridView Width="100%" ID="gvApplicants" runat="server" CssClass="applicants_table" AutoGenerateColumns="False" OnRowDataBound="gvApplicants_RowDataBound" OnRowCommand="gvApplicants_RowCommand">
                <Columns>
                <asp:TemplateField AccessibleHeaderText="Name" HeaderText="Name">
                    <HeaderStyle CssClass="name_style" />
                    <ItemTemplate>
                        <asp:HyperLink ID="hlApplication" runat="server" NavigateUrl='<%# Eval("ApplicantId", "~/ShowApplication.aspx?applicant={0}") %>'
                            Target="_self" Text='<%# ((string)Eval("FirstName")) + " " + ((string)Eval("LastName")).ToUpper() %>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:BoundField AccessibleHeaderText="NameInChinese" DataField="NameInChinese" HeaderText="Chinese Name" />
                    <asp:BoundField DataField="jobinfosource" HeaderText="JobInfoSource" />
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
                    <asp:BoundField Visible="false" AccessibleHeaderText="InterestedGroup" HeaderText="Interested Group" DataField="InterestedGroup" />
                <asp:TemplateField AccessibleHeaderText="Status" HeaderText="Status" Visible="false">
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
                <asp:TemplateField AccessibleHeaderText="CurrentStatus" HeaderText="Last Action" Visible="false">
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
                <asp:TemplateField AccessibleHeaderText="Operation" HeaderText="Action" ItemStyle-Wrap="false">
                    <HeaderStyle CssClass="operation_style" />
                    <ItemTemplate>
                     <div runat="server" id="divSchedual" style="cursor:pointer;width:100%;padding:2 2;margin:1 1">
                                <a id="anchorSchedule" href="javascript:void(0);">
                                <img align="absmiddle" onclick="OpenNewWindow('ArrangeInterview.aspx','<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>')" id="btnSchedule" src="../../Resource/Images/schedule_small.png" border="0" alt="Schedule Interview:<%# (string)Eval("FirstName") + " " + (string)Eval("LastName") %>"
                                    style="width: 16px; height: 16px;"/></a>
                                <label for="anchorSchedule" onclick="OpenNewWindow('ArrangeInterview.aspx','<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>')">Schedule</label>
                            </div>                    
                            <div style="cursor:pointer;width:100%;padding:2 2;margin:1 1" runat="server" id="txtFav">                            
                                <asp:ImageButton
                                    ImageUrl="~/Resource/Images/addtofavorite_small.png" AlternateText="Add to Favorite" ID="btnAddFavorite"
                                    runat="server" CommandName="AddFavorite" Style="border:0px;height:16px;width:16px;" ImageAlign="AbsMiddle">
                                </asp:ImageButton><label runat="server" id="lbl">   Add Favorite</label>
                            </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
            None Records Found
            </EmptyDataTemplate>
    </asp:GridView>
</div>
</div>
</div>
</asp:Content>
