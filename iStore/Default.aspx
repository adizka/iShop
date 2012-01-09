<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="iStore._Default" EnableEventValidation="false" %>

<%@Register TagPrefix="iS" TagName="AddToCart" Src="~/Modules/Controls/AddToCard.ascx" %>
<%@Register TagPrefix="iS" TagName="NewDeals" Src="~/Modules/Controls/NewProducts/NewProducts.ascx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <h1>New Deals</h1>    
    </div>
    <div class="last_goods">
    <iS:NewDeals runat="server" RowsCount="1" ColumnsCount="4" />
    </div>
</asp:Content>