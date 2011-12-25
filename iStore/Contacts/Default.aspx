<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="iStore.Contacts.Default" MasterPageFile="~/Page.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContent">
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.easing-1.3.pack.js"></script>
<div>
<p>
<div>
User Name:
</div>
<div>
<asp:TextBox ID="userNameTxt" runat="server" Text=""></asp:TextBox>
</div>
</p>
<p>
<div>
e-mail:
</div>
<div>
<asp:TextBox ID="emailTxt" runat="server" Text=""></asp:TextBox>
</div>
</p>
<p>
<div>
<div>
message:
</div>
<div>
<asp:TextBox ID="msgTxt" runat="server" Text="" TextMode="MultiLine"></asp:TextBox>
</div>
</div>
</p>
<p>
<div>
<asp:Button ID="sendBtn" runat="server" Text="Send" OnClick="Send"></asp:Button>
</div>
</p>
</div>

</asp:Content>