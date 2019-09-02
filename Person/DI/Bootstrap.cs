using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Person.Core.Notifications.Events;
using Person.Infra;
using Person.Infra.Persistence;
using Person.Infra.Transactions;
using Person.Services.Base;
using Person.Services.Core;
using Person.Services.Core.Interfaces;

namespace Person.DI {
    public static class Bootstrap {
        public static void Configure(IServiceCollection services, string conn) {
            #region - context -

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conn));

            #endregion

            #region - kernel -

            services.AddScoped(typeof(IServiceBase), typeof(ServiceBase));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IEventNotifier), typeof(EventNotifier));

            #endregion

            #region - core -

            services.AddScoped(typeof(IServiceUsuario), typeof(ServiceUsuario));
            services.AddScoped(typeof(IServicePessoa), typeof(ServicePessoa));

            #endregion
        }
    }
}
