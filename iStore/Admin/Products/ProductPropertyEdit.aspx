<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductPropertyEdit.aspx.cs"
    Inherits="iStore.Admin.Products.ProductPropertyEdit" MasterPageFile="~/Admin/Admin.Master" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>
<asp:content id="Content1" runat="server" contentplaceholderid="head">
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.easing-1.3.pack.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.mousewheel-3.0.6.pack.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.fancybox.pack.js"></script>
<link rel="Stylesheet" type="text/css" href="../../Scripts/jquery.fancybox-2.0/jquery.fancybox.css" media="screen" />
<style type="text/css">
    
</style>
<script type="text/javascript">

</script>
</asp:content>
<asp:content id="Content2" runat="server" contentplaceholderid="main">
<asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
<asp:UpdatePanel runat="server" ID="up">
<ContentTemplate>
<script type="text/javascript" src="../Scripts/ProdPropertyEdit.js"></script>
<p>
    <a href="<%= iStore.Site.SiteAdminUrl %>Products/?cid=<%= Request.QueryString["cid"] %>">Вернуться к списку товаров</a>
</p>
<br />

<div style="font-size:30px;text-align:center;">
<%=Product.Name %>
</div>
<div class="Admin_LoginErrors" id="divError" runat="server">
        
</div>
    <div id="AddNewPropContainer">
       <a id="AddButton" href="javascript:ShowInputForm();">Add New Property</a>
        <div id="AddFormID" style="display:none">
            <span>
                <input type="text" id="PropName" />
            </span><span>
                <input type="text" id="ValName" />
            </span>
            <span id="AddID" style="cursor:pointer;color:Blue;" onclick="AddProperty()">Add</span>
        </div>
        <div id="WarningMsg" style="display:none;">Такое свойство уже существует.</div>
    </div>

    <div id="ProductPropertyContainer">
        <%
            int sortIndex = -1;
            foreach (var item in ProductsProperies)
              
          {
              sortIndex++; %>
        <div class="ProductPropertyItem">
                <span class="MoveUp" style="cursor:pointer;color:Blue;">MoveUp</span>
                <span class="MoveDown" style="cursor:pointer;color:Blue;">MoveDown</span>
                <span class="PropertyName"><%=item.PropertyName %></span>
                <span class="PropertyValue"><%=item.PropertyValue %></span>
                <span class="SortIndex"><%=sortIndex.ToString()%></span>
                <span class="DeleteButton" style="cursor:pointer;color:Blue;">Del</span>
                <span class="EditButton" style="cursor:pointer;color:Blue;">Edit</span>
        </div>
        <%} %>
    </div>

   
    <div id="CopyingPropertyContainer" style="padding:50px">
    <div>
    копировать свойства выбранного продукта который входит в категории текущего продукта<br />
    <span>
    <asp:DropDownList ID="cpddl" runat="server" Width="150"></asp:DropDownList>
    </span><span><asp:Button runat="server" ID="AddPropertiesbtn1"  Text="Copy" OnClick="Copy"/></span>
    </div>
    <div>
    копировать свойства выбранного продукта<br />
    <span>
    <asp:DropDownList ID="apddl" runat="server" Width="150"></asp:DropDownList>
    </span><span><asp:Button runat="server" ID="AddPropertiesbtn2"  Text="Copy" OnClick="Copy"/></span>
    </div>
    </div>



    <div class="ProductPropertyItemTemplate" style="display:none;">
                <span class="MoveUp" style="cursor:pointer;color:Blue;">MoveUp</span>
                <span class="MoveDown" style="cursor:pointer;color:Blue;">MoveDown</span>
                <span class="PropertyName"></span>
                <span class="PropertyValue"></span>
                <span class="SortIndex"></span>
                <span class="DeleteButton" style="cursor:pointer;color:Blue;">Del</span>
                <span class="EditButton" style="cursor:pointer;color:Blue;">Edit</span>
    </div>
    <div id="EditPropertyTemplate" style="display: none;z-index:100;height:100%;width:100%;">
        <span>
            <input type="text" id="EditName" />
        </span><span>
            <input type="text" id="EditVal" />
        </span><span id="ChangeID" style="cursor: pointer; color: Blue;">Change </span>
        <div id="EditErrorMsg" style="display:none;">Такое свойство уже существует.</div>
    </div>

    <p class="ProductEdit_Save">
        <asp:Button runat="server" ID="btnSave" Text="Save"  OnClick="Save" />
    </p>
    <asp:HiddenField runat="server" ID="hf" ClientIDMode="Static" />
    <script type="text/javascript">
        function pageLoad() {
            AssignEvents();
            UpdateStoredValues();
        }
    </script>
    </ContentTemplate>
    </asp:UpdatePanel>
<br />
<p>
    <a href="<%= iStore.Site.SiteAdminUrl %>Products/?cid=<%= Request.QueryString["cid"] %>">Вернуться к списку товаров</a>
</p>
</asp:Content>
