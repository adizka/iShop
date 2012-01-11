<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Page.Master"  CodeBehind="Test.aspx.cs" Inherits="iStore.Test" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Scripts/jquery-lightbox-0.5/css/jquery.lightbox-0.5.css" rel="stylesheet"
        type="text/css" />
    <script src="Scripts/jquery-lightbox-0.5/js/jquery.lightbox-0.5.js" type="text/javascript"></script>
    <script src="Scripts/jquery-lightbox-0.5/js/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $('#gallery a').lightBox();
        });
    </script>
   	<style type="text/css">
	/* jQuery lightBox plugin - Gallery style */
	#gallery {
		background-color: #444;
		padding: 10px;
		width: 520px;
	}
	#gallery ul { list-style: none; }
	#gallery ul li { display: inline; }
	#gallery ul img {
		border: 5px solid #3e3e3e;
		border-width: 5px 5px 20px;
	}
	#gallery ul a:hover img {
		border: 5px solid #fff;
		border-width: 5px 5px 20px;
		color: #fff;
	}
	#gallery ul a:hover { color: #fff; }
	</style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">


    <div id="gallery">
         <a href="Scripts/jquery-lightbox-0.5/photos/image1.jpg" title="">
                <img src="Scripts/jquery-lightbox-0.5/photos/thumb_image1.jpg" width="72" height="72" alt="" />
         </a><br />
         <a href="Scripts/jquery-lightbox-0.5/photos/image2.jpg" title="">
                <img src="Scripts/jquery-lightbox-0.5/photos/thumb_image2.jpg" width="72" height="72" alt="" />
         </a><br />
         <a href="Scripts/jquery-lightbox-0.5/photos/image3.jpg" title="">
                <img src="Scripts/jquery-lightbox-0.5/photos/thumb_image3.jpg" width="72" height="72" alt="" />
         </a><br />
    </div>
</asp:Content>