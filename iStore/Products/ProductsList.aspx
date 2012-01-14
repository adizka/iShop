<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductsList.aspx.cs" Inherits="iStore.Products.ProductsList" MasterPageFile="~/Page.Master" Title="Product list | Marvel Worldwide" %>

<%@ Register TagPrefix="iS" TagName="Pager" Src="~/Modules/Controls/Pager/Pager.ascx" %>
<%@ Register TagPrefix="iS" TagName="AddToCart" Src="~/Modules/Controls/AddToCard.ascx" %>
<%@ Register TagPrefix="iS" TagName="BreadCrumbs" Src="~/Modules/Controls/BreadCrumbs/BreadCrumbs.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<script type="text/javascript">
    function addTocart(prodID, count) {
        var hds = $("[type=hidden]", "#<%=addtoCart.ClientID %>");
        $(hds[0]).val(count);
        $(hds[1]).val(prodID);
        $("[type=submit]", "#<%=addtoCart.ClientID %>").click();
    }
</script>

<iS:BreadCrumbs runat="server" ID="bc" SiteMode="true" EntityType="Products/ProductsList.aspx" />
<div class="rep">

        <% if (!PageProducts.Any())
           { %>
                <p>Нет товаров</p>
           <%} %>
        <% int i = 0; string cssClass = string.Empty;
           foreach (var item in PageProducts)
               {
        %>
        <div style="width:100%;float:left;">
        <div style="float:left;">
        <img src="/Content/Products/Preview/<%=GetPreviewUrl(item.Product) %>" />
        </div>
        <div style="float:left;">
        <a href='/Products/?pid=<%=item.Product.ProductID%>'>
        <%=item.Product.Name %></a>
        <br />
        <%foreach (var itemProp in item.Product.ProductProperties.Where(p => p.PropertyName != BL.ProductPropertyConstants.ProductDescription
                            && p.PropertyName != BL.ProductPropertyConstants.ProductPhotoOriginal
                            && p.PropertyName != BL.ProductPropertyConstants.ProductPhotoPreview).OrderBy(s=>s.Sort).Take(4))
          {
              %>
              <div style="vertical-align:top;">
              <%= itemProp.PropertyValue + ":" + itemProp.PropertyValue %>
              </div>
              <br />

              <%
              
          } %>
        </div>
        <div style="float:right;">
        Our price:
        <br /> 
        <%=item.Product.Price %>$
        <%=GetRenderedControl(item)%>
        </div>
        </div>
        <br />
        <% } %>
    </div>
    <div style="display:none">
    <iS:AddToCart ID="addtoCart" IsCounterVisible="false" runat="server" />
    </div>

<iS:Pager runat="server" ID="pager"></iS:Pager>
</asp:Content>