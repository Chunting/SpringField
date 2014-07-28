<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNote.aspx.cs" Inherits="AddNote" MasterPageFile="~/SpringfieldMaster.master" %>
<%@ Register TagPrefix="Springfield" TagName="CommentList" Src="~/Controls/Comment.ascx" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="cntCommentList" runat="server">
<Springfield:CommentList id="commentList" runat="server"></Springfield:CommentList>
</asp:Content>