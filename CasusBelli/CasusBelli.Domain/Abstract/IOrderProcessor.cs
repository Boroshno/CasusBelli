using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(OrderDetails orderDetails);
    }
}
