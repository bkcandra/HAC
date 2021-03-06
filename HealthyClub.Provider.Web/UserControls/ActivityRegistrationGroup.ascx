﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityRegistrationGroup.ascx.cs"
    Inherits="HealthyClub.Providers.Web.UserControls.ActivityRegistrationGroup" %>
<script>
    function CheckNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
</script>

<style type="text/css">
    .style10 {
        width: 550px;
        text-decoration: none;
    }

    .style1 {
        width: 280px;
        vertical-align: top;
        text-align: justify;
    }
</style>
<span>When you select who your activity is suitable for we can promote it to specific
    groups.
    <br />
    <br />
    Entering keywords related to your activity means it will be listed in search results
    when a Club Member/Guest types in that keyword. For example, if your activity is under
    18's football and you enter in the keywords footy; youth; sport; AFL, your activity
    will appear in all search results where one of these words is typed into the search
    bar.</span>
<hr />
<asp:UpdatePanel ID="UpdatePanelGroup" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div style="width: 100%">
            <br />
            <table>
                <tr>
                    <td>This activity is suitable for&nbsp;(<span style="color: Red;">*</span>)
                    </td>
                    <td class="style10">
                        <asp:CheckBox ID="chkMale" runat="server" Text="Males" />
                        &nbsp;&nbsp;
                        <asp:CheckBox ID="chkFemale" runat="server" Text="Females" />&nbsp;&nbsp;
                        <asp:CheckBox ID="chkChild" runat="server" Text="Children" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td class="style10">
                        <asp:CheckBox ID="chkAgeAll" runat="server" Text="Suitable for all ages" OnCheckedChanged="CheckBox3_CheckedChanged"
                            Checked="false" AutoPostBack="true" />
                    </td>
                </tr>
                <tr id="tabRowAge" runat="server" visible="true">
                    <td>&nbsp;
                    </td>
                    <td class="style10">from
                        <asp:TextBox ID="txtOldFrom" runat="server" Width="100px" MaxLength="3" onkeypress="return CheckNumber(event)"></asp:TextBox>


                        &nbsp;&nbsp;&nbsp; to
                        <asp:TextBox ID="txtOldTo" runat="server" Width="100px" MaxLength="3" onkeypress="return CheckNumber(event)"></asp:TextBox>

                        &nbsp;year olds
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top">Keywords<br />
                        <span class="bodyText2">Please enter key words associated with your activity separated by a semicolon ( ; )</span>
                    </td>
                    <td class="style10">
                        <asp:TextBox ID="txtKeyword" runat="server" TextMode="MultiLine" Height="100px" Width="500px"></asp:TextBox>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtKeyword_TextBoxWatermarkExtender" WatermarkCssClass="bodyText2"
                            runat="server" TargetControlID="txtKeyword" WatermarkText="E.g. tennis; sport; healthy living; exercise">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hdnEdit" runat="server" />
        <asp:HiddenField ID="hdnActivityID" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
