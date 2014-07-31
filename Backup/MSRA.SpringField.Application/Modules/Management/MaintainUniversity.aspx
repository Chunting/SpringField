<%@ Page Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Management.Management_MaintainUniversity" Title="SpringField" Codebehind="MaintainUniversity.aspx.cs" %>
<%@ Register TagPrefix="SFUserControl" TagName="CollegeSelector" Src="~/Controls/CollegeSelector.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">

<table style="width:100%" cellpadding="0" cellspacing="0">
<tr>
    <td align="center">
        <div class="panel_title_expand">Add University</div>
    </td>
</tr>
    <tr>
        <td>
            <div style="width:100%;border-bottom:solid 1px #888;border-right:solid 1px #888;border-left:solid 1px #888">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin:5 5">
                    <tr style="height:30px">
                        <td style="width:60" align="left">Continent:</td>
                        <td style="width:300">
                            <asp:TextBox ID="txtContinent" runat="server" /><span style="color:Red">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required!" 
                                ControlToValidate="txtContinent" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                        </td>
                        <td style="width:60" align="right">Country:</td>
                        <td style="width:300">
                            <asp:TextBox Width="180" ID="txtRegion" runat="server"></asp:TextBox><span style="color:Red">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required!" 
                                ControlToValidate="txtRegion" ValidationGroup="VG2"></asp:RequiredFieldValidator>
                        </td>
                        </tr>
                        <tr style="height:30px">
                            <td align="left">University:</td>
                            <td style="width:auto" colspan="3">
                            <asp:TextBox Width="655" ID="txtUniversity" runat="server" /><span style="color:Red">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required!" 
                                ControlToValidate="txtUniversity" ValidationGroup="VG2"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="btnAddUser" runat="server" Text="Add University" OnClick="btnAddUniversity_Click" CausesValidation="true" ValidationGroup="AddUser" />
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
                        <div class="panel_title_expand">University List</div></td></tr></table></td></tr><tr>
        <td>
            <div id="notionbar" style="background-color:#FFFFE1;border:solid 1px #FEC951;height:50;vertical-align:middle;margin:5 0;text-align:left">
            <table style="width:100%;height:100%">
                <tr>
                    <td align="left" valign="middle">
                        <ul>
                            <li><p>The following items are <span style="color: #0000cc">only</span> related to Key-In, Offline Hiring and Key Referral (not related to Check-In Form.)</p></li>
                        </ul>    
                    </td>
                </tr>
            </table>                
            </div>
            <div style="width:100%;border:solid 1px #888">
                    <table style="width:100%;" cellpadding="0" cellspacing="0">                    
                        <tr>
                            <td>
                                <div style="width:100%;margin:5 5">
                                <table>
                                    <tr>
                                        <td>Continent:</td>
                                        <td><asp:DropDownList Width="210" ID="ddlContinent" runat="server" CausesValidation="false" AutoPostBack="True" OnSelectedIndexChanged="ddlContinent_SelectedIndexChanged"></asp:DropDownList></td>
                                        <td>&nbsp;</td>
                                        <td>Region:</td>
                                        <td><asp:DropDownList Width="210" ID="ddlCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList></td>
                                    </tr>
                                </table>
                                </div>
                           </td>
                            <td align="right">
                    <asp:Button ID="Button1" runat="server" Text="Delete University(s)"  CausesValidation="false" OnClick="btnDeleteRecords_Click" />&nbsp;
                </td>
                        </tr>
                        <tr> 
                            <td colspan="2">                            
                                <div style="width:100%;margin:5 5">             
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="15" OnRowCommand="gvUsers_RowCommand" CssClass="applicants_table" OnPageIndexChanging="gvUsers_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50">
                                    <ItemTemplate>
                                        <input type="checkbox" id="cb_ischeck" name="cb_ischeck" value="<%# Eval("Continent")%>:<%# Eval("Country")%>:<%# Eval("University")%>"/></ItemTemplate></asp:TemplateField>
                                        <asp:TemplateField AccessibleHeaderText="Continent" HeaderText="Continent">
                                    <ItemTemplate>
                                    <%# Eval("Continent")%></ItemTemplate></asp:TemplateField>
                                        <asp:TemplateField AccessibleHeaderText="Country" HeaderText="Country">
                                    <ItemTemplate>
                                        <%# Eval("Country")%></ItemTemplate></asp:TemplateField>
                                        <asp:TemplateField AccessibleHeaderText="University" HeaderText="University">
                                    <ItemTemplate>
                                        <%# Eval("University")%></ItemTemplate></asp:TemplateField>
                                        <asp:TemplateField AccessibleHeaderText="Action" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnRemove" runat="server" Text="Delete" CommandName="Remove"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>There are no university</EmptyDataTemplate>
                                </asp:GridView>
                                </div></td></tr>
                                <tr style="height:30px">
                            <td></td>
                            <td align="right">
                            <asp:Button ID="btnDeleteRecords" runat="server" Text="Delete University(s)"  CausesValidation="false" OnClick="btnDeleteRecords_Click" />
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

</asp:Content>