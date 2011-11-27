<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/Admin.Master" CodeBehind="ProductEdit.aspx.cs" Inherits="iStore.Admin.Products.ProductEdit" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.easing-1.3.pack.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.mousewheel-3.0.6.pack.js"></script>
<script type="text/javascript" src="../../Scripts/jquery.fancybox-2.0/jquery.fancybox.pack.js"></script>
<link rel="Stylesheet" type="text/css" href="../../Scripts/jquery.fancybox-2.0/jquery.fancybox.css" media="screen" />
<style type="text/css">
    .ProductEdit { width:600px; }    
    .ProductEdit_Left { width:300px; float:left; }
    .ProductEdit_Right { width:300px; float:right; }
    .ProductEdit_Save {  padding-top:20px; } 
    .ProductEdit_AddCategory { clear:both; padding-top:20px;}
</style>
<script type="text/javascript">
    $(document).ready(function () {
        $(".various").fancybox({
            maxWidth: 800,
            maxHeight: 600,
            fitToView: false,
            width: '30%',
            height: '30%',
            autoSize: false,
            closeClick: false,
            openEffect: 'none',
            closeEffect: 'none'
        });
    });

    function SetCategory(categoryId) {
        if ($("#chk" + categoryId).is(':checked')) {
            SetCategoryInHiddenField(categoryId);
        }
        else {
            RemoveCategoryFromHiddenField(categoryId);
        }
    }

    function SetCategoryInHiddenField(categoryId) {
        var value = $("#<%=hf.ClientID %>").val();
        $("#<%=hf.ClientID %>").val(value + "!~!" + categoryId);
    }

    function RemoveCategoryFromHiddenField(categoryId) {
        var value = $("#<%=hf.ClientID %>").val();
        var repString = categoryId + "!~!"
        
        if (value.indexOf(repString) != -1) {
            value = value.replace(repString, "");
            $("#<%=hf.ClientID %>").val(value);
        }
        repString = "!~!" + categoryId;
        if (value.indexOf(repString) != -1) {
            value = value.replace(repString, "");
            $("#<%=hf.ClientID %>").val(value);
        }
    }

</script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
<asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
<asp:UpdatePanel runat="server" ID="up">
<ContentTemplate>
     <iS:ValidateErrors runat="server" ID="ve" Visible="false" />
     <div class="ProductEdit">
        <div class="ProductEdit_Left">
             <p>
                <asp:Label runat="server" ID="lblName" AssociatedControlID="txtName">Name</asp:Label>   
                <asp:TextBox runat="server" ID="txtName" />
             </p>
             <p>
                <asp:Label runat="server" ID="lblPhoto" AssociatedControlID="fu">Photo</asp:Label>   
                <asp:FileUpload runat="server" ID="fu" />
            </p>
            <p>
                <asp:Label runat="server" ID="lblUnit" AssociatedControlID="txtUnit">Unit</asp:Label> 
                <asp:TextBox runat="server" ID="txtUnit" Width="30" />   

                <asp:Label runat="server" ID="lblPrice" AssociatedControlID="txtPrice">Price</asp:Label>
                <asp:TextBox runat="server" ID="txtPrice" Width="30" />
            </p>
            <p>
                <asp:Label runat="server" ID="lblVisible" AssociatedControlID="chkVisible">is Visible</asp:Label>
                <asp:CheckBox runat="server" ID="chkVisible" Checked="true" />

                <asp:Label runat="server" ID="lblCount" AssociatedControlID="txtCount">Count</asp:Label>
                <asp:TextBox runat="server" ID="txtCount" Width="30" />
            </p>
        </div>
        <div class="ProductEdit_Right">
            img
        </div>
    </div>
    <script type="text/javascript">
    </script>
    <div class="ProductEdit_AddCategory">
        <a class="various" href="#SelectCategoties">Add/Remove Categories</a>
        <div id="SelectCategoties">
            <% foreach (BL.Category item in allCategories)
               { %>
                <p>
                    <span>
                        <label for="chk<%= item.CategoryID.ToString() %>"><%= item.Name %></label>
                        <% if (allCategoriesRefsCurrentProduct.Any(r=>r.CategoryID == item.CategoryID))
                           { %>
                                <% hf.Value = hf.Value + "!~!" + item.CategoryID.ToString(); %>
                                <input type="checkbox" id="chk<%= item.CategoryID.ToString() %>" checked="checked" onclick="SetCategory('<%= item.CategoryID.ToString() %>');" />
                            <% } else 
                           { %>
                                <input type="checkbox" id="chk<%= item.CategoryID.ToString() %>" onclick="SetCategory('<%= item.CategoryID.ToString() %>');"  />
                         <% } %>
                    </span>
                </p>
            <% } %>
            <a href="#">Выбрать</a>
        </div>
    </div>
    <p class="ProductEdit_Save">
        <asp:Button runat="server" ID="btnSave" Text="Save"  OnClick="Save" />
    </p>
    <asp:HiddenField runat="server" ID="hf" />
    
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
