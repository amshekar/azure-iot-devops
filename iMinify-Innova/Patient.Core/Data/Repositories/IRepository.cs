using Patients.Core.Entities.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patients.Core.Data.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        List<TEntity> GetAll();

        Task<TEntity> GetByIdAsync(int id);

        void Add(TEntity entity);
        
    }
}
