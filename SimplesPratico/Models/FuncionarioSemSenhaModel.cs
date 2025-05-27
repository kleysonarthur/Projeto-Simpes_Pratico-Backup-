using System.ComponentModel.DataAnnotations;
using SimplesPratico.Models.Enum;

namespace SimplesPratico.Models {
    public class FuncionarioSemSenhaModel {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o login do Funcionario!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o nome do Funcionario!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do Funcionario!")]
        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Selecione o tipo de Perfil!")]
        public Perfil? Perfil { get; set; }
    }
}
