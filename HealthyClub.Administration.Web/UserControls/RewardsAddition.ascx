<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RewardsAddition.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.RewardsAddition" %>

<style type="text/css">
    .auto-style1 {
        width: 220px;
    }

    .auto-style2 {
        width: 221px;
    }

    .medium {
    }

    .auto-style3 {
        width: 222px;
    }

</style>
<script language="javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (((charCode > 47) && (charCode < 58)) || (charCode == 8))
            return true;

        return false;
    }

 </script>
        <div id="addreward" style="font-weight: bold;">
            <table class="form">
                <tr>
                    <td class="col1">
                        <label>
                            Reward Name</label>
                    </td>
                    <td>
                        <asp:TextBox ID="rname" runat="server" CssClass="medium"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td style ="vertical-align:middle;">
                        <label>
                            Reward Description</label>
                    </td>
                    <td>
                        <asp:TextBox ID="rdesc" runat="server" Height="50px" Width="209px" TextMode="MultiLine" CssClass="medium"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Reward Expiry Date</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCalendarFrom" runat="server" Width="80px"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtCalendarFrom_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtCalendarFrom" WatermarkText="ExpiryDate" WatermarkCssClass="bodyText2">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <ajaxToolkit:CalendarExtender ID="txtCalendarFrom_CalendarExtender" CssClass="cal_Theme1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtCalendarFrom">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" PromptCharacter=" "
                            MaskType="Date" TargetControlID="txtCalendarFrom">
                        </ajaxToolkit:MaskedEditExtender>
                    </td><asp:Label ID="lbldate" ForeColor="Red" runat="server" Text="Please enter valid date" Visible="false"></asp:Label>
                </tr>
                <tr>
                    <td>
                        <label>
                            Reward Type</label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRewType" runat="server" OnSelectedIndexChanged="ddlRewType_SelectedIndexChanged" AutoPostBack="True" CssClass="medium">
                            <asp:ListItem>Select</asp:ListItem>
                        </asp:DropDownList>
                        
                    </td>
                </tr>
                <tr id="tr4" runat="server">
                    <td>
                        <label>
                            Sponsor</label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlsponsors" runat="server"></asp:DropDownList>
                    </td>
                </tr>


                <tr>
                    <td>
                        <label>
                            Required Reward Points</label>
                    </td>
                    <td>
                        <asp:TextBox ID="point" runat="server" CssClass="medium" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style ="vertical-align:middle;">
                        <label>
                            Keywords(Enter with semi-colon)</label>
                    </td>
                    <td>
                        <asp:TextBox ID="Keyword" runat="server" Height="50px" Width="212px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Usage Times Available</label>
                    </td>
                    <td>
                        <asp:TextBox ID="usage" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Reward Source</label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlrewsource" runat="server">
                            <asp:ListItem>Internal/Activity Based</asp:ListItem>
                            <asp:ListItem>External/Other Products Based</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Reward Image</label>
                    </td>
                    <td style="margin-left: 80px">
                        <asp:FileUpload ID="fileUpRewardImage" runat="server" />
                    </td>
                </tr>
            </table>
            
            <div id="discount" visible="false" runat="server">
               
                <table>
                    <tr>
                        <td style="margin-left: 80px" class="auto-style1">Discounted Activity Name
                        </td>
                        <td style="margin-left: 80px">
                            <asp:DropDownList ID="ddlDiscAct" runat="server"></asp:DropDownList>

                        </td>
                    </tr>
                    <tr>
                        <td style="margin-left: 80px" class="auto-style1">Discount Selection</td>
                        <td style="margin-left: 80px">
                            <asp:DropDownList ID="select" OnSelectedIndexChanged="select_index" runat="server" AutoPostBack="True">
                                <asp:ListItem>Percentage</asp:ListItem>
                                <asp:ListItem>Price Off</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="margin-left: 80px" class="auto-style1">
                            <asp:Label ID="selection" runat="server"></asp:Label>
                        <td style="margin-left: 80px">
                            <asp:TextBox ID="disperc" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="offer" visible="false" runat="server">
                <table>
                    <tr>
                        <td style="margin-left: 80px" class="auto-style3">Required Activity Name</td>
                        <td style="margin-left: 80px">
                            <asp:DropDownList ID="ddlReqActivityName" runat="server" Style="margin-left: 0px"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="margin-left: 80px" class="auto-style3">Free Activity Name</td>
                        <td style="margin-left: 80px">
                            <asp:DropDownList ID="ddlFreeAct" runat="server" Style="margin-left: 0px"></asp:DropDownList></td>
                    </tr>
                </table>
            </div>
            <div id="gift" visible="false" runat="server">
                <table>
                    <tr>
                        <td style="margin-left: 80px" class="auto-style2">Free Gift Name</td>
                        <td style="margin-left: 80px">
                            <asp:TextBox ID="giftname" runat="server"></asp:TextBox>

                        </td>


                    </tr>
                </table>
            </div>

            <div id="other" visible="false" runat="server">
                <table>
                    <tr>
                        <td style="margin-left: 80px" class="auto-style1">Bonus Point</td>
                        <td>
                            <asp:TextBox ID="Bonuspoint" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox></td></tr>
                    <tr>
                        <td style="margin-left: 80px" class="auto-style1">Bonus Activity Name</td><td>
                        
                            <asp:DropDownList ID="ddlBonusAct" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                </table>
                     

            </div>

            <br />
            <asp:Button CssClass="btn btn-grey" ID="lnkAdd" runat="server" OnClick="lnkAdd_Click" Text="Add"></asp:Button>
               

        </div>


<div id="addeddiv" visible="false" runat="server">
    <asp:Label ID="rewardadded" Text="Reward is Added" ForeColor="red" runat="server"></asp:Label>
</div>
