using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Volunteer.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Main
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Method ini digunakan untuk menambahkan layanan ke dalam container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "News API", Version = "v1" });

                //Define the BearerAuth scheme that's in use
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "https://mosframtech.com",
                        ValidAudience = "https://mosframtech.com",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("UWETU23507953NOVALSMITH8793485!!##2739458"))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            // Contoh: mendapatkan ID pengguna dari token
                            var userId = context.Principal?.Identity?.Name;

                            // Contoh: mendapatkan peran pengguna dari database
                            var userRoles = new[] { "User" };

                            // Tambahkan klaim peran ke identitas pengguna
                            var claimsIdentity = context.Principal?.Identity as ClaimsIdentity;
                            claimsIdentity?.AddClaims(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                            await Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MultipleRolesPolicy", policy =>
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c => c.Type == ClaimTypes.Role && (c.Value == "Admin" || c.Value == "User"))));

                options.AddPolicy("Admin", policy =>
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c => c.Type == ClaimTypes.Role && (c.Value == "Admin"))));
            });




            // Dependency Injection
            services.AddVolunteerRegisterServices(Configuration);
        }

        // Method ini digunakan untuk mengkonfigurasi HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "News API V1");
                });
            }

            app.UseHttpsRedirection();
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
