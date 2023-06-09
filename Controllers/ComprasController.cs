using Microsoft.AspNetCore.Mvc;
using WebListaMercado.Filters;
using WebListaMercado.Helper;
using WebListaMercado.Models;
using WebListaMercado.Repositorio;

namespace WebListaMercado.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ComprasController : Controller
    {
        private readonly IComprasRepositorio _comprasRepositorio;
        private readonly ISessao _sessao;
        public ComprasController(IComprasRepositorio comprasRepositorio,
                                 ISessao sessao)
        {
            _comprasRepositorio = comprasRepositorio;
            _sessao = sessao;
        }


        public IActionResult Index()
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            List<ComprasModel> compras = _comprasRepositorio.BuscarTodos(usuarioLogado.Id);
            return View(compras);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            ComprasModel compras = _comprasRepositorio.ListarPorId(id);
            return View(compras);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ComprasModel compras = _comprasRepositorio.ListarPorId(id);
            return View(compras);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _comprasRepositorio.Apagar(id);

                if(apagado)
                {
                    TempData["MensagemSucesso"] = "Item excluído com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos excluir seu item da lista, tente novamente";
                }
                
                return RedirectToAction("Index");

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos excluir seu item da lista, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }  
        }


        [HttpPost]
        public IActionResult Criar(ComprasModel compras)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    compras.UsuarioId = usuarioLogado.Id;

                    compras = _comprasRepositorio.Adicionar(compras);

                    TempData["MensagemSucesso"] = "Item cadastrado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(compras);

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu item, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }     
        }

        [HttpPost]
        public IActionResult Alterar(ComprasModel compras)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    compras.UsuarioId = usuarioLogado.Id;

                    compras = _comprasRepositorio.Atualizar(compras);

                    TempData["MensagemSucesso"] = "Item alterado com sucesso";
                    return RedirectToAction("Index");
                }

                return View("Editar", compras);

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar seu item, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }  
        }
    }
}
