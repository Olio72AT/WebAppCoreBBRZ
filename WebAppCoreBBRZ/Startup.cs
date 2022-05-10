using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAppCoreBBRZ
{
    public class Startup : ControllerBase
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            // MyOwn();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            Services.PersistenceService.Init();

            // HACK OLIO 1 - Added on: Sessions (Configuration)  
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(1000);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false; // Default is true, make it false
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // HACK OLIO 1 - Added on: Sessions (Configure)
            // BEFORE USE ENDPOINTS 
            app.UseSession();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            MyOwn();

            resetLoggedUser();

        }

        public void resetLoggedUser()
        {
            
            //HttpContext.Session.SetString("SessionId", "");
            //HttpContext.Session.SetString("UserId", "");

        }




        public void MyOwn()
        {
            // Initialize new List data 

            WebAppCoreBBRZ.Controllers.TodoController.todoListe.Add(new Models.Todo()
            {
                Id = WebAppCoreBBRZ.Controllers.TodoController.todoListe.Count + 1,
                Aufgabe = "Schokolade essen",
                Beschreibung = "Osterhase vom Küchentisch",
                Done = false
            });

            WebAppCoreBBRZ.Controllers.TodoController.todoListe.Add(new Models.Todo()
            {
                Id = WebAppCoreBBRZ.Controllers.TodoController.todoListe.Count + 1,
                Aufgabe = "Apfelsaft trinken",
                Beschreibung = "langsam",
                Done = false
            }); 
            
            WebAppCoreBBRZ.Controllers.TodoController.todoListe.Add(new Models.Todo()
            {
                Id = WebAppCoreBBRZ.Controllers.TodoController.todoListe.Count + 1,
                Aufgabe = "Aufgaben zusammenfassen",
                Beschreibung = "Schulaufgaben, dann die Hausübung",
                Done = false
            }); 
            
            WebAppCoreBBRZ.Controllers.TodoController.todoListe.Add(new Models.Todo()
            {
                Id = WebAppCoreBBRZ.Controllers.TodoController.todoListe.Count + 1,
                Aufgabe = "Roman lesen",
                Beschreibung = "freiwillig",
                Done = true
            });

        }
    }
}
