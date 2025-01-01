using Microsoft.EntityFrameworkCore;
using VehicleInsurance.Models;

namespace VehicleInsurance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<myDbContext>(opt => opt.UseSqlServer
            (builder.Configuration.GetConnectionString("mycon")));
            builder.Services.AddSession();
            builder.Services.AddMvc();
            var app = builder.Build();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Index}"
                );

                

            app.UseSession();   
            app.UseStaticFiles();
            app.Run();
        }
    }
}
