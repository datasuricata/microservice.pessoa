
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Person.Infra.Seeder {
    public static class DataSeed {
        public static async Task InitializeAsync(IServiceProvider service) {

            using (var context = new AppDbContext(service.GetRequiredService<DbContextOptions<AppDbContext>>())) {

                //if (!context.Usuarios.Any()) {

                //    var pass = "123456";

                //    var u9999 = new Usuario("root", "xCF2zdWcmfQBDZpF", "root@autotech.com"); u9999.SetType(UserType.Root);
                //    var u0001 = new Usuario("operator", pass, "operator@autotech.com"); u0001.SetType(UserType.Operator);
                //    context.Users.AddRange(u9999, u0001);

                //    var p9999 = new Pessoa("Ninja das Sombras", "GM master virtual pica das galaxias", new DateTime(1994, 09, 22), EstagioAtual.Aproved, u9999.Id);
                //    var p0001 = new Pessoa("Operador AutoTech", "Operador de Sistemas", DateTime.Now, EstagioAtual.Aproved, u0001.Id);
                //    context.People.AddRange(p9999, p0001);

                //    var a9999 = new Endereco(TipoConstrucao.Commercial, 10, 20, "Av Digital Hacking", "Alamo", "Skynet", "Shadow", "Cloud", "00000000", p9999.Id);
                //    var a0001 = new Endereco(TipoConstrucao.Commercial, 10, 20, "Av Digital Hacking", "Alamo", "Skynet", "Shadow", "Cloud", "00000000", p0001.Id);
                //    context.Addresses.AddRange(a9999, a0001);

                //    var d9999 = new Documento("000000", "https://www.google.com.br/", TipoDocumento.Undefined, p9999.Id);
                //    var d0001 = new Documento("000000", "https://www.google.com.br/", TipoDocumento.Undefined, p0001.Id);
                //    context.Documents.AddRange(d9999, d0001);
                //}

                await context.SaveChangesAsync();
            }
        }
    }
}
