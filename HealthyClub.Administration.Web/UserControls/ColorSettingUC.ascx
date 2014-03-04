<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ColorSettingUC.ascx.cs" Inherits="HealthyClub.Administration.Web.UserControls.ColorSettingUC" %>
<style type="text/css">
    .style1 {
        width: 180px;
    }
</style>

<table width="100%">
    <tr>
        <td style="width: 500px">
            <asp:Label ID="lblTitle0" Text="Page Color" Font-Bold="True" runat="server" CssClass="PageTitle"></asp:Label>
            <br />
            <table style="width: 450px">
                <tr>
                    <td class="style1">Header status color
                    </td>
                    <td class="style8">:
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <div id="divSampleColor1" runat="server" style="border: thin ridge #000000; width: 15px; height: 15px">
                                    </div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHeaderStatusColor" runat="server" ForeColor="Black"></asp:TextBox>
                                    <ajaxToolkit:ColorPickerExtender ID="ColorPickerExtender1" runat="server" TargetControlID="txtHeaderStatusColor"
                                        PopupButtonID="lnkPopupColor1" SampleControlID="divSampleColor1" PopupPosition="Center">
                                    </ajaxToolkit:ColorPickerExtender>
                                    <asp:LinkButton ID="lnkPopupColor1" runat="server" Text="Change Color" OnClick="lnkPopupColor1_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style1">Header background color
                    </td>
                    <td class="style8">:
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <div id="divSampleColor2" runat="server" style="border: thin ridge #000000; width: 15px; height: 15px">
                                    </div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtheaderBackgroundColor" runat="server" ForeColor="Black"></asp:TextBox>
                                    <ajaxToolkit:ColorPickerExtender ID="ColorPickerExtender2" runat="server" TargetControlID="txtheaderBackgroundColor"
                                        PopupButtonID="lnkPopupColor2" SampleControlID="divSampleColor2" PopupPosition="Center">
                                    </ajaxToolkit:ColorPickerExtender>
                                    <asp:LinkButton ID="lnkPopupColor2" runat="server" Text="Change Color" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style1">Body outer color
                    </td>
                    <td class="style8">:
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <div id="divInnerColor" runat="server" style="border: thin ridge #000000; width: 15px; height: 15px">
                                    </div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBodyOuterColor" runat="server" ForeColor="Black"></asp:TextBox>
                                    <ajaxToolkit:ColorPickerExtender ID="ColorPickerExtender4" runat="server" TargetControlID="txtBodyOuterColor"
                                        PopupButtonID="lnkInnerColor" SampleControlID="divInnerColor" PopupPosition="Center">
                                    </ajaxToolkit:ColorPickerExtender>
                                    <asp:LinkButton ID="lnkInnerColor" runat="server" Text="Change Color" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style1">Footer color&nbsp;
                    </td>
                    <td class="style8">:
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <div id="divSampleColor3" runat="server" style="border: thin ridge #000000; width: 15px; height: 15px">
                                    </div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFooter" runat="server" ForeColor="Black"></asp:TextBox>
                                    <ajaxToolkit:ColorPickerExtender ID="ColorPickerExtender3" runat="server" TargetControlID="txtFooter"
                                        PopupButtonID="lnkPopupColor3" SampleControlID="divSampleColor3" PopupPosition="Center">
                                    </ajaxToolkit:ColorPickerExtender>
                                    <asp:LinkButton ID="lnkPopupColor3" runat="server" Text="Change Color" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Label ID="lblTitle1" Text="Menu Color" Font-Bold="True" runat="server" CssClass="PageTitle"></asp:Label>
            <br />
            <table style="width: 450px">
                <tr>
                    <td class="style1">Table heading color&nbsp;
                    </td>
                    <td class="style8">:
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <div id="divInnerColor0" runat="server" style="border: thin ridge #000000; width: 15px; height: 15px">
                                    </div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTableHeading" runat="server" ForeColor="Black"></asp:TextBox>
                                    <ajaxToolkit:ColorPickerExtender ID="txtMenuBackgroundColor_ColorPickerExtender" runat="server"
                                        TargetControlID="txtTableHeading" PopupButtonID="lnkInnerColor0" SampleControlID="divInnerColor0"
                                        PopupPosition="Center">
                                    </ajaxToolkit:ColorPickerExtender>
                                    <asp:LinkButton ID="lnkInnerColor0" runat="server" Text="Change Color" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style1">Search bar background color
                    </td>
                    <td class="style8">:
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <div id="divSampleColor6" runat="server" style="border: thin ridge #000000; width: 15px; height: 15px">
                                    </div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSearchBar" runat="server" ForeColor="Black"></asp:TextBox>
                                    <ajaxToolkit:ColorPickerExtender ID="txtMenuForeColor_ColorPickerExtender" runat="server"
                                        TargetControlID="txtSearchBar" PopupButtonID="lnkPopupColor6" SampleControlID="divSampleColor6"
                                        PopupPosition="Center">
                                    </ajaxToolkit:ColorPickerExtender>
                                    <asp:LinkButton ID="lnkPopupColor6" runat="server" Text="Change Color" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style1">Activity detail tab color&nbsp;&nbsp;&nbsp;
                    </td>
                    <td class="style8">:
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <div id="divSampleColor7" runat="server" style="border: thin ridge #000000; width: 15px; height: 15px">
                                    </div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtActivityTab" runat="server" ForeColor="Black"></asp:TextBox>
                                    <ajaxToolkit:ColorPickerExtender ID="txtMenuSelectedColor_ColorPickerExtender" runat="server"
                                        TargetControlID="txtActivityTab" PopupButtonID="lnkPopupColor7" SampleControlID="divSampleColor7"
                                        PopupPosition="Center">
                                    </ajaxToolkit:ColorPickerExtender>
                                    <asp:LinkButton ID="lnkPopupColor7" runat="server" Text="Change Color" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style1">&nbsp;
                    </td>
                    <td class="style8">&nbsp;
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkSaveMenuColor" runat="server" OnClick="lnkSaveMenuColor_Click">Save</asp:LinkButton>
                        &nbsp;
                        <asp:Label ID="lblSaved1" runat="server" Visible="False" ForeColor="Silver"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top">
            <asp:Label ID="lblImageTitle" Text="Page Color" Font-Bold="True" runat="server" CssClass="PageTitle"></asp:Label><br />
            <asp:Image ID="Image" runat="server" Style="margin: 10px 10px; max-height: 980px; max-width: 680px"></asp:Image>
        </td>
    </tr>
</table>


