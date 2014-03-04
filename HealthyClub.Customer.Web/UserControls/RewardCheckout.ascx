<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RewardCheckout.ascx.cs" Inherits="HealthyClub.Customer.Web.UserControls.RewardCheckout" %>
<link rel="stylesheet" href="../Content/rewardlist.css" type="text/css" />
<Content-Disposition: attachment; filename="<coupons.pdf>">
<style type="text/css">
        @media print
        {
            .printButton
            {
                display: none;
            }
        
        }
    </style> <script type = "text/javascript">
                 function PrintPanel() {
                     var panel = document.getElementById("<%=coupons.ClientID%>");
            var printWindow = window.open('', 'Print.html', 'height=500,width=1000');
            printWindow.document.write('<html><head><title>Print Your Vouchers</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            
            setTimeout(function () {
                printWindow.print()
            }, 500);
            
            printWindow.document.write('</body></html>');
            printWindow.document.close();
         
            return false;
           }

                 function save() {
                     
                      //var content = coupons.innerHTML;
                      //uriContent = "data:application/octet-stream," + encodeURIComponent(content);
                     //newWindow=window.open(uriContent, 'coupon');
                     var panel = document.getElementById("<%=coupons.ClientID%>");
                     str = panel.innerHTML;
                     mydoc = document.open();
                     mydoc.write(str);
                     mydoc.execCommand("saveAs", true, "coupon.html");
                     mydoc.close();
                     return false;
                       
                     }
                    
                
    </script>
  <div class="ProductGroupHeading">
                <h1 style="margin:0px"><span>Redemption Cart</span></h1>
            <br />
            </div>

<div class="contentcartmain" style="width:710px;">
<div class="SecondaryColor">
        <div class="ContentSection">
            <div class="ContentCart">
                Your Cart
            </div>
            <div class="ContentSectionBody">    
                
              <div id="divproductsListViewContent" runat="server" style="width: 100%">
    <asp:ListView ID="RewardCheckoutView" runat="server" DataSourceID="ods" OnItemDataBound="RewardCheckoutView_ItemDataBound"
        OnItemCommand="RewardCheckoutView_ItemCommand" OnPagePropertiesChanging="RewardCheckoutView_PagePropertiesChanging" Header="Cant">
        <LayoutTemplate>
            <table style="width: 100%; border-collapse: collapse;" border="0" cellspacing="0" cellpadding="3" runat="server">
                
                <thead>
                    
                        <tr runat="server">
                            <th style="white-space: nowrap;" runat="server" align="center">
                                
                            </th>
                            <th scope="col" colspan="4" align="center" runat="server">
                               Description 
                            </th>
                            <th align="right" style="white-space: nowrap;" scope="col" runat="server">
                                
                            </th>
                            
                            <th align="right" style="white-space: nowrap;" scope="col" runat="server">
                                
                            </th>
                            <th align="right" scope="col" runat="server">
                                Quantity
                            </th>
                            <th align="right" style="white-space: nowrap;" scope="col" runat="server">
                                
                            </th>
                            <th align="right" style="white-space: nowrap;" scope="col" runat="server">
                                
                            </th>
                            <th align="right" scope="col" runat="server">
                                Points
                            </th>
                        </tr>
                    
                        </thead>
                <tbody>
                        <tr id ="ItemPlaceholder" runat="server">
                            <td runat="server"></td>
                            <td runat="server"></td>
                            <td runat="server"></td>
                            <td runat="server"></td>
                            <td runat="server"></td>
                            <td runat="server"></td>


                        </tr>
            
                        </tbody>
                </table>

        </LayoutTemplate>
        <EmptyDataTemplate>
            <div style="color: black; margin: 10px 2px; font-size: 15px">There No Items in the Cart</div>
        </EmptyDataTemplate>
        <ItemTemplate>
    
    
            
            <tr>
                <td valign="top" colspan="1" align="center">
                    <asp:HiddenField ID="hdnRewardImage" runat="server" value='<%#Eval("RewardImage") %>' />
                    <asp:Image ID="imgPreview" runat="server" Style="max-width: 100px; max-height: 100px" />
                </td>
                <td align="left" colspan="6">
                  
                            <asp:HyperLink ID="HlnkRewardsName" NavigateUrl="#" style="display:block"
                                runat="server"><%#Eval("RewardsName") %></asp:HyperLink>
                    
                    <span style="font-size: 80%;">Expiry Date:</span>&nbsp;
                        <asp:Label ID="lblExpiry" runat="server" style="font-size: 80%;"  Text='<%#Eval("RewardExpiryDate").ToString().Length > 250 ? Eval("RewardExpiryDate").ToString().Substring(0,250) : Eval("RewardExpiryDate").ToString()%>'></asp:Label>
                    
                </td>
                <td valign="center" colspan="1" align="right">
                    <asp:TextBox ID="lblquant" runat="server" style="width:20px;" OnTextChanged="lblquant_textchanged"></asp:TextBox> <asp:HiddenField ID="hiddenquant" runat="server"/>              
                </td>
                <td rowspan="1" align="center" valign="top">
                    <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("RewardExpiryDate") %>' />
                    </td>
                <td align="right" valign="top">
                  <asp:HiddenField ID="hdnRewardsID" runat="server" Value='<%#Eval("ID") %>' />              
                            </td>

                <td align="right" valign="center">
                    <asp:label ID="lblPoints" runat="server" Text='<%#Eval("RequiredRewardPoint").ToString().Length > 250 ? Eval("RequiredRewardPoint").ToString().Substring(0,250) : Eval("RequiredRewardPoint").ToString()%>'></asp:label>

                </td>
                <td valign="center">
                    <asp:Button ID="Remove" runat="server" Text="Remove" CssClass="button button-submit" OnClick="Remove_Click" />
                </td>
            </tr>

                </div>
    
    
    
            </ItemTemplate>
           
        </asp:ListView>
</div><div style="width:85%;height:40px">
        <div style="float:right">
            <table>
                <tbody>
                <tr>
                        <td colspan="2"></td>
                         <td>
                                 
                            </td>
                            <td align="right" style="white-space: nowrap;">
                                <strong>Subtotal:</strong>&nbsp;&nbsp;
                            </td>
                            <td align="right" style="white-space: nowrap;font-weight:bold;"><asp:label ID="lblTotal" runat="server"></asp:label></td>
                        <td></td>
                    </tr></tbody></table></div></div>
            </div>
    </div>
    </div>
    <div style="width:100%;height:40px">
        <div style="float:right; padding-top:5px";>
            <table>
                <tbody>
                <tr>
                    <td><asp:label ID ="cannotadd" Text="You do not have enough points to update the cart" ForeColor="Red" runat="server" Visible="false"></asp:label></td>
                        <td colspan="2"></td>
                         <td  align="right" style="white-space: nowrap;">
                                 <asp:Button ID="updatecart" runat="server" Text="Update Cart" CssClass="button button-submit" OnClick="Updatecart_Click" />
                            </td>
                            <td align="right" style="white-space: nowrap;">
                               <asp:Button ID="continueshop" runat="server" Text="Continue Shopping" CssClass="button button-submit" OnClick="continueshop_Click" />
                            </td>
                            <td align="right" style="white-space: nowrap;font-weight:bold;"><asp:Button ID="checkout" runat="server" Text="Checkout" CssClass="button button-submit" OnClick="checkout_Click" /></td>
                        <td></td>
                    </tr></tbody></table></div></div>
    </div>

<div id="Checkoutview" visible="false" runat="server">
    <table style="width:690px;">
    <tbody>
            <tr>
                    <td colspan="3">
                        <span class="bodyText1"><strong>Review and accept the following agreements</strong></span>
                        <div style="padding-left: 20px">
                             <ul class="style10" style="outline: 0;">
                                <li class="style10" style="outline: 0;">
                                    <asp:HyperLink ID="hlntoc" runat="server" Target="_blank" NavigateUrl="~/Pages/Default.aspx?Pgid=20">Privacy Statement</asp:HyperLink>
                                <li>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/Rewards/Terms.aspx">Terms and Conditions</asp:HyperLink></li>
                            </ul>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">To accept the terms of service click &quot;I accept.&quot; By clicking I accept
                                    you are agreeing to the above Terms and Conditions and Privacy Statement.&nbsp;
                                    If you do not agree with these terms, click &quot;Cancel&quot;<br />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
    
        <tr align="right">
                    <td colspan="3">
                        <asp:Button ID="Voucher" runat="server" BackColor="#499ECA" BorderColor="#499ECA"
                            BorderStyle="Solid" BorderWidth="0px" Font-Names="Verdana"
                            ForeColor="White" Height="28px" Text="Confirm Redeem"
                            OnClick= "Voucher_Click"/>
                        
                        </tbody>
    </table>

    
</div>
<div id="redempted" runat="server" visible="false">Congratulations..!! Your Rewards are Redeemed. Please enter, if you want any other name to be printed on the coupon&nbsp;<asp:textbox ID="othername" runat="server"></asp:textbox><br />
    <div style="text-align:right"> <asp:Button ID="generate" runat="server" BackColor="#499ECA" BorderColor="#499ECA"
                            BorderStyle="Solid" BorderWidth="0px" Font-Names="Verdana"
                            ForeColor="White" Height="28px" Text="Generate Coupons"
                            OnClick= "Generate_Click"/></div>
</div>
<div id="coupons" runat="server" style="width: 100%" visible="false">
    <style>

        .short {
        color:red;
        font-size:80%;
        }
        .small {
        font-size:80%;}
    </style>
  
    <div class="coupondp">
        
<asp:ListView ID="Couponview" runat="server" DataSourceID="ods1" OnItemDataBound="Couponview_ItemDataBound">
    <LayoutTemplate>
            <div id="ItemPlaceHolder" runat="server">
            </div>
        </LayoutTemplate>
        <EmptyDataTemplate>
            <div style="color: Red; margin: 10px 2px; font-size: 15px">No Coupons Generated</div>
        </EmptyDataTemplate>
     <ItemTemplate>
         
         <table style="border:2px dashed; border-color:#86b788; ">
             <tbody>
                  <tr>
                     <td><span class="short">Member Name: </span><span class="small"><asp:label ID="memname" runat="server"></asp:label></span></td>
                       <td><span class ="short">Coupon: </span><span class="small"><asp:label ID="code" Text='<%#Eval("VoucherCode")%>' runat="server"></asp:label></span></td>
                      <td><span class="short">Signature: </span><span class="small"><asp:Label ID ="lblsignature" runat="server"></asp:Label></span></td>
                  </tr>
                 <tr>
                    
                     <td><span class ="short">Issue:   </span><span class="small"><asp:label ID="Issuedate" Text='<%#Bind("Issuedate", "{0:dd-MM-yyyy}")%>' runat="server"></asp:label></span></td>
                     <td><span class ="short">Expires:  </span><span class="small"> <asp:label ID="Expirydate" Text='<%#Bind("ExpiryDate", "{0:dd-MM-yyyy}")%>' runat="server"></asp:label></span></td>
                     <td><span class="short">Points: </span><span class="small"><asp:Label ID ="lblpoints" runat="server"></asp:Label></span></td>
                 </tr>
                 <tr>
                     <td colspan="3"><span class="short">This coupon entitles the Member to the following reward:</span></td>
                 </tr>
                 <tr>
                     <td><Img ID="logo" runat="server" src="../Content/StyleImages/hbc_logo.png" style="width:80px;"/></td>
                     <td style="font-size:150%; text-align:center;" colspan="2">
                     <asp:label ID="lblrewname" Text='<%#Eval("RewardName")%>' runat="server"></asp:label></td>
                 </tr>
                
                 <tr>
                     <td colspan ="1"><span class="short">Reward Provider Name: </span></td>
                     <td colspan ="2"><span class ="small"><asp:Label ID="sponsor" runat="server" Text='<%#Eval("Name")%>'></asp:Label></span></td>
                 </tr>
                 <tr>
                     <td><span class ="short">Provider Address: </span></td>
                     <td><span class="small"><asp:Label ID="Label4" runat="server" Text='<%#Eval("Address")%>'></asp:Label></span></td>
                 </tr>
                 <tr>
                     <td colspan ="1"><span class="short">Issuer: </span></td>
                     <td colspan="2"><span class="small">Healthy Australia Club(Web:www.healthyaustraliaclub.com)</span></td>
                 </tr>
                 <tr>
                 <td colspan="2"><span class="small">Each coupon is single use only</span></td>
                 <td colspan="1"><span class="short">(security feature only)</span></td>
                 </tr>
                
                 <tr>
                     <td colspan="3"><span class="small">Reward Provider Notes: <asp:Label ID="lblprvnotes" runat="server"></asp:Label></span></td>
                     </tr>                 
             </tbody>

         </table>

         </ItemTemplate>

</asp:ListView></div>
    
</div>


<div id ="print" visible="false" runat="server" style="padding-top:5px;">
    <asp:Button ID ="saveall" runat="server" Text="Save/Open in new window" OnClientClick="save();" />
    <asp:Button ID="btnprint" Text ="Print" runat="server" OnClientClick="return PrintPanel();" />
    </div>
<asp:ObjectDataSource ID="ods1" runat="server"></asp:ObjectDataSource>
<asp:HiddenField ID="hdnselected" runat="server" />
<asp:ObjectDataSource ID="ods" runat="server"></asp:ObjectDataSource>