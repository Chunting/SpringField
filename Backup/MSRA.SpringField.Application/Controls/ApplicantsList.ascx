<%@ Control Language="C#" AutoEventWireup="true" Inherits="MSRA.SpringField.Application.Controls.Controls_ApplicantsList"
    CodeBehind="ApplicantsList.ascx.cs" %>
<%@ Register Assembly="MSRA.SpringField.Controls" Namespace="MSRA.SpringField.Controls"
    TagPrefix="MSRA" %>

<script type="text/javascript">
    function switchSummary(appid) {
        var summaryObj = document.getElementById(appid);
        if (summaryObj != null) {
            if (summaryObj.style.display == 'none') {
                summaryObj.style.display = 'block';
            }
            else {
                summaryObj.style.display = 'none';
            }
        }
    }
</script>

<div style="width: 100%">
    <div class="toolbar">
        <table style="height: 100%" cellpadding="0" cellspacing="0">
            <tr>
                <td runat="server" id="btnMultiInterview" style="padding: 0 10; height: 30;" onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/schedule.png"
                        ID="btnInterviewCurrent" runat="server" AlternateText="Schedule Interview" OnClick="btnMultiInterview_Click"
                        ImageAlign="AbsMiddle" />
                    <label for="<%=btnInterviewCurrent.ClientID %>">
                        <span style="cursor: hand">Schedule Interview</span></label>
                </td>
                <td>&nbsp;</td>
                <td style="padding: 0 10; height: 30;" runat="server" id="btnFavorite" onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/addtofavorite.png"
                        ID="btnAddFavoriteCurrent" runat="server" AlternateText="Add to Favorite" OnClick="btnFavorite_Click"
                        ImageAlign="AbsMiddle" />
                    <label for="<%=btnAddFavoriteCurrent.ClientID %>">
                        <span style="cursor: hand">Add To Favorite</span></label>
                </td>
                <td>&nbsp;</td>
                <td style="padding: 0 10; height: 30;" runat="server" id="btnMultiRefer" onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" CssClass="img_icon" ImageUrl="~/Resource/Images/forward.png"
                        ID="btnForward" runat="server" AlternateText="Forward to mentor" OnClick="btnMultiRefer_Click"
                        ImageAlign="AbsMiddle" />
                    <label for="<%=btnForward.ClientID %>">
                        <span style="cursor: hand">Forward To Mentor</span></label>
                </td>
                <td>&nbsp;</td>
                <td style="padding: 0 10; height: 30;" runat="server" id="btnDeleteSelection" onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" ID="btnDelete" ImageUrl="~/Resource/Images/delete.png"
                        CssClass="img_icon" runat="server" AlternateText="Delete Applicant(s)" OnClientClick="return ConfirmDelete();"
                        Visible="true" OnClick="btnMultiDelete_Click" ImageAlign="AbsMiddle" />
                    <label for="<%=btnDelete.ClientID %>">
                        <span style="cursor: hand">Delete Applicant(s)</span></label>
                </td>
                <td>&nbsp;</td>
                <td style="padding: 0 10; height: 30;" runat="server" id="Td1" onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" ID="btnRemove" ImageUrl="~/Resource/Images/remove.png"
                        CssClass="img_icon" runat="server" AlternateText="Remove Applicant(s) From Favorite List"
                        OnClientClick="return confirm('Are your sure you want to remove the selected applicant(s) from your favorite list?');"
                        Visible="true" OnClick="btnMultiRemove_Click" ImageAlign="AbsMiddle" />
                    <label for="<%=btnRemove.ClientID %>">
                        <span style="cursor: hand">Remove Applicant(s)</span></label>
                </td>
                <td>&nbsp;</td>
                <td style="padding: 0 10; height: 30;" runat="server" id="btnSendEmailSelection"
                    onmouseover="this.style.borderStyle='solid';this.style.borderWidth='1';this.style.borderColor='#B6C2D8';"
                    onmouseout="this.style.borderWidth='0';">
                    <asp:ImageButton Width="28" Height="28" ID="btnSendEmail" ImageUrl="~/Resource/Images/NoticeEmail.jpg"
                        CssClass="img_icon" runat="server" AlternateText="Send Notice Email to the applicant(s)"
                        OnClientClick="return confirm('Are your sure you want to send the notice email(s) ?');"
                        Visible="true" OnClick="btnMultiSend_Click" ImageAlign="AbsMiddle" />
                    <label for="<%=btnSendEmail.ClientID %>">
                        <span style="cursor: hand">Send Notice Email(s)</span></label>
                </td>
            </tr>
        </table>
    </div>
    <div id="content_title" class="panel_title_expand" style="border-bottom-style: none">
        Applicants -
        <asp:Label ID="lbCount" runat="server" Text="0"></asp:Label>&nbsp;Records Found
    </div>
    <div id="content_content" class="panel_content">
        <asp:GridView ID="gvApplicants" runat="server" HeaderStyle-BorderColor="#999" HeaderStyle-BorderStyle="Solid"
            GridLines="Both" HeaderStyle-Height="40" AutoGenerateColumns="False" CssClass="applicants_table"
            OnRowCommand="gvApplicants_RowCommand" PageSize="12" OnRowDataBound="gvApplicants_RowDataBound"
            AllowSorting="True">
            <Columns>
                <%--多选框--%>
                <asp:TemplateField AccessibleHeaderText="Check" HeaderText="Check">
                    <ItemTemplate>
                        <input type="checkbox" id="cb_ischeck" name="cb_ischeck" value="<%# ParseCheckBox(Container.DataItem) %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--Name--%>
                <asp:TemplateField AccessibleHeaderText="Name" HeaderText="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlApplication" runat="server" NavigateUrl='<%# Eval("ApplicantId", "~/ShowApplication.aspx?applicant={0}&contrid="+Request.QueryString["contrid"]) %>'
                            Target="_self" Text='<%# ((string)Eval("FirstName")) + " " + ((string)Eval("LastName")).ToUpper()%>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--Degree--%>
                <asp:TemplateField AccessibleHeaderText="Degree" HeaderText="Degree">
                    <ItemTemplate>
                        <%# ParseDegree(Container.DataItem) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="University" HeaderText="University">
                    <ItemTemplate>
                        <%# ParseInstitution(Container.DataItem)%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField AccessibleHeaderText="Major" DataField="Major" HeaderText="Major">
                </asp:BoundField>
                <asp:TemplateField AccessibleHeaderText="Status" HeaderText="Status">
                    <ItemTemplate>
                        <%# ParseStatus(Container.DataItem)%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Mentor" HeaderText="Mentor">
                    <ItemTemplate>
                        <%# ParseMentor(Container.DataItem)%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="CompleteDate" HeaderText="Apply Date">
                    <ItemTemplate>
                        <%# ParseDate(Container.DataItem) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="CurrentStatus" HeaderText="Last Action">
                    <ItemTemplate>
                        <%# ParseInterviewStatus(Container.DataItem) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last PA">
                    <ItemTemplate>
                        <%# ParseLastPA(DataBinder.Eval(Container.DataItem, "ApplicantId").ToString())%>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--Action--%>
                <asp:TemplateField AccessibleHeaderText="Action" ItemStyle-HorizontalAlign="Left"
                    HeaderText="Action" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <%--Addnote--%>
                        <div style="cursor: pointer; width: 100%; padding: 2 2; margin: 1 1">
                            <asp:Button ID="btnDelete" runat="server" CommandName="DeleteApplicant" OnClientClick="return ConfirmDelete();"
                                Visible="false" Text="Delete"></asp:Button>
                            <a id="btnAddNote" target="_self" href="AddNote.aspx?applicant=<%# Eval("ApplicantId") %>">
                                <img align="absmiddle" src="Resource/Images/addnote.png" style="width: 16px; height: 16px;
                                    border: 0px; vertical-align: absmiddle;" alt="add/read note" /></a><label for="btnAddNote">Add
                                        Note</label>
                        </div>
                        <%--面试--%>
                        <div runat="server" id="divSchedual" style="cursor: pointer; width: 100%; padding: 2 2;
                            margin: 1 1">
                            <a id="anchorSchedule" href="javascript:void(0);">
                                <img align="absmiddle" onclick="OpenNewWindow('ArrangeInterview.aspx','<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>')"
                                    id="btnSchedule" src="Resource/Images/schedule_small.png" border="0" alt="Schedule Interview:<%# (string)Eval("FirstName") + " " + (string)Eval("LastName") %>"
                                    style="width: 16px; height: 16px; vertical-align: absmiddle;" /></a>
                            <label for="anchorSchedule" onclick="OpenNewWindow('ArrangeInterview.aspx','<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>')">
                                Schedule</label>
                        </div>
                        <%--favourite--%>
                        <div style="cursor: pointer; width: 100%; padding: 2 2; margin: 1 1" runat="server"
                            id="txtFav">
                            <asp:ImageButton ImageUrl="~/Resource/Images/addtofavorite_small.png" AlternateText="Add to Favorite"
                                ID="btnAddFavorite" runat="server" CommandName="AddFavorite" Style="border: 0px;
                                height: 16px; width: 16px;" ImageAlign="AbsMiddle"></asp:ImageButton><label runat="server"
                                    id="lbl">Add Favorite</label>
                        </div>
                        <%--remove--%>
                        <div style="cursor: pointer; width: 100%; padding: 2 2; margin: 1 1" runat="server"
                            id="divRemove">
                            <asp:ImageButton ImageUrl="~/Resource/Images/remove_small.png" AlternateText="Remove from Favorite"
                                ID="ibRemove" runat="server" CommandName="Remove" OnClientClick="return confirm('Are your sure you want to remove the selected applicant from your favorite list?');"
                                Style="border: 0px; height: 16px; width: 16px; margin: 0 3" ImageAlign="AbsMiddle">
                            </asp:ImageButton><label for="ibRemove%>">Remove</label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--感兴趣组，几乎没用--%>
                <asp:TemplateField AccessibleHeaderText="IG" HeaderText="IG">
                    <ItemTemplate>
                        <input runat="server" id="hidValue" style="display: none" type="text" value='<%#Eval("InterestedGroup")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <div style="width: 100%; vertical-align: middle; text-align: center">
                    <img id="empty" width="64" height="64" src="Resource/Images/empty.png" style="vertical-align: absmiddle" /><br />
                    <span style="font-size: 11">There is no application fit for your filter now</span>
                </div>
            </EmptyDataTemplate>
            <PagerSettings Mode="NumericFirstLast" />
        </asp:GridView>
        <div style="width: 100%; height: 100%; border-left: solid 1px #999; border-right: solid 1px #999">
            <MSRA:PagerControl ID="PagerControl1" runat="server" PageSize="20" OnPagerClick="PagerControl1_PagerClick" />
        </div>
    </div>
</div>
