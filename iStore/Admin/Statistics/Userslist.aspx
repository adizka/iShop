<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Userslist.aspx.cs" Inherits="iStore.Admin.Statistics.Userslist" MasterPageFile="~/Admin/Admin.Master" %>
<%@ Register TagPrefix="iS" TagName="Pager" Src="~/Modules/Controls/Pager/Pager.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">


<p>Login e-mail Status</p>
    <%foreach (var item in PageUsers)
      {
    %>
    <p><a href="<%=iStore.Site.SiteAdminUrl%>Statistics/UserOrdersHistory.aspx?uid=<%=item.UserID.ToString() %>"><%=item.Login %></a> <%=item.Email %>  <%=item.IsActive?"active":"not active" %></p>
    <%  } %>
    <iS:Pager runat="server" ID="pager"></iS:Pager>
</asp:Content>