<%@ Page Title="" Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="MSRA.SpringField.Application.Modules.Management.UserList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" runat="server">
<div class="panel_title_expand" style="width:100%">User List</div>
<div style="border:solid 1px #999;border-top:none;width:100%"> 
    <table cellpadding="0" cellspacing="0" width="100%"> 
        <tr>
            <td style="vertical-align:middle;padding:3 5">
                <div style="vertical-align:absmiddle">
                    User Name:<asp:TextBox runat="server" ID="txtRoleName"></asp:TextBox>
                    <asp:ImageButton runat="server" ID="ibSearch" ImageAlign="AbsMiddle" OnClick="ibSearch_Click" ImageUrl="~/Resource/Images/search.png" Width="24" Height="24" />
                </div>
            </td>          
            <td align="right">
            <div style="padding:5 5 3 5">
                    <a href="RoleEdit.aspx" style="color:Black"><img alt="Create new user" src="../../Resource/Images/create.png" width="24" height="24" align="absmiddle" style="margin:0 3"/>Create User</a>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton runat="server" ID="ImageButton1" OnClick="btnDeleteRecords_Click" ImageUrl="~/Resource/Images/delete.png" Width="24" Height="24" ImageAlign="AbsMiddle"/>
                <label for="<%=ImageButton1.ClientID %>"><span style="cursor:hand">Delete User(s)</span></label>
            </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border-left:none;border-right:none">
                <div style="width:100%;margin:3 3">                                                 
                    <asp:GridView HeaderStyle-Height="35" ID="gvUsers" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="15" 
                        OnRowCommand="gvUsers_RowCommand" 
                        CssClass="applicants_table" 
                        OnPageIndexChanging="gvUsers_PageIndexChanging" >
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <input type="checkbox" id="cb_ischeck" name="cb_ischeck" value='<%# Eval("UserId") %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <a href='RoleView.aspx?roleid=<%# Eval("UserId") %>'><%# Eval("UserName")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                                                                
                        <asp:BoundField HeaderText="Lowered User Name" DataField="LoweredUserName" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="Description" HeaderStyle-Width="400" DataField="Description" HeaderStyle-HorizontalAlign="Left" />
                        <asp:TemplateField AccessibleHeaderText="Action" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnRemove" runat="server" Text="Delete" CommandArgument='<%# Eval("RoleId") %>' CommandName="Remove"></asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("RoleId") %>' CommandName="Edit"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div style="width:100%;vertical-align:middle;text-align:center">
                            <img id="empty" width="64" height="64" src="Resource/Images/empty.png" style="vertical-align:absmiddle"/><br />
                            <span style="font-size:11">There is no role in this list...</span>
                        </div>                                          
                    </EmptyDataTemplate>
                </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
            <div style="padding:4 5">
                <a href="RoleEdit.aspx" style="color:Black"><img alt="Create new role" src="../../Resource/Images/create.png" width="24" height="24" align="absmiddle" style="margin:0 3"/>Create User</a>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:ImageButton runat="server" ID="btnDelete" OnClick="btnDeleteRecords_Click" ImageUrl="~/Resource/Images/delete.png" Width="24" Height="24" ImageAlign="AbsMiddle"/>
                <label for="<%=btnDelete.ClientID %>"><span style="cursor:hand">Delete User(s)</span></label>
            </div>
            </td>
        </tr>
    </table>
</div>
</asp:Content>
