using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Prometheus;
using Prometheus.DotNetRuntime;
using SendGrid;
using System;
using System.Threading.Tasks;
using Vol;
using Vol.Common;
using Vol.EntityFrameworkCore;
using Vol.Filters;
using Vol.Infrastructure;
using Vol.Services;
using Vol.Users;
using Vol.V1.Account;
using Vol.V1.Common;
using Vol.V1.Emails;
using Vol.V1.Events;
using Vol.V1.Users;

namespace volunteering_back
{
    public class Startup
    {
        public static IDisposable Collector;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Collector = DotNetRuntimeStatsBuilder.Default().StartCollecting();

            services.AddScoped<UnitOfWorkFilter>();
            services.AddControllers(options =>
            {
                options.Filters.Add(new ExceptionFilter());
                options.Filters.AddService<UnitOfWorkFilter>(1);
            }).AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.Converters.Add(new StringEnumConverter
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                });
            });

            ConfigureCors(services, Configuration);
            ConfigureSwaggerServices(services, Configuration);

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
            });

            services.AddHttpClient("Metrics")
                   .UseHttpClientMetrics();
            services.AddHealthChecks().ForwardToPrometheus();

            /*services.AddSingleton<IFacebookProvider, FacebookProvider>();
            services.AddSingleton<IGoogleProvider, GoogleProvider>();*/
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<ITemplateRenderer, TemplateRenderer>();
            services.AddSingleton<IEmailSender, SendGridEmailSender>();
            services.AddSingleton<ICallbackUrlService, CallbackUrlService>();
            services.AddSingleton<IPasswordsGenService, PasswordsGenService>();

            services.AddSingleton<SendGridClient>(new SendGridClient(Configuration["SendGrid:ApiKey"]));
            services.AddSingleton(Configuration.GetSection("Centrifugo").Get<CentrifugoSettings>());
            services.AddScoped<IEventEmitter, EventEmitter>();

            services.AddScoped<IAccountAppService, AccountAppService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ILanguageSwitcherService, LanguageSwitcherService>();


            services.AddDbContext<VolDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("Default")));

            ConfigureAuthentication(services, Configuration)
            .AddEntityFrameworkStores<VolDbContext>()
            .AddDefaultTokenProviders();

            services.AddAutoMapper(typeof(VolApplicationAutoMapperProfile));
            services.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 20;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            /*services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(Configuration.GetConnectionString("HangfireConnection")));*/

            //services.AddHangfireServer();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();
            app.Use((context, next) =>
            {
                context.Request.Scheme = "https";
                context.Request.Host = new HostString("www.volkaz.com");
                return next();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vol v1"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (!env.IsDevelopment())
            {
                //app.UseErrorPage();
            }

            //app.UseHangfireDashboard();

            app.UseApiVersioning();

            app.UseRouting();
            app.UseHttpMetrics();

            app.UseCors("Default");
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Lax,
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapMetrics();
                endpoints.MapControllers();
            });

        }

        private IdentityBuilder ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
            })
            .AddCookie(IdentityConstants.ApplicationScheme, options =>
            {
                options.Events.OnRedirectToAccessDenied = c =>
                {
                    c.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                };

                options.Events.OnRedirectToLogin = c =>
                {
                    c.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            })
            .AddCookie(IdentityConstants.ExternalScheme);

            services.AddHttpContextAccessor();
            // Identity services
            services.TryAddScoped<IUserValidator<User>, UserValidator<User>>();
            services.TryAddScoped<IPasswordValidator<User>, PasswordValidator<User>>();
            services.TryAddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.TryAddScoped<IRoleValidator<IdentityRole<Guid>>, RoleValidator<IdentityRole<Guid>>>();
            // No interface for the error describer so we can add errors without rev'ing the interface
            services.TryAddScoped<IdentityErrorDescriber>();
            services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<User>>();
            services.TryAddScoped<ITwoFactorSecurityStampValidator, TwoFactorSecurityStampValidator<User>>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory<User, IdentityRole<Guid>>>();
            services.TryAddScoped<IUserConfirmation<User>, DefaultUserConfirmation<User>>();
            services.TryAddScoped<UserManager<User>>();
            services.TryAddScoped<SignInManager<User>>();
            services.TryAddScoped<RoleManager<IdentityRole<Guid>>>();

            return new IdentityBuilder(typeof(User), typeof(IdentityRole<Guid>), services);
        }

        private static void ConfigureSwaggerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Vol API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        private void ConfigureCors(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Default", builder =>
                {
                    builder
                        .WithOrigins("http://localhost:3000",
                            "https://localhost:3000",
                            "http://localhost",
                            "http://94.237.100.88",
                            "http://94.237.100.88:8081",
                            "https://94.237.100.88:8081")
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

    }
}
