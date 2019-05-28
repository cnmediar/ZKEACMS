/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;

namespace Easy.Models
{
    public class HumanBase : EditorEntity
    {
        /// <summary>
        /// 姓
        /// </summary>
        //public string LastName { get; set; }
        ///// <summary>
        ///// 名
        ///// </summary>
        //public string FirstName { get; set; }
        ///// <summary>
        ///// 昵称
        ///// </summary>
        //public string NickName { get; set; }
        ///// <summary>
        ///// 英文名
        ///// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get { return Birthday.HasValue ? (DateTime.Now.Year - Birthday.Value.Year) : 0; } }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }
        /// <summary>
        /// 出生地
        /// </summary>
        public string Birthplace { get; set; }
        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        //public string ZipCode { get; set; }
        /// <summary>
        /// 学校
        /// </summary>
        public string School { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        //public string Profession { get; set; }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public int? MaritalStatus { get; set; }
        /// <summary>
        /// 爱好
        /// </summary>
        //public string Hobby { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        //public string QQ { get; set; }

        public string Email { get; set; }

        public string CompanyNameEnglish { get; set; }
        public string CompanyNameLocal { get; set; }
        public string AddressEnglish { get; set; }
        public string AddressLocal { get; set; }
        public string BusinessLicenseNumber { get; set; }
        public string YearofFacilityEstablished { get; set; }
        public string ContactPerson { get; set; }
        public string TelephoneNumber { get; set; }
        //public string MobileNumber { get; set; }

        public string MainLanguageofemployees { get; set; }
        public string Mainservices { get; set; }


        public string LocalName { get; set; }

        public string Gender { get; set; }
        public string Marriage { get; set; }
        public string Photo { get; set; }
        public string AuditFirm { get; set; }
        public string IndependentAuditor { get; set; }
        public string JobTitle { get; set; }

        public string ProductsbyCategory { get; set; }
        public string SpecificProduct { get; set; }
        public string Employees { get; set; }
        public string Outside { get; set; }
        public string ShowroomorMainproducts { get; set; }
        public string ProductionAreas { get; set; }
        public string ProductionWorkers { get; set; }
        public string ManagementStaff { get; set; }
        public string Male { get; set; }
        public string Female { get; set; }
        public string TotalFacilityFloorSize { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string IDNumber { get; set; }

        public string IDNumberPhoto { get; set; }
        public bool BSCI { get; set; }
        public bool ICS { get; set; }
        public bool Sedex { get; set; }
        public bool  ICTI { get; set; }
        public bool RBA { get; set; }
        public bool CTPAT { get; set; }
        public bool SCS { get; set; }
        public bool SCAN { get; set; }
        public bool GSV { get; set; }
        public bool FCCA { get; set; }
        public bool SQP { get; set; }
        public bool QMS { get; set; }
        public bool GMP { get; set; }
        public string ClientsProgram1 { get; set; }
        public string ClientsProgram2 { get; set; }
        public string ClientsProgram3 { get; set; }
        public string SystemCertification { get; set; }
        public string Others { get; set; }

       
  public string ListTypeofIndustryExperience { get; set; }

        public string Workexperience { get; set; }
        public string Workexperience2 { get; set; }
        public string Workexperience3 { get; set; }
        public string ListWrittenLanguageofAuditor { get; set; }
        public string LevelofProficiencywiththisWritten { get; set; }
        public string ListofLanguageSpokenbyAuditor { get; set; }
        public string ListAuditorsSpecialSkills { get; set; }
        public string LevelofEducation { get; set; }
        public string YearofGraduation { get; set; }

        public string MembershipType { get; set; }


        public string VocationalTraining { get; set; }




    }
}
