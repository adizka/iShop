<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/Admin.Master"  CodeBehind="Default.aspx.cs" Inherits="iStore.Admin.Categories.Default" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>
<%@ Register TagPrefix="iS" TagName="BreadCrumbs" Src="~/Modules/Controls/BreadCrumbs/BreadCrumbs.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
    
    <iS:BreadCrumbs runat="server" ID="bc" SiteMode="false" />

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
        <% if (!allCategories.Any())
           { %>
                <p>Нет категорий</p>
           <%} %>
        <% int i = 0; string cssClass = string.Empty; foreach (var item in allCategories)
           {
               i++; cssClass = ((i % 2) == 1 ) ? "first" : "second";
        %>
        <p class="repBody <%= cssClass %>">
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>Categories/?cid=<%= item.CategoryID.ToString() %>"><%= item.Name%></a> 
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>Categories/Properties.aspx?cid=<%= item.CategoryID.ToString() %>">Properties</a>
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>Categories/CategoryEdit.aspx?cid=<%= item.CategoryID.ToString() %>">Edit</a>
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>Categories/CategorySort.aspx?cid=<%= item.ParentID.ToString() %>">Sort</a>
                &nbsp; <%= item.Sort.ToString() %>
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>Categories/Default.aspx?delcid=<%= item.CategoryID.ToString() %>">Delete</a>
            </span>
        </p>       
        <% } %>
    </div>
</asp:Content>  