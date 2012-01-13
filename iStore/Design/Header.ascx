<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="iStore.Design.Header" %>

<%@Register tagPrefix="iS" tagName="AuthorizationControl" src="AutorizationControl.ascx" %>
<%@Register tagPrefix="iS" tagName="Search" src="~/Search/Search.ascx" %>

<div class="header_linear">
    <div class="Header_Logo">
        <a href="<%= iStore.Site.SiteUrl %>">
            <img src="<%= iStore.Site.SiteUrl %>Images/marvel_logo.png" alt="logo" border="0" />
        </a>
    </div>

    <div class="Header_Items">
        <div class="Header_ItemsTop">
        <span class="Header_CompanyName"><%= iStore.Site.ProjectName.ToUpper() %> Ltd.</span>
        <span class="Header_Links">
            <a href="<%= iStore.Site.SiteUrl %>">HOME</a>
            <a target="_blank" href="<%= iStore.Site.SiteAdminUrl %>Products/">ADMIN LOGIN</a>
        </span>
        <iS:AuthorizationControl runat="server" ID="authorization" />
   </div>
   </div>
   <div class="Header_ItemsBottom">
        <div class="hd_clipter">
            <a href="<%= iStore.Site.SiteUrl %>Pages/?name=About_Us">ABOUT US</a> 
            <a href="<%= iStore.Site.SiteUrl %>NewDeals/">NEW DEALS</a> 
            <a href="<%= iStore.Site.SiteUrl %>Pages/?name=Delivery">DELIVERY</a> 
            <a href="<%= iStore.Site.SiteUrl %>Contacts/">CONTACTS</a>
            <iS:Search runat="server" ID="searchControl" />
        </div>
    </div>
</div>