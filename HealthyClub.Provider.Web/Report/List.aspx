<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="HealthyClub.Providers.Web.Report.Report" %>

<%@ Register Src="../UserControls/ReportViewer.ascx" TagName="ReportViewer" TagPrefix="uc1" %>

<%@ Register src="../UserControls/ScheduleViewerUC.ascx" tagname="ScheduleViewerUC" tagprefix="uc3" %>
<style type="text/css">
    .infoLabel
    {
        font-size: 11px;
        border-style: none;
        text-align: left;
    }
</style>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divMain" runat="server">
        <div style="width: 100%">
            <div id="divTitle" runat="server">
            </div>
            <br />
            <div id="divBody" runat="server" style="font-family: 'comic Sans MS'; font-size: 12px">
                
                
                
                <asp:ListView ID="ViewerListview" runat="server" GroupItemCount="2" OnItemDataBound="ViewerListview_ItemDataBound">
                    <LayoutTemplate>
                        <table width="100%" id="asd" runat="server">
                            <tr>
                                <td>
                                    <table border="0" cellpadding="5" width="100%">
                                        <asp:PlaceHolder runat="server" ID="groupPlaceHolder"></asp:PlaceHolder>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                        </tr>
                    </GroupTemplate>
                    <GroupSeparatorTemplate>
                        <tr id="Tr1" runat="server">
                            <td colspan="3">
                            </td>
                        </tr>
                    </GroupSeparatorTemplate>
                    <ItemTemplate>
                        <td id="tdGroupWidth" runat="server" style="vertical-align: top; width: 50%;" class="infoLabel">
                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' Font-Bold="True">
                            </asp:Label><br />
                                            <asp:Label ID="lblShortDescription" CssClass="infoLabel" runat="server" Text='<%#Eval("FullDescription") %>'></asp:Label>
                            <div id="divEligibility" runat="server">
                                <asp:Label ID="lblEligibility" runat="server" CssClass="infoLabel" Text='<%#Eval("eligibilityDescription") %>'></asp:Label>
                            </div>
                            <uc3:ScheduleViewerUC ID="ScheduleViewerUC1" runat="server" />
                            <a class="infoLabel" style="font-weight: bold">Contact Details :</a><br />
                            <asp:Label ID="LblAddress" runat="server" CssClass="infoLabel" Text='<%#Eval("Address") %>'></asp:Label><br />
                            <asp:Label ID="lblSuburb" runat="server" CssClass="infoLabel" Text='<%#Eval("Suburb") %>'></asp:Label>
                            <asp:Label ID="lblState" runat="server" CssClass="infoLabel" Text='<%#Eval("StateName") %>'></asp:Label>&nbsp;
                            <asp:Label ID="lblPostcode" runat="server" CssClass="infoLabel" Text='<%#Eval("PostCode") %>'></asp:Label><br />
                            <a class="infoLabel" style="font-weight: bold">Phone :</a>
                            <asp:Label ID="lblPhone" runat="server" CssClass="infoLabel" Text='<%#Eval("PhoneNumber") %>'></asp:Label>,
                            <asp:Label ID="lblWebsite" runat="server" CssClass="infoLabel" Text='<%#Eval("Website") %>'></asp:Label>
                            <asp:HiddenField ID="hdnActivityID" runat="server" Value='<%#Eval("ActivityID") %>' />
                        </td>
                    </ItemTemplate>
                    <ItemSeparatorTemplate>
                        <td id="Td1" class="separator" runat="server">
                            &nbsp;&nbsp;
                        </td>
                    </ItemSeparatorTemplate>
                </asp:ListView>
            </div>
            <asp:ObjectDataSource ID="ods" runat="server"></asp:ObjectDataSource>
        </div>
        <asp:HiddenField ID="hdnTitle" runat="server" />
        <asp:HiddenField ID="hdnName" runat="server" />
        <asp:HiddenField ID="hdnShortDescription" runat="server" />
        <asp:HiddenField ID="hdnCategoryID" runat="server" />
        <asp:HiddenField ID="hdnAgeTo" runat="server" />
        <asp:HiddenField ID="hdnAgeFrom" runat="server" />
        <asp:HiddenField ID="hdnEligibility" runat="server" />
        <asp:HiddenField ID="hdnSearchKey" runat="server" />
        <asp:HiddenField ID="hdnProviderID" runat="server" />
        <asp:HiddenField ID="hdnAddress" runat="server" />
        <asp:HiddenField ID="hdnWebsite" runat="server" />
        <asp:HiddenField ID="hdnPrice" runat="server" />
        <asp:HiddenField ID="hdnColumn" runat="server" />
        <asp:HiddenField ID="hdnSortValue" runat="server" />
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="hdnPostCode" runat="server" />
        <asp:HiddenField ID="hdnUseTimetable" runat="server" />
    </div>
    </form>
</body>
</html>
