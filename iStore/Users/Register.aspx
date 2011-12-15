<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" MasterPageFile="~/Site.master" Inherits="iStore.Users.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
<asp:UpdatePanel runat="server" ID="up">
    <ContentTemplate>
        <div class="Title" runat="server" id="divRegistrationTitle">Регистрация</div>
        <div class="Registration" runat="server" id="divRegistration">
            <p>
                <asp:Label runat="server" ID="lblLogin" AssociatedControlID="txtLogin">Логин</asp:Label><br />
                <asp:TextBox runat="server" ID="txtLogin"></asp:TextBox><br />
                <asp:Label runat="server" ID="lblLoginError" Visible="false" CssClass="Red"></asp:Label>
            </p>
            <p>
                <asp:Label runat="server" ID="lblEmail" AssociatedControlID="txtEmail">Email</asp:Label><br />
                <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox><br />
                <asp:Label runat="server" ID="lblEmailError" CssClass="Red" Visible="false"></asp:Label>
            </p>
            <p>
                <asp:Label runat="server" ID="lblPassword" AssociatedControlID="txtPassword">Пароль</asp:Label><br />
                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox><br />
                <asp:Label runat="server" ID="lblPasswordError" CssClass="Red" Visible="false"></asp:Label>
            </p> 
            <br />
            <p>
                <asp:Button runat="server" ID="btnRegister" Text="Зарегестрироваться" OnClick="RegistrationUser" />
            </p>
        </div>
        <div class="Title" runat="server" ID="divAfterRegistrationTitle" Visible="false">Завершение регистрации</div>
        <div class="Registration_Info" runat="server" id="divAfterRegistration" Visible="false"><br />123</div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>