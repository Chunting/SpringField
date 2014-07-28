<%@ Page Title="" Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" CodeBehind="ApprovingPAList.aspx.cs"
    Inherits="MSRA.SpringField.Application.ApprovingPAList" %>

<%@ Import Namespace="MSRA.SpringField.Components" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" runat="server">

    <script language="JavaScript" src="../../Resource/Scripts/popcalendar.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../Resource/Scripts/master.js"></script>

    <script type="text/javascript">
        function selectAll() {
            var allCheckboxes = document.getElementsByName("__selPA__");
            var cbSelAll = document.getElementById("__selall__");
            if (allCheckboxes != null) {
                for (var i = 0; i < allCheckboxes.length; i++) {
                    allCheckboxes[i].checked = cbSelAll.checked;
                }
            }
        }
    </script>

    <!--
* PA Approval
* Author: Yin.P
* Date: 2010-1-5
-->
    <p style="font-size: 18px">PA List</p>
    <table style="width: 100%; border: solid 1px #888; padding-left: 5px" cellpadding="0" cellspacing="0" border="0">
        <tr style="line-height: 35px; height: 35px;">
            <td style="width: 120px; border-bottom: solid 1px #888; border-right: solid 1px #888">Name: </td>
            <td align="left" style="border-bottom: solid 1px #888;">
                <asp:TextBox ID="txtName" Width="120px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr id="trMentorName" runat="server" style="line-height: 35px; height: 35px;" visible="false">
            <td style="width: 120px; border-bottom: solid 1px #888; border-right: solid 1px #888">Mentor Name: </td>
            <td align="left" style="border-bottom: solid 1px #888;">
                <asp:TextBox ID="txtMentorName" Width="120px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr style="line-height: 35px; height: 35px;">
            <td style="border-bottom: solid 1px #888; border-right: solid 1px #888">Approval Status: </td>
            <td align="left" style="border-bottom: solid 1px #888">
                <asp:DropDownList ID="ddlApprovalStatus" runat="server" AutoPostBack="False" Width="120px" OnSelectedIndexChanged="ddlApprovalStatus_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Text="All" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Complete" Value="1"></asp:ListItem>
                    <%-- <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
            <asp:ListItem Text="Rejected" Value="2"></asp:ListItem>
            <asp:ListItem Text="Invalid" Value="3"></asp:ListItem>--%>
                    <asp:ListItem Text="Incomplete" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Inprocess" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Completed By UR" Value="9"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <!-- Add by Yuanqin, 2011.4.23-->
        <tr style="line-height: 35px; height: 35px;">
            <td style="border-bottom: solid 1px #888; border-right: solid 1px #888">PA from Mentor: </td>
            <td align="left" style="border-bottom: solid 1px #888">
                <asp:DropDownList ID="ddlPAResult" runat="server" AutoPostBack="False" Width="120px">
                    <asp:ListItem Selected="True" Text="All" Value="7"></asp:ListItem>
                    <asp:ListItem Text="Excellent" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Good" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Fair" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Poor" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Very Bad" Value="1"></asp:ListItem>
                    <asp:ListItem Text="N/A" Value="6"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="line-height: 35px; height: 35px;">
            <td style="border-bottom: solid 1px #888; border-right: solid 1px #888">Checkout Date: </td>
            <td align="left" style="border-bottom: solid 1px #888">From:
                <asp:TextBox ID="dtFrom" runat="server" ReadOnly="false"></asp:TextBox>
                &nbsp;&nbsp; To:
                <asp:TextBox ID="dtTo" runat="server" ReadOnly="false"></asp:TextBox>
                <asp:CompareValidator ID="cmpCheckoutDate" runat="server" ControlToValidate="dtTo" ControlToCompare="dtFrom" Operator="GreaterThan"
                    SetFocusOnError="true" ErrorMessage="End date must be greater than start date!" Type="Date" Display="Dynamic" />
            </td>
        </tr>
    </table>
    <div runat="server" id="Div1" class="toolbar">
        <table>
            <tr align="center" style="line-height: 35px; height: 35px;">
                <td align="center" style="border-bottom: solid 0px #888; width: 120px;" onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/filter.png" ID="btnFilter" runat="server"
                        AlternateText="Schedule Interview" OnClick="btnFind_Clicked" CausesValidation="true" ImageAlign="AbsMiddle" />
                    <label for="<%=btnFilter.ClientID %>">
                        <span style="cursor: hand">Find</span></label>
                </td>
                <%--Add by Yuanqin, 2011.5.25--%>
                <td align="center" style="border-bottom: solid 0px #888; width: 200px;" onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';" runat="server" id="tdBtnPARemind">
                    <asp:ImageButton Width="30" Height="24" CssClass="img_icon" ImageUrl="~/Resource/Images/hiring_mail.png" ID="btnPARemind"
                        runat="server" AlternateText="Schedule Interview" OnClick="btnPARemind_Clicked" CausesValidation="true" ImageAlign="AbsMiddle" />
                    <label for="<%=btnPARemind.ClientID %>">
                        <span style="cursor: hand">Send PARemind Mail</span></label>
                </td>
            </tr>
        </table>
    </div>
    <div runat="server" id="toolbar" class="toolbar">
        <table style="height: 100%" cellpadding="0" cellspacing="0">
            <tr>
                <td runat="server" id="btnPass" style="padding: 0 10; height: 30;" onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/schedule.png" ID="btnPassCurrent"
                        runat="server" AlternateText="Pass the approval" OnClick="btnPass_Click" ImageAlign="AbsMiddle" />
                    <label for="<%=btnPassCurrent.ClientID %>">
                        <span style="cursor: hand">Pass</span></label>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td style="padding: 0 10; height: 30;" runat="server" id="btnReject" onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/addtofavorite.png" ID="btnRejectCurrent"
                        runat="server" AlternateText="Reject the PAs" OnClick="btnReject_Click" ImageAlign="AbsMiddle" />
                    <label for="<%=btnRejectCurrent.ClientID %>">
                        <span style="cursor: hand">Reject</span></label>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td style="padding: 0 10; height: 30;" runat="server" id="btnInvalid" onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/remove.png" ID="btnInvalidCurrent"
                        runat="server" AlternateText="Forward to mentor" OnClick="btnInvalid_Click" ImageAlign="AbsMiddle" />
                    <label for="<%=btnInvalidCurrent.ClientID %>">
                        <span style="cursor: hand">Invalid</span></label>
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="gvApprovingList" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None"
        BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%" AllowPaging="True" OnPageIndexChanging="gvApprovingList_PageIndexChanging"
        OnSelectedIndexChanging="gvApprovingList_SelectedIndexChanging" PageSize="20">
        <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                    <input type="checkbox" name="__selall__" id="__selall__" onclick="selectAll()" />
                </HeaderTemplate>
                <ItemTemplate>
                    <input type="checkbox" name="__selPA__" value='<%# Eval("id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField HeaderStyle-HorizontalAlign="Left" HeaderText="Name" DataTextField="InternName" DataNavigateUrlFormatString="~/ShowApplication.aspx?applicant={0}&tab=2&paid={1}"
                DataNavigateUrlFields="Applicantid,id">
                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:HyperLinkField>
            <asp:TemplateField>
                <ItemTemplate>
                    <%#  CheckInFormResourceManager.IdToText("Groups", int.Parse(DataBinder.Eval(Container.DataItem, "GroupId").ToString().Trim()))%>
                </ItemTemplate>
                <HeaderTemplate>Group</HeaderTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "MentorName").ToString().Trim()%>
                </ItemTemplate>
                <HeaderTemplate>Mentor</HeaderTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "MentorAlias").ToString().Trim()%>
                </ItemTemplate>
                <HeaderTemplate>Mentor Alias</HeaderTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <%#  GetApprovalStatusString(
                        int.Parse(DataBinder.Eval(Container.DataItem, "IsApproved").ToString().Trim()))%>
                </ItemTemplate>
                <HeaderTemplate>Approval Status</HeaderTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# GetPerformance(DataBinder.Eval(Container.DataItem, "OverrallEvaluation").ToString())%>
                </ItemTemplate>
                <HeaderTemplate>PA from Mentor</HeaderTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "CheckOutDate", "{0:yyyy-MM-dd}")%>
                </ItemTemplate>
                <HeaderTemplate>Check-out Date</HeaderTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>Deadline</HeaderTemplate>
                <ItemTemplate>
                    <%# GetDeadline(DataBinder.Eval(Container.DataItem, "InsertDate").ToString())%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60" ItemStyle-Width="60"
                HeaderText="Action" Text="Edit" DataNavigateUrlFormatString="~/MentorPA.aspx?PAId={0}" DataNavigateUrlFields="id">
                <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
            </asp:HyperLinkField>
        </Columns>
        <EmptyDataTemplate><p style="font-size: 12px">There is not any approving PA at all...</p></EmptyDataTemplate>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#9c969c" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
</asp:Content>
