<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SavedListUC.ascx.cs" Inherits="HealthyClub.Customer.Web.UserControls.SavedListUC" %>
<%@ Register src="ScheduleViewerUC.ascx" tagname="ScheduleViewerUC" tagprefix="uc1" %>

<style>
    div.breaking {
        display: table-cell;
        word-break: break-all;
        position: relative;
        left: -2px;
    }
</style>
<table style="width: 100%">
    <tr>
        <td style="width: 60%"> Showing&nbsp;<asp:Label ID="lblStartIndex" runat="server"></asp:Label>&nbsp;-
            <asp:Label ID="lblEndIndex"
                runat="server"></asp:Label>
            &nbsp;of
            <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
            &nbsp;activities
            <asp:Label ID="lblKeyword" runat="server" CssClass="SearchResults" Visible="false"></asp:Label></td>
        <td style="width: 40%; text-align: right">
            <span>Activities per page&nbsp;</span>
            <asp:DropDownList ID="ddlPagingTop" runat="server" CssClass="limitOptions bodyText1" Style="height: 24px"
                OnSelectedIndexChanged="ddlPagingTop_SelectedIndexChanged" Width="50px" AutoPostBack="true">
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
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="divproductsListViewContent" runat="server" style="width: 100%">
            <asp:ListView ID="ListViewActivities" runat="server" OnItemDataBound="ListViewActivities_ItemDataBound"
                OnItemCommand="ListViewActivities_ItemCommand" OnPagePropertiesChanging="ListViewActivities_PagePropertiesChanging" OnSorting="ListViewActivities_Sorting">
                <LayoutTemplate>
                    <div id="ItemPlaceHolder" runat="server">
                    </div>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <div style="color: Red; margin: 10px 2px; font-size: 15px">No saved activities.</div>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <table style="width: 100%" border="0">
                        <thead>
                            <tr>
                                <th scope="col" colspan="4">
                                    <span style="float: left;">
                                        <asp:HyperLink ID="HlnkActivitiesName" CssClass="ListViewlistingTitle" NavigateUrl="#" Style="padding-top: 3px"
                                            runat="server" Text='<%#Eval("Name") %>'></asp:HyperLink>
                                    </span>
                                    <span style="float: right; margin: 0; padding: 0">
                                        <asp:Image ID="imgCostIcon" runat="server" Style="max-height: 26px; max-width: 26px;" />
                                    </span>
                                    <span style="clear: both;"></span>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td style="text-align: justify; padding: 5px">
                                    <span style="font-weight: bold">Description</span><br />
                                    <div id="divDescription" runat="server">
                                        <asp:Label ID="lblShortDescription" runat="server" Text='<%#Eval("FullDescription").ToString().Length > 250 ? Eval("FullDescription").ToString().Substring(0,250) : Eval("FullDescription").ToString()%>'></asp:Label><asp:HyperLink
                                            ID="HlnkReadMore" CssClass="hlnkReadMore" NavigateUrl="#" runat="server"> [Read More...]</asp:HyperLink>
                                    </div>
                                </td>
                                <td style="width: 155px; padding-left: 25px; padding: 5px; text-align: center;"
                                    rowspan="2">
                                    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%#Eval("Status") %>' />
                                    <asp:HiddenField ID="hdnExpiryDate" runat="server" Value='<%#Eval("ExpiryDate") %>' />
                                    <asp:HiddenField ID="hdnType" runat="server" Value='<%#Eval("ActivityType") %>' />
                                    <asp:HiddenField ID="hdnProviderID" runat="server" Value='<%#Eval("ProviderID") %>' />
                                    <asp:HiddenField ID="hdnActivityID" runat="server" Value='<%#Eval("ID") %>' />
                                    <asp:Image ID="imgPreview" runat="server" Style="max-width: 150px; max-height: 150px" />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px">
                                    <span style="font-weight: bold">Activity location</span><br />
                                    <asp:Label ID="lblProviderName" runat="server" Text='<%#Eval("ProviderName") %>'></asp:Label><br />
                                    <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>'></asp:Label><br />
                                    <asp:Label ID="lblSub" runat="server" Text='<%# Eval("Suburb") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnSuburb" runat="server" Value='<%#Eval("SuburbID") %>' />
                                    <asp:Label ID="lblState" runat="server" Text='<%#Eval("StateName") %>'></asp:Label>
                                    <asp:HiddenField ID="HiddenField3" runat="server" Value='<%#Eval("StateID") %>' />
                                    <asp:Label ID="lblPostCode" runat="server" Text='<%#Eval("PostCode") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnPostCode" runat="server" Value='<%#Eval("PostCode") %>' />
                                    <asp:HiddenField ID="hdnisSaved" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px">
                                    <span style="font-weight: bold">Activity Time</span><br />
                                    <div id="divSchedule" runat="server" style="position: relative; left: -3px">
                                        <uc1:ScheduleViewerUC runat="server" ID="ScheduleViewerUC" />
                                    </div>

                                </td>
                                <td id="tdSaved" runat="server" style="text-align: right; vertical-align: bottom; padding: 5px">
                                    <asp:LinkButton ID="lnkSaved" class="btn-icon btn-white btn-star btn-radius" runat="server" CausesValidation="false" CommandName="ToggleSave">
                                        <span></span>
                                        <asp:Label ID="lblSaved" runat="server" Text="Save Activity" AssociatedControlID="lnkSaved"></asp:Label>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>

                    <hr />
                </ItemTemplate>
            </asp:ListView>
            <div id="divPager" runat="server" class="pages">
                <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListViewActivities">
                    <Fields>
                        <asp:NextPreviousPagerField ButtonType="Link" PreviousPageText="◄ Previous" ShowFirstPageButton="false"
                            ShowLastPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="true" ButtonCssClass="nextprev" />
                        <asp:NumericPagerField CurrentPageLabelCssClass="current" />
                        <asp:NextPreviousPagerField ButtonType="Link" NextPageText="Next ►" ShowFirstPageButton="false"
                            ShowLastPageButton="false" ShowNextPageButton="true" ShowPreviousPageButton="false" ButtonCssClass="nextprev" />
                    </Fields>
                </asp:DataPager>
            </div>
            <div style="clear: both"></div>
            <div id="ItemCountBottom" runat="server" style="width: 100%; padding: 0.2em 0.5em;">
                Showing:
                <asp:Label ID="lblStartIndex1" runat="server"></asp:Label>&nbsp;-&nbsp;<asp:Label ID="lblEndIndex1"
                    runat="server"></asp:Label>
                &nbsp;of
                <asp:Label ID="lblAmount1" runat="server" Text=""></asp:Label>
                &nbsp;activities
          
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:HiddenField ID="hdnSearchKey" runat="server" />
<asp:HiddenField ID="hdnSortValue" runat="server" />
<asp:HiddenField ID="hdnStartRow" runat="server" />
<asp:HiddenField ID="hdnProviderID" runat="server" />
<asp:HiddenField ID="hdnCategoryID" runat="server" />
<asp:HiddenField ID="hdnSuburbID" runat="server" />
<asp:HiddenField ID="hdnAgeFrom" runat="server" />
<asp:HiddenField ID="hdnAgeTo" runat="server" />
<asp:HiddenField ID="hdnPageSize" runat="server" />
<asp:HiddenField ID="hdnDateFrom" runat="server" />
<asp:HiddenField ID="hdnDateTo" runat="server" />
<asp:HiddenField ID="hdnFiltered" runat="server" />
<asp:HiddenField ID="hdnMonFiltered" runat="server" />
<asp:HiddenField ID="hdnTueFiltered" runat="server" />
<asp:HiddenField ID="hdnWedFiltered" runat="server" />
<asp:HiddenField ID="hdnThuFiltered" runat="server" />
<asp:HiddenField ID="hdnFriFiltered" runat="server" />
<asp:HiddenField ID="hdnSatFiltered" runat="server" />
<asp:HiddenField ID="hdnSavedList" runat="server" />
<asp:HiddenField ID="hdnSunFiltered" runat="server" />
<asp:HiddenField ID="hdnTimespan" runat="server" />
<asp:ObjectDataSource ID="ods" runat="server"></asp:ObjectDataSource>

