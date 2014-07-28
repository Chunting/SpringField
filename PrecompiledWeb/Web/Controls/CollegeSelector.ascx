<%@ control language="C#" autoeventwireup="true" inherits="CollegeSelector, App_Web_eecyfkj6" %>
<asp:DropDownList ID="ddlCollegeList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCollegeList_SelectedIndexChanged">
</asp:DropDownList><br />
<asp:UpdatePanel ID="u1" runat="server"><ContentTemplate>
<asp:TextBox ID="tbCollegeName" runat="server" Enabled="false" Width="495px" CssClass="required_input"></asp:TextBox><br />
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="ddlCollegeList" EventName="SelectedIndexChanged" />
</Triggers></asp:UpdatePanel>
<asp:RequiredFieldValidator ControlToValidate="tbCollegeName" ID="rfvCollegeName" runat="server" ErrorMessage="Required!"></asp:RequiredFieldValidator>