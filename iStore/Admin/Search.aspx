<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="iStore.Admin.Search" MasterPageFile="~/Admin/Admin.Master" Title="Search | Marvel Worldwide" %>

<%@ Register TagPrefix="iS" TagName="Pager" Src="~/Modules/Controls/Pager/Pager.ascx" %>
<%@ Register TagPrefix="iS" TagName="SearchControl" Src="~/Admin/SearchControl.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
 
 <div style="float:left;width:100%;">
 <iS:SearchControl runat="server" ID="SearchControl" ></iS:SearchControl>
 </div>
 <br />
 <div style="float: left;">
     
     <% if (!Products.Any() && !string.IsNullOrEmpty(Key))
        { %>
     <p>
         Ничего не найдено по вашему запросу</p>
     <%}
        else
        { 
        %>
        <p>
        Search Result
        </p>
        <%
        } %>
     <% int i = 0; string cssClass = string.Empty;
        foreach (var item in Products.Skip(pager.PageIndex * pager.EntitiesPerPage).Take(pager.EntitiesPerPage))
        {
            i++; cssClass = ((i % 2) == 1) ? "first" : "second";
     %>
     <p class="repBody <%= cssClass %>">
         <span>
         <a href="<%= iStore.Site.SiteAdminUrl %>Products/ProductEdit.aspx?pid=<%=item.ProductID %>">
             <%= item.Name%>
             </a>
             
         </span>
     </p>
     <% } %>
     <iS:Pager runat="server" ID="pager"></iS:Pager>
 </div>
</asp:Content>
