﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Uni.Models
{
    public class HistoricoStatus
    {
        [Key]
        public int Id_historicoStatus { get; set; }

        [Display(Name = "Data Início")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? Data_inicio { get; set; }

        [Display(Name = "Data Final")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? Data_final { get; set; }

        [ForeignKey("Funcionario_Cpf")]
        public Funcionario Funcionario { get; set; }

        [Display(Name = "Funcionário")]
        public string Funcionario_Cpf { get; set; }

        public string Status { get; set; }
    }
}
