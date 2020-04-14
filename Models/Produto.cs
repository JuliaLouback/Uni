using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uni.Models
{
    public class Produto
    {
        [Key]
        public int Id_produto { get; set; }

        [Required(ErrorMessage = "Nome do Produto", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [Display(Name = "Nome")]
        [StringLength(80)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Valor do produto é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Valor Unitário")]
        [DataType(DataType.Currency)]
        [StringLength(10)]
        public string Valor_unitario { get; set; }

        [Required(ErrorMessage = "Unidade de medida é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Unidade de Medida")]
        [StringLength(60)]
        public string Unidade_medida { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatório", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [Display(Name = "Descrição do produto")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Estoque minimo é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Estoque Mínimo")]
        [Range(1,9999, ErrorMessage = "O valor mínimo deve ser {1}")]
        public long Estoque_minimo { get; set; }

        [Required(ErrorMessage = "Estoque maximo é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Estoque Máximo")]
        [Range(1, 9999, ErrorMessage = "O valor mínimo deve ser {1}")]
        public long Estoque_maximo { get; set; }

        [Required(ErrorMessage = "Estoque atual", AllowEmptyStrings = false)]
        [Display(Name = "Estoque Atual")]
        public long Estoque_atual { get; set; }

        [ForeignKey("Fornecedor_Cnpj")]
        public Fornecedor Fornecedor { get; set; }
        [Display(Name = "Fornecedor")]
        public long Fornecedor_Cnpj { get; set; }


    }
}