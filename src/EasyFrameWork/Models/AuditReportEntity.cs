using Easy.Constant;
using Easy.MetaData;
using Easy.Models;
using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Easy.Models
{
    [DataTable("AuditReport")]
    public class AuditReportEntity : EditorEntity
    {
        [Key]
        public int id { get; set; }

        public string UserId { get; set; }
        public string FactoryName { get; set; }
        public string FactoryAdd { get; set; }
        public string AuditStandard { get; set; }
        public string AuditMethod { get; set; }
        public string AuditDate { get; set; }
        public string AuditType { get; set; }
        public int LengthofAudit { get; set; }
        public string AuditBrief { get; set; }
        public string Product { get; set; }
        public string AuditCompany { get; set; }
        public string AuditorName { get; set; }
        public string AuditorExperience { get; set; }
        public string AuditorQualification { get; set; }
        public string Finding { get; set; }
        public string Standard { get; set; }
        public string NonComplianceDescription { get; set; }
        public string Grade { get; set; }
        public string ImmediateAction { get; set; }
        public string RootCause { get; set; }
        public string PreventiveAction { get; set; }
        public string PoliciesProcedures { get; set; }
        public string Communication { get; set; }
        public string TrainingSkills { get; set; }
        public string MonitoringTracking { get; set; }
        public string GovernanceEnforcemen { get; set; }
        public string PlannedCompletionDate { get; set; }
        public string ResponderComments { get; set; }
        public string Evidence { get; set; }
        //public int Status { get; set; }
        public string AllowUserRead { get; set; }
        public string AllowGroupRead { get; set; }
        public string AllowGroupDown { get; set; }
        public string AllowUserDown { get; set; }

    public string FilePath { get; set; }

 

    }
    class AuditReportEntityMetaData : ViewMetaData<AuditReportEntity>
    {
        protected override void ViewConfigure()
        {
            ViewConfig(p => p.FactoryName).AsTextBox();
            //ViewConfig(p => p.Email).AsTextBox().Email();
            ViewConfig(p => p.AuditDate).AsTextBox().FormatAsDate();
            //  ViewConfig(p => p.ServiceStandard).AsDropDownList().DataSource(SourceType.Dictionary);
            ViewConfig(p => p.AuditType).AsDropDownList().DataSource(SourceType.Dictionary);
            ViewConfig(p => p.AuditMethod).AsDropDownList().DataSource(SourceType.Dictionary);
           //ViewConfig(p => p.AllowGroupRead).AsMutiSelect().DataSource(SourceType.Dictionary);
           // ViewConfig(p => p.AllowGroupDown).AsMutiSelect().DataSource(SourceType.Dictionary);
        }
    }
}
