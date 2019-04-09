/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Text;
using Easy.Modules.User.Models;
using Easy.Notification;
using Microsoft.AspNetCore.Http;
using ZKEACMS.Notification.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Easy.Models;

namespace ZKEACMS.Notification
{
    public class NotifyService : INotifyService
    {
        private readonly INotificationManager _notificationManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        public NotifyService(INotificationManager notificationManager, IHttpContextAccessor httpContextAccessor, IDataProtectionProvider dataProtectionProvider)
        {
            _notificationManager = notificationManager;
            _httpContextAccessor = httpContextAccessor;
            _dataProtectionProvider = dataProtectionProvider;
        }
        public void ResetPassword(UserEntity user)
        {
            var dataProtector = _dataProtectionProvider.CreateProtector("ResetPassword");
            _notificationManager.Send(new RazorEmailNotice
            {
                Subject = "重置密码",
                To = new string[] { user.Email },
                Model = new ResetPasswordViewModel
                {
                    Link = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Account/Reset?token={user.ResetToken}&pt={dataProtector.Protect(user.ResetToken)}"
                },
                TemplatePath = "~/EmailTemplates/ResetPassword.cshtml"
            });
        }

        public void NewPassword(UserEntity user,string password)
        {
           
            _notificationManager.Send(new RazorEmailNotice
            {
                Subject = "New Password",
                To = new string[] { user.Email },
                Model = new ResetPasswordViewModel
                {
                    Link = password
                },
                TemplatePath = "~/EmailTemplates/NewPassword.cshtml"
            });
        }
        public void HaveNewUser(UserEntity user)
        {

            _notificationManager.Send(new RazorEmailNotice
            {
                Subject = "Have New User",
                To = new string[] { "compliance@asianlinks.co.uk" },
                Model = new ResetPasswordViewModel
                {
                    Link = user.Email
                },
                TemplatePath = "~/EmailTemplates/HaveNewUser.cshtml"
            });
        }

        public void ApplyOnline(ApplyOnline apply)
        {

            _notificationManager.Send(new RazorEmailNotice
            {
                Subject = "Apply Online",
                To = new string[] { "compliance@asianlinks.co.uk",apply.Email },
                Model = apply,
                TemplatePath = "~/EmailTemplates/ApplyOnline.cshtml"
            });
        }

    }
}
