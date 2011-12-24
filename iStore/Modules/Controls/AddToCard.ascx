<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddToCard.ascx.cs" Inherits="iStore.Modules.Controls.AddToCard" %>

    
    <div>
    <div id="CounterContainer" runat="server" visible="<%#IsCounterVisible %>" >
    <b>Number Of items</b> 
    <span style="cursor:pointer;color:Blue;" onclick="Decr(this,'<%=hf.ClientID %>')">-</span>
    <span class="ProdCount">1</span>
    <span style="cursor:pointer;color:Blue;" onclick="Incr(this,'<%=hf.ClientID %>')">+</span> 
    </div>
  <asp:Button  Text="ADD TO CART" OnClick="AddToCart" runat="server" />
  <asp:HiddenField ID="hf" runat="server" />
  <asp:HiddenField ID="ProdID" runat="server" />
  </div>
