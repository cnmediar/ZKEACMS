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

namespace ZKEACMS.Product.Service
{
    public class ProductCategoryWidgetService : WidgetService<ProductCategoryWidget>
    {
        private const string ProductCategoryWidgetRelatedPageUrls = "ProductCategoryWidgetRelatedPageUrls";
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;
        public ProductCategoryWidgetService(IWidgetBasePartService widgetService, IProductService productService,
            IProductCategoryService productCategoryService, IApplicationContext applicationContext, CMSDbContext dbContext)
            : base(widgetService, applicationContext, dbContext)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
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
            ProductCategoryWidget currentWidget = widget as ProductCategoryWidget;
            int cate = actionContext.RouteData.GetCategory();
            int bigCate = 0;
            ProductCategory productCategory = null;

            IEnumerable<ProductEntity> products = null;

            Expression<Func<ProductEntity, bool>> filter = null;
            if (cate != 0)
            {
                filter = m => m.IsPublish && m.ProductCategoryID == cate;
            }
            else
            {
                var ids = _productCategoryService.Get(m => m.ID == currentWidget.ProductCategoryID || m.ParentID == currentWidget.ProductCategoryID).Select(m => m.ID).ToList();
                filter = m => m.IsPublish && ids.Contains(m.ProductCategoryID);
            }

            products = _productService.Get().Where(filter).OrderBy(m => m.OrderIndex).ThenByDescending(m => m.ID).ToList();


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
    }
}