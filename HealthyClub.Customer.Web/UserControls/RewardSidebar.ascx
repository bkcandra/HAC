<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RewardSidebar.ascx.cs" Inherits="HealthyClub.Web.UserControls.RewardSidebar" %>



<style>
    .multiSelectTextbox {
        background-color: #fff;
        color: #1B274F;
        font-weight: bold;
        padding: 3px;
        border: 1px solid #b7b7b7;
        width: 150px;
    }

    .multiSelectButton {
        display: block;
        background-image: url(../Content/StyleImages/multiselectarrow2.png);
        background-position: bottom;
        background-repeat: no-repeat;
        height: 24px;
        width: 21px;
    }

    .multiSelectTooltip {
        position: absolute;
        display: block;
        padding: 2px 12px 3px 7px;
        margin-left: 5px;
        background: #fff;
        color: #1B274F;
        border: 1px solid #b7b7b7;
        font-weight: bold;
    }

    .multiSelectDropdown {
        border: 1px solid #b7b7b7;
        min-width: 150px;
    }

        .multiSelectDropdown td {
            border: 1px dotted #bbbbbb;
            background-color: #fff;
            padding-left: 2px;
        }

    .dropdown {
        width: 280px;
        display: inline-block;
        margin-right: 2px;
    }

    .textbox {
        width: 200px;
        display: inline-block;
        margin-right: 5px;
    }

    .dropdown .textbox {
        vertical-align: bottom;
        padding: 2px;
        border: 1px solid #b7b7b7;
    }
    /*AJAX Calendar
    */
    .cal_Theme1 .ajax__calendar_container {
        background-color: #e2e2e2;
        border: solid 1px #cccccc;
    }

    .cal_Theme1 .ajax__calendar_header {
        background-color: #ffffff;
        margin-bottom: 4px;
    }

    .cal_Theme1 .ajax__calendar_title,
    .cal_Theme1 .ajax__calendar_next,
    .cal_Theme1 .ajax__calendar_prev {
        color: #004080;
        padding-top: 3px;
    }

    .cal_Theme1 .ajax__calendar_body {
        background-color: #e9e9e9;
        border: solid 1px #cccccc;
        z-index: 1003;
    }

    .cal_Theme1 .ajax__calendar_dayname {
        text-align: center;
        font-weight: bold;
        margin-bottom: 4px;
        margin-top: 2px;
    }

    .cal_Theme1 .ajax__calendar_day {
        text-align: center;
    }

    .cal_Theme1 .ajax__calendar_hover .ajax__calendar_day,
    .cal_Theme1 .ajax__calendar_hover .ajax__calendar_month,
    .cal_Theme1 .ajax__calendar_hover .ajax__calendar_year,
    .cal_Theme1 .ajax__calendar_active {
        color: #004080;
        font-weight: bold;
        background-color: #ffffff;
    }

    .cal_Theme1 .ajax__calendar_container {
        z-index: 1000;
    }

    .cal_Theme1 .ajax__calendar_today {
        font-weight: bold;
    }

    .cal_Theme1 .ajax__calendar_other,
    .cal_Theme1 .ajax__calendar_hover .ajax__calendar_today,
    .cal_Theme1 .ajax__calendar_hover .ajax__calendar_title {
        color: #bbbbbb;
    }

    .loginHighlight {
        border: 1px solid black;
        background-image: url("gr.jpg" );
        color: #B24F04;
        font-family: serif;
        width: 80%;
    }

    .loginNormal {
        border: 2px solid #FF8326;
        background-image: url("gr.jpg" );
        width: 80%;
        color: #B24F04;
        font-family: serif;
        opacity: 0.6;
        filter: alpha(opacity=60);
    }

    .loginText {
        color: #FF8326;
    }

    .style2 {
        width: 55px;
    }
</style>

<script src="../Scripts/ui.dropdownchecklist-1.4-min.js"></script>
<script language="javascript">
    function isNumberKey(evt) 
        {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (((charCode > 47) && (charCode < 58 ) ) || (charCode == 8))
                return true;

            return false;            
        }

 </script>
<div style="height: auto">
    <div class="sidebar_box">
        <div class="sb_title">
            Categories
        </div>
        <div class="sb_content">
            <div style="max-width: 210px; overflow: hidden; height: auto; width: 196px;">

            Search<br />
             <asp:DropDownList ID="categories" runat="server" Width="170px" 
                  OnSelectedIndexChanged="categories_SelectedIndexChanged" AutoPostBack="true">
               <asp:ListItem Text="All Categories" Value="0" />
         
            </asp:DropDownList>            
            </div>
        </div>
        <div class="sb_bottom">
        </div>
    </div>

    
    
         

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div class="sidebar_box">
         <div class="sb_title">
           Reward Type
        </div>
        
        <div class="sb_content">
            <table id="Table1" runat="server" style="width: 200px;">
                <tr>
                                                        
                            <td class="style2">
                                <asp:CheckBox ID="chkinternal" runat="server" Text=" Internal/Activity Based" AutoPostBack="true"  />
                            </td></tr><tr>
                            <td class="style2">
                                <asp:CheckBox ID="chkexternal" runat="server" Text=" External/Products Based" AutoPostBack="true"  />
                            </td>
                    </tr>
             
                </table>
            </div>
        <div class="sb_bottom">
        </div>
            </div>
                
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="sidebar_box">
        <div class="sb_title">
            Reward Points
        </div>
        <div class="sb_content" style="height:auto">
            <table style="width: 100%; padding-top: 10px">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="From"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAgeFrom" runat="server" Width="80px" CssClass="bodyText1 textbox" onkeypress="return isNumberKey(event)"></asp:TextBox>
                     
                        points
                    
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="To"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAgeTo" runat="server" Width="80px" CssClass="bodyText1 textbox"></asp:TextBox>
                        
                        
                        points
                 
                    </td>
                </tr>
                <tr>
                    <td>&nbsp&nbsp&nbsp</td><td>
                        <span style="float:right"><asp:LinkButton ID="lnkResetAge" runat="server" OnClick="lnkResetAge_Click" Style="float: right" Visible="false">Reset</asp:LinkButton></span>
                    </td>

                </tr>
            </table>
            
            
        </div>

        <div class="sb_bottom">
        </div>
    
    </div>
    <div style="float: right;">
        <asp:Button ID="Apply" runat="server" Text="Apply" OnClick="Apply_Click" Width="90px"
            CssClass="button button-submit" />
        
    </div>
    <asp:HiddenField ID="hdnCategoryID" runat="server" />
    <asp:HiddenField ID="hdnProvider" runat="server" />
    <asp:HiddenField ID="hdnErrorText" runat="server" />
    <asp:HiddenField ID="hdnTagID" runat="server" />
    <asp:HiddenField ID="hdnFilterError" runat="server" />
    <asp:HiddenField ID="hdnShowLevel" runat="server" />
    <asp:HiddenField ID="hdnDTFrom" runat="server" />
    <asp:HiddenField ID="hdnDTTo" runat="server" />
    <asp:HiddenField ID="hdnSectionID" runat="server" />
    <asp:HiddenField ID="hdnRewardType" runat="server" />
    <asp:HiddenField ID="hdnAgeFrom" runat="server" />
    <asp:HiddenField ID="hdnAgeTo" runat="server" />
    <asp:HiddenField ID="hdnSectionItemNo" runat="server" />
    <asp:HiddenField ID="hdnShowListingAllCategory" runat="server" />
    <asp:HiddenField ID="hdnShowListingTitle" runat="server" />
    <asp:HiddenField ID="hdnShowTitle" runat="server" />
    <asp:HiddenField ID="hdnGroupingType" runat="server" />
    <asp:HiddenField ID="hdnStartingRef" runat="server" />
    <asp:HiddenField ID="hdnFiltered" runat="server" />
</div>
