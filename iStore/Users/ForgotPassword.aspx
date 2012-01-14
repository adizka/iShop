<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Page.Master" CodeBehind="ForgotPassword.aspx.cs" Inherits="iStore.Users.ForgotPassword" Title="Forgot password | Marvel" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>    
<asp:UpdatePanel runat="server" ID="up">
<ContentTemplate>
   <h1>I forgot my password</h1>
   <div class="Form">
       <p>
            <asp:Label runat="server" ID="lblLogin" AssociatedControlID="txtEmail">Username or email</asp:Label> 
            <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox> <br />
            <label>&nbsp;</label>
            <asp:Label runat="server" ID="lblEmailError"></asp:Label>
       </p>
       <p>
            <asp:Label runat="server" AssociatedControlID="txtEmail">&nbsp;</asp:Label>
            <span class="universal_btn">
                <span>
                    <asp:LinkButton runat="server" ID="btnGo" OnClick="Go" Text="Send" />
                </span>
            </span>
       </p>
   </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>