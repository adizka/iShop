<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/Admin.Master" CodeBehind="ProductEdit.aspx.cs" Inherits="iStore.Admin.Products.ProductEdit" %>

<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
<script type="text/javascript">
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
<script type="text/javascript">
    $(document).ready(function() {
        $('#AddRemoveCategories').click(function () {
            $("#SelectCategoties").toggle();
        });

        $('#selectCatLink').click(function () {
            $("#SelectCategoties").css("display", "none");
        });    
    });
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
                <asp:Label runat="server" ID="lblUnit" AssociatedControlID="txtUnit">Unit</asp:Label> 
                <asp:TextBox runat="server" ID="txtUnit" Width="30" />
            </p>
            <p>
                <asp:Label runat="server" ID="lblPrice" AssociatedControlID="txtPrice">Price</asp:Label>
                <asp:TextBox runat="server" ID="txtPrice" Width="30" />
            </p>
            <p>
                <asp:Label runat="server" ID="lblVisible" AssociatedControlID="chkVisible">is Visible</asp:Label>
                <asp:CheckBox runat="server" ID="chkVisible" Checked="true" />
            </p>
            <p>
                <asp:Label runat="server" ID="lblCount" AssociatedControlID="txtCount">Count</asp:Label>
                <asp:TextBox runat="server" ID="txtCount" Width="30" />
            </p>
        </div>
       
    </div>
    <div class="ProductEdit_AddCategory">
        <div id="AddRemoveCategories" class="Link">Add/Remove Categories</div>
        <div id="SelectCategoties">
            <% Guid currentCategoryId = Guid.NewGuid();
               if (currentCategory != null)
               {
                   currentCategoryId = currentCategory.CategoryID;
               }
               foreach (BL.Category item in allCategories)
               { %>
                <p>
                    <span>
                        <label for="chk<%= item.CategoryID.ToString() %>"><%= item.Name %></label>
                        <% if (allCategoriesRefsCurrentProduct.Any(r => r.CategoryID == item.CategoryID) || (item.CategoryID == currentCategoryId))
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
            <div  id="selectCatLink" class="Link">Выбрать</div>
        </div>
    </div>
    <p class="ProductEdit_Save">
        <asp:Button runat="server" ID="btnSave" Text="Save"  OnClick="Save" />
    </p>
    <asp:HiddenField runat="server" ID="hf" />
    
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
