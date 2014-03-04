<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RewardsView.ascx.cs" Inherits="HealthyClub.Customer.Web.UserControls.RewardsView" %>


<table width="100%">
    <tr>
        <td align="left">
            <asp:HyperLink ID ="hlnkHome" Text="Home" NavigateUrl="~/Rewards/RewardsHome" runat="server" Visible="false"></asp:HyperLink>&nbsp;<asp:Image ID="image1" ImageUrl="../Images/rightArrow.gif"  Visible ="false" runat="server"></asp:Image>&nbsp;
            <asp:HyperLink ID ="hlnkRewardsShop" Text="Rewards Shop" NavigateUrl="~/Rewards/Rewardshop" runat="server" Visible="false"></asp:HyperLink>&nbsp;<asp:Image ID="image2" ImageUrl="../Images/rightArrow.gif"  Visible ="false" runat="server"></asp:Image>&nbsp;
            <asp:Label ID="lblsearch" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            <br />
        </td>

    </tr>
    <tr>
        <td align="left" colspan="2">Showing:
            <asp:Label ID="lblStartIndex" runat="server"></asp:Label>&nbsp;-
            <asp:Label ID="lblEndIndex"
                runat="server"></asp:Label>
            &nbsp;of
            <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
            &nbsp;Rewards
            <asp:Label ID="lblKeyword" runat="server" CssClass="SearchResults" Visible="True"></asp:Label>
        </td>

    </tr>
    <tr>
        <td valign="middle" align="left" style="width: 60%">Sort by&nbsp;
            <asp:DropDownList ID="ddSort" runat="server" Width="225px" CssClass="limitOptions bodyText1" Style="height: 24px"
                OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Value="1">Recently added</asp:ListItem>
                <asp:ListItem Value="4">Reward title: A to Z</asp:ListItem>
                <asp:ListItem Value="5">Reward title: Z to A</asp:ListItem>
                <asp:ListItem Value="6">Reward Points: Low - High</asp:ListItem>
                <asp:ListItem Value="7">Reward Points: High - Low</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td valign="top" align="right" style="width: 40%">
            <span>Rewards per page&nbsp;</span>
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
<table><tr><th><asp:Label ID="lblerror" runat="server" Text="You do not have enough points to add this Reward" ForeColor="Red"></asp:Label></th></tr></table>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<div id="divproductsListViewContent" runat="server" style="width: 100%">
    <asp:ListView ID="ListViewRewards" runat="server" DataSourceID="ods" OnItemDataBound="ListViewRewards_ItemDataBound"
        OnItemCommand="ListViewRewards_ItemCommand" OnPagePropertiesChanging="ListViewRewards_PagePropertiesChanging">
        <LayoutTemplate>
            <div id="ItemPlaceHolder" runat="server">
            </div>
        </LayoutTemplate>
        <EmptyDataTemplate>
            <div style="color: Red; margin: 10px 2px; font-size: 15px">No Rewards listed.</div>
        </EmptyDataTemplate>
        <ItemTemplate>
            <table style="width: 100%" border="0">
                <thead>
                    <tr>
                        <th scope="col" align="left" colspan="4" >
                            <asp:HiddenField ID="hdnRewardsID" runat="server" Value='<%#Eval("ID") %>' />
                            <asp:HiddenField ID="hdnRewardImage" runat="server" Value='<%#Eval("RewardImage") %>'/>
                            <asp:HyperLink ID="HlnkRewardsName" CssClass="ListViewlistingTitle" NavigateUrl="#" style="display:block"
                                runat="server"><%#Eval("RewardsName") %></asp:HyperLink>
                        </th>
                        </tr>
                </thead>

                <tbody>
                    <tr>
                        
                        <td style="width: 155px; padding-left: 25px; padding: 5px; text-align: center;"
                            rowspan="2">
                            
                            <asp:HiddenField ID="hdnExpiryDate" runat="server" Value='<%#Eval("RewardDescription") %>' />
                            
                            <asp:Image ID="imgPreview" runat="server" Style="max-width: 150px; max-height: 150px" />
                            <span style="font-weight: bold">Points:</span>&nbsp<asp:Label ID="lblpoints" runat="server" Text='<%#Eval("RequiredRewardPoint").ToString().Length > 250 ? Eval("RequiredRewardPoint").ToString().Substring(0,250) : Eval("RequiredRewardPoint").ToString()%>'></asp:Label>
                        </td>
                        
                        <td align="left" style="text-align: justify; padding: 5px">
                            <span style="font-weight: bold">Description</span><br />
                            <asp:Label ID="lblShortDescription" runat="server" Text='<%#Eval("RewardDescription").ToString().Length > 250 ? Eval("RewardDescription").ToString().Substring(0,250) : Eval("RewardDescription").ToString()%>'></asp:Label><asp:HyperLink
                                ID="HlnkReadMore" CssClass="hlnkReadMore" NavigateUrl="#" runat="server"> [Read More...]</asp:HyperLink>
                       <br /><br />
                            <span style="font-weight: bold">Expires On:</span><br />
                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("RewardExpiryDate").ToString().Length > 250 ? Eval("RewardExpiryDate").ToString().Substring(0,250) : Eval("RewardExpiryDate").ToString()%>'></asp:Label>
                            
                       
                          
                            <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("RewardExpiryDate") %>' />
                            
                            <asp:Image ID="Image1" runat="server" Style="max-width: 150px; max-height: 150px" />
                            <br />
                        <br />
                
                           <asp:Button ID="Addtocart" runat="server" Text="Add To Cart" CssClass="button button-submit" OnClick="AddToCart_Click" />
                        <asp:Button ID="AddtocartNcheckout" runat="server" Text="Add To Cart and Redeem" CssClass="button button-submit" OnClick="Checkout_Click" />
                            <asp:Label ID="lblType" runat="server" Text="" CssClass="TextIcon"></asp:Label>
                            

                        </td>
                    
                    </tr>
                   
                </tbody>
            </table>
            
            <hr />
        </ItemTemplate>
    </asp:ListView>
    <div id="divPager" runat="server" class="pages">
                <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListViewRewards">
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
                &nbsp;Rewards
          
            </div>
        </div>
</ContentTemplate>
</asp:UpdatePanel>
<asp:HiddenField ID="hdnSearchKey" runat="server" />
<asp:HiddenField ID="hdnSortValue" runat="server" />
<asp:HiddenField ID="hdnStartRow" runat="server" />
<asp:HiddenField ID="hdnProviderID" runat="server" />
<asp:HiddenField ID="hdnCategoryID" runat="server" />
<asp:HiddenField ID="hdnRewardType" runat="server" />
<asp:HiddenField ID="hdnCartItems" runat="server" />
<asp:HiddenField ID="hdnAgeFrom" runat="server" />
<asp:HiddenField ID="hdnAgeTo" runat="server" />
<asp:HiddenField ID="hdnPageSize" runat="server" />
<asp:HiddenField ID="hdnFiltered" runat="server" />
<asp:ObjectDataSource ID="ods" runat="server"></asp:ObjectDataSource>

