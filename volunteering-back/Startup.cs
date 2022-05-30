using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volunteer.BL.Configuration;
using Volunteer.Common.Crypto;
using Volunteer.Dal.Configuration;
using Volunteer.Extensions;

namespace Volunteer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDal(Configuration);
            services.ConfigureBL();

            services.AddJwtAuthentication(Configuration);
            services.AddSwaggerGen();
            services.AddAutoMapperWithProfiles();

            services.AddTransient<IPasswordHasher, DumbPasswordHasher>();

            services.AddControllers(options => options.AllowEmptyInputInBodyModelBinding = true)
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200", "https://")
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

            app.UseHttpsRedirection();

            app.UseCors("MyPolicy");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
