using CasusBelli.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasusBelli.UI.Models
{
    public class WebLinkViewModel:WebLink
    {
        public WebLinkViewModel()
        {

        }

        public WebLinkViewModel(WebLink link)
        {
            Id = link.Id;
            Name = link.Name;
            URL = link.URL;
            Login = link.Login;
            Password = link.Password;
            AdditionalInfo = link.AdditionalInfo;
        }
    }
}