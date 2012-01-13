<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddToCard.ascx.cs" Inherits="iStore.Modules.Controls.AddToCard" %>

<div id="<%= GlobalID%>">
    <div class="cart_item"  >
    <div runat="server" id="CounterContainer"  visible="<%#IsCounterVisible %>">
        <span class="minus_triger" id="dec" onclick="Decr('<%=GlobalID %>','<%=hf.ClientID %>')"></span>
        <span class="ProdCount" id="cCont">1</span>
        <span class="plus_triger" id="inc" onclick="Incr('<%=GlobalID %>','<%=hf.ClientID %>')"></span>
    </div>
        <asp:Button ID="addBtn" CssClass="add_to_cart" Text="" OnClick="AddToCart" runat="server" />
    </div>
  <asp:HiddenField ID="hf" runat="server" Value="1" />
  <asp:HiddenField ID="ProdID" runat="server" />

</div>


