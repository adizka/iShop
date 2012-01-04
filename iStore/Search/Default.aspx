<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="iStore.Search.Default" MasterPageFile="~/Page.Master" %>

<%@ Register TagPrefix="iS" TagName="Pager" Src="~/Modules/Controls/Pager/Pager.ascx" %>
<%@ Register TagPrefix="iS" TagName="AddToCart" Src="~/Modules/Controls/AddToCard.ascx" %>
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

<div class="rep">

        <% if (!PageProducts.Any())
           { %>
                <p class="pad_nogoods">Not found, try again.</p>
           <%} %>
        <% int i = 0; string cssClass = string.Empty;
           foreach (var item in PageProducts)
               {
                   var prod = item.Value.First();
        %>
<div class="ProductsList">
    <div class="image_ploater">
        <img src="/Content/Products/Preview/<%=GetPreviewUrl(prod) %>" />
    </div>
    <div class="info_blocked">
        <div class="ProductName">
            <a href='/Products/?pid=<%=item.Key%>'><%=prod.Name%></a>
        </div>
        <div class="ProductInfo">
            Categories: (
            <%foreach (var cats in item.Value)
              {
                  %>
                  <a href="/Categories/?cid=<%=cats.CategoryID%>"><%= cats.CategoryName%></a> 
                  <%
              } %>)
        </div>
    </div>
    <div class="price_blocked">
        <p class="price_paraq">Our price:</p>
        <p class="price_paraq2"> <%=prod.Price%>$ </p>
        <%=GetRenderedControl(prod)%>
    </div>
</div>
        <br />
        <% } %>

        <% if(PageProducts.Count ==0)
           {
               %>
               Categories: <br />
               <ul>
               <%
               foreach (var item in Categories)
               {
                %>
                
                <li>
                <a href="/Categories/?cid=<%= item.CategoryID %>">
                <%= item.Name %>
                     </a>
                </li>
        
        <%}
          %>
          </ul>
          <%
           }%>
    </div>
    <div style="display:none">
    <iS:AddToCart ID="addtoCart" IsCounterVisible="false" runat="server" />
    </div>

<iS:Pager runat="server" ID="pager"></iS:Pager>
</asp:Content>