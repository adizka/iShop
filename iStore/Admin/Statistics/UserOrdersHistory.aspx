<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserOrdersHistory.aspx.cs" Inherits="iStore.Search.UserOrdersHistory" MasterPageFile="~/Admin/Admin.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
  <table id="ProdTable">
    <tr>
        <td>
            Заказ от числа:
        </td>
        <td>
            Cумма
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
           <a href="<%=iStore.Site.SiteAdminUrl + "Statistics/OrderDetails.aspx?oid=" + item.OrderID.ToString() %>"><%= (item.IsActive)?"Current cart": item.CreateDate.ToShortDateString() %></a>
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