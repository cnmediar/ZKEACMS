using Easy.Constant;
using Easy.MetaData;
using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easy.Models
{
    //[DataTable("ApplyOnline")]
    public class ApplyOnline
    {
        public string Id { get; set; }
        public string ApplyType { get; set; }

       public string Payer { get; set; }
        public string UserId { get; set; }

        public IUser User { get; set; }

        #region Information of Vendor (If applicable)
        public string VendorEmail { get; set; }
        public string VendorCompanyNameEnglish { get; set; }
        public string VendorCompanyNameLocal { get; set; }
        public string VendorAddressEnglish { get; set; }
        public string VendorAddressLocal { get; set; }
      
        public string VendorContactPerson { get; set; }
        public string VendorTelephoneNumber { get; set; }

        #endregion


        #region Details of Physical Facility to be assessed
        public string Email { get; set; }
        public string CompanyNameEnglish { get; set; }
        public string CompanyNameLocal { get; set; }
        public string AddressEnglish { get; set; }
        public string AddressLocal { get; set; }
        public string BusinessLicenseNumber { get; set; }
        public string YearofFacilityEstablished { get; set; }
        public string ContactPerson { get; set; }
        public string TelephoneNumber { get; set; }

        public string MobileNumber { get; set; }
      

        public string MainLanguageofemployees { get; set; }

        public string ProductsbyCategory { get; set; }
        public string SpecificProduct { get; set; }

        public string ProductionWorkers { get; set; }
        public string ManagementStaff { get; set; }
        public string Male { get; set; }
        public string Female { get; set; }
        public string TotalFacilityFloorSize { get; set; }
        #endregion
        public string ServiceStandard { get; set; }
         public string ClientProgram { get; set; }
         public string ServiceRequest { get; set; }
          public string AuditType { get; set; }
          public string AuditMethod { get; set; }
          public DateTime DesiredAuditdate { get; set; }
    }

    class ApplyOnlineMetaData : ViewMetaData<ApplyOnline>
    {
        protected override void ViewConfigure()
        {           
        ViewConfig(p => p.CompanyNameEnglish).AsTextBox();
        ViewConfig(p => p.Email).AsTextBox().Email();           
            ViewConfig(p => p.DesiredAuditdate).AsTextBox().FormatAsDate();
            ViewConfig(p => p.ServiceStandard).AsDropDownList().DataSource(SourceType.Dictionary);      
            ViewConfig(p => p.AuditType).AsDropDownList().DataSource(SourceType.Dictionary);           
            ViewConfig(p => p.AuditMethod).AsDropDownList().DataSource(SourceType.Dictionary);
            ViewConfig(p => p.Payer).AsDropDownList().DataSource(SourceType.Dictionary);

        }
    }
}
