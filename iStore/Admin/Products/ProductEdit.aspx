<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/Admin.Master" CodeBehind="ProductEdit.aspx.cs" Inherits="iStore.Admin.Products.ProductEdit" Title="Product edit | Marvel Worldwide" %>


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
    function pageLoad() {
        $('#AddRemoveCategories').click(function () {
            $("#SelectCategoties").toggle();
        });
        $('#selectCatLink').click(function () {
            $("#SelectCategoties").css("display", "none");
        });
    }
</script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
<asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
<asp:UpdatePanel runat="server" ID="up">
<ContentTemplate>
     
    <div class="Admin_LoginErrors" id="divError" runat="server"></div>
     <div class="ProductEdit">
        <div class="ProductEdit_Left">
            <p>
                <asp:Label runat="server" ID="lblName" AssociatedControlID="txtName">Name:</asp:Label>   
                <asp:TextBox runat="server" ID="txtName" />
            </p>
            <p>
                <asp:Label runat="server" ID="lblUnit" AssociatedControlID="txtUnit">Unit:</asp:Label> 
                <asp:TextBox CssClass="small_count" runat="server" ID="txtUnit" />
            </p>
            <p>
                <asp:Label runat="server" ID="lblPrice" AssociatedControlID="txtPrice">Price:</asp:Label>
                <asp:TextBox CssClass="small_count" runat="server" ID="txtPrice"  />
            </p>
            <p>
                <asp:Label runat="server" ID="lblCount" AssociatedControlID="txtCount">Count:</asp:Label>
                <asp:TextBox CssClass="small_count" runat="server" ID="txtCount" />
            </p>
            <p>
                <asp:Label runat="server" ID="lblTax" AssociatedControlID="taxTxt">Tax per unit:</asp:Label>
                <asp:TextBox CssClass="small_count" runat="server" ID="taxTxt" />
            </p>
            <p>
                <asp:Label runat="server" ID="lblShipping" AssociatedControlID="shippingTxt">Shipping per unit:</asp:Label>
                <asp:TextBox CssClass="small_count" runat="server" ID="shippingTxt" />
            </p>
            <p>
                <asp:Label runat="server" ID="lblVisible" AssociatedControlID="chkVisible">Is visible:</asp:Label>
                <asp:CheckBox CssClass="small_count" runat="server" ID="chkVisible" Checked="true" />
            </p>
            <div class="ProductEdit_AddCategory">
                <div class="Link">
                    <p>
                        <label>&nbsp;</label>
                        <span id="AddRemoveCategories">Add/Remove Categories</span> 
                    </p>
                </div>
                <div class="list_of_category" id="SelectCategoties">
                    <% Guid currentCategoryId = Guid.NewGuid();
                       if (currentCategory != null)
                       {
                           currentCategoryId = currentCategory.CategoryID;
                       }
                       foreach (BL.Category item in allCategories)
                       { %>
                        <p class="paraq_linear">
                                <% if (allCategoriesRefsCurrentProduct.Any(r => r.CategoryID == item.CategoryID) || (item.CategoryID == currentCategoryId))
                                   { %>
                                        <% hf.Value = hf.Value + "!~!" + item.CategoryID.ToString(); %>
                                        <input type="checkbox" id="chk<%= item.CategoryID.ToString() %>" checked="checked" onclick="SetCategory('<%= item.CategoryID.ToString() %>');" />
                                    <% } else 
                                   { %>
                                        <input type="checkbox" id="chk<%= item.CategoryID.ToString() %>" onclick="SetCategory('<%= item.CategoryID.ToString() %>');"  />
                                 <% } %>
                                 <label for="chk<%= item.CategoryID.ToString() %>"><%= item.Name %></label>
                        </p>
                    <% } %>
                    <%--<div class="Link">
                        <p>
                            <label>&nbsp;</label>
                            <span id="selectCatLink">Выбрать</span> 
                        </p>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>
    
    <p class="ProductEdit_Save">
        <span class="universal_button">
            <span>
                <asp:LinkButton runat="server" ID="btnSave" Text="Save"  OnClick="Save" />
            </span>
        </span>
    </p>
    <asp:HiddenField runat="server" ID="hf" />
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
