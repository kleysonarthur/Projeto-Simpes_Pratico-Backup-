using Microsoft.AspNetCore.Mvc;
using SimplesPratico.Filters;
using SimplesPratico.Helper;
using SimplesPratico.Models;
using SimplesPratico.Repositorio;

namespace SimplesPratico.Controllers {
    [FiltroLogado]
    public class ClientesController : Controller {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly ISessao _sessao;

        public ClientesController(IClienteRepositorio clienteRepositorio, ISessao sessao) {
            _clienteRepositorio = clienteRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index() {
            FuncionarioModel funcionarioLogado = _sessao.BuscarSessao();
            List<ClienteModel> clientes = _clienteRepositorio.BuscarTodos(funcionarioLogado.Id);
            return View(clientes);
        }
        public IActionResult Novo() {
            return View();
        }
        public IActionResult Datlhes() {
            return View();
        }
        public IActionResult Editar(int id) {
            ClienteModel cliente = _clienteRepositorio.ListarPorId(id);
            return View(cliente);
        }
        public IActionResult ApagarConfirmacao(int id) {
            ClienteModel cliente = _clienteRepositorio.ListarPorId(id);
            return View(cliente);
        }
        public IActionResult Apagar(int id) {
            try {
                bool apagado = _clienteRepositorio.Apagar(id);
                if (apagado) {
                    TempData["MensagemSucesso"] = "Cliente EXCLUIDO com SUCESSO!";
                }
                else {
                    TempData["MensagemErro"] = $"ERRO na Exclusão do Cadastro! Tente Novamente!";
                }
                return RedirectToAction("Index");
            }
            catch (System.Exception erro) {
                TempData["MensagemErro"] = $"ERRO na Exclusão do Cadastro! Detalhe: {erro.Message}";
                return RedirectToAction("Index");
            }
        }





        [HttpPost]
        public IActionResult Novo(ClienteModel cliente) {
            try {
                if (ModelState.IsValid) {
                    FuncionarioModel funcionarioLogado = _sessao.BuscarSessao();
                    cliente.FuncionarioId = funcionarioLogado.Id;
                    cliente = _clienteRepositorio.Adicionar(cliente);
                    TempData["MensagemSucesso"] = "Cliente Cadastrado com SUCESSO!";
                    return RedirectToAction("Index");
                }
                return View(cliente);
            }
            catch (System.Exception erro) {
                TempData["MensagemErro"] = $"ERRO no Cadastro! Tente Novamente {erro.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Atualizar(ClienteModel cliente) {
            try {
                if (ModelState.IsValid) {
                    FuncionarioModel funcionarioLogado = _sessao.BuscarSessao();
                    cliente.FuncionarioId = funcionarioLogado.Id;
                    cliente = _clienteRepositorio.Atualizar(cliente);
                    TempData["MensagemSucesso"] = "Cadastrado ATUALIZADO com SUCESSO!";
                    return RedirectToAction("Index");
                }
                return View("Editar", cliente);
            }
            catch (SystemException erro) {
                TempData["MensagemErro"] = $"ERRO na Atualização do Cadastro! Tente Novamente {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
