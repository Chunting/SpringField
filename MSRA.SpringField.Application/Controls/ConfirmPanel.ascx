<%@ Control Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Controls.Controls_ConfirmPanel" Codebehind="ConfirmPanel.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
 <asp:LinkButton ID="LinkButton1" runat="server" Text="Click here to change the paragraph style" />
<asp:Panel ID="Panel1" runat="server" Style="display: none">
            <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup">
                <div>
                    <p>goodluck</p>
                    <p style="text-align: center;">
                        <asp:Button ID="OkButton" runat="server" Text="OK" OnClick="OkButton_Click" Width="120" />
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" Width="120" />
                    </p>
                </div>
            </asp:Panel>
        </asp:Panel>
        
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server"
            TargetControlID="LinkButton1"
            PopupControlID="Panel1"
            BackgroundCssClass="modalBackground"
            CancelControlID="CancelButton"
            DropShadow="true"
            Drag="true"
            PopupDragHandleControlID="Panel3"/>
