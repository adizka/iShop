<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Page.Master"  CodeBehind="Default.aspx.cs" Inherits="iStore.Categories.Default" %>

<%@ Register tagPrefix="iS" tagName="AddToCard" src="~/Modules/Controls/AddToCard.ascx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<style type="text/css">
.ProductsList {}
.ChildCategoryList {}
.ProductPhoto { width:150px; }
.ProductProperties {width: 500px; padding-left:40px; }
.ProductName { font-weight: bold; }
.ProductPrice { width:100px;  }
</style>

<div class="Title"><%= CurrentCategory.Name %> [<%= GetProductsRefCurrentCategory.Count().ToString()%>]</div><br />

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
</div> <br /><br />
<div class="ProductsList">
    <% foreach (var productRefCat in GetProductsRefCurrentCategory)
       { %>
            <table class="ProductList_Table" >
            <tr>
                <%--Photo--%>
                <td class="ProductPhoto">
                    <img  src="<%= iStore.Site.PreUrlProductPreviewImage %><%= GetProductPreview(productRefCat.ProductID) %>" alt="<%= productRefCat.Product.Name %>" />
                </td>
                <%--Propery--%>
                <td class="ProductProperties">
                    <div class="ProductName">
                        <%= productRefCat.Product.Name %>
                    </div>
                    <% foreach (var property in GetProductPropery(productRefCat.ProductID))
                    { %>
                        <%= property.PropertyName %>: <%= property.PropertyValue %> <br />
                 <% } %>
                </td>
                <td class="ProductPrice">
                    Our Price: <br />
                    $<%= productRefCat.Product.Price.ToString() %><br />
                    <iS:AddToCard runat="server" ProductId="<%= productRefCat.ProductID.ToString() %>" />
                </td>
            </tr>
            </table>
    <% } %>
</div>
</asp:Content>
