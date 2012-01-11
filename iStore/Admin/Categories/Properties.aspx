<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Properties.aspx.cs" Inherits="iStore.Admin.Categories.Properties" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit"%>
<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
<style type="text/css">
   
   
   
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="main">
<asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>    
<asp:UpdatePanel runat="server" ID="up">
<ContentTemplate>

    <iS:ValidateErrors runat="server" ID="ve" />
    <div class="Admin_CategoryProperties">
    <div class="reorderListDemo">
        <ajaxToolkit:ReorderList runat="server" ID="rlProp"
            PostBackOnReorder="false"
            CallbackCssStyle="callbackStyle"
            DragHandleAlignment="Left">
            <ItemTemplate>
                <div class="itemArea">
                    <asp:Label  runat="server" Text='<%# HttpUtility.HtmlEncode(Convert.ToString(Eval("PropertName"))) %>' />    
                    <asp:Label  runat="server" Text='<%# HttpUtility.HtmlEncode(Convert.ToString(Eval("PropertName"))) %>' />    
                </div>
            </ItemTemplate>
        
            <EmptyListTemplate>
                <div class="itemArea">
                    Не добавлено ни одного свойства
                </div>
            </EmptyListTemplate>
            <ReorderTemplate>
                <asp:Panel ID="Panel2" runat="server" CssClass="reorderCue" />
            </ReorderTemplate>
            <DragHandleTemplate>
                <div class="dragHandle"></div>
            </DragHandleTemplate>
        </ajaxToolkit:ReorderList>
    </div>
    </div>
    <div class="demobottom"></div>
    <p>
        <span>
            <asp:Label runat="server" ID="lblName">Name</asp:Label>
            <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
            <asp:Label runat="server" ID="lblValue">Value</asp:Label>
            <asp:TextBox runat="server" ID="txtValue"></asp:TextBox>
            <asp:Button runat="server" ID="btnSave" Text="Add" OnClick="Save" />
        </span>
    </p>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content> 