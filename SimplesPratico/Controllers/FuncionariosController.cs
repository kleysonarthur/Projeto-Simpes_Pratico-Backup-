using Microsoft.AspNetCore.Mvc;
using SimplesPratico.Filters;
using SimplesPratico.Models;
using SimplesPratico.Repositorio;

namespace SimplesPratico.Controllers {
    [FiltroLogadoRestrict]
    public class FuncionariosController : Controller {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;

        public FuncionariosController(IFuncionarioRepositorio funcionarioRepositorio, IClienteRepositorio clienteRepositorio) {
            _funcionarioRepositorio = funcionarioRepositorio;
            _clienteRepositorio = clienteRepositorio;
        }
        public IActionResult Index() {
            List<FuncionarioModel> funcionarios = _funcionarioRepositorio.BuscarTodos();
            return View(funcionarios);
        }
        public IActionResult Novo() {
            return View();
        }
        public IActionResult Datlhes() {
            return View();
        }
        public IActionResult Editar(int id) {
            FuncionarioModel funcionario = _funcionarioRepositorio.ListarPorId(id);
            return View(funcionario);
        }
        public IActionResult ApagarConfirmacao(int id) {
            FuncionarioModel funcionario = _funcionarioRepositorio.ListarPorId(id);
            return View(funcionario);
        }
        public IActionResult Apagar(int id) {
            try {
                bool apagado = _funcionarioRepositorio.Apagar(id);
                if (apagado) {
                    TempData["MensagemSucesso"] = "Funcionario EXCLUIDO com SUCESSO!";
                }
                else {
                    TempData["MensagemErro"] = $"ERRO na Exclusão do Funcionario! Tente Novamente!";
                }
                return RedirectToAction("Index");
            }
            catch (System.Exception erro) {
                TempData["MensagemErro"] = $"ERRO na Exclusão do Cadastro! Detalhe: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
        public IActionResult ListarClientesPorFuncionarioId(int id) {
            List<ClienteModel> clientes = _clienteRepositorio.BuscarTodos(id);
            return PartialView("_ClientesFuncionario", clientes);
        }





        [HttpPost]
        public IActionResult Novo(FuncionarioModel funcionario) {
            try {
                if (ModelState.IsValid) {
                    _funcionarioRepositorio.Adicionar(funcionario);
                    TempData["MensagemSucesso"] = "Funcionario Cadastrado com SUCESSO!";
                    return RedirectToAction("Index");
                }
                return View(funcionario);
            }
            catch (System.Exception erro) {
                TempData["MensagemErro"] = $"ERRO no Cadastro! Tente Novamente {erro.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Atualizar(FuncionarioSemSenhaModel funcionarioSemSenhaModel) {
            try {
                FuncionarioModel funcionario = null;
                if (ModelState.IsValid) {
                    funcionario = new FuncionarioModel() {
                        Id = funcionarioSemSenhaModel.Id,
                        Nome = funcionarioSemSenhaModel.Nome,
                        Email = funcionarioSemSenhaModel.Email,
                        Login = funcionarioSemSenhaModel.Login,
                        Perfil = funcionarioSemSenhaModel.Perfil,
                    };

                    funcionario = _funcionarioRepositorio.Atualizar(funcionario);
                    TempData["MensagemSucesso"] = "Cadastrado ATUALIZADO com SUCESSO!";
                    return RedirectToAction("Index");
                }
                return View("Editar", funcionario);
            }
            catch (SystemException erro) {
                TempData["MensagemErro"] = $"ERRO na Atualização do Cadastro! Tente Novamente {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
