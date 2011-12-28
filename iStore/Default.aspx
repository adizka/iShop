<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="iStore._Default" %>

<%@Register TagPrefix="iS" TagName="AddToCart" Src="~/Modules/Controls/AddToCard.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <h1>New Deals</h1>    
    </div>
    <div class="last_goods">
        <% foreach (var product in AllProducts)
           {%>
           <div class="goods_item">
                <div class="img_field02">
                    <img alt="<%= product.Name  %>" src="<%= iStore.Site.PreUrlProductPreviewImage %><%= GetProductPreviewById(product.ProductID) %>"  />
                </div>
                <div class="price_name">
                    <span class="name_product"><%= product.Name %></span>
                    <span class="price_product">$<%= product.Price.ToString() %></span> <br />
                    <a class="more_details" href="<%= iStore.Site.SiteUrl %>Products/?pid=<%= product.ProductID.ToString() %>">
                        more details
                    </a>
                </div>
                <div class="cart_button">
                        <span class="universal_btn">
                            <span>
                                <a href="#">Add to cart</a>
                            </span>
                        </span>
                     <%--<iS:AddToCart ProductId='<%#Eval("ProductID") %>' runat="server" IsCounterVisible="false"  />--%>
                </div>
            </div>     
        <% } %>
     
    </div>
</asp:Content>