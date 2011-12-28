<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Page.Master" CodeBehind="Default.aspx.cs"  Inherits="iStore.Pages.Default" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>
        <%= CurrentPageName %>
    </h1>
    <div>
        <%= CurrentPage.PageBody %>
    </div>
</asp:Content>