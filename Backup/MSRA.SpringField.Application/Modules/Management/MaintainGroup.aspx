<%@ Page Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Management.Management_MaintainGroup" Title="SpringField" Codebehind="MaintainGroup.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
    <table style="width:100%" cellpadding="0" cellspacing="0">
<tr>
    <td align="center">
        <div class="panel_title_expand">Add Group</div>
    </td>
</tr>
    <tr>
        <td>
            <div style="width:100%;border-bottom:solid 1px #888;border-right:solid 1px #888;border-left:solid 1px #888">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin:5 5">
                    <tr style="height:30px">
                        <td style="width:10%" align="right">Group Name:</td>
                            <td style="width:300">
                                <asp:TextBox ID="txtGroup" runat="server" Width="226px"></asp:TextBox><span style="color:Red">*</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ErrorMessage="Required!" ControlToValidate="txtGroup"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                Short Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtShortName" runat="server"></asp:TextBox><span style="color:Red">*</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ErrorMessage="Required!" ControlToValidate="txtShortName"></asp:RequiredFieldValidator>
                                
                            </td>
                            <td align="left">
                            <div style="width:120px;"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                     <asp:ImageButton Width="24" Height="24" CssClass="img_icon" ImageUrl="~/Resource/Images/addgroup.png" ID="btnAddUser"
                            runat="server" AlternateText="Add Group" OnClick="btnAddGroup_Click" 
                            CausesValidation="true" ImageAlign="AbsMiddle" />
                            <label for="<%=btnAddUser.ClientID %>"><span style="cursor:hand">Add Group</span></label>
                    </div>
                            
                                <%--<asp:Button ID="btnAddUser" runat="server" Text="Add Group" OnClick="btnAddGroup_Click" CausesValidation="true" ValidationGroup="AddUser" />--%>
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
                        <div class="panel_title_expand">Group List</div></td></tr></table></td></tr>
     <tr>
        <td>
            <div id="notionbar" style="background-color:#FFFFE1;border:solid 1px #FEC951;height:50;vertical-align:middle;margin:5 0;text-align:left">
            <table style="width:100%;height:100%">
                <tr>
                    <td align="left" valign="middle">
                        <ul>
                            <li><p>The following items are <span style="color: #0000cc">only</span> related to Key-In, Offline Hiring and Key Referral (not related to Check-In Form.)</p></li>
                            <li><p>If this list does <span style="color: #0000cc">not</span> match resume collector, the groups not in this list will lost after key-in editing.</p></li>
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
                                        <td>Group:</td>
                                        <td><asp:TextBox ID="txtGroupFilterValue" runat="server" Width="380"></asp:TextBox></td>
                                        <td>
                                            <%--<asp:Button ID="btnSearch" runat="server" Text="Search" Width="120" CausesValidation="false" OnClick="btnSearch_Click"/>--%>
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
                                </div>
                           </td>
                            <td align="right">
                    <asp:Button ID="btnDeleteTop" runat="server" Text="Delete Group(s)"  CausesValidation="false" OnClick="btnDeleteRecords_Click" />&nbsp;
                </td>
                        </tr>
                        <tr> 
                            <td colspan="2">                            
                                <div style="width:100%;margin:5 5">             
                                <asp:GridView ID="gvGroup" HeaderStyle-Height="30" runat="server" OnRowCommand="gvGroup_RowCommand" AllowPaging="True" CssClass="applicants_table" PageSize="15"
                                        AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="gvGroup_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <input type="checkbox" id="cb_ischeck" name="cb_ischeck" value="<%# Eval("Group")%>"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Group" DataField="Group" />
                                        <asp:TemplateField AccessibleHeaderText="Action" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton CausesValidation="false" ID="btnRemove" runat="server" Text="Delete" CommandName="Remove"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        There are no groups in the system.
                                    </EmptyDataTemplate>
                                    <PagerSettings Mode="NumericFirstLast" />
                                </asp:GridView>
                                </div></td></tr>
                                <tr style="height:30px">
                            <td></td>
                            <td align="right">
                            <asp:Button ID="btnDeleteBottom" runat="server" Text="Delete Group(s)"  CausesValidation="false" OnClick="btnDeleteRecords_Click" />
                            </td>
                        </tr>
                    </table>
            </div>
            <div id="statusbar" style="background-color:#FFFFE1;border:solid 1px #FEC951;height:30;vertical-align:middle;margin:5 0;text-align:left">
            <table style="width:100%;height:100%">
                <tr>
                    <td align="left" valign="middle">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            </div>
        </td>
    </tr>
</table>
</asp:Content>

