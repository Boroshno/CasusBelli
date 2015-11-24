using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Concrete
{
    public class EFTransactionRepository:ITransactionRepository
    {
        public EFTransactionRepository() : this(new DefaultCasheProvider())
        {
            
        }

        public EFTransactionRepository(ICacheProvider cacheProvider)
        {
            this.Cache = cacheProvider;
        }
        private EFDbContext context = new EFDbContext();
        public ICacheProvider Cache { get; set; }

        public IEnumerable<Transaction> transactions
        {
            get
            {
                List<Transaction> transactionsData = Cache.Get("transactions") as List<Transaction>;
                if (transactionsData == null)
                {
                    transactionsData = context.Transactions.SqlQuery("SELECT * FROM Transactions").ToList(); 
                    if (transactionsData.Any())
                    {
                        Cache.Set("transactions", transactionsData, 99999);
                    }
                }
                return transactionsData;
            }
        }

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

            ClearCache();
        }


        public void DeleteTransaction(Transaction transaction)
        {
            Transaction deletedTran = context.Transactions.First(t => t.TransactionId == transaction.TransactionId);
            context.Transactions.Remove(deletedTran);
            context.SaveChanges();

            ClearCache();
        }

        public void ClearCache()
        {
            Cache.Invalidate("transactions");
        }
    }
}
