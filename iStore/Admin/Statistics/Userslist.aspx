<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Userslist.aspx.cs" Inherits="iStore.Admin.Statistics.Userslist" MasterPageFile="~/Admin/Admin.Master" Title="Users list | Marvel Worldwide" %>
<%@ Register TagPrefix="iS" TagName="Pager" Src="~/Modules/Controls/Pager/Pager.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">

<div class="pader_category">
<h1>Login e-mail Status</h1>

    <%foreach (var item in PageUsers)
      {
    %>
    <p class="classed_userlist">
        <span class="spaned_user"><a href="<%=iStore.Site.SiteAdminUrl%>Statistics/UserOrdersHistory.aspx?uid=<%=item.UserID.ToString() %>"><%=item.Login %></a> </span>
        <span class="spaned_user"><%=item.Email %>  <%=item.IsActive?"active":"not active" %> </span>
    </p>
    <%  } %>
    <iS:Pager runat="server" ID="pager"></iS:Pager>
</div>
</asp:Content>
