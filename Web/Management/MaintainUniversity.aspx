<%@ Page Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true" CodeFile="MaintainUniversity.aspx.cs" Inherits="Management_MaintainUniversity" Title="SpringField" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
<div style="width: 100%;">
    <div id="ch_title" class="panel_title_expand">
        University List
    </div>
    <div id="ch_content" class="panel_content">
    <p>The following items are <span style="color: #0000cc">only</span> related to Key-In, Offline Hiring and Key Referral (not related to Check-In Form.)
    </p>
    Continent:
    <asp:DropDownList ID="ddlContinent" runat="server" CausesValidation="false" AutoPostBack="True" OnSelectedIndexChanged="ddlContinent_SelectedIndexChanged">   
    </asp:DropDownList>
        Region:
        <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
        </asp:DropDownList><br />
    <br />
    <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" CssClass="applicants_table" AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="gvUsers_PageIndexChanging">
        <Columns>
            <asp:TemplateField AccessibleHeaderText="Check" HeaderText="Check">
                <ItemTemplate>
                    <input type="checkbox" id="cb_ischeck" name="cb_ischeck" value="<%# Eval("Continent")%>:<%# Eval("Country")%>:<%# Eval("University")%>"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField AccessibleHeaderText="Continent" HeaderText="Continent">
                <ItemTemplate>
                <%# Eval("Continent")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField AccessibleHeaderText="Region" HeaderText="Region">
                <ItemTemplate>
                    <%# Eval("Country")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField AccessibleHeaderText="University" HeaderText="University">
                <ItemTemplate>
                    <%# Eval("University")%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            There are no university in the system.
        </EmptyDataTemplate>
        <PagerSettings Mode="NumericFirstLast" />
    </asp:GridView>
    <asp:Button ID="btnDeleteRecords" runat="server" Text="Delete Records"  CausesValidation="false" OnClick="btnDeleteRecords_Click" />
    
    <br />
    <br />
    <table>
    <tr>
        <td colspan="2">
        <span class="bold_font">Add University</span></td>
    </tr>
    <tr>
        <td style="width: 100px; height: 26px;">
        Continent:
        </td>
        <td style="height: 26px">
            <asp:TextBox ID="tbContinent" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvUserAlias" runat="server" ErrorMessage="Required!" ControlToValidate="tbContinent" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
        Region:
        </td>
        <td>
            <asp:TextBox ID="tbCountry" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvRealName" runat="server" ErrorMessage="Required!" ControlToValidate="tbCountry" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
        University:
        </td>
        <td>
        <asp:TextBox ID="tbUniversity" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvGroupName" runat="server" ErrorMessage="Required!" ControlToValidate="tbUniversity" ValidationGroup="AddUser"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnAddUniversity" runat="server" Text="Add"  CausesValidation="true" ValidationGroup="AddUser" OnClick="btnAddUniversity_Click" />
            <asp:Label EnableViewState="false" ID="lbMessage" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>
    </table>
    </div>
    </div>
</asp:Content>

