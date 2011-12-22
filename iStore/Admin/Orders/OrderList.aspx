<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="iStore.Admin.Orders.OrderList" MasterPageFile="~/Admin/Admin.Master" %>


<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="main">
Фильтр:
<div style="width:100%">
<span style="float:left">User:<br />
<asp:TextBox ID="usrtxt" runat="server" OnTextChanged="Update" AutoPostBack="True"></asp:TextBox>
</span>
<span style="float:left">По статусу:<br />
    <asp:DropDownList runat="server" ID="statusDdl" OnSelectedIndexChanged="Update" AutoPostBack="True">
    <asp:ListItem Text="st1" Value="1"></asp:ListItem>
    <asp:ListItem Text="st2" Value="2"></asp:ListItem>
    <asp:ListItem Text="st3" Value="3"></asp:ListItem>
    </asp:DropDownList>
</span>
<span style="float:left">По типу доставки:<br />
    <asp:DropDownList runat="server" ID="deliveryDdl" OnSelectedIndexChanged="Update" AutoPostBack="True">
    <asp:ListItem Text="t1" Value="1"></asp:ListItem>
    <asp:ListItem Text="t2" Value="2"></asp:ListItem>
    <asp:ListItem Text="t3" Value="3"></asp:ListItem>
    </asp:DropDownList>
</span>
<span style="float:left">По дате:<br />
    <asp:DropDownList runat="server" ID="dateDdl" OnSelectedIndexChanged="Update" AutoPostBack="True">
    <asp:ListItem Text="last hour" Value="1"> </asp:ListItem>
    <asp:ListItem Text="last 24" Value="24"> </asp:ListItem>
    <asp:ListItem Text="last week" Value="168"> </asp:ListItem>
    <asp:ListItem Text="last mounth" Value="5208"> </asp:ListItem>
    </asp:DropDownList>
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
        <td>Способ доставки:</td>
        <td>Login:</td>
    </tr>

<%foreach (var item in FilteredOrders)
  {
      %>

      <tr>
      <td>
           <a href="<%=iStore.Site.SiteUrl + "Admin/Orders/Details.aspx?oid=" + item.OrderID.ToString() %>"><%= item.CreateDate %></a>
        </td>
        <td>
            <%=item.TotalSum.ToString() %>
        </td>
        <td>
            <%=item.OrderStatuse.OrderStatus %>
        </td>
        <td>
            <%=item.CreateDate.ToShortDateString()%>
        </td>
        <td>
            <%=item.PaymentType.PaymentType1%>
        </td>
                <td>
            <%=item.DeliveryType.DeliveryType1%>
        </td>
                <td>
            <%=item.User.Login%>
        </td>
      </tr>

      <%
  } %>
  </table>
</div>



</asp:Content>
