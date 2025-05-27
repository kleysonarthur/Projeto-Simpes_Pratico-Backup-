using System.ComponentModel.DataAnnotations;

namespace SimplesPratico.Models {
    public class RedefinirSenhaModel {
        [Required(ErrorMessage = "Digite o Login do Usuário!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o Email do Usuário!")]
        public string Email { get; set; }
    }
}
