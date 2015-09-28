using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Concrete
{
    public class EFTransactionRepository:ITransactionRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Transaction> transactions { get { return context.Transactions; } }

        public void AddTransaction(Transaction transaction)
        {
            Transaction newTran = new Transaction();
            newTran.BecameMoney = transaction.BecameMoney;
            newTran.ClientId = transaction.ClientId;
            newTran.Currency = transaction.Currency;
            newTran.Date = transaction.Date;
            newTran.Text = transaction.Text;
            newTran.WasMoney = transaction.WasMoney;
            context.Transactions.Add(newTran);
            context.SaveChanges();
        }
    }
}
