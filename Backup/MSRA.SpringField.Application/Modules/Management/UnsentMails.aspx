<%@ Page Title="" Language="C#" MasterPageFile="~/SpringfieldMaster.master" AutoEventWireup="true"
    CodeBehind="UnsentMails.aspx.cs" Inherits="MSRA.SpringField.Application.Modules.Management.UnsentMails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="全部发送" OnClick="Button1_Click" />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
    <div>
        <asp:LinqDataSource ID="MailLinqDataSource" runat="server" ContextTypeName="MSRA.SpringField.Application.Config.Schemas.SpringFieldDataContext"
            EnableDelete="True" EnableInsert="True" EnableUpdate="True" TableName="sf_Emails"
            Where="IsSend == @IsSend">
            <WhereParameters>
                <asp:Parameter DefaultValue="0" Name="IsSend" Type="Int32" />
            </WhereParameters>
        </asp:LinqDataSource>
        <asp:GridView ID="GridViewMailList" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" DataKeyNames="EmailId" DataSourceID="MailLinqDataSource"
            OnRowCommand="GridViewMailList_RowCommand" CellPadding="4" 
            ForeColor="#333333" GridLines="None">
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="EmailId" HeaderText="EmailId" InsertVisible="False" ReadOnly="True"
                    SortExpression="EmailId" />
                <asp:BoundField DataField="Priority" HeaderText="Priority" SortExpression="Priority" />
                <asp:BoundField DataField="From" HeaderText="From" SortExpression="From" />
                <asp:BoundField DataField="To" HeaderText="To" SortExpression="To" />
                <asp:BoundField DataField="Cc" HeaderText="Cc" SortExpression="Cc" />
                <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
                <asp:BoundField DataField="Body" HeaderText="Body" HtmlEncode="False" SortExpression="Body" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="SendButton" runat="server" CommandName="Send" CommandArgument='<%# Eval("EmailId") %>'
                            Text='发送' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </div>
</asp:Content>
