<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestorePassword.aspx.cs" Inherits="iStore.Users.RestorePassword" MasterPageFile="~/Page.Master" Title="Restore password | Marvel Worldwide" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div id="errMsg" runat="server" visibl="false"></div>

<div id="NewPasswordForm" runat="server" visible="false" >
type your new password:<br />
<asp:TextBox runat="server" ID="passwd1"></asp:TextBox><br />
retype your new password:<br />
<asp:TextBox runat="server" ID="passwd2"></asp:TextBox>
<br />

<asp:Button runat="server" ID="RestoreBtn" Text="Restore" OnClick="Restore" />
</div>
</asp:Content>