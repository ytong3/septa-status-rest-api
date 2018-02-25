using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Alexa.NET.Security.Middleware;
using SEPTAInquierierForAlexa;
using Microsoft.AspNetCore.Http;

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
            services.AddSingleton<ISeptapiClient, SEPTAAPIClient>();
            services.AddScoped<ISeptaSpeechGenerator, SpetaSpeechGenerator>();
            services.AddTransient<IAlexaSpeakStrategy, BoringAlexaSpeakStrategy>();

            //TODO: need an AutoMapper to map a SEPTANextToArriveAPIResult to TrainInfo?
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
                await context.Response.WriteAsync("Don't look at me. POST to api/alex.");
            });
        }
    }
}
