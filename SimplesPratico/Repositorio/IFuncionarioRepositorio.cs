using SimplesPratico.Models;

namespace SimplesPratico.Repositorio {
    public interface IFuncionarioRepositorio {
        FuncionarioModel ListarPorId(int id);
        FuncionarioModel BuscarPorLogin(string login);
        List<FuncionarioModel> BuscarTodos();
        FuncionarioModel Adicionar(FuncionarioModel funcionario);
        FuncionarioModel Atualizar(FuncionarioModel funcionario);
        FuncionarioModel BuscarPorEmailELogin(string login, string email);
        FuncionarioModel ObterEmail(string email);
        FuncionarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);
        bool Apagar(int id);
    }
}
