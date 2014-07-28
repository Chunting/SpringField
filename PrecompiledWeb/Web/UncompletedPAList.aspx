<%@ page language="C#" autoeventwireup="true" inherits="UncompletedPAList, App_Web_wkngwpi-" masterpagefile="~/SpringfieldMaster.master" %>

<asp:Content ID="cntGeneralInfo" runat="server" ContentPlaceHolderID="mainPlaceHolder">
    <p style="font-size: 18px">
        Uncompleted PA List:</p>
    <asp:GridView ID="gvCompletedPA" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <a id="A1" runat="server" href='<%# GetApplicantLink(DataBinder.Eval(Container.DataItem, "ApplicantId").ToString()) %>'>
                        <%# Eval("InternName").ToString()%>
                    </a>
                </ItemTemplate>
                <HeaderTemplate>
                    Name</HeaderTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />                    
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "CheckOutDate", "{0:yyyy-MM-dd}")%>
                </ItemTemplate>
                <HeaderTemplate>
                    Check-out Date</HeaderTemplate>
                    <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# GetGroupNameByID(DataBinder.Eval(Container.DataItem, "GroupId").ToString().Trim())%>
                </ItemTemplate>
                <HeaderTemplate>
                    Group</HeaderTemplate>
                     <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <a id="A2" runat="server" href='<%# GetEditLink(DataBinder.Eval(Container.DataItem, "id").ToString()) %>'>
                        Edit </a>
                </ItemTemplate>
                <HeaderTemplate>
                    Actions</HeaderTemplate>
                     <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No Completed PA</EmptyDataTemplate>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#9c969c" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
</asp:Content>
