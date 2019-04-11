using Easy.Models;
using Easy.RepositoryPattern;
using EasyFrameWork.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZKEACMS.Report
{
  public  interface IReportServer : IService<AuditReportEntity>
    {
        void Publish(int ID);
        void Publish(AuditReportEntity report );
    }
}
