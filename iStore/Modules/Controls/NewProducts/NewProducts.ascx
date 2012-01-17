<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewProducts.ascx.cs" Inherits="iStore.Modules.Controls.NewProducts.NewProducts" %>

<%@ Register TagPrefix="iS" TagName="AddToCart" Src="~/Modules/Controls/AddToCard.ascx" %>
<script type="text/javascript">
    function addTocart(prodID, count) {
        var hds = $("[type=hidden]", "#<%=addtoCart.GlobalID %>");
        $(hds[0]).val(count);
        $(hds[1]).val(prodID);
        $("[type=submit]", "#<%=addtoCart.GlobalID %>").click();
    }
</script>
<div class="rep">
    <%
        int counter = 1;
        foreach (var item in NewestProducts)
        {
    %>
    <span class="goods_linear">
        <img src="/Content/Products/Preview/<%=GetPreviewUrl(item.Product) %>" />
        <br />
        <a href='/Products/?pid=<%= item.Product.ProductID%>'>
            <%= item.Product.Name%></a>
        <%=GetRenderedControl(item)%>
        </span>
    <%if (counter % ColumnsCount == 0)
      {%>
    <br />
    <%} counter++;
       } %>
    </div>
    <div style="display:none">
    <iS:AddToCart ID="addtoCart" IsCounterVisible="false" runat="server" />
    </div>