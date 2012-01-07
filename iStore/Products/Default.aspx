<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" MasterPageFile="~/Page.Master" CodeBehind="Default.aspx.cs" Inherits="iStore.Products.Default" %>

<%@ Register TagPrefix="iS" TagName="AddToCart" Src="~/Modules/Controls/AddToCard.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<script type="text/javascript" src="../Scripts/AddToCart.js"></script>

    <div class="ProducContainer">
        <div>
            <span class="right_pricer"><%=CurrentProduct.Price%>$</span>
            <h2><%=CurrentProduct.Name%></h2>
        </div>

        <div class="prod_diverst">
            <img align="left" src="/Content/Products/Preview/<%=ProductPreviewUrl %>" />
            <%foreach (var item in ProductProperties)
              {
                  %>
                <p>
                  <span class="bold_span"><%=item. PropertyName%>: </span>
                  <span><%=item. PropertyValue%> </span>
                </p>
                  <%
              } %>
              <iS:AddToCart ProductId="<%#CurrentProduct.ProductID %>" ID="CurrProdAddToCart" runat="server" IsCounterVisible="true"  />
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