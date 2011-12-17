<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" MasterPageFile="~/Site.Master" Inherits="iStore.Pages.Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%if (Page != null)
      { %>
    <div class="PageContent">
        <%=Page.PageBody%>
    </div>
    <div class="PageKeyWords">
        <%foreach (var item in Page.Keywords.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
          {
              %>
              <a><%=item%></a>
              <%

          } %>
    </div>
    <%}
      else
      {%>
      <b> Такая страница отсутствует на сайте.</b>
    <%} %>
</asp:Content>