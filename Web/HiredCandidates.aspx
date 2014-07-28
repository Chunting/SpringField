<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HiredCandidates.aspx.cs"
    Inherits="HiredCandidates" MasterPageFile="~/SpringfieldMaster.master" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="mainContent" runat="server">
    <div style="width: 100%">
        <div>
            <p style="font-size: 20px">
                Hired Candidates List</p>
        </div>
        <div id="filter_title" class="panel_title_expand" onclick="ChangeStyle(this,'filter_content')">
            Filter Condition
        </div>
        <div id="filter_content" class="panel_content" style="display: block;">
            <table class="applicants_table">
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
                    <td colspan="2" style="text-align: right">
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" /></td>
                </tr>
            </table>
        </div>
        <hr class="split_line" />
        <div>
            <div id="content_title" class="panel_title_expand" onclick="ChangeStyle(this,'content_content')">
                Applicants -
                <asp:Label ID="lbCount" runat="server" Text="0"></asp:Label>&nbsp;Records Found
            </div>
            <div class="panel_action">
                <asp:Button ID="btnOnBoardSelected" runat="Server" Text="On Board" OnClick="btnOnBoardSelected_Click"
                    OnClientClick="return confirm('Are you sure to change their status to On Board');" />
                <asp:Button ID="btnDeclineSelected" runat="Server" Text="Offer Decline" OnClick="btnDeclineSelected_Click"
                    OnClientClick="return confirm('Are you sure to change their status to Offer Decline');" />
                <asp:Button ID="btnRejectedSelected" runat="Server" Text="Reject" OnClick="btnRejectedSelected_Click"
                    OnClientClick="return confirm('Are you sure to change their status to Rejected');" />
            </div>
            <div id="content_content" class="panel_content" style="display: block;">
                <asp:GridView ID="gvHiredCandidates" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    OnPageIndexChanging="gvHiredCandidates_PageIndexChanging" OnRowCommand="gvHiredCandidates_RowCommand">
                    <Columns>
                        <asp:TemplateField AccessibleHeaderText="Check" HeaderText="Check">
                            <ItemTemplate>
                                <input type="checkbox" id="cb_ischeck" name="cb_ischeck" value="<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Name</HeaderTemplate>
                            <ItemTemplate>
                                <a id="A1" runat="server" href='<%# GetApplicantLink(DataBinder.Eval(Container.DataItem, "ApplicantId").ToString()) %>'>
                                    <%# Eval("FirstName")%>
                                    <%# Eval("LastName").ToString().ToUpper()%>
                                </a>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" Width="100px" />
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
                        <asp:TemplateField Visible="False">
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
                        <asp:TemplateField Visible="False">
                            <HeaderTemplate>
                                Check Out Date</HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "PreferLastWorkingDay", "{0:yyyy-MM-dd}")%>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Approval Email</HeaderTemplate>
                            <ItemTemplate>
                                <%# GetApprovalEmailLink(DataBinder.Eval(Container.DataItem, "GMApprovalDocId").ToString().Trim(),DataBinder.Eval(Container.DataItem, "MentorApprovalDocId").ToString().Trim())%>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Actions</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Button ID="btnOnBoard" Text="On Board" runat="server" CommandName="OnBoard"
                                    CommandArgument=' <%# DataBinder.Eval(Container.DataItem, "ApplicantId").ToString().Trim()%>'
                                    OnClientClick="return ConfirmOnBoard();" Visible="true"></asp:Button>
                                <asp:Button ID="btnDecline" runat="server" CommandName="Decline" OnClientClick="return ConfirmDecline();"
                                    CommandArgument=' <%# DataBinder.Eval(Container.DataItem, "ApplicantId").ToString().Trim()%>'
                                    Visible="true" Text="Offer Decline"></asp:Button>
                                <asp:Button ID="btnReject" runat="server" CommandName="Reject" OnClientClick="return ConfirmReject();"
                                    CommandArgument=' <%# DataBinder.Eval(Container.DataItem, "ApplicantId").ToString().Trim()%>'
                                    Visible="true" Text="Reject"></asp:Button>
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
