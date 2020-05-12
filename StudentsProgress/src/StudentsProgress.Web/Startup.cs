using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StudentsProgress.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentsProgress.Web.Constants;
using StudentsProgress.Web.Data.Identity;
using StudentsProgress.Web.Data.Repository;
using StudentsProgress.Web.Hubs;
using StudentsProgress.Web.Infrastructure;
using StudentsProgress.Web.Logics;

namespace StudentsProgress.Web
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
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                    options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
            services.AddScoped<IUserRatingsLogic, UserRatingsLogic>();
            services.AddScoped<IAttendancesLogic, AttendancesLogic>();
            services.AddScoped<IStudentsLogic, StudentsLogic>();
            services.AddScoped<IGroupsLogic, GroupsLogic>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPersonalCabinetLogic, PersonalCabinetLogic>();

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<RatingNotificationHub>("/ratingNotificationHub");
            });

            CreateRoles(services).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // here in this line we are adding Admin Role
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                //here in this line we are creating admin role and seed it to the database
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            // here in this line we are adding Student Role
            if (!await roleManager.RoleExistsAsync(Roles.Student))
            {
                //here in this line we are creating Student role and seed it to the database
                await roleManager.CreateAsync(new IdentityRole(Roles.Student));
            }

            //here we are assigning the Admin role to the User that we have registered above 
            //Now, we are assigning admin role to this user("Ali@gmail.com"). When will we run this project then it will
            //be assigned to that user.

            var admin = new ApplicationUser
            {
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
            };

            const string password = "Test1234!";

            var result = await userManager.CreateAsync(admin, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, Roles.Admin);
            }
        }
    }
}
