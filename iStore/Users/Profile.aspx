<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" MasterPageFile="~/Page.Master" Inherits="iStore.Users.Profile" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<script type="text/javascript">
    function ChangePassword() {
        $("#changePassword").toggle();
    }

    function ChangeMail() {
        $("#changeMail").toggle();
    }
</script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
<asp:UpdatePanel runat="server" ID="up">
<ContentTemplate>
    <p>
        User profile (<%= auth.CurrentUser.Login%>)
    </p>
    <p>
        <a href="#" onclick="ChangePassword();">Change Password</a>
    </p>
    <p id="changePassword" style="display: none;">
        <asp:Label runat="server" ID="lblNewPassword">New Password</asp:Label>
        <asp:TextBox runat="server" TextMode="Password" ID="txtNewPassword"></asp:TextBox>
        <asp:Label runat="server" ID="lblNewPasswordError"></asp:Label>
        <br />
        <asp:Button runat="server" ID="btnChangePassword" OnClick="ChangePassword" Text="Change" />
    </p>
    <p>
        <a href="#" onclick="ChangeMail();">Change Email</a>
    </p>
    <p id="changeMail" style="display: none;">
        <asp:Label runat="server" ID="lblNewMail">New Email</asp:Label>
        <asp:TextBox runat="server" ID="txtNewMail"></asp:TextBox>
        <asp:Label runat="server" ID="lblNewMailError"></asp:Label>
        <br />
        <asp:Button runat="server" ID="btnChangeMail" OnClick="ChangeMail" Text="Change" />
    </p>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>