<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Page.Master" CodeBehind="Default.aspx.cs" Inherits="iStore.Products.Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<script type="text/javascript">
    
    function Incr(el) {

        var count = $($(el).parent().children()[1]).html() / 1;

        if (count == 1)
            $($(el).parent().children()[0]).css("cursor", "pointer")

        $($(el).parent().children()[1]).html(count + 1);
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
    }
    function Add(pid) {
        var count = 1;
        if ('<%=CurrentProduct.ProductID %>' == pid)
            count = $(".ProdCount").html();

        $("#hf").val(count + ";" + pid);
        
        $("#btnAdd").click();
    }

</script>

<div class="ProducContainer">
<div>
<span>
<%=CurrentProduct.Name%>
</span>
<span><%=CurrentProduct.Price%>$</span>
</div>
<div style="float:left">
<span>
<img src="/Content/Products/Preview/<%=ProductPreviewUrl %>" />
</span>
</div>
<div style="float:left">
<%foreach (var item in ProductProperties)
  {
      %>

      <span>
      <%=item. PropertyName%>:
      </span>
      <span>
      <%=item. PropertyValue%>
      </span>
      <br />

      <%
  } %>
  <b>Number Of items</b> <div><span style="cursor:pointer;color:Blue;" onclick="Decr(this)">-</span><span class="ProdCount">1</span><span style="cursor:pointer;color:Blue;" onclick="Incr(this)">+</span> 
  <input type="button" value="ADD TO CART" onclick="Add('<%=CurrentProduct.ProductID %>')" />
  </div>
  <asp:Button  OnClick="AddToCart" ID="btnAdd" runat="server" Text="ADD TO CART" ClientIDMode="Static" style="display:none" />
  <asp:HiddenField ID="hf" runat="server" Value="1" ClientIDMode="Static" ></asp:HiddenField>
</div>
<br />
<div style="float:left;width:100%">
<p><b>Description</b></p><br />
<%= ProductDescription %>
</div>
</div>
<div>
<p><b>Related Products</b></p>
<% foreach (var item in RelatedProducts)
   {
       %>

       <span>
            <img src="/Content/Products/Preview/<%=GetPreviewUrl(item) %>" />
            <br />
       <a href="/Products/?pid=<%=item.ProductID %>">     <%=item.Name %></a>
       <input type="button" value="ADD TO CART" onclick="Add('<%=item.ProductID %>')" />
       </span>
       <%
   } %>

</div>
</asp:Content>