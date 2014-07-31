<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="MSRA.SpringField.Application.HiredCandidates" MasterPageFile="~/SpringfieldMaster.master" Codebehind="HiredCandidates.aspx.cs" %>

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
                    <td colspan="2" style="text-align: center">
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
        <hr class="split_line" />
        
        <div class="toolbar">
        <table style="height:100%" cellpadding="0" cellspacing="0">
            <tr>
                <td runat="server" id="btnMultiInterview" style="padding:0 10;height:30;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton OnClientClick="return confirm('Are you sure to change their status to On Board');" 
                        Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/onboard.png" ID="btnOnBoardSelected"
                        runat="server" AlternateText="On Board" OnClick="btnOnBoardSelected_Click" ImageAlign="AbsMiddle" />
                        <label for="<%=btnOnBoardSelected.ClientID %>"><span style="cursor:hand">On Board</span></label>
                </td>
                <td>&nbsp;</td>
                <td style="padding:0 10;height:30;" runat="server" id="btnFavorite"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton OnClientClick="return confirm('Are you sure to change their status to Offer Decline');"
                     Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/offer_decline.png"
                        ID="btnDeclineSelected" runat="server" AlternateText="Offer Decline" OnClick="btnDeclineSelected_Click" ImageAlign="AbsMiddle"/>
                         <label for="<%=btnDeclineSelected.ClientID %>"><span style="cursor:hand">Offer Decline</span></label>
                </td>
                <td>&nbsp;</td>
                <td style="padding:0 10;height:30;"   runat="server" id="btnMultiRefer"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton OnClientClick="return confirm('Are you sure to change their status to Rejected');"
                    Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/reject.png"
                        ID="btnRejectedSelected" runat="server" AlternateText="Reject" OnClick="btnRejectedSelected_Click" ImageAlign="AbsMiddle"/>
                        <label for="<%=btnRejectedSelected.ClientID %>"><span style="cursor:hand">Reject</span></label>
                </td>
            </tr>
        </table>        
    </div>
        
        <div>
            <div id="content_title" class="panel_title_expand" onclick="ChangeStyle(this,'content_content')">
                Applicants -
                <asp:Label ID="lbCount" runat="server" Text="0"></asp:Label>&nbsp;Records Found
            </div>            
            <div id="content_content" class="panel_content" style="display: block;">
                <asp:GridView ID="gvHiredCandidates" runat="server" AutoGenerateColumns="False" AllowPaging="True" CssClass="applicants_table"
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
                              <div style="cursor:pointer;width:100%;padding:2 2;margin:1 1">
                                <asp:ImageButton Width="16px" Height="16px" ID="btnOnBoard" Text="On Board" runat="server" CommandName="OnBoard"
                                    CommandArgument=' <%# DataBinder.Eval(Container.DataItem, "ApplicantId").ToString().Trim()%>'
                                    OnClientClick="return ConfirmOnBoard();" Visible="true" ImageUrl="~/Resource/Images/onboard.png"></asp:ImageButton>
                                 <label runat="server" id="lbOb" for="<%=btnOnBoard.ClientID %>">On Board</label>
                            </div>        
                            <div runat="server" id="divSchedual" style="cursor:pointer;width:100%;padding:2 2;margin:1 1">
                                <asp:ImageButton Width="16px" Height="16px" ID="btnDecline" runat="server" CommandName="Decline" OnClientClick="return ConfirmDecline();"
                                    CommandArgument=' <%# DataBinder.Eval(Container.DataItem, "ApplicantId").ToString().Trim()%>'
                                    Visible="true" Text="Offer Decline" ImageUrl="~/Resource/Images/offer_decline.png"></asp:ImageButton>
                                 <label runat="server" id="lbOb2" for="<%=btnDecline.ClientID %>">Offer Decline</label>
                            </div>                    
                            <div style="cursor:pointer;width:100%;padding:2 2;margin:1 1" runat="server" id="txtFav">                            
                                <asp:ImageButton Width="16px" Height="16px" ID="btnReject" runat="server" CommandName="Reject" OnClientClick="return ConfirmReject();"
                                    CommandArgument=' <%# DataBinder.Eval(Container.DataItem, "ApplicantId").ToString().Trim()%>'
                                    Visible="true" Text="Reject" ImageUrl="~/Resource/Images/reject.png"></asp:ImageButton>                                
                                 <label runat="server" id="lbOb3" for="<%=btnReject.ClientID %>">Reject</label>
                            </div>
                            
                            
                            
                            
                              <%--  <asp:Button ID="btnOnBoard" Text="On Board" runat="server" CommandName="OnBoard"
                                    CommandArgument=' <%# DataBinder.Eval(Container.DataItem, "ApplicantId").ToString().Trim()%>'
                                    OnClientClick="return ConfirmOnBoard();" Visible="true"></asp:Button>
                                <asp:Button ID="btnDecline" runat="server" CommandName="Decline" OnClientClick="return ConfirmDecline();"
                                    CommandArgument=' <%# DataBinder.Eval(Container.DataItem, "ApplicantId").ToString().Trim()%>'
                                    Visible="true" Text="Offer Decline"></asp:Button>
                                <asp:Button ID="btnReject" runat="server" CommandName="Reject" OnClientClick="return ConfirmReject();"
                                    CommandArgument=' <%# DataBinder.Eval(Container.DataItem, "ApplicantId").ToString().Trim()%>'
                                    Visible="true" Text="Reject"></asp:Button>--%>
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
