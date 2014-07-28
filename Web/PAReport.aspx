<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PAReport.aspx.cs" Inherits="PAReport"
    MasterPageFile="~/SpringfieldMaster.master" EnableEventValidation="false" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="mainContent" runat="server">
    <div style="width: 100%">
        <div>
            <p style="font-size: 20px">
                Performance Assessment Report</p>
            <p>
                <asp:Label ID="lbTimeSpan" runat="server" Font-Bold="True"></asp:Label>
                &nbsp; &nbsp; &nbsp; &nbsp; <a href="Default.aspx" style="text-decoration: underline">
                    <b>Back to Home</b></a>&nbsp; &nbsp; &nbsp; &nbsp;<a href="reportgenerator.aspx"
                        style="text-decoration: underline"><b>Back to Reports</b></a></p>
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
                        <b>Mentor's Alias:</b></td>
                    <td>
                        <asp:TextBox ID="tbMentorAlias" runat="server"></asp:TextBox></td>
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
                        <b>Status:</b></td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Last Action Time:</b></td>
                    <td>
                        From:
                        <asp:TextBox ID="tbBeginDate" runat="server"></asp:TextBox>&nbsp;
                        <input type="button" value="Select" id="btnBeginDate" name="btnBeginDate" runat="server" />
                        To:
                        <asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>&nbsp;
                        <input type="button" value="Select" id="btnEndDate" name="btnEndDate" runat="server" />
                        <br />
                        <asp:CompareValidator ID="cmpDate1" runat="server" ControlToValidate="tbEndDate"
                            ControlToCompare="tbBeginDate" Operator="GreaterThan" SetFocusOnError="true"
                            ErrorMessage="End date must be greater than start date!" Type="Date" Display="Dynamic" /></td>
                </tr>
                <tr>
                    <td>
                        <b>Check Out Date:</b></td>
                    <td>
                        From:
                        <asp:TextBox ID="tbCheckOutBeginDate" runat="server"></asp:TextBox>&nbsp;
                        <input id="btnCheckOutBeginDate" runat="server" name="btnCheckOutBeginDate" type="button"
                            value="Select" />
                        To:
                        <asp:TextBox ID="tbCheckOutEndDate" runat="server"></asp:TextBox>&nbsp;
                        <input id="btnCheckOutEndDate" runat="server" name="btnCheckOutEndDate" type="button"
                            value="Select" />
                        <br />
                        <asp:CompareValidator ID="cmpDate2" runat="server" ControlToValidate="tbCheckOutEndDate"
                            ControlToCompare="tbCheckOutBeginDate" Operator="GreaterThan" SetFocusOnError="true"
                            ErrorMessage="End date must be greater than start date!" Type="Date" Display="Dynamic" /></td>
                </tr>
                <tr>
                    <td style="text-align: left; border-right: none">
                        <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel" OnClick="btnExportExcel_Click" />
                    </td>
                    <td style="text-align: right; border-left: none">
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />&nbsp;
                    </td>
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
                <asp:Button ID="btnCheckOutSelected" runat="Server" Text="Check Out" OnClick="btnCheckOutSelected_Click"
                    OnClientClick="return confirm('Are you sure to change their status to Check-out');" />
            </div>
            <div id="content_content" class="panel_content" style="display: block;">
                <asp:GridView ID="gvPAReport" runat="server" AllowPaging="True" OnPageIndexChanging="gvPAReport_PageIndexChanging"
                    AutoGenerateColumns="False" OnRowCommand="gvPAReport_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Check</HeaderTemplate>
                            <ItemTemplate>
                                <input type="checkbox" id="cb_ischeck" name="cb_ischeck" value="<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Status</HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("ApplicationStatus") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Group</HeaderTemplate>
                            <ItemTemplate>
                                <%# GetGroupNameByID(Eval("GroupId").ToString().Trim())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Name</HeaderTemplate>
                            <ItemTemplate>
                                <a href='<%# GetApplicantLink(Eval("ApplicantId").ToString().Trim())%>' runat="server">
                                    <%# Eval("InternName").ToString().Trim()%>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                On Board Date</HeaderTemplate>
                            <ItemTemplate>
                                <%# ParseDate(DataBinder.Eval(Container.DataItem, "CheckInDate").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Check Out Date</HeaderTemplate>
                            <ItemTemplate>
                                <%# ParseDate(DataBinder.Eval(Container.DataItem, "CheckOutDate").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False">
                            <HeaderTemplate>
                                Graduation Date</HeaderTemplate>
                            <ItemTemplate>
                                <%# ParseDate(DataBinder.Eval(Container.DataItem, "GraduationDate").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Mentor</HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "MentorName").ToString().Trim()%>
                            </ItemTemplate>
                            <ControlStyle Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Mentor Alias</HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "MentorAlias").ToString().Trim()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Intern PA Submitted On</HeaderTemplate>
                            <ItemTemplate>
                                <%# ParseDate(DataBinder.Eval(Container.DataItem, "InsertDate").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                PA from Mentor</HeaderTemplate>
                            <ItemTemplate>
                                <%# GetPerformance(DataBinder.Eval(Container.DataItem, "OverrallEvaluation").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Deadline</HeaderTemplate>
                            <ItemTemplate>
                                <%# GetDeadline(DataBinder.Eval(Container.DataItem, "InsertDate").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Last Action</HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ModifyDate", "{0:yyyy-MM-dd}")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Action</HeaderTemplate>
                            <ItemTemplate>
                                <asp:Button PostBackUrl='<%# GetViewPALink(Eval("ApplicantId").ToString().Trim(),Eval("Id").ToString().Trim())%>'
                                    Text="Detail" runat="server" Width="90px" /><div style="height: 2px">
                                    </div>
                                <asp:Button ID="btnCheckOut" Text="Check Out" runat="server" CommandArgument='<%# Eval("ApplicantId").ToString()%>'
                                    CommandName="CheckOut" Width="90px" OnClientClick="return confirm('Are you sure to his/her status to Check-out');">
                                </asp:Button>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No PA meet your conditions.</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
