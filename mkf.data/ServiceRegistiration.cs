using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using mkf.data.Abstract;
using mkf.data.Concrete;
using mkf.data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mkfs = mkf.data.Context;

namespace mkf.data
{
    public static class ServiceRegistiration
    {
        public static void AddDataRepositories(this IServiceCollection services)
        {
     
            MySqlServerVersion serviceLifetime = new (new Version(8, 0, 29));
            services.AddDbContext<mkfs.AppDbContext>
                (options => options.UseMySql("server=localhost;port=3306;user=root;password=gokhan949;database=mkfdb;"
                , serviceLifetime));

            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }

    public class DesginTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            DbContextOptionsBuilder<AppDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=gokhan949;database=mkfdb;", serverVersion);
            return new AppDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
