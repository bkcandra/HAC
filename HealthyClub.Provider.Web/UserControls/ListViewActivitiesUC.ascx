<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListViewActivitiesUC.ascx.cs" Inherits="HealthyClub.Providers.Web.UserControls.ListViewActivitiesUC" %>
<table>
    <tr>
        <td align="left" style="width: 50%;">
            Showing :
            <asp:Label ID="lblStartIndex" runat="server"></asp:Label>-<asp:Label ID="lblEndIndex"
                runat="server"></asp:Label>
            &nbsp;of
            <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
            &nbsp;activities
            <asp:Label ID="lblKeyword" runat="server" CssClass="SearchResults" Visible="false"></asp:Label>
        </td>
        <td id="tdPager1" runat="server" align="right" style="width: 50%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td valign="middle" align="left" style="width: 50%">
            Sort by :
            <asp:DropDownList ID="ddSort" runat="server" Height="18px" Width="170px" Font-Size="8"
                OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Value="1">New Activity(Most Recent)</asp:ListItem>
                <asp:ListItem Value="2">Expiring</asp:ListItem>
                <asp:ListItem Value="3">Expired</asp:ListItem>
                <asp:ListItem Value="4">Title: A to Z</asp:ListItem>
                <asp:ListItem Value="5">Title: Z to A</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td valign="top" align="right" style="width: 50%">
            Activities per page :
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
<div id="divproductsListViewContent" runat="server" style="width: 700px">
    <asp:ListView ID="ListViewActivities" runat="server" DataSourceID="ods" OnItemDataBound="ListViewActivities_ItemDataBound"
        OnItemCommand="ListViewActivities_ItemCommand" OnPagePropertiesChanging="ListViewActivities_PagePropertiesChanging">
        <LayoutTemplate>
            <div id="ItemPlaceHolder" runat="server">
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <table width="100%" border="1px">
                <tr>
                    <td style="padding-top: 4px; vertical-align: top; width: 20px">
                        <asp:Label ID="lblNo" CssClass="listingTitle" Text="" runat="server"></asp:Label>
                        <asp:HiddenField ID="hdnActivityID" runat="server" Value='<%#Eval("ID") %>' />
                    </td>
                    <td>
                        <table width="100%" border="1px">
                            <tr>
                                <td class="style1" align="left" colspan="4">
                                    <asp:HyperLink ID="HlnkActivitiesName" CssClass="listingTitle" NavigateUrl="#" runat="server"><%#Eval("Name") %></asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="text-align: justify">
                                    <asp:Label ID="lblShortDescription" runat="server" Text='<%#Eval("ShortDescription") %>'></asp:Label><asp:HyperLink
                                        ID="HlnkReadMore" CssClass="hlnkReadMore" NavigateUrl="#" runat="server"> read more.</asp:HyperLink>
                                </td>
                                <td style="width: 100px; padding-left: 25px;">
                                    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%#Eval("Status") %>' />
                                    <asp:HiddenField ID="hdnExpiryDate" runat="server" Value='<%#Eval("ExpiryDate") %>' />
                                    <!--<asp:Image ID="imgStatus" runat="server" />-->
                                    <asp:Label ID="lblStatus" runat="server" Text="" CssClass="TextIcon"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="width: 100px; padding-left: 25px;">
                                    <asp:HiddenField ID="hdnType" runat="server" Value='<%#Eval("ActivityType") %>' />
                                    <asp:Label ID="lblType" runat="server" Text="" CssClass="TextIcon"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    service held at:
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>'></asp:Label><br />
                                    <asp:Label ID="lblSub" runat="server" Text='<%# Eval("Suburb") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnSuburb" runat="server" Value='<%#Eval("SuburbID") %>' />
                                    <asp:Label ID="lblState" runat="server" Text='<%#Eval("StateName") %>'></asp:Label>
                                    <asp:HiddenField ID="HiddenField3" runat="server" Value='<%#Eval("StateID") %>' />
                                    <asp:Label ID="lblPostCode" runat="server" Text='<%#Eval("PostCode") %>'></asp:Label>
                                    <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("PostCode") %>' />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblPhone" runat="server" Text='<%#Eval("PhoneNumber") %>'></asp:Label>
                                </td>
                                <td style="padding-left: 25px;">
                                    <asp:Label ID="lblExpiryDate" runat="server" Text="Expiry on"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <hr />
        </ItemTemplate>
    </asp:ListView>
    <asp:Label ID="lblPageBottom" runat="server" Text="Page :"></asp:Label>
    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListViewActivities">
        <Fields>
            <asp:NextPreviousPagerField ButtonType="Link" PreviousPageText="◄ Previous" ShowFirstPageButton="false"
                ShowLastPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="true" />
            <asp:NumericPagerField />
            <asp:NextPreviousPagerField ButtonType="Link" NextPageText="Next ►" ShowFirstPageButton="false"
                ShowLastPageButton="false" ShowNextPageButton="true" ShowPreviousPageButton="false" />
        </Fields>
    </asp:DataPager>
</div>
<div id="divproductsListViewCommand">
    <asp:HiddenField ID="hdnSearchKey" runat="server" />
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
            <td align="left" style="width: 50%;">
                Showing :
                <asp:Label ID="lblStartIndex1" runat="server"></asp:Label>-<asp:Label ID="lblEndIndex1"
                    runat="server"></asp:Label>
                &nbsp;of
                <asp:Label ID="lblAmount1" runat="server" Text=""></asp:Label>
                &nbsp;activities
            </td>
            <td id="tdPager2" runat="server" align="right" style="width: 50%;">
                &nbsp;
            </td>
        </tr>
    </table>
</div>