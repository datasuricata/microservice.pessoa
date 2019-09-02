using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Person.Startups {
    public static class Configs {

        #region - services -

        public static void AddCustomMvc(this IServiceCollection services) {

            services.Configure<CookiePolicyOptions>(opt => {
                opt.CheckConsentNeeded = context => true;
                opt.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc(config => {
                // config.Filters.Add(typeof(CrossCutting.ApiFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
              .AddJsonOptions(options => {
                  options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                  options.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ssZ";
                  options.SerializerSettings.Formatting = Formatting.Indented;
                  options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                  options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
              });
        }

        public static void AddCustomSwagger(this IServiceCollection services) {
            services.AddSwaggerGen(config => {
                config.SwaggerDoc("v1", new Info {
                    Title = "Microservice Person",
                    Version = "v1",
                    Contact = new Contact {
                        Name = "Lucas Rocha de Moraes",
                        Email = "lucas.moraes.dev@gmail.com",
                    }
                });

                config.AddSecurityDefinition("Bearer", new ApiKeyScheme {
                    In = "header",
                    Description = "Please enter into field with: Bearer [Token]",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                config.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    {"Bearer", Enumerable.Empty<string>()},
                });
            });
        }

        public static void AddJWTService(this IServiceCollection services, IConfiguration configuration) {
            var key = Encoding.ASCII.GetBytes(configuration["SecurityKey"]);
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x => {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
        }

        public static void AddLocalizations(this IServiceCollection services) {
            services.Configure<RequestLocalizationOptions>(
               options => {
                   var supportedCultures = new List<CultureInfo> {
                        new CultureInfo("pt-BR"),
                        new CultureInfo("en-US"),
                   };

                   options.DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR");
                   options.SupportedCultures = supportedCultures;
                   options.SupportedUICultures = supportedCultures;

                   options.RequestCultureProviders.Insert(0, new AcceptLanguageHeaderRequestCultureProvider());
               });
        }

        #endregion

        #region - application -

        public static void UserCustomCors(this IApplicationBuilder app) {
            app.UseCors(options => {
                options.AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowAnyOrigin()
                       .AllowCredentials();
            });
        }

        public static void UseSwaggerDocs(this IApplicationBuilder app) {
            app.UseSwagger();
            app.UseSwaggerUI(config => {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Person V1");
            });
        }

        public static void UserDevExceptionIfDebug(this IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();
        }

        #endregion

    }
}
