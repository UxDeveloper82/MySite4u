using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySite4u.Data;
using MySite4u.Interfaces;
using MySite4u.Services;
using MySite4u.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySite4u.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IPortRepository, PortRepository>();
            services.AddTransient<ITechRepository, TechRepository>();
            services.AddTransient<IFileManager, FileManager>();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
          .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(
               config.GetConnectionString("DefaultConnection")));
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });
            return services;
        }
    }
}
