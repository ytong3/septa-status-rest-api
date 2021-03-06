﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SEPTAInquierierForAlexa;
using Microsoft.AspNetCore.Http;
using SEPTAInquirer.Configurations;

namespace SEPTAInquirer
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
            services.AddMvc();

            services.Configure<SeptaApiConfig>(Configuration.GetSection("SeptaAPIConfig"));
            services.Configure<AlexaSkillConfig>(options => Configuration.GetSection("AlexaSkillConfig").Bind(options));
            services.AddSingleton<ISeptapiClient, SEPTAAPIClient>();
            services.AddSingleton<ISeptaSpeechGenerator, SpetaSpeechGenerator>();
            services.AddSingleton<IAlexaSpeakStrategy, BoringAlexaSpeakStrategy>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseAlexaRequestValidation();
            app.UseMvc();

            app.Run(async (context)=>{
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Don't look at me. POST to api/alexa.");
            });
        }
    }
}
