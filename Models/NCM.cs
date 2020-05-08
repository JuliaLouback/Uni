using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Uni.Models
{
    public class NCM
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Display(Name = "NCM")]
        [Required(ErrorMessage = "NCM é obrigatório", AllowEmptyStrings = false)]
        public long Codigo { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Descrição")]
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
