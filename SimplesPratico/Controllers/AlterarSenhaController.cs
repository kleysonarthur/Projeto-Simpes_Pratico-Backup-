
using Microsoft.AspNetCore.Mvc;
using SimplesPratico.Helper;
using SimplesPratico.Models;
using SimplesPratico.Repositorio;

namespace SimplesPratico.Controllers {
    public class AlterarSenhaController : Controller {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly ISessao _sessao;

        public AlterarSenhaController(IFuncionarioRepositorio funcionarioRepositorio, ISessao sessao) {
            _funcionarioRepositorio = funcionarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(AlterarSenhaModel alterarSenhaModel) {
            try {
                FuncionarioModel funcionarioLogado = _sessao.BuscarSessao();
                alterarSenhaModel.Id = funcionarioLogado.Id;
                if(ModelState.IsValid) {

                    _funcionarioRepositorio.AlterarSenha(alterarSenhaModel);
                    TempData["MensagemSucesso"] = "Senha alterada com sucesso!";
                    return View("Index", alterarSenhaModel);
                }
                return View("Index", alterarSenhaModel);
            }
            catch (Exception erro) {
                TempData["MensagemErro"] = $"Não foi possivel alterar sua senha, tente novamente ou contate o suporte local! {erro.Message}";
                return View("Index", alterarSenhaModel);
            }
        }
    }
}
