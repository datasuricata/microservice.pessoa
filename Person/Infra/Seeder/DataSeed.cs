
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Person.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Person.Infra.Seeder {
    public static class DataSeed {
        public static async Task InitializeAsync(IServiceProvider service) {

            using (var context = new AppDbContext(service.GetRequiredService<DbContextOptions<AppDbContext>>())) {

                if (!context.Usuarios.Any()) {

                    var estado = new Estado("Paraná", "PR");

                    var usuario = new Usuario("root", "xCF2zdWcmfQBDZpF", "root@autotech.com") {
                        Pessoa = new Pessoa("Master", "Admin System", "41998623719", Core.Enums.EstadoCivil.Casado, Core.Enums.EtapaAtual.Aprovado, Core.Enums.TipoPessoa.PJ, Core.Enums.TipoSexo.M, new DateTime(1994, 09, 22)) {
                            Empresa = new Empresa("Data Suricata", "0000", "41998623719", "41998623719", "lucas.moraes.dev@gmail.com", "nobody", null),
                            Documentos = new List<Documento>() { new Documento("28.010.014/0001-05", "https://www.google.com.br", Core.Enums.TipoDocumento.CNPJ) },
                            Enderecos = new List<Endereco>() { new Endereco(Core.Enums.TipoConstrucao.Comercial, true, 10, 20, "Av Digital Hacking", "Skynet", "00000000") {
                                Estado = estado,
                                Cidade = new Cidade("Curitiba", true) {
                                    Estado = estado,
                                },
                            }}
                        }
                    };

                    usuario.DefinirTipo(Core.Enums.TipoUsuario.Root);

                    context.Usuarios.Add(usuario);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}