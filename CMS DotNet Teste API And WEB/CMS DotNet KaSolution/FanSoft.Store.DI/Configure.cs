using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FanSoft.Store.Data.EF;
using FanSoft.Store.Data.EF.Repositories;
using FanSoft.Sotre.Domain.Contracts.Data;
using FanSoft.Sotre.Domain.Contracts.Repositories;

namespace FanSoft.Store.DI
{
    public static class Configure
    {

        public static void AddDI(this IServiceCollection services)
        {
            services.AddScoped<StoreDataContext>();
            services.AddTransient<IUnitofWork, UnitOfWork>();
            services.AddTransient<IProdutoRepository, ProdutoRepositoryEF>();
            services.AddTransient<IUsuarioRepository, UsuarioRepositoryEF>();
            services.AddTransient<ICategoriaRepository, CategoriaRepositoryEF>();
        }

    }
}
