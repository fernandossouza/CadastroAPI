﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CadastroAPI.Service;
using CadastroAPI.Service.Interface;
using CadastroAPI.Models;
using CadastroAPI.Models.Repository;

namespace CadastroAPI
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
            services.AddCors (o => o.AddPolicy ("CorsPolicy", builder => {
                builder.AllowAnyOrigin ()
                    .AllowAnyMethod ()
                    .AllowAnyHeader ();
            }));
            services.AddMvc();
            services.AddTransient<IRotasTrechoService ,RotasTrechoService>();
            services.AddTransient<TbRotasTrechoRepository,TbRotasTrechoRepository>();
            services.AddTransient<TbRotasCadastroRepository,TbRotasCadastroRepository>();
            services.AddTransient<TbRotasTrechoXRotasRepository,TbRotasTrechoXRotasRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
