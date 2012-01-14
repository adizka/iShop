<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductsPhoto.aspx.cs" MasterPageFile="~/Admin/Admin.Master" Inherits="iStore.Admin.Products.ProductsPhoto" Title="Products photo | Marvel Worldwide" %>


<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.easing-1.3.pack.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.mousewheel-3.0.6.pack.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.fancybox.pack.js"></script>
<link rel="Stylesheet" type="text/css" href="../../Scripts/jquery.fancybox-2.0/jquery.fancybox.css" media="screen" />
<script type="text/javascript">
    $(document).ready(function () {
        $(".various").fancybox({
            fitToView: false,
            width: '90%',
            height: '90%',
            autoSize: true,
            closeClick: false,
            openEffect: 'none',
            closeEffect: 'none'
        });
    });
 </script>
 <style type="text/css">
    .fancybox-inner { text-align:center; vertical-align:middle; display:table-cell; } 
 </style>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
    <p class="BCCategories">
        <a href="<%= iStore.Site.SiteAdminUrl %>Products/?cid=<%= Request.QueryString["cid"] %>">Back to product list</a>
    </p>
    <iS:ValidateErrors runat="server" ID="ve" />
<div class="copy_contacter">
    <p class="prod_h6">
        Edit product photo <span><b><%= CuurentProduct.Name %></b></span>
    </p>

    <p>
        Change Preview
    </p>
    <p>
        <asp:FileUpload size="49" runat="server" ID="upPreview" />
        <span class="universal_button">
            <span>
                <asp:LinkButton runat="server" ID="btnSavePreview" OnClick="SavePreview" Text="Upload" />
            </span>
        </span>
        &nbsp;
        <span class="universal_button">
            <span>
                <a class="various" href="#PreviewImage">View</a>
            </span>
        </span>
        <img id="PreviewImage" src="<%= CurrentProductPreview %>" alt="<%= CuurentProduct.Name %>" style="display: none;" />
    </p>
    <p>
        Change Original Photo 
    </p>
    <p>
        <asp:FileUpload size="49" runat="server" ID="upOriginal" />
        <span class="universal_button">
            <span>
                <asp:LinkButton runat="server" ID="btnSaveOriginal" OnClick="SaveOriginal" Text="Upload" />
            </span>
        </span>
        &nbsp;
        <span class="universal_button">
            <span>
                <a class="various" href="#OriginalImage">View</a>
            </span>
        </span>
        <img id="OriginalImage" src="<%= CurrentProductOriginal %>" alt="<%= CuurentProduct.Name %>" style="display: none; max-width:800px;" />
        <br />
        <asp:FileUpload size="49" runat="server" ID="upOriginal2" />
        <span class="universal_button">
            <span>
                <asp:LinkButton runat="server" ID="btnSaveOriginal2" OnClick="SaveOriginal2" Text="Upload" />
            </span>
        </span>
        &nbsp;
        <span class="universal_button">
            <span>
                <a class="various" href="#OriginalImage2">View</a>
            </span>
        </span>
        <img id="OriginalImage2" src="<%= CurrentProductOriginal2 %>" alt="<%= CuurentProduct.Name %>" style="display: none; max-width:800px;" />
        <br />
        <asp:FileUpload size="49" runat="server" ID="upOriginal3" />
        <span class="universal_button">
            <span>
                <asp:LinkButton runat="server" ID="btnSaveOriginal3" OnClick="SaveOriginal3" Text="Upload" />
            </span>
        </span>
        &nbsp;
        <span class="universal_button">
            <span>
                <a class="various" href="#OriginalImage3">View</a>
            </span>
        </span>
        <img id="OriginalImage3" src="<%= CurrentProductOriginal3 %>" alt="<%= CuurentProduct.Name %>" style="display: none; max-width:800px;" />
    </p>

</div>
    <p class="BCCategories">
        <a href="<%= iStore.Site.SiteAdminUrl %>Products/?cid=<%= Request.QueryString["cid"] %>">Back to product list</a>
    </p>

</asp:Content>