<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="iStore.Admin.Statistics.OrderDetails" MasterPageFile="~/Admin/Admin.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
<script type="text/javascript">

    function Update() {
        var total = 0;

        $(".TotalSum", "#ProdTable").each(function () {
            var parent = $(this).parent();
            $(".Price", parent).html(($(".Price", parent).html().replace(",", ".")/1).toFixed(2));
            var sum = $(".Price", parent).html().replace(",", ".") * $(".ProdCount", parent).html();
            $(this).html(sum.toFixed(2));

        });


        $(".TotalSum", "#ProdTable").each(function () {
            total += $(this).html() / 1;
        });
        $("#TotalSumID").html(total.toFixed(2));
    }
</script>
<h1>Shopping Cart</h1>

<table class="cart_table" width="760" cellpadding="0" cellspacing="" id="ProdTable">
    <tr>
        <th align="left">
            Name
        </th>
        <th align="center" width="100">
            Price
        </th>
        <th align="center" width="70">
            Quantity
        </th>
        <th align="center" width="70">
            Total
        </th>
    </tr>
<%  int counter = 0;
      foreach (var prodRef in Order.OrdersRefProducts)
      {
          counter++;
          %>
    <tr>
        <td class="left_border" align="left">
            <%=prodRef.Product.Name%>
        </td>
        <td align="center" class="Price">
            <%=prodRef.Product.Price%>
        </td>
        <td align="center">
            <div class="cent_triger">
                <span class="ProdCount"><%=Math.Min(prodRef.Count, prodRef.Product.Count)%></span>
            </div>
        </td>
        <td class="right_border TotalSum" align="center"></td>
    </tr>
       <%
      } %>
    <tr>
        <td class="left_border">Sub Total</td><td></td><td></td>
        <td class="right_border" align="center" id="TotalSumID"></td>
    </tr>
    </table>
        <script type="text/javascript" >
        Update();
    </script> 
</asp:Content>