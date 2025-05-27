using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SimplesPratico.Models;

namespace SimplesPratico.Helper {
    public class Sessao : ISessao {
        private readonly IHttpContextAccessor _httpContext;

        public Sessao(IHttpContextAccessor httpContext) {
            _httpContext = httpContext;
        }

        FuncionarioModel ISessao.BuscarSessao() {
            string sessaoFuncionario = _httpContext.HttpContext.Session.GetString("Logado");
            if (string.IsNullOrEmpty(sessaoFuncionario)) return null;
            return JsonConvert.DeserializeObject<FuncionarioModel>(sessaoFuncionario);
        }

        void ISessao.CriarSessao(FuncionarioModel funcionario) {
            string valor = JsonConvert.SerializeObject(funcionario);
            _httpContext.HttpContext.Session.SetString("Logado", valor);
        }

        void ISessao.RemoverSessao() {
            _httpContext.HttpContext.Session.Remove("Logado");
        }
    }
}
