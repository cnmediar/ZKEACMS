using Easy;
using Easy.Models;
using Easy.RepositoryPattern;
using EasyFrameWork.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZKEACMS.Report
{
   public class ReportServer : ServiceBase<AuditReportEntity, CMSDbContext>, IReportServer
    {
  
        private readonly ILocalize _localize;
        public ReportServer(IApplicationContext applicationContext,         
            ILocalize localize,
            CMSDbContext dbContext) : 
            base(applicationContext, dbContext)
        {
       
            _localize = localize;
        }

        public void Publish(int ID)
        {
            throw new NotImplementedException();
        }

        public void Publish(AuditReportEntity report)
        {

            this.Update(report);

        }
    }
}
