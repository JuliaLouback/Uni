using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Uni.Models
{
    public class Venda
    {
        [Key]
        public int Id_venda { get; set; }

        [Required(ErrorMessage = "Data da Venda", AllowEmptyStrings = false)]
        [Display(Name = "Data da Venda")]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        [DataType(DataType.Date)]
        public DateTime Data_venda { get; set; }

        [Required(ErrorMessage = "Valor do Frete", AllowEmptyStrings = false)]
        [Display(Name = "Valor do Frete")]

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Valor_frete { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Valor_total { get; set; }

        [ForeignKey("Funcionario_Cpf")]
        public Funcionario Funcionario { get; set; }

        public long Funcionario_Cpf { get; set; }

    }
}
