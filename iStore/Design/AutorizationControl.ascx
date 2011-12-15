<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutorizationControl.ascx.cs" Inherits="iStore.Design.AutorizationControl" %>

<script type="text/javascript" src="<%= iStore.Site.SiteUrl %>Scripts/flowplayerOverlay/jquery.tools.min.js"></script>
<script type="text/javascript">
      $(function () {
          $("button[rel]").overlay({ mask: '#777', effect: 'apple' });
      });
</script>

<% if (CurrentUser == null) { %>

NOT LOGGED YET
<button class="Header_LoginLink" rel="#Header_Login" type="button">sign in</button> or 
<a href="<%= iStore.Site.SiteUrl %>Users/Register.aspx">register</a>

<div class="Header_Login apple_overlay" id="Header_Login">
    <span class="Title">Вход</span>
    <p>
        <asp:Label runat="server" ID="lblLogin" AssociatedControlID="txtLogin">Логин или E-mail</asp:Label><br />
        <asp:TextBox runat="server" ID="txtLogin"></asp:TextBox>
    </p>
    <p>
        <asp:Label runat="server" ID="lblPassword" AssociatedControlID="txtPassword">Пароль</asp:Label><br />
        <asp:TextBox runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox>
    </p>
    <p>
        <asp:Label runat="server" ID="lblSaveMe" AssociatedControlID="chbSaveMe">Запомнить меня</asp:Label> 
        <asp:CheckBox runat="server" ID="chbSaveMe" Checked="true" />
    </p>
    <p>
       <asp:Button runat="server" ID="btnLogin" OnClick="LoginUser" Text="Войти" />
        <%--<input type="button" id="btnLogin" value="Войти" />--%>
    </p>
    <p>
        <a href="<%= iStore.Site.SiteUrl %>Users/Register.aspx">Регистрация</a><br />
        <a href="<%= iStore.Site.SiteUrl %>Users/ForgotPassword.aspx">Забыли пароль</a>
    </p>
</div>

<% } else { %>

<%= CurrentUser.Login %>  
<a href="<%= iStore.Site.SiteUrl %>Users/Profile.aspx">profile</a>
<asp:LinkButton runat="server" ID="lbLogOur" OnClick="LogOut">Log out</asp:LinkButton>
<% } %>