<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="Default.aspx.cs" Inherits="iStore.Admin.News.Default" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="main">
<br />
    <div class="top_repeater"></div>
        <div class="mid_repeater">
            <p class="cat_h3">News</p>
        </div>
        <div class="mid_repeatergrey">
            <p class="repparaq">
                <span class="cat_span06"><%= global::Resources.Default.Title %></span>
                <span class="cat_span07"><%= global::Resources.Default.CreateDate %></span>
                <span class="cat_span07"><%= global::Resources.Default.Action %></span>
            </p>
        </div>
        <div class="mid_repeater">
       <% foreach (var item in allNews)
          { %>
          <p class="repparaq">
                <span class="cat_span06"><%= item.NewsTitle %></span>
                <span class="cat_span07"><%= item.CreateDate.ToShortDateString() %></span>
                <span class="cat_span07">
                    <a class="view_ico" href="<%= iStore.Site.SiteUrl %>News/?id=<%= item.NewsID.ToString() %>"></a>
                    <% if (item.IsActive)
                     { %>
                    <a href="<%= iStore.Site.SiteAdminUrl %>News/?deactivid=<%= item.NewsID.ToString() %>"></a>
                    <% } else { %>
                    <a href="<%= iStore.Site.SiteAdminUrl %>News/?activid=<%= item.NewsID.ToString() %>"></a>
                    <% } %>
                    <a class="edit_ico" href="<%= iStore.Site.SiteAdminUrl %>News/NewsEdit.aspx?id=<%= item.NewsID.ToString() %>"></a>
                    <a class="delete_ico" href="<%= iStore.Site.SiteAdminUrl %>News/?delid=<%= item.NewsID.ToString() %>"></a>
                </span>
           </p>                    
       <% } %>
        </div>
        <div class="bot_repeater"></div>

</asp:Content>