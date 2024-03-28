using Jodami.DAL.DBContext;
using Jodami.DAL.Implementacion;
using Jodami.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jodami.IOC
{
    public static class Dependencia
    {
        //public static void InyectarDependencia(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddDbContext<DbJodamiContext>(Options =>
        //    {
        //        Options.UseSqlServer(configuration.GetConnectionString("CnSqlServer"));
        //    });

        //    services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        //    //services.AddScoped<IVentaRepository, VentaRepository>();

        //}

        public static IServiceCollection InyectarDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbJodamiContext>(Options =>
            {
                Options.UseSqlServer(configuration.GetConnectionString("CnSqlServer"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //services.AddScoped<IVentaRepository, VentaRepository>();

            return services;
        }

    }
}