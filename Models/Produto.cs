using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Uni.Models
{
    public class Produto
    {
        [Key]
        public int Id_produto { get; set; }

        [Required(ErrorMessage = "Nome do Produto", AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        [StringLength(80)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Valor do produto é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Valor Unitario")]
        [StringLength(80)]
        public string Valor_unitario { get; set; }

        [Required(ErrorMessage = "Unidade de medida é obrigatorio", AllowEmptyStrings = false)]
        [Display(Name = "Unidade de Medida")]
        [StringLength(60)]
        public string Unidade_medida { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Descrição do produto")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Estoque minimo é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Estoque Minimo")]
        public long Estoque_minimo { get; set; }

        [Required(ErrorMessage = "Estoque maximo é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Estoque Maximo")]
        public long Estoque_maximo { get; set; }

        [ForeignKey("Fornecedor Cnpj")]
        public Fornecedor Fornecedor { get; set; }

        public long Fornecedor_Cnpj { get; set; }


    }
}