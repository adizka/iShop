﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddToCard.ascx.cs" Inherits="iStore.Modules.Controls.AddToCard" %>
<div id="<%= this.ClientID%>">
    <div class="cart_item"  >
    <div runat="server" id="CounterContainer"  visible="<%#IsCounterVisible %>">
        <span class="minus_triger" onclick="Decr(this,'<%=hf.ClientID %>')"></span>
        <span class="ProdCount">1</span>
        <span class="plus_triger" onclick="Incr(this,'<%=hf.ClientID %>')"></span>
        </div>
        &nbsp;&nbsp;
        <asp:Button ID="addBtn" CssClass="add_to_cart" Text="" OnClick="AddToCart" runat="server" />
    </div>
  <asp:HiddenField ID="hf" runat="server" Value="1" />
  <asp:HiddenField ID="ProdID" runat="server" />
</div>
