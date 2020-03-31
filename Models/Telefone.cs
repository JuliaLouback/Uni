using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uni.Models
{
    public class Telefone
    {
        [Key]
        public int Id_telefone { get; set; }

        [Display (Name ="Telefone")]
        public long Telefones { get; set; }
    }
}
