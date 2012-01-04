<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="iStore.Admin.ShopProperties.Default"  MasterPageFile="~/Admin/Admin.Master"%>


<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="main">
<p>
<b><a href="PropertyEdit.aspx">Создать свойство магазина</a> </b>
</p>
<%foreach (var item in PageProperties)
  {
      
      %>
      <div>
           <a href="PropertyEdit.aspx?pid=<%=item.ID %>"> <%=item.Key %></a>
      </div>
      <br />
          <%
      
  } %>
  
</asp:Content>