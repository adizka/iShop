<%@ Page Language="C#" AutoEventWireup="true" Title="New Deals" CodeBehind="Default.aspx.cs" Inherits="iStore.NewDeals.Default" MasterPageFile="~/Page.Master" %>

<%@ Register tagPrefix="iS" tagName="AddToCard" src="~/Modules/Controls/AddToCard.ascx" %>
<%@ Register TagPrefix="iS" TagName="Pager" Src="~/Modules/Controls/Pager/Pager.ascx" %>
<%@ Register TagPrefix="iS" TagName="BreadCrumbs" Src="~/Modules/Controls/BreadCrumbs/BreadCrumbs.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<script type="text/javascript">
    function addTocart(prodID, count) {
        var hds = $("[type=hidden]", "#<%=addtoCart.GlobalID %>");
        $(hds[0]).val(count);
        $(hds[1]).val(prodID);
        $("[type=submit]", "#<%=addtoCart.GlobalID %>").click();
    }
</script>
<iS:BreadCrumbs runat="server" ID="bc" SiteMode="true" EntityType="Categories" />

<br />
    <% foreach (var productRefCat in GetPageProductsRefCurrentCategory)
       { %>
       <div class="ProductsList">
                    <div class="image_ploater">
                        <img src="<%= iStore.Site.PreUrlProductPreviewImage %><%= GetProductPreview(productRefCat.ProductID) %>" alt="<%= productRefCat.Product.Name %>" />
                    </div>
                    <div class="info_blocked">
                        <div class="ProductName">
                            <a href="<%= iStore.Site.SiteUrl %>Products/?pid=<%= productRefCat.ProductID.ToString() %>"><%= productRefCat.Product.Name %></a>
                        </div>
                        <div class="ProductInfo">
                        <% foreach (var property in GetProductPropery(productRefCat.ProductID))
                        { %>
                            <p>
                                <span class="prod_span bold_span"><%= property.PropertyName %>: </span>
                                <span class="prod_span"><%= property.PropertyValue %> </span>
                            </p>
                        <% } %>
                        </div>
                    </div>
                   <div class="price_blocked">
                       <p class="price_paraq">Our Price:</p> 
                       <p class="price_paraq2"> $<%= productRefCat.Product.Price.ToString() %></p>
                       <%=GetRenderedControl(productRefCat)%>
                   </div>
        </div>
    <% } %>
    <div style="display: none">
        <iS:AddToCard ID="addtoCart" IsCounterVisible="false" runat="server" />
    </div>

    <iS:Pager runat="server" ID="pager"></iS:Pager>
    <br />
</asp:Content>
