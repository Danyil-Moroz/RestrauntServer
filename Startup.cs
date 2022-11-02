using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using RestrauntServer.Data;
using RestrauntServer.Services;
using Microsoft.AspNetCore.Authentication.Certificate;
using System.Security.Cryptography.X509Certificates;
using System;

namespace RestrauntServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<RestrauntDb>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("RestrauntDb")));
            services.AddAuthentication(
        CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate();
            services.AddCertificateForwarding(options =>
             {
                 options.CertificateHeader = "X-SSL-CERT";

                 options.HeaderConverter = headerValue =>
                 {
                     X509Certificate2? clientCertificate = null;

                     if (!string.IsNullOrWhiteSpace(headerValue))
                     {
                         clientCertificate = new X509Certificate2(StringToByteArray(headerValue));
                     }

                     return clientCertificate!;

                     static byte[] StringToByteArray(string hex)
                     {
                         var numberChars = hex.Length;
                         var bytes = new byte[numberChars / 2];

                         for (int i = 0; i < numberChars; i += 2)
                         {
                             bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
                         }

                         return bytes;
                     }
                 };
             });
                 services.AddScoped<MenuService>();
            services.AddScoped<ClientORderService>();
            services.AddScoped<CleintAccountService>();
            services.AddScoped<AdminService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MyTestService", Version = "v1", });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCertificateForwarding();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
