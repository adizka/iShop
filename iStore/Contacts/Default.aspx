<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="iStore.Contacts.Default" MasterPageFile="~/Page.Master" Title="Contacts | Marvel Worldwide" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
<style type="text/css">
    .Even { display:none; }
    #loader { padding-left:250px; padding-top: 30px; padding-bottom:30px;}
</style>
<script type="text/javascript">
    function SetPreloader() {
        $("#preloader").css("display", "block");
        $("#<%= sendBtn.ClientID %>").click();
    }
</script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
<asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
<asp:UpdatePanel runat="server" ID="up">
<ContentTemplate>
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.easing-1.3.pack.js"></script>
<div class="right_adrees">
    <h1>Head Office</h1>
    <p>123 Main Street </p>
    <p>Los Angeles, CA, 94101 </p>
    <p>Phone: +1 800 123 4567 </p>
    <p>FAX: +1 800 891 2345 </p>
    <p>Email: <a href="">testmail@sitename.com </a> </p>
</div>
<div class="Form left_prizm">
    <h1>Contact Us</h1>
    <div runat="server" id="errMsg" visible="false"></div>
    <p>
        <label>User Name:</label>
        <asp:TextBox ID="userNameTxt" runat="server" Text=""></asp:TextBox>
    </p>

    <p>
        <label>E-mail:</label>
        <asp:TextBox ID="emailTxt" runat="server" Text=""></asp:TextBox>
    </p>
    <p>
        <label>Message:</label>
        <asp:TextBox ID="msgTxt" runat="server" Text="" TextMode="MultiLine"></asp:TextBox>
    </p>
    <p>
        <label>&nbsp;</label>
        <span class="universal_btn">
            <span>
                <a href="#" onclick="SetPreloader();" >Send</a>
            </span> 
        </span>        
    </p>
    
    <asp:Button ID="sendBtn" runat="server" Text="Send" OnClick="Send" CssClass="Even" />
    
    <div id="preloader" style="display:none">
        <img src="<%= iStore.Site.SiteUrl %>Images/loader.gif" alt="loader" id="loader" /> 
    </div>
</div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>