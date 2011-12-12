<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/Admin.Master"  CodeBehind="Default.aspx.cs" Inherits="iStore.Admin.Products.Default" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>
<%@ Register TagPrefix="iS" TagName="Pager" Src="~/Modules/Controls/Pager/Pager.ascx" %>
<%@ Register tagPrefix="iS" TagName="BreadCrumbs" src="~/Modules/Controls/BreadCrumbs/BreadCrumbs.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
    <iS:BreadCrumbs runat="server" ID="bc" SiteMode="false" EntityType="Products" /> 
    Перейти: <asp:DropDownList runat="server" ID="ddlChildCategories" OnSelectedIndexChanged="RedirectToSelectedCategory"></asp:DropDownList>
    <p>
        <a href="<%= iStore.Site.SiteAdminUrl %>Products/ProductEdit.aspx">Добавить продукт</a>
        <a href="">Сортировать продукты в данной категории</a>
    </p>
    <br />
    <iS:ValidateErrors runat="server" ID="ve" Visible="false" />
    <div class="rep">
        <p class="repHeader">
            <span>Name</span>
            <span>Properies</span>
            <span>Edit</span>
            <span>Delete</span>
        </p>
        <% if (!PageProducts.Any())
           { %>
                <p>Нет товаров</p>
           <%} %>
        <% int i = 0; string cssClass = string.Empty;
           foreach (var item in PageProducts)
               {
               i++; cssClass = ((i % 2) == 1 ) ? "first" : "second";
        %>
        <p class="repBody <%= cssClass %>">
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>"><%= item.Product.Name %></a> 
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>Products/ProductPropertyEdit.aspx?pid=<%= item.ProductID.ToString() %>">Properties</a>
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>Products/ProductEdit.aspx?pid=<%= item.ProductID.ToString() %>">Edit</a>
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>">Delete</a>
            </span>
        </p>       
        <% } %>
    </div>
    <iS:Pager runat="server" ID="pager"  AddInsParams="Products"></iS:Pager>
</asp:Content>
