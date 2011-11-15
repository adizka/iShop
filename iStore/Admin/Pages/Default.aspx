<%@ Page  MasterPageFile="~/Admin/Admin.Master"   Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="iStore.Admin.Pages.Default" %>
<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="main">
    <table>
    <thead>
        <tr>
            <td><%= global::Resources.Default.Name %></td>
            <td><%= global::Resources.Default.Action %></td>
        </tr>
    </thead>
    <tbody>
       <% foreach (var item in allPages)
          { %>
            <tr>
                <td><%= item.PageName %></td>
                <td>
                    <a href="<%= iStore.Site.SiteAdminUrl %>Pages/?delid=<%= item.PageID.ToString() %>"><%= global::Resources.Default.Delete %></a>
                    <a href="<%= iStore.Site.SiteAdminUrl %>Pages/PageEdit.aspx?id=<%= item.PageID.ToString() %>"><%= global::Resources.Default.Edit %></a>
                    <a href="<%= iStore.Site.SiteUrl %>Pages/?name=<%= item.PageName %>"><%= global::Resources.Default.View %></a>
                </td>
            </tr>                          
       <% } %>
    </tbody>
    </table>
</asp:Content>  