using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Uni.Models
{
    public class Fornecedor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Required(ErrorMessage = "CNPJ é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "CNPJ")]
        public long Cnpj { get; set; }

        [Required(ErrorMessage = "Nome da empresa é obrigatório", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Informe uma empresa válida")]
        [Display(Name = "Nome")]
        [StringLength(80)]
        public string NomeEmpresa { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "E-mail")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
        [StringLength(60)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Inscrição Estadual é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Inscrição Estadual")]
        public long InscricaoEstadual { get; set; }

        [Required(ErrorMessage = "Inscrição Municipal é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Inscrição Municipal")]
        public long InscricaoMunicipal { get; set; }

        [ForeignKey("Endereco_Id_endereco")]
        public Endereco Endereco { get; set; }

        public int Endereco_Id_endereco { get; set; }

        [ForeignKey("Telefone_Id_telefone")]
        public Telefone Telefone { get; set; }

        public int Telefone_Id_telefone { get; set; }

    }
}
