﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Uni.Data;

namespace Uni.Migrations
{
    [DbContext(typeof(UniContext))]
    [Migration("20200508023301_status")]
    partial class status
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Uni.Models.CFOP", b =>
                {
                    b.Property<long>("Codigo")
                        .HasColumnType("bigint");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Codigo");

                    b.ToTable("CFOP");
                });

            modelBuilder.Entity("Uni.Models.CST", b =>
                {
                    b.Property<string>("Codigo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Codigo");

                    b.ToTable("CST");
                });

            modelBuilder.Entity("Uni.Models.Cliente", b =>
                {
                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Endereco_Id_endereco")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<int>("Telefone_Id_telefone")
                        .HasColumnType("int");

                    b.HasKey("Cpf");

                    b.HasIndex("Endereco_Id_endereco");

                    b.HasIndex("Telefone_Id_telefone");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("Uni.Models.Cotacao", b =>
                {
                    b.Property<int>("Id_cotacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cliente_Cpf")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("Data_venda")
                        .HasColumnType("datetime2");

                    b.Property<string>("Funcionario_Cpf")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Valor_total")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id_cotacao");

                    b.HasIndex("Cliente_Cpf");

                    b.HasIndex("Funcionario_Cpf");

                    b.ToTable("Cotacao");
                });

            modelBuilder.Entity("Uni.Models.CotacaoProduto", b =>
                {
                    b.Property<int>("Id_vendaProduto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cotacao_Id_cotacao")
                        .HasColumnType("int");

                    b.Property<int>("Produto_Id_produto")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id_vendaProduto");

                    b.HasIndex("Cotacao_Id_cotacao");

                    b.HasIndex("Produto_Id_produto");

                    b.ToTable("CotacaoProduto");
                });

            modelBuilder.Entity("Uni.Models.Endereco", b =>
                {
                    b.Property<int>("Id_endereco")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("nvarchar(45)")
                        .HasMaxLength(45);

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(45)")
                        .HasMaxLength(45);

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("Id_endereco");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("Uni.Models.Fornecedor", b =>
                {
                    b.Property<string>("Cnpj")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<int>("Endereco_Id_endereco")
                        .HasColumnType("int");

                    b.Property<string>("Inscricao_estadual")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Inscricao_municipal")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome_empresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<int>("Telefone_Id_telefone")
                        .HasColumnType("int");

                    b.HasKey("Cnpj");

                    b.HasIndex("Endereco_Id_endereco");

                    b.HasIndex("Telefone_Id_telefone");

                    b.ToTable("Fornecedor");
                });

            modelBuilder.Entity("Uni.Models.Funcionario", b =>
                {
                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasColumnType("nvarchar(45)")
                        .HasMaxLength(45);

                    b.Property<DateTime>("Data_nascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Endereco_Id_endereco")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("Senha")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(45)")
                        .HasMaxLength(45);

                    b.Property<int>("Telefone_Id_telefone")
                        .HasColumnType("int");

                    b.HasKey("Cpf");

                    b.HasIndex("Endereco_Id_endereco");

                    b.HasIndex("Telefone_Id_telefone");

                    b.ToTable("Funcionario");
                });

            modelBuilder.Entity("Uni.Models.NCM", b =>
                {
                    b.Property<long>("Codigo")
                        .HasColumnType("bigint");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Codigo");

                    b.ToTable("NCM");
                });

            modelBuilder.Entity("Uni.Models.Produto", b =>
                {
                    b.Property<int>("Id_produto")
                        .HasColumnType("int");

                    b.Property<long>("CFOP_Codigo")
                        .HasColumnType("bigint");

                    b.Property<string>("CST_Codigo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Estoque_atual")
                        .HasColumnType("bigint");

                    b.Property<long>("Estoque_maximo")
                        .HasColumnType("bigint");

                    b.Property<long>("Estoque_minimo")
                        .HasColumnType("bigint");

                    b.Property<string>("Fornecedor_Cnpj")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("NCM_Codigo")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("Peso_bruto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Peso_liquido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unidade_medida")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("Valor_unitario")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id_produto");

                    b.HasIndex("CFOP_Codigo");

                    b.HasIndex("CST_Codigo");

                    b.HasIndex("Fornecedor_Cnpj");

                    b.HasIndex("NCM_Codigo");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("Uni.Models.Telefone", b =>
                {
                    b.Property<int>("Id_telefone")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Telefones")
                        .IsRequired()
                        .HasColumnType("nvarchar(14)")
                        .HasMaxLength(14);

                    b.HasKey("Id_telefone");

                    b.ToTable("Telefone");
                });

            modelBuilder.Entity("Uni.Models.Venda", b =>
                {
                    b.Property<int>("Id_venda")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cliente_Cpf")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("Data_venda")
                        .HasColumnType("datetime2");

                    b.Property<string>("Funcionario_Cpf")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Valor_total")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id_venda");

                    b.HasIndex("Cliente_Cpf");

                    b.HasIndex("Funcionario_Cpf");

                    b.ToTable("Venda");
                });

            modelBuilder.Entity("Uni.Models.VendaProduto", b =>
                {
                    b.Property<int>("Id_vendaProduto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Produto_Id_produto")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int>("Venda_Id_venda")
                        .HasColumnType("int");

                    b.HasKey("Id_vendaProduto");

                    b.HasIndex("Produto_Id_produto");

                    b.HasIndex("Venda_Id_venda");

                    b.ToTable("VendaProduto");
                });

            modelBuilder.Entity("Uni.Models.Cliente", b =>
                {
                    b.HasOne("Uni.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("Endereco_Id_endereco")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Models.Telefone", "Telefone")
                        .WithMany()
                        .HasForeignKey("Telefone_Id_telefone")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Uni.Models.Cotacao", b =>
                {
                    b.HasOne("Uni.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("Cliente_Cpf");

                    b.HasOne("Uni.Models.Funcionario", "Funcionario")
                        .WithMany()
                        .HasForeignKey("Funcionario_Cpf");
                });

            modelBuilder.Entity("Uni.Models.CotacaoProduto", b =>
                {
                    b.HasOne("Uni.Models.Cotacao", "Cotacao")
                        .WithMany()
                        .HasForeignKey("Cotacao_Id_cotacao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("Produto_Id_produto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Uni.Models.Fornecedor", b =>
                {
                    b.HasOne("Uni.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("Endereco_Id_endereco")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Models.Telefone", "Telefone")
                        .WithMany()
                        .HasForeignKey("Telefone_Id_telefone")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Uni.Models.Funcionario", b =>
                {
                    b.HasOne("Uni.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("Endereco_Id_endereco")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Models.Telefone", "Telefone")
                        .WithMany()
                        .HasForeignKey("Telefone_Id_telefone")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Uni.Models.Produto", b =>
                {
                    b.HasOne("Uni.Models.CFOP", "CFOP")
                        .WithMany()
                        .HasForeignKey("CFOP_Codigo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Models.CST", "CST")
                        .WithMany()
                        .HasForeignKey("CST_Codigo");

                    b.HasOne("Uni.Models.Fornecedor", "Fornecedor")
                        .WithMany()
                        .HasForeignKey("Fornecedor_Cnpj");

                    b.HasOne("Uni.Models.NCM", "NCM")
                        .WithMany()
                        .HasForeignKey("NCM_Codigo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Uni.Models.Venda", b =>
                {
                    b.HasOne("Uni.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("Cliente_Cpf");

                    b.HasOne("Uni.Models.Funcionario", "Funcionario")
                        .WithMany()
                        .HasForeignKey("Funcionario_Cpf");
                });

            modelBuilder.Entity("Uni.Models.VendaProduto", b =>
                {
                    b.HasOne("Uni.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("Produto_Id_produto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uni.Models.Venda", "Venda")
                        .WithMany()
                        .HasForeignKey("Venda_Id_venda")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
