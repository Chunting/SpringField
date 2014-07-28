<%@ page language="C#" masterpagefile="~/SpringfieldMaster.master" autoeventwireup="true" inherits="OperationResult, App_Web_wkngwpi-" title="Springfield-Message" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainPlaceHolder" Runat="Server">
<asp:Panel ID="pMessage" runat="server">
                    <div id="ch_title" class="panel_title_expand">
                        Message
                    </div>
                    <div id="ch_content" class="panel_content">
                        <asp:Literal ID="litMessage" runat="server"></asp:Literal>
                    </div>
                </asp:Panel>
</asp:Content>

