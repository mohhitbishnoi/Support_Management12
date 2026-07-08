using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity;
        Task<int>Save(CancellationToken cancellationToken);
    }
}
