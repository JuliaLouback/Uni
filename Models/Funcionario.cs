using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Uni.Models
{
    public class Funcionario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public long Cpf { get; set; }

        [Required]
        [StringLength(80)]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(45)]
        public string Cargo { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime Data_nascimento { get; set; }

        [ForeignKey("Endereco_Id_endereco")]
        public Endereco Endereco { get; set; }

        public int Endereco_Id_endereco { get; set; }

        [ForeignKey("Telefone_Id_telefone")]
        public Telefone Telefone { get; set; }

        public int Telefone_Id_telefone { get; set; }
    }
}
