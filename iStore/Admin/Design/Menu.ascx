<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="iStore.Admin.Design.Menu" %>


<div class="admin_Menu">
     <script type="text/javascript">
         $(document).ready(function () {
             $('.acc_container').hide(); //Hide/close all containers
             var url = $(location).attr('href');
             var set = 0;
             if (url.indexOf('Admin/Pages') != -1) {
                 $('#admin_Pages').addClass('active').next().show();
                 set = -1;
             }
             if (url.indexOf('Admin/News') != -1) {
                 $('#admin_News').addClass('active').next().show();
                 set = -1;
             }

             if (url.indexOf('Admin/Categories') != -1) {
                 $('#admin_Categories').addClass('active').next().show();
                 set = -1;
             }
             if (url.indexOf('Admin/Products') != -1) {
                 $('#admin_Products').addClass('active').next().show();
                 set = -1;
             }
             if (set == 0) {
                 $('.acc_trigger:first').addClass('active').next().show(); //Add "active" class to first trigger, then show/open the immediate next container
             }
             $('.acc_trigger').click(function () {
                 if ($(this).next().is(':hidden')) { //If immediate next container is closed...
                     $('.acc_trigger').removeClass('active').next().slideUp(); //Remove all .acc_trigger classes and slide up the immediate next container
                     $(this).toggleClass('active').next().slideDown(); //Add .acc_trigger class to clicked trigger and slide down the immediate next container
                 }
                 return false;
             });
         });
     </script>
<div class="container"> 
 
	<h2 class="acc_trigger"><a href="#"><%= global::Resources.Shops.Orders %></a></h2> 
	<div class="acc_container"> 
		<div class="block"> 
			 <p>
                <ul>
                    <li><a href="#"><%= global::Resources.Shops.OrdersList %></a></li>
                    <li><a href="#"><%= global::Resources.Shops.OrdersHistory %></a></li>
                </ul>
		     </p>
        </div> 
	</div> 
	
	<h2 class="acc_trigger" id="admin_Pages"><a href="#"><%= global::Resources.Shops.Pages %></a></h2> 
	<div class="acc_container"> 
		<div class="block"> 
			 <p>
		        <ul>
                    <li><a href="<%= iStore.Site.SiteAdminUrl %>Pages/"><%= global::Resources.Shops.PagesList %></a></li>
                    <li><a href="<%= iStore.Site.SiteAdminUrl %>Pages/PageEdit.aspx"><%= global::Resources.Shops.AddPage %></a></li>
                </ul>
            </p>
        </div> 
	</div> 
	
    <h2 class="acc_trigger" id="admin_News"><a href="#"><%= global::Resources.Shops.News %></a></h2> 
	<div class="acc_container"> 
		<div class="block"> 
	        <p>
                <ul>
                    <li><a href="<%= iStore.Site.SiteAdminUrl %>News/"><%= global::Resources.Shops.NewsList %></a></li>
                    <li><a href="<%= iStore.Site.SiteAdminUrl %>News/NewsEdit.aspx"><%= global::Resources.Shops.AddNews %></a></li>
                </ul>
		    </p>
        </div> 
	</div> 
	
	<h2 class="acc_trigger" id="admin_Products"><a href="#"><%= global::Resources.Shops.Products %></a></h2> 
	<div class="acc_container"> 
		<div class="block"> 
			<p>
		        <ul>
                    <li><a href="<%= iStore.Site.SiteAdminUrl %>Products/"><%= global::Resources.Shops.ProductsList %></a></li>
                    <li><a href="<%= iStore.Site.SiteAdminUrl %>Products/ProductEdit.aspx"><%= global::Resources.Shops.AddProducts %></a></li>
                </ul>
		    </p>
       </div> 
	</div> 
    <h2 class="acc_trigger" id="admin_Categories"><a href="#"><%= global::Resources.Shops.Categories %></a></h2> 
	<div class="acc_container"> 
		<div class="block"> 
			<p>
		        <ul>
                    <li><a href="<%= iStore.Site.SiteAdminUrl %>Categories/"><%= global::Resources.Shops.CategoriesList %></a></li>
                    <li><a href="<%= iStore.Site.SiteAdminUrl %>Categories/"><%= global::Resources.Shops.AddCategories %></a></li>
                </ul>
		    </p>
        </div> 
	</div> 
    <h2 class="acc_trigger"><a href="#"><%= global::Resources.Shops.Users %></a></h2> 
	<div class="acc_container"> 
		<div class="block"> 
			<p>
		        <ul>
                    <li><a href="#"><%= global::Resources.Shops.Statistics %></a></li>
                </ul>
		    </p>
        </div> 
	</div> 
    <h2 class="acc_trigger"><a href="#"><%= global::Resources.Shops.Payment %></a></h2> 
	<div class="acc_container"> 
		<div class="block"> 
        </div> 
	</div> 
    <h2 class="acc_trigger"><a href="#"><%= global::Resources.Shops.Delivery %></a></h2> 
	<div class="acc_container"> 
		<div class="block"> 
        </div> 
	</div>
    </div> 
    <h2 class="acc_trigger"><a href="#"><%= global::Resources.Shops.ShopProperties %></a></h2> 
	<div class="acc_container"> 
		<div class="block"> 
        </div> 
	</div>
    
	
</div> 
</div>

