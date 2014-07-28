<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CandidatesReport.aspx.cs"
    Inherits="CandidatesReport" MasterPageFile="~/SpringfieldMaster.master" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="mainContent" runat="server">
    <div style="width: 100%">
        <div>
            <p style="font-size: 20px">
                Candidates Report</p>
            <p>
                <asp:Label ID="lbTimeSpan" runat="server" Font-Bold="True"></asp:Label>
                &nbsp; &nbsp; &nbsp; &nbsp; <a href="Default.aspx" style="text-decoration: underline">
                    <b>Back to Home</b></a>&nbsp; &nbsp; &nbsp; &nbsp;<a href="reportgenerator.aspx"
                        style="text-decoration: underline"><b>Back to Reports</b></a></p>
        </div>
        <div id="filter_title" class="panel_title_expand" onclick="ChangeStyle(this,'filter_content')">
            Filter Condition
        </div>
        <div id="filter_content" class="panel_content" style="display: none;">
            <table class="applicants_table">
                <tr>
                    <td class="bold_font">
                        Apply Date :</td>
                    <td style="height: 33px">
                        From :
                        <asp:TextBox ID="tbStartApplyDate" runat="server"></asp:TextBox>&nbsp; To :
                        <asp:TextBox ID="tbEndApplyDate" runat="server"></asp:TextBox><br />
                        <asp:CompareValidator ID="cmpDate1" runat="server" ControlToValidate="tbEndApplyDate"
                            ControlToCompare="tbStartApplyDate" Operator="GreaterThan" SetFocusOnError="true"
                            ErrorMessage="End date must be greater than start date!" Type="Date" Display="Dynamic" /></td>
                </tr>
                <tr>
                    <td class="bold_font" style="height: 33px">
                        Preferred Start Date :</td>
                    <td style="height: 33px">
                        From :
                        <asp:TextBox ID="tbStartPreferredStartDate" runat="server"></asp:TextBox>&nbsp;
                        To :
                        <asp:TextBox ID="tbEndPreferredStartDate" runat="server"></asp:TextBox><br />
                        <asp:CompareValidator ID="cmpDate2" runat="server" ControlToValidate="tbEndPreferredStartDate"
                            ControlToCompare="tbStartPreferredStartDate" Operator="GreaterThan" SetFocusOnError="true"
                            ErrorMessage="End date must be greater than start date!" Type="Date" Display="Dynamic" /></td>
                </tr>
                <tr>
                    <td class="bold_font" style="height: 33px">
                        Interested Group :</td>
                    <td style="height: 33px">
                        <asp:UpdatePanel ID="upInterestedGroup" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlInterestedGroup" runat="server" AutoPostBack="true" Width="300px"
                                    OnSelectedIndexChanged="ddlInterestedGroup_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Label runat="server" Text="Please specify: " ID="lbOtherGroup" Visible="false"></asp:Label>
                                <asp:TextBox runat="server" ID="tbOtherGroup" Visible="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="bold_font" style="height: 33px">
                        Candidate Sourcing :</td>
                    <td style="height: 33px">
                        <asp:DropDownList ID="ddlSourcing" runat="server" Width="300px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="bold_font" style="height: 33px">
                        Order By :</td>
                    <td style="height: 33px">
                        <asp:DropDownList ID="ddlOrderBy" runat="server" Width="300px">
                            <asp:ListItem Value="ApplicationDate Desc" Text="Apply Date--Descend"></asp:ListItem>
                            <asp:ListItem Value="ApplicationDate Asc" Text="Apply Date--Ascend"></asp:ListItem>
                            <asp:ListItem Value="Status Desc" Text="Status--Descend"></asp:ListItem>
                            <asp:ListItem Value="Status Asc" Text="Status--Ascend"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="bold_font">
                        Columns Management:</td>
                    <td>
                        <asp:UpdatePanel ID="upChooseColums" runat="server">
                            <ContentTemplate>
                                <table style="border: solid 0px black">
                                    <tr>
                                        <td style="border: solid 0px black">
                                            <b>Available columns</b></td>
                                        <td style="border: solid 0px black">
                                        </td>
                                        <td style="border: solid 0px black">
                                            <b>Current columns</b></td>
                                    </tr>
                                    <tr>
                                        <td style="border: solid 0px black">
                                            <asp:ListBox ID="lbAvailablecolumns" runat="server" Width="200px" Height="280px"></asp:ListBox>
                                        </td>
                                        <td style="border: solid 0px black">
                                            <asp:Button ID="btnAddColumn" Width="50px" runat="server" Text=">>" OnClick="btnAddColumn_Click" /><br />
                                            <br />
                                            <asp:Button ID="btnRemoveColumn" Width="50px" runat="server" Text="<<" OnClick="btnRemoveColumn_Click" />
                                        </td>
                                        <td style="border: solid 0px black">
                                            <asp:ListBox ID="lbCurrentcolumns" runat="server" Width="200px" Height="280px"></asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="border: none">
                        <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel" OnClick="btnExportExcel_Click" />
                    </td>
                    <td align="right" style="border: solid 0px black">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                            Width="100px" /></td>
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
                <asp:GridView ID="gvCandidates" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    OnPageIndexChanging="gvCandidates_PageIndexChanging" EnableViewState="False">
                    <EmptyDataTemplate>
                        There is no application fit for your filter now
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%# ParseEnglishName(Container.DataItem)%>
                            </ItemTemplate>
                            <HeaderTemplate>
                                English Name</HeaderTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%# Eval("NameInChinese") %>
                            </ItemTemplate>
                            <HeaderTemplate>
                                Chinese Name</HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%# Eval("HighestEducationalInstitution") %>
                            </ItemTemplate>
                            <HeaderTemplate>
                                University</HeaderTemplate>
                            <HeaderStyle Width="400px" />
                            <ItemStyle Width="400px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%# Eval("Major") %>
                            </ItemTemplate>
                            <HeaderTemplate>
                                Major</HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%# ParseStatus(Container.DataItem)%>
                            </ItemTemplate>
                            <HeaderTemplate>
                                Status</HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%# ParseDegree(Container.DataItem)%>
                            </ItemTemplate>
                            <HeaderTemplate>
                                Degree</HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%# ParseLastAction(Container.DataItem)%>
                            </ItemTemplate>
                            <HeaderTemplate>
                                Last Action</HeaderTemplate>
                            <HeaderStyle Width="200px" />
                            <ItemStyle Width="200px" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#EAEAEA" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
