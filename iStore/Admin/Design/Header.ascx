<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="iStore.Admin.Design.Header" %>

<div class="Admin_HeaderCompanyName">
<span><%= iStore.Site.ProjectName %> </span> | <i> <%= global::Resources.Default.AdminPanel %> </i>
</div>
<div class="Admin_HeaderCenter"></div>
<div class="Admin_HeaderToolbar">
    <span class="view_span"><a target="_blank" href="<%= iStore.Site.SiteUrl %>"><%= global::Resources.Default.ViewTheSite %></a></span> 
    <span class="exit_span"><asp:LinkButton runat="server" ID="lnkLogOut" OnClick="LogOut"><%= global::Resources.Default.LogOut %></asp:LinkButton></span>
</div>