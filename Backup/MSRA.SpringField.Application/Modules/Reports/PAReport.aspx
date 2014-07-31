<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.PAReport"
    MasterPageFile="~/SpringfieldMaster.master" EnableEventValidation="false" Codebehind="PAReport.aspx.cs" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="mainContent" runat="server">
 
   <script language="JavaScript" src="../../Resource/Scripts/popcalendar.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Resource/Scripts/master.js"></script>
    <div style="width: 100%">
        <div>
            <p style="font-size: 20px">Performance Assessment Report</p>
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
                <td onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';"
                    style="padding:0 10;height:30;">
                    <asp:ImageButton Width="24" Height="24" ImageAlign="AbsMiddle" ImageUrl="~/Resource/Images/export_excel.png" ID="expExcel" runat="server" Font-Bold="true" OnClick="btnExportExcel_Click" />
                    <label style="cursor:hand" for="<%=expExcel.ClientID %>"><b>Export to Excel</b></label>
                    <%--<asp:LinkButton ID="btnExportExcel" runat="server" Font-Bold="true" Text="Export to Excel" OnClick="btnExportExcel_Click"/>--%>
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
                        <%--<input type="button" value="Select" id="btnBeginDate" name="btnBeginDate" runat="server" />--%>
                        To:
                        <asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>&nbsp;
                        <%--<input type="button" value="Select" id="btnEndDate" name="btnEndDate" runat="server" />--%>
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
                        <%--<input id="btnCheckOutBeginDate" runat="server" name="btnCheckOutBeginDate" type="button"
                            value="Select" />--%>
                        To:
                        <asp:TextBox ID="tbCheckOutEndDate" runat="server"></asp:TextBox>&nbsp;
                        <%--<input id="btnCheckOutEndDate" runat="server" name="btnCheckOutEndDate" type="button"
                            value="Select" />--%>
                        <br />
                        <asp:CompareValidator ID="cmpDate2" runat="server" ControlToValidate="tbCheckOutEndDate"
                            ControlToCompare="tbCheckOutBeginDate" Operator="GreaterThan" SetFocusOnError="true"
                            ErrorMessage="End date must be greater than start date!" Type="Date" Display="Dynamic" /></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; border-left: none">
                     <div style="width:120px;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                     <asp:ImageButton Width="24" Height="24" CssClass="img_icon" ImageUrl="~/Resource/Images/filter.png" ID="btnSearch"
                            runat="server" AlternateText="Search" OnClientClick="return CauseValidationForUniversityName()" OnClick="btnSearch_Click" ImageAlign="AbsMiddle" />
                            <label for="<%=btnSearch.ClientID %>"><span style="cursor:hand">Search</span></label>
                    </div>
                    </td>
                </tr>
            </table>
        </div>
        <%--<hr class="split_line" />--%>
        <div class="toolbar">
            <table style="height:100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td runat="server" id="tdCheckOutSelected" style="padding:0 10;height:30;"
                        onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                        onmouseout="this.style.borderWidth='0';">
                        <asp:ImageButton OnClientClick="return confirm('Are you sure to change their status to Check-out');" 
                            Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/checkout.png" ID="btnCheckOutSelected"
                            runat="server" AlternateText="On Board" OnClick="btnCheckOutSelected_Click" ImageAlign="AbsMiddle" />
                            <label for="<%=btnCheckOutSelected.ClientID %>"><span style="cursor:hand">Check Out</span></label>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div id="content_title" class="panel_title_expand" onclick="ChangeStyle(this,'content_content')">
                Applicants -
                <asp:Label ID="lbCount" runat="server" Text="0"></asp:Label>&nbsp;Records Found
            </div>
           <%-- <div class="panel_action" style="padding:3 3">
                <asp:Button ID="btnCheckOutSelected" runat="Server" Text="Check Out" OnClick="btnCheckOutSelected_Click"
                    OnClientClick="return confirm('Are you sure to change their status to Check-out');" />
            </div>--%>
            <div id="content_content" class="panel_content" style="display: block;">
                <asp:GridView ID="gvPAReport" runat="server" AllowPaging="True" OnPageIndexChanging="gvPAReport_PageIndexChanging" Width="100%"
                    AutoGenerateColumns="False" OnRowCommand="gvPAReport_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>Check</HeaderTemplate>
                            <ItemTemplate>
                                <input type="checkbox" id="cb_ischeck" name="cb_ischeck" value="<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>Status</HeaderTemplate>
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
                                <div style="margin:5 5">
                                <div style="cursor:pointer;width:100%;padding:2 2;margin:1 1">
                                <asp:ImageButton Width="16px" Height="16px" ID="btnDetail" Text="Detail" runat="server" CommandName="Detail"  CommandArgument='<%# Eval("ApplicantId").ToString()%>'
                                PostBackUrl='<%# GetViewPALink(Eval("ApplicantId").ToString().Trim(),Eval("Id").ToString().Trim())%>'
                                    OnClientClick='<%# GetViewPALink(Eval("ApplicantId").ToString().Trim(),Eval("Id").ToString().Trim())%>'
                                    Visible="true" ImageUrl="~/Resource/Images/detail.png"></asp:ImageButton>
                                 <label runat="server" id="lbOb" for="<%=btnDetail.ClientID %>">Detail</label>
                                </div>        
                                <div runat="server" id="divSchedual" style="cursor:pointer;width:100%;padding:2 2;margin:1 1">
                                    <asp:ImageButton Width="16px" Height="16px" ID="btnCheckOut" runat="server"  CommandArgument='<%# Eval("ApplicantId").ToString()%>'
                                        CommandName="CheckOut" OnClientClick="return confirm('Are you sure to his/her status to Check-out');"
                                        Visible="true" Text="Checkout" ImageUrl="~/Resource/Images/checkout.png"></asp:ImageButton>
                                     <label runat="server" id="lbOb2" for="<%=btnCheckOut.ClientID %>">Checkout</label>
                                </div>      
                                
                                <%--
                                    <asp:Button PostBackUrl='<%# GetViewPALink(Eval("ApplicantId").ToString().Trim(),Eval("Id").ToString().Trim())%>'
                                        Text="Detail" runat="server" Width="90px" /><br />
                                    <asp:Button ID="btnCheckOut" Text="Check Out" runat="server" CommandArgument='<%# Eval("ApplicantId").ToString()%>'
                                        CommandName="CheckOut" Width="90px" OnClientClick="return confirm('Are you sure to his/her status to Check-out');">
                                    </asp:Button>--%>
                                </div>
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
