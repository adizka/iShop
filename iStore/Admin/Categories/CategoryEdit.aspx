<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CategoryEdit.aspx.cs" Inherits="iStore.Admin.Categories.CategoryEdit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
<asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
<asp:UpdatePanel runat="server" ID="up">
<ContentTemplate>
   <div class="Admin_LoginErrors" id="divError" runat="server"></div>
   <div class="line_style ProductEdit_Save">
    <p>
        <asp:Label runat="server" ID="lblName" AssociatedControlID="txtName">Category Name</asp:Label>

        <asp:TextBox runat="server" ID="txtName" CssClass="CategoryInput" CausesValidation="false"></asp:TextBox>

        <asp:DropDownList ID="ddlCategories" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
            <asp:ListItem Value="parent">-- pick one --</asp:ListItem>
        </asp:DropDownList>
                    <span class="universal_button">
                        <span>
                            <asp:LinkButton runat="server" ID="btnSaveCategory" Text="Save" OnClick="Save" />
                        </span>
                    </span>
    </p>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>   