using Easy;
using Easy.Constant;
using Easy.Modules.User.Models;
using Easy.Modules.User.Service;
using Easy.Mvc.Authorize;
using Easy.Mvc.Controllers;
using EasyFrameWork.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using ZKEACMS.Report;

namespace ZKEACMS.Controllers
{
    [DefaultAuthorize]
    public class ReportController : BasicController<AuditReportEntity, int, IReportServer>
    {
        private IApplicationContextAccessor _applicationContextAccessor;
        private ILocalize _localize;
        public ReportController(IReportServer userService, IApplicationContextAccessor applicationContextAccessor, ILocalize localize)
            : base(userService)
        {
            _applicationContextAccessor = applicationContextAccessor;
            _localize = localize;
        }

        [DefaultAuthorize(Policy = PermissionKeys.ViewReport)]
        public override IActionResult Index()
        {
            return base.Index();
        }
        [DefaultAuthorize(Policy = PermissionKeys.ManageReport)]
        public override IActionResult Create()
        {
            return base.Create();
        }
        [HttpPost, DefaultAuthorize(Policy = PermissionKeys.ManageReport)]
        public override IActionResult Create(AuditReportEntity entity)
        {

            Service.Publish(entity);

            var result = base.Create(entity);
            return result;
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
        [DefaultAuthorize(Policy = PermissionKeys.ManageReport)]
        public override IActionResult Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
