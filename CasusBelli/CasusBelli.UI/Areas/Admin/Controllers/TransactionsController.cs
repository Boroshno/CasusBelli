using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;
using CasusBelli.UI.Models;

namespace CasusBelli.UI.Areas.Admin.Controllers
{
    public class TransactionsController : Controller
    {
        //
        // GET: /Admin/Transactions/

        private ITransactionRepository transactionRepository;
        private IClientRepository clientRepository;

        public TransactionsController(ITransactionRepository transRep, IClientRepository clientRep)
        {
            transactionRepository = transRep;
            clientRepository = clientRep;
        }

        public ViewResult Index()
        {
            List<TransactionsViewModel> transVM = new List<TransactionsViewModel>();
            var transactions = transactionRepository.transactions.ToList();
            var clients = clientRepository.Clients.ToList();
            clients.Add(new Client { Name = "No client", ClientId = -1 });
            foreach (var transaction in transactions)
            {
                transVM.Add(new TransactionsViewModel(transaction, clients));
            }
            return View(transVM);
        }

        [HttpPost]
        public ActionResult CreateTransaction(string text, decimal currency, int clientId)
        {
            if (ModelState.IsValid)
            {
                Transaction lasttrans = transactionRepository.transactions.OrderByDescending(t => t.Date).First();
                Transaction newtran = new Transaction();
                newtran.BecameMoney = lasttrans.BecameMoney + currency;
                newtran.ClientId = clientId;
                newtran.Currency = currency;
                newtran.Date = DateTime.Now;
                newtran.Text = text;
                newtran.WasMoney = lasttrans.BecameMoney;
                transactionRepository.AddTransaction(newtran);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteTransaction(int id)
        {
            Transaction trans = transactionRepository.transactions.First(t => t.TransactionId == id);
            transactionRepository.DeleteTransaction(trans);
            return RedirectToAction("Index");
        }

    }
}
