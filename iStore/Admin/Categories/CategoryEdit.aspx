<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CategoryEdit.aspx.cs" Inherits="iStore.Admin.Categories.CategoryEdit" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="main">
<asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="upAddCategory">
       <ContentTemplate>
        <script type="text/javascript">
            function setSelectedValue() {
                var value = $('#ddlParentCategories').val();
                $('#<%= hf.ClientID %>').val(value);
            }
        </script>
        <iS:ValidateErrors runat="server" ID="ve" />
        <p>
            <asp:Label runat="server" ID="lblName" AssociatedControlID="txtName">Category Name</asp:Label>
            <asp:TextBox runat="server" ID="txtName" CssClass="CategoryInput" CausesValidation="false"></asp:TextBox>
            <select id="ddlParentCategories" onchange="setSelectedValue();">
                <option value="parent">Parent Category</option>
                <% foreach (var item in allCategories) { %>
                    <option value="<%= item.CategoryID.ToString() %>"><%= item.Name %></option>
                <% } %>
            </select>
            <asp:Button runat="server" ID="btnSaveCategory" Text="Сохранить" OnClick="Save" />
            <asp:HiddenField runat="server" ID="hf" Value="parent" />
        </p>
       </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content> 