using System;
using System.ComponentModel.DataAnnotations;

namespace Uni.Models
{
    public class AddProdutoView
    {
        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "Produto")]

        public int ProductId { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(0, double.MaxValue, ErrorMessage = "Valores maiores que 0")]
        [Display(Name = "Quantidade")]

        public double Quantity { get; set; }
    }
}
