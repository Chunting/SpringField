<%@ page language="C#" autoeventwireup="true" inherits="InterviewedCandidates, App_Web_wkngwpi-" masterpagefile="~/SpringfieldMaster.master" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="mainContent" runat="server">
    <div style="width: 100%">
        <div>
            <p style="font-size: 20px">
                Uploading Approval Email</p>
        </div>
        <div id="filter_title" class="panel_title_expand" onclick="ChangeStyle(this,'filter_content')">
            Filter Condition
        </div>
        <div id="filter_content" class="panel_content" style="display: block;">
            <table class="applicants_table">
                <tr>
                    <td>
                        <b>Status:</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="All">All</asp:ListItem>
                            <asp:ListItem Value="FM">Waiting for Interview Feedback or Mentor Decision</asp:ListItem>
                            <asp:ListItem Value="GMA">Waiting for Group Manager's Decision</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Candidate's Name (EN):</b></td>
                    <td>
                        <asp:TextBox ID="tbCandidateName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <b>Group Manager's Alias:</b></td>
                    <td>
                        <asp:TextBox ID="tbGMAlias" runat="server"></asp:TextBox><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Group:</b></td>
                    <td>
                        <asp:DropDownList ID="ddlGroup" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Mentor's Alias:</b></td>
                    <td>
                        <asp:TextBox ID="tbMentorAlias" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right" colspan="2">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /></td>
                </tr>
            </table>
        </div>
        <hr class="split_line" />
        <div>
            <div id="content_title" class="panel_title_expand" onclick="ChangeStyle(this,'content_content')">
                Applicants -
                <asp:Label ID="lbCount" runat="server" Text="0"></asp:Label>&nbsp;Records Found
            </div>
            <div id="content_content" class="panel_content" style="display: block;">
                <asp:GridView ID="gvInternviewedCandidates" runat="server" AutoGenerateColumns="False"
                    PagerSettings-Mode="Numeric" PageSize="15" OnPageIndexChanging="gvInternviewedCandidates_PageIndexChanging"
                    AllowPaging="True">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Name</HeaderTemplate>
                            <ItemTemplate>
                                <a runat="server" href='<%# GetApplicantLink(DataBinder.Eval(Container.DataItem, "ApplicantId").ToString()) %>'>
                                    <%# Eval("FirstName")%>
                                    <%# Eval("LastName").ToString().ToUpper()%>
                                </a>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Group</HeaderTemplate>
                            <ItemTemplate>
                                <%# GetGroupNameByID(DataBinder.Eval(Container.DataItem, "GroupId").ToString().Trim())%>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Group Manager Alias</HeaderTemplate>
                            <ItemTemplate>
                                <%# GetGMAliasByID(DataBinder.Eval(Container.DataItem, "GroupManagerId").ToString().Trim())%>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="MentorAlias" HeaderText="Mentor Alias" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Degree</HeaderTemplate>
                            <ItemTemplate>
                                <%# GetDegree(DataBinder.Eval(Container.DataItem, "ApplicantId").ToString().Trim())%>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="HighestEducationalInstitution" HeaderText="University">
                            <ItemStyle Wrap="True" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                On Board Date</HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "PreferCheckInDate", "{0:yyyy-MM-dd}")%>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Check Out Date</HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "PreferLastWorkingDay", "{0:yyyy-MM-dd}")%>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False">
                            <HeaderTemplate>
                                Status</HeaderTemplate>
                            <ItemTemplate>
                                <%# GetStatus(DataBinder.Eval(Container.DataItem, "InterviewStatus").ToString().Trim())%>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Actions</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Button PostBackUrl='<%# GetUploadLink(DataBinder.Eval(Container.DataItem, "ApplicantId").ToString()) %>'
                                    Text="Upload Approval" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        There is no application fit for your filter now
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
