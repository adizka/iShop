<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu2.ascx.cs" Inherits="iStore.Design.Menu2" %>
<script type="text/javascript" src="<%= iStore.Site.SiteUrl %>Scripts/Menu.js"></script>


<div class="Menu">
<%-- 1--%>
    <div class="MenuItemFirst">
        <% int i = -1; string topStyle = string.Empty;  
        foreach (var item in ParentCategory) 
        {   i++; topStyle = "top:" + (20 * i).ToString() + "px"; %>
            <div class="MenuHoverFirst" id="<%= item.CategoryID.ToString() %>">
                <a href="<%= iStore.Site.SiteUrl %>Categories/?cid=<%= item.CategoryID.ToString() %>"><%= item.Name %></a>
            </div>
            <%--2--%>
            <% var secondCategories = GetCategoryByParent(item.CategoryID);
            if (secondCategories.Count() > 0) 
            { %>
               <div id="div<%= item.CategoryID.ToString() %>" class="MenuItemSecond" style="<%= topStyle %>">
                   <% int j = -1; string topstyle2 = string.Empty; 
                    foreach (var item2 in GetCategoryByParent(item.CategoryID))
                    {  j++; topstyle2 = "top:" + ((20*j)).ToString() + "px"; %>
                    <div class="MenuHoverSecond" id="<%= item2.CategoryID.ToString() %>">
                        <a href="<%=iStore.Site.SiteUrl%>Categories/?cid=<%=item2.CategoryID.ToString()%>"><%=item2.Name%></a>
                    </div>
                    <%--3--%>
                    <% var thirdCategories = GetCategoryByParent(item2.CategoryID);
                       if (thirdCategories.Count() > 0)
                       { %>
                            <div id="div<%= item2.CategoryID.ToString() %>" class="MenuItemThird" style="<%= topstyle2 %>">
                                <% foreach (var item3 in thirdCategories) {%>
                                    <div class="MenuHoverThird" id="<%= item3.CategoryID.ToString() %>">
                                        <a href="<%= iStore.Site.SiteUrl %>Categories/?cid=<%= item3.CategoryID.ToString() %>"><%= item3.Name %></a>
                                    </div>
                                <% } %>
                            </div>
                       <% } %>
                    <%--end 3--%>
                    <% } %>
               </div>
           <% } %>
           <%--end 2--%>
    <% } %>
    </div>
<%--end 1--%>
</div>