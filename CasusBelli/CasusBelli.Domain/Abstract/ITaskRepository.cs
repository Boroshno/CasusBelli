using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Abstract
{
    public interface ITaskRepository
    {
        IQueryable<Task> Tasks { get; }

        void AddOrUpdateCountry(Task task);

        void DeleteCountry(Task task);
    }
}
