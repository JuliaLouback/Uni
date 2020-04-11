﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uni.Models
{
    public class Venda
    {
        [Key]
        public int Id_venda { get; set; }

        [Required(ErrorMessage = "Data da Venda")]
        [Display(Name = "Data da Venda")]
        // [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? Data_venda { get; set; }

        [Required(ErrorMessage = "Valor", AllowEmptyStrings = false)]
        [Display(Name = "Valor")]

        /*[Column(TypeName = "decimal(10, 2)")]
        public decimal Valor_frete { get; set; }*/

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Valor_total { get; set; }

        [ForeignKey("Funcionario_Cpf")]
        public Funcionario Funcionario { get; set; }

        [Display(Name = "Funcionário")]
        public long Funcionario_Cpf { get; set; }

        [ForeignKey("Cliente_Cpf")]
        public Cliente Cliente { get; set; }

        [Display(Name = "Cliente")]
        public long Cliente_Cpf { get; set; }

    }
}
