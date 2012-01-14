<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/Admin.Master"  CodeBehind="Default.aspx.cs" Inherits="iStore.Admin.Products.Default" Title="Products | Marvel Worldwide" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>
<%@ Register TagPrefix="iS" TagName="Pager" Src="~/Modules/Controls/Pager/Pager.ascx" %>
<%@ Register tagPrefix="iS" TagName="BreadCrumbs" src="~/Modules/Controls/BreadCrumbs/BreadCrumbs.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
<script type="text/javascript">
    function DeleteProduct(prodID) {
        $("#hf").val(prodID);
        $("#delBtn").click();
    }
</script>
<div style="display:none;">
<asp:Button runat="server" ID="delBtn" OnClick="DeleteProduct" ClientIDMode="Static" />
<asp:HiddenField runat="server" ID="hf" ClientIDMode="Static" />
</div>
    <iS:BreadCrumbs runat="server" ID="bc" SiteMode="false" EntityType="Products" /> 
    <p class="right_category">
        <asp:DropDownList runat="server" ID="ddlChildCategories"></asp:DropDownList>
        <span class="universal_button">
            <span>
                <asp:LinkButton runat="server" ID="btnRedirect" OnClick="RedirectToSelectedCategory" Text="view"/>
            </span>
        </span>
    </p>

    <p class="BCCategories">
        <a href="<%= iStore.Site.SiteAdminUrl %>Products/ProductEdit.aspx?cid=<%= Request.QueryString["cid"] %>">Add product</a>
    </p>
    <br />
    <iS:ValidateErrors runat="server" ID="ve" Visible="false" />
<div class="pader_category">
    <div class="top_repeater"></div>
        <div class="mid_repeater">
            <p class="cat_h3">Products</p>
        </div>
        <div class="mid_repeatergrey">
            <p class="repparaq">
                <span class="prod_span01">Name</span>
                <span class="prod_span02">Count</span>
                <span class="prod_span02">Price</span>
                <span class="prod_span03">Properies</span>
                <span class="prod_span03">Description</span>
                <span class="prod_span04">Photo</span>
                <span class="prod_span05">Action</span>
            </p>
        </div>
        <div class="mid_repeater">
             <% if (!PageProducts.Any())
           { %>
                <p class="no_goods">No products</p>
           <%} %>
        <% int i = 0; string cssClass = string.Empty;
           foreach (var item in PageProducts)
               {
                    i++; cssClass = ((i % 2) == 1 ) ? "first" : "second";
        %>
        <p class="repparaq <%= cssClass %>">
            <span class="prod_span01">
                <a target="_blank" href="<%= iStore.Site.SiteUrl %>Products/?pid=<%= item.ProductID.ToString() %>"><%= item.Product.Name %></a> 
            </span>
            <span class="prod_span02"><%= item.Product.Count.ToString() %></span>
            <span class="prod_span02">
                <%= item.Product.Price.ToString() %>
            </span>
            <span class="prod_span03">
                <a class="blue_yanc" href="<%= iStore.Site.SiteAdminUrl %>Products/ProductPropertyEdit.aspx?pid=<%= item.ProductID.ToString() %>&cid=<%= Request.QueryString["cid"] %>">properties</a>
            </span>
            <span class="prod_span03">
                <a class="blue_yanc" href="<%= iStore.Site.SiteAdminUrl %>Products/ProductDescriptionEdit.aspx?pid=<%= item.ProductID.ToString() %>&cid=<%= Request.QueryString["cid"] %>">description</a>
            </span>
            <span class="prod_span04">
                <a class="photo_ico" href="<%= iStore.Site.SiteAdminUrl %>Products/ProductsPhoto.aspx?pid=<%= item.ProductID.ToString() %>&cid=<%= Request.QueryString["cid"] %>"></a>
            </span>
            <span class="prod_span05">
                <a class="edit_ico" href="<%= iStore.Site.SiteAdminUrl %>Products/ProductEdit.aspx?pid=<%= item.ProductID.ToString() %>&cid=<%= Request.QueryString["cid"] %>"></a>
                <a class="delete_ico" href="javascript:DeleteProduct('<%= item.Product.ProductID %>')"></a>
            </span>
        </p>       
        <% } %>
        </div>
        <div class="bot_repeater"></div>
    <iS:Pager runat="server" ID="pager" EntitiesPerPage="10"></iS:Pager>
</div>
</asp:Content>
