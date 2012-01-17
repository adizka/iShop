<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="ProductSort.aspx.cs" Inherits="iStore.Admin.Products.ProductSort" Title="Sort products | Marvel Worldwide" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="iS" TagName="ValidateErrors" Src="~/Modules/Controls/Validators/ValidateErrors.ascx" %>

<%--<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head"></asp:Content>--%>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="main">
<script type="text/javascript">

    var catIds = [];

    function MoveUp(index) {
        if (index == 0)
            return;

        var temp = catIds[index];
        catIds[index] = catIds[index - 1];
        catIds[index - 1] = temp;

        temp = $("#CategoryName" + index).html();
        $("#CategoryName" + index).html($("#CategoryName" + (index - 1)).html());
        $("#CategoryName" +( index - 1)).html(temp);
        UpdateResult();
                
    }
    function MoveDown(index) {
        if (index == <%=CatProducts.Count()-1 %>)
            return;

        var temp = catIds[index];
        catIds[index] = catIds[index + 1];
        catIds[index + 1] = temp;

        temp = $("#CategoryName" + index).html();
        $("#CategoryName" + index).html($("#CategoryName" + (index + 1)).html());
        $("#CategoryName" + (index + 1)).html(temp);
        UpdateResult()
   
    }


    function UpdateResult()
    {
        var result = "";
        for(var i=0;i<catIds.length;i++)
        {
            result += catIds[i]+ " ";
        }

        $("#<%=hd.ClientID %>").val(result);
    }

</script>
<asp:HiddenField ID="hd" runat="server" />
<div style="width:100%">
<div style="width:100%;float:left">
    
      <%
        int index = -1;
        foreach (var item in CatProducts)
        {
            index++;
      %>
      <div>
            <span class="MoveUp">
            
            <a href="javascript:MoveUp(<%=index %>);">
            Up
            </a>
            </span>
            <span class="MoveDown">
            <a href="javascript:MoveDown(<%=index %>);">Down
            </a></span>
            <script type="text/javascript">
                catIds.push("<%= item.ID%>");
            </script>
            <span class="CategoryName" id="CategoryName<%=index %>">
            <%= item.Product.Name%>
            </span>
           </div>
            <%} %>
</div>
<asp:Button ID="Button1" Text="Save" OnClick="SaveCategoriesRate" runat="server"/>
</div>


</asp:Content>  




 