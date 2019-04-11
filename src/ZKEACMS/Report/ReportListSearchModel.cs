using Easy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

namespace ZKEACMS.Shop.ViewModel
{
    public class ReportListSearchModel
    {
        public IList<AuditReportEntity>  Reports { get; set; }
        public Pagin Pagin { get; set; }

        public string Keyword { get; set; }

    public string Product { get; set; }

    public string Address { get; set; }

    public string AuditStandard { get; set; }
        
              
    }
}
