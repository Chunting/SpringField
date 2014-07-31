<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.PAReportForHR"
    MasterPageFile="~/SpringfieldMaster.master" EnableEventValidation="false" Codebehind="PAReportForHR.aspx.cs" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="mainContent" runat="server">
<script language="JavaScript" src="../../Resource/Scripts/popcalendar.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Resource/Scripts/master.js"></script>
    <div style="width: 100%">
        <div>
            <p style="font-size: 20px">Performance Assessment Report for HR</p>
            <p><asp:Label ID="lbTimeSpan" runat="server" Font-Bold="True"></asp:Label></p>
        </div>
        <div class="toolbar">
        <table style="height:100%" cellpadding="0" cellspacing="0">
            <tr>
                <td onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';" 
                    style="padding:0 10;height:30;"><a href="../../Default.aspx" style="text-decoration: none">
                     <img src="../../Resource/Images/backhome.png" width="24" height="24" align="absmiddle" />
                    <b>Back to Home</b></a></td>
               <%-- <td onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';" 
                    style="padding:0 10;height:30;"><a href="../../ReportGenerator.aspx" style="text-decoration: none"><b>Back to Reports</b></a></td>--%>
                <td onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';"
                    style="padding:0 10;height:30;">
                    <asp:ImageButton Width="24" Height="24" ImageAlign="AbsMiddle" ImageUrl="~/Resource/Images/export_excel.png" ID="expExcel" runat="server" Font-Bold="true" OnClick="btnExportExcel_Click" />
                    <label style="cursor:hand" for="<%=expExcel.ClientID %>"><b>Export to Excel</b></label>
                </td>
            </tr>
        </table>
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
                        <b>PA Result:</b></td>
                    <td align="left">
                    <asp:DropDownList ID="ddlPAResult" runat="server" AutoPostBack="False" Width="120px" >
                        <asp:ListItem Selected="True" Text="All" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Excellent" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Good" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Fair" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Poor" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Very Bad" Value="1"></asp:ListItem>
        </asp:DropDownList>
        </td>
                </tr>
                <tr>
                    <td>
                        <b>Graduation Date:</b></td>
                    <td>
                        From:
                        <asp:TextBox ID="tbBeginDate" runat="server"></asp:TextBox>&nbsp;
                        <%--<input type="button" value="Select" id="btnBeginDate" name="btnBeginDate" runat="server" />--%>
                        To:
                        <asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>&nbsp;
                       <%-- <input type="button" value="Select" id="btnEndDate" name="btnEndDate" runat="server" /><br />--%>
                        <asp:CompareValidator ID="cmpDate" runat="server" ControlToValidate="tbEndDate" ControlToCompare="tbBeginDate"
                            Operator="GreaterThan" SetFocusOnError="true" ErrorMessage="End date must be greater than start date!"
                            Type="Date" Display="Dynamic" /></td>
                </tr>
                 <tr>
                    <td>
                        <b>CheckOut Date:</b></td>
                    <td>
                        From:
                        <asp:TextBox ID="tbBeginDate1" runat="server"></asp:TextBox>&nbsp;
                        <%--<input type="button" value="Select" id="btnBeginDate" name="btnBeginDate" runat="server" />--%>
                        To:
                        <asp:TextBox ID="tbEndDate1" runat="server"></asp:TextBox>&nbsp;
                       <%-- <input type="button" value="Select" id="btnEndDate" name="btnEndDate" runat="server" /><br />--%>
                        <asp:CompareValidator ID="cmpDate1" runat="server" ControlToValidate="tbEndDate1" ControlToCompare="tbBeginDate1"
                            Operator="GreaterThan" SetFocusOnError="true" ErrorMessage="End date must be greater than start date!"
                            Type="Date" Display="Dynamic" /></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; border-left: none">
                     <div style="width:120px;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                     <asp:ImageButton Width="24" Height="24" CssClass="img_icon" ImageUrl="~/Resource/Images/search.png" ID="btnSearch"
                            runat="server" AlternateText="Search" OnClick="btnSearch_Click" 
                            CausesValidation="true" ImageAlign="AbsMiddle" />
                            <label for="<%=btnSearch.ClientID %>"><span style="cursor:hand">Search</span></label>
                </div>
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
            <div id="content_content" class="panel_content" style="display: block; overflow:scroll">
                <asp:GridView ID="gvPAReport" Width="150%" AllowPaging="True" OnPageIndexChanging="gvPAReport_PageIndexChanging" RowStyle-Height="30"
                    runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Name</HeaderTemplate>
                            <ItemTemplate>
                                <a id="A1" href='<%# GetViewPALink(Eval("ApplicantId").ToString().Trim())%>' runat="server">
                                    <%# Eval("InternName").ToString().Trim()%>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField>
                            <HeaderTemplate>
                                Status</HeaderTemplate>
                            <ItemTemplate>
                                <%# GetStatusByApplicantId(Eval("ApplicantId").ToString().Trim())%>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Group</HeaderTemplate>
                            <ItemTemplate>
                                <%# GetGroupNameByID(Eval("GroupId").ToString().Trim())%>
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
                                PA Result</HeaderTemplate>
                            <ItemTemplate>
                                <%# GetPerformance(DataBinder.Eval(Container.DataItem, "OverrallEvaluation").ToString())%>
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
                        <%--<asp:TemplateField>
                            <HeaderTemplate>
                                Intern PA Submitted On</HeaderTemplate>
                            <ItemTemplate>
                                <%# ParseDate(DataBinder.Eval(Container.DataItem, "InsertDate").ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:BoundField DataField="HighestEducationalInstitution" HeaderText="University" />
                        <asp:BoundField DataField="Major" HeaderText="Major" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Degree</HeaderTemplate>
                            <ItemTemplate>
                                <%# GetDegree(DataBinder.Eval(Container.DataItem, "ApplicantId").ToString().Trim())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                PA</HeaderTemplate>
                            <ItemTemplate>
                                <a id="A1" href='<%# GetPAZipLink(DataBinder.Eval(Container.DataItem, "id").ToString().Trim())%>'
                                    runat="server">Download</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>No PA meet your conditions.</EmptyDataTemplate>
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#9C969C" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
