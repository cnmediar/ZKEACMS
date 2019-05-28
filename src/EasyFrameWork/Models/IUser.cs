/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using Easy.Modules.Role;

namespace Easy.Models
{
    public interface IUser
    {
        string UserID { get; set; }
        //string NickName { get; set; }
        string PassWord { get; set; }
        long Timestamp { get; set; }
        string LoginIP { get; set; }
        string PhotoUrl { get; set; }
        int? UserTypeCD { get; set; }
        string UserName { get; set; }
        string ApiLoginToken { get; set; }
        //string LastName { get; set; }
        //string FirstName { get; set; }
        string EnglishName { get; set; }
        //int? Age { get; }
        DateTime? Birthday { get; set; }
        int? Sex { get; set; }
        string Birthplace { get; set; }
        string Address { get; set; }
        //string ZipCode { get; set; }
        string School { get; set; }
        string Telephone { get; set; }
        string MobilePhone { get; set; }
        //string Profession { get; set; }
        int? MaritalStatus { get; set; }
        //string Hobby { get; set; }
        //string QQ { get; set; }
        string Email { get; set; }
        List<UserRoleRelation> Roles { get; set; }

         string CompanyNameEnglish { get; set; }
         string CompanyNameLocal { get; set; }
         string AddressEnglish { get; set; }
         string AddressLocal { get; set; }
         string BusinessLicenseNumber { get; set; }
         string YearofFacilityEstablished { get; set; }
         string ContactPerson { get; set; }
         string TelephoneNumber { get; set; }
        // string MobileNumber { get; set; }

         string MainLanguageofemployees { get; set; }
         string Mainservices { get; set; }


         string LocalName { get; set; }

         string Gender { get; set; }
         string Marriage { get; set; }
         string Photo { get; set; }
         string AuditFirm { get; set; }
         string IndependentAuditor { get; set; }
         string JobTitle { get; set; }

         string ProductsbyCategory { get; set; }
         string SpecificProduct { get; set; }
         string Employees { get; set; }
         string Outside { get; set; }
         string ShowroomorMainproducts { get; set; }
         string ProductionAreas { get; set; }
         string ProductionWorkers { get; set; }
         string ManagementStaff { get; set; }
         string Male { get; set; }
         string Female { get; set; }
         string TotalFacilityFloorSize { get; set; }
        


        string Country { get; set; }
        string City { get; set; }
        string IDNumber { get; set; }
        string IDNumberPhoto { get; set; }


        bool BSCI { get; set; }
         bool ICS { get; set; }
         bool Sedex { get; set; }
         bool ICTI { get; set; }
         bool RBA { get; set; }
         bool CTPAT { get; set; }
         bool SCS { get; set; }
         bool SCAN { get; set; }
         bool GSV { get; set; }
         bool FCCA { get; set; }
         bool SQP { get; set; }
         bool QMS { get; set; }
         bool GMP { get; set; }
        string ClientsProgram1 { get; set; }
         string ClientsProgram2 { get; set; }
         string ClientsProgram3 { get; set; }
         string SystemCertification { get; set; }
         string Others { get; set; }

        string ListTypeofIndustryExperience { get; set; }
        string Workexperience { get; set; }

          string Workexperience2 { get; set; }
          string Workexperience3 { get; set; }
        string ListWrittenLanguageofAuditor { get; set; }
         string LevelofProficiencywiththisWritten { get; set; }
         string ListofLanguageSpokenbyAuditor { get; set; }
         string ListAuditorsSpecialSkills { get; set; }
         string LevelofEducation { get; set; }
         string YearofGraduation { get; set; }
         string MembershipType { get; set; }

          string VocationalTraining { get; set; }

    }
}
