<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="iStore.Admin.Design.Header" %>

<div class="Admin_HeaderCompanyName">
<%= iStore.Site.ProjectName %> <i> <%= global::Resources.Default.AdminPanel %> </i>
</div>
<div class="Admin_HeaderCenter"></div>
<div class="Admin_HeaderToolbar">
    <span><a href="<%= iStore.Site.SiteUrl %>"><%= global::Resources.Default.ViewTheSite %></a></span> | 
    <span><asp:LinkButton runat="server" ID="lnkLogOut" OnClick="LogOut"><%= global::Resources.Default.LogOut %></asp:LinkButton></span>
</div>