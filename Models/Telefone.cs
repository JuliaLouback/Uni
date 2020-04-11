using System.ComponentModel.DataAnnotations;

namespace Uni.Models
{
    public class Telefone
    {
        [Key]
        public int Id_telefone { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Telefone")]
        public long Telefones { get; set; }
    }
}
