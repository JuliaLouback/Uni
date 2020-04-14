using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uni.Models
{
    public class CriarRole
    {
        [Key]

        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Informe um nome válido")]
        [Display(Name = "Nome")]
        public string NomeRole { get; set; }
    }
}
