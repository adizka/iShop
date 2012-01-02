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
                <p>Нет товаров</p>
           <%} %>
        <% int i = 0; string cssClass = string.Empty;
           foreach (var item in PageProducts)
               {
                   var prod = item.Value.First();
        %>
        <div style="width:100%;float:left;">
        <div style="float:left;">
        <img src="/Content/Products/Preview/<%=GetPreviewUrl(prod) %>" />
        </div>
        <div style="float:left;">
        <a href='/Products/?pid=<%=item.Key%>'>
        <%=prod.Name%></a>
        Categories: (
        <%foreach (var cats in item.Value)
          {
              %>
              <a href="/Categories/?cid=<%=cats.CategoryID%>"><%= cats.CategoryName%></a> 
              <%
          } %>)
        <br />
        </div>
        <div style="float:right;">
        Our price:
        <br /> 
        <%=prod.Price%>$
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