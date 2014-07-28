<%@ Page Title="" Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" CodeBehind="RoleView.aspx.cs" Inherits="MSRA.SpringField.Application.Modules.Management.RoleView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" runat="server">

<div class="panel_title_expand"><span runat="server" id="titleLable">Role View</span></div>
<div style="width:100%;border:solid 1px #999;border-top:none">
    <table style="width:100%" cellpadding="0" cellspacing="0">
        <tr>
        <td>
            <div style="width:100%;">
                <table cellpadding="0" cellspacing="0" width="100%" style="margin:5 5">
                    <tr style="height:30">
                        <td class="fieldlable">Role Name:</td>
                        <td style="width:300"><asp:Label runat="server" ID="lblRoleName" CssClass="viewlabel"></asp:Label></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr style="height:30px">
                        <td class="fieldlable">Description:</td>
                        <td style="width:auto" colspan="3"><asp:Label CssClass="viewlabel" runat="server" ID="lblDesc"></asp:Label></td>
                    </tr>
                    
                    <tr>
                        <td colspan="4">
                        
                        </td>
                    </tr>
                </table>
            </div>
        </td>    
    </tr>
    </table>
 </div>
 <div class="toolbar_small">
    <a style="color:Black" href="RoleEdit.aspx?roleid=<%=RoleID %>"><img alt="Edit current role" src="../../Resource/Images/edit.png" width="24" height="24" align="absmiddle" style="margin:0 3"/>Edit Current Role</a>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:ImageButton runat="server" ID="btnDelete" OnClick="btnDelete_Click" ImageUrl="~/Resource/Images/delete.png" Width="24" Height="24" ImageAlign="AbsMiddle"/>
    <label for="<%=btnDelete.ClientID %>"><span style="cursor:hand">Delete Current Role</span></label>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <a href="RoleList.aspx" style="color:Black"><img alt="Edit current role" src="../../Resource/Images/list.png" width="24" height="24" align="absmiddle" style="margin:0 3"/>Return to List</a>
 </div>
</asp:Content>
