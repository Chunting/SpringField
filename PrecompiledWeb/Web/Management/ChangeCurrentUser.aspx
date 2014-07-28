<%@ page language="C#" masterpagefile="~/SpringfieldMaster.master" autoeventwireup="true" inherits="Management_ChangeCurrentUser, App_Web_7bnxe2jf" title="SpringField" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
    Current User Name<br />
    &nbsp;
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
    Change To:<br />
    &nbsp;<asp:RadioButton ID="RadioButton1" runat="server" Text="fareast\xcui" ValidationGroup="v" GroupName="g" /><br />
    &nbsp;<asp:RadioButton ID="RadioButton2" runat="server" Text="fareast\bainguo" ValidationGroup="v" GroupName="g" /><br />
    &nbsp;<asp:RadioButton ID="RadioButton3" runat="server" Text="fareast\wenchen" ValidationGroup="v" GroupName="g" />
    <br />
    &nbsp;<asp:RadioButton ID="RadioButton4" runat="server" Text="Other:" ValidationGroup="v" GroupName="g" /><br />
    &nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
    &nbsp;<asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
</asp:Content>

