using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Interface;
using Volunteer.Repository;
using Volunteer.Service;

namespace Volunteer.Utility
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddVolunteerRegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Mendapatkan connection string dari IConfiguration
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Menambahkan DbContext dengan konfigurasi koneksi
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Interface
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();
            services.AddScoped<IVolunteerService, VolunteerService>();
  
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

    }
}
