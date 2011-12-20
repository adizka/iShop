<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MostPopularProducts.ascx.cs" Inherits="iStore.Design.MostPopularProducts" %>

<div class="Title">
    Products
</div>
<div>
    <div class="Slider_Left" id="slide_left" onclick="SlideToLeft();"><</div>
    <div class="Slider">
        <div class="HiddenLeft">0</div>
        <div class="HiddenRight"><%= AllProducts.Count().ToString() %></div>
        <ul>
          <% int i = 0; foreach (var product in AllProducts)
             { %>

            <li id="li<%= i.ToString() %>" class="SliderItem">
                <div>
                    <img alt="<%= product.Name  %>" src="<%= iStore.Site.PreUrlProductPreviewImage %><%= GetProductPreviewById(product.ProductID) %>"  />
                </div>
                <div>
                    <span><%= product.Name %></span>
                    <span>$<%= product.Price.ToString() %></span>
                </div>
                <div>
                    <a href="<%= iStore.Site.SiteUrl %>Products/?pid=<%= product.ProductID.ToString() %>">
                        more details
                    </a>
                </div>
            </li>

           <% i++;
             } %>
        </ul>
    </div>
    <div class="Slider_Right" id="slide_right" onclick="SlideToRight();">></div>
</div>