<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivitiesTableView.ascx.cs"
    Inherits="HealthyClub.Web.UserControls.ActivitiesTableView" %>
<style type="text/css">
    .modalBackground {
        background-color: Gray;
        filter: alpha(opacity=50);
        opacity: 0.7;
    }

    #tableContent table tr:hover td div.button {
        display: inline-block;
    }

    #tableContent table tr td div.button {
        display: none;
    }

    #tableContent table tr:nth-child(even) td {
        background-color: #F3F3F3;
    }

    #tableContent table tr:nth-child(even):hover td {
        color: #333333;
        background-color: #E5E5D8;
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
        padding: 1px 8px;
        margin: 6px 9px;
        text-align: left;
        vertical-align: top;
    }

    #tableContent table thead th {
        background-color: #86B788;
        border-bottom: 1px solid #ccc;
        border-left: 1px solid #D9D9D9;
        font-weight: bold;
        text-align: left;
        vertical-align: top;
        padding: 6px 9px;
        color: White;
    }

    #tableContent table tbody tr th {
        background-color: #86B788;
        border-bottom: 1px solid #ccc;
        border-left: 1px solid #D9D9D9;
        font-weight: bold;
        text-align: left;
        padding: 6px 9px;
        color: White;
    }



    #tableContent table tbody tr:hover {
        color: #333333;
        background-color: #E5E5D8;
    }

    #tableContent table tbody tr td {
        vertical-align: middle;
        padding: 10px 5px;
        font-family: Tahoma;
        font-weight: bold;
    }

    #tableContent table tfoot td, #tableContent table tfoot th {
        border-top: 1px solid #ccc;
        font-weight: bold;
        color: #592C16;
        padding: 6px 9px;
        font-size: 14px;
    }

    .style3 {
        width: 4px;
    }

    .style4 {
        width: 66px;
    }
</style>
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
        <td valign="middle" align="left" style="width: 50%">Sort by&nbsp;
            <asp:DropDownList ID="ddSort" runat="server" Width="225px" CssClass="limitOptions bodyText1"
                OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Value="1">Recently added</asp:ListItem>
                <asp:ListItem Value="4">Activity title: A to Z</asp:ListItem>
                <asp:ListItem Value="5">Activity title: Z to A</asp:ListItem>
                <asp:ListItem Value="6">Cost: Free - Paid</asp:ListItem>
                <asp:ListItem Value="7">Cost: Paid - Free</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td valign="top" align="right" style="width: 50%">
            <span>Activities per page&nbsp;</span>
            <asp:DropDownList ID="ddlPagingTop" runat="server" CssClass="limitOptions bodyText1"
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
<div id="tableContent">
    <asp:GridView ID="GridViewActivities" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataSourceID="ods" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
        OnRowDataBound="GridView1_RowDataBound" Width="100%">
        <EmptyDataTemplate>
            <div style="color: Red; margin: 10px 0; font-size: 15px">No activities listed.</div>
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="Activity Name">
                <ItemTemplate>
                    <%--<asp:LinkButton ID="lnkProductName" runat="server" CommandName="SetProduct" Text='<%#String.Format("{0} - {1}",Eval("BrandName"), Eval("Name")) %>'></asp:LinkButton>--%>
                    <asp:HyperLink ID="HlnkOrgName" CssClass="listingTitle" NavigateUrl="#" runat="server"><%#Eval("ProviderName") %></asp:HyperLink>&nbsp;-&nbsp;
                    <asp:HyperLink ID="HlnkActivitiesName" CssClass="listingTitle" NavigateUrl="#" runat="server"><%#Eval("Name") %></asp:HyperLink>
                    <%--<%#Eval("Suburb") %>--%>
                    <asp:HiddenField ID="hdnActivityID" runat="server" Value='<%#Eval("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Details">
                <ItemTemplate>
                    <asp:Label ID="lblType" runat="server" CssClass="listingTitle"></asp:Label>

                    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%#Eval("Status") %>' />
                    <asp:HiddenField ID="hdnExpiryDate" runat="server" Value='<%#Eval("ExpiryDate") %>' />
                    <asp:HiddenField ID="hdnType" runat="server" Value='<%#Eval("ActivityType") %>' />
                </ItemTemplate>
                <ItemStyle Width="250px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
<br />
<div id="divproductsListViewCommand">
    <asp:HiddenField ID="hdnSearchKey" runat="server" />
    <asp:HiddenField ID="hdnPage" runat="server" />
    <asp:HiddenField ID="hdnDateFrom" runat="server" />
    <asp:HiddenField ID="hdnDateTo" runat="server" />
    <asp:HiddenField ID="hdnTmTo" runat="server" />
    <asp:HiddenField ID="hdnTmFrom" runat="server" />
    <asp:HiddenField ID="hdnSortValue" runat="server" />
    <asp:HiddenField ID="hdnStartRow" runat="server" />
    <asp:HiddenField ID="hdnProviderID" runat="server" />
    <asp:HiddenField ID="hdnCategoryID" runat="server" />
    <asp:HiddenField ID="hdnPageSize" runat="server" />
    <asp:ObjectDataSource ID="ods" runat="server"></asp:ObjectDataSource>
    <asp:HiddenField ID="hdnFiltered" runat="server" />
    <asp:HiddenField ID="hdnSuburbID" runat="server" />
    <asp:HiddenField ID="hdnAgeFrom" runat="server" />
    <asp:HiddenField ID="hdnAgeTo" runat="server" />
    <asp:HiddenField ID="hdnMonFiltered" runat="server" />
    <asp:HiddenField ID="hdnTueFiltered" runat="server" />
    <asp:HiddenField ID="hdnWedFiltered" runat="server" />
    <asp:HiddenField ID="hdnThuFiltered" runat="server" />
    <asp:HiddenField ID="hdnFriFiltered" runat="server" />
    <asp:HiddenField ID="hdnSatFiltered" runat="server" />
    <asp:HiddenField ID="hdnSunFiltered" runat="server" />
    <asp:HiddenField ID="hdnTimespan" runat="server" />
</div>
<div style="width: 100%;">
    <table width="100%">
        <tr>
            <td align="left" style="width: 50%;">Showing:
                <asp:Label ID="lblStartIndex1" runat="server"></asp:Label>&nbsp;-&nbsp;<asp:Label
                    ID="lblEndIndex1" runat="server"></asp:Label>
                &nbsp;of
                <asp:Label ID="lblAmount1" runat="server" Text=""></asp:Label>
                &nbsp;activities
            </td>
            <td id="tdPager2" runat="server" align="right" style="width: 50%;">&nbsp;
            </td>
        </tr>
    </table>
</div>
