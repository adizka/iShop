<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormOrder.aspx.cs" Inherits="iStore.Orders.FormOrder"  MasterPageFile="~/Paypal.Master"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.easing-1.3.pack.js"></script>


<script type="text/javascript">
    
    var tax = <%= ConfigurationManager.AppSettings["Tax"]%>;

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

        <%if(IsDataAccepted){ %>
        $(".PayPalAmount", "#ProdTable").each(
        function()
        {
            $(this).val($(this).val().replace(",", "."));
        });
        $(".ShippingRate", "#ProdTable").each(
        function()
        {
            $(this).val($(this).val().replace(",", "."));
        });
        $(".TaxRate", "#ProdTable").each(
        function()
        {
            $(this).val($(this).val().replace(",", "."));
        });
        
        <%} %>
    }
</script>
    <div id="errMsg" runat="server" class="">
    </div>
    
    <h1>Orders</h1>
    <div class="Form">
        <p>
            <label class="more_width">First Name</label>
            <asp:TextBox runat="server" ID="FirstNameTxt"></asp:TextBox>
        </p>
        <p>
            <label class="more_width">Last Name</label>
            <asp:TextBox runat="server" ID="LastNameTxt"></asp:TextBox>
        </p>
        <p>
            <label class="more_width">Address 1</label>
            <asp:TextBox runat="server" TextMode="MultiLine" ID="address1Txt"></asp:TextBox>
        </p>
        <p>
            <label class="more_width">Address 2 (optional)</label>
            <asp:TextBox runat="server" TextMode="MultiLine" ID="address2Txt"></asp:TextBox>
        </p>
        <p>
            <label class="more_width">City</label>
            <asp:TextBox runat="server" ID="cityTxt"></asp:TextBox>
        </p>
        <p>
            <label class="more_width">State/Province/Region</label>
            <asp:TextBox runat="server" ID="provinceTxt"></asp:TextBox>
        </p>
        <p>
            <label class="more_width">Zip code:</label>
            <asp:TextBox runat="server" ID="zipTxt"></asp:TextBox>
        </p>
        <p>
            <label class="more_width">Country</label>
            <asp:DropDownList runat="server" ID="countryDdl"></asp:DropDownList>
        </p>
        <p>
            <label class="more_width">Phone number</label>
            <asp:TextBox runat="server" ID="phoneTxt"></asp:TextBox>
        </p>
        <p>
            <label class="more_width">e-mail</label>
            <asp:TextBox runat="server" ID="emailTxt"></asp:TextBox>
        </p>
    </div>
    <%if (UserOrder.OrdersRefProducts.Count != 0)
      { %>
    <div class="options_div">
        <asp:ImageButton CommandName="Make payments with PayPal - it's fast, free and secure!" CssClass="right_pay" ImageUrl="http://www.paypal.com/en_US/i/btn/x-click-but01.gif" runat="server" OnClick="Pay" Text="Pay" /> 
    </div>
    <%} %>
<h1>Your Shopping Cart</h1>

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
        
        <th style="display:none">
         <%if (IsDataAccepted)
           {
               %>
            <input type="hidden" name="cmd" value="_cart" />
            <input type="hidden" name="upload" value="1" />
            <input type="hidden" name="business" value='<%=ConfigurationManager.AppSettings["Login"] %>' />
            <input type="hidden" name="currency_code" value="US" />
            <input type="hidden" name="custom" value="<%=UserOrder.OrderID.ToString() %>" />
            <input type="hidden" name="address1" value="<%=UserOrder.Address1 %>" />
            <input type="hidden" name="address2" value="<%=UserOrder.Address2 %>" />
            <input type="hidden" name="city" value="<%=UserOrder.City %>" />
            <input type="hidden" name="country" value="<%=UserOrder.Country.ShortName %>" />
            <input type="hidden" name="email" value="<%=UserOrder.email %>" />
            <input type="hidden" name="first_name" value="<%=UserOrder.FirstName %>" />
            <input type="hidden" name="last_name" value="<%=UserOrder.LastName %>" />
            <input type="hidden" name="lc" value="US" />
            <input type="hidden" name="state" value="<%=UserOrder.StateProvinceRegion %>" />
            <input type="hidden" name="zip" value="<%=UserOrder.zipcode %>" />
            <input type="hidden" name="cancel_return" value="<%=iStore.Site.SiteUrl %>Orders/Default.aspx" />
            <input type="hidden" name="return" value="<%=iStore.Site.SiteUrl %>Orders/Thanks.aspx" />
            <%--<input type="hidden" name="no_shipping" value="1" />--%>
            <input type="hidden" name="no_note" value="1" />
            <%} %>
            
        </th>
    </tr>
<%  int counter = 0;
      foreach (var prodRef in UserOrder.OrdersRefProducts)
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
        <td style="display:none">
         <%if (IsDataAccepted)
           {%>
            <input type="hidden" name="item_name_<%=counter.ToString() %>" value="<%=prodRef.Product.Name%>" />
            <input type="hidden" class="PayPalAmount" name="amount_<%=counter.ToString() %>" value="<%=(prodRef.Product.Price).ToString()%>" />
            <input type="hidden" name="quantity_<%=counter.ToString() %>" value="<%=(Math.Min(prodRef.Count, prodRef.Product.Count)).ToString()%>" />
            <input type="hidden" class="TaxRate" name="tax_<%=counter.ToString() %>" value="<%= prodRef.Product.Tax.ToString()%>" />
            <input type="hidden" class="ShippingRate" name="shipping_<%=counter.ToString() %>" value="<%= (Math.Min(prodRef.Count, prodRef.Product.Count) * prodRef.Product.Shipping).ToString()%>" />
            <%} %>
        </td>
    </tr>
       <%
      } %>
    <tr>
        <td colspan="3" class="left_border">Sub Total</td>
        <td class="right_border" align="center" id="TotalSumID"></td>
    </tr>
    </table>
        <script type="text/javascript" >
        Update();
        <%if(IsDataAccepted) {%>
            $("form").attr("action", '<%=ConfigurationManager.AppSettings["PayPalPaymentUrlTest"] %>');
            $("form").submit();
            <%}%>
    </script> 
   <br />
</asp:Content>