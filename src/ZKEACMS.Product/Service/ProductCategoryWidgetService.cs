/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */

using ZKEACMS.Product.Models;
using ZKEACMS.Product.ViewModel;
using Easy.Extend;
using ZKEACMS.Widget;
using Microsoft.AspNetCore.Http;
using Easy;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Threading;

namespace ZKEACMS.Product.Service
{
    public class ProductCategoryWidgetService : WidgetService<ProductCategoryWidget>
    {
        private const string ProductCategoryWidgetRelatedPageUrls = "ProductCategoryWidgetRelatedPageUrls";
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;

        private readonly IHostingEnvironment _hostingEnvironment;

     

        public ProductCategoryWidgetService(IWidgetBasePartService widgetService, IProductService productService,
            IProductCategoryService productCategoryService, IApplicationContext applicationContext, CMSDbContext dbContext, IHostingEnvironment hostingEnvironment)
            : base(widgetService, applicationContext, dbContext)
        {
            _productService = productService;
            _productCategoryService = productCategoryService; _hostingEnvironment = hostingEnvironment;
        }
        private void DismissRelatedPageUrls()
        {
            ProductPlug.AllRelatedUrlCache.TryRemove(ProductCategoryWidgetRelatedPageUrls, out var urls);
        }
        public override void AddWidget(WidgetBase widget)
        {
            base.AddWidget(widget);
            DismissRelatedPageUrls();
        }

        public override void DeleteWidget(string widgetId)
        {
            base.DeleteWidget(widgetId);
            DismissRelatedPageUrls();
        }
        public override WidgetViewModelPart Display(WidgetBase widget, ActionContext actionContext)
        {
            //insertProdut();
            ProductCategoryWidget currentWidget = widget as ProductCategoryWidget;
            int cate = actionContext.RouteData.GetCategory();
            int bigCate = 0;
            ProductCategory productCategory = null;

            IEnumerable<ProductEntity> products = null;

            Expression<Func<ProductEntity, bool>> filter = null;
            if (cate != 0)
            {
                filter = m => m.IsPublish && m.ProductCategoryID == cate;
                products = _productService.Get().Where(filter).OrderBy(m => m.OrderIndex).ThenByDescending(m => m.ID).ToList();
            }
            else
            {
             var aa =  _productService.Get().GroupBy(m => m.ProductCategoryID);
             

                products = _productService.Get().ToList().GroupBy(m=>m.ProductCategoryID).Select(m=>m.FirstOrDefault()).ToList();
            }

            


            if (cate > 0)
            {
                productCategory = _productCategoryService.Get(cate);

                bigCate = productCategory.ParentID == 0 ? cate : productCategory.ParentID;
            }
            if (actionContext.RouteData.GetCategoryUrl().IsNullOrEmpty() && productCategory != null)
            {
                if (productCategory.Url.IsNotNullAndWhiteSpace())
                {
                    actionContext.RedirectTo($"{actionContext.RouteData.GetPath()}/{productCategory.Url}", true);
                }
            }
            if (productCategory != null)
            {
                var layout = actionContext.HttpContext.GetLayout();
                if (layout != null && layout.Page != null)
                {
                    layout.Page.Title = productCategory.SEOTitle ?? productCategory.Title;
                    layout.Page.MetaKeyWorlds = productCategory.SEOKeyWord;
                    layout.Page.MetaDescription = productCategory.SEODescription;
                }
            }
            return widget.ToWidgetViewModelPart(new ProductCategoryWidgetViewModel
            {
                Categorys = _productCategoryService.Get(m => m.Status == 1),
                BigCategorys = _productCategoryService.Get(m => m.Status == 1 && m.ParentID == 0),

                // Categorys = _productCategoryService.Get(m => m.ParentID == cate),
                CurrentCategory = cate,
                CurrentBigCategory = bigCate,
                 Products= products

            });
        }

        public string[] GetRelatedPageUrls()
        {
            return ProductPlug.AllRelatedUrlCache.GetOrAdd(ProductCategoryWidgetRelatedPageUrls, fac =>
            {
                var pages = WidgetBasePartService.Get(w => Get().Select(m => m.ID).Contains(w.ID)).Select(m => m.PageID).ToArray();
                return DbContext.Page.Where(p => pages.Contains(p.ID)).Select(m => m.Url.Replace("~/", "/")).Distinct().ToArray();
            });
        }

        public void insertProdut()
        {

           


          string[] d=  Directory.GetDirectories (Path.Combine(_hostingEnvironment.WebRootPath, @"UpLoad\product"));

            foreach (var item in d)
            {
                string[] sds = Directory.GetDirectories(item);

                foreach (var sd in sds)
                {
                    string[] fs = System.IO.Directory.GetFiles(sd);

                    DirectoryInfo dinfo = new DirectoryInfo(sd);
                    try
                    {
                        var cat = _productCategoryService.GetSingle(n => n.Title == dinfo.Name);

                        if (cat != null)
                        {

                            foreach (var f in fs)
                            {

                                var finfo = new FileInfo(f);
                                string path = finfo.FullName.Substring(finfo.FullName.IndexOf("UpLoad"));
                                path = path.Replace('\\', '/');

                                int c = _productService.Count(n => n.Title == finfo.Name);

                                if (c == 0)
                                {
                                    string tick = DateTime.Now.Ticks.ToString();
                                    var filepath = Path.Combine(_hostingEnvironment.WebRootPath, @"UpLoad\productImages", tick) + finfo.Extension;

                                    Thread.Sleep(1);

                                    var webpath = filepath.Substring(filepath.IndexOf("UpLoad"));
                                    webpath = webpath.Replace('\\', '/');


                                    File.Copy(finfo.FullName, filepath);

                                    _productService.Add(new ProductEntity
                                    {
                                        Title = finfo.Name,
                                        ProductCategoryID = cat.ID,
                                        IsPublish = true,
                                        CreateDate = DateTime.Now,
                                        Status = 1,
                                        ImageThumbUrl = webpath,
                                        ImageUrl = webpath,
                                        PartNumber = tick
                                    });

                                }

                            }
                        }
                    }
                    catch (Exception)
                    {

                        ;
                    }
                 
                } 
               

            }       

           

        }

    }
}