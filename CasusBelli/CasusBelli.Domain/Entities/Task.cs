using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace CasusBelli.Domain.Entities
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Text { get; set; }
        public int ProductId { get; set; }
        public int TransactionId { get; set; }
        public int ClientId { get; set; }
        public int TaskDateTypeId { get; set; }
        public DateTime TaskDate { get; set; }
        public int TaskStatusId { get; set; }
    }
}
