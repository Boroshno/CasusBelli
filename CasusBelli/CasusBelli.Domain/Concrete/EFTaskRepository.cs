using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Concrete
{
    public class EFTaskRepository:ITaskRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Task> Tasks { get { return context.Tasks; } }

        public void AddOrUpdateCountry(Task task)
        {
            context.Tasks.AddOrUpdate(p => p.TaskId, task);
            context.SaveChanges();
        }

        public void DeleteCountry(Task task)
        {
            context.Tasks.Remove(task);
            context.SaveChanges();
        }
    }
}
