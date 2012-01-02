<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cart.ascx.cs" Inherits="iStore.Modules.Controls.Cart" %>

<%if(Order != null){ %>
        <h2><a href="/Orders/?oid=<%= Order.OrderID.ToString()%>">Shopping cart</a></h2>
<div class="cart_div">
  You have <a href="/Orders/?oid=<%= Order.OrderID.ToString()%>"> <%= ((ProdCount>0)? ProdCount.ToString(): "no") %> items </a> in your shopping cart.
     <%= (ProdCount > 0)? "Total sum "+ TotalSum.ToString():string.Empty %>
</div>
<%}else
  { %>
          <h2><a href="/Orders/?oid=">Shopping cart</a></h2>
<div class="cart_div">
   You have <a href="/Orders/?oid="> no items </a> in your shopping cart.
</div>
  <%} %>