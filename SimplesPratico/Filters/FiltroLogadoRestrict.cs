using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SimplesPratico.Models;

namespace SimplesPratico.Filters {
    public class FiltroLogadoRestrict : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext context) {
            string sessaoFuncionario = context.HttpContext.Session.GetString("Logado");
            if (string.IsNullOrEmpty(sessaoFuncionario)) {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            }
            else {
                FuncionarioModel funcionario = JsonConvert.DeserializeObject<FuncionarioModel>(sessaoFuncionario);
                if (funcionario == null) {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }
                if(funcionario.Perfil != Models.Enum.Perfil.Administrador || funcionario.Perfil == Models.Enum.Perfil.RH) {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
