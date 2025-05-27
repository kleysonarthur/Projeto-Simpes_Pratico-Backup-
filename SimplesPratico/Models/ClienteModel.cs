using System.ComponentModel.DataAnnotations;

namespace SimplesPratico.Models {
    public class ClienteModel {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do Cliente!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o e-mail do Cliente!")]
        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite o celular do Cliente!")]
        [Phone(ErrorMessage = "Telefone inválido!")]
        public string Celular { get; set; }
        public int? FuncionarioId { get; set; }
        public FuncionarioModel? Funcionario { get; set; }
    }
}
