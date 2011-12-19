<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="iStore.Design.Menu" %>

<script type="text/javascript">
    $(document).ready(function () {
        $(".item").addClass("hover");
        $(".pItem").addClass("hover");
        var firstId = "#UL" + "<%= FirstElementId %>";
        $(firstId).removeClass("hover");
        //First
        $(".itemA").click(function () {
            $(".item").addClass("hover");
            $(".pItem").addClass("hover");
            var ids = "#UL" + $(this).attr("id");
            $(ids).removeClass("hover");
            $(this).removeClass("Plus");
            $(this).addClass("Minus");
        });
    });
</script>

<%--Вложенность 1--%>
<div class="Menu">
<ul>
<% foreach (BL.Category item in parentCategory)
{ %>
    <li>
        <div class="MenuItem">
            <div class="MOpen Plus itemA"></div>
            <a href="<%= iStore.Site.SiteUrl %>Categories/?cid=<%= item.CategoryID.ToString() %>" class="MenuLink" id="<%= item.CategoryID.ToString() %>">
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
                    <div class="MOpen Plus"></div>
                    <%--Вложенность 3--%>
                    <ul id="UL<%= pItem.CategoryID.ToString() %>" class="pItem">
                        <% foreach (var wpItem in GetCategoryByParent(pItem.CategoryID))
                        { %>
                            <li>
                                 <a href="<%= iStore.Site.SiteUrl %>Categories/?cid=<%= wpItem.CategoryID.ToString() %>">
                                    <%= wpItem.Name%>
                                </a>
                            </li> 
                        <% } %>
                    </ul>
                </li>
            <% } %>
            </ul>
        </div><br />
    </li>

<% } %>
</ul>
</div>