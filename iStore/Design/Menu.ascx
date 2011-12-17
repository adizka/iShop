<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="iStore.Design.Menu" %>

<style type="text/css">
.itemA {  }
</style>


<script type="text/javascript">
    $(document).ready(function () {
        $(".item").addClass("hover");
        $(".wpItem").addClass("hover");
        $(".pItem").addClass("hover");
        var firstId = "#UL" + "<%= FirstElementId %>";
        $(firstId).removeClass("hover");
        //First
        $(".itemA").hover(function () {
            $(".item").addClass("hover");
            $(".wpItem").addClass("hover");
            $(".pItem").addClass("hover");
            var ids = "#UL" + $(this).attr("id");
            $(ids).removeClass("hover");
        });
    });
</script>

<%--Вложенность 1--%>
<ul>
<% foreach (BL.Category item in parentCategory)
{ %>
    <li>
        <a href="<%= iStore.Site.SiteUrl %>Categories/?cid=<%= item.CategoryID.ToString() %>" class="itemA" id="<%= item.CategoryID.ToString() %>">
            <%= item.Name %>
        </a>
       
        <%--Вложенность 2--%>
        <ul id="UL<%= item.CategoryID.ToString() %>" class="item">
        <% foreach (BL.Category pItem in GetCategoryByParent(item.CategoryID))
        {  %> 
            <li> 
                 <a href="<%= iStore.Site.SiteUrl %>Categories/?cid=<%= pItem.CategoryID.ToString() %>">
                    <%= pItem.Name %>
                </a>
                <%--Вложенность 3--%>
                <ul id="UL<%= pItem.CategoryID.ToString() %>" class="pItem">
                    <% foreach (var wpItem in GetCategoryByParent(pItem.CategoryID))
                    { %>
                        <li>
                             <a href="<%= iStore.Site.SiteUrl %>Categories/?cid=<%= wpItem.CategoryID.ToString() %>">
                                <%= wpItem.Name%>
                            </a>      
                            
                            <%--Вложенность 4--%>
                            <ul id="UL<%= wpItem.CategoryID.ToString() %>" class="wpItem">
                            <% foreach (var tpItem in GetCategoryByParent(wpItem.CategoryID))
                            { %>
                                <li>
                                    <a href="<%= iStore.Site.SiteUrl %>Categories/?cid=<%= tpItem.CategoryID.ToString() %>">
                                         <%= tpItem.Name%>
                                    </a>      
                                </li>
                            <% } %>
                            </ul>
                        </li> 
                    <% } %>
                </ul>
            </li>
        <% } %>
        </ul>
    </li>

<% } %>
</ul>