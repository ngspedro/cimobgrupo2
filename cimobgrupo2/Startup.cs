using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using cimobgrupo2.Data;
using cimobgrupo2.Models;
using cimobgrupo2.Services;
using Microsoft.Extensions.FileProviders;
using System.IO;
using cimobgrupo2.Extensions;

namespace cimobgrupo2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)

        {
            //services.AddDbContext<ApplicationDbContext>(options =>
              //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("Azure")));

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files")));

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
            });


            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            services.Configure<AuthMessageSenderOptions>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitializer.Initialize(context);
            CreateRole(serviceProvider, "Estudante");
            CreateRole(serviceProvider, "CIMOB");
            CreateRole(serviceProvider, "Admin");
            CreateUser(serviceProvider, "nunoadmin@gmail.com", "nuno pedro", "nunoadmin", "@Abc123", "961222222", "07/06/1996", "Admin");
            CreateUser(serviceProvider, "teste@cimob.com", "teste cimob", "testecimob", "@Abc123", "961234567", "01/01/1900", "CIMOB");
        }
        private void CreateRole(IServiceProvider serviceProvider, string role)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            Task<IdentityResult> roleResult;

            Task<bool> hasRole = roleManager.RoleExistsAsync(role);
            hasRole.Wait();

            if (!hasRole.Result)
            {
                roleResult = roleManager.CreateAsync(new IdentityRole(role));
                roleResult.Wait();
            }
        }

        private void CreateUser(IServiceProvider serviceProvider, string email, string nome, 
            string username, string password, string contato, string dataNascimento, string role)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            Task<ApplicationUser> testUser = userManager.FindByEmailAsync(email);
            testUser.Wait();

            if (testUser.Result == null)
            {
                ApplicationUser testuser = new ApplicationUser
                {
                    Email = email,
                    Nome = nome,
                    UserName = username,
                    Contato = contato,
                    DataNascimento = dataNascimento,
                    PasswordHashAux = PasswordHashExtensions.Encode(password),
                    EmailConfirmed = true
                };

                Task<IdentityResult> newUser = userManager.CreateAsync(testuser, password);
                newUser.Wait();

                Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(testuser, role);
                newUserRole.Wait();
            }
        }
    }
}
