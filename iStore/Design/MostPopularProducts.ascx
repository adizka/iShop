<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MostPopularProducts.ascx.cs" Inherits="iStore.Design.MostPopularProducts" %>

<h1>Products</h1>
<div class="border_bg">
<div class="black_bg">
    <a class="Slider_Left" id="slide_left" onclick="SlideToLeft();"></a>
    <a class="Slider_Right" id="slide_right" onclick="SlideToRight();"></a>
    <div class="Slider">
        <div class="HiddenLeft">0</div>
        <div class="HiddenRight"><%= AllProducts.Count().ToString() %></div>
        <ul>
          <% int i = 0; foreach (var product in AllProducts)
             { %>

            <li id="li<%= i.ToString() %>" class="SliderItem">
                <div class="img_field">
                    <img alt="<%= product.Name  %>" src="<%= iStore.Site.PreUrlProductPreviewImage %><%= GetProductPreviewById(product.ProductID) %>"  />
                </div>
                <div class="price_name">
                    <span class="name_product"><%= product.Name %></span>
                    <span class="price_product">$<%= product.Price.ToString() %></span> <br />
                    <a class="more_details" href="<%= iStore.Site.SiteUrl %>Products/?pid=<%= product.ProductID.ToString() %>">
                        more details
                    </a>
                </div>
            </li>

           <% i++;
             } %>
        </ul>
    </div>
</div>
</div>