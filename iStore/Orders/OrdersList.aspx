<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrdersList.aspx.cs" Inherits="iStore.Orders.OrdersList" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
<table id="ProdTable">
    <tr>
        <td>
            Заказ от числа:
        </td>
        <td>
            Ceмма
        </td>
        <td>
            Статус
        </td>
        <td>
        Доставлено:
        </td>
        <td>
            Способ оплаты:
        </td>
    </tr>

<%foreach (var item in UserOrders)
  {
      %>

      <tr>
      <td>
           <a href="<%=iStore.Site.SiteUrl + "Orders/?oid=" + item.OrderID.ToString() %>"><%= item.CreateDate %></a>
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