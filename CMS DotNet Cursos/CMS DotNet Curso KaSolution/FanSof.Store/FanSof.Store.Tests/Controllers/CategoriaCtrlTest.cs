using FanSoft.Sotre.Domain.Contracts.Data;
using FanSoft.Sotre.Domain.Contracts.Repositories;
using FanSoft.Sotre.Domain.Entities;
using FanSoft.Store.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSof.Store.Tests.Controllers
{

    [TestClass]
    [TestCategory("Controller - Categoria")]
    public class CategoriaCtrlTest
    {

        [TestMethod]
        public void OMetodoIndexDeveraPossuirUmaListaCategoria()
        {

            // ------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------

            //Regra do 3 as -> 01 -> arrage -> Ambiente ( onde vc instancia os objetos)
            var categRepos = new CategoriaRepositoryFake();
            var categUow = new UnitofWorkFake();
            var categCtrl = new CategoriaController(categRepos, categUow);

            // ------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------

            //Regra do 3 as -> 02 -> action -> ação ( é a execução do metodo que vc vai testar)
            //var result = (ViewResult)categCtrl.Index();
            var result = (categCtrl.Index()).Result as ViewResult;
            var model = result.Model as IEnumerable<Categoria>;

            // ------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------

            //Regra do 3 as -> 03 -> assert -> validação do resultado
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count());
            Assert.AreEqual(2, model.Count());
            Assert.AreEqual(3, model.Count());

            // ------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------

        }

    }

    public class CategoriaRepositoryFake : ICategoriaRepository
    {
        public void Add(Categoria entity)
        {
            throw new NotImplementedException();
        }

        public void Deletee(Categoria entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Categoria>> GetAsync()
        {
            // throw new NotImplementedException();

            var listaCateg = new List<Categoria>
            {
                new Categoria{ Id = 1, Nome = "Alimento 01" },
                new Categoria{ Id = 2, Nome = "Alimento 02" }
            }.AsEnumerable();

            return Task.FromResult(listaCateg);

        }

        public Task<Categoria> GetAsync(object pk)
        {
            throw new NotImplementedException();
        }

        public Task<Categoria> GetByNomeAsync(string nome)
        {
            throw new NotImplementedException();
        }

        public void Update(Categoria entity)
        {
            throw new NotImplementedException();
        }
    }

    public class UnitofWorkFake : IUnitofWork
    {
        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public Task RollbackAsync()
        {
            throw new NotImplementedException();
        }
    }

}
