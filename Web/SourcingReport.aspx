<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SourcingReport.aspx.cs" Inherits="SourcingReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Sourcing Report</title>
</head>
<body>
    <form id="frmMain" runat="server">
    <div style="font-size: 15px;">
        <h2>Intern Recruiting - Monthly Sourcing Report</h2>
        <asp:DataList ID="dlSourceTable" runat="server">
            <ItemTemplate>
                <h4>Date From <%# Eval("StartDate") %> To <%# Eval("EndDate") %></h4>
                <table cellspacing="1" cellpadding="0" style="background: black;width:1245px;">
                    <tr style="background: white;font-weight: bold;">
                        <td>
                        Month
                        </td>
                        <td>
                        Status
                        </td>
                        <%# Eval("SourceTitle") %>
                    </tr>
                    <tr style="background: white;">
                        <td rowspan="5">
                            <%# Eval("Month") %>
                        </td>
                        <td>
                            Received #
                        </td>
                        <%# Eval("ReceiveCount") %>
                    </tr>
                    <tr style="background: white;">
                        <td>
                            Interview #
                        </td>
                        <%# Eval("InterviewCount") %>
                    </tr>
                    <tr style="background: white;">
                        <td>
                           Hire # 
                        </td>
                        <%# Eval("HireCount") %>
                    </tr>
                    <tr style="background: white;">
                        <td>
                            Decline Offer #
                        </td>
                        <%# Eval("DeclineOfferCount") %>
                    </tr>
                    <tr style="background: white;">
                        <td>
                            Rejected #
                        </td>
                        <%# Eval("RejectCount") %>
                    </tr>                    
                    </table>
                    <br />
            </ItemTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>
