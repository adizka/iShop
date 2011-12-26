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
        %>
        <div style="width:100%;float:left;">
        <div style="float:left;">
        <img src="/Content/Products/Preview/<%=GetPreviewUrl(item) %>" />
        </div>
        <div style="float:left;">
        <a href='/Products/?pid=<%=item.ProductID%>'>
        <%=item.Name %></a>
        <br />
        </div>
        <div style="float:right;">
        Our price:
        <br /> 
        <%=item.Price %>$
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