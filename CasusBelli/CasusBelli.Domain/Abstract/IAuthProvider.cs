using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CasusBelli.Domain.Abstract
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
}
