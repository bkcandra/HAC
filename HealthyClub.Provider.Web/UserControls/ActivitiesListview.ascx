<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivitiesListview.ascx.cs"
    Inherits="HealthyClub.Providers.Web.UserControls.ActivitiesListview" %>
<%@ Register Src="~/UserControls/ScheduleViewerUC.ascx" TagPrefix="uc1" TagName="ScheduleViewerUC" %>

<style>
    table thead th div.edit {
        float: right;
    }

    div.breaking {
        display: table-cell;
        word-break: break-all;
        position:relative;
        left:-2px;
    }

    .TextIcon {
        color: #1B274F;
    }

    .auto-style1 {
        width: 30px;
    }
</style>
<table width="100%">
    <tr>
        <td align="left" style="width: 50%;">Showing:&nbsp;
            <asp:Label ID="lblStartIndex" runat="server"></asp:Label>&nbsp;-
            <asp:Label ID="lblEndIndex"
                runat="server"></asp:Label>
            &nbsp;of
            <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
            &nbsp;activities
            <asp:Label ID="lblKeyword" runat="server" CssClass="SearchResults" Visible="false"></asp:Label>
        </td>
        <td id="tdPager1" runat="server" align="right" style="width: 50%">&nbsp;
        </td>
    </tr>
    <tr>
        <td valign="middle" align="left" style="width: 70%">Sort by&nbsp;
            <asp:DropDownList ID="ddSort" runat="server" Height="18px" Width="200px" Style="height: 24px" Font-Size="8"
                OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True"
                CssClass="bodyText1">
                <asp:ListItem Value="1">Newly listed</asp:ListItem>
                <%--<asp:ListItem Value="2">Expiry date</asp:ListItem>--%>
                <asp:ListItem Value="3">Expiry date</asp:ListItem>
                <asp:ListItem Value="4">Title: A - Z</asp:ListItem>
                <asp:ListItem Value="5">Title: Z - A</asp:ListItem>
                <asp:ListItem Value="6">Cost: Free - Paid</asp:ListItem>
                <asp:ListItem Value="7">Cost: Paid - Free</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td valign="top" align="right" style="width: 30%">
            <span>Activities per page&nbsp;</span>
            <asp:DropDownList ID="ddlPagingTop" runat="server" OnSelectedIndexChanged="ddlPagingTop_SelectedIndexChanged" Style="height: 24px"
                AutoPostBack="true">
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
            </asp:DropDownList>
            &nbsp;
        </td>
    </tr>
</table>
<br />
<div id="divproductsListViewContent" runat="server">
    <asp:ListView ID="ListViewActivities" runat="server" DataSourceID="ods" OnItemDataBound="ListViewActivities_ItemDataBound"
        OnItemCommand="ListViewActivities_ItemCommand" OnPagePropertiesChanging="ListViewActivities_PagePropertiesChanging"
        allowSorting="true" OnItemDeleted="ListViewActivities_ItemDeleted" 
        OnItemDeleting="ListViewActivities_ItemDeleting">
        <LayoutTemplate>
            <div id="ItemPlaceHolder" runat="server">
            </div>
        </LayoutTemplate>
        <EmptyDataTemplate>
            <span style="color: Red; padding: 10px 2px;">No activities listed.</span>
        </EmptyDataTemplate>
        <ItemTemplate>
            <table border="0" style="width: 100%">
                <thead>
                    <tr>
                        <th scope="col" colspan="3">
                            <div style="float: left">
                                <asp:HyperLink ID="HlnkActivitiesName" CssClass="ListViewlistingTitle" NavigateUrl="#" Text='<%#Eval("Name") %>'
                                    runat="server"></asp:HyperLink>
                            </div>
                            <div class="edit">
                                <asp:LinkButton ID="lnkDeleteAct" runat="server" CausesValidation="False"
                                    CommandName="DeleteAct" Text="Delete"
                                    OnClientClick="return confirm('Are you sure you want to delete this activity?  Once you have deleted an activity it will be removed in three days and cannot be recovered.')" />&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lnkEditAct" runat="server">Edit</asp:LinkButton>&nbsp;&nbsp;
                            </div>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td align="left" style="text-align: justify; padding: 5px">
                            <span style="font-weight: bold">Description</span><br />
                            <div id="divDescription" runat="server" >
                                <asp:Label ID="lblShortDescription" runat="server" Text='<%#Eval("FullDescription").ToString().Length > 250 ? Eval("FullDescription").ToString().Substring(0,250) : Eval("FullDescription").ToString()%>'></asp:Label>
                                <asp:HyperLink
                                    ID="HlnkReadMore" CssClass="hlnkReadMore" NavigateUrl="#" runat="server">&nbsp;&nbsp;[Read more...]</asp:HyperLink>
                                <asp:HiddenField ID="hdnActivityID" runat="server" Value='<%#Eval("ID") %>' />

                            </div>
                        </td>
                        <td style="width: 120px; padding: 5px 0 5px 5px; vertical-align: top"
                            rowspan="1">
                            <asp:Label ID="lblStatus" runat="server" Text="" CssClass="TextIcon"></asp:Label>
                            <asp:HiddenField ID="hdnType" runat="server" Value='<%#Eval("ActivityType") %>' />
                            <asp:HiddenField ID="hdnStatus" runat="server" Value='<%#Eval("Status") %>' />
                            <asp:HiddenField ID="hdnExpiryDate" runat="server" Value='<%#Eval("ExpiryDate") %>' />
                            <asp:HiddenField ID="hdnisApproved" runat="server" Value='<%#Eval("isApproved") %>' />
                        </td>
                        <td style="width: 30px; vertical-align: top; text-align: center" rowspan="2">
                            <asp:Image ID="imgStatus" runat="server" Style="max-height: 20px; max-width: 20px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding: 5px;">
                            <span style="font-weight: bold">Activity location</span><br />
                            <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>'></asp:Label><br />
                            <asp:Label ID="lblSub" runat="server" Text='<%# Eval("Suburb") %>'></asp:Label>
                            <asp:HiddenField ID="hdnSuburb" runat="server" Value='<%#Eval("SuburbID") %>' />
                            <asp:Label ID="lblState" runat="server" Text='<%#Eval("StateName") %>'></asp:Label>
                            <asp:HiddenField ID="HiddenField3" runat="server" Value='<%#Eval("StateID") %>' />
                            <asp:Label ID="lblPostCode" runat="server" Text='<%#Eval("PostCode") %>'></asp:Label>
                            <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("PostCode") %>' />
                             <asp:HiddenField ID="hdnModified" runat="server" Value='<%#Eval("ModifiedDateTime") %>' />
                        </td>
                        <td style="vertical-align: top; padding: 5px;">
                            <asp:Label ID="lblType" runat="server" Text="" CssClass="TextIcon"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding: 5px">
                            <span style="font-weight: bold">Activity Time</span><br />
                            <div id="divSchedule" runat="server" style="position: relative; left: -3px">
                                <uc1:ScheduleViewerUC runat="server" ID="ScheduleViewerUC" />
                            </div>
                        </td>
                        <td style="width: 155px; padding-left: 25px; padding: 5px; vertical-align: top" colspan="2">
                            <asp:Label ID="lblExpiryDate" runat="server" CssClass="TextIcon" Text="Expiry on"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
            <hr />
        </ItemTemplate>
    </asp:ListView>
    <div id="divPager" runat="server">
        <asp:Label ID="lblPageBottom" runat="server" Text="Page:"></asp:Label>
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
    <table id="tabShowAct" runat="server" style="width: 100%;">
        <tr>
            <td align="left" style="width: 50%;">Showing:
                <asp:Label ID="lblStartIndex1" runat="server"></asp:Label>&nbsp;-
                <asp:Label ID="lblEndIndex1"
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


