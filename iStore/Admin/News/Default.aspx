<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="Default.aspx.cs" Inherits="iStore.Admin.News.Default" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="main">
<table>
    <thead>
        <tr>
            <td><%= global::Resources.Default.Title %></td>
            <td><%= global::Resources.Default.CreateDate %></td>
            <td><%= global::Resources.Default.Action %></td>
        </tr>
    </thead>
    <tbody>
       <% foreach (var item in allNews)
          { %>
            <tr>
                <td><%= item.NewsTitle %></td>
                <td><%= item.CreateDate.ToShortDateString() %></td>
                <td>
                    <a href="<%= iStore.Site.SiteAdminUrl %>News/?delid=<%= item.NewsID.ToString() %>"><%= global::Resources.Default.Delete %></a>
                    <a href="<%= iStore.Site.SiteAdminUrl %>News/NewsEdit.aspx?id=<%= item.NewsID.ToString() %>"><%= global::Resources.Default.Edit %></a>
                    <a href="<%= iStore.Site.SiteUrl %>News/?id=<%= item.NewsID.ToString() %>"><%= global::Resources.Default.View %></a>
                    <% if (item.IsActive)
                       { %>
                    <a href="<%= iStore.Site.SiteAdminUrl %>News/?deactivid=<%= item.NewsID.ToString() %>"><%= global::Resources.Default.Deactivate %></a>
                    <% } else { %>
                    <a href="<%= iStore.Site.SiteAdminUrl %>News/?activid=<%= item.NewsID.ToString() %>"><%= global::Resources.Default.Activate %></a>
                    <% } %>
                </td>
            </tr>                          
       <% } %>
    </tbody>
</table>
</asp:Content>