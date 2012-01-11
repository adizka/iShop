<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="iStore.Products.test" MasterPageFile="~/Page.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="../Scripts/fancybox/jquery.mousewheel-3.0.4.pack.js" type="text/javascript"></script>
    <script src="../Scripts/fancybox/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <link href="../Scripts/fancybox/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" />
 	<style type="text/css">
 	    html, body, div, ul {
	margin: 0;
	padding: 0;
}

body {
    color: #262626;
	background: #f4f4f4;
	font: normal 12px/18px Verdana, sans-serif;
}

#content {
	width: 400px;
	margin: 40px auto 0 auto;
	padding: 0 60px 30px 60px;
	border: solid 1px #cbcbcb;
	background: #fafafa;
	-moz-box-shadow: 0px 0px 10px #cbcbcb;
	-webkit-box-shadow: 0px 0px 10px #cbcbcb;
}

h1 {
	margin: 30px 0 15px 0;
	font-size: 30px;
	font-weight: bold;
	font-family: Arial;
}

h1 span {
	font-size: 50%;
	letter-spacing: -0.05em;
}

hr {
	border: none;
	height: 1px; line-height: 1px;
	background: #E5E5E5;
	margin-bottom: 20px;
	padding: 0;
}

p {
	margin: 0;
	padding: 7px 0;
}

a {
	outline: none;
}

a img {
	border: 1px solid #BBB;
	padding: 2px;
	margin: 10px 20px 10px 0;
	vertical-align: top;
}

a img.last {
	margin-right: 0;	
}

ul {
	margin-bottom: 24px;
	padding-left: 30px;
}

    </style>
	<script type="text/javascript">
	    $(document).ready(function () {
	        /*
	        *   Examples - images
	        */

	        $("a#example1").fancybox();

	        $("a#example2").fancybox({
	            'overlayShow': false,
	            'transitionIn': 'elastic',
	            'transitionOut': 'elastic'
	        });

	        $("a#example3").fancybox({
	            'transitionIn': 'none',
	            'transitionOut': 'none'
	        });

	        $("a#example4").fancybox({
	            'opacity': true,
	            'overlayShow': false,
	            'transitionIn': 'elastic',
	            'transitionOut': 'none'
	        });

	        $("a#example5").fancybox();

	        $("a#example6").fancybox({
	            'titlePosition': 'outside',
	            'overlayColor': '#000',
	            'overlayOpacity': 0.9
	        });

	        $("a#example7").fancybox({
	            'titlePosition': 'inside'
	        });

	        $("a#example8").fancybox({
	            'titlePosition': 'over'
	        });

	        $("a[rel=example_group]").fancybox({
	            'transitionIn': 'none',
	            'transitionOut': 'none',
	            'titlePosition': 'over',
	            'titleFormat': function (title, currentArray, currentIndex, currentOpts) {
	                return '<span id="fancybox-title-over">Image ' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? ' &nbsp; ' + title : '') + '</span>';
	            }
	        });

	        /*
	        *   Examples - various
	        */

	        $("#various1").fancybox({
	            'titlePosition': 'inside',
	            'transitionIn': 'none',
	            'transitionOut': 'none'
	        });

	        $("#various2").fancybox();

	        $("#various3").fancybox({
	            'width': '75%',
	            'height': '75%',
	            'autoScale': false,
	            'transitionIn': 'none',
	            'transitionOut': 'none',
	            'type': 'iframe'
	        });

	        $("#various4").fancybox({
	            'padding': 0,
	            'autoScale': false,
	            'transitionIn': 'none',
	            'transitionOut': 'none'
	        });
	    });
	</script>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<div id="content">
	<h1>fancybox <span>v1.3.4</span></h1>

	<p>This is a demonstration. <a href="http://fancybox.net">Home page</a></p>

	<hr />

	<p>
		Different animations<br />

		<a id="example1" href="<%= iStore.Site.SiteUrl %>Images/ex/1_b.jpg"><img alt="example1" src="<%= iStore.Site.SiteUrl %>Images/ex/1_s.jpg" /></a>

		<a id="example2" href="<%= iStore.Site.SiteUrl %>Images/ex/2_b.jpg"><img alt="example2" src="<%= iStore.Site.SiteUrl %>Images/ex/2_s.jpg" /></a>

		<a id="example3" href="<%= iStore.Site.SiteUrl %>Images/ex/3_b.jpg"><img alt="example3" src="<%= iStore.Site.SiteUrl %>Images/ex/3_s.jpg" /></a>
		
		<a id="example4" href="<%= iStore.Site.SiteUrl %>Images/ex/4_b.jpg"><img class="last" alt="example4" src="<%= iStore.Site.SiteUrl %>Images/ex/4_s.jpg" /></a>
	</p>

	<p>
		Different title positions<br />

		<a id="example5" href="<%= iStore.Site.SiteUrl %>Images/ex/5_b.jpg" title="Lorem ipsum dolor sit amet, consectetur adipiscing elit."><img alt="example4" src="<%= iStore.Site.SiteUrl %>Images/ex/5_s.jpg" /></a>
		
		<a id="example6" href="<%= iStore.Site.SiteUrl %>Images/ex/6_b.jpg" title="Etiam quis mi eu elit tempor facilisis id et neque. Nulla sit amet sem sapien. Vestibulum imperdiet porta ante ac ornare. Vivamus fringilla congue laoreet."><img alt="example5" src="<%= iStore.Site.SiteUrl %>Images/ex/6_s.jpg" /></a>

		<a id="example7" href="<%= iStore.Site.SiteUrl %>Images/ex/7_b.jpg" title="Cras neque mi, semper at interdum id, dapibus in leo. Suspendisse nunc leo, eleifend sit amet iaculis et, cursus sed turpis."><img alt="example6" src="<%= iStore.Site.SiteUrl %>Images/ex/7_s.jpg" /></a>

		<a id="example8" href="<%= iStore.Site.SiteUrl %>Images/ex/8_b.jpg" title="Sed vel sapien vel sem tempus placerat eu ut tortor. Nulla facilisi. Sed adipiscing, turpis ut cursus molestie, sem eros viverra mauris, quis sollicitudin sapien enim nec est. ras pulvinar placerat diam eu consectetur."><img class="last" alt="example7" src="<%= iStore.Site.SiteUrl %>Images/ex/8_s.jpg" /></a>
	</p>

	<p>
		Image gallery (ps, try using mouse scroll wheel)<br />

		<a rel="example_group" href="<%= iStore.Site.SiteUrl %>Images/ex/9_b.jpg" title="Lorem ipsum dolor sit amet"><img alt="" src="<%= iStore.Site.SiteUrl %>Images/ex/9_s.jpg" /></a>

		<a rel="example_group" href="<%= iStore.Site.SiteUrl %>Images/ex/10_b.jpg" title=""><img alt="" src="<%= iStore.Site.SiteUrl %>Images/ex/10_s.jpg" /></a>

		<a rel="example_group" href="<%= iStore.Site.SiteUrl %>Images/ex/11_b.jpg" title=""><img alt="" src="<%= iStore.Site.SiteUrl %>Images/ex/11_s.jpg" /></a>
		
		<a rel="example_group" href="<%= iStore.Site.SiteUrl %>Images/ex/12_b.jpg" title=""><img class="last" alt="" src="<%= iStore.Site.SiteUrl %>Images/ex/12_s.jpg" /></a>
	</p>

	<p>
		Various examples
	</p>

	<ul>
		<li><a id="various1" href="#inline1" title="Lorem ipsum dolor sit amet">Inline</a></li>
		<li><a id="various2" href="ajax.txt">Ajax</a></li>
		<li><a id="various3" href="http://google.ca">Iframe</a></li>
		<li><a id="various4" href="http://www.adobe.com/jp/events/cs3_web_edition_tour/swfs/perform.swf">Swf</a></li>
	</ul>

	<div style="display: none;">
		<div id="inline1" style="width:400px;height:100px;overflow:auto;">
			Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam quis mi eu elit tempor facilisis id et neque. Nulla sit amet sem sapien. Vestibulum imperdiet porta ante ac ornare. Nulla et lorem eu nibh adipiscing ultricies nec at lacus. Cras laoreet ultricies sem, at blandit mi eleifend aliquam. Nunc enim ipsum, vehicula non pretium varius, cursus ac tortor. Vivamus fringilla congue laoreet. Quisque ultrices sodales orci, quis rhoncus justo auctor in. Phasellus dui eros, bibendum eu feugiat ornare, faucibus eu mi. Nunc aliquet tempus sem, id aliquam diam varius ac. Maecenas nisl nunc, molestie vitae eleifend vel, iaculis sed magna. Aenean tempus lacus vitae orci posuere porttitor eget non felis. Donec lectus elit, aliquam nec eleifend sit amet, vestibulum sed nunc.
		</div>
	</div>

	<p>
		Ajax example will not run from your local computer and requires a server to run.
	</p>
	<p>
		Photo Credit: <a href="http://www.flickr.com/people/kharied/">Katie Harris</a>
	</p>
</div>
</asp:Content>