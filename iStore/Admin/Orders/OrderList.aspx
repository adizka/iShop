<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="iStore.Admin.Orders.OrderList" MasterPageFile="~/Admin/Admin.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="main">
<br />
<div class="pader_category">
    <div class="top_repeater"></div>
        <div class="mid_repeater">
            <p class="cat_h3">Order list</p>
        </div>
        <div class="mid_repeater">
            <p class="repparaq">
                <span class="order_span02">
                    User: <br />
                    <asp:TextBox CssClass="field_universe" ID="usrtxt" runat="server" OnTextChanged="Update" AutoPostBack="True"></asp:TextBox>
                </span>
                <span class="order_span02">
                    Status of payment: <br />
                    <asp:DropDownList CssClass="field_universe" runat="server" ID="statusDdl" OnSelectedIndexChanged="Update" AutoPostBack="True">
                        <asp:ListItem Text="Not paid" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Paid" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </span>
                <span class="order_span02">
                    Delivery status <br />
                    <asp:DropDownList CssClass="field_universe" runat="server" ID="deliveryDdl" OnSelectedIndexChanged="Update" AutoPostBack="True">
                        <asp:ListItem Text="Delivered" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Not delivered" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </span>

                <span class="order_span02">
                    Date <br />
                    <asp:DropDownList CssClass="field_universe" runat="server" ID="dateDdl" OnSelectedIndexChanged="Update" AutoPostBack="True">
                        <asp:ListItem Text="last hour" Value="1"> </asp:ListItem>
                        <asp:ListItem Text="last 24" Value="24"> </asp:ListItem>
                        <asp:ListItem Text="last week" Value="168"> </asp:ListItem>
                        <asp:ListItem Text="last mounth" Value="744"> </asp:ListItem>
                        <asp:ListItem Text="last 6 mounth" Value="4464"> </asp:ListItem>
                        <asp:ListItem Text="last year" Value="8784"> </asp:ListItem>
                    </asp:DropDownList>
                </span>
                <span class="order_span03">
                    By transaction ID <br />
                    <asp:TextBox CssClass="field_universe" runat="server" Text="" ID="txName"></asp:TextBox>
                    &nbsp;
                    <span class="universal_button">
                        <span>
                            <asp:LinkButton runat="server" Text="Get" ID="txBtn" OnClick="GetByTransactionID" />
                        </span>
                    </span>
                </span>
            </p>
        </div>
        <div class="mid_repeatergrey">
            <p class="repparaq">
                <span class="order_span05">Login</span>
                <span class="order_span05">Order date</span>
                <span class="order_span07">Sum</span>
                <span class="order_span08">Status</span>
                <span class="order_span09">Payment</span>
                <span class="order_span09">Delivery</span>
                <span class="order_span05">Del. date</span>
                <span class="order_span10">Trans ID</span>
            </p>
        </div>
    <div class="mid_repeater">
    <%foreach (var item in FilteredOrders)
      {
          %>
            <p class="repparaq">
                <span class="order_span05"><%=item.User.Login%></span>
                <span class="order_span05"><a href="<%=iStore.Site.SiteUrl + "Admin/Orders/Details.aspx?oid=" + item.OrderID.ToString() %>"><%= item.CreateDate %></a></span>
                <span class="order_span07"><%= ((item.IsPaid) ? item.TotalSum.ToString() : Convert.ToDecimal(item.OrdersRefProducts.Sum(r => r.Product.Price * r.Count)).ToString())%></span>
                <span class="order_span08"><%=item.OrderStatuse.OrderStatus %></span>
                <span class="order_span09"><%= ((item.IsPaid) ? item.PaymentType.PaymentType1 : string.Empty)%></span>
                <span class="order_span09"><%=item.DeliveryType.DeliveryType1%></span>
                <span class="order_span05"><%=((!string.IsNullOrEmpty(item.TransactionID)) ? item.TransactionID.ToString():string.Empty)%></span>
                <span class="order_span10"><%=((item.DeliveryTypeID == 1) ? item.DeliveryDate.ToUniversalTime().ToString() : string.Empty)%></span>
            </p>
      <%
  } %>
  </div>
  <div class="bot_repeater"></div>
</div>

</asp:Content>
