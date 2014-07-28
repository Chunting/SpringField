<%@ page language="C#" autoeventwireup="true" inherits="FeedbackList, App_Web_wkngwpi-" masterpagefile="~/SpringfieldMaster.master" %>

<asp:Content ContentPlaceHolderID="mainPlaceHolder" runat="server" ID="cntFeedbackList">
<div style="width: 80%;">

<ul>
<li>
This list shows the applicants which are still waiting for your interview feedback. 
</li>
<li>
You can click applicant¡¯s name to submit interview feedback.
</li>
</ul>

<div id="ch_title" class="panel_title_expand">
Incomplete Feedback List
</div>
<div id="ch_content" class="panel_content">

    <asp:GridView ID="gvFeedbackList" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="applicants_table" OnPageIndexChanging="gvFeedbackList_PageIndexChanging" PageSize="15">
        <PagerSettings Mode="NumericFirstLast" />
        <Columns>
            <asp:TemplateField AccessibleHeaderText="Applicant" HeaderText="Applicant">
                <ItemTemplate>
                    <a href='InterviewFeedback.aspx?feedback=<%# DataBinder.Eval(Container.DataItem, "FeedbackId") %>' target="_blank"><%# ParseName(Container.DataItem) %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField AccessibleHeaderText="DueDate" HeaderText="DueDate">
                <ItemTemplate>
                    <%# ParseDate(Container.DataItem) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField AccessibleHeaderText="Action" HeaderText="Action">
                <ItemTemplate>
                    <a target="_blank" href="AddNote.aspx?applicant=<%# Eval("ApplicantId") %>"><img class="img_icon" src="ProUI\images\addnote.png" style="width: 16px; height: 16px;border: 0px;" alt="add/read note" /></a>&nbsp;&nbsp;&nbsp;<input class="img_icon" type="image" src="ProUI/images/changeinterview.png" alt="Change Interviewer" onclick="OpenWindow('ChangeInterviewer.aspx?feedback=','<%# DataBinder.Eval(Container.DataItem, "FeedbackId") %>');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            You don't have any incomplete feedback
        </EmptyDataTemplate>
    </asp:GridView>

</div>
</div>

</asp:Content>