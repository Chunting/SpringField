<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Management.Management_SiteRoleManager" MasterPageFile="~/SpringfieldMaster.master" Codebehind="SiteRoleManager.aspx.cs" %>

<asp:Content ID="cntRoleManager" runat="server" ContentPlaceHolderID="mainPlaceHolder">

    <script type="text/javascript">
        var tmpAlias = "";
        function SetCurrentUser(currObj) {
            document.getElementById('<%=txtAlias.ClientID %>').value = currObj["alias"];
            document.getElementById('<%=txtRealName.ClientID %>').value = currObj["name"];
            document.getElementById('<%=ddlRoles.ClientID %>').value = currObj["role"];

            document.getElementById("spAddUser").innerHTML = "Edit User";
            tmpAlias = currObj["alias"];
        }

        function SetButtonText() {
            if (this.value == "" || this.value == tmpAlias) {
                document.getElementById("spAddUser").innerHTML = "Add User";
            }
            else {
                document.getElementById("spAddUser").innerHTML = "Edit User";
            }
        }
</script>
<%--<div id="toolbar" style="border:solid 1px #000;text-align:center;vertical-align:middle;margin:8 5">
<table style="height:40px">
    <tr>
        <td>
            <asp:Button ID="Button2" runat="server" Text="Add User" OnClick="btnAddUser_Click" CausesValidation="true" ValidationGroup="AddUser" />
        </td>
        <td>&nbsp;</td>
        <td>
            <asp:Button ID="Button3" runat="server" Text="Remove Selected" OnClick="btnDeleteRecords_Click" CausesValidation="false" />
        </td>
    </tr>
</table>
</div>--%>
<table style="width:100%" cellpadding="0" cellspacing="0">
<tr>
    <td align="center"> 
        <div class="panel_title_expand">Add User To Role</div>
    </td>
</tr>
    <tr>
        <td>
            <div style="width:100%;border-bottom:solid 1px #888;border-right:solid 1px #888;border-left:solid 1px #888">
                <table cellpadding="0" cellspacing="0" width="100%" style="margin:5 5">
                    <tr style="height:30px">
                        <td style="width:110">Domain\Alias:</td>
                        <td style="width:300">
                            <asp:TextBox Width="180" ID="txtAlias" runat="server"></asp:TextBox><span style="color:Red">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required!" 
                                ControlToValidate="txtAlias" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                        </td>
                        <td style="width:150">Name:</td>
                        <td style="width:300">
                            <asp:TextBox Width="180" ID="txtRealName" runat="server"></asp:TextBox><span style="color:Red">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required!" 
                                ControlToValidate="txtRealName" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height:30px">
                        <td>Roles:</td>
                        <td style="width:auto" colspan="5">
                            <asp:DropDownList ID="ddlRoles" Width="210" runat="server">
                                <asp:ListItem Text="Group Manager" Value="GroupManager" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="UR" Value="UnivRelation"></asp:ListItem>
                                <asp:ListItem Text="Intern Recruiter" Value="InternRecruiter"></asp:ListItem>
                                <asp:ListItem Text="On Board Manager" Value="OnBoardManager"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="6">
                        <div style="width:120px;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                     <asp:ImageButton Width="24" Height="24" CssClass="img_icon" ImageUrl="~/Resource/Images/adduser.png" ID="btnAddUser"
                            runat="server" AlternateText="Add User" OnClick="btnAddUser_Click" 
                            CausesValidation="true" ImageAlign="AbsMiddle" />
                            <label for="<%=btnAddUser.ClientID %>"><span id="spAddUser" style="cursor:hand">Add User</span></label>
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
                        <div class="panel_title_expand">Users in Role List</div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <div id="notionbar" style="background-color:#FFFFE1;border:solid 1px #FEC951;height:50;vertical-align:middle;margin:5 0;text-align:left">
            <table style="width:100%;height:100%">
                <tr>
                    <td align="left" valign="middle">
                        <ul>
                            <li>The list below shows the users by role</li>
                        </ul>    
                    </td>
                </tr>
            </table>                
            </div>
            <div style="width:100%;border:solid 1px #888;">
                    <table style="width:100%;" cellpadding="0" cellspacing="0">                    
                        <tr>
                            <td>
                                <div style="width:100%;margin:5 5">
                                Please select a role:
                                <asp:DropDownList ID="ddlRoleType" runat="server" OnSelectedIndexChanged="ddlRoleType_SelectedIndexChanged" AutoPostBack="True" CausesValidation="false">
                                    <asp:ListItem Text="Group Manager" Value="GroupManager" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="UR" Value="UnivRelation"></asp:ListItem>
                                    <asp:ListItem Text="Intern Recruiter" Value="InternRecruiter"></asp:ListItem>
                                    <asp:ListItem Text="On Board Manager" Value="OnBoardManager"></asp:ListItem>
                                </asp:DropDownList>
                                </div>
                            </td>
                            <td onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';"
                    style="padding:0 5;height:30;" align="right">
                    <asp:ImageButton Width="24" Height="24" ImageAlign="AbsMiddle" ImageUrl="~/Resource/Images/deluser.png" 
                    ID="btnDeleteRecords" runat="server" Font-Bold="true" OnClick="btnDeleteRecords_Click" />
                    <label style="cursor:hand" for="<%=btnDeleteRecords.ClientID %>">Delete User(s)</label>                                        
                    </td>
                            
                    <%--<asp:Button ID="btnDeleteRecords" runat="server" Text="Delete User(s)" OnClick="btnDeleteRecords_Click" CausesValidation="false" />&nbsp;
                    
                </td>--%>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="width:100%;margin:5 5">                
                                    <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="15" OnRowCommand="gvUsers_RowCommand" CssClass="applicants_table" OnPageIndexChanging="gvUsers_PageIndexChanging" OnRowDataBound="gvUsers_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <input type="checkbox" id="cb_ischeck" name="cb_ischeck" value='<%# Eval("UserName") %>'/>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField AccessibleHeaderText="Name" HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lbRealName" runat="server"></asp:HyperLink>
                                                <asp:Literal ID="litUserAlias" runat="server"></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField AccessibleHeaderText="Role" HeaderText="Role">
                                            <ItemTemplate>
                                                <asp:Label ID="lbGroupName" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:BoundField AccessibleHeaderText="User Alias" HeaderText="User Alias" DataField="UserName" />                                    
                                        <asp:TemplateField AccessibleHeaderText="Action" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnRemove" runat="server" Text="Delete" CommandName="Remove"></asp:LinkButton>&nbsp;&nbsp;|&nbsp;&nbsp;
                                                <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" CommandName="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        There are no user in current role
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                        <td>
                                <%--<div style="width:100%;margin:5 5">
                                Please select a role:
                                <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True" CausesValidation="false">
                                    <asp:ListItem Text="Group Manager" Value="GroupManager" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="UR" Value="UnivRelation"></asp:ListItem>
                                    <asp:ListItem Text="Intern Recruiter" Value="InternRecruiter"></asp:ListItem>
                                    <asp:ListItem Text="On Board Manager" Value="OnBoardManager"></asp:ListItem>
                                </asp:DropDownList>
                                </div>--%>
                            </td>
                            <td align="right" onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                            onmouseout="this.style.borderWidth='0';"
                            style="padding:0 5;height:30;">
                            <asp:ImageButton Width="24" Height="24" ImageAlign="AbsMiddle" ImageUrl="~/Resource/Images/deluser.png" 
                            ID="Button1" runat="server" Font-Bold="true" OnClick="btnDeleteRecords_Click" />
                            <label style="cursor:hand" for="<%=Button1.ClientID %>">Delete User(s)</label>                                        
                            </td>
                            <%--<td align="right">
                            <asp:Button ID="Button1" runat="server" Text="Delete User(s)" OnClick="btnDeleteRecords_Click" CausesValidation="false" />&nbsp;
                            </td>--%>
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


    
       
</asp:Content>