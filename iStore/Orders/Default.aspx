<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="iStore.Orders.Default" MasterPageFile="~/Page.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.easing-1.3.pack.js"></script>

<%if (UserOrder.IsActive)
  { %>



<script type="text/javascript">
    function Update() {

        var selectedEl = " ";
        $("input:checkbox:checked", "#ProdTable").
        each(function () {
            selectedEl += $(this).attr("id") + "~";
        });

        selectedEl += "~~~";
        $("input:checkbox", "#ProdTable").
        each(function () {
            if ($(".ProdCount", $(this).parent().parent()).html() / 1 < 1 || isNaN($(".ProdCount", $(this).parent().parent()).html()))
                $(".ProdCount", $(this).parent().parent()).html("1");

            $(".ProdCount", $(this).parent().parent()).val(Math.floor($(".ProdCount", $(this).parent().parent()).html()));

            selectedEl += $(this).attr("id") + "~" + $(".ProdCount", $(this).parent().parent()).html() + "~~";
        });
        $("#hf").val(selectedEl);

        Sum();
    }
    function Sum() {
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

    function Incr(el) {

        var count = $($(el).parent().children()[1]).html() / 1;

        if (count == 1)
            $($(el).parent().children()[0]).css("cursor", "pointer")

        $($(el).parent().children()[1]).html(count + 1);
        Sum();
        Update();
    }
    function Decr(el) {

        var count = $($(el).parent().children()[1]).html() / 1;

        if (count == 1) {
            $(el).css("cursor", "");
            return;
        }
        else {
            $(el).css("cursor", "pointer");
        }

        $($(el).parent().children()[1]).html(count - 1);
        Sum();
        Update();
    }

    function SelDes(val) {
        $("input:checkbox", "#ProdTable").attr("checked", val);
        Update();
    }

</script>

<h1>Your Shopping Cart</h1>
<table class="cart_table" width="760" cellpadding="0" cellspacing="" id="ProdTable">
    <tr>
        <th align="center" width="70">
            Delete
        </th>
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
      foreach (var prodRef in UserOrder.OrdersRefProducts)
      {
          counter++;
          %>
    <tr>
        <td class="left_border" align="center">
            <input type="checkbox" onchange="Update()" id="<%=prodRef.ID%>" /> 
        </td>  
        <td align="left">
            <%=prodRef.Product.Name%>
        </td>
        <td align="center" class="Price">
            <%=prodRef.Product.Price%>
        </td>
        <td align="center">
            <div class="cent_triger">
                <span class="minus_triger" onclick="Decr(this)"></span>
                <span class="ProdCount"><%=Math.Min(prodRef.Count, prodRef.Product.Count)%></span>
                <span class="plus_triger" onclick="Incr(this)"></span>
            </div>
        </td>
        <td class="right_border TotalSum" align="center"></td>
    </tr>
       <%
      } %>
    <tr>
        <td class="left_border">Sub Total</td><td></td><td></td><td></td>
        <td class="right_border" align="center" id="TotalSumID"></td>
    </tr>
    </table>
    <asp:HiddenField ID="hf" runat="server" ClientIDMode="Static" ></asp:HiddenField>
    <script type="text/javascript" > 
        function pageLoad() {
            Update();
        }
        Update();
    </script> 
    <%if (UserOrder.OrdersRefProducts.Count != 0)
      { %>
    <div class="options_div">
              <span class="right_pay"><span>
                  <asp:LinkButton runat="server" CssClass="right_pay" OnClick="FromOrder" Text="Order" />
              </span></span>
        <p>
            <a class="clear_select" href="javascript:SelDes(true)">select all</a>
            <a class="clear_select" href="javascript:SelDes(false)">deselect all</a>
            <span class="universal_btn">
                <span>
                    <asp:LinkButton OnClick="Save" ID="btnSave" runat="server" Text="Save" />
                </span>
            </span> &nbsp
            <span class="universal_btn">
                <span>
                    <asp:LinkButton OnClick="Clear" ID="btnClear" runat="server" Text="Clear" />
                </span>
            </span>
            <span class="universal_btn">
                <span>
                    <asp:LinkButton OnClick="DeleteSelected" ID="delSelected" runat="server" Text="Delete Selected" />
                </span>
            </span>
        </p>
    </div>
    <%} %>
    <%}
  else
  { %>
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
      foreach (var prodRef in UserOrder.OrdersRefProducts)
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
    
    <tr>
    <td>Итого</td>
    <td><%=UserOrder.TotalSum%></td>
    </tr>
    </table>
    <%} %>
    <a href="/Orders/OrdersList.aspx">Перейти к остории заказов</a>
</asp:Content>