<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SF_Mail._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="全部发送" OnClick="Button1_Click" />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <asp:LinqDataSource ID="MailLinqDataSource" runat="server" ContextTypeName="SF_Mail.MailDataClassesDataContext"
            EnableDelete="True" EnableInsert="True" EnableUpdate="True" TableName="sf_Emails"
            Where="IsSend == @IsSend">
            <WhereParameters>
                <asp:Parameter DefaultValue="0" Name="IsSend" Type="Int32" />
            </WhereParameters>
        </asp:LinqDataSource>
    </div>
    <asp:GridView ID="GridViewMailList" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" DataKeyNames="EmailId" DataSourceID="MailLinqDataSource"
        OnRowCommand="GridViewMailList_RowCommand">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="EmailId" HeaderText="EmailId" InsertVisible="False" ReadOnly="True" SortExpression="EmailId" />
            <asp:BoundField DataField="Priority" HeaderText="Priority" SortExpression="Priority" />
            <asp:BoundField DataField="BodyFormat" HeaderText="BodyFormat" SortExpression="BodyFormat" />
            <asp:BoundField DataField="From" HeaderText="From" SortExpression="From" />
            <asp:BoundField DataField="To" HeaderText="To" SortExpression="To" />
            <asp:BoundField DataField="Cc" HeaderText="Cc" SortExpression="Cc" />
            <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
            <asp:BoundField DataField="Body" HeaderText="Body" HtmlEncode="False" SortExpression="Body" />
            <asp:BoundField DataField="NextTryTime" HeaderText="NextTryTime" SortExpression="NextTryTime" />
            <asp:BoundField DataField="NumberOfTries" HeaderText="NumberOfTries" SortExpression="NumberOfTries" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="SendButton" runat="server" CommandName="Send" CommandArgument='<%# Eval("EmailId") %>'
                        Text='发送'  />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </form>
</body>
</html>
