<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/Admin.Master" CodeBehind="NewsEdit.aspx.cs" Inherits="iStore.Admin.News.NewsEdit" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="main">
    <div class="Admin_AddOrUpdate">
           
            
            <div class="Title">
                <h1><%= global::Resources.Admin_Edits.PageEdit_Title%></h1>
            </div>
            <div runat="server" visible="false" id="errMsg"></div>
            <div class="line_style">
                <p>
                    <span><%= global::Resources.Default.Title %> </span>
                    <asp:TextBox runat="server" ID="txtTitle"  />
                    <span><%= global::Resources.Default.Description %> </span>
                    <asp:TextBox runat="server" ID="txtDesc"  />
                </p>
                <p>
                    <asp:TextBox runat="server" ID="txtBody" TextMode="MultiLine"/>
                </p>
                <p>
                    <span class="universal_button">
                        <span>
                            <asp:LinkButton runat="server" ID="btnSave" Text="<%$ Resources:Default, Save %>" OnClick="Save" />
                        </span>
                    </span>
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
