<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="iStore.Admin.Orders.OrderList" MasterPageFile="~/Admin/Admin.Master" %>


<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="main">
Фильтр:
<div style="width:100%">
<span style="float:left">User:<br />
<asp:TextBox ID="usrtxt" runat="server" OnTextChanged="Update" AutoPostBack="True"></asp:TextBox>
</span>
<span style="float:left">По статусу оплаты:<br />
    <asp:DropDownList runat="server" ID="statusDdl" OnSelectedIndexChanged="Update" AutoPostBack="True">
    <asp:ListItem Text="Not paid" Value="1"></asp:ListItem>
    <asp:ListItem Text="Paid" Value="2"></asp:ListItem>
    </asp:DropDownList>
</span>
<span style="float:left">По статусу доставки:<br />
    <asp:DropDownList runat="server" ID="deliveryDdl" OnSelectedIndexChanged="Update" AutoPostBack="True">
    <asp:ListItem Text="Delivered" Value="1"></asp:ListItem>
    <asp:ListItem Text="Not delivered" Value="2"></asp:ListItem>
    </asp:DropDownList>
</span>
<span style="float:left">По дате:<br />
    <asp:DropDownList runat="server" ID="dateDdl" OnSelectedIndexChanged="Update" AutoPostBack="True">
    <asp:ListItem Text="last hour" Value="1"> </asp:ListItem>
    <asp:ListItem Text="last 24" Value="24"> </asp:ListItem>
    <asp:ListItem Text="last week" Value="168"> </asp:ListItem>
    <asp:ListItem Text="last mounth" Value="744"> </asp:ListItem>
    <asp:ListItem Text="last 6 mounth" Value="4464"> </asp:ListItem>
    <asp:ListItem Text="last year" Value="8784"> </asp:ListItem>
    
    </asp:DropDownList>
</span>
<span>
<asp:Button runat="server" Text="By transaction ID" ID="txBtn" OnClick="GetByTransactionID" /><br />
<asp:TextBox runat="server" Text="" ID="txName"></asp:TextBox>
</span>
</div>
<br />
<div style="width:100%;float:left">
<table>
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
        <td>Статус доставки:</td>
        <td>Login:</td>
        <td>
            Transaction ID
        </td>
    </tr>

<%foreach (var item in FilteredOrders)
  {
      %>

      <tr>
      <td>
           <a href="<%=iStore.Site.SiteUrl + "Admin/Orders/Details.aspx?oid=" + item.OrderID.ToString() %>"><%= item.CreateDate %></a>
        </td>
        <td>
            <%= ((item.IsPaid) ? item.TotalSum.ToString() : Convert.ToDecimal(item.OrdersRefProducts.Sum(r => r.Product.Price * r.Count)).ToString())%>
        </td>
        <td>
            <%=item.OrderStatuse.OrderStatus %>
        </td>
        <td>
            <%=((item.DeliveryTypeID == 1) ? item.DeliveryDate.ToUniversalTime().ToString() : string.Empty)%>
        </td>
        <td>
            <%= ((item.IsPaid) ? item.PaymentType.PaymentType1 : string.Empty)%>
        </td>
                <td>
            <%=item.DeliveryType.DeliveryType1%>
        </td>
          <td>
              <%=item.User.Login%>
          </td>
          <td>
              <%=((!string.IsNullOrEmpty(item.TransactionID)) ? item.TransactionID.ToString():string.Empty)%>
          </td>
      </tr>

      <%
  } %>
  </table>
</div>



</asp:Content>
