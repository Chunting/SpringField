<%@ Page Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.AddNote" MasterPageFile="~/SpringfieldMaster.master" Codebehind="AddNote.aspx.cs" %>
<%@ Register TagPrefix="Springfield" TagName="CommentList" Src="~/Controls/Comment.ascx" %>


<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="cntCommentList" runat="server">
    <Springfield:CommentList id="commentList" runat="server"></Springfield:CommentList>
</asp:Content>