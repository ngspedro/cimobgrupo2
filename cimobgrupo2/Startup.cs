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
            string email = "cimobtestes@testes.com"; //email associado à conta de testes CIMOB
            Task<IdentityResult> roleResult;
            

            //Check that there is an Administrator role and create if not
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

            Task<ApplicationUser> testUser = userManager.FindByEmailAsync(email);
            testUser.Wait();

            if (testUser.Result == null)
            {
                ApplicationUser cimobTeste = new ApplicationUser
                {
                    Email = email,
                    UserName = "testeCimob"
                };

                Task<IdentityResult> newUser = userManager.CreateAsync(cimobTeste, "@Abc123");
                newUser.Wait();

                Task<String> code = userManager.GenerateEmailConfirmationTokenAsync(cimobTeste);
                code.Wait();
                Task<IdentityResult> resultActivation = userManager.ConfirmEmailAsync(cimobTeste, code.Result);
                resultActivation.Wait();

                if (newUser.Result.Succeeded && resultActivation.Result.Succeeded)
                {
                    Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(cimobTeste, "CIMOB");
                    newUserRole.Wait();
                }
            }
        }
    }
}
