using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iStore.Admin.Products
{
    public partial class ProductsPhoto : System.Web.UI.Page
    {
        public BL.Modules.Products.Products pbl = new BL.Modules.Products.Products();
        public BL.Modules.Products.ProductProperies ppbl = new BL.Modules.Products.ProductProperies();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public BL.Product CuurentProduct
        {
            get
            {
                string spid = Request.QueryString["pid"];
                if (string.IsNullOrEmpty(spid))
                {
                    Response.Redirect(iStore.Site.SiteAdminUrl + "Products/?cid=" + Request.QueryString["cid"]);
                }
                Guid pid = Guid.NewGuid();
                try
                {
                    pid = new Guid(spid);
                }
                catch
                {
                    Response.Redirect(iStore.Site.SiteAdminUrl + "Products/?cid=" + Request.QueryString["cid"]);
                }
                BL.Product product = pbl.GetProductById(pid);
                if (product != null) return product;
                return null;
            }
        }

        public string CurrentProductPreview
        {
            get
            {
                BL.Product product = CuurentProduct;
                if (product != null)
                {
                    return iStore.Site.PreUrlProductPreviewImage + ppbl.GetProductPreviewByProductId(product.ProductID);
                }
                return string.Empty;
            }
        }

        public string CurrentProductOriginal
        {
            get
            {
                BL.Product product = CuurentProduct;
                if (product != null)
                {
                    return iStore.Site.PreUrlProductOriginalImage + ppbl.GetProductOriginalByProductId(product.ProductID);
                }
                return string.Empty;
            }
        }

        public string CurrentProductOriginal2
        {
            get
            {
                BL.Product product = CuurentProduct;
                if (product != null)
                {
                    return iStore.Site.PreUrlProductOriginalImage + ppbl.GetProductOriginal2ByProductId(product.ProductID);
                }
                return string.Empty;
            }
        }
        public string CurrentProductOriginal3
        {
            get
            {
                BL.Product product = CuurentProduct;
                if (product != null)
                {
                    return iStore.Site.PreUrlProductOriginalImage + ppbl.GetProductOriginal3ByProductId(product.ProductID);
                }
                return string.Empty;
            }
        }



        public string CurrentProductOriginalID
        {
            get
            {
                BL.Product product = CuurentProduct;
                if (product != null)
                {
                    return ppbl.GetProductOriginalIdByProductId(product.ProductID);
                }
                return string.Empty;
            }
        }

        public string CurrentProductOriginalID2
        {
            get
            {
                BL.Product product = CuurentProduct;
                if (product != null)
                {
                    return ppbl.GetProductOriginalId2ByProductId(product.ProductID);
                }
                return string.Empty;
            }
        }

        public string CurrentProductOriginalID3
        {
            get
            {
                BL.Product product = CuurentProduct;
                if (product != null)
                {
                    return ppbl.GetProductOriginalId3ByProductId(product.ProductID);
                }
                return string.Empty;
            }
        }

        object _OriginalCountObj;
        int _OriginalCount;
        protected int OriginalCount
        {
            get
            {
                if (_OriginalCountObj == null)
                {
                    _OriginalCountObj = new object();
                    _OriginalCount = ppbl.GetAllProperyByProductId(CuurentProduct.ProductID).Count(p => p.PropertyName == BL.ProductPropertyConstants.ProductPhotoOriginal);
                }
                return _OriginalCount;

            }
        }
        protected string CheckPreviewPhoto()
        {
            string result = string.Empty;
            string preUrl = Guid.NewGuid().ToString();
            if (upPreview.HasFile)
            {
                //filext.jpg .gif .png .jpeg
                string fileExt = System.IO.Path.GetExtension(upPreview.FileName);
                if (!string.IsNullOrEmpty(fileExt))
                {
                    fileExt = fileExt.ToLower();
                    if (fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".gif" || fileExt == ".png")
                    {
                        try
                        {
                            upPreview.SaveAs(iStore.Site.FileUploadProductPhotoPreview + preUrl + upPreview.FileName);
                            result = preUrl + upPreview.FileName;
                        }
                        catch (Exception ex)
                        {
                            ve.ClearErrors();
                            ve.Errors = "Не удалось сохранить фотографию. (Preview)";
                            ve.SetErrors();
                            ve.Visible = true;
                        }
                    }
                    else
                    {
                        ve.ClearErrors();
                        ve.Errors = "Не удалось сохранить файл. Неверный формат загружаемого файла. (Preview)";
                        ve.SetErrors();
                        ve.Visible = true;
                    }
                }
            }
            else
            {
                ve.ClearErrors();
                ve.Errors = "Файл не выбран. (Preview)";
                ve.SetErrors();
                ve.Visible = true;
            }
            return result;
        }

        protected void SavePreview(object sender, EventArgs e)
        {
            string result = CheckPreviewPhoto();
            if (!string.IsNullOrEmpty(result))
            {
                bool allRight = ppbl.UpdateProductPhotoPreview(result, CuurentProduct.ProductID);
                if (allRight)
                {
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            return;
        }

        protected void SaveOriginal(object sender, EventArgs e)
        {
            string result = CheckOriginalPhoto();
            if (!string.IsNullOrEmpty(result))
            {
                //bool allRight = ppbl.UpdateProductPhotoProperty(result, CuurentProduct.ProductID);
                string spropId = CurrentProductOriginalID;
                if (spropId == string.Empty)
                {
                    Guid productId = CuurentProduct.ProductID;
                    if (ppbl.AddProdProp("ProductPhotoOriginal", result, productId))
                    {
                        Response.Redirect(Request.Url.AbsoluteUri);
                    }
                }
                Guid propId = new Guid();
                try
                {
                    propId = new Guid(spropId);
                }
                catch
                {
                    ve.ClearErrors();
                    ve.Errors = "Файл не выбран. (Original)";
                    ve.SetErrors();
                    ve.Visible = true;
                    return;
                }
                bool allRight = ppbl.UpdateProductPhotoProperty(result, propId);
                if (allRight)
                {
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            return;
        }

        protected void SaveOriginal3(object sender, EventArgs e)
        {
            string result = CheckOriginalPhoto3();
            if (!string.IsNullOrEmpty(result))
            {
                string spropId = CurrentProductOriginalID3;
                if (spropId == string.Empty)
                {
                    Guid productId = CuurentProduct.ProductID;
                    if (ppbl.AddProdProp("ProductPhotoOriginal3", result, productId))
                    {
                        Response.Redirect(Request.Url.AbsoluteUri);
                    }
                    else
                    {
                        ve.ClearErrors();
                        ve.Errors = "Файл не выбран. (Original3)";
                        ve.SetErrors();
                        ve.Visible = true;
                        return;
                    }
                }
                Guid propId = new Guid();
                try
                {
                    propId = new Guid(spropId);
                }
                catch
                {
                    ve.ClearErrors();
                    ve.Errors = "Файл не выбран. (Original3)";
                    ve.SetErrors();
                    ve.Visible = true;
                    return;
                }
                bool allRight = ppbl.UpdateProductPhotoProperty(result, propId);
                if (allRight)
                {
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            return;
        }

        private string CheckOriginalPhoto3()
        {
            string result = string.Empty;
            string preUrl = Guid.NewGuid().ToString();
            if (upOriginal3.HasFile)
            {
                //filext.jpg .gif .png .jpeg
                string fileExt = System.IO.Path.GetExtension(upOriginal3.FileName);
                if (!string.IsNullOrEmpty(fileExt))
                {
                    fileExt = fileExt.ToLower();
                    if (fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".gif" || fileExt == ".png")
                    {
                        try
                        {
                            upOriginal3.SaveAs(iStore.Site.FileUploadProductPhotoOriginal + preUrl + upOriginal3.FileName);
                            result = preUrl + upOriginal3.FileName;
                        }
                        catch
                        {
                            ve.ClearErrors();
                            ve.Errors = "Не удалось сохранить фотографию. (Original3)";
                            ve.SetErrors();
                            ve.Visible = true;
                        }
                    }
                    else
                    {
                        ve.ClearErrors();
                        ve.Errors = "Не удалось сохранить файл. Неверный формат загружаемого файла. (Original3)";
                        ve.SetErrors();
                        ve.Visible = true;
                    }
                }
            }
            else
            {
                ve.ClearErrors();
                ve.Errors = "Файл не выбран. (Original3)";
                ve.SetErrors();
                ve.Visible = true;
            }
            return result;
        }

        protected void SaveOriginal2(object sender, EventArgs e)
        {
            string result = CheckOriginalPhoto2();
            if (!string.IsNullOrEmpty(result))
            {
                string spropId = CurrentProductOriginalID2;
                if (spropId == string.Empty)
                {
                    Guid productId = CuurentProduct.ProductID;
                    if (ppbl.AddProdProp("ProductPhotoOriginal2", result, productId))
                    {
                        Response.Redirect(Request.Url.AbsoluteUri);
                    }
                    else
                    {
                        ve.ClearErrors();
                        ve.Errors = "Файл не выбран. (Original2)";
                        ve.SetErrors();
                        ve.Visible = true;
                        return;
                    }
                }
                Guid propId = new Guid();
                try
                {
                    propId = new Guid(spropId);
                }
                catch
                {
                    ve.ClearErrors();
                    ve.Errors = "Файл не выбран. (Original2)";
                    ve.SetErrors();
                    ve.Visible = true;
                    return;
                }
                bool allRight = ppbl.UpdateProductPhotoProperty(result, propId);
                if (allRight)
                {
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            return;
        }

        private string CheckOriginalPhoto2()
        {
            string result = string.Empty;
            string preUrl = Guid.NewGuid().ToString();
            if (upOriginal2.HasFile)
            {
                //filext.jpg .gif .png .jpeg
                string fileExt = System.IO.Path.GetExtension(upOriginal2.FileName);
                if (!string.IsNullOrEmpty(fileExt))
                {
                    fileExt = fileExt.ToLower();
                    if (fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".gif" || fileExt == ".png")
                    {
                        try
                        {
                            upOriginal2.SaveAs(iStore.Site.FileUploadProductPhotoOriginal + preUrl + upOriginal2.FileName);
                            result = preUrl + upOriginal2.FileName;
                        }
                        catch
                        {
                            ve.ClearErrors();
                            ve.Errors = "Не удалось сохранить фотографию. (Original2)";
                            ve.SetErrors();
                            ve.Visible = true;
                        }
                    }
                    else
                    {
                        ve.ClearErrors();
                        ve.Errors = "Не удалось сохранить файл. Неверный формат загружаемого файла. (Original2)";
                        ve.SetErrors();
                        ve.Visible = true;
                    }
                }
            }
            else
            {
                ve.ClearErrors();
                ve.Errors = "Файл не выбран. (Original2)";
                ve.SetErrors();
                ve.Visible = true;
            }
            return result;
        }

        private string CheckOriginalPhoto()
        {
            string result = string.Empty;
            string preUrl = Guid.NewGuid().ToString();
            if (upOriginal.HasFile)
            {
                //filext.jpg .gif .png .jpeg
                string fileExt = System.IO.Path.GetExtension(upOriginal.FileName);
                if (!string.IsNullOrEmpty(fileExt))
                {
                    fileExt = fileExt.ToLower();
                    if (fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".gif" || fileExt == ".png")
                    {
                        try
                        {
                            upOriginal.SaveAs(iStore.Site.FileUploadProductPhotoOriginal + preUrl + upOriginal.FileName);
                            result = preUrl + upOriginal.FileName;
                        }
                        catch (Exception ex)
                        {
                            ve.ClearErrors();
                            ve.Errors = "Не удалось сохранить фотографию. (Original)";
                            ve.SetErrors();
                            ve.Visible = true;
                        }
                    }
                    else
                    {
                        ve.ClearErrors();
                        ve.Errors = "Не удалось сохранить файл. Неверный формат загружаемого файла. (Original)";
                        ve.SetErrors();
                        ve.Visible = true;
                    }
                }
            }
            else
            {
                ve.ClearErrors();
                ve.Errors = "Файл не выбран. (Original)";
                ve.SetErrors();
                ve.Visible = true;
            }
            return result;
        }
    }
}