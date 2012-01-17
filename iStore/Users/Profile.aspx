<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" MasterPageFile="~/Page.Master" Inherits="iStore.Users.Profile" Title="Your profile | Marvel Worldwide" %>
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
    <h1>
        User profile (<%= auth.CurrentUser.Login%>)
    </h1>
<%--<p>
        <a href="#" onclick="ChangePassword();">Change Password</a>
    </p>--%>
<div id="changePassword" class="Form">
    <p>
        <asp:Label AssociatedControlID="txtNewPassword" runat="server" ID="lblNewPassword">New Password</asp:Label>
        <asp:TextBox runat="server" TextMode="Password" ID="txtNewPassword"></asp:TextBox>
        <asp:Label runat="server" ID="lblNewPasswordError"></asp:Label>
    </p>
    <p>
        <label>&nbsp;</label>
        <span class="universal_btn">
            <span>
                <asp:LinkButton runat="server" ID="btnChangePassword" OnClick="ChangePassword" Text="Change" />
            </span>
        </span>
    </p>
</div>
<div id="changeMail" class="Form">
<%--<p>
        <a href="#" onclick="ChangeMail();">Change Email</a>
    </p>--%>
    <p>
        <asp:Label AssociatedControlID="txtNewMail" runat="server" ID="lblNewMail">New Email</asp:Label>
        <asp:TextBox runat="server" ID="txtNewMail"></asp:TextBox>
        <asp:Label runat="server" ID="lblNewMailError"></asp:Label>
    </p>
    <p>
        <label>&nbsp;</label>
        <span class="universal_btn">
            <span>
                <asp:LinkButton runat="server" ID="btnChangeMail" OnClick="ChangeMail" Text="Change" />
            </span>
        </span>
    </p>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>