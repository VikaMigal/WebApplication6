﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApplication1.Contracts;
using WebApplication1.Repository;
using DbContext = WebApplication1.Repository.DbContext;
using StructureMap;

namespace WebApplication1
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
//            services.AddDbContext<RepositoryContext>(opt =>
//                opt.UseInMemoryDatabase("Accountowner"));
//            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
//            services.AddDbContext<RepositoryBase>();
//            services.AddTransient<IRepositoryBase, RepositoryBase>();
//            services.AddDbContext<RepositoryContext>(opt =>
//                opt.UseInMemoryDatabase("Accountowner"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<DbContext>(); 
            services.AddSingleton<IRepositoryBase, RepositoryBase>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           
            app.UseMvc();
        }
    }
}