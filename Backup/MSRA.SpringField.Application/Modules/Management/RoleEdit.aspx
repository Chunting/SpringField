<%@ Page Title="" Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" CodeBehind="RoleEdit.aspx.cs" Inherits="MSRA.SpringField.Application.Modules.Management.RoleEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" runat="server">
    <div class="panel_title_expand"><span runat="server" id="titleLable">Role Edit</span></div>
    <div style="width:100%;border:solid 1px #999;border-top:none">
    <table style="width:100%" cellpadding="0" cellspacing="0">    
        <tr>
        <td>
            <div style="width:100%;">
                <table cellpadding="0" cellspacing="0" width="100%" style="margin:5 5">
                    <tr style="height:30px">
                        <td class="fieldlable">Role Name:</td>
                        <td style="width:300">
                            <asp:TextBox Width="180" ID="txtRoleName" runat="server"></asp:TextBox><span style="color:Red">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required!" 
                                ControlToValidate="txtRoleName"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height:30px">
                        <td class="fieldlable">Description:</td>
                        <td style="width:auto" colspan="5">
                            <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="420" Height="120"></asp:TextBox>
                        </td>
                    </tr>
                   
                    <tr>
                        <td colspan="2">
                        
                        </td>
                    </tr>
                </table>
            </div>
        </td>    
    </tr>
    </table>
    </div>
    <div class="toolbar_small">
        <asp:ImageButton runat="server" ID="btnSaveRole" OnClick="btnSaveRole_Click" ImageUrl="~/Resource/Images/save.png" Width="24" Height="24" ImageAlign="AbsMiddle"/>
        <label for="<%=btnSaveRole.ClientID %>"><span style="cursor:hand">Save</span></label>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ImageButton runat="server" ID="btnSaveNewRole" OnClick="btnSaveNewRole_Click" ImageUrl="~/Resource/Images/save_new.png" Width="24" Height="24" ImageAlign="AbsMiddle"/>
        <label for="<%=btnSaveNewRole.ClientID %>"><span style="cursor:hand">Save & New</span></label>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <a href="RoleList.aspx" style="color:Black"><img alt="Edit current role" src="../../Resource/Images/list.png" width="24" height="24" align="absmiddle" style="margin:0 3"/>Return to List</a>
    </div>
</asp:Content>
