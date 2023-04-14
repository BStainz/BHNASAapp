using BHNASAapp.Models;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;


namespace BHNASAapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Load appsettings.json configuration
            builder.Configuration.AddJsonFile("appsettings.json", optional: false);
            builder.Services.AddControllersWithViews();
            _ = builder.Services.AddScoped(provider =>
            {
                // Retrieve appsettings.json parameters
                string baseAddress = builder.Configuration["BaseAddress"];
                string basePath = builder.Configuration["BasePath"];
                string apiKey = builder.Configuration["apiKey"];
                string mediaType = builder.Configuration["mediaType"];
                // Create a new instance of YKClient with the retrieved parameters
                return new BHClient(baseAddress, basePath, mediaType, apiKey);
            });
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for productionscenarios, see https://aka.ms/aspnetcore-hsts.
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
//Brendan Hannon CPS-3330-01 Spring2023 HW7