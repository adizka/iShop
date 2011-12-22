<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="iStore.Admin.Orders.Details" MasterPageFile="~/Admin/Admin.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">

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
      <tr>
      <td>
           <%= Order.CreateDate%>
        </td>
        <td>
            <%=Order.TotalSum.ToString()%>
        </td>
        <td>
        <asp:DropDownList ID="ordstatDdl" runat="server">
            <asp:ListItem Text="st1" Value="1"></asp:ListItem>
            <asp:ListItem Text="st2" Value="2"></asp:ListItem>
            <asp:ListItem Text="st3" Value="3"></asp:ListItem>
        </asp:DropDownList>
            
        </td>
        <td>
            <%=Order.CreateDate.ToShortDateString()%>
        </td>
        <td>
            <%=Order.PaymentType.PaymentType1%>
        </td>
        <td>
            <%=Order.DeliveryType.DeliveryType1%>
        </td>
                <td>
            <%=Order.User.Login%>
        </td>
      </tr>
  </table>

  <asp:Button runat="server" ID="saveBtn" OnClick="Save" />




</asp:Content>
