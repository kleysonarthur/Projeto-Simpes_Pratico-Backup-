using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimplesPratico.Models;

namespace SimplesPratico.ViewComponents {
    public class Menu : ViewComponent {
        public async Task<IViewComponentResult> InvokeAsync() {
            string sessaoFuncionario = HttpContext.Session.GetString("Logado");
            if(string.IsNullOrEmpty(sessaoFuncionario)) return null;
            FuncionarioModel funcionario = JsonConvert.DeserializeObject<FuncionarioModel>(sessaoFuncionario);
            return View(funcionario);
        }
    }
}
