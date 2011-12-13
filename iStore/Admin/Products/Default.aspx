<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/Admin.Master"  CodeBehind="Default.aspx.cs" Inherits="iStore.Admin.Products.Default" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>
<%@ Register TagPrefix="iS" TagName="Pager" Src="~/Modules/Controls/Pager/Pager.ascx" %>
<%@ Register tagPrefix="iS" TagName="BreadCrumbs" src="~/Modules/Controls/BreadCrumbs/BreadCrumbs.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
    <iS:BreadCrumbs runat="server" ID="bc" SiteMode="false" EntityType="Products" /> 
    <asp:DropDownList runat="server" ID="ddlChildCategories"></asp:DropDownList>
    <asp:Button runat="server" ID="btnRedirect" OnClick="RedirectToSelectedCategory" Text="Перейти"/>
    <p >
        <a href="<%= iStore.Site.SiteAdminUrl %>Products/ProductEdit.aspx?cid=<%= Request.QueryString["cid"] %>">Добавить продукт</a>
        <a href="">Сортировать продукты в данной категории</a>
    </p>
    <br />
    <iS:ValidateErrors runat="server" ID="ve" Visible="false" />
    <div class="rep">
        <p class="repHeader">
            <span>Name</span>
            <span>Count</span>
            <span>Price</span>
            <span>Properies</span>
            <span>Edit</span>
            <span>Delete</span>
            <span>Photo</span>
        </p>
        <% if (!PageProducts.Any())
           { %>
                <p>Нет товаров</p>
           <%} %>
        <% int i = 0; string cssClass = string.Empty;
           foreach (var item in PageProducts)
               {
                    i++; cssClass = ((i % 2) == 1 ) ? "first" : "second";
        %>
        <p class="repBody <%= cssClass %>">
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>"><%= item.Product.Name %></a> 
            </span>
            <span><%= item.Product.Count.ToString() %></span>
            <span>
                <%= item.Product.Price.ToString() %>
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>Products/ProductPropertyEdit.aspx?pid=<%= item.ProductID.ToString() %>&cid=<%= Request.QueryString["cid"] %>">Properties</a>
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>Products/ProductEdit.aspx?pid=<%= item.ProductID.ToString() %>&cid=<%= Request.QueryString["cid"] %>">Edit</a>
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>">Delete</a>
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>Products/ProductsPhoto.aspx?pid=<%= item.ProductID.ToString() %>&cid=<%= Request.QueryString["cid"] %>">
                    Edit Photo
                </a>
            </span>
        </p>       
        <% } %>
    </div>
    <iS:Pager runat="server" ID="pager"  AddInsParams="Products"></iS:Pager>
</asp:Content>
