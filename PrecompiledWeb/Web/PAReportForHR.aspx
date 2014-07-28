<%@ page language="C#" autoeventwireup="true" inherits="PAReportForHR, App_Web_wkngwpi-" masterpagefile="~/SpringfieldMaster.master" enableeventvalidation="false" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="mainContent" runat="server">
    <div style="width: 100%">
        <div>
            <p style="font-size: 20px">
                Performance Assessment Report for HR</p>
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
                        <b>Graduation Date:</b></td>
                    <td>
                        From:
                        <asp:TextBox ID="tbBeginDate" runat="server"></asp:TextBox>&nbsp;
                        <input type="button" value="Select" id="btnBeginDate" name="btnBeginDate" runat="server" />
                        To:
                        <asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>&nbsp;
                        <input type="button" value="Select" id="btnEndDate" name="btnEndDate" runat="server" /><br />
                        <asp:CompareValidator ID="cmpDate" runat="server" ControlToValidate="tbEndDate" ControlToCompare="tbBeginDate"
                            Operator="GreaterThan" SetFocusOnError="true" ErrorMessage="End date must be greater than start date!"
                            Type="Date" Display="Dynamic" /></td>
                </tr>
                <tr>
                    <td style="text-align: left; border-right: none">
                        <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel" OnClick="btnExportExcel_Click" /></td>
                    <td style="text-align: right; border-left: none">
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
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
            <div id="content_content" class="panel_content" style="display: block;">
                <asp:GridView ID="gvPAReport" AllowPaging="True" OnPageIndexChanging="gvPAReport_PageIndexChanging"
                    runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Status</HeaderTemplate>
                            <ItemTemplate>
                                <%# GetStatusByApplicantId(Eval("ApplicantId").ToString().Trim())%>
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
                                PA from Mentor</HeaderTemplate>
                            <ItemTemplate>
                                <%# GetPerformance(DataBinder.Eval(Container.DataItem, "OverrallEvaluation").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Mentor</HeaderTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "MentorName").ToString().Trim()%>
                            </ItemTemplate>
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
                                Name</HeaderTemplate>
                            <ItemTemplate>
                                <a id="A1" href='<%# GetViewPALink(Eval("ApplicantId").ToString().Trim())%>' runat="server">
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
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Graduation Date</HeaderTemplate>
                            <ItemTemplate>
                                <%# ParseDate(DataBinder.Eval(Container.DataItem, "GraduationDate").ToString())%>
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
                                Degree</HeaderTemplate>
                            <ItemTemplate>
                                <%# GetDegree(DataBinder.Eval(Container.DataItem, "ApplicantId").ToString().Trim())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="HighestEducationalInstitution" HeaderText="University" />
                        <asp:BoundField DataField="Major" HeaderText="Major" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                PA</HeaderTemplate>
                            <ItemTemplate>
                                <a id="A1" href='<%# GetPAZipLink(DataBinder.Eval(Container.DataItem, "id").ToString().Trim())%>'
                                    runat="server">Download</a>
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
