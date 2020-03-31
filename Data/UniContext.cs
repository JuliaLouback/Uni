using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Uni.Models;

namespace Uni.Data
{
    public class UniContext : DbContext
    {
        public UniContext (DbContextOptions<UniContext> options)
            : base(options)
        {
        }

        public DbSet<Uni.Models.Fornecedor> Fornecedor { get; set; }

        public DbSet<Uni.Models.Telefone> Telefone { get; set; }
        public DbSet<Uni.Models.Endereco> Endereco { get; set; }
        public DbSet<Uni.Models.Funcionario> Funcionario { get; set; }
        public DbSet<Uni.Models.Cliente> Cliente { get; set; }

    }
}
