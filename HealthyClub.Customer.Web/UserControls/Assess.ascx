<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Assess.ascx.cs" Inherits="HealthyClub.Customer.Web.UserControls.Assess" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<link rel="stylesheet" href="../Content/rewardlist.css" type="text/css" />

<table>
        <tbody>
            <tr>
                <td class="contentcartmain">


<div class="SecondaryColor">
        <div class="ContentSection">
            <div class="ContentCart">
        My Dashboard            

            </div>
            <div class="ContentSectionBody">                    

                <div class="Reward_details">
                    Current Reward Points:&nbsp;<asp:label ID="RewardPts" runat="server"></asp:label>&nbsp; Points<br />
                  
                    Total Points Redempted:&nbsp;<asp:label ID="Redeempts" runat="server"></asp:label>&nbsp; Points<br />
                    Special Reward Points Earned:&nbsp;<asp:label ID="Bonuspts" runat="server"></asp:label>&nbsp; Points<br />

                </div>

            </div>
</div>
    </div>
    
          
                    </td>

            </tr><tr>
                
                <td class="SideMain" colspan="3">
<div class="SecondaryColor">
        <div class="ContentSection">
            <div class="ContentCart">
        Recent Activities         

            </div>
            <div class="ContentSectionBody" style="padding: 0px;">

                <div id="divproductsListViewContent" runat="server" style="width: 100%; overflow:scroll; height:150px;">
    <asp:ListView ID="ActivityAttView" runat="server" DataSourceID="ods" OnItemDataBound="ActivityAttView_ItemDataBound"
        OnItemCommand="ActivityAttView_ItemCommand" OnPagePropertiesChanging="ActivityAttView_PagePropertiesChanging">
        <LayoutTemplate>
            <table style="width: 100%; border-collapse: collapse;" border="0" cellspacing="0" cellpadding="3" runat="server">
                
                <thead>
                    
                        <tr runat="server">
                               
                            <th scope="col" colspan="1" align="center" runat="server">
                               Activity Name 
                            </th>
                            
                               <th id="Th2" align="right" style="white-space: nowrap;" scope="col" runat="server">
                                
                            </th>
                            <th scope="col" colspan="10" runat="server" align="center">
                                Date/Time
                            </th>
                         <th id="Th1" align="right" style="white-space: nowrap;" scope="col" runat="server">
                                
                            </th>
                            <th scope="col" colspan="6" runat="server" align="right">
                                Points Earned
                            </th>
                        </tr>
                    
                        </thead>
                <tbody>
                        <tr id ="ItemPlaceholder" runat="server">
                            <td runat="server"></td>
                            <td runat="server"></td>
                            <td runat="server"></td>
                            


                        </tr>
                    
            
                        </tbody>
                </table>

        </LayoutTemplate>
        <EmptyDataTemplate>
            <div style="color: black; margin: 10px 2px; font-size: 15px">No Recent Activities</div>
        </EmptyDataTemplate>
        <ItemTemplate>
            <tr>
            <td colspan="1" valign="center" align="center">

                <asp:HyperLink ID="lblActname" style="display:block"
                                runat="server"><%#Eval("Name") %></asp:HyperLink>
            </td>
                 <td valign="top" colspan="1">
                     <asp:HiddenField ID="hdnActivityID" runat="server" Value='<%#Eval("ID") %>' />              
                </td>
            
                <td colspan="10" align="center">
                     <asp:Label ID="lblAttend" runat="server" Text='<%#Bind("CreatedDateTime","{0:dd-MM-yyyy}")%>'></asp:Label>

                </td>
                <td valign="top" colspan="2">
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
                <td colspan="10" align ="center">

                      <asp:label ID="lblPoints" runat="server" Text='<%#Eval("Earnrewards").ToString().Length > 250 ? Eval("Earnrewards").ToString().Substring(0,250) : Eval("Earnrewards").ToString()%>'></asp:label>

                </td>
            </tr>

        </ItemTemplate>

    </asp:ListView>
             
            </div>
</div>
    </div>
    </div>
    

                    </td></tr>
            <tr>
                <td class="graph" colspan ="3">          <div class="SecondaryColor">
     <div class="ContentSection">
            <div class="ContentCart">
        My Reward Point Report            

            </div>
            <div class="ContentSectionBody">                    

                <div class="Reward_details">
                    
                        <asp:label ID="header" runat="server" Text="Choose Year"></asp:label>&nbsp;
                    <asp:DropDownList ID="ddlYears" runat="server" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList>
                    <hr />
                    <cc1:BarChart ID="BarChart1" runat="server" ChartHeight="300" ChartWidth = "674" style="width:676px" ChartType="Column" ChartTitleColor="#0E426C" Visible = "false" CategoryAxisLineColor="#22536e " ValueAxisLineColor="#22536e " BaseLineColor="#3a8bb8 ">
</cc1:BarChart>
                </div>

            </div>
</div>
    
    </div>
    </td>
            </tr>
        </tbody></table>
    <asp:ObjectDataSource ID="ods" runat="server"></asp:ObjectDataSource>