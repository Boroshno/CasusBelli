using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Abstract
{
    public  interface ITransactionRepository
    {
        IEnumerable<Transaction> transactions { get; }
        void AddTransaction(Transaction transaction);

        void DeleteTransaction(Transaction transaction);
    }
}
