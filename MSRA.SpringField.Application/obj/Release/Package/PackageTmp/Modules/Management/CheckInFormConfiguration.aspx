<%@ Page Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Management.Management_CheckInFormConfiguration" Title="SpringField" Codebehind="CheckInFormConfiguration.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">

<table style="width:100%" cellpadding="0" cellspacing="0">
    <tr>
        <td>
        <div id="notionbar" style="background-color:#FFFFE1;border:solid 1px #FEC951;height:50;vertical-align:middle;margin:5 0;text-align:left">
            <table style="width:100%;height:100%">
                <tr>
                    <td style="text-indent:10">The following items are <span style="color: #0000ff">only </span>related to Check-In Form feature.</td>
                </tr>
                <tr>
                    <td align="left" valign="middle">
                        <ul>
                            <li>Hide Item: Click "Edit", uncheck "Display" column and click "Update".</li>
                            <li>Rename Item: Click "Edit", change "Name" column and click "Update".</li>
                            <li>Add Item: Click button "New Item", select the last page number and edit the new item.</li>
                            <li>Remove Item: Click "Delete". (this action may cause display problem for old filled out check-in form. Normally, please hide or rename.)</li>
                        </ul>
                    </td>
                </tr>
            </table>                
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <div id="ch_title" class="panel_title_expand">Check-In Form Item List</div>
            <div style="width:100%;border:solid 1px #888;border-top:none 0px #000">
                    <table style="width:100%;" cellpadding="0" cellspacing="0">                    
                        <tr>
                            <td>
                                <div style="width:100%;margin:5 5">
                                    <asp:DropDownList ID="ddlType" Width="120" runat="server" CausesValidation="false" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                           </td>     
                           <td align="right">
                                <%--<asp:Button ID="Button1" runat="server" Text="Add New Item"  CausesValidation="true" ValidationGroup="NewItem" OnClick="btnNewItem_Click" />--%>
                           </td>                       
                        </tr>
                        <tr> 
                            <td colspan="2">                            
                                <div style="width:100%;margin:5 5">             
                                    <asp:GridView ID="gvItems" HeaderStyle-Height="30" runat="server" AllowSorting="true" OnSorting="gvItems_Sorting" AllowPaging="True" CssClass="applicants_table" AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="gvItems_PageIndexChanging" 
                                        OnRowDeleting="gvItems_RowDeleting" OnRowEditing="gvItems_RowEditing"
                                        OnRowUpdating="gvItems_RowUpdating" OnRowCancelingEdit="gvItems_RowCancelingEdit">
                                        <Columns>
                                            <asp:TemplateField AccessibleHeaderText="ID" HeaderText="ID" HeaderStyle-Width="100" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                                    
                                            <asp:TemplateField AccessibleHeaderText="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="tbColName" runat="server" Text='<%# Bind("name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                <%# Eval("Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                                    
                                            <asp:TemplateField AccessibleHeaderText="Display" HeaderText="Display" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# Eval("Display")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="cbColDisplay" runat="server" Checked='<%#Eval("Display").ToString() == "true" ? true : false%>'/>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField HeaderText="Edit" ShowEditButton="True" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center"/>
                                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center" />
                                        </Columns>                                                
                                        <EmptyDataTemplate>
                                                    There are no groups.
                                                </EmptyDataTemplate>
                                        <PagerSettings Mode="NumericFirstLast" />
                                        
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        
                                        
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr style="height:30px">
                            <td colspan="2" align="right">
                                <%--<asp:Button ID="btnNewItem" runat="server" Text="Add New Item"  CausesValidation="true" ValidationGroup="NewItem" OnClick="btnNewItem_Click" />--%>
                            </td>
                        </tr>
                    </table>
            </div>
        </td>
    </tr>
</table>
</asp:Content>