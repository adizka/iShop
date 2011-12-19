<%@ Page MasterPageFile="~/Admin/Admin.Master"  Language="C#" AutoEventWireup="true" CodeBehind="PageEdit.aspx.cs" Inherits="iStore.Admin.Pages.PageEdit" %>
<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="main">
        <div class="Admin_AddOrUpdate">
            <div class="Admin_AddOrUpdateErrors">
                <iS:ValidateErrors runat="server" ID="ve" />
            </div>
            <div class="Title">
                <%= global::Resources.Admin_Edits.PageEdit_Title %>
            </div>
            <div>
                <p>
                    <%= global::Resources.Default.Name %>
                    <asp:TextBox runat="server" ID="txtName"  />
                </p>
                <p>
                    <%= global::Resources.Admin_Edits.Keywords%>
                    <asp:TextBox runat="server" ID="txtDesc"  />
                </p>
                <p>
                    <%= global::Resources.Default.Body %>:
                    <br />
                    <asp:TextBox runat="server" ID="txtBody" TextMode="MultiLine"/>
                </p>
                <p>
                    <asp:Button runat="server" ID="btnSave"  Text="<%$ Resources:Default, Save %>" OnClick="Save" />
                </p>
            </div>
        </div>
    <script src="<%= iStore.Site.SiteUrl %>Scripts/fck/fckeditor.js" type="text/javascript"></script>
    <script type="text/javascript">

        function replaceAll() {
            $("#<%=txtDesc.ClientID %>").val($("#<%=txtDesc.ClientID %>").val().split(' ').join('_'));
        }
        $("#<%=txtDesc.ClientID %>").keyup(replaceAll)
        $("#<%=txtDesc.ClientID %>").keyup();

        window.onload = function () {
            var sBasePath = '<%= iStore.Site.SiteUrl %>Scripts/FCK/';
            var oFCKeditor = new FCKeditor('<%=txtBody.ClientID %>');
            oFCKeditor.Config.Enabled = true;
            oFCKeditor.Config.UserFilesPath = '<%= iStore.Site.SiteUrl %>Content/Pages';
            oFCKeditor.Config.UserFilesAbsolutePath = '<%= iStore.Site.SiteUrl %>Content/Pages';

            oFCKeditor.Height = '500';
            oFCKeditor.Width = '716';
            oFCKeditor.BasePath = sBasePath;
            oFCKeditor.ReplaceTextarea();
        }
    </script>
</asp:Content>
