<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutorizationControl.ascx.cs" Inherits="iStore.Design.AutorizationControl" %>

<% if (CurrentUser == null) { %>
<span class="not_loginyet">
    NOT LOGGED YET <br />
    <span>
        <%--<a class="Header_LoginLink" rel="#Header_Login" type="button">sign in</a> or --%>
        <a href="<%= iStore.Site.SiteUrl %>Users/Login.aspx">sign in</a> or 
        <a href="<%= iStore.Site.SiteUrl %>Users/Register.aspx">register</a>
    </span>
</span>

<div class="Header_Login apple_overlay" id="Header_Login">
    <p>
        <asp:Label runat="server" ID="lblLogin" AssociatedControlID="txtLogin">User name:</asp:Label><br />
        <asp:TextBox runat="server" ID="txtLogin"></asp:TextBox>
    </p>

    <p>
        <asp:Label runat="server" ID="lblPassword" AssociatedControlID="txtPassword">Password:</asp:Label><br />
        <asp:TextBox runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox>
    </p>
    <p>
        <asp:CheckBox runat="server" ID="chbSaveMe" Checked="true" />
        <asp:Label runat="server" ID="lblSaveMe" AssociatedControlID="chbSaveMe">Remember me</asp:Label> 
    </p>
    <p>
        <span class="universal_btn">
            <span>
                <asp:LinkButton runat="server" ID="btnLogin" OnClick="LoginUser" Text="Log in" />
            </span>
        </span>
        <%--<input type="button" id="btnLogin" value="Войти" />--%>
    </p>
    <p>
        <a href="<%= iStore.Site.SiteUrl %>Users/ForgotPassword.aspx">Forgot your password?</a> <br />
        <a href="<%= iStore.Site.SiteUrl %>Users/Register.aspx">Register</a>
    </p>
</div>

<% } else { %>

<span class="not_loginyet">
    <%= CurrentUser.Login %> <br />
    <a class="anchor_nm" href="<%= iStore.Site.SiteUrl %>Users/Profile.aspx">Profile</a>
    <asp:LinkButton runat="server" ID="lbLogOur" OnClick="LogOut">Log out</asp:LinkButton>
</span>
<% } %>