<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" MasterPageFile="~/Page.Master" CodeBehind="Default.aspx.cs" Inherits="iStore.Products.Default" %>
<%@ Register TagPrefix="iS" TagName="AddToCart" Src="~/Modules/Controls/AddToCard.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <script src="../Scripts/fancybox/jquery.mousewheel-3.0.4.pack.js" type="text/javascript"></script>
    <script src="../Scripts/fancybox/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <link href="../Scripts/fancybox/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("a[rel=example_group]").fancybox({
                'transitionIn': 'none',
                'transitionOut': 'none',
                'titlePosition': 'over'
            });
        });
</script>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<script type="text/javascript" src="../Scripts/AddToCart.js"></script>
<div class="ProducContainer">
    <div>
        <span class="right_pricer"><%=CurrentProduct.Price%>$</span>
        <h2><%=CurrentProduct.Name%></h2>
    </div>
    <div class="prod_diverst">
        <div class="photo_field">
            <img src="/Content/Products/Preview/<%=ProductPreviewUrl %>" />
            <p class="center_viewer">
                <a rel="example_group" href="<%=ProductOriginalPhoto(string.Empty) %>" title="Image 1">View photo</a>
            </p>
        </div>
        <div class="properties_field">
        <% string original = ProductOriginalPhoto("2"); if (original != string.Empty)
            {  %>
                <a rel="example_group" href="<%= original %>" title="Image 2" style="display:none">2</a>
            <% } %>
            <% string original2 = ProductOriginalPhoto("3"); if (original2 != string.Empty)
            {  %>
		        <a rel="example_group" href="<%= original2 %>" title="Image 3" style="display:none">3</a>
            <% } %>
            <%foreach (var item in ProductProperties)
            {  %>
                <p>
                    <span class="prod_span bold_span"><%=item. PropertyName%>: </span>
                    <span class="prod_span"><%=item. PropertyValue%> </span>
                </p>
            <%
            } %>
            <iS:AddToCart ProductId="<%#CurrentProduct.ProductID %>" ID="CurrProdAddToCart" runat="server" IsCounterVisible="true"  />
        </div>
    </div>
    <div class="desriptiones"> 
        <p class="prod_descrip">Product Description</p>
        <%= ProductDescription %>
    </div>
    <div class="realte_pro"> 
        <p class="prod_descrip">Related Products</p>
        <div runat="server" id="RelatedProductsContainer"></div>
        <%counter=0; %>
        <asp:Repeater runat="server" id="rpt" >
        <ItemTemplate> 
            <span class="goods_linear">
                <img src="/Content/Products/Preview/<%=GetPreviewUrl() %>" /> <br />
                <a href='/Products/?pid=<%#Eval("ProductID")%>'><%#Eval("Name")%></a>
                <iS:AddToCart ProductId='<%#Eval("ProductID") %>' runat="server" IsCounterVisible="false"  />
                <%counter++; %>
            </span>
        </ItemTemplate > 
        </asp:Repeater>
    </div>
</div>
</asp:Content>
