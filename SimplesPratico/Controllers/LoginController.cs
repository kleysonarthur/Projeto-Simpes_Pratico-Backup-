using Microsoft.AspNetCore.Mvc;
using SimplesPratico.Filters;
using SimplesPratico.Helper;
using SimplesPratico.Models;
using SimplesPratico.Repositorio;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimplesPratico.Controllers {
    public class LoginController : Controller {
        private readonly IFuncionarioRepositorio _funcionarioRepositorio;
        private readonly ISessao _sessao;
        private readonly EmailService _emailService;

        public LoginController(IFuncionarioRepositorio funcionarioRepositorio, ISessao sessao, EmailService emailService) {
            _funcionarioRepositorio = funcionarioRepositorio;
            _sessao = sessao;
            _emailService = emailService;
        }

        public IActionResult Index() {
            //Se estiver logado, redirecionar para a Home
            if(_sessao.BuscarSessao() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult RedefinirSenha() {
            return View();
        }

        public IActionResult Sair() {
            _sessao.RemoverSessao();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel) {
            try {
                if (ModelState.IsValid) {
                    FuncionarioModel funcionario = _funcionarioRepositorio.BuscarPorLogin(loginModel.Login);
                    if (funcionario != null) {
                        if (funcionario.SenhaValida(loginModel.Senha)) {
                            _sessao.CriarSessao(funcionario);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = $"Senha inválida!";
                    }
                    TempData["MensagemErro"] = $"Usuário e/ou senha inválido(s)!";
                }
                return View("Index");
            }
            catch (Exception erro) {

                TempData["MensagemErro"] = $"Não foi possivel realizar o login, tente novamente ou contate os suporte local: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EnviarLink(RedefinirSenhaModel redefinirSenhaModel) {
            try {
                if (ModelState.IsValid) {
                    FuncionarioModel funcionario = _funcionarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email,redefinirSenhaModel.Login);
                    if (funcionario != null) {
                        string novaSenha = funcionario.GerarNovaSenha();
                        string mensagem = $"Sua nova senha é: {novaSenha}";
                        bool emailEnviado = _emailService.EnviarEmail(funcionario.Email, "Nova Senha", mensagem);
                        if (emailEnviado) {
                            _funcionarioRepositorio.Atualizar(funcionario);
                            TempData["MensagemSucesso"] = $"Enviamos para o seu email cadastrado uma nova senha.";
                        }
                        else {
                            TempData["MensagemErro"] = $"Não foi possivel enviar o email. Verifique os dados informados ou tente novamente!";
                        }
                            return RedirectToAction("Index", "Login");
                    }
                    TempData["MensagemErro"] = $"Não foi possivel redefinir a senha. Verifique os dados informados!";
                }
                return View("Index");
            }
            catch (Exception erro) {

                TempData["MensagemErro"] = $"Não foi possivel redefinir a senha, tente novamente ou contate o suporte local! {erro.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
