using CasusBelli.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CasusBelli.Domain.Abstract
{
    public interface IWebLinkRepository
    {
        IEnumerable<WebLink> WebLink { get; }
        void AddOrUpdateWebLink(WebLink webLink);
        void DeleteWebLink(WebLink webLink);
    }
}
