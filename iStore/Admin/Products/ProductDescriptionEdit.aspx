<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductDescriptionEdit.aspx.cs" MasterPageFile="~/Admin/Admin.Master" Inherits="iStore.Admin.Products.ProductDescriptionEdit" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
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
        Редактирование описания продукта <span><b><%= CurrentProduct.Name%></b></span>
    </p>
    <p>
        <asp:TextBox runat="server" ID="txtBody" TextMode="MultiLine" />
    </p>
        <script src="<%= iStore.Site.SiteUrl %>Scripts/fck/fckeditor.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            var sBasePath = '<%= iStore.Site.SiteUrl %>Scripts/FCK/';
            var oFCKeditor = new FCKeditor('<%=txtBody.ClientID %>');
            oFCKeditor.Config.Enabled = true;
            oFCKeditor.Config.UserFilesPath = "'<%= iStore.Site.SiteUrl %>Content/Pages'";
            oFCKeditor.Config.UserFilesAbsolutePath = "'<%= iStore.Site.SiteUrl %>Content/Pages'";

            oFCKeditor.Height = '500';
            oFCKeditor.Width = '716';
            oFCKeditor.BasePath = sBasePath;
            oFCKeditor.ReplaceTextarea();
        }
    </script>
    <asp:Button runat="server" ID="SaveBtn" OnClick="SaveDescription" Text="Save" />

     <p>
        <a href="<%= iStore.Site.SiteAdminUrl %>Products/?cid=<%= Request.QueryString["cid"] %>">Вернуться к списку товаров</a>
    </p>
    <br />
</div>

</asp:Content>