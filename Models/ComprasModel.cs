using System.ComponentModel.DataAnnotations;

namespace WebListaMercado.Models
{
    public class ComprasModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Digite o nome do item")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite a marca do item")]
        public string Marca { get; set; }
        
        public int Quantidade { get; set; }
        
        [Required(ErrorMessage = "Digite os detalhes do item")]
        public string Detalhes { get; set; }

        public int? UsuarioId { get; set; }

        public UsuarioModel? Usuario { get; set; }
    }
}
