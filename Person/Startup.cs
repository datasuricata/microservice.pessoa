using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Person.DI;
using Person.Startups;
using Person.Startups.Extensions;

namespace Person {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {

            // # DI
            Bootstrap.Configure(services, Configuration.GetConnectionString("DefaultConnection"));

            services.AddDistributedMemoryCache();
            services.AddCustomMvc();
            services.AddJWTService(Configuration);
            services.AddCustomSwagger();
            services.AddLocalizations();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseUnitOfWork();
            app.UserDevExceptionIfDebug(env);

            // todo (create filter exception middleware?)
            //app.UseFilterException();

            app.UserCustomCors();

            // todo (enable redirection on application or infra layer?)
            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseSwaggerDocs();
            app.UseCookiePolicy();
            app.UseMvc();
        }
    }
}
