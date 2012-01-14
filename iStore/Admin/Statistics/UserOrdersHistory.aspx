<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserOrdersHistory.aspx.cs" Inherits="iStore.Search.UserOrdersHistory" MasterPageFile="~/Admin/Admin.Master" Title="Orders history | Marvel Worldwide" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
<br />
<div class="pader_category" id="ProdTable">
    <div class="top_repeater"></div>
        <div class="mid_repeater">
            <p class="cat_h3">User Orders History</p>
        </div>
        <div class="mid_repeatergrey">
            <p class="repparaq">
                <span class="cat_span03">Order date:</span>
                <span class="prod_span06">Sum</span>
                <span class="prod_span06">Status</span>
                <span class="prod_span06">Delivered</span>
                <span class="prod_span06">Payment option</span>
            </p>
        </div>
        <div class="mid_repeater">
        <%foreach (var item in UserOrders)
          {
              %>
            <p class="repparaq">
                <span class="cat_span03"><a href="<%=iStore.Site.SiteAdminUrl + "Statistics/OrderDetails.aspx?oid=" + item.OrderID.ToString() %>"><%= (item.IsActive)?"Current cart": item.CreateDate.ToShortDateString() %></a></span>
                <span class="prod_span06"><%=(item.OrderStatusID == (int)BL.OrderStatus.Paid)?item.TotalSum.ToString():item.OrdersRefProducts.Sum(r=>r.Product.Price* r.Count).ToString() %></span>
                <span class="prod_span06"><%=item.OrderStatuse.OrderStatus %></span>
                <span class="prod_span06"><%= (item.OrderStatusID == (int)BL.OrderStatus.Paid)? item.CreateDate.ToShortDateString():string.Empty %></span>
                <span class="prod_span06"><%= (item.OrderStatusID == (int)BL.OrderStatus.Paid)? item.PaymentType.PaymentType1:string.Empty %></span>
            </p>
                <%
          } %>
        </div>
        <div class="bot_repeater"></div>
</div>
</asp:Content>
