<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pager.ascx.cs" Inherits="iStore.Modules.Controls.Pager.Pager" %>

    <div class="Paging" >

<%
    int i;
    if(PagesCount > 1){
    if (PageIndex !=0)
  { %>
  <span class="NavigateLastPage"><a href ="<%=NavigateUrl %>?p=0<%=Params%>">first</a></span>

  <span class="NavigateLastPage"><a href ="<%=NavigateUrl %>?p=<%=(PageIndex-1).ToString() + Params%>">&larr;</a></span>
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
              <span class="NeigbourPageNumber"><a href="<%=NavigateUrl %>?p=<%= i.ToString() + Params%>"> <%= (i + 1).ToString()%></a> </span>
              <%}
        }
        if (PageIndex != PagesCount - 1)
  {if (LastIdnex != PagesCount)
    { %>
    <span>...</span>
    <%} %>
  <span class="NavigateLastPage"><a href ="<%=NavigateUrl %>?p=<%=(PageIndex+1).ToString() + Params%>">&rarr;</a></span>
  <span class="NavigateLastPage"><a href ="<%=NavigateUrl %>?p=<%=(PagesCount-1).ToString() + Params%>">last</a></span>
  
    <%  
  }    
  }
  %>
        
    </div>