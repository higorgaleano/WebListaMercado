using WebListaMercado.Models;

namespace WebListaMercado.Repositorio
{
    public interface IComprasRepositorio
    {
        ComprasModel ListarPorId(int id);
        List<ComprasModel> BuscarTodos(int UsuarioId);
        ComprasModel Adicionar(ComprasModel compras);
        ComprasModel Atualizar(ComprasModel compras);
        bool Apagar(int id);
    }
}
