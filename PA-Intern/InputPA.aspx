<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InputPA.aspx.cs" Inherits="InputPA"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
    <div>
        <br />
        <b style="font-size: 24px">Second Step:</b>&nbsp;Input performance assessment<br />
        <br />
        <table style="width: 68%">
            <tr>
                <td>
                    <div style="background-color: #879096; color: White; height: 20px; vertical-align: middle">
                        <b style="font-size: 16px">OBJECTIVE and SELF EVALUATION</b></div>
                    Summarize your performance against each objective considering WHAT you have achieved
                    and HOW you have achieved it.
                </td>
            </tr>
            <tr>
                <td>
                    <strong>GOAL/OBJECTIVE:</strong><br />
                    <asp:TextBox ID="tbObjective" runat="server" TextMode="MultiLine" Height="235px"
                        Width="980px"></asp:TextBox><br />
                    <br />
                    <strong>SELF EVALUATION:</strong><br />
                    <asp:TextBox ID="tbSelfEvaluation" runat="server" TextMode="MultiLine" Height="235px"
                        Width="980px"></asp:TextBox><br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <div style="background-color: #879096; color: White; height: 20px; vertical-align: middle">
                        <b style="font-size: 16px">STRENGTHS and WEAKNESSES</b></div>
                    Please comment on your work assignment, your experience working with your mentor,
                    our organization and the company Microsoft, or this review process. Please comment
                    on your performance STRENGTHS and WEAKNESSES demonstrated in your daily work here.</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="tbStengthsAndWeaknesses" runat="server" TextMode="MultiLine" Height="235px"
                        Width="980px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                <br />
                    <div id="divPublication" runat="server">
                        <div style="background-color: #879096; color: White; height: 20px; vertical-align: middle">
                            <b style="font-size: 16px">Info of the papers finished at MSRA</b></div>
                        <div>
                        <br />
                            <asp:GridView ID="gvPublication" runat="server" AutoGenerateColumns="False" AutoGenerateDeleteButton="True"
                                AutoGenerateEditButton="True" OnRowEditing="gvPublication_RowEditing" DataKeyNames="PublicationId"
                                OnRowUpdating="gvPublication_RowUpdating" OnRowCancelingEdit="gvPublication_RowCancelingEdit"
                                OnRowDeleting="gvPublication_RowDeleting" CellPadding="4" GridLines="None" ForeColor="#333333">
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Publication">
                                        <ItemStyle Width="500px" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%#StatusIdToString(Eval("CurrentStatus").ToString())%>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Current status of the paper</HeaderTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlgvPaperStatus" DataSource='<%# Springfield.Components.PAResourceManager.GetTypeDisplayItems("PaperStatus") %>'>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#879096" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                            <p>
                                <strong>Add New Publication:</strong><br />
                                <strong>Example</strong>:<br />
                                <em>In conference: </em>
                                <br />
                                Yong Wang, Jiang Li, Kun Zhou and Heung-Yeung Shum, ��Interacting with 3D Graphic
                                Objects in an Image-based Environment,�� The Second IEEE Pacific-Rim Conference on
                                Multimedia, pp.229-236, Beijing, China, October 24-26, 2001.
                                <br />
                                <em>In Journal:</em><br />
                                Yueting Zhuang, Jiashi Chen, Fei Wu, Qiang Zhu��A Recursive Framework for Automatic
                                Face Tracking����Chinese Journal of Electronics����Vol.13, No.1, Jan.,2004 [SCI/EI��¼]
                            </p>
                            <table>
                                <tr>
                                    <td>
                                        Publication:
                                        <br />
                                        <asp:TextBox ID="tbNewPublication" runat="server" TextMode="MultiLine" Width="980px"
                                            Height="96px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>
                                        Current status of the paper:
                                        <asp:DropDownList ID="ddlPaperStatus" runat="server">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="tbnSummitNewPublication" BackColor="#35769E"  ForeColor="white" runat="server" Text="Add Publication" OnClick="tbnSummitNewPublication_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 80%; text-align: right;">
        <asp:Button ID="btnSummit" Width="100px" BackColor="#35769E"  ForeColor="white" runat="server" Text="Submit" OnClick="btnSummit_Click" /></div>
    <br />
</asp:Content>