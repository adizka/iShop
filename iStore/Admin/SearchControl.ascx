<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchControl.ascx.cs" Inherits="iStore.Admin.SearchControl" %>

<div class="InputConteiner">
<span>
Название:
<asp:TextBox runat="server" ID="CriteriaTxt"></asp:TextBox>
<br />
Цена от:
<asp:TextBox runat="server" ID="PriceTxtFrom"></asp:TextBox>
<br />
Цена до:
<asp:TextBox runat="server" ID="PriceTxtTo"></asp:TextBox>
</span>
<span>
<br />
<asp:Button runat="server" ID="SearchBtn" Text="Search" OnClick="SearchProduct" /></span>
</div>