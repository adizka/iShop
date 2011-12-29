<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Page.Master"  CodeBehind="Default.aspx.cs" Inherits="iStore.Categories.Default" %>

<%@ Register tagPrefix="iS" tagName="AddToCard" src="~/Modules/Controls/AddToCard.ascx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div class="Title"><h1><%= CurrentCategory.Name %> [<%= GetProductsRefCurrentCategory.Count().ToString()%>]</h1></div>
<div class="ChildCategoryList">
    <span class="Title">Подкатегории: </span> 
    <% foreach (var category in GetChildCategoryWhereParentIsCurrentCategory)
       { %>
            <span>
                <a href="<%= iStore.Site.SiteUrl %>Categories/?cid=<%= category.CategoryID.ToString() %>">
                    <%= category.Name %>
                </a> 
                <span>[<%= GetProductsCountInCategory(category.CategoryID) %>]</span>
            </span>      
    <% } %>    
</div> 
<br />
    <% foreach (var productRefCat in GetProductsRefCurrentCategory)
       { %>
       <div class="ProductsList">
                    <div class="image_ploater">
                        <img src="<%= iStore.Site.PreUrlProductPreviewImage %><%= GetProductPreview(productRefCat.ProductID) %>" alt="<%= productRefCat.Product.Name %>" />
                        <%--<img src="../images/new_flac.png" alt="" class="flac_triangle" />
                        <img src="../images/sale_flac.png" alt="" class="flac_triangle" />
                        <img src="../images/hot_flac.png" alt="" class="flac_triangle" />--%>
                    </div>
                    <div class="info_blocked">
                        <div class="ProductName">
                            <%= productRefCat.Product.Name %>
                        </div>
                        <div class="ProductInfo">
                        <% foreach (var property in GetProductPropery(productRefCat.ProductID))
                        { %>
                            <%= property.PropertyName %>: <%= property.PropertyValue %> <br />
                        <% } %>
                        </div>
                    </div>
                   <div class="price_blocked">
                       <p class="price_paraq">Our Price:</p> 
                       <p class="price_paraq2"> $<%= productRefCat.Product.Price.ToString() %></p>
                       <%-- <iS:AddToCard runat="server" ProductId="<%= productRefCat.ProductID.ToString() %>" />--%>
                   </div>
        </div>
    <% } %>
    <br />
</asp:Content>
