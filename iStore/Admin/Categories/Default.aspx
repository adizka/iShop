<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/Admin.Master"  CodeBehind="Default.aspx.cs" Inherits="iStore.Admin.Categories.Default" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
    <p>
        <a href="<%= iStore.Site.SiteAdminUrl %>Categories/CategoryEdit.aspx">Добавить категорию</a>
    </p><br />
    <iS:ValidateErrors runat="server" ID="ve" Visible="false" />
    <div class="rep">
        <p class="repHeader">
            <span>Name</span>
            <span>Properies</span>
            <span>Edit</span>
            <span>Sort</span>
            <span>Delete</span>
        </p>
        <% int i = 0; string cssClass = string.Empty; foreach (var item in allCategories)
           {
               i++; cssClass = ((i % 2) == 1 ) ? "first" : "second";
        %>
        <p class="repBody <%= cssClass %>">
            <span>
                <%= item.Name %>
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>Categories/Properties.aspx?id=<%= item.ID.ToString() %>">Properties</a>
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>Categories/CategoryEdit.aspx?id=<%= item.ID.ToString() %>">Edit</a>
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>Categories/CategorySort.aspx?id=<%= item.ID.ToString() %>">Sort</a>
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>Categories/Default.aspx?delid=<%= item.ID.ToString() %>">Delete</a>
            </span>
        </p>       
        <% } %>
    </div>
</asp:Content>  