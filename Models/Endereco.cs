using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uni.Models
{
    public class Endereco
    {
        [Key]
        public int Id_endereco { get; set; }

        [Display(Name = "CEP")]
        public long Cep { get; set; }

        [Display(Name = "Número")]
        public int Numero { get; set; }

        [StringLength(60)]
        public string Rua { get; set; }

        [StringLength(45)]
        public string Bairro { get; set; }

        [StringLength(45)]
        public string Cidade { get; set; }

        [StringLength(10)]
        public string Estado { get; set; }
    }
}
