using Microsoft.AspNetCore.Mvc;
using WebListaMercado.Filters;
using WebListaMercado.Models;
using WebListaMercado.Repositorio;

namespace WebListaMercado.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IComprasRepositorio _comprasRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio,
                                 IComprasRepositorio comprasRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _comprasRepositorio = comprasRepositorio;
        }

        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuário apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos excluir o usuário, tente novamente";
                }

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos excluir o usuário da lista, tente novamente, detalhe do erro {erro.Message}";
                return RedirectToAction("Index");
            } 
        }

        public IActionResult ListarItensComprasPorUsuarioId(int id)
        {
            List<ComprasModel> compras = _comprasRepositorio.BuscarTodos(id);
            return PartialView("_ComprasUsuario", compras);
        }


        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    usuario = _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o usuário, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenha)
        {
            try
            {
                UsuarioModel? usuario = null;

                if(ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenha.Id,
                        Nome = usuarioSemSenha.Nome,
                        Login = usuarioSemSenha.Login,
                        Email = usuarioSemSenha.Email,
                        Perfil = usuarioSemSenha.Perfil
                    };

                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário alterado com sucesso";
                    return RedirectToAction("Index");
                }

                return View("Editar", usuario);

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar o usuário, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");            
            }
        }

    }
}
