/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */
using Easy.Models;
using Easy.Modules.User.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZKEACMS.Notification
{
    public interface INotifyService
    {
        void ResetPassword(UserEntity user);

        void HaveNewUser(UserEntity user);
        void NewPassword(UserEntity user, string password);

        void ApplyOnline(ApplyOnline apply);
    }
}
