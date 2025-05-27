using Microsoft.EntityFrameworkCore;
using SimplesPratico.Data;
using SimplesPratico.Models;

namespace SimplesPratico.Repositorio {
    public class FuncionarioRepositorio : IFuncionarioRepositorio {

        private readonly SimplesPraticoDb _simplesPraticoDb;

        public FuncionarioRepositorio(SimplesPraticoDb simplesPraticoDb) {
            this._simplesPraticoDb = simplesPraticoDb;
        }
        public List<FuncionarioModel> BuscarTodos() {
            return _simplesPraticoDb.Funcionarios
                .Include(x => x.Clientes)
                .ToList();
        }
        public FuncionarioModel BuscarPorLogin(string login) {
            return _simplesPraticoDb.Funcionarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }
        public FuncionarioModel BuscarPorEmailELogin(string email, string login) {
            return _simplesPraticoDb.Funcionarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper());
        }
        public FuncionarioModel Adicionar(FuncionarioModel funcionario) {
            funcionario.Contratacao = DateTime.Now;
            funcionario.SetSenhaHash();
            _simplesPraticoDb.Funcionarios.Add(funcionario);
            _simplesPraticoDb.SaveChanges();

            return funcionario;
        }
        public FuncionarioModel ListarPorId(int id) {
            return _simplesPraticoDb.Funcionarios.FirstOrDefault(x => x.Id == id);
        }

        public FuncionarioModel Atualizar(FuncionarioModel funcionario) {
            FuncionarioModel funcionarioDb = ListarPorId(funcionario.Id);
            if (funcionarioDb == null)
                throw new Exception("Houve um erro na atualização do cadastro!");

            funcionarioDb.Nome = funcionario.Nome;
            funcionarioDb.Email = funcionario.Email;
            funcionarioDb.Login = funcionario.Login;
            funcionarioDb.Perfil = funcionario.Perfil;
            funcionarioDb.Atualizacao = DateTime.Now;

            _simplesPraticoDb.Funcionarios.Update(funcionarioDb);
            _simplesPraticoDb.SaveChanges();
            return funcionarioDb;
        }

        public bool Apagar(int id) {
            FuncionarioModel funcionarioDb = ListarPorId(id);
            if (funcionarioDb == null)
                throw new Exception("Houve um erro na deleção do cadastro!");

            _simplesPraticoDb.Funcionarios.Remove(funcionarioDb);
            _simplesPraticoDb.SaveChanges();
            return true;
        }

        public FuncionarioModel ObterEmail(string email) {
            return _simplesPraticoDb.Funcionarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper());
        }

        public FuncionarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel) {
            FuncionarioModel funcionarioDb = ListarPorId(alterarSenhaModel.Id);
            if (funcionarioDb == null)
                throw new Exception("Houve um erro na atualização da senha, usuário não encontrado!");
            if (!funcionarioDb.SenhaValida(alterarSenhaModel.SenhaAtual))
                throw new Exception("senha atual não confere!");
            if (funcionarioDb.SenhaValida(alterarSenhaModel.NovaSenha))
                throw new Exception("Nova senha deve ser diferente da atual!");
            funcionarioDb.SetNovaSenha(alterarSenhaModel.NovaSenha);
            funcionarioDb.Atualizacao = DateTime.Now;
            _simplesPraticoDb.Funcionarios.Update(funcionarioDb);
            _simplesPraticoDb.SaveChanges();
            return funcionarioDb;
        }
    }
}
