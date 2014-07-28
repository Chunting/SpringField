<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="MSRA.SpringField.Application.CandidatesReport" MasterPageFile="~/SpringfieldMaster.master" Codebehind="CandidatesReport.aspx.cs" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="mainContent" runat="server">

    <script language="JavaScript" src="../../Resource/Scripts/popcalendar.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Resource/Scripts/master.js"></script>
    
    <p style="font-size: 20px">Candidates Report</p>
    <asp:Label Visible="true" ID="lbTimeSpan" runat="server" Font-Bold="True"></asp:Label>
    <div style="width: 100%">
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
                    </td>
            </tr>
        </table>
        </div>
           
        <table id="mainContentdiv" style="width:100%" cellpadding="0" cellspacing="0">
            <tr>
                <td> 
                <div id="filter_title" class="panel_title_expand" onclick="ChangeStyle(this,'filter_content')">Filter Condition</div>
                    <div id="filter_content" class="panel_content" style="display: none;">
                        <table class="applicants_table">
                            <tr>
                                <td class="bold_font">
                                    Apply Date :</td>
                                <td colspan="3">
                                    From :
                                    <asp:TextBox ID="tbStartApplyDate" runat="server" ReadOnly="false"></asp:TextBox>&nbsp; To :
                                    <asp:TextBox ID="tbEndApplyDate" runat="server" ReadOnly="false"></asp:TextBox><br />
                                    <asp:CompareValidator ID="cmpDate1" runat="server" ControlToValidate="tbEndApplyDate"
                                        ControlToCompare="tbStartApplyDate" Operator="GreaterThan" SetFocusOnError="true"
                                        ErrorMessage="End date must be greater than start date!" Type="Date" Display="Dynamic" /></td>
                            </tr>
                            <tr>
                                <td class="bold_font" style="height: 33px">
                                    Preferred Start Date :</td>
                                <td colspan="3">
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
                                    Preferred End Date :</td>
                                <td colspan="3">
                                    From :
                                    <asp:TextBox ID="tbStartPreferredEndDate" runat="server"></asp:TextBox>&nbsp;
                                    To :
                                    <asp:TextBox ID="tbEndPreferredEndDate" runat="server"></asp:TextBox><br />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbEndPreferredEndDate"
                                        ControlToCompare="tbStartPreferredEndDate" Operator="GreaterThan" SetFocusOnError="true"
                                        ErrorMessage="End date must be greater than start date!" Type="Date" Display="Dynamic" /></td>
                            </tr>
                            <tr>
                                <td class="bold_font"  style="height: 33px">
                                    Search in Resume:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbResume" runat="server" ></asp:TextBox>
                                </td>
                                <!-- Add by Yuanqin, 2011.4.27 -->
                                <td class="bold_font"  style="height: 33px">
                                    Interested Area:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbArea" runat="server"></asp:TextBox>
                                </td>
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
                                <td class="bold_font" style="height: 33px">
                                    Candidate Sourcing :</td>
                                <td style="height: 33px">
                                    <asp:DropDownList ID="ddlSourcing" runat="server" Width="300px">
                                    </asp:DropDownList></td>
                            </tr>
                            
                            <!-- Add by Yuanqin, 2011.2.22, DropDownList:ddlOffline                           
                                 2011.4.27, ddlStatus-->
                            <tr>
                                <td class="bold_font" style="height: 33px">
                                    Status:
                                </td>
                                <td style="height: 33px">         
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="300px">
                                    <asp:ListItem Text="All" Value=""  Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Available" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Waiting For Interview Feedback" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Waiting For Mentor Decision" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Waiting For Group Manager Decision" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Hired" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Qualified But Not Matched" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Rejected" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Offer Declined" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="On Board" Value="9"></asp:ListItem>
                                </asp:DropDownList>
                                </td>
                                <td class="bold_font" style="height: 33px">
                                    Way Of Incruit :</td>
                                <td style="height: 33px">
                                    <asp:DropDownList ID="ddlOffline" runat="server" Width="300px">
                                    </asp:DropDownList></td>
                            </tr>                              
                            <tr>
                                <td class="bold_font" style="height: 33px">
                                    Order By :</td>
                                <td colspan="3">
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
                                <td colspan="3">
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
                                <td colspan="4" align="center" style="border: solid 0px black">
                                <div style="width:120px;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                     <asp:ImageButton Width="24" Height="24" CssClass="img_icon" ImageUrl="~/Resource/Images/filter.png" ID="btnSubmit"
                            runat="server" AlternateText="Search" OnClick="btnSubmit_Click" 
                            CausesValidation="true" ImageAlign="AbsMiddle" />
                            <label for="<%=btnSubmit.ClientID %>"><span style="cursor:hand">Search</span></label>
                </div>
                                        </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td><hr class="split_line" /></td>
            </tr>
            <tr>
                <td id="content_title" class="panel_title_expand" onclick="ChangeStyle(this,'content_content')">
                     Applicants - <asp:Label ID="lbCount" runat="server" Text="0"></asp:Label>&nbsp;Records Found
                </td>
            </tr>
            <tr>
                <td>
                    <div id="content_content" class="panel_content" style="display: block;">            
                            <!--Report Content-->
                            <asp:GridView CssClass="applicants_table" HeaderStyle-Height="30" 
                            Width="100%" ID="gvCandidates" RowStyle-Height="35" HeaderStyle-Wrap="false" runat="server" 
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="20"
                            OnPageIndexChanging="gvCandidates_PageIndexChanging" EnableViewState="False">
                        <EmptyDataTemplate>
                            There is no application fit for your filter now
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <%# ParseEnglishName(Container.DataItem)%>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    English Name</HeaderTemplate>
                                <HeaderStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%# Eval("NameInChinese") %>
                                </ItemTemplate>
                                <HeaderTemplate>Chinese Name</HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <div style="width:100%">
                                       <span title='<%# Eval("HighestEducationalInstitution") %>' 
                                        style="text-overflow:ellipsis;overflow:hidden;width:100%"><%# Eval("HighestEducationalInstitution") %></span>
                                    </div>
                                </ItemTemplate>                                
                                <HeaderTemplate>University</HeaderTemplate>
                                <HeaderStyle Width="400px" />
                                <ItemStyle Width="400px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <span title='<%# Eval("Major") %>' 
                                    style="text-overflow:ellipsis;overflow:hidden;width:180px"><%# Eval("Major") %></span>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    Major</HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%# ParseStatus(Container.DataItem)%>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    Status</HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <%# ParseDegree(Container.DataItem)%>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    Degree</HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
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
                </td>
            </tr>
        </table>        
        
        
    </div>
    
</asp:Content>
