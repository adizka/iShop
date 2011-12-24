<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductsList.aspx.cs" Inherits="iStore.Products.ProductsList" MasterPageFile="~/Page.Master" %>

<%@ Register TagPrefix="iS" TagName="Pager" Src="~/Modules/Controls/Pager/Pager.ascx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<div class="rep">

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
            <div>
                <a href="<%= iStore.Site.SiteAdminUrl %>"><%= item.Product.Name %></a> 
            </div>
            <div><%= item.Product.Count.ToString() %></div>
            <div>
                <%= item.Product.Price.ToString() %>
            </div>
            <div>
                <a href="<%= iStore.Site.SiteAdminUrl %>Products/ProductPropertyEdit.aspx?pid=<%= item.ProductID.ToString() %>&cid=<%= Request.QueryString["cid"] %>">Properties</a>
            </div>
            <div>
                <a href="<%= iStore.Site.SiteAdminUrl %>Products/ProductEdit.aspx?pid=<%= item.ProductID.ToString() %>&cid=<%= Request.QueryString["cid"] %>">Edit</a>
            </div>
            <div>
                <a href="<%= iStore.Site.SiteAdminUrl %>">Delete</a>
            </div>
            <div>
                <a href="<%= iStore.Site.SiteAdminUrl %>Products/ProductsPhoto.aspx?pid=<%= item.ProductID.ToString() %>&cid=<%= Request.QueryString["cid"] %>">
                    Edit Photo
                </a>
            </div>
            <div>
                <a href="<%= iStore.Site.SiteAdminUrl %>Products/ProductDescriptionEdit.aspx?pid=<%= item.ProductID.ToString() %>&cid=<%= Request.QueryString["cid"] %>">
                    Edit Description
                </a>
            </div>
        </p>       
        <% } %>
    </div>

<iS:Pager runat="server" ID="pager"  AddInsParams="Products"></iS:Pager>
</asp:Content>