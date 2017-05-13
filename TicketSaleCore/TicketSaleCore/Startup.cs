using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TicketSaleCore.Models;
using TicketSaleCore.AuthorizationPolit.Password;

namespace TicketSaleCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddTransient<IPasswordValidator<User>,
                CustomPasswordValidator>(serv => new CustomPasswordValidator(6));




            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();


            /* services.AddIdentity<User, IdentityRole>(opts => {
                     opts.Password.RequiredLength = 5;   // минимальная длина
                     opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                     opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
                     opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
                     opts.Password.RequireDigit = false; // требуются ли цифры
                 })
                 .AddEntityFrameworkStores<ApplicationContext>()
                 .AddDefaultTokenProviders();
                 */

            /*  services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();
            //.AddDefaultTokenProviders();*/
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            /*  app.UseCookieAuthentication(new CookieAuthenticationOptions
              {
                  AuthenticationScheme = "Cookies",
                  LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Register"),
                  AutomaticAuthenticate = true,
                  AutomaticChallenge = true
              });*/
            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            // инициализация базы данных
            DatabaseInitialize(app.ApplicationServices).Wait();
        }
        public async Task DatabaseInitialize(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = "admin@gmail.com";
            string password = "Admin1";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
