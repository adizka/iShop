<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="iStore.Admin.Statistics.OrderDetails" MasterPageFile="~/Admin/Admin.Master" Title="Orders details | Marvel Worldwide" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
<script type="text/javascript">

    function Update() {
        var total = 0;

        $("#TotalSum", "#ProdTable").each(function () {
            var parent = $(this).parent();
            $("#Price", parent).html(($("#Price", parent).html().replace(",", ".") / 1).toFixed(2));
            var sum = $("#Price", parent).html().replace(",", ".") * $("#ProdCount", parent).html();
            $(this).html(sum.toFixed(2));

        });


        $("#TotalSum", "#ProdTable").each(function () {
            total += $(this).html() / 1;
        });
        $("#TotalSumID").html(total.toFixed(2));
    }
</script>
<br />
<div class="pader_category" id="ProdTable">
<div class="top_repeater"></div>
        <div class="mid_repeater">
            <p class="cat_h3">Shopping Cart</p>
        </div>
        <div class="mid_repeatergrey">
            <p class="repparaq">
                <span class="prod_span01">Name</span>
                <span class="prod_span06">Price</span>
                <span class="prod_span06">Quantity</span>
                <span class="prod_span06">Total</span>
            </p>
        </div>
        <div class="mid_repeater">
        <%  int counter = 0;
              foreach (var prodRef in Order.OrdersRefProducts)
              {
                  counter++;
                  %>
            <p class="repparaq">
                <span class="prod_span01"><%=prodRef.Product.Name%></span>
                <span class="prod_span06" id="Price"><%=prodRef.Product.Price%></span>
                <span class="prod_span06"><span id="ProdCount"><%=Math.Min(prodRef.Count, prodRef.Product.Count)%></span></span>
                <span class="prod_span06" id="TotalSum"></span>
            </p>
           <%
          } %>
        </div>
        <div class="mid_repeater">
            <p class="repparaq">
                <span class="prod_span01">Sub Total:</span>
                <span class="prod_span06">&nbsp;</span>
                <span class="prod_span06">&nbsp;</span>
                <span class="prod_span06" id="TotalSumID">500</span>
            </p>
        </div>
        <div class="bot_repeater"></div>
    <script type="text/javascript" >
            Update();
    </script> 
</div>
</asp:Content>