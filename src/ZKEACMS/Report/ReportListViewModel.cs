using Easy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

namespace ZKEACMS.Shop.ViewModel
{
    public class ReportListViewModel
    {
        public IList<AuditReportEntity>  Reports { get; set; }
        public Pagin Pagin { get; set; }
    }
}
