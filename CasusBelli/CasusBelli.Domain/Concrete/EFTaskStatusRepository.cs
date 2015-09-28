using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Concrete
{
    public class EFTaskStatusRepository:ITaskStatusRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<TaskStatus> TaskStatuses { get { return context.TaskStatuses; } }

        public void AddOrUpdateCountry(TaskStatus taskStatus)
        {
            context.TaskStatuses.AddOrUpdate(p => p.TaskStatusId, taskStatus);
            context.SaveChanges();
        }

        public void DeleteCountry(TaskStatus taskStatus)
        {
            context.TaskStatuses.Remove(taskStatus);
            context.SaveChanges();
        }
    }
}
