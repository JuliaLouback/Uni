using Microsoft.EntityFrameworkCore;
using Uni.Models;

namespace Uni.Data
{
    public class UniContext : DbContext
    {
        public UniContext(DbContextOptions<UniContext> options)
            : base(options)
        {
        }

        public DbSet<Uni.Models.Fornecedor> Fornecedor { get; set; }

        public DbSet<Uni.Models.Telefone> Telefone { get; set; }
        public DbSet<Uni.Models.Endereco> Endereco { get; set; }
        public DbSet<Uni.Models.Funcionario> Funcionario { get; set; }
        public DbSet<Uni.Models.Cliente> Cliente { get; set; }
        public DbSet<Uni.Models.Produto> Produto { get; set; }
        public DbSet<Uni.Models.Venda> Venda { get; set; }
        public DbSet<Uni.Models.VendaProduto> VendaProduto { get; set; }


    }
}
