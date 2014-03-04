<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImagesListViewUC.ascx.cs" Inherits="HealthyClub.Providers.Web.UserControls.ImagesListViewUC" %>
<link href="../../Content/gallery/jquery.ad-gallery.css" rel="stylesheet" />
<link rel="stylesheet" href="../../Content/gallery/prettyPhoto.css" type="text/css" media="screen"
    title="prettyPhoto main stylesheet" charset="utf-8" />
<script src="../../Scripts/jquery-1.8.2.js"></script>
<script src="../../Scripts/jquery-ui-1.8.24.js"></script>
<script src="../../Scripts/gallery/jquery.prettyPhoto.js"></script>
<script src="../../Scripts/gallery/jquery.ad-gallery.js"></script>
<script type="text/javascript">
    $(function () {
        var galleries = $('.ad-gallery').adGallery();
        $('#switch-effect').change(
      function () {
          galleries[0].settings.effect = $(this).val();
          return false;
      }
    );
        $('#toggle-slideshow').click(
      function () {
          galleries[0].slideshow.toggle();
          return false;
      }
    );
        $('#toggle-description').click(
      function () {
          if (!galleries[0].settings.description_wrapper) {
              galleries[0].settings.description_wrapper = $('#descriptions');
          } else {
              galleries[0].settings.description_wrapper = false;
          }
          return false;
      }
    );

    });
</script>
<style type="text/css">
   
    select, input, textarea
    {
        font-size: 1em;
    }
    .example
    {
        border: 1px solid #CCC;
        background: #f2f2f2;
        padding: 10px;
    }
    ul
    {
        list-style-image: url(list-style.gif);
    }
    pre
    {
        font-family: "Lucida Console" , "Courier New" , Verdana;
        border: 1px solid #CCC;
        background: #f2f2f2;
        padding: 10px;
    }
    code
    {
        font-family: "Lucida Console" , "Courier New" , Verdana;
        margin: 0;
        padding: 0;
    }
    #gallery
    {
        padding: 5px;
        background: #e1eef5;
    }
    #descriptions
    {
        position: relative;
        height: 50px;
        background: #EEE;
        margin-top: 10px;
        width: 640px;
        padding: 10px;
        overflow: hidden;
    }
    #descriptions .ad-image-description
    {
        position: absolute;
    }
    #descriptions .ad-image-description .ad-description-title
    {
        display: block;
    }
</style>
<asp:HiddenField ID="hdnActivityID" runat="server" />
<div id="ImagesListViewTitle">
</div>
<div id="gallery" class="ad-gallery">
    <asp:Image ID="imgNoImage" Visible="false" runat="server" Style="max-height: 300px;
        max-width: 300px" />
    <div id="theDiv" class="gallery clearfix" runat="server">
        <asp:HyperLink ID="HyperLink1" rel="Fullscreen[Gall]" runat="server">
            <div class="ad-image-wrapper">
                
            </div>
        </asp:HyperLink>
    </div>
    <div class="ad-nav" id="divImagesList" runat="server">
        <div class="ad-thumbs">
            <asp:ListView ID="ListView2" runat="server" OnItemDataBound="ListView2_ItemDataBound">
                <LayoutTemplate>
                    <ul id="ulGal" runat="server" class="ad-thumb-list gallery clearfix">
                        <li id="itemPlaceHolder" runat="server"></li>
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>
                    <asp:UpdatePanel ID="updatePanelThumb" runat="server">
                        <ContentTemplate>
                            <li>
                                <asp:HyperLink ID="hlnkgal2" runat="server">
                                    <asp:Image ID="imgGal2" runat="server" title='<%#Eval("ImageTitle") %>' AlternateText='<%#Eval("Description") %>'
                                    Style="vertical-align: middle; text-align: center; max-height: 50px" /></asp:HyperLink>
                            </li>
                            <asp:HiddenField ID="hdnImageID2" runat="server" Value='<%#Eval("ID") %>' />
                            <asp:HiddenField ID="hdnMainImage2" runat="server" Value='<%#Eval("IsPrimaryImage") %>' />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ItemTemplate>
            </asp:ListView>
            <asp:ListView ID="ListView1" runat="server" OnItemDataBound="ListView1_ItemDataBound">
                <LayoutTemplate>
                    <ul id="ulGal" runat="server" class="ad-thumb-list gallery clearfix">
                        <li id="itemPlaceHolder" runat="server"></li>
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>
                    <asp:UpdatePanel ID="updatePanelThumb" runat="server">
                        <ContentTemplate>
                            <li>
                                <asp:HyperLink ID="hlnkgal1" runat="server" Style="display: none">
                                    <asp:Image ID="imgGal1" runat="server" title='<%#Eval("ImageTitle") %>' AlternateText='<%#Eval("Description") %>' 
                                        Style="vertical-align: middle; text-align: center; max-height: 50px; display: none" /></asp:HyperLink>
                            </li>
                            <asp:HiddenField ID="hdnImageID1" runat="server" Value='<%#Eval("ID") %>' />
                            <asp:HiddenField ID="hdnMainImage1" runat="server" Value='<%#Eval("IsPrimaryImage") %>' />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
</div>
<script type="text/javascript" charset="utf-8">
    $(document).ready(function () {
        $("a[rel^='Fullscreen']").prettyPhoto({ animation_speed: 'normal' });
    });
</script>
