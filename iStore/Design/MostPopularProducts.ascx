<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MostPopularProducts.ascx.cs" Inherits="iStore.Design.MostPopularProducts" %>
<%@Register TagPrefix="iS" TagName="AddToCart" Src="~/Modules/Controls/AddToCard.ascx" %>
<h1>Products</h1>
<div class="border_bg">
<div class="black_bg">
    <a class="Slider_Left" id="slide_left" onclick="SlideToLeft();"></a>
    <a class="Slider_Right" id="slide_right" onclick="SlideToRight();"></a>
    <div class="Slider">
        <div class="HiddenLeft">0</div>
        <div class="HiddenRight"><%= AllProducts.Count().ToString() %></div>
        <ul>
        <asp:Repeater ID="rpt"  runat="server">
        <ItemTemplate>
          <% int i = 0; %>

            <li id="li<%= i.ToString() %>" class="SliderItem">
                <div class="img_field">
                    <img alt="<%#Eval("Name")  %>" src="<%= iStore.Site.PreUrlProductPreviewImage %><%#GetProductPreviewById(Eval("ProductID").ToString()) %>"  />
                </div>
                <div class="price_name">
                    <span class="name_product"><%#Eval("Name") %></span>
                    <span class="price_product">$<%#Eval("Price").ToString() %></span> <br />
                    <a class="more_details" href="<%= iStore.Site.SiteUrl %>Products/?pid=<%#Eval("ProductID").ToString() %>">
                        more details
                    </a>
                </div>
                <div class="cart_button">
                    <is:addtocart id="AddToCart1" productid='<%#Eval("ProductID") %>' runat="server"
                        iscountervisible="false" />
                </div>
            </li>

           <% i++;
              %>
             </ItemTemplate>
             </asp:Repeater>
        </ul>
    </div>
</div>
</div>