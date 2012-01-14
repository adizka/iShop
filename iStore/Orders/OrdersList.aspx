<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrdersList.aspx.cs" Inherits="iStore.Orders.OrdersList" MasterPageFile="~/Page.Master" Title="Orders List | Marvel Worldwide" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
<h1>Orders List</h1>
<table class="cart_table" width="100%" id="ProdTable" cellpadding="0" cellspacing="0">
    <tr>
        <th>
            Order date
        </th>
        <th>
            Sum
        </th>
        <th>
            Status
        </th>
        <th>
            Delivered
        </th>
        <th>
            Pay type
        </th>
    </tr>
<%foreach (var item in UserOrders)
  {
      %>
    <tr>
        <td>
           <a href="<%=iStore.Site.SiteUrl + "Orders/?oid=" + item.OrderID.ToString() %>"><%= (item.IsActive)?"Current cart": item.CreateDate.ToShortDateString() %></a>
        </td>
        <td>
            <%=(item.OrderStatusID == (int)BL.OrderStatus.Paid)?item.TotalSum.ToString():item.OrdersRefProducts.Sum(r=>r.Product.Price* r.Count).ToString() %>
        </td>
        <td>
            <%=item.OrderStatuse.OrderStatus %>
        </td>
        <td>
            <%= (item.OrderStatusID == (int)BL.OrderStatus.Paid)? item.CreateDate.ToShortDateString():string.Empty %>
        </td>
        <td>
            <%= (item.OrderStatusID == (int)BL.OrderStatus.Paid)? item.PaymentType.PaymentType1:string.Empty %>
        </td>
    </tr>

      <%
  } %>
  </table>
</asp:Content>