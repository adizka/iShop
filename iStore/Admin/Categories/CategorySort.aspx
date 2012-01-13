<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/Admin.Master"  CodeBehind="CategorySort.aspx.cs" Inherits="iStore.Admin.Categories.CategorySort" %>
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
        if (index == <%=SiblingCategories.Count()-1 %>)
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
      <%
        int index = -1;
        foreach (var item in SiblingCategories)
        {
            index++;
      %>
    <div class="ProductPropertyItem">
            <span class="MoveUp">
                <a class="MoveUp" href="javascript:MoveUp(<%=index %>);">Up</a>
            </span>
            <span class="MoveDown">
                <a class="MoveDown" href="javascript:MoveDown(<%=index %>);">Down</a>
            </span>
            <script type="text/javascript">
                catIds.push("<%= item.CategoryID%>");
            </script>
            <span class="CategoryName" id="CategoryName<%=index %>">
                <%= item.Name%>
            </span>
    </div>
            <%} %>
    <div class="ProductEdit_Save">
                    <span class="universal_button">
                        <span>
                            <asp:LinkButton ID="Button1" Text="Save" OnClick="SaveCategoriesRate" runat="server"/>
                        </span>
                    </span>
        
    </div>


</asp:Content>  




 