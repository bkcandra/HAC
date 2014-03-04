<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivitiesTableView.ascx.cs"
    Inherits="HealthyClub.Providers.Web.UserControls.ActivitiesTableView" %>
<style type="text/css">
    .modalBackground {
        background-color: Gray;
        filter: alpha(opacity=50);
        opacity: 0.7;
    }

    #tableContent table tr:hover td div.edit {
        display: inline-block;
    }

    #tableContent table tr td div.edit {
        display: none;
    }

    #tableContent table tr:nth-child(even) td {
        background-color: #F3F3F3;
    }

    #tableContent table a:link {
        font-size: 14px;
    }

    #tableContent table a:visited {
        font-size: 14px;
    }

    #tableContent table a:hover {
        text-decoration: underline;
        font-size: 14px;
    }

    #tableContent table a:active {
        font-size: 14px;
    }

    #tableContent table a:focus {
        font-size: 14px;
    }

    #tableContent table caption {
        padding: 18px 2px 15px 2px;
        color: #cfcfcf;
        background-color: inherit;
        font-weight: normal;
        text-align: center;
        text-transform: capitalize;
    }

    #tableContent table tr td {
        padding: 1px 3px;
        margin: 6px 9px;
        text-align: left;
        vertical-align: top;
    }

    #tableContent table thead th {
        background-color: #86B788;
        font-weight: bold;
        text-align: left;
        vertical-align: top;
        padding: 6px 9px;
        color: White;
    }

    #tableContent table tbody tr th {
        background-color: #86B788;
        font-weight: bold;
        text-align: left;
        padding: 6px 9px;
        color: White;
    }

    #tableContent table tbody tr.odd {
        background-color: #F3F3F3;
    }

    #tableContent table tbody tr:hover {
        color: #333333;
        background-color: #E5E5D8;
    }

    #tableContent table tbody tr.odd:hover {
        color: #333333;
        background-color: #E5E5D8;
    }

    #tableContent table tfoot td, #tableContent table tfoot th {
        border: 1px solid #ccc;
        font-weight: bold;
        color: #592C16;
        padding: 6px 9px;
    }

    .style3 {
        width: 4px;
    }

    .style4 {
        width: 66px;
    }
</style>
<script type="text/javascript">
    function Ok() {

        var btnExecPopUp = document.getElementById('<%= btnExecPopUp.ClientID %>');

        if (btnExecPopUp != null) {
            btnExecPopUp.click();
        }
    }
    function ShowPopUp() {
        //alert('Move all item in this folder to ----------------');
        var btnShowPopUp = document.getElementById('<%= btnShowPopUp.ClientID %>');

        if (btnShowPopUp != null) {
            btnShowPopUp.click();
        }
    }

    function Alert() {

    }
</script>
<table width="100%">
    <tr>
        <td align="left" style="width: 50%;" colspan="2">Showing:
            <asp:Label ID="lblStartIndex" runat="server"></asp:Label>&nbsp;-
            <asp:Label ID="lblEndIndex"
                runat="server"></asp:Label>
            &nbsp;of
            <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
            &nbsp;activities
            <asp:Label ID="lblKeyword" runat="server" CssClass="SearchResults" Visible="false"></asp:Label>
        </td>
    </tr>
    <tr>
        <td valign="middle" align="left" style="width: 50%">Sort by
            <asp:DropDownList ID="ddSort" runat="server" Height="18px" Width="200px" Font-Size="8"
                OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Value="1">Newly listed</asp:ListItem>
                <%--<asp:ListItem Value="2">Expiring</asp:ListItem>--%>
                <asp:ListItem Value="3">Expiry date</asp:ListItem>
                <asp:ListItem Value="4">Title: A to Z</asp:ListItem>
                <asp:ListItem Value="5">Title: Z to A</asp:ListItem>
                <asp:ListItem Value="6">Cost: Free - Paid</asp:ListItem>
                <asp:ListItem Value="7">Cost: Paid - Free</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td valign="top" align="right" style="width: 50%">Activities per page
            <asp:DropDownList ID="ddlPagingTop" runat="server" OnSelectedIndexChanged="ddlPagingTop_SelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
            </asp:DropDownList>
            &nbsp;
        </td>
    </tr>
</table>
<br />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="tableContent">
            <asp:GridView ID="GridViewActivities" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                DataSourceID="ods" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                Width="100%" OnRowDataBound="GridView1_RowDataBound" BorderColor="#cccccc">
                <Columns>
                    <asp:TemplateField HeaderText="Activity Name">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <%--<asp:LinkButton ID="lnkProductName" runat="server" CommandName="SetProduct" Text='<%#String.Format("{0} - {1}",Eval("BrandName"), Eval("Name")) %>'></asp:LinkButton>--%>
                                        <asp:HyperLink ID="HlnkActivitiesName" CssClass="listingTitle" NavigateUrl="#" runat="server"><%#Eval("Name") %></asp:HyperLink>
                                        <asp:HiddenField ID="hdnActivityID" runat="server" Value='<%#Eval("ID") %>' />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Spec 1">
                <ItemTemplate>
                    <asp:Label ID="lblSpec1" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Spec 2">
                <ItemTemplate>
                    <asp:Label ID="lblSpec2" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Details">
                        <ItemTemplate>
                            <table id="actDetails" style="width: 430px; padding: 0">
                                <tr>
                                    <td style="text-align: left; vertical-align: top; width: 120px">
                                        <asp:Label ID="lblType" runat="server" Text="" CssClass="bodyText1"></asp:Label>
                                    </td>
                                    <td style="text-align: left; width: 180px; vertical-align: top">
                                        <asp:Label ID="lblStatus" runat="server" Text="" CssClass="bodyText1"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblExpiryDate" runat="server" Text="" CssClass="bodyText1"></asp:Label>
                                    </td>
                                    <td style="width: 120px; vertical-align: top; text-align: right">
                                        <div class="edit">
                                            <asp:LinkButton ID="btnImgStatus" runat="server" CommandName="changeStatus">Status</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="lnkEditAct" runat="server">Edit</asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="hdnStatus" runat="server" Value='<%#Eval("Status") %>' />
                            <asp:HiddenField ID="hdnExpiryDate" runat="server" Value='<%#Eval("ExpiryDate") %>' />
                            <asp:HiddenField ID="hdnType" runat="server" Value='<%#Eval("ActivityType") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle HorizontalAlign="Left" />
            </asp:GridView>
        </div>
        <br />
        <div id="divproductsListViewCommand">
            <asp:HiddenField ID="hdnSearchKey" runat="server" />
            <asp:HiddenField ID="hdnPage" runat="server" />
            <asp:HiddenField ID="hdnSortValue" runat="server" />
            <asp:HiddenField ID="hdnStartRow" runat="server" />
            <asp:HiddenField ID="hdnProviderID" runat="server" />
            <asp:HiddenField ID="hdnCategoryID" runat="server" />
            <asp:HiddenField ID="hdnPageSize" runat="server" />
            <asp:ObjectDataSource ID="ods" runat="server"></asp:ObjectDataSource>
        </div>
        <div style="width: 100%;">
            <table width="100%">
                <tr>
                    <td align="left" style="width: 50%;">Showing:
                        <asp:Label ID="lblStartIndex1" runat="server"></asp:Label>-<asp:Label ID="lblEndIndex1"
                            runat="server"></asp:Label>
                        &nbsp;of
                        <asp:Label ID="lblAmount1" runat="server" Text=""></asp:Label>
                        &nbsp;activities
                    </td>
                    <td id="tdPager2" runat="server" align="right" style="width: 50%;">&nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div id="divStatusPopUp" runat="server" style="border: medium ridge #000000; background-color: #FFFFFF; width: 450px;">
            <div id="divMoveDestinationTitle" style="border-bottom: thin solid #000000; font-size: 14px; font-weight: bold; height: 17px; width: 400px; border-left-color: #000000; border-right-color: #000000; border-top-color: #000000; width: 100%">
                <asp:Label ID="lblMoveDestinationTitle" runat="server" Text=" Activity Status"></asp:Label>
            </div>
            <table width="400px">
                <tr>
                    <td class="style4">Status
                    </td>
                    <td class="style3">:
                    </td>
                    <td>
                        <asp:RadioButton ID="radButtonAct" runat="server" Text="Active" GroupName="ActivityStatus" />
                        <asp:RadioButton ID="radButtonInact" runat="server" Text="Inactive" GroupName="ActivityStatus" />
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        <asp:HiddenField ID="hdnStatus" runat="server" />
                        <asp:HiddenField ID="hdnCurrentActID" runat="server" />
                    </td>
                    <td class="style3"></td>
                    <td align="left">
                        <asp:LinkButton ID="lnkOk" runat="server">Ok</asp:LinkButton>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="lnkCancel" runat="server">Cancel</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopUp"
            PopupControlID="divStatusPopUp" CancelControlID="lnkCancel" OkControlID="lnkOk"
            OnOkScript="Ok()" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Button ID="btnShowPopUp" runat="server" Text="asd" Style="display: none"></asp:Button>
        <asp:Button ID="btnExecPopUp" runat="server" Text="" OnClick="btnExecPopUp_Click"
            Style="display: none" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowAlertMessage"
            PopupControlID="divMessage" CancelControlID="btnAlertCancel" OkControlID="btnAlertOK"
            OnOkScript="Alert()" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Button ID="btnShowAlertMessage" runat="server" Text="asd" Style="display: none"></asp:Button>
        <div id="divMessage" runat="server" style="border: medium ridge #000000; background-color: #FFFFFF; width: 450px; text-align: center">
            <div id="div1" style="border-bottom: thin solid #000000; font-size: 14px; font-weight: bold; height: 17px; width: 400px; border-left-color: #000000; border-right-color: #000000; border-top-color: #000000; text-align: left; width: 100%">
                <asp:Label ID="Label1" runat="server" Text=" Expired Activity"></asp:Label>
            </div>
            <asp:Label ID="lblAlert" runat="server" Text="" ForeColor="Red"></asp:Label>
            <br />
            <asp:Button ID="btnAlertOK" runat="server" Text="Ok" />
        </div>
        <asp:Button ID="btnAlertCancel" runat="server" Text="Ok" Style="display: none" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
