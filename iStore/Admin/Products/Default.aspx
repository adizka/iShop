<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/Admin.Master"  CodeBehind="Default.aspx.cs" Inherits="iStore.Admin.Products.Default" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>
<%@ Register TagPrefix="iS" TagName="Pager" Src="~/Admin/Pager.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
    <p>
        <a href="<%= iStore.Site.SiteAdminUrl %>Products/ProductEdit.aspx">Добавить продукт</a>
    </p>
    <br />
    <iS:ValidateErrors runat="server" ID="ve" Visible="false" />
    <div class="rep">
        <p class="repHeader">
            <span>Name</span>
            <span>Properies</span>
            <span>Edit</span>
            <span>Sort</span>
            <span>Delete</span>
        </p>
        <% if (!PageProducts.Any())
           { %>
                <p>Нет категорий</p>
           <%} %>
        <% int i = 0; string cssClass = string.Empty;
           foreach (var item in PageProducts)
               {
               i++; cssClass = ((i % 2) == 1 ) ? "first" : "second";
        %>
        <p class="repBody <%= cssClass %>">
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>"><%= item.Name%></a> 
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>">Properties</a>
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>">Edit</a>
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>">Sort</a>
                &nbsp; <%= item.Name.ToString() %>
            </span>
            <span>
                <a href="<%= iStore.Site.SiteAdminUrl %>">Delete</a>
            </span>
        </p>       
        <% } %>
    </div>

    <iS:Pager runat="server" ID="pager"></iS:Pager>
</asp:Content>
