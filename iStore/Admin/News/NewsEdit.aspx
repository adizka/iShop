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
                    <asp:TextBox runat="server" ID="txtDesc" />
                </p>
                <p>
                    <%= global::Resources.Default.Body %>:
                    <asp:TextBox runat="server" ID="txtBody" />
                </p>
                <p>
                    <asp:Button runat="server" ID="btnSave" Text="<%$ Resources:Default, Save %>" OnClick="Save" />
                </p>
            </div>
        </div>    
</asp:Content>
