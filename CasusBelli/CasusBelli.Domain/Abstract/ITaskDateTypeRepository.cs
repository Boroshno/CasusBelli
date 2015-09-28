using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CasusBelli.Domain.Entities;

namespace CasusBelli.Domain.Abstract
{
    public interface ITaskDateTypeRepository
    {
        IQueryable<TaskDateType> TaskDateTypes { get; }

        void AddOrUpdateCountry(TaskDateType taskDateType);

        void DeleteCountry(TaskDateType taskDateType);
    }
}
