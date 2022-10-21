using FanSoft.Sotre.Domain.Entities;
using FanSoft.Sotre.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanSoft.Store.Domain.ViewModels
{
    public class SignInVM
    {
        [Required(ErrorMessage = "Email é Obrigatorio")]
        [EmailAddress(ErrorMessage = "Email Invalido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é Obrigatoria")]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "Tamanho da Senha invalido", MinimumLength = 2)]
        public string Senha { get; set; }

        public bool Lembrar { get; set; }
    }

    public static class AuthUsuariosModelExtensions
    {
        static public UsuariosAddEditVM ToVM(this Usuario data) => new UsuariosAddEditVM { Nome = data.Nome, Email = data.Email };
        static public Usuario ToData(this SignInVM data) => new Usuario { Email = data.Email, Senha = data.Senha.Encrypt() };
    }
}
