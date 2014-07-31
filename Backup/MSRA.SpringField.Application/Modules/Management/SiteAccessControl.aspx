<%@ Page Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Management.Management_SiteAccessControl" Title="SpringField" Codebehind="SiteAccessControl.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
<script type="text/javascript">
    function SetCurrentUser(currObj) {
        document.getElementById('<%=tb_alias.ClientID %>').value = currObj["alias"];
        document.getElementById('<%=ddl_Roles.ClientID %>').value = currObj["rolename"];
    }
</script>

<table style="width:100%" cellpadding="0" cellspacing="0">
<tr>
    <td align="center">
        <div class="panel_title_expand">Add User To Role</div>
    </td>
</tr>
    <tr>
        <td>
            <div style="width:100%;border-bottom:solid 1px #888;border-right:solid 1px #888;border-left:solid 1px #888">
                <table cellpadding="0" cellspacing="0" width="100%" style="margin:5 5;height:30px">
                    <tr>
                        <td style="width:110">Domain\Alias:</td>
                        <td style="width:380"><asp:TextBox Width="210" ID="tb_alias" runat="server"></asp:TextBox></td>
                        <td>Roles:</td>
                        <td style="width:160"><asp:DropDownList ID="ddl_Roles" Width="210" runat="server"></asp:DropDownList></td>            
                        <td align="center">
                            <div style="width:120px;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                     <asp:ImageButton Width="24" Height="24" CssClass="img_icon" ImageUrl="~/Resource/Images/adduser.png" ID="btn_AddRole"
                            runat="server" AlternateText="Add User" OnClick="btn_AddRole_Click" 
                            CausesValidation="true" ImageAlign="AbsMiddle" />
                            <label for="<%=btn_AddRole.ClientID %>"><span style="cursor:hand">Add User</span></label>
                    </div>
                        </td>
                    </tr>
                </table>
            </div>
        </td>    
    </tr>
</table>
<hr class="split_line" />
<table style="width:100%" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <table style="width:100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center">
                        <div class="panel_title_expand">User List</div>
                    </td>
                </tr>
            </table>
            
            
        </td>
    </tr>
    <tr>
        <td>
        <div id="notionbar" class="infobar">
            <table style="width:100%;height:100%">
                <tr>
                    <td align="left" valign="middle">
                        <ul>
                            <li>The list below shows the users and the role they belong to.</li>
                        </ul>    
                    </td>
                </tr>
            </table>                
            </div>
            <div style="width:100%;border:solid 1px #888;">
                    <table style="width:100%;" cellpadding="0" cellspacing="0">
                    <tr>
                            <td>
                            <table>
                            <tr>
                                <td>
                             <div style="width:100%;margin:5 5">
                                <asp:DropDownList ID="ddlFilterCondition" runat="server" Width="120">
                                    <asp:ListItem Text="User Alias" Value="UserName" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Role Name" Value="RoleName"></asp:ListItem>
                                </asp:DropDownList>&nbsp;&nbsp;
                                <asp:TextBox ID="txtConditionValue" runat="server" Width="180"></asp:TextBox>&nbsp;&nbsp;
                                
                                </td>
                           <td>
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
                                
                               <%-- <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />--%>
                                </div>
                            </td>
                        </tr>
                    <tr>
                <td>
                 <asp:Label ID="lblRecordSize" runat="server" Visible="false"></asp:Label>   
                </td>
            </tr>
                    <tr>
                <td>
                <div style="width:100%;margin:5 5">
                    <asp:GridView OnRowDataBound="gvGridView_RowDataBound" ID="gvRoleList" HeaderStyle-Height="30" runat="server" AutoGenerateColumns="false" PageSize="15" 
                        AllowPaging="true" Width="100%" 
                        onpageindexchanging="gvRoleList_PageIndexChanging" OnRowCommand="gvRoleList_RowCommand" CssClass="applicants_table">
                        <Columns>
                            <%--<asp:TemplateField HeaderText="User Alias" HeaderStyle-Width="200" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <a href="javascript:SetCurrentUser({'alias':'<%#Eval("UserName") %>','rolename':'<%# Eval("RoleName") %>'})"><%#Eval("UserName") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField HeaderStyle-Width="200" ItemStyle-Width="200" HeaderStyle-HorizontalAlign="Left" HeaderText="User Alias" DataField="UserName"/>
                            <asp:BoundField HeaderStyle-HorizontalAlign="Left" HeaderText="Role Name" DataField="RoleName"/>
                            <asp:BoundField HeaderStyle-HorizontalAlign="Left" HeaderText="Role Description" DataField="Description" />
                            <%--Add delete by Yuanqin,2011.5.7--%>
                            <asp:TemplateField AccessibleHeaderText="Action" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnRemove" runat="server" Text="Delete" CommandName="Remove"></asp:LinkButton>
                                    </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            There is no record fit for your filter condition now...
                        </EmptyDataTemplate>                                
                    </asp:GridView>
                    </div>
                </td>
            </tr>
                </table>
            </div>
            <div id="statusbar" style="background-color:#FFFFE1;border:solid 1px #FEC951;height:30;vertical-align:middle;margin:5 0;text-align:left">
            <table style="width:100%;height:100%">
                <tr>
                    <td align="left" valign="middle">
                        <asp:Label ID="lbMessage" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            </div>
        </td>
    </tr>
    
</table>

<asp:Label ID="lb_msg" runat="server"></asp:Label>
</asp:Content>

