<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HireReport.aspx.cs" Inherits="HireReport" MasterPageFile="~/SpringfieldMaster.master" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="cntHireReport" runat="server">
<div>
    <div id="content_title" class="panel_title_expand">
    Hire Report
    </div>
    <div id="content_content" class="panel_content">
    <asp:DataList ID="dlHireTable" runat="server">
        <ItemTemplate>
            <table class="applicants_table" style="width: 100%;">
                <tr class="bold_font">
                    <td rowspan="2">
                       Mentor (Alias)
                    </td>
                    <td colspan="4">
                        <%# Eval("Month") %>
                    </td>
                </tr>
                <tr class="bold_font">
                    <td>
                        Interview Intern #				
                    </td>
                    <td>
                        Hire Intern #
                    </td>
                    <td>
                        Reject Intern #
                    </td>
                    <td>
                        Decline Offer Intern #
                    </td>
                </tr>
                <%# Eval("HireReport") %>
            </table>
        </ItemTemplate>
    </asp:DataList>
    </div>
</div>
</asp:Content>