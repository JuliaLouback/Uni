using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Uni.Models
{
    public class FuncionarioStatusViewModel
    {
        public List<Funcionario> Funcionarios { get; set; }
        public SelectList Status { get; set; }
        public string FuncionarioStatus { get; set; }
        public string SearchString { get; set; }
    }
}
