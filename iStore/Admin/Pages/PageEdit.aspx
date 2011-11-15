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
                    <asp:TextBox runat="server" ID="txtDesc" />
                </p>
                <p>
                    <%= global::Resources.Default.Body %>:
                    <asp:TextBox runat="server" ID="txtBody" />
                </p>
                <p>
                    <asp:Button runat="server" ID="btnSave"  Text="<%$ Resources:Default, Save %>" OnClick="Save" />
                </p>
            </div>
        </div>        
</asp:Content>
