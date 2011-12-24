<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" MasterPageFile="~/Page.Master" CodeBehind="Default.aspx.cs" Inherits="iStore.Products.Default" %>

<%@ Register TagPrefix="iS" TagName="AddToCart" Src="~/Modules/Controls/AddToCard.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<script type="text/javascript" src="../Scripts/AddToCart.js"></script>

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
  <iS:AddToCart ProductId="<%#CurrentProduct.ProductID %>" ID="CurrProdAddToCart" runat="server" IsCounterVisible="true"  />
</div>
<br />
<div style="float:left;width:100%">
<p><b>Description</b></p><br />
<%= ProductDescription %>
</div>
</div>
<div>
<p><b>Related Products</b></p>
<div runat="server" id="RelatedProductsContainer">

</div>

<%counter=0; %>
<asp:Repeater runat="server" id="rpt" >
    <ItemTemplate> 
            <span style="float:left;">
        <img src="/Content/Products/Preview/<%=GetPreviewUrl() %>" />
        <br />
        <a href='/Products/?pid=<%#Eval("ProductID")%>'>
        <%#Eval("Name")%>
        </a>
        <iS:AddToCart ProductId='<%#Eval("ProductID") %>' runat="server" IsCounterVisible="false"  />
        <%counter++; %>
    </span>
    </ItemTemplate > 
</asp:Repeater>

</div>
</asp:Content>