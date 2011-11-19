<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/Admin.Master"  CodeBehind="CategorySort.aspx.cs" Inherits="iStore.Admin.Categories.CategorySort" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head"></asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
    <style type="text/css">
.dragHandle {
	width:10px;
	height:15px;
	background-color:Blue;
	background-image:url(images/bg-menu-main.png);
	cursor:move;
	border:outset thin white;
}
</style>
<asp:ScriptManager runat="server" ></asp:ScriptManager>
<asp:UpdatePanel ID="up1" runat="server">  
    <ContentTemplate>  
        <div class="reorderListDemo">  
            <ajaxToolkit:ReorderList ID="rl" runat="server"  
                PostBackOnReorder="false"  
                CallbackCssStyle="callbackStyle"  
                DragHandleAlignment="Left"  
                ItemInsertLocation="Beginning"  
                DataKeyField="Sort"  
                SortOrderField="Priority">  
                <ItemTemplate>  
                    <div class="itemArea">  
                        <asp:Label ID="Label1" runat="server"  
                            Text='<%# HttpUtility.HtmlEncode(Convert.ToString(Eval("Name"))) %>' />  
                    </div>  
                </ItemTemplate>  
                <EditItemTemplate>  
                    <div class="itemArea">  
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Name") %>' ValidationGroup="edit" />  
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Name") %>' ValidationGroup="edit" />  
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Name") %>' ValidationGroup="edit" />  
                    </div>  
                </EditItemTemplate>  
                <ReorderTemplate>  
                    <asp:Panel ID="Panel2" runat="server" CssClass="reorderCue" />  
                </ReorderTemplate>  
                <DragHandleTemplate>  
                    <div class="dragHandle"></div>  
                </DragHandleTemplate>  
                <InsertItemTemplate>  
                    <!-- bottom border is workaround for IE7 Beta issue where bg doesn't render -->  
                    <div style="padding-left:25px; border-bottom:thin solid transparent;">  
                        <asp:Panel ID="panel1" runat="server" DefaultButton="Button1">  
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Name") %>' ValidationGroup="add" />  
                            <asp:Button ID="Button1" runat="server" CommandName="Insert" Text="Add"  ValidationGroup="add" />  
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="add"  
                                ErrorMessage="Please enter some text" ControlToValidate="TextBox1" />  
                        </asp:Panel>  
                    </div>  
                </InsertItemTemplate>  
            </ajaxToolkit:ReorderList>  
        </div>  
  
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete"  
            InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="Select"  
            TypeName="SessionTodoXmlDataObject" UpdateMethod="Update">  
            <DeleteParameters>  
                <asp:Parameter Name="Original_ItemID" Type="Int32" />  
            </DeleteParameters>  
            <UpdateParameters>  
                <asp:Parameter Name="Title" Type="String" />  
                <asp:Parameter Name="Description" Type="String" />  
                <asp:Parameter Name="Priority" Type="Int32" />  
                <asp:Parameter Name="Original_ItemID" Type="Int32" />  
            </UpdateParameters>  
            <InsertParameters>  
                <asp:Parameter Name="Title" Type="String" />  
                <asp:Parameter Name="Description" Type="String" />  
                <asp:Parameter Name="Priority" Type="Int32" />  
            </InsertParameters>  
        </asp:ObjectDataSource>      
    </ContentTemplate>  
</asp:UpdatePanel> 
    
</asp:Content>  




 