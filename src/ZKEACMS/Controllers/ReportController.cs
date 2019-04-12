using Easy;
using Easy.Constant;
using Easy.Models;
using Easy.Modules.User.Models;
using Easy.Modules.User.Service;
using Easy.Mvc.Authorize;
using Easy.Mvc.Controllers;
using Easy.RepositoryPattern;
using EasyFrameWork.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ZKEACMS.Report;
using ZKEACMS.Shop.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;

namespace ZKEACMS.Controllers
{
    [CustomerAuthorize]
    public class ReportController : BasicController<AuditReportEntity, int, IReportServer>
    {
        private IApplicationContextAccessor _applicationContextAccessor;
        private ILocalize _localize;

        private IReportServer _reportServer;
        private IHostingEnvironment environment;
        private FileExtensionContentTypeProvider contentTypeProvider;
        public ReportController(IReportServer userService, IApplicationContextAccessor applicationContextAccessor, ILocalize localize,
             IHostingEnvironment environment
     
            )
            : base(userService)
        {
            _applicationContextAccessor = applicationContextAccessor;
            _localize = localize;
            _reportServer = userService;
            this.environment = environment;
            this.contentTypeProvider = new FileExtensionContentTypeProvider(); 

           // _contentTypeProvider = options.Value.ContentTypeProvider ?? new FileExtensionContentTypeProvider();
        }

    


        public override IActionResult Index()
        {
            int Id = 0;
            ReportListViewModel viewModel = new ReportListViewModel();
            Pagin pagin = new Pagin { PageIndex = Id, OrderByDescending = "CreateDate" };
            viewModel.Reports = _reportServer.Get(m => m.UserId == _applicationContextAccessor.Current.CurrentCustomer.UserID, pagin);
            viewModel.Pagin = pagin;
            viewModel.Pagin.BaseUrlFormat = "~/MyOrder/Index/{0}";
            return View(viewModel);

         
        }
     
        public override IActionResult Create()
        {
            return base.Create();
        }
  
        public override IActionResult Create(AuditReportEntity entity)
        {
          entity.UserId = _applicationContextAccessor.Current.CurrentCustomer.UserID;


            entity.LastUpdateByName = _applicationContextAccessor.Current.CurrentCustomer.UserID;
            entity.CreatebyName = _applicationContextAccessor.Current.CurrentCustomer.UserID;
            entity.Status = 2;
            //entity.Title=


            var result = Service.Add(entity);
       
            

           // var result = base.Create(entity);
            return     View("CreateSuccess"); 
        }
        [DefaultAuthorize(Policy = PermissionKeys.ManageReport)]
        public override IActionResult Edit(int Id)
        {
            return base.Edit(Id);
        }
        [HttpPost, DefaultAuthorize(Policy = PermissionKeys.ManageReport)]
        public override IActionResult Edit(AuditReportEntity entity)
        {
            var result = base.Edit(entity);

            Service.Publish(entity);

            if (Request.Query["ReturnUrl"].Count > 0)
            {
                return Redirect(Request.Query["ReturnUrl"]);
            }
            return result;
        }


 

        public override IActionResult GetList(DataTableOption query)
        {
            return base.GetList(query);
        }

        public  IActionResult Search(ReportListViewModel rm)
        {
           
            DataTableOption query = new DataTableOption();
            var pagin = new Pagination { PageSize = query.Length, PageIndex = query.Start / query.Length };
            
            query.Columns = new ColumnOption[] {
                new ColumnOption {
                 Name="FactoryName",
                  Data="FactoryName",
                    Search=new SearchOption {
                         Opeartor=  Easy.LINQ.Query.Operators.Contains,
                          Value=rm.Keyword
                    }
            },

          new ColumnOption {
                 Name="Address",
                  Data="Address",
                    Search=new SearchOption {
                         Opeartor=  Easy.LINQ.Query.Operators.Contains,
                          Value=rm.Address
                    }
            },

                    new ColumnOption {
                 Name="Product",
                  Data="Product",
                    Search=new SearchOption {
                         Opeartor=  Easy.LINQ.Query.Operators.Contains,
                          Value=rm.Product
                    }
            },

                              new ColumnOption {
                 Name="AuditStandard",
                  Data="AuditStandard",
                    Search=new SearchOption {
                         Opeartor=  Easy.LINQ.Query.Operators.Contains,
                          Value=rm.AuditStandard
                    }
            },


            };
            var expression = query.AsExpression<AuditReportEntity>();

                 

            var order = query.GetOrderBy<AuditReportEntity>();
            if (order != null)
            {
                if (query.IsOrderDescending())
                {
                    pagin.OrderByDescending = order;
                }
                else
                {
                    pagin.OrderBy = order;
                }
            }
            var entities = Service.Get(expression, pagin)
                .Where(n =>
                      (  n.UserId == _applicationContextAccessor.Current.CurrentCustomer.UserID
                        || n.AllowUserRead == _applicationContextAccessor.Current.CurrentCustomer.UserID
                            || n.AllowGroupRead == _applicationContextAccessor.Current.CurrentCustomer.MembershipType)
                            &&n.Status==1
            )   ;
            //  return Json(new TableData(entities, pagin.RecordCount, query.Draw));

            //  viewModel = new ReportListViewModel();                                  

            rm.Reports = entities.ToList();

            return View(rm);

        }

        [HttpGet("download-file")]
        public IActionResult downloadFile(int Id)
        {
 


            var entities = Service.Get()
             .Where(n =>
                    (n.UserId == _applicationContextAccessor.Current.CurrentCustomer.UserID
                     || n.AllowUserDown == _applicationContextAccessor.Current.CurrentCustomer.UserID
                         || n.AllowGroupDown == _applicationContextAccessor.Current.CurrentCustomer.MembershipType)
                         && n.id == Id
         ).FirstOrDefault();


            if (entities != null)
            {


                string contentType;
                contentTypeProvider.TryGetContentType(entities.FilePath, out contentType);
                HttpContext.Response.ContentType = contentType;
                string path =System.IO.Path.Combine( environment.WebRootPath, entities.FilePath); // 注意哦, 不要像我这样直接使用客户端的值来拼接 path, 危险的
                path = path.Replace("~","").Replace("/","\\");

                var f = new System.IO.FileInfo(path);

                FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(path), contentType)
                {
                    FileDownloadName = f.Name
                };
                // return File("~/excels/report.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx"); // 返回 File + 路径也是可以, 这个路径是从 wwwroot 走起 
                // return File(await System.IO.File.ReadAllBytesAsync(path), same...) // 或则我们可以直接返回 byte[], 任意从哪里获取都可以. 

                return result;
            }
                   

            return Forbid();
        }



        public  IActionResult Remove(int id)
        {
            _reportServer.Remove(id);


            return RedirectToAction("Index");
        }
    }

    [DefaultAuthorize]
    public class AdminReportController : BasicController<AuditReportEntity, int, IReportServer>
    {
        private IApplicationContextAccessor _applicationContextAccessor;
        private ILocalize _localize;

        private IReportServer _reportServer;
        private IHostingEnvironment environment;
        private FileExtensionContentTypeProvider contentTypeProvider;
        public AdminReportController(IReportServer userService, IApplicationContextAccessor applicationContextAccessor, ILocalize localize,
             IHostingEnvironment environment

            )
            : base(userService)
        {
            _applicationContextAccessor = applicationContextAccessor;
            _localize = localize;
            _reportServer = userService;
            this.environment = environment;
            this.contentTypeProvider = new FileExtensionContentTypeProvider();

          
        }




        public override IActionResult Index()
        {
            int Id = 0;
            ReportListViewModel viewModel = new ReportListViewModel();
            //Pagin pagin = new Pagin { PageIndex = Id, OrderByDescending = "CreateDate" };
            //viewModel.Reports = _reportServer.Get(m => m.UserId == _applicationContextAccessor.Current.CurrentCustomer.UserID, pagin);
            //viewModel.Pagin = pagin;
            //viewModel.Pagin.BaseUrlFormat = "~/MyOrder/Index/{0}";
            return View(@"\Views\report\AdminIndex.cshtml");


        }

        public override IActionResult Create()
        {
            return base.Create();
        }

        public override IActionResult Create(AuditReportEntity entity)
        {
            entity.UserId = _applicationContextAccessor.Current.CurrentCustomer.UserID;


            entity.LastUpdateByName = _applicationContextAccessor.Current.CurrentCustomer.UserID;
            entity.CreatebyName = _applicationContextAccessor.Current.CurrentCustomer.UserID;
            entity.Status = 1;
            //entity.Title=


            var result = Service.Add(entity);
            // var result = base.Create(entity);
            return View("CreateSuccess");
        }
        [DefaultAuthorize(Policy = PermissionKeys.ManageReport)]
        public override IActionResult Edit(int Id)
        {
            return base.Edit(Id);
        }
        [HttpPost, DefaultAuthorize(Policy = PermissionKeys.ManageReport)]
        public override IActionResult Edit(AuditReportEntity entity)
        {
            var result = base.Edit(entity);

            Service.Publish(entity);

            if (Request.Query["ReturnUrl"].Count > 0)
            {
                return Redirect(Request.Query["ReturnUrl"]);
            }
            return result;
        }


        public IActionResult AdminIndex()
        {
            return base.Index();
        }
       
        public override IActionResult GetList(DataTableOption query)
        {
            return base.GetList(query);
        }

       

        [HttpGet("download-file")]
        public IActionResult downloadFile(int Id)
        {



            var entities = Service.Get()
             .Where(n =>
                    (n.UserId == _applicationContextAccessor.Current.CurrentCustomer.UserID
                     || n.AllowUserDown == _applicationContextAccessor.Current.CurrentCustomer.UserID
                         || n.AllowGroupDown == _applicationContextAccessor.Current.CurrentCustomer.MembershipType)
                         && n.id == Id
         ).FirstOrDefault();


            if (entities != null)
            {


                string contentType;
                contentTypeProvider.TryGetContentType(entities.FilePath, out contentType);
                HttpContext.Response.ContentType = contentType;
                string path = System.IO.Path.Combine(environment.WebRootPath, entities.FilePath); // 注意哦, 不要像我这样直接使用客户端的值来拼接 path, 危险的
                path = path.Replace("~", "").Replace("/", "\\");

                var f = new System.IO.FileInfo(path);

                FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(path), contentType)
                {
                    FileDownloadName = f.Name
                };
                // return File("~/excels/report.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx"); // 返回 File + 路径也是可以, 这个路径是从 wwwroot 走起 
                // return File(await System.IO.File.ReadAllBytesAsync(path), same...) // 或则我们可以直接返回 byte[], 任意从哪里获取都可以. 

                return result;
            }


            return Forbid();
        }



        public IActionResult Remove(int id)
        {
            _reportServer.Remove(id);


            return RedirectToAction("Index");
        }
    }
}
