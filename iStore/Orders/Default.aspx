<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="iStore.Orders.Default" MasterPageFile="~/Site.Master" %>

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
        $("input:checkbox:not(:checked)", "#ProdTable").
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
            var sum = $(".Price", parent).html() * $(".ProdCount", parent).html();  
            $(this).html(sum);

        });


        $(".TotalSum", "#ProdTable").each(function () {
            total += $(this).html() / 1;
        });
        $("#TotalSumID").html(total);
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

</script>

<table id="ProdTable">
    <tr>
        <td>
            Удалить
        </td>
        <td>
            Название
        </td>
        <td>
            Цена
        </td>
        <td>
            Количество
        </td>
        <td>
            Итого
        </td>

    </tr>
<%  
      foreach (var prodRef in UserOrder.OrdersRefProducts)
      {%>
       <tr>
       <td>
<input type="checkbox" onchange="Update()" id="<%=prodRef.ID%>" /> 
</td>  
<td>
<%=prodRef.Product.Name%>
</td>
<td class="Price">
<%=prodRef.Product.Price%>
</td>
<td >
<div><span style="cursor:pointer;color:Blue;" onclick="Decr(this)">-</span><span class="ProdCount"><%=prodRef.Count%></span><span style="cursor:pointer;color:Blue;" onclick="Incr(this)">+</span></div>
</td>
<td class="TotalSum">

</td>
     
       </tr>
       <%
      } %>
    
    <tr>
    <td>Итого</td><td></td><td></td>
    <td id="TotalSumID"></td>
    <td></td>
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
    <asp:Button  OnClick="Save" ID="btnSave" runat="server" Text="Save" />
    <asp:Button  OnClick="Clear" ID="btnClear" runat="server" Text="Clear" />
    <a href="<%= iStore.Site.SiteUrl + "Orders/FormOrder.aspx" %>">Оплатить</a>
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

    
</asp:Content>
