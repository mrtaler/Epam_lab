using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
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
            //use HTTPS
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });
            //use localization
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru"),
                    new CultureInfo("ja"),
                    new CultureInfo("be")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
                {
                    // My custom request culture logic
                    return new ProviderCultureResult("en");
                }));
            });



            //Add Pasword validator
            services.AddTransient<IPasswordValidator<User>,
                CustomPasswordValidator>(serv => new CustomPasswordValidator(5));

            //Add localizaion based on Resx files
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            //use EF in memory
            services.AddDbContext<ApplicationContext>(opt => opt.UseInMemoryDatabase());
            //Use existing DB
            /*
             services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
         */
            //Add Identity to services
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();


            services.AddMvc()
                // Add support for finding localized views, based on file name suffix, e.g. Index.fr.cshtml
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
                // Add support for localizing strings in data annotations (e.g. validation messages)
                .AddDataAnnotationsLocalization();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Logger settings
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //Available localization
            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("ru"),
                new CultureInfo("ja"),
                new CultureInfo("be")
            };
            //Add Localization (default is en-US)
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

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
            //use Identity
            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            #region DbInit
            //User&role Init 
            DatabaseInitialize(app.ApplicationServices).Wait();
            //Db content init
            AddTestData(app.ApplicationServices.GetService<ApplicationContext>());
            #endregion
        }

        private static void AddTestData(ApplicationContext context)
        {
            #region City Table Init
            context.CityDbSet.Add(new City { Name = "Minsk" });
            context.CityDbSet.Add(new City { Name = "Gomel" });
            context.CityDbSet.Add(new City { Name = "Grodno" });
            context.CityDbSet.Add(new City { Name = "Vitebsk" });
            context.CityDbSet.Add(new City { Name = "Brest" });
            context.CityDbSet.Add(new City { Name = "Mogilev" });
            #endregion

            #region TiketStatus Table Init
            context.StatusDbSet.Add(new Status { StatusName = "Waiting for conformation" });
            context.StatusDbSet.Add(new Status { StatusName = "Confirmed" });
            context.StatusDbSet.Add(new Status { StatusName = "Rejected" });
            #endregion

            context.EventDbSet.Add(new Event
            {
                Name = "Event1",
                Date = DateTime.Now,
                Banner = "/images/EventImg/0cb43207294335ec2b6274a39a54aa72.jpg",
                Description = 1,
                Venue = new Venue
                {
                    Name = "Venue1",
                    Address = "Address1",
                    City = context.CityDbSet.Local.First(p => p.Name.Equals("Minsk")),
                }
            });
            context.EventDbSet.Add(new Event
            {
                Name = "Event2",
                Date = DateTime.Now,
                Banner = "/images/EventImg/1cbdc2f1aecb4b433cf6c1bb70d80fac.jpg",
                Description = 1,
                Venue = new Venue
                {
                    Name = "Venue2",
                    Address = "Address2",
                    City = context.CityDbSet.Local.First(p => p.Name.Equals("Gomel")),
                }
            });
            context.EventDbSet.Add(new Event
            {
                Name = "Event3",
                Date = DateTime.Now,
                Banner = "/images/EventImg/3086cc801a9e6f0ea6a49d5a53db325e.jpg",
                Description = 1,
                Venue = new Venue
                {
                    Name = "Venue3",
                    Address = "Address3",
                    City = context.CityDbSet.Local.First(p => p.Name.Equals("Grodno")),
                }
            });

            context.EventDbSet.Add(new Event
            {
                Name = "Event21",
                Date = DateTime.Now,
                Banner = null,
                Description = 21,
                Venue = new Venue
                {
                    Name = "Venue21",
                    Address = "Address21",
                    City = context.CityDbSet.Local.First(p => p.Name.Equals("Minsk")),
                }
            });
            context.EventDbSet.Add(new Event
            {
                Name = "Event22",
                Date = DateTime.Now,
                Banner = null,
                Description = 21,
                Venue = new Venue
                {
                    Name = "Venue22",
                    Address = "Address22",
                    City = context.CityDbSet.Local.First(p => p.Name.Equals("Gomel")),
                }
            });
            context.EventDbSet.Add(new Event
            {
                Name = "Event23",
                Date = DateTime.Now,
                Banner = null,
                Description = 21,
                Venue = new Venue
                {
                    Name = "Venue23",
                    Address = "Address23",
                    City = context.CityDbSet.Local.First(p => p.Name.Equals("Grodno")),
                }
            });


         
            context.TicketDbSet.Add(
                new Ticket
                {
                    Event = context.EventDbSet.Local.First(p => p.Name.Equals("Event1")),
                    Price = 130M,
                    SellerNotes = "orderded",
                    Seller = context.Users.Local.First(p => p.Email.Equals("User1"))
                });

            var ti = new Ticket
            {
                Event = context.EventDbSet.Local.First(p => p.Name.Equals("Event2")),
                Price = 130M,
                SellerNotes = "asdasdasdasd2",
                Seller = context.Users.Local.First(p => p.Email.Equals("User1"))
            };

            context.TicketDbSet.Add(ti);

            context.TicketDbSet.Add(
                new Ticket
                {
                    Event = context.EventDbSet.Local.First(p => p.Name.Equals("Event1")),
                    Price = 130M,
                    SellerNotes = "asdasdasdasd3",
                    Seller = context.Users.Local.First(p => p.Email.Equals("User3"))
                });

            context.TicketDbSet.Add(
                new Ticket
                {
                    Event = context.EventDbSet.Local.First(p => p.Name.Equals("Event2")),
                    Price = 130M,
                    SellerNotes = "asdasdasdasd1",
                    Seller = context.Users.Local.First(p => p.Email.Equals("User1"))
                });
            context.TicketDbSet.Add(
                new Ticket
                {
                    Event = context.EventDbSet.Local.First(p => p.Name.Equals("Event3")),
                    Price = 130M,
                    SellerNotes = "asdasdasdasd2",
                    Seller = context.Users.Local.First(p => p.Email.Equals("User1"))
                });
            context.TicketDbSet.Add(
                new Ticket
                {
                    Event = context.EventDbSet.Local.First(p => p.Name.Equals("Event3")),
                    Price = 130M,
                    SellerNotes = "asdasdasdasd3",
                    Seller = context.Users.Local.First(p => p.Email.Equals("User3"))
                });

            context.OrderDbSet.Add(
                new Order
                {
                    Buyer = context.Users.Local.First(p => p.Email.Equals("User3")),
                    Status = context.StatusDbSet.Local.First(p=>p.StatusName.Equals("Waiting for conformation")),
                    Ticket = context.TicketDbSet.Local.First(p=>p.Seller.Email.Equals("User1")),
                    TrackNo = "BY3123123123123C"
                });

            context.OrderDbSet.Add(
                new Order
                {
                    Buyer = context.Users.Local.First(p => p.Email.Equals("User2")),
                    Status = context.StatusDbSet.Local.First(p => p.StatusName.Equals("Confirmed")),
                    Ticket = ti,
                    TrackNo = "BY3123123123123C"
                });

            context.SaveChanges();
        }

        public async Task DatabaseInitialize(IServiceProvider serviceProvider)
        {


            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = "Admin";
            string password = "Admin";
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
                User admin = new User { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
                List<User> users = new List<User>();
                users.Add(new User
                {
                    Email = "User1",
                    UserName = "User1",
                    EmailConfirmed = true,
                    FirstName = "Firstname1",
                    LastName = "LastName1",
                    Localization = "ru-RU",
                    Address = "adress1",
                    PhoneNumber = "5-53-53-56"
                });

                users.Add(new User
                {
                    Email = "User2",
                    UserName = "User2",
                    EmailConfirmed = true,
                    FirstName = "Firstname2",
                    LastName = "LastName2",
                    Localization = "ru-RU",
                    Address = "adress2",
                    PhoneNumber = "5-53-53-56"
                });
                users.Add(new User
                {
                    Email = "User3",
                    UserName = "User3",
                    EmailConfirmed = true,
                    FirstName = "Firstname3",
                    LastName = "LastName3",
                    Localization = "ru-RU",
                    Address = "adress3",
                    PhoneNumber = "5-53-53-56"
                });

                foreach (var item in users)
                {
                    IdentityResult result1 = await userManager.CreateAsync(item, item.Email);
                    if (result1.Succeeded)
                    {
                        await userManager.AddToRoleAsync(item, "user");
                    }
                }
            }
        }
    }
}