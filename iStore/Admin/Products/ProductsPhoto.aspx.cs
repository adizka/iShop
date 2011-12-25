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

        protected  void SavePreview(object sender, EventArgs e)
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
                bool allRight = ppbl.UpdateProductPhotoOriginal(result, CuurentProduct.ProductID);
                if (allRight)
                {
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            return;    
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