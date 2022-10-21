using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Sotre.Domain.Contracts.Data
{
    public interface IUnitofWork
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}
