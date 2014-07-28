<%@ page language="C#" autoeventwireup="true" inherits="Management_SiteRoleManager, App_Web_7bnxe2jf" masterpagefile="~/SpringfieldMaster.master" %>


<asp:Content ID="cntRoleManager" runat="server" ContentPlaceHolderID="mainPlaceHolder">
    <div style="width: 80%;">
    <div id="ch_title" class="panel_title_expand">
        Role List
    </div>
    <div id="ch_content" class="panel_content">
    <br />
    Please select a role:
    <asp:DropDownList ID="ddlRoleType" runat="server" OnSelectedIndexChanged="ddlRoleType_SelectedIndexChanged" AutoPostBack="True" CausesValidation="false">
        <asp:ListItem Text="Group Manager" Value="GroupManager" Selected="True"></asp:ListItem>
        <asp:ListItem Text="UR" Value="UnivRelation"></asp:ListItem>
        <asp:ListItem Text="Intern Recruiter" Value="InternRecruiter"></asp:ListItem>
        <asp:ListItem Text="On Board Manager" Value="OnBoardManager"></asp:ListItem>
    </asp:DropDownList><br />
    <br />
    <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowCommand="gvUsers_RowCommand" CssClass="applicants_table" OnPageIndexChanging="gvUsers_PageIndexChanging" OnRowDataBound="gvUsers_RowDataBound">
        <Columns>
            <asp:TemplateField AccessibleHeaderText="Check" HeaderText="Check">
                <ItemTemplate>
                    <input type="checkbox" id="cb_ischeck" name="cb_ischeck" value="<%# Container.DataItem.ToString() %>"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField AccessibleHeaderText="Name" HeaderText="Name">
                <ItemTemplate>
                    <asp:Label ID="lbRealName" runat="server"></asp:Label><asp:Literal ID="litUserAlias"
                        runat="server"></asp:Literal>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField AccessibleHeaderText="Group" HeaderText="Group">
                <ItemTemplate>
                    <asp:Label ID="lbGroupName" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField AccessibleHeaderText="UserAlias" HeaderText="UserAlias">
                <ItemTemplate>
                    <%# Container.DataItem.ToString() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField AccessibleHeaderText="Action" HeaderText="Action">
                <ItemTemplate>
                    <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="Remove" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            There are no user in current role
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:Button ID="btnDeleteRecords" runat="server" Text="Remove Selected" OnClick="btnDeleteRecords_Click" CausesValidation="false" />
    
    <br />
    <br />
    <table>
    <tr>
        <td colspan="2">
        <span class="bold_font">Add User to role</span>
        </td>
    </tr>
    <tr>
        <td style="width: 100px;">
        (Domain\Alias:):
        </td>
        <td>
            <asp:TextBox ID="tbNewUser" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvUserAlias" runat="server" ErrorMessage="Required!" ControlToValidate="tbNewUser" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
        Real Name:
        </td>
        <td>
            <asp:TextBox ID="tbRealName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvRealName" runat="server" ErrorMessage="Required!" ControlToValidate="tbRealName" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
        Group Name:
        </td>
        <td>
        <asp:TextBox ID="tbGroupName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvGroupName" runat="server" ErrorMessage="Required!" ControlToValidate="tbGroupName" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnAddUser" runat="server" Text="Add/Update" OnClick="btnAddUser_Click" CausesValidation="true" ValidationGroup="AddUser" />
        </td>
    </tr>
    </table>
    </div>
    </div>
       
</asp:Content>