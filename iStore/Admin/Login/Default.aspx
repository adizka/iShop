<%@ Page Title="Authorization - Admin Panel | Marvel WordlWide LTD" MasterPageFile="~/Admin/AdminLogin.Master" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="iStore.Admin.Login.Default" %>
<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>

<asp:Content ContentPlaceHolderID="head" runat="server" ID="hd"></asp:Content>
<asp:Content ContentPlaceHolderID="main" runat="server" ID="md">
<asp:ScriptManager  ID="sm" runat="server" EnablePageMethods="true"></asp:ScriptManager>
<asp:UpdatePanel runat="server" ID="up" updatemode="Conditional">
<ContentTemplate>
    <script type="text/javascript">
        function Forgot() {
            $('.Admin_Login').hide();
            $('#linkForgot').hide();
            $('.Admin_ForgotPassword').show();
            $('#linkLogin').show();
        }

        function Login() {
            $('.Admin_ForgotPassword').hide();
            $('#linkLogin').hide();
            $('.Admin_Login').show();
            $('#linkForgot').show();
        }
    </script>
    <div class="Admin_Login">
        <div class="Admin_LoginErrors">
            <iS:ValidateErrors runat="server" ID="veLogin" Visible="false" />
        </div>
        <div class="Admin_LoginForm">
            <div class="top_loged"></div>
            <div class="mid_loged">

                <p>
                    <span class="spn_spec">Username </span>
                    <asp:TextBox runat="server" ID="txtLogin" AutoPostBack="true" />
                </p>  
                <p>
                    <span class="spn_spec">Password </span>
                    <asp:TextBox runat="server" ID="txrPassword" TextMode="Password" AutoPostBack="true" />
                </p>
                <p>
                    <span class="spn_spec">&nbsp;</span>
                    <asp:CheckBox runat="server" ID="cbSaveMe" Checked="true" /> Remember me 
                </p>
                <p>
                    <span class="spn_spec">&nbsp;</span>
                    <span class="universal_button">
                        <span>
                            <asp:LinkButton runat="server" ID="btnLogin" OnClick="SignIn" Text="Sign in" />
                        </span>
                    </span>
                </p>
            </div>
            <div class="bot_loged"></div>
        </div>
    </div>
    <div class="Admin_ForgotPassword" style="display:none;">
        <div class="Admin_ForgotPasswordsErrors">
            <iS:ValidateErrors runat="server" ID="veForgotPasswords" Visible="false" />
        </div>   
        <div class="Admin_ForgotPasswordForm">
             <p>Username or email</p>
             <asp:TextBox runat="server" ID="txtLoginOrEmail" AutoPostBack="true"  />    
             <br />
             <p>
                <asp:Button runat="server" ID="btnSend" OnClick="ForgotPassword" Text="Send password"  />
            </p>
        </div>         
    </div>
   <%-- <div class="Admin_LoginLinks">
        <a href="#" onclick="Forgot();" id="linkForgot">Forgot password?</a>
        <a href="#" onclick="Login();" id="linkLogin" style="display:none">Sign in</a>
    </div>   --%>    
</ContentTemplate>     
</asp:UpdatePanel>
</asp:Content>

