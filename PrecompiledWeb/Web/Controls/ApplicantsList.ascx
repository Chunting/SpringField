<%@ control language="C#" autoeventwireup="true" inherits="Controls_ApplicantsList, App_Web_eecyfkj6" %>
<%@ Register Assembly="MSRA.ServerControls" Namespace="MSRA.ServerControls" TagPrefix="MSRA" %>
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
<div style="width:100%">
    <div id="content_title" class="panel_title_expand">
        Applicants -
        <asp:Label ID="lbCount" runat="server" Text="0"></asp:Label>&nbsp;Records Found
    </div>
    <div id="content_content" class="panel_content">
        <div>
            <asp:Button ID="btnScheduleInterview" runat="server" Text="Schedule Interviews" OnClick="btnMultiInterview_Click" />
            <asp:Button ID="btnDeleteSelection" runat="server" OnClick="btnMultiDelete_Click"
                OnClientClick="return ConfirmDelete();" Text="Delete Records" />
            <asp:Button ID="btnRecommend" runat="server" Text="Forward to Mentor" OnClientClick="return ConfirmMultiRefer();"
                OnClick="btnMultiRefer_Click" />
        </div>
        <asp:GridView ID="gvApplicants" runat="server" AutoGenerateColumns="False" CssClass="little_applicants_table"
            OnRowCommand="gvApplicants_RowCommand" PageSize="12" OnRowDataBound="gvApplicants_RowDataBound"
            AllowSorting="True">
            <Columns>                
                
                <asp:TemplateField AccessibleHeaderText="Check" HeaderText="Check">
                    <ItemTemplate>
                        <input type="checkbox" id="cb_ischeck" name="cb_ischeck" value="<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Name" HeaderText="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlApplication" runat="server" NavigateUrl='<%# Eval("ApplicantId", "~/ShowApplication.aspx?applicant={0}") %>'
                            Target="_blank" Text='<%# ((string)Eval("FirstName")) + " " + ((string)Eval("LastName")).ToUpper()%>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
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
                <asp:TemplateField AccessibleHeaderText="Operation" HeaderText="Action" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Button ID="btnDelete" runat="server" CommandName="DeleteApplicant" OnClientClick="return ConfirmDelete();"
                            Visible="false" Text="Delete"></asp:Button><a target="_blank" href="AddNote.aspx?applicant=<%# Eval("ApplicantId") %>"><img
                                src="ProUI\images\addnote.png" style="width: 16px; height: 16px; border: 0px;"
                                alt="add/read note" /></a> <a href="#">
                                    <img src="ProUI\images\interview.jpg" border="0" alt="Schedule Interview:<%# (string)Eval("FirstName") + " " + (string)Eval("LastName") %>"
                                        style="width: 16px; height: 16px;" onclick="OpenNewWindow('ArrangeInterview.aspx','<%# DataBinder.Eval(Container.DataItem, "ApplicantId") %>');" /></a>
                                        <asp:ImageButton
                                            ImageUrl="~\ProUI\images\addfavorite.gif" AlternateText="Add to Favorite" ID="btnAddFavorite"
                                            runat="server" CommandName="AddFavorite" Style="border: 0px; height: 16px; width: 16px;">
                                        </asp:ImageButton>
                                        <a runat="server" id="switchbtn">
                                            <img src="ProUI\images\summary.png" width="16" height="16" style="vertical-align:absmiddle" alt="switch summary" />
                                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="IG" HeaderText="IG">
                    <ItemTemplate>
                        <input runat="server" id="hidValue" style="display:none" type="text" value='<%#Eval("InterestedGroup")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
            <EmptyDataTemplate>
                There is no application fit for your filter now
            </EmptyDataTemplate>
            <PagerSettings Mode="NumericFirstLast" />
        </asp:GridView>
        <MSRA:PagerControl ID="PagerControl1" runat="server" PageSize="20" OnPagerClick="PagerControl1_PagerClick" />
        <div>
            <asp:Button ID="btnMultiInterview" runat="server" Text="Schedule Interviews" OnClick="btnMultiInterview_Click" />
            <asp:Button ID="btnMultiDelete" runat="server" OnClick="btnMultiDelete_Click" OnClientClick="return ConfirmDelete();"
                Text="Delete Records" />
            <asp:Button ID="btnMultiRefer" runat="server" Text="Forward to Mentor" OnClientClick="return ConfirmMultiRefer();"
                OnClick="btnMultiRefer_Click" />
            <asp:Button ID="btnDeleteFavorite" runat="server" Text="Remove Selections" OnClientClick="return ConfirmMultiRefer();"
                OnClick="btnDeleteFavorite_Click" />
        </div>
    </div>
</div>
