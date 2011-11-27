<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pager.ascx.cs" Inherits="iStore.Admin.Pager" %>

    <div class="Paging" >

<%
    int i;
    if(EntityCount != 0){
    if (PageIndex !=0)
  { %>
  <span class="NavigateLastPage"><a href ="<%=NavigateUrl %>"?p=0&<%=AddInsParams%>">first</a></span>
  <span class="NavigateLastPage"><a href ="<%=NavigateUrl %>?p=<%=(PageIndex-1).ToString() %>&<%=AddInsParams%>">prev</a></span>
  <%if (FirstIdnex != 0)
    { %>
    <span>...</span>
    <%  }
  }          
        for (i = FirstIdnex; i < LastIdnex; i++)
        {
            if (i == PageIndex)
            { %>
            <span class="CurrentPageNumber"> <%= (i + 1).ToString()%> </span>
            <%}
            else
            { %>
              <span class="NeigbourPageNumber"><a href="<%=NavigateUrl %>?p=<%= i.ToString() %>&<%=AddInsParams%>"> <%= (i + 1).ToString()%></a> </span>
              <%}
        }
        if (PageIndex != PagesCount - 1)
  {if (LastIdnex != PagesCount)
    { %>
    <span>...</span>
    <%} %>
  <span class="NavigateLastPage"><a href ="<%=NavigateUrl %>?p=<%=(PageIndex+1).ToString() %>&<%=AddInsParams%>">next</a></span>
  <span class="NavigateLastPage"><a href ="<%=NavigateUrl %>?p=<%=(PagesCount-1).ToString() %>&<%=AddInsParams%>">last</a></span>
  
    <%  
  }    
  }
  %>
        
    </div>