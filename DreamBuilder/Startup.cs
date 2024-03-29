﻿using AutoMapper;
using CloudinaryDotNet;
using DreamBuilder.Data;
using DreamBuilder.Models;
using DreamBuilder.Models.Products.InputModels;
using DreamBuilder.Models.Products.ViewModels;
using DreamBuilder.Services;
using DreamBuilder.Services.Contracts;
using DreamBuilder.Services.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace DreamBuilder
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<DreamBuilderDbContext>(options =>
                options.UseSqlServer(
                    this.Configuration.GetConnectionString("DefaultConnection")));

            //TODO: AddDefaultUI.Bootsrap4 & Stores maybe?
            //TODO: AddDefaultIdentity || MyIdentity?
            // TODO: services.AddScoped<SignInManager<DreamBuilderUser>>();
            services.AddIdentity<DreamBuilderUser, IdentityRole>(
                options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;

                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<DreamBuilderDbContext>()
                .AddDefaultTokenProviders();

            Account cloudinaryCredentials = new Account(
               this.Configuration["Cloudinary:CloudName"],
               this.Configuration["Cloudinary:ApiKey"],
               this.Configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.AddSingleton(cloudinaryUtility);

            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IInvoicesService, InvoicesService>();
            services.AddScoped<IInquiriesService, InquiriesService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();

            //Registers the service Automapper
            //services.AddAutoMapper();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Configures AutoMapper
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            AutoMapperConfig.RegisterMappings(
               typeof(ProductsCreateInputModel).GetTypeInfo().Assembly,
               typeof(ProductsAllViewModel).GetTypeInfo().Assembly,
               typeof(ProductsDetailsViewModel).GetTypeInfo().Assembly,
               typeof(Product).GetTypeInfo().Assembly);

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DreamBuilderDbContext>())
                {
                    context.Database.EnsureCreated();

                    if (!context.Roles.Any())
                    {
                        context.Roles.Add(new IdentityRole
                        {
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });

                        context.Roles.Add(new IdentityRole
                        {
                            Name = "User",
                            NormalizedName = "USER"
                        });
                    }
                    context.SaveChanges();

                    if (!context.OrderStatuses.Any())
                    {
                        context.OrderStatuses.Add(new OrderStatus
                        {
                            Name = "Active"
                        });

                        context.OrderStatuses.Add(new OrderStatus
                        {
                            Name = "Completed"
                        });

                        context.SaveChanges();
                    }

                    if (!context.InquiryStatuses.Any())
                    {
                        context.InquiryStatuses.Add(new InquiryStatus
                        {
                            Name = "Pending"
                        });

                        context.InquiryStatuses.Add(new InquiryStatus
                        {
                            Name = "Replied"
                        });

                        context.SaveChanges();
                    }
                }
            }
            //TODO: We do not controll development env at this stage => remove it
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}