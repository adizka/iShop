<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainPageHeader.ascx.cs" Inherits="iStore.Design.MainPageHeader" %>

<%@ Register tagPrefix="iS" tagName="Menu" src="Menu2.ascx" %>
<%@ Register tagPrefix="iS" tagName="MostPopularProducts" src="MostPopularProducts.ascx" %>

    <div class="MainPageHeader">
        <div class="MainPageHeader_Left">
            <h1>Categories</h1>
            <iS:Menu runat="server" ID="menu" />
        </div>
        <div class="MainPageHeader_Right">
            <iS:MostPopularProducts runat="server" ID="mostPopularProducts" />
        </div>
    </div>