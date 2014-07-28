<%@ Page Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" CodeFile="CheckInFormConfiguration.aspx.cs" Inherits="Management_CheckInFormConfiguration" Title="SpringField" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
    <div id="ch_title" class="panel_title_expand">
        Check-In Form Item List
    </div>
    <div id="ch_content" class="panel_content">
        The following items are <span style="color: #0000ff">only </span>related to Check-In
        Form feature.<br />
        <ul>
            <li>Hide Item: Click "Edit", uncheck "Display" column and click "Update".</li>
            <li>Rename Item: Click "Edit", change "Name" column and click "Update".</li>
            <li>Add Item: Click button "New Item", select the last page number and edit the new item.</li>
            <li>Remove Item: Click "Delete". (this action may cause display problem for old filled out check-in form. Normally, please hide or rename.)</li>
        </ul>
        <p>
            <asp:DropDownList ID="ddlType" runat="server" CausesValidation="false" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">   
            </asp:DropDownList>
        </p>
<asp:GridView ID="gvItems" runat="server" AllowSorting="true" OnSorting="gvItems_Sorting" AllowPaging="True" CssClass="applicants_table" AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="gvItems_PageIndexChanging" 
OnRowDeleting="gvItems_RowDeleting" OnRowEditing="gvItems_RowEditing"
OnRowUpdating="gvItems_RowUpdating" OnRowCancelingEdit="gvItems_RowCancelingEdit">
        <Columns>
            <asp:TemplateField AccessibleHeaderText="Id" HeaderText="Id">
                <ItemTemplate>
                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField AccessibleHeaderText="Name" HeaderText="Name">
                <EditItemTemplate>
                    <asp:TextBox ID="tbColName" runat="server" Text='<%# Bind("name") %>'></asp:TextBox>
                </EditItemTemplate>

                <ItemTemplate>
                <%# Eval("Name")%>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField AccessibleHeaderText="Display" HeaderText="Display">
                <ItemTemplate>
                    <%# Eval("Display")%>
                </ItemTemplate>
                <EditItemTemplate>
                    
                    <asp:CheckBox ID="cbColDisplay" runat="server" Checked='<%#Eval("Display").ToString() == "true" ? true : false%>'/>
                    
                </EditItemTemplate>

            </asp:TemplateField>
            <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
        </Columns>
        
        <EmptyDataTemplate>
            There are no groups.
        </EmptyDataTemplate>
        <PagerSettings Mode="NumericFirstLast" />
          <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    <br />
    <br />

    <asp:Button ID="btnNewItem" runat="server" Text="New Item"  CausesValidation="true" ValidationGroup="NewItem" OnClick="btnNewItem_Click" />
    </div>
</asp:Content>