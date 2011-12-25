<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PropertyEdit.aspx.cs" Inherits="iStore.Admin.ShopProperty.PropertyEdit" MasterPageFile="~/Admin/Admin.Master" %>


<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="main">
<div>    
    <p>
        Редактирование свойств магазина
    </p>
        <p>
        <div runat="server" id="ErrorMsg" visible="false">
        Данное поле необходимо заполнить!!!
        </div>
        <br />
        Ключ
        <asp:TextBox runat="server" ID="keyTxt" />
    </p>
    <p>
        <asp:TextBox runat="server" ID="valTxt" TextMode="MultiLine" />
    </p>
        <script src="<%= iStore.Site.SiteUrl %>Scripts/fck/fckeditor.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            var sBasePath = '<%= iStore.Site.SiteUrl %>Scripts/FCK/';
            var oFCKeditor = new FCKeditor('<%=valTxt.ClientID %>');
            oFCKeditor.Config.Enabled = true;
            oFCKeditor.Config.UserFilesPath = "'<%= iStore.Site.SiteUrl %>Content/Pages'";
            oFCKeditor.Config.UserFilesAbsolutePath = "'<%= iStore.Site.SiteUrl %>Content/Pages'";

            oFCKeditor.Height = '500';
            oFCKeditor.Width = '716';
            oFCKeditor.BasePath = sBasePath;
            oFCKeditor.ReplaceTextarea();
        }
    </script>
    <asp:Button runat="server" ID="SaveBtn" OnClick="Save" Text="Save" />

     <p>
        <a href="<%= iStore.Site.SiteAdminUrl %>ShopProperties/">Вернуться к списку свойств</a>
    </p>
    <br />
</div>



</asp:Content>
