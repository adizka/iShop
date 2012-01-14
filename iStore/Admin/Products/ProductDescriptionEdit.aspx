<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductDescriptionEdit.aspx.cs" MasterPageFile="~/Admin/Admin.Master" Inherits="iStore.Admin.Products.ProductDescriptionEdit" Title="Product description | Marvel Worldwide" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
<div>    

    <p class="BCCategories">
        <a href="<%= iStore.Site.SiteAdminUrl %>Products/?cid=<%= Request.QueryString["cid"] %>">Back to Product list</a>
    </p>
    <br />
    <div class="Admin_LoginErrors" id="divError" runat="server">
        
    </div>
    <div class="line_style ProductEdit_Save">
        <p>
            <span>Edit product</span> <span><b><%= CurrentProduct.Name%></b></span>
        </p>
        <p>
            <asp:TextBox runat="server" ID="txtBody" TextMode="MultiLine" />
        </p> 
    </div>

    <div class="ProductEdit_Save">
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
                    <span class="universal_button"> 
                        <span>
                            <asp:LinkButton runat="server" ID="SaveBtn" OnClick="SaveDescription" Text="Save" />
                        </span>
                    </span>
    </div>

    <p class="BCCategories">
        <a href="<%= iStore.Site.SiteAdminUrl %>Products/?p=<%=  Request.QueryString["p"] + "&cid=" + Request.QueryString["cid"] %>">Back to Product list</a>
    </p>
</div>

</asp:Content>