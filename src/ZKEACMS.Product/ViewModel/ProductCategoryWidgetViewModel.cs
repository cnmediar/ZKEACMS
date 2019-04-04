/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;
using ZKEACMS.Product.Models;

namespace ZKEACMS.Product.ViewModel
{
    public class ProductCategoryWidgetViewModel
    {
        public IEnumerable<ProductCategory> BigCategorys { get; set; }

        public IEnumerable<ProductCategory> Categorys { get; set; }
        public int CurrentCategory { get; set; }
    }
}