using FanSoft.Sotre.Domain.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Store.Data.EF
{
    public class UnitOfWork : IUnitofWork
    {

        private StoreDataContext _ctx;

        public UnitOfWork(StoreDataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task CommitAsync()
        {
            await _ctx.SaveChangesAsync();
        }

        public Task RollbackAsync()
        {
            return null;
        }
    }
}
