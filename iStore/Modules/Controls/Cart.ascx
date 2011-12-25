<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cart.ascx.cs" Inherits="iStore.Modules.Controls.Cart" %>

<%if(Order != null){ %>
<div>
  You have <a href="/Orders/?oid=<%= Order.OrderID.ToString()%>"> <%= ((ProdCount>0)? ProdCount.ToString(): "no") %> items </a> in your shopping cart.
     <%= (ProdCount > 0)? "Total sum "+ TotalSum.ToString():string.Empty %>
</div>
<%}else
  { %>
   You have <a href="/Orders/?oid="> no items </a> in your shopping cart.
  <%} %>