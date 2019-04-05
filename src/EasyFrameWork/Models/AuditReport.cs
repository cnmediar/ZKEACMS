using System;
using System.Collections.Generic;
using System.Text;

namespace EasyFrameWork.Models
{
   public class AuditReport
    {
        public int id                   { get; set; }
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
    public int Status { get; set; }
}
}
