<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Page.Master" CodeBehind="ForgotPassword.aspx.cs" Inherits="iStore.Users.ForgotPassword" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>    
<asp:UpdatePanel runat="server" ID="up">
<ContentTemplate>
   <p>
        I forgot my password
   </p>
   <p>
        <asp:Label runat="server" ID="lblLogin">Username or email</asp:Label> 
        <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox> 
        <asp:Label runat="server" ID="lblEmailError"></asp:Label>
   </p>
   <p>
        <asp:Button runat="server" ID="btnGo" OnClick="Go" Text="Go" />
   </p>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>