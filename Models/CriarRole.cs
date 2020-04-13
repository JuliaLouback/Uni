using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Uni.Models
{
    public class CriarRole
    {
        [Key]

        public int Id { get; set; }
        public string NomeRole { get; set; }
    }
}
