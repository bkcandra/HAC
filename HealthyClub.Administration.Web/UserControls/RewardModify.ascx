<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RewardModify.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.RewardModify" EnableTheming="true" %>

<style>
    td {
        text-align: left;
        vertical-align: top;
    }
</style>
<script>
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (((charCode > 47) && (charCode < 58)) || (charCode == 8))
            return true;

        return false;
    }

</script>

<div id="showreward" runat="server" style="border:1px solid black; background-color:white;">
    <table class="form">
        <tr>
            <td class="col1">
                <label>
                    Reward Name</label>
            </td>
            <td>
                <asp:Label ID="lblRewName" runat="server"></asp:Label>
                <asp:TextBox ID="txtRewName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: middle;">
                <label>
                    Reward Description</label>
            </td>
            <td>
                <asp:Label ID="lblRewDesc" runat="server" Height="50px" Width="209px" TextMode="MultiLine"></asp:Label>
                <asp:TextBox ID="txtRewDesc" runat="server" Height="50px" Width="209px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <label>
                    Reward Expiry Date</label>
            </td>
            <td>
                <asp:Label ID="lblCalendarFrom" runat="server"></asp:Label>
                <asp:TextBox ID="txtCalendarFrom" runat="server"></asp:TextBox>
                <ajaxToolkit:TextBoxWatermarkExtender ID="txtCalendarFrom_TextBoxWatermarkExtender" runat="server"
                    TargetControlID="txtCalendarFrom" WatermarkText="ExpiryDate" WatermarkCssClass="bodyText2">
                </ajaxToolkit:TextBoxWatermarkExtender>
                <ajaxToolkit:CalendarExtender ID="txtCalendarFrom_CalendarExtender" CssClass="cal_Theme1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtCalendarFrom">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" PromptCharacter=" "
                    MaskType="Date" TargetControlID="txtCalendarFrom">
                </ajaxToolkit:MaskedEditExtender>
            </td>
            <asp:Label ID="lbldate" ForeColor="Red" runat="server" Text="Please enter valid date" Visible="false"></asp:Label>
        </tr>
        <tr>
            <td>
                <label>
                    Reward Type</label>
            </td>
            <td>
                <asp:Label ID="lblRewtype" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlRewType" runat="server" OnSelectedIndexChanged="rtype_SelectedIndexChanged" AutoPostBack="True" CssClass="medium">
                </asp:DropDownList>
            </td>
        </tr>

        <tr id="tr4" runat="server">
            <td>
                <label>
                    Sponsor</label>
            </td>
            <td>
                
                <asp:Label ID="lblSponsor" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlSponsor" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <label>
                    Required Reward Points</label>
            </td>
            <td>
                <asp:Label ID="lblReqRewardPoints" runat="server"></asp:Label>
                <asp:TextBox ID="txtReqRewardPoints" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: middle;">
                <label>
                    Keywords</label>
            </td>
            <td>
                <asp:Label ID="lblKeywords" runat="server"></asp:Label>
                <asp:TextBox ID="txtKeywords" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <label>
                    Usage Times Available</label>
            </td>
            <td>
                <asp:Label ID="lblUsageTimes" runat="server"></asp:Label>
                <asp:TextBox ID="txtUsageTimes" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
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
                        <asp:Label ID="lblrewsource" runat="server"></asp:Label>
                    </td>
                </tr>
        <tr>
            <td style="vertical-align: middle;">
                <label>
                    Reward Image</label>
            </td>
            <td >
                <asp:Label ID="lblImage" runat="server" Text="Do not upload any Image, if you wish to keep the original"></asp:Label><br />
                <asp:FileUpload ID="fileUpRewardImage" runat="server" />
                <asp:Image ID="imgPreview" runat="server" Style="max-width: 150px; max-height: 150px;" />
            </td>
        </tr>
    </table>

    <div id="discount" visible="false" runat="server">
        <table class="form">
            <tr>
                <td class="col1"><label> Discounted Activity Name</label>
                </td>
                <td>
                    <asp:Label ID="lblDiscAct" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlDiscAct" runat="server"></asp:DropDownList>

                    

                </td>
            </tr>
            <tr>
                <td class="col1"><label>Discount Selection</label></td>
                <td >
                    <asp:Label ID="lblDiscSelect" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlDiscSelect" OnSelectedIndexChanged="select_index" runat="server" AutoPostBack="True">
                        <asp:ListItem>Percentage</asp:ListItem>
                        <asp:ListItem>Money Disccount</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="col1">

                    <asp:Label ID="lblTextSelection" runat="server" Text="Label"></asp:Label></td>
                <td >
                    <asp:Label ID="lblDiscPerc" runat="server"></asp:Label>
                    <asp:TextBox ID="txtDiscPerc" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div id="offer" visible="false" runat="server">
        <table class="form">
            <tr>
                <td class="col1"><label>Required Activity Name</label></td>
                <td>
                    <asp:Label ID="lblReqActivityName" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlReqActivityName" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="col1"><label>Free Activity Name</label></td>
                <td>
                    <asp:Label ID="lblFreeAct" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlFreeAct" runat="server"></asp:DropDownList></td>
            </tr>
        </table>
    </div>
    <div id="gift" visible="false" runat="server">
        <table class="form">
            <tr>
                <td class="col1"><label>Free Gift Name</label></td>
                <td>
                    <asp:Label ID="lblGiftName" runat="server"></asp:Label>
                    <asp:TextBox ID="txtGiftName" runat="server"></asp:TextBox>

                </td>


            </tr>
        </table>
    </div>

    <div id="other" visible="false" runat="server">
        <table class="form">
            <tr>
                <td class="col1"><label>Bonus Point</label></td>
                <td>
                    <asp:Label ID="lblBonusPoint" runat="server"></asp:Label>
                    <asp:TextBox ID="txtBonusPoint" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="col1"><label>Bonus Activity Name</label></td>
                <td>

                    <asp:Label ID="lblBonusAct" runat="server">
                    </asp:Label>

                    <asp:DropDownList ID="ddlBonusAct" runat="server">
                    </asp:DropDownList></td>
            </tr>
            
        </table>


    </div>

    <br />
    <table class="form">
<tr><td>
    <asp:Button CssClass="btn btn-grey" ID="lnkUpdate" runat="server" OnClick="lnkUpdate_Click" Text="Update"></asp:Button>


    &nbsp;&nbsp;
    <asp:Button CssClass="btn btn-grey" ID="lnkEdit" runat="server" OnClick="lnkEdit_Click" Text="Edit"></asp:Button>
    &nbsp;&nbsp;
    <asp:Button CssClass="btn btn-grey" ID="lnkback" runat="server" OnClick="Back_Click" Text="<< Back to Rewards"></asp:Button>

    </td></tr>
    </table>


<br />





<div id="addeddiv" visible="false" runat="server">
    <asp:Label ID="rewardadded" Text="Reward is Updated" ForeColor="red" runat="server"></asp:Label>
</div>
</div>


<asp:HiddenField ID="hdnFormMode" runat="server" />
<asp:HiddenField ID="hdnRewardID" runat="server" />
<asp:HiddenField ID="EditMode" runat="server" />
<asp:HiddenField ID="ImageAvail" runat="server" />
