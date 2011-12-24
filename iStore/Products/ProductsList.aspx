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
                <a href="/Products/?pid=<%=item.ProductID %>"><%= item.Product.Name %></a> 
            </div>
            <div>
                <%= item.Product.Price.ToString() %>
            </div>
        </p>       
        <% } %>
    </div>

<iS:Pager runat="server" ID="pager"  AddInsParams="Products"></iS:Pager>
</asp:Content>