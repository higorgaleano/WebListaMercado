using WebListaMercado.Data;
using WebListaMercado.Models;

namespace WebListaMercado.Repositorio
{
    public class ComprasRepositorio : IComprasRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ComprasRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ComprasModel ListarPorId(int id)
        {
            return _bancoContext.Compras.FirstOrDefault(c => c.Id == id);
        }

        public List<ComprasModel> BuscarTodos(int usuarioId)
        {
            return _bancoContext.Compras.Where(x => x.UsuarioId == usuarioId).ToList();
        }

        public ComprasModel Adicionar(ComprasModel compras)
        {
            _bancoContext.Compras.Add(compras);
            _bancoContext.SaveChanges();
            return compras;
        }

        public ComprasModel Atualizar(ComprasModel compras)
        {
            ComprasModel comprasDB = ListarPorId(compras.Id);

            if (comprasDB == null) throw new Exception("Houve um erro na atualização do item!");

            comprasDB.Nome = compras.Nome;
            comprasDB.Marca = compras.Marca;
            comprasDB.Quantidade = compras.Quantidade;
            comprasDB.Detalhes = compras.Detalhes;

            _bancoContext.Compras.Update(comprasDB);
            _bancoContext.SaveChanges();

            return comprasDB;
        }

        public bool Apagar(int id)
        {
            ComprasModel comprasDB = ListarPorId(id);

            if (comprasDB == null) throw new Exception("Houve um erro na deleção do item!");

            _bancoContext.Compras.Remove(comprasDB);
            _bancoContext.SaveChanges();

            return true;
        }
    }
}
