<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" MasterPageFile="~/Page.Master" Inherits="iStore.Users.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
<asp:UpdatePanel runat="server" ID="up">
    <ContentTemplate>
        <div class="Title" runat="server" id="divRegistrationTitle">
            <h1>Registration</h1> 
        </div>
        <div class="Form" runat="server" id="divRegistration">
            <p>
                <asp:Label runat="server" ID="lblLogin" AssociatedControlID="txtLogin">User name:</asp:Label>
                <asp:TextBox runat="server" ID="txtLogin"></asp:TextBox>
                <asp:Label runat="server" ID="lblLoginError" Visible="false" CssClass="Red"></asp:Label>
            </p>
            <p>
                <asp:Label runat="server" ID="lblEmail" AssociatedControlID="txtEmail">E-mail:</asp:Label>
                <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
                <asp:Label runat="server" ID="lblEmailError" CssClass="Red" Visible="false"></asp:Label>
            </p>
            <p>
                <asp:Label runat="server" ID="lblPassword" AssociatedControlID="txtPassword">Password:</asp:Label>
                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox>
                <asp:Label runat="server" ID="lblPasswordError" CssClass="Red" Visible="false"></asp:Label>
            </p> 
            <p>
                <Label>&nbsp;</Label>
                <asp:Button CssClass="register_btn" runat="server" ID="btnRegister" Text="" OnClick="RegistrationUser" />
            </p>
        </div>
        <div class="Title Form" runat="server" ID="divAfterRegistrationTitle" Visible="false">
            <p>Complete the registration</p>
        </div>
        <div class="Registration_Info Form" runat="server" id="divAfterRegistration" Visible="false">
            <p>Registration is successful, a confirmation email has been sent to your email</p>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>