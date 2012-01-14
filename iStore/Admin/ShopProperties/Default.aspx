<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="iStore.Admin.ShopProperties.Default"  MasterPageFile="~/Admin/Admin.Master" Title="Shop | Marvel Worldwide" %>
<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="main">
    <p class="BCCategories">
    <a href="PropertyEdit.aspx">Create shop property</a>
</p>
<%foreach (var item in PageProperties)
  {
      %>
      <div class="ProductPropertyItem">
                <a class="blue_anchored" href="PropertyEdit.aspx?pid=<%=item.ID %>"> <%=item.Key %></a>
      </div>
      <%
  } %>
<p class="BCCategories">
    <span class="universal_button">
        <span>
            <a href="PropertyEdit.aspx">Create new shop properties</a>
        </span>
    </span>
</p>
</asp:Content>
