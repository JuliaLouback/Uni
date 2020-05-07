using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Uni.Models
{
    public class CFOP
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Display(Name = "CFOP")]
        [Required(ErrorMessage = "CFOP é obrigatório", AllowEmptyStrings = false)]
        public long Codigo { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatório", AllowEmptyStrings = false)]
        
        public string Descricao { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return Codigo + " - " + Descricao;
            }
        }
    }
}
