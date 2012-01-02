<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormOrder.aspx.cs" Inherits="iStore.Orders.FormOrder"  MasterPageFile="~/Site.Master"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.easing-1.3.pack.js"></script>

<%if (UserOrder != null && UserOrder.OrdersRefProducts.Count != 0)
  {
       %>
 <p>
 <b>Payment type:</b> <br />
     <asp:DropDownList ID="paymentDdl" runat="server">
     <asp:ListItem Text="PayPal" Value="1"></asp:ListItem>
     <asp:ListItem Text="e-Gold" Value="2"></asp:ListItem>
     </asp:DropDownList>
 </p>
 
 <p>
 <b>Delivery type:</b> <br />
     <asp:DropDownList ID="deliveryDdl" runat="server">
     <asp:ListItem Text="fast" Value="1"></asp:ListItem>
     <asp:ListItem Text="slow" Value="2"></asp:ListItem>

     </asp:DropDownList>
 </p>
 <asp:Button runat="server" Text="Pay" ID="payBtn" OnClick="Pay" />
   <%
  }
  else
  { %>
  <p>
  <b><%if (IsPostBack)
       { %>Заявка принята<%}
       else
       { %>
       Ваша карзина пустая
       <%} %></b>
  </p>
   <%} %>
</asp:Content>