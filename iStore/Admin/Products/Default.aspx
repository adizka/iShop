<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/Admin.Master"  CodeBehind="Default.aspx.cs" Inherits="iStore.Admin.Products.Default" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
    <p>
        <a href="<%= iStore.Site.SiteAdminUrl %>Products/ProductEdit.aspx">Добавить продукт</a>
    </p>

</asp:Content>
