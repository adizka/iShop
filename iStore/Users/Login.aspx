<%@ Page Title="Marvel | Authorization" MasterPageFile="~/Page.Master" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="iStore.Users.Login" %>
<%@ Register tagPrefix="iS" tagName="ValidateErrors" src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>

<asp:Content runat="server" ID="hc" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content runat="server" ID="mc" ContentPlaceHolderID="MainContent">

<h1>Log in</h1> 
<asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
<asp:UpdatePanel runat="server" ID="up">
<ContentTemplate>
    <div class="Admin_LoginErrors" id="divError" runat="server">
        
    </div>
    <div class="Form">
        <p>
            <asp:Label runat="server" ID="lblLogin" AssociatedControlID="txtLogin">Login or Email</asp:Label>
            <asp:TextBox runat="server" ID="txtLogin"></asp:TextBox>
        </p>
        <p>
            <asp:Label runat="server" ID="lblPassword" AssociatedControlID="txtPassword">Password</asp:Label>
            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" ></asp:TextBox>
            <a class="forgot_pass" href="<%= iStore.Site.SiteUrl %>Users/ForgotPassword.aspx">Forgot your password?</a>
        </p>
        <p>
            <asp:Label runat="server" ID="lblSaveMe" AssociatedControlID="chbSaveMe">Remember me</asp:Label>
            <asp:CheckBox runat="server" ID="chbSaveMe" Checked="true" />
        </p>
        <p>
            <label>&nbsp;</label>
            <asp:Button CssClass="log_inbtn" runat="server" ID="btnLogin" Text=""  OnClick="Log_in" />
        </p>
    </div>
</ContentTemplate>    
</asp:UpdatePanel>
</asp:Content>

