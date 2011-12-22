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
            if ($(".CountInput", $(this).parent().parent()).val() / 1 < 0 || isNaN($(".CountInput", $(this).parent().parent()).val()))
                $(".CountInput", $(this).parent().parent()).val("0");

            $(".CountInput", $(this).parent().parent()).val(Math.floor($(".CountInput", $(this).parent().parent()).val()));
            
            selectedEl += $(this).attr("id") + "~" + $(".CountInput", $(this).parent().parent()).val() + "~~";
        });
        $("#hf").val(selectedEl);

        Sum();
    }
    function Sum() {
        var total = 0;

        $(".TotalSum", "#ProdTable").each(function () {
            var parent = $(this).parent();
            var sum = $(".Price", parent).html() * $(".CountInput", parent).val();  
            $(this).html(sum);

        });


        $(".TotalSum", "#ProdTable").each(function () {
            total += $(this).html() / 1;
        });
        $("#TotalSumID").html(total);
    }
</script>

<table id="ProdTable">
    <tr>
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
        <td>
            Удалить
        </td>
    </tr>
<%  
      foreach (var prodRef in UserOrder.OrdersRefProducts)
      {%>
       <tr>
<td>
<%=prodRef.Product.Name%>
</td>
<td class="Price">
<%=prodRef.Product.Price%>
</td>
<td >
<input type="text" class="CountInput" onkeyup="Update()" value="<%=prodRef.Count%>" />
</td>
<td class="TotalSum">

</td>
<td>
<input type="checkbox" onchange="Update()" id="<%=prodRef.ID%>" /> 
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
