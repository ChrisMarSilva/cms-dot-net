using FanSoft.Sotre.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Sotre.Domain.Contracts.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity //where TEntity: class
    {

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Deletee(TEntity entity);

        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(object pk);

    }
}
