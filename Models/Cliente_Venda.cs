using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Uni.Models
{
    public class Cliente_Venda
    {
        [Key]
        public int Id_clienteVenda { get; set; }

        [ForeignKey("Cliente_Cpf")]
        public Cliente Cliente { get; set; }

        public long Cliente_Cpf { get; set; }

        [ForeignKey("Venda_Id_venda")]
        public Venda Venda { get; set; }

        public int Venda_Id_venda { get; set; }
    }
}
