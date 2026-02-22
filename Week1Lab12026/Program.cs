using Microsoft.AspNetCore.Builder;
using Tracker.WebAPIClient;
using Week1Lab12026.Models;
using Microsoft.EntityFrameworkCore;

namespace Week1Lab12026
{
    public class Program
    {
        public static void Main(string[] args)
        {

            ActivityAPIClient.Track(StudentID : "S00236888", StudentName : "Ryan McClelland",
                activityName: "RAD302 2026 Week 1 Lab 1", 
                Task:"Database Initializer setup succesfully");
            
            var builder = WebApplication.CreateBuilder(args);

            // Here we retieve the connection string from the appsettings.json file
            // and create the UserContext with the connection string
            var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<UserContext>(options =>
            //New target assembly directive for migrations
            options.UseSqlServer(dbConnectionString));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            //Retrieve the User context from the services container
            using (var scope = app.Services.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetRequiredService<UserContext>();
                //Retrieve the IWebHostEnvironment for the content root even though we are nopt using the file system here
                var hostEnvironment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                //Create a new instance of the DbSeeder class and call the seed method
                DbSeeder dbSeeder = new DbSeeder(_ctx, hostEnvironment);
                dbSeeder.Seed(); // seed method is in the dbSeeder class
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
