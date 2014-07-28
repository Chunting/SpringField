<%@ page language="C#" autoeventwireup="true" inherits="AddNote, App_Web_wkngwpi-" masterpagefile="~/SpringfieldMaster.master" %>
<%@ Register TagPrefix="Springfield" TagName="CommentList" Src="~/Controls/Comment.ascx" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" ID="cntCommentList" runat="server">
<Springfield:CommentList id="commentList" runat="server"></Springfield:CommentList>
</asp:Content>