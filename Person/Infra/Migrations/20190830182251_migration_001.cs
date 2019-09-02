using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Person.Infra.Migrations
{
    public partial class migration_001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.CreateTable(
                name: "Estado",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoEm = table.Column<DateTimeOffset>(nullable: true),
                    AtualizadoEm = table.Column<DateTimeOffset>(nullable: true),
                    Deletado = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Sigla = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoEm = table.Column<DateTimeOffset>(nullable: true),
                    AtualizadoEm = table.Column<DateTimeOffset>(nullable: true),
                    Deletado = table.Column<bool>(nullable: false),
                    Login = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cidade",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoEm = table.Column<DateTimeOffset>(nullable: true),
                    AtualizadoEm = table.Column<DateTimeOffset>(nullable: true),
                    Deletado = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Capital = table.Column<bool>(nullable: false),
                    EstadoId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cidade_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalSchema: "Core",
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoEm = table.Column<DateTimeOffset>(nullable: true),
                    AtualizadoEm = table.Column<DateTimeOffset>(nullable: true),
                    Deletado = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Profissao = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    EstadoCivil = table.Column<int>(nullable: false),
                    Etapa = table.Column<int>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    Sexo = table.Column<int>(nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true),
                    ConjugeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pessoa_Pessoa_ConjugeId",
                        column: x => x.ConjugeId,
                        principalSchema: "Core",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pessoa_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "Core",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documento",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoEm = table.Column<DateTimeOffset>(nullable: true),
                    AtualizadoEm = table.Column<DateTimeOffset>(nullable: true),
                    Deletado = table.Column<bool>(nullable: false),
                    Dados = table.Column<string>(nullable: true),
                    ImagemUri = table.Column<string>(nullable: true),
                    Aprovado = table.Column<bool>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    PessoaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documento_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalSchema: "Core",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoEm = table.Column<DateTimeOffset>(nullable: true),
                    AtualizadoEm = table.Column<DateTimeOffset>(nullable: true),
                    Deletado = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    InscricaoEstadual = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    Celular = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Responsavel = table.Column<string>(nullable: true),
                    CPFResponsavel = table.Column<string>(nullable: true),
                    PessoaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empresa_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalSchema: "Core",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CriadoEm = table.Column<DateTimeOffset>(nullable: true),
                    AtualizadoEm = table.Column<DateTimeOffset>(nullable: true),
                    Deletado = table.Column<bool>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    Principal = table.Column<bool>(nullable: false),
                    Numero = table.Column<int>(nullable: false),
                    Complemento = table.Column<int>(nullable: false),
                    Logradouro = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    CEP = table.Column<string>(nullable: true),
                    CidadeId = table.Column<string>(nullable: true),
                    EstadoId = table.Column<string>(nullable: true),
                    PessoaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalSchema: "Core",
                        principalTable: "Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Endereco_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalSchema: "Core",
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Endereco_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalSchema: "Core",
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cidade_EstadoId",
                schema: "Core",
                table: "Cidade",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_PessoaId",
                schema: "Core",
                table: "Documento",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_PessoaId",
                schema: "Core",
                table: "Empresa",
                column: "PessoaId",
                unique: true,
                filter: "[PessoaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_CidadeId",
                schema: "Core",
                table: "Endereco",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_EstadoId",
                schema: "Core",
                table: "Endereco",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_PessoaId",
                schema: "Core",
                table: "Endereco",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_ConjugeId",
                schema: "Core",
                table: "Pessoa",
                column: "ConjugeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_UsuarioId",
                schema: "Core",
                table: "Pessoa",
                column: "UsuarioId",
                unique: true,
                filter: "[UsuarioId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documento",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Empresa",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Endereco",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Cidade",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Pessoa",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Estado",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "Core");
        }
    }
}
