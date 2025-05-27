using SimplesPratico.Data;
using SimplesPratico.Models;

namespace SimplesPratico.Repositorio {
    public class ClienteRepositorio : IClienteRepositorio {

        private readonly SimplesPraticoDb _simplesPraticoDb;

        public ClienteRepositorio(SimplesPraticoDb simplesPraticoDb) {
            this._simplesPraticoDb = simplesPraticoDb;
        }
        public List<ClienteModel> BuscarTodos(int funcionarioId) {
            return _simplesPraticoDb.Clientes.Where(x => x.FuncionarioId == funcionarioId).ToList();
        }
        public ClienteModel Adicionar(ClienteModel cliente) {
            _simplesPraticoDb.Clientes.Add(cliente);
            _simplesPraticoDb.SaveChanges();

            return cliente;
        }
        public ClienteModel ListarPorId(int id) {
            return _simplesPraticoDb.Clientes.FirstOrDefault(x => x.Id == id);
        }

        public ClienteModel Atualizar(ClienteModel cliente) {
            ClienteModel clienteDb = ListarPorId(cliente.Id);
            if (clienteDb == null)
                throw new Exception("Houve um erro na atualização do cadastro!");

            clienteDb.Nome = cliente.Nome;
            clienteDb.Email = cliente.Email;
            clienteDb.Celular = cliente.Celular;

            _simplesPraticoDb.Clientes.Update(clienteDb);
            _simplesPraticoDb.SaveChanges();
            return clienteDb;
        }

        public bool Apagar(int id) {
            ClienteModel clienteDb = ListarPorId(id);
            if (clienteDb == null)
                throw new Exception("Houve um erro na deleção do cadastro!");

            _simplesPraticoDb.Clientes.Remove(clienteDb);
            _simplesPraticoDb.SaveChanges();
            return true;
        }
    }
}
