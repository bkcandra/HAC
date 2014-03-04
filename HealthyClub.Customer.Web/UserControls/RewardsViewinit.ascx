<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RewardsViewinit.ascx.cs" Inherits="HealthyClub.Customer.Web.UserControls.RewardsViewinit" %>
<link rel="stylesheet" href="../Content/rewardlist.css" type="text/css" />
<table width="100%">
    <tr>
        <td align="left" colspan="2">Showing:
            <asp:Label ID="lblStartIndex" runat="server"></asp:Label>&nbsp;-
            <asp:Label ID="lblEndIndex"
                runat="server"></asp:Label>
            &nbsp;of
            <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
            &nbsp;Rewards
            <asp:Label ID="lblKeyword" runat="server" CssClass="SearchResults" Visible="false"></asp:Label>
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
                <asp:ListItem>8</asp:ListItem>
                <asp:ListItem>16</asp:ListItem>
               
            </asp:DropDownList>
            &nbsp;
        </td>
        </tr>
        
    
        </table>
<br />

            <div class="ProductGroupHeading">
                <h1 style="margin:0px"><span>All Rewards</span></h1>
            <br />
            </div>
            

<div id="divproductsListViewContent" runat="server" style="width: 100%">
    <asp:ListView ID="ListViewRewards" runat="server" DataSourceID="ods" OnItemDataBound="ListViewRewards_ItemDataBound"
        OnItemCommand="ListViewRewards_ItemCommand" OnPagePropertiesChanging="ListViewRewards_PagePropertiesChanging" GroupItemCount="4">


    <LayoutTemplate>
<div class ="ViewDepartProductList">
        <table width:"100%">
            <tbody>
                <asp:PlaceHolder runat="server" ID="groupPlaceHolder"></asp:PlaceHolder>
            </tbody>
                </table>
     </div>
    </LayoutTemplate>
  <GroupTemplate>
      <tr>
         <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
      </tr>
   </GroupTemplate>
        <EmptyDataTemplate>
            <div style="color: Red; margin: 10px 2px; font-size: 15px">No Rewards listed.</div>
        </EmptyDataTemplate>
                  
        <ItemTemplate>
            
        
                      <td>
                          <div class="ProductBrowseViewMain">
                              <div class="productImagePanel">
                                   <asp:HyperLink ID="HlnkImg" CssClass="ListViewlistingTitle" NavigateUrl="#" style="display:block; text-align:center"
                                runat="server"><asp:Image ID="imgPreview" runat="server" Style="max-width: 150px; max-height: 150px;"/></asp:HyperLink>
                              </div>
                              <div class="ProductBrowseTitle"> <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("ID") %>' />
                            <asp:HiddenField ID="hdnRewardsID" runat="server" Value='<%#Eval("ID") %>' />
                                  <asp:HiddenField ID="hdnRewardImage" runat="server" Value='<%#Eval("RewardImage") %>'/>
                                  <asp:HyperLink ID="HlnkRewardsName" CssClass="ListViewlistingTitle" NavigateUrl="#" style="display:block"
                                runat="server"><%#Eval("RewardsName") %></asp:HyperLink></div>
                          </div>
                          <div class="productPoints">
                              <div class="pointsContainer"><span class="originalPoints" style="font-weight: bold">Points:&nbsp<asp:Label ID="Label3" runat="server" Text='<%#Eval("RequiredRewardPoint").ToString().Length > 250 ? Eval("RequiredRewardPoint").ToString().Substring(0,250) : Eval("RequiredRewardPoint").ToString()%>'></asp:Label></span></div>
                          </div>

                      </td>
            
                
            
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
<asp:HiddenField ID="hdnSearchKey" runat="server" />
<asp:HiddenField ID="hdnSortValue" runat="server" />
<asp:HiddenField ID="hdnStartRow" runat="server" />
<asp:HiddenField ID="hdnProviderID" runat="server" />
<asp:HiddenField ID="hdnCategoryID" runat="server" />
<asp:HiddenField ID="hdnRewardType" runat="server" />
<asp:HiddenField ID="hdnAgeFrom" runat="server" />
<asp:HiddenField ID="hdnAgeTo" runat="server" />
<asp:HiddenField ID="hdnPageSize" runat="server" />
<asp:HiddenField ID="hdnFiltered" runat="server" />
<asp:ObjectDataSource ID="ods" runat="server"></asp:ObjectDataSource>



