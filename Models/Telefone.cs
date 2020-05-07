using System.ComponentModel.DataAnnotations;

namespace Uni.Models
{
    public class Telefone
    {
        [Key]
        public int Id_telefone { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório", AllowEmptyStrings = false)]
        
        [Display(Name = "Telefone")]
        [StringLength(14, ErrorMessage = "Número incorreto", MinimumLength = 6)]
        public string Telefones { get; set; }
    }
}
