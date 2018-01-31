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
            CreateRoles(serviceProvider);
        }

        private void CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
           
            Task<IdentityResult> roleResult;
   
            Task<bool> hasStudentRole = roleManager.RoleExistsAsync("Estudante");
            hasStudentRole.Wait();

            if (!hasStudentRole.Result)
            {
                roleResult = roleManager.CreateAsync(new IdentityRole("Estudante"));
                roleResult.Wait();
            }

            Task<bool> hasCimobRole = roleManager.RoleExistsAsync("CIMOB");
            hasCimobRole.Wait();

            if (!hasCimobRole.Result)
            {
                roleResult = roleManager.CreateAsync(new IdentityRole("CIMOB"));
                roleResult.Wait();
            }

            Task<bool> hasAdminRole = roleManager.RoleExistsAsync("Admin");
            hasAdminRole.Wait();

            if (!hasAdminRole.Result)
            {
                roleResult = roleManager.CreateAsync(new IdentityRole("Admin"));
                roleResult.Wait();
            }

            string email = "cimobtestes@testes.com"; //email associado à conta de testes CIMOB
            Task<ApplicationUser> testUser = userManager.FindByEmailAsync(email);
            testUser.Wait();

            if (testUser.Result == null)
            {
                ApplicationUser cimobTeste = new ApplicationUser
                {
                    Email = email,
                    UserName = "testeCimob",
                    PasswordHashAux = PasswordHashExtensions.Encode("@Abc123"),
                    EmailConfirmed = true
                };

                Task<IdentityResult> newUser = userManager.CreateAsync(cimobTeste, "@Abc123");
                newUser.Wait();

                Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(cimobTeste, "CIMOB");
                newUserRole.Wait();
            }

            email = "nunopedro@admin.com"; 
            testUser = userManager.FindByEmailAsync(email);
            testUser.Wait();

            if (testUser.Result == null)
            {
                ApplicationUser adminNuno = new ApplicationUser
                {
                    Email = email,
                    Nome = "Nuno Miguel Gonçalves São Pedro",
                    UserName = "NunoAdmin",
                    Contato = "961861587",
                    DataNascimento = "06/07/1996",
                    PasswordHashAux = PasswordHashExtensions.Encode("@Abc123"),
                    EmailConfirmed = true
                };

                Task<IdentityResult> newUser = userManager.CreateAsync(adminNuno, "@Abc123");
                newUser.Wait();

                Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(adminNuno, "Admin");
                newUserRole.Wait();
            }
        }
    }
}
