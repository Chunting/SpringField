<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="MSRA.SpringField.Application.CompletedPAList" MasterPageFile="~/SpringfieldMaster.master" Codebehind="CompletedPAList.aspx.cs" %>

<asp:Content ID="cntGeneralInfo" runat="server" ContentPlaceHolderID="mainPlaceHolder">
    <p style=" font-size:18px">Completed PA List:</p>
    <asp:GridView ID="gvCompletedPA" runat="server" 
        OnRowDataBound="gvCompletedPA_RowDataBound" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
        CellPadding="3" GridLines="Vertical" Width="100%" 
        onpageindexchanging="gvCompletedPA_PageIndexChanging">
        <Columns>
           <%-- <asp:TemplateField>
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
            <asp:TemplateField>
                <ItemTemplate>
                    <%# GetPerformance(DataBinder.Eval(Container.DataItem, "OverrallEvaluation").ToString().Trim())%>
                </ItemTemplate>
                <HeaderTemplate>
                    Performance</HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Left" />                    
                    <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
                        <asp:TemplateField>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ModifyDate", "{0:yyyy-MM-dd}")%>
                </ItemTemplate>
                <HeaderTemplate>
                    Last Action Date</HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Left" />                    
                    <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <a id="lnkDetail" runat="server" href='<%# GetViewLink(DataBinder.Eval(Container.DataItem, "ApplicantId").ToString(),DataBinder.Eval(Container.DataItem, "id").ToString()) %>'>View</a>&nbsp;&nbsp;
                    <a id="lnkEdit" runat="server" href='<%# GetEditLink(DataBinder.Eval(Container.DataItem, "id").ToString()) %>'>Edit</a>
                </ItemTemplate>
                <HeaderTemplate>
                    Actions</HeaderTemplate>
                    <HeaderStyle Width="90" HorizontalAlign="Center" />                    
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
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
