<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductsPhoto.aspx.cs" MasterPageFile="~/Admin/Admin.Master" Inherits="iStore.Admin.Products.ProductsPhoto" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.easing-1.3.pack.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.mousewheel-3.0.6.pack.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.fancybox.pack.js"></script>
<link rel="Stylesheet" type="text/css" href="../../Scripts/jquery.fancybox-2.0/jquery.fancybox.css" media="screen" />
<script type="text/javascript">
    $(document).ready(function () {
        $(".various").fancybox({
            maxWidth: 800,
            maxHeight: 600,
            fitToView: false,
            width: '100%',
            height: '100%',
            autoSize: false,
            closeClick: false,
            openEffect: 'none',
            closeEffect: 'none'
        });
    });
 </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
<div>    
    <iS:ValidateErrors runat="server" ID="ve" />
    <br />
    <p>
        Редактирование фотографии для продукта <span><b><%= CuurentProduct.Name %></b></span>
    </p>
    <p>
        <img src="<%= CurrentProductPreview %>" alt="<%= CuurentProduct.Name %>" />
    </p>
    <p>
         <a class="various" href="#OriginalImage">Original</a>
         <img id="OriginalImage" src="<%= CurrentProductOriginal %>" alt="<%= CuurentProduct.Name %>" style="display: none;" />
    </p>
</div>

</asp:Content>