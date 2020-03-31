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
        public long Cnpj { get; set; }

        [Required]
        [Display(Name = "Nome")]
        [StringLength(80)]
        public string NomeEmpresa { get; set; }

        [Display(Name = "E-mail")]
        [StringLength(60)]
        public string Email { get; set; }

        [Display(Name = "Inscrição Estadual")]
        public long InscricaoEstadual { get; set; }

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
