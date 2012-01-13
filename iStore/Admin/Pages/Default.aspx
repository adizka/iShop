<%@ Page  MasterPageFile="~/Admin/Admin.Master"   Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="iStore.Admin.Pages.Default" %>
<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="main">
<br />
<div class="pader_category">
    <div class="top_repeater"></div>
        <div class="mid_repeater">
            <p class="cat_h3">Page</p>
        </div>
        <div class="mid_repeatergrey">
            <p class="repparaq">
                <span class="cat_span05"><%= global::Resources.Default.Name %></span>
                <span class="cat_span04">View page</span>
                <span class="cat_span04"><%= global::Resources.Default.Action %></span>
            </p>
        </div>
        <div class="mid_repeater">
        <% foreach (var item in allPages)
          { %>
            <p class="repparaq">
                <span class="cat_span05"><%= item.PageName %></span>
                <span class="cat_span04"><a class="view_ico" href="<%= iStore.Site.SiteUrl %>Pages/?name=<%= item.PageName %>"></a></span>
                <span class="cat_span04">
                    <a class="edit_ico" href="<%= iStore.Site.SiteAdminUrl %>Pages/PageEdit.aspx?id=<%= item.PageID.ToString() %>"></a>
                    <a class="delete_ico" href="<%= iStore.Site.SiteAdminUrl %>Pages/?delid=<%= item.PageID.ToString() %>"></a>
                </span>
            </p>
        <% } %>
        </div>
        <div class="bot_repeater"></div>
</div>

</asp:Content>
  
