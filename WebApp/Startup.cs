using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.DataProtection;
using WeVsVirus.DataAccess.Repositories;
using WeVsVirus.Models.Entities;
using WeVsVirus.Business.Utility;
using WeVsVirus.Business.Mappers;
using System.Reflection;
using WeVsVirus.DataAccess.DatabaseContext;
using WeVsVirus.DataAccess;
using WeVsVirus.Business.Services;
using WeVsVirus.Business.Services.EmailServices;

namespace WeVsVirus.WebApp
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddDatabaseContext(services);

            AddAuthenticationAndAuthorization(services);

            services.AddAutoMapper(typeof(WeVsVirusMapperProfile).GetTypeInfo().Assembly);

            AddRepositories(services);

            BindConfigurationVariables(services);

            AddBusinessServices(services);

            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeVsVirus API", Version = "v1" });
            });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    // TODO don't allow any origin
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                //options.ExcludedHosts.Add("example.com");
                //options.ExcludedHosts.Add("www.example.com");
            });
        }

        private void AddDatabaseContext(IServiceCollection services)
        {
            try
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Sql"),
                    b => { b.MigrationsAssembly("DataAccess"); b.UseNetTopologySuite(); })
                );

                services.AddDbContext<DataProtectionKeyContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("Sql"));
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    options.EnableSensitiveDataLogging();
                })
                    .AddDataProtection()
                    .SetApplicationName("DataAccess")
                    .PersistKeysToDbContext<DataProtectionKeyContext>();
            }
            catch
            {
                Console.Error.Write("Could not connect to Database. Did you forget the connection string?");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services, ApplicationDbContext databaseContext)
        {
            //migrate when startup project
            databaseContext.Database.Migrate();
            if (env.IsDevelopment() || env.IsEnvironment("Development.Local"))
            {
                app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();
                app.UseHttpStatusCodeExceptionMiddleware();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeVsVirus API V1");
                });

            }
            else
            {
                app.UseStatusCodePages();
                app.UseHttpStatusCodeExceptionMiddleware();
                app.UseHsts();
            }

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment() || env.IsEnvironment("Development.Local"))
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            InitializeSeedData(services);
        }

        private void AddAuthenticationAndAuthorization(IServiceCollection services)
        {
            var secretKey = Configuration["Jwt:SecretKey"];
            if (secretKey == null)
            {
                throw new InvalidOperationException("Secret key ist not configured.");
            }
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserStore<IdentityUserStore>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IJwtFactory, JwtFactory>();

            // Configure JwtIssuerOptions
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyNames.AnyUserPolicy, policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, AccessRoles.ApiUser, AccessRoles.WebClientUser));
                options.AddPolicy(PolicyNames.HealthOfficeUserPolicy, policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, AccessRoles.HealthOfficeUser));
                options.AddPolicy(PolicyNames.DriverUserPolicy, policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, AccessRoles.DriverUser));
                options.AddPolicy(PolicyNames.ApiUserPolicy, policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, AccessRoles.ApiUser));
            });

            // services.AddSingleton<ITelemetryInitializer>(new LoggingInitializer(Configuration["Logging:ApplicationInsights:RoleName"]));
            // services.AddApplicationInsightsTelemetry(options => options.EnableDebugLogger = false);
        }

        private static void InitializeSeedData(IServiceProvider services)
        {
            var roleManager = services.GetService<RoleManager<IdentityRole>>();
            DatabaseInitializer.InitializeSeedRoles(roleManager);
        }

        private void AddRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private void AddBusinessServices(IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IDriverAccountService, DriverAccountService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IWeVsVirusEmailService, WeVsVirusEmailService>();
            services.AddTransient<IAccountEmailService, AccountEmailService>();
        }

        private void BindConfigurationVariables(IServiceCollection services)
        {
            var emailTemplateIds = new EmailTemplateIdsConfiguration();
            Configuration.GetSection("EmailTemplateIds").Bind(emailTemplateIds);
            services.AddSingleton(emailTemplateIds);

            var frontendConfiguration = new FrontendConfiguration();
            Configuration.GetSection("Frontend").Bind(frontendConfiguration);
            services.AddSingleton(frontendConfiguration);
        }
    }
}
