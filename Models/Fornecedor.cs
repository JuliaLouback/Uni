using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uni.Models
{
    public class Fornecedor
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Required(ErrorMessage = "CNPJ é obrigatório")]
        [Display(Name = "CNPJ")]
        public long Cnpj { get; set; }

        [Required(ErrorMessage = "Nome da empresa é obrigatório")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Informe uma empresa válida")]
        [Display(Name = "Empresa")]
        [StringLength(80)]
        public string Nome_empresa { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório")]
        [Display(Name = "E-mail")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
        [StringLength(60)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Inscrição Estadual é obrigatório")]
        [Display(Name = "Inscrição Estadual")]
        public long Inscricao_estadual { get; set; }

        [Required(ErrorMessage = "Inscrição Municipal é obrigatório")]
        [Display(Name = "Inscrição Municipal")]
        public long Inscricao_municipal { get; set; }

        [ForeignKey("Endereco_Id_endereco")]
        public Endereco Endereco { get; set; }

        public int Endereco_Id_endereco { get; set; }

        [ForeignKey("Telefone_Id_telefone")]
        public Telefone Telefone { get; set; }

        public int Telefone_Id_telefone { get; set; }

    }
}
