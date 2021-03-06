﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Person.Infra;

namespace Person.Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190903141833_migration_0001")]
    partial class migration_0001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Core")
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Person.Core.Cidade", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("AtualizadoEm");

                    b.Property<bool>("Capital");

                    b.Property<DateTimeOffset?>("CriadoEm");

                    b.Property<bool>("Deletado");

                    b.Property<string>("EstadoId");

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.ToTable("Cidade");
                });

            modelBuilder.Entity("Person.Core.Documento", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Aprovado");

                    b.Property<DateTimeOffset?>("AtualizadoEm");

                    b.Property<DateTimeOffset?>("CriadoEm");

                    b.Property<string>("Dados");

                    b.Property<bool>("Deletado");

                    b.Property<string>("ImagemUri");

                    b.Property<string>("PessoaId");

                    b.Property<int>("Tipo");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId");

                    b.ToTable("Documento");
                });

            modelBuilder.Entity("Person.Core.Empresa", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("AtualizadoEm");

                    b.Property<string>("CPFResponsavel");

                    b.Property<string>("Celular");

                    b.Property<DateTimeOffset?>("CriadoEm");

                    b.Property<bool>("Deletado");

                    b.Property<string>("Email");

                    b.Property<string>("InscricaoEstadual");

                    b.Property<string>("Nome");

                    b.Property<string>("PessoaId");

                    b.Property<string>("Responsavel");

                    b.Property<string>("Telefone");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId")
                        .IsUnique()
                        .HasFilter("[PessoaId] IS NOT NULL");

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("Person.Core.Endereco", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("AtualizadoEm");

                    b.Property<string>("Bairro");

                    b.Property<string>("CEP");

                    b.Property<string>("CidadeId");

                    b.Property<int?>("Complemento");

                    b.Property<DateTimeOffset?>("CriadoEm");

                    b.Property<bool>("Deletado");

                    b.Property<string>("EstadoId");

                    b.Property<string>("Logradouro");

                    b.Property<int>("Numero");

                    b.Property<string>("PessoaId");

                    b.Property<bool>("Principal");

                    b.Property<int>("Tipo");

                    b.HasKey("Id");

                    b.HasIndex("CidadeId");

                    b.HasIndex("EstadoId");

                    b.HasIndex("PessoaId");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("Person.Core.Estado", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("AtualizadoEm");

                    b.Property<DateTimeOffset?>("CriadoEm");

                    b.Property<bool>("Deletado");

                    b.Property<string>("Nome");

                    b.Property<string>("Sigla");

                    b.HasKey("Id");

                    b.ToTable("Estado");
                });

            modelBuilder.Entity("Person.Core.Pessoa", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("AtualizadoEm");

                    b.Property<string>("ConjugeId");

                    b.Property<DateTimeOffset?>("CriadoEm");

                    b.Property<DateTime>("DataNascimento");

                    b.Property<bool>("Deletado");

                    b.Property<int>("EstadoCivil");

                    b.Property<int>("Etapa");

                    b.Property<string>("Nome");

                    b.Property<string>("Profissao");

                    b.Property<int>("Sexo");

                    b.Property<string>("Telefone");

                    b.Property<int>("Tipo");

                    b.Property<string>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("ConjugeId");

                    b.HasIndex("UsuarioId")
                        .IsUnique()
                        .HasFilter("[UsuarioId] IS NOT NULL");

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("Person.Core.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("AtualizadoEm");

                    b.Property<DateTimeOffset?>("CriadoEm");

                    b.Property<bool>("Deletado");

                    b.Property<string>("Email");

                    b.Property<string>("Login");

                    b.Property<string>("Senha");

                    b.Property<int>("Tipo");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Person.Core.Cidade", b =>
                {
                    b.HasOne("Person.Core.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId");
                });

            modelBuilder.Entity("Person.Core.Documento", b =>
                {
                    b.HasOne("Person.Core.Pessoa", "Pessoa")
                        .WithMany("Documentos")
                        .HasForeignKey("PessoaId");
                });

            modelBuilder.Entity("Person.Core.Empresa", b =>
                {
                    b.HasOne("Person.Core.Pessoa", "Pessoa")
                        .WithOne("Empresa")
                        .HasForeignKey("Person.Core.Empresa", "PessoaId");
                });

            modelBuilder.Entity("Person.Core.Endereco", b =>
                {
                    b.HasOne("Person.Core.Cidade", "Cidade")
                        .WithMany()
                        .HasForeignKey("CidadeId");

                    b.HasOne("Person.Core.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId");

                    b.HasOne("Person.Core.Pessoa", "Pessoa")
                        .WithMany("Enderecos")
                        .HasForeignKey("PessoaId");
                });

            modelBuilder.Entity("Person.Core.Pessoa", b =>
                {
                    b.HasOne("Person.Core.Pessoa", "Conjuge")
                        .WithMany()
                        .HasForeignKey("ConjugeId");

                    b.HasOne("Person.Core.Usuario", "Usuario")
                        .WithOne("Pessoa")
                        .HasForeignKey("Person.Core.Pessoa", "UsuarioId");
                });
#pragma warning restore 612, 618
        }
    }
}
