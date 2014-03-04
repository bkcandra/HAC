<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RewardDetail.ascx.cs" Inherits="HealthyClub.Customer.Web.UserControls.RewardDetail" %>
<link rel="stylesheet" href="../Content/rewardlist.css" type="text/css" />

<div id ="Links">
    <asp:HyperLink ID ="hlnkHome" Text="Home" NavigateUrl="~/Rewards/RewardsHome" runat="server"></asp:HyperLink>&nbsp;<asp:Image ID="image1" ImageUrl="../Images/rightArrow.gif" runat="server"></asp:Image>&nbsp;
            <asp:HyperLink ID ="hlnkRewardsShop" Text="Rewards Shop" NavigateUrl="~/Rewards/Rewardshop" runat="server"></asp:HyperLink>&nbsp;<asp:Image ID="image2" ImageUrl="../Images/rightArrow.gif" runat="server"></asp:Image>&nbsp;
            <asp:Label ID="lblsearch" runat="server" ForeColor="Red"></asp:Label>
    
</div>
<br />
    <br />

  <div class="ProductGroupHeading">
                <h1 style="margin:0px"><span><asp:Label ID="lblName" runat="server"></asp:Label></span></h1>
            <br />
            </div>
     
<div id ="ProductDetailsDiv" style="padding:8px;">
    <table>
        <tbody>
            <tr>
                <td class="ProductImageAttributeVariety">
                    <div style="width:250px">
                        <div class="productDetailImagePanel">
                            <asp:Image ID="imgPreview" runat="server" class="productImage" />
                        </div>
                        
                    </div>
                    <div class="BoxSelector">
                        <div class="UpperBox">
                            <div class="BoxContainer"><span class="originalPoints" style="font-weight: bold">Points:&nbsp<asp:Label ID="lblpoints" runat="server"></asp:Label></span></div>
                                                  
                        </div>
                        <div class="BoxFooter">
                            
                            <span style="font-weight: bold" class="span3">Expires On:</span>
                            <br />
                            <asp:Label ID="RwdExpiry" runat="server"></asp:Label>
                            <br />
                            
                            
                            
                        </div>

                    </div>

                </td>
                <td style="width:10px;"></td>
                <td class="Details">
                    <span style="font-weight: bold">Description</span><br />
                     <asp:Label ID="lblLongDescription" runat="server"></asp:Label>
                  <br />
                    <span style="font-weight: bold">Redemption</span><br />
                       <asp:Button ID="Addtocart" runat="server" Text="Add To Cart" CssClass="button button-submit" OnClick="Addtocart_Click" />
                        <asp:Button ID="AddtocartNcheckout" runat="server" Text="Add To Cart and Redeem" CssClass="button button-submit" OnClick="AddtocartNcheckout_Click" />
                            <asp:Label ID="lblType" runat="server" Text="" CssClass="TextIcon"></asp:Label><br /><br />
                <asp:Label ID="lblerror" runat="server" Text="You do not have enough points to add this Reward" ForeColor="Red"></asp:Label>                       
                </td>
</tr>
            </tbody>
        </table>
</div>
<asp:HiddenField ID="hdnRewardExpiry" runat="server" />
<asp:HiddenField ID="hdnRewardID" runat="server" />
<asp:HiddenField ID="hdnRwdDescription" runat="server" />


