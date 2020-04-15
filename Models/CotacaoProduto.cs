using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uni.Models
{
    public class CotacaoProduto
    {
        [Key]
        public int Id_vendaProduto { get; set; }
        public int Quantidade { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Valor { get; set; }

        [ForeignKey("Cotacao_Id_cotacao")]
        public Venda Cotacao { get; set; }

        public int Cotacao_Id_cotacao { get; set; }

        [ForeignKey("Produto_Id_produto")]
        public Produto Produto { get; set; }

        public int Produto_Id_produto { get; set; }
    }
}
