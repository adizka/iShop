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
                <td>
            Transaction ID
        </td>
    </tr>
      <tr>
     <td>
           <a href="<%=iStore.Site.SiteUrl + "Admin/Orders/Details.aspx?oid=" + Order.OrderID.ToString() %>"><%= Order.CreateDate %></a>
        </td>
        <td>
            <%= ((Order.IsPaid) ? Order.TotalSum.ToString() : Convert.ToDecimal(Order.OrdersRefProducts.Sum(r => r.Product.Price * r.Count)).ToString())%>
        </td>
          <td>
              <asp:DropDownList runat="server" ID="statusDdl">
                  <asp:ListItem Text="Not paid" Value="1"></asp:ListItem>
                  <asp:ListItem Text="Paid" Value="2"></asp:ListItem>
              </asp:DropDownList>
          </td>
          <td>
            <%=((Order.DeliveryTypeID == 1) ? Order.DeliveryDate.ToUniversalTime().ToString() : string.Empty)%>
        </td>
        <td>
            <%= ((Order.IsPaid) ? Order.PaymentType.PaymentType1 : string.Empty)%>
        </td>
          <td>
              <asp:DropDownList runat="server" ID="deliveryDdl">
                  <asp:ListItem Text="Delivered" Value="1"></asp:ListItem>
                  <asp:ListItem Text="Not delivered" Value="2"></asp:ListItem>
              </asp:DropDownList>
          </td>
          <td>
              <%=Order.User.Login%>
          </td>
          <td>
              <%=((!string.IsNullOrEmpty(Order.TransactionID)) ? Order.TransactionID.ToString():string.Empty)%>
          </td>
      </tr>
  </table>

  <p>Products list</p>
 <table id="Table1">
    <tr>
        <td>
            Название
        </td>
        <td>
            Количество
        </td>
    </tr>
<%  
      foreach (var prodRef in Order.OrdersRefProducts)
      {%>
       <tr>
<td>
<%=prodRef.Product.Name%>
</td>
<td >
<%=prodRef.Count%>
</td>
       </tr>
       <%
      } %>
    </table>
  <asp:Button runat="server" ID="saveBtn" OnClick="Save" Text="Save" />

  <%if (Order.IsPaid)
    { %>
    <br />
    Shipping address:<br />
    <table>
        <tr>
            <td>
                First name:
            </td>
            <td>
                <%=Order.FirstName %>
            </td>
        </tr>
        <tr>
            <td>
                Last name:
            </td>
            <td>
                <%=Order.LastName %>
            </td>
        </tr>
        <tr>
            <td>
                Address line 1:
            </td>
            <td>
                <%=Order.Address1 %>
            </td>
        </tr>
        <tr>
            <td>
                Address line 2:
            </td>
            <td>
                <%=Order.Address2 %>
            </td>
        </tr>
        <tr>
            <td>
                City:
            </td>
            <td>
                <%=Order.City %>
            </td>
        </tr>
        <tr>
            <td>
                Country:
            </td>
            <td>
                <%=Order.Country.Name +"("+Order.Country.ShortName +")"%>
            </td>
        </tr>
        <tr>
            <td>
                Zip code:
            </td>
            <td>
                <%=Order.zipcode.ToString() %>
            </td>
        </tr>
        <tr>
            <td>
                Contact e-mail:
            </td>
            <td>
                <%=Order.email %>
            </td>
        </tr>
        <tr>
            <td>
                Phone number:
            </td>
            <td>
                <%=Order.PhoneNumber%>
            </td>
        </tr>
        <tr>
            <td>
                Special notes:
            </td>
            <td>
                <%=Order.SpecialNote %>
            </td>
        </tr>
    </table>
    <%} %>

  <a href="/Admin/Orders/OrderList.aspx">вернуться к списку!</a>


</asp:Content>
