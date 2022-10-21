using FanSoft.Sotre.Domain.Entities;
using FanSoft.Sotre.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Store.Domain.ViewModels
{
    public class UsuariosAddEditVM
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public static class UsuariosModelExtensions
    {
        static public UsuariosAddEditVM ToVM(this Usuario data) => new UsuariosAddEditVM { Nome = data.Nome, Email = data.Email };
        static public Usuario ToData(this UsuariosAddEditVM data, int id = 0) => new Usuario { Id = id, Nome = data.Nome, Email = data.Email, Senha = data.Senha.Encrypt() };
    }

}
