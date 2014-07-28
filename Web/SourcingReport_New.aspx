<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SourcingReport_New.aspx.cs"
    Inherits="SourcingReport_New" MasterPageFile="~/SpringfieldMaster.master" %>

<%@ Register Src="Controls/SRChannelSource.ascx" TagName="SRChannelSource" TagPrefix="ucSRChannelSource" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="mainContent" runat="server">
    Date from:
    <asp:TextBox ID="tbStartDate" runat="server"></asp:TextBox>
    &nbsp; &nbsp; &nbsp;To :
    <asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>
    <asp:CompareValidator ID="cmpDate" runat="server" ControlToValidate="tbEndDate" ControlToCompare="tbStartDate"
        Operator="GreaterThan" SetFocusOnError="true" ErrorMessage="End date must be greater than start date!"
        Type="Date" Display="Dynamic" /><br />
    Please select a report type:
    <asp:DropDownList ID="ddlReportType" runat="server">
        <asp:ListItem Value="ChannelSourcing" Text="Sourcing Report - by Channel and Sourcing"></asp:ListItem>
        <asp:ListItem Value="PreferredPosition" Text="Sourcing Report - by Preferred Position"></asp:ListItem>
        <asp:ListItem Text="Sourcing Report - by Special Program" Value="SpecialProgram"></asp:ListItem>
        <asp:ListItem Text="Sourcing Report - by Degree" Value="Degree"></asp:ListItem>
        <asp:ListItem Text="Sourcing Report - by Research Group" Value="ResearchGroup"></asp:ListItem>
    </asp:DropDownList><br />
    <br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp;
    <asp:Button ID="btnSubmit" Text="Generate" runat="server" OnClick="btnSubmit_Click" /><br />
    <asp:Label ID="lbDateSpan" runat="server" Font-Bold="True"></asp:Label>
    <asp:PlaceHolder ID="phReport" runat="server"></asp:PlaceHolder>
</asp:Content>
