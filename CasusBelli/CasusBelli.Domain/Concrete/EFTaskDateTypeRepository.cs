using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Abstract;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Concrete
{
    public class EFTaskDateTypeRepository:ITaskDateTypeRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<TaskDateType> TaskDateTypes { get { return context.TaskDateTypes; } }

        public void AddOrUpdateCountry(TaskDateType TaskDateType)
        {
            context.TaskDateTypes.AddOrUpdate(p => p.TaskDateTypeId, TaskDateType);
            context.SaveChanges();
        }

        public void DeleteCountry(TaskDateType TaskDateType)
        {
            context.TaskDateTypes.Remove(TaskDateType);
            context.SaveChanges();
        }

    }
}
