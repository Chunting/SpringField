<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CollegeSelector.ascx.cs" Inherits="CollegeSelector" %>
<asp:DropDownList ID="ddlCollegeList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCollegeList_SelectedIndexChanged">
</asp:DropDownList><br />
<asp:TextBox ID="tbCollegeName" runat="server" Enabled="false" Width="495px" CssClass="required_input"></asp:TextBox><br />
<asp:RequiredFieldValidator ControlToValidate="tbCollegeName" ID="rfvCollegeName" runat="server" ErrorMessage="Required!"></asp:RequiredFieldValidator>