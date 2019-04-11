using Easy;
using Easy.Constant;
using Easy.Models;
using Easy.Modules.User.Models;
using Easy.Modules.User.Service;
using Easy.Mvc.Authorize;
using Easy.Mvc.Controllers;
using EasyFrameWork.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using ZKEACMS.Report;
using ZKEACMS.Shop.ViewModel;

namespace ZKEACMS.Controllers
{
    [CustomerAuthorize]
    public class ReportController : BasicController<AuditReportEntity, int, IReportServer>
    {
        private IApplicationContextAccessor _applicationContextAccessor;
        private ILocalize _localize;

        private IReportServer _userService;
        public ReportController(IReportServer userService, IApplicationContextAccessor applicationContextAccessor, ILocalize localize
            )
            : base(userService)
        {
            _applicationContextAccessor = applicationContextAccessor;
            _localize = localize;
            _userService = userService;
        }

       
        public override IActionResult Index()
        {
            int Id = 0;
            ReportListViewModel viewModel = new ReportListViewModel();
            Pagin pagin = new Pagin { PageIndex = Id, OrderByDescending = "CreateDate" };
            viewModel.Reports = _userService.Get(m => m.UserId == _applicationContextAccessor.Current.CurrentCustomer.UserID, pagin);
            viewModel.Pagin = pagin;
            viewModel.Pagin.BaseUrlFormat = "~/MyOrder/Index/{0}";
            return View(viewModel);

            return base.Index();
        }
     
        public override IActionResult Create()
        {
            return base.Create();
        }
  
        public override IActionResult Create(AuditReportEntity entity)
        {
          entity.UserId = _applicationContextAccessor.Current.CurrentCustomer.UserID;
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
        [HttpPost, DefaultAuthorize(Policy = PermissionKeys.ViewReport)]
        public override IActionResult GetList(DataTableOption query)
        {
            return base.GetList(query);
        }
    
        public  IActionResult Remove(int id)
        {
            _userService.Remove(id);


            return RedirectToAction("Index");
        }
    }
}
