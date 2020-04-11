using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uni.Models
{
    public class VendaProduto
    {
        [Key]
        public int Id_vendaProduto { get; set; }
        public int Quantidade { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Valor { get; set; }

        [ForeignKey("Venda_Id_venda")]
        public Venda Venda { get; set; }

        public int Venda_Id_venda { get; set; }

        [ForeignKey("Produto_Id_produto")]
        public Produto Produto { get; set; }

        public int Produto_Id_produto { get; set; }
    }
}
