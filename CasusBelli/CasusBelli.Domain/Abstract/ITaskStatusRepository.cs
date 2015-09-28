using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Abstract
{
    public interface ITaskStatusRepository
    {
        IQueryable<TaskStatus> TaskStatuses { get; }

        void AddOrUpdateCountry(TaskStatus TaskStatuse);

        void DeleteCountry(TaskStatus TaskStatuse);
    }
}
