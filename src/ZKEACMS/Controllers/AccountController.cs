

using CacheManager.Core;
using Easy.Cache;
using Easy.Constant;
using Easy.Extend;
using Easy.Models;
using Easy.Modules.User.Models;
using Easy.Modules.User.Service;
using Easy.Mvc.Authorize;
using Easy.Mvc.Extend;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
 
using System.Security.Claims;
using System.Threading.Tasks;
using ZKEACMS.Account;
using ZKEACMS.Notification;

namespace ZKEACMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly INotifyService _notifyService;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IApplicationContextAccessor _applicationContextAccessor;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserService userService,
            INotifyService notifyService,
            IDataProtectionProvider dataProtectionProvider,
            ILogger<AccountController> logger,
            IApplicationContextAccessor applicationContextAccessor,
            Easy.Cache.ICacheManager<int> cacheManager)
        {
            _userService = userService;
            _notifyService = notifyService;
            _dataProtectionProvider = dataProtectionProvider;
            _applicationContextAccessor = applicationContextAccessor;
            _logger = logger;  _cacheManager = cacheManager;
        }

        private readonly Easy.Cache.ICacheManager<int> _cacheManager;
 
        #region Admin
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string userName, string password, string ReturnUrl)
        {
            var user = _userService.Login(userName, password, UserType.Administrator, Request.HttpContext.Connection.RemoteIpAddress.ToString());
            if (user != null)
            {

                user.AuthenticationType = DefaultAuthorizeAttribute.DefaultAuthenticationScheme;
                var identity = new ClaimsIdentity(user);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserID));
                await HttpContext.SignInAsync(DefaultAuthorizeAttribute.DefaultAuthenticationScheme, new ClaimsPrincipal(identity));

                if (ReturnUrl.IsNullOrEmpty() || !Url.IsLocalUrl(ReturnUrl))
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                return Redirect(ReturnUrl);
            }
            ViewBag.Errormessage = "登录失败，用户名密码不正确";
            return View();
        }

        public async Task<ActionResult> Logout(string returnurl)
        {
            await HttpContext.SignOutAsync(DefaultAuthorizeAttribute.DefaultAuthenticationScheme);
            return Redirect(returnurl ?? "~/");
        }
        #endregion

        #region Customer
        [CustomerAuthorize]
        public ActionResult Index()
        {
            return View(_applicationContextAccessor.Current.CurrentCustomer);
            return View();
        }
        [CustomerAuthorize]
        public ActionResult Edit()
        {
            return View(_applicationContextAccessor.Current.CurrentCustomer);
        }
        [HttpPost, ValidateAntiForgeryToken, CustomerAuthorize]
        public ActionResult Edit(UserEntity user)
        {
            if (_applicationContextAccessor.Current.CurrentCustomer.UserID == user.UserID)
            {
                user.UserTypeCD = (int)UserType.Customer;
                var newPhoto = Request.SaveFile();

  var d= _userService.Get(user.UserID);
                if (newPhoto.IsNotNullAndWhiteSpace())
                {
                    user.PhotoUrl = newPhoto;
                }
                else
                    user.PhotoUrl = d.PhotoUrl;




                try
                {
                 
                    user.PassWord = d.PassWord;
                     

                    _userService.Update(user);
                }
                catch (Exception ex)
                {
                    ViewBag.Errormessage = ex.Message;
                    return View(user);
                }
            }
            return RedirectToAction("Index");
        }
        [CustomerAuthorize]
        public ActionResult PassWord()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken, CustomerAuthorize]
        public ActionResult PassWord(UserEntity user)
        {
            var logOnUser = _userService.Login(_applicationContextAccessor.Current.CurrentCustomer.UserID, user.PassWord, UserType.Customer, Request.HttpContext.Connection.RemoteIpAddress.ToString());
            if (logOnUser != null)
            {
                logOnUser.PassWordNew = user.PassWordNew;
                _userService.Update(logOnUser);
                return RedirectToAction("SignOut", new { returnurl = "~/Account/SignIn" });
            }
            ViewBag.Message = "Error in original password";
            return View();
        }
        public ActionResult SignIn(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(string email, string password, string ReturnUrl)
        {
            var user = _userService.Login(email, password, UserType.Customer, Request.HttpContext.Connection.RemoteIpAddress.ToString());
            if (user == null)
            {
                ViewBag.Errormessage = "Logon failed, username password incorrect";
            }
            else if (user != null && user.Status == (int)RecordStatus.Active)
            {
                user.AuthenticationType = CustomerAuthorizeAttribute.CustomerAuthenticationScheme;
                var identity = new ClaimsIdentity(user);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserID));
                await HttpContext.SignInAsync(CustomerAuthorizeAttribute.CustomerAuthenticationScheme, new ClaimsPrincipal(identity));

                if (ReturnUrl.IsNullOrEmpty() || !Url.IsLocalUrl(ReturnUrl))
                {
                    return RedirectToAction("Index");
                }
                return Redirect(ReturnUrl);
            }
            else if (user != null && user.Status != (int)RecordStatus.Active)
            {
                ViewBag.Errormessage = "Logon failed, user not activated";
            }

                ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        public async Task<ActionResult> SignOut(string returnurl)
        {
            await HttpContext.SignOutAsync(CustomerAuthorizeAttribute.CustomerAuthenticationScheme);
            if (returnurl.IsNotNullAndWhiteSpace())
            {
                return Redirect(returnurl);
            }
            return RedirectToAction("SignIn");
        }
        public ActionResult SignUp(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View(new UserEntity());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SignUp(UserEntity user, string ReturnUrl)
        {
            if ( user.Email.IsNotNullAndWhiteSpace())
            {
                try
                {
                 var ou=   _userService.Count(n => n.Email == user.Email);

                    if (ou >0)
                        throw new Exception("This email address has been registered. Please fill in the other email address	");


                    //调用---------------   
                    string str = GenerateRandom(6);



                    user.UserID = (_userService.Count(n=>n.UserID.Length>0)+ 60000001).ToString();
                    user.PassWord = str;
                    user.Status = (int)RecordStatus.InActive;
                    user.UserTypeCD = (int)UserType.Customer;
                    //user.VocationalTraining = System.Web.HttpUtility.UrlEncode(user.VocationalTraining);
                    _userService.Add(user);

                  

                    _notifyService.NewPassword(user, str);

                    _notifyService.HaveNewUser(user);

                }
                catch (Exception ex)
                {
                    ViewBag.Errormessage = ex.Message;
                    ViewBag.ReturnUrl = ReturnUrl;
                    return View(user);
                }

            }
            return RedirectToAction("SignUpSuccess", new { ReturnUrl });
        }
        private static char[] constant =
     {
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
      };
        public static string GenerateRandom(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(52);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(52)]);
            }
            return newRandom.ToString();
        }


        //[CustomerAuthorize]
        public ActionResult ApplyOnline()
        {

            ViewBag.User = _applicationContextAccessor.Current.CurrentCustomer;


            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ApplyOnline(ApplyOnline apply, string ReturnUrl)
        {
           
                try
                {
                    //调用---------------   
                    string str = GenerateRandom(6);

                    string month = DateTime.Now.Month.ToString();
                    string date = DateTime.Now.ToString("yyyyMMdd");
                   if ( DateTime.Now.Month==12) month="C";
                    if (DateTime.Now.Month == 11) month = "B";
                    if (DateTime.Now.Month == 10) month = "A";


                   


                    //var item = new CacheItem<object>(key, value, ExpirationMode.Absolute, TimeSpan.FromMinutes(TimeOut_Minutes));
                    //cache.Manager.Put(item);
                    int no = 0;

                    if (_cacheManager.Exists(date))
                    {
                        no = _cacheManager.Get(date);
                        no += 1;

                        _cacheManager.AddOrUpdate(date,no, v => no);

                    }
                    else
                    {
                        no += 1;
                       _cacheManager.Add(date, no);
                    }
                    apply.Id = $"AL" + DateTime.Now.Year.ToString().Substring(2) + month + DateTime.Now.ToString("dd")+no.ToString().PadLeft(2,'0');

                    apply.User = _applicationContextAccessor.Current.CurrentCustomer;

                    //if (apply.ApplyType == "Information    of  Applican")
                    //{

                    //    var user = _applicationContextAccessor.Current.CurrentCustomer;

                    //    apply.Email = user.Email;
                    //    apply.CompanyNameEnglish = user.CompanyNameEnglish;
                    //    apply.CompanyNameLocal = user.CompanyNameLocal;
                    //    apply.AddressEnglish = user.AddressEnglish;
                    //    apply.AddressLocal = user.AddressLocal;
                    //    apply.BusinessLicenseNumber = user.BusinessLicenseNumber;
                    //    apply.YearofFacilityEstablished = user.YearofFacilityEstablished;
                    //    apply.ContactPerson = user.ContactPerson;
                    //    apply.TelephoneNumber = user.TelephoneNumber;
                    //    apply.MainLanguageofemployees = user.MainLanguageofemployees;
                    //    apply.ProductsbyCategory = user.ProductsbyCategory;
                    //    apply.SpecificProduct = user.SpecificProduct;
                    //    apply.ProductionWorkers = user.ProductionWorkers;
                    //    apply.ManagementStaff = user.ManagementStaff;
                    //    apply.Male = user.Male;
                    //    apply.Female = user.Female;
                    //    apply.TotalFacilityFloorSize = user.TotalFacilityFloorSize;                                                       
                  
                    //}


                

                    _notifyService.ApplyOnline(apply);
                  

                }
                catch (Exception ex)
                {
                    ViewBag.Errormessage = ex.Message;
                    ViewBag.ReturnUrl = ReturnUrl;
                    return View(apply);
                }

          
              return View("ApplyOnlinesSuccess", new { ReturnUrl });
        }


        public JsonResult UploadFile()
        {

            var newPhoto = Request.SaveFile();
          
            var o = new { path = newPhoto};


            return Json(o);
        }



        public ActionResult SignUpSuccess()
        {
            return View();
        }

        public ActionResult Forgotten()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Forgotten(string Email)
        {
            if (Email.IsNotNullAndWhiteSpace())
            {
                var user = _userService.SetResetToken(Email, UserType.Customer);
                if (user != null)
                {
                    _notifyService.ResetPassword(user);
                }
                return RedirectToAction("Sended", new { to = Email, status = (user != null ? 1 : 2) });
            }
            return RedirectToAction("Forgotten");
        }

        public ActionResult Sended(string to)
        {
            ViewBag.Email = to;
            return View();
        }
        public ActionResult Reset(string token, string pt)
        {
            try
            {
                var dataProtector = _dataProtectionProvider.CreateProtector("ResetPassword");
                if (pt.IsNullOrWhiteSpace() || dataProtector.Unprotect(pt) != token)
                {
                    ViewBag.Errormessage = "The access reset link is invalid. Please reapply.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                ViewBag.Errormessage = "The access reset link is invalid. Please reapply.";
            }
            return View(new ResetViewModel { ResetToken = token, Protect = pt });
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Reset(ResetViewModel user)
        {
            try
            {
                var dataProtector = _dataProtectionProvider.CreateProtector("ResetPassword");
                if (user.Protect.IsNotNullAndWhiteSpace() && dataProtector.Unprotect(user.Protect) == user.ResetToken)
                {
                    if (_userService.ResetPassWord(user.ResetToken, user.PassWordNew))
                    {
                        return RedirectToAction("SignIn");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            ViewBag.Errormessage = "Failed to reset password";
            return View(user);
        }
        #endregion
    }
}
