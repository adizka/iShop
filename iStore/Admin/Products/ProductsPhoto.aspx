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
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
<div>    
    <p>
        <a href="<%= iStore.Site.SiteAdminUrl %>Products/?cid=<%= Request.QueryString["cid"] %>">Вернуться к списку товаров</a>
    </p>
    <br />
    <iS:ValidateErrors runat="server" ID="ve" />
    <br />
    <p>
        Редактирование фотографии для продукта <span><b><%= CuurentProduct.Name %></b></span>
    </p>
    <p>
        <img src="<%= CurrentProductPreview %>" alt="<%= CuurentProduct.Name %>" style="max-height: 200px; max-width:200px;" />
    </p>
    <p>
         <a class="various" href="#OriginalImage">Просмотреть полный размер</a>
         <img id="OriginalImage" src="<%= CurrentProductOriginal %>" alt="<%= CuurentProduct.Name %>" style="display: none;" />
    </p>
    <p>
        Изменить изображение            
    </p>
    <br />
    <p>
        <asp:Label runat="server" id="lblPreview" AssociatedControlID="upPreview">Изменение Preview</asp:Label>
        <asp:FileUpload runat="server" ID="upPreview" /><br />
        <asp:Button runat="server" ID="btnSavePreview" OnClick="SavePreview" Text="Загрузить Preview" />
    </p>
    <p>
        <img src="<%= iStore.Site.SiteUrl %>Images/hint.png" alt="Подсказка"   />
    </p>
    <br /><br />
    <p>
        <b>Всего загружено <%=OriginalCount %> photo</b><br />
        <asp:Label runat="server" ID="lblOriginal">Изменение Original</asp:Label>
        <asp:FileUpload runat="server" ID="upOriginal" /><br />
        <asp:Button runat="server" ID="btnSaveOriginal" OnClick="SaveOriginal" Text="Загрузить Original" />
    </p>
    <p> 
        <img src="<%= iStore.Site.SiteUrl %>Images/hint.png" alt="Подсказка" />    
    </p>
    <br />
     <p>
        <a href="<%= iStore.Site.SiteAdminUrl %>Products/?cid=<%= Request.QueryString["cid"] %>">Вернуться к списку товаров</a>
    </p>
    <br />
</div>

</asp:Content>