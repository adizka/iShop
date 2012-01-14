<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PropertyEdit.aspx.cs" Inherits="iStore.Admin.ShopProperty.PropertyEdit" MasterPageFile="~/Admin/Admin.Master" Title="Shop properties | Marvel Worldwide" %>



<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="main">
<p class="BCCategories">
    Shop property edit
</p>
<div class="pader_category">  
    <div runat="server" id="ErrorMsg" visible="false">
        This field shoul be filled!!!
    </div>
    <div class="line_style">
        <p>
            Property name
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
    <p>
        <span class="universal_button">
            <span>
                <asp:LinkButton runat="server" ID="SaveBtn" OnClick="Save" Text="Save" />
            </span>
        </span>
    </p>
    </div>
</div>
    <p class="BCCategories">
        <a href="<%= iStore.Site.SiteAdminUrl %>ShopProperties/">Back to shop properties</a>
    </p>
</asp:Content>
