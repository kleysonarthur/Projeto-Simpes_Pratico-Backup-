using SimplesPratico.Models;

namespace SimplesPratico.Helper {
    public interface ISessao {
        void CriarSessao(FuncionarioModel funcionario);
        void RemoverSessao();
        FuncionarioModel BuscarSessao();
    }
}
