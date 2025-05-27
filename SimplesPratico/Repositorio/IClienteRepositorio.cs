using SimplesPratico.Models;

namespace SimplesPratico.Repositorio {
    public interface IClienteRepositorio {
        ClienteModel ListarPorId(int id);
        List<ClienteModel> BuscarTodos(int funcionarioId);
        ClienteModel Adicionar(ClienteModel cliente);
        ClienteModel Atualizar(ClienteModel cliente);
        bool Apagar(int id);
    }
}
