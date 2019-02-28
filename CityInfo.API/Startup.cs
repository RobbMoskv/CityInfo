﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace CityInfo.API
{
    public class Startup
    {
        #region ASP.NET Core 1.0 Approach
        //// Initiate a configuration propertiy
        //public static IConfigurationRoot Configuration;

        //// Inject the hosting environment to the constructor
        //public Startup(IHostingEnvironment env)
        //{
        //    // Initialize a config builder
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(env.ContentRootPath) // Tell where to find the customized settings file
        //        .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
        //        .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);

        //    // Create the configuration by build it
        //    Configuration = builder.Build();
        //}
        #endregion
        #region ASP.NET Core 2.0 Approach
        // Initiate a configuration propertiy
        public static IConfiguration Configuration { get; private set; }

        // Inject the hosting environment to the constructor
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    // Add Response Formatter for the Api
                    new XmlDataContractSerializerOutputFormatter()
                    ));

            #region Possibilities how to Add services
            // services.AddTransient<Service>(); -> Create each time this service is requested (Lightweight/stateless services)
            // services.AddScoped<Service>(); -> This services are created once per request
            // services.AddSingleton<Service>(); -> This services are created the first time they are requested
            #endregion
            // Add the mailService as a lightweight service to have it available for injections.
#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Log to the console
            loggerFactory.AddConsole();
            // Log to the debug window
            loggerFactory.AddDebug();

            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            /// Display status codes
            app.UseStatusCodePages();

            /// Use the core mvc pattern 
            app.UseMvc();

        }
    }
}