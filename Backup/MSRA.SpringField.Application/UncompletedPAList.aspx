<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="MSRA.SpringField.Application.UncompletedPAList" MasterPageFile="~/SpringfieldMaster.master" Codebehind="UncompletedPAList.aspx.cs" %>

<asp:Content ID="cntGeneralInfo" runat="server" ContentPlaceHolderID="mainPlaceHolder">    
<p style="font-size: 18px">PA In Process</p>
    <asp:GridView ID="gvCompletedPA" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%">
        <Columns>
            <%--<asp:TemplateField>
                <ItemTemplate>
                    <a id="A1" runat="server" href='<%# GetApplicantLink(DataBinder.Eval(Container.DataItem, "ApplicantId").ToString()) %>'>
                        <%# Eval("InternName").ToString()%>
                    </a>
                </ItemTemplate>
                <HeaderTemplate>
                    Name</HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Left" />                    
                    <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>--%>
            <asp:HyperLinkField HeaderStyle-HorizontalAlign="Left" HeaderText="Name" DataTextField="InternName" DataNavigateUrlFormatString="~/ShowApplication.aspx?applicant={0}&tab=2&paid={1}"
             DataNavigateUrlFields="Applicantid,id" />
            <asp:TemplateField>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "CheckOutDate", "{0:yyyy-MM-dd}")%>
                </ItemTemplate>
                <HeaderTemplate>
                    Check-out Date</HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# GetGroupNameByID(DataBinder.Eval(Container.DataItem, "GroupId").ToString().Trim())%>
                </ItemTemplate>
                <HeaderTemplate>
                    Group</HeaderTemplate>
                     <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="60" ItemStyle-Width="60">
                <ItemTemplate>
                    <a id="A2" runat="server" href='<%# GetEditLink(DataBinder.Eval(Container.DataItem, "id").ToString()) %>'>Edit </a>
                </ItemTemplate>
                <HeaderTemplate>Actions</HeaderTemplate>
                     <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
           <p style="font-size: 12px">
        There is not any processing PA at all...</p>
            </EmptyDataTemplate>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#9c969c" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
</asp:Content>
