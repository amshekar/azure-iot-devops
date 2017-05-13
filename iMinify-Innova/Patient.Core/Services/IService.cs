using Patients.Core.Entities.Foundation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Patients.Core.Services
{
    public interface IService<TEntity> : IDisposable where TEntity : BaseEntity
    {
        List<TEntity> GetAll();

        Task<TEntity> GetByIdAsync(int id);

        TEntity Add(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity);
              
    }
}
