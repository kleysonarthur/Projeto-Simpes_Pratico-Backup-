using System.ComponentModel.DataAnnotations;
using SimplesPratico.Helper;
using SimplesPratico.Models.Enum;

namespace SimplesPratico.Models {
    public class FuncionarioModel {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o login do Funcionario!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o nome do Funcionario!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do Funcionario!")]
        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        public string Email { get; set; }
        public DateTime Contratacao { get; set; }
        public DateTime Atualizacao { get; set; }

        [Required(ErrorMessage = "Selecione o tipo de Perfil!")]
        public Perfil? Perfil { get; set; }

        [Required(ErrorMessage = "Digite a senha do Funcionario!")]
        public string Senha { get; set; }

        public virtual List<ClienteModel>? Clientes { get; set; }


        public bool SenhaValida(string senha) {
            return Senha == senha.GerarHash();
        }
        public void SetSenhaHash() {
            Senha = Senha.GerarHash();
        }
        public string GerarNovaSenha() {
            string novaSenha = Guid.NewGuid().ToString().Substring(0,8);
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }
        public void SetNovaSenha(string novaSenha) {
            Senha = novaSenha.GerarHash();
        }
    }
}
