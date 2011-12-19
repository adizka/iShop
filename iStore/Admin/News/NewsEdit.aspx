<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/Admin.Master" CodeBehind="NewsEdit.aspx.cs" Inherits="iStore.Admin.News.NewsEdit" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="main">
    <div class="Admin_AddOrUpdate">
            <div class="Admin_AddOrUpdateErrors">
                <iS:ValidateErrors runat="server" ID="ve" />
            </div>
            <div class="Title">
                <%= global::Resources.Admin_Edits.PageEdit_Title%>
            </div>
            <div>
                <p>
                    <%= global::Resources.Default.Title %>
                    <asp:TextBox runat="server" ID="txtTitle"  />
                </p>
                <p>
                    <%= global::Resources.Default.Description %>
                    <br />
                    <asp:TextBox runat="server" ID="txtDesc"  />
                </p>
                <p>
                    <%= global::Resources.Default.Body %>:
                    <asp:TextBox runat="server" ID="txtBody" TextMode="MultiLine"/>
                </p>
                <p>
                    <asp:Button runat="server" ID="btnSave" Text="<%$ Resources:Default, Save %>" OnClick="Save" />
                </p>
            </div>
        </div>    
        <script src="<%= iStore.Site.SiteUrl %>Scripts/fck/fckeditor.js" type="text/javascript"></script>
        <script type="text/javascript">
            window.onload = function () {
                var sBasePath = '<%= iStore.Site.SiteUrl %>Scripts/FCK/';
                var oFCKeditor = new FCKeditor('<%=txtBody.ClientID %>');
                oFCKeditor.Config.Enabled = true;
                oFCKeditor.Config.UserFilesPath = '<%= iStore.Site.SiteUrl %>Content/News';
                oFCKeditor.Config.UserFilesAbsolutePath = '<%= iStore.Site.SiteUrl %>Content/News';

                oFCKeditor.Height = '500';
                oFCKeditor.Width = '716';
                oFCKeditor.BasePath = sBasePath;
                oFCKeditor.ReplaceTextarea();
            }
</script>
</asp:Content>
