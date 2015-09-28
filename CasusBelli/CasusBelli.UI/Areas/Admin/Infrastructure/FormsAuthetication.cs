using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CasusBelli.Domain.Abstract;

namespace CasusBelli.UI.Areas.Admin.Infrastructure
{
    public class FormsAuthProvider:IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result) FormsAuthentication.SetAuthCookie(username, false);
            return result;
        }
    }
}