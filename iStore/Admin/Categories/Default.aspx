<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/Admin.Master"  CodeBehind="Default.aspx.cs" Inherits="iStore.Admin.Categories.Default" Title="Categories | Marvel Worldwide" %>

<%@ Register TagPrefix="iS" TagName="BreadCrumbs" Src="~/Modules/Controls/BreadCrumbs/BreadCrumbs.ascx" %>
<%@ Register TagPrefix="iS" TagName="Pager" Src="~/Modules/Controls/Pager/Pager.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">

    <iS:BreadCrumbs runat="server" ID="bc" SiteMode="false" EntityType="Categories" />
    <p class="BCCategories">
        <a href="<%= iStore.Site.SiteAdminUrl %>Categories/CategoryEdit.aspx?parentId=<%= Request.QueryString["cid"] %>">Add category</a>
        <a href="<%= iStore.Site.SiteAdminUrl %>Categories/CategorySort.aspx?cid=<%= Request.QueryString["cid"] %>">Sort categories</a>
        <a href="<%= iStore.Site.SiteAdminUrl %>Products/ProductEdit.aspx?cid=<%= Request.QueryString["cid"] %>">Add product to category</a>
    </p>
    <br />

    <div class="Admin_LoginErrors" runat="server" id="divError" visible="false"></div>
<div class="pader_category">
    <div class="top_repeater"></div>
    <div class="mid_repeater">
        <p class="cat_h3">Categories</p>
    </div>
    <div class="mid_repeatergrey">
        <p class="repparaq">
            <span class="cat_span01">Name</span>
            <span class="cat_span02">Subcategory</span>
            <span class="cat_span03">Number of products</span>
            <span class="cat_span04">Actions</span>
        </p>
    </div>
    <div class="mid_repeater">
        <% if (!allCategories.Any())
           { %>
                <p class="repparaq">No categories</p>
           <%} %>
        <% int i = 0; string cssClass = string.Empty; foreach (var item in allCategories.Skip(pager.PageIndex * pager.EntitiesPerPage).Take(pager.EntitiesPerPage))
               {
                    i++; cssClass = ((i % 2) == 1 ) ? "first" : "second";
        %>
        <p class="repparaq <%= cssClass %>">
            <span class="cat_span01">
                <a href="<%= iStore.Site.SiteAdminUrl %>Categories/?cid=<%= item.CategoryID.ToString() %>"><%= item.Name%></a> 
            </span>
            <span class="cat_span02">
                <a href="<%= iStore.Site.SiteAdminUrl %>Categories/?cid=<%= item.CategoryID.ToString() %>"><%= cbl.GetCategoriesByParentId(item.CategoryID).Count().ToString()  %></a> 
            </span>
            <span class="cat_span03">
                <%= prcbl.GetProductRefCategoriesByCategoryId(item.CategoryID).Count().ToString() %>
            </span>
            <span class="cat_span04">
                <a class="edit_ico" href="<%= iStore.Site.SiteAdminUrl %>Categories/CategoryEdit.aspx?cid=<%= item.CategoryID.ToString() %>&parentId=<%= Request.QueryString["cid"] %>"></a>
                <a class="delete_ico" href="<%= iStore.Site.SiteAdminUrl %>Categories/Default.aspx?delcid=<%= item.CategoryID.ToString() %>"></a>
            </span>
        </p>       
        <% } %>
        </div>
    <div class="bot_repeater"></div>
</div>
    <iS:Pager runat="server" ID="pager"></iS:Pager>
</asp:Content>  