<%@ Page Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" CodeFile="MaintainGroup.aspx.cs" Inherits="Management_MaintainGroup" Title="SpringField" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
<div style="width: 100%;">
    <div id="ch_title" class="panel_title_expand">
        Group List
    </div>
    <div id="ch_content" class="panel_content">
     <p>The following items are <span style="color: #0000cc">only</span> related to Key-In, Offline Hiring and Key Referral (not related to Check-In Form.)
        </p>
     <p>If this list does <span style="color: #0000cc">not</span> match resume collector, the groups not in this list will lost after key-in editing.</p>
        <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" CssClass="applicants_table" AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="gvUsers_PageIndexChanging">
        <Columns>
            <asp:TemplateField AccessibleHeaderText="Check" HeaderText="Check">
                <ItemTemplate>
                    <input type="checkbox" id="cb_ischeck" name="cb_ischeck" value="<%# Eval("Group")%>"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField AccessibleHeaderText="Continent" HeaderText="Continent">
                <ItemTemplate>
                <%# Eval("Group")%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            There are no groups in the system.
        </EmptyDataTemplate>
        <PagerSettings Mode="NumericFirstLast" />
    </asp:GridView>
    <asp:Button ID="btnDeleteRecords" runat="server" Text="Delete Records"  CausesValidation="false" OnClick="btnDeleteRecords_Click" />
    
    <br />
    <br />
    <table>
    <tr>
        <td colspan="2">
        <span class="bold_font">Add Group</span></td>
    </tr>
    <tr>
        <td style="width: 100px; height: 26px;">
        Group Name:
        </td>
        <td style="height: 26px">
            <asp:TextBox ID="tbGroup" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvGroup" runat="server" ErrorMessage="Required!" ControlToValidate="tbGroup" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnAddGroup" runat="server" Text="Add"  CausesValidation="true" ValidationGroup="AddUser" OnClick="btnAddGroup_Click" />
            <asp:Label EnableViewState="false" ID="lbMessage" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>
    </table>
    </div>
    </div>
</asp:Content>

