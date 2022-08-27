using AppControleJuridico.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppControleJuridico.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Erro tem que ter três posições e será recebido como int
        /// </summary>
        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Mensagem = "Erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Titulo = "Erro!";
                modelErro.ErroCode = id;
            }
            else if (id == 404)
            {
                modelErro.Mensagem = "Página inexistente! <br /> Em caso de dúvidas entre em contato com o nosso suporte.";
                modelErro.Titulo = "Página não encontrada.";
                modelErro.ErroCode = id;

            }
            else if (id == 403)
            {
                modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                modelErro.Titulo = "Acesso Negado.";
                modelErro.ErroCode = id;
            }
            else
            {
                return StatusCode(404);
            }
            ///<summary>
            ///Passamos essa modelErro para a View do Erro
            /// </summary>
            return View("Error", modelErro);
        }
    }
}