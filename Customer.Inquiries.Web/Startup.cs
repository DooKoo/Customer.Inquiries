using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using Customer.Inquiries.DataAccess.Base;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using MediatR;
using Customer.Inquiries.Core.Queries;
using Customer.Inquiries.DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Customer.Inquiries.Web.Security;
using Newtonsoft.Json;
using Customer.Inquiries.Core;

namespace Customer.Inquiries.Web
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
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(options =>
            {
                //options.DefaultChallengeScheme = ApiAuthenticationHandler.AUTH_SCHEME_NAME;
            })
                .AddCookie(cfg => cfg.SlidingExpiration = true)
                .AddApiKeyAuthenticationMiddleware(o => this.Configuration.GetSection("ApiKeyAuthorization").Bind(o));


            //TODO: Clean up this mass
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiPolicy", policyBuilder =>
                {
                    policyBuilder.RequireClaim("role");
                    policyBuilder.RequireAuthenticatedUser();

                    //Multiple authentication schemes can be added and the output from them will then be merged into a single identity.
                    policyBuilder.AddAuthenticationSchemes(ApiKeyAuthorizationHandler.AUTH_SCHEME_NAME);
                });

                options.AddPolicy("DefaultPolicy", policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                });

                options.DefaultPolicy = options.GetPolicy("DefaultPolicy");
            });

            services.AddScoped<IRepository, Repository>();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.DateFormatString = "dd/MM/yyyy HH:mm";
                });

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = context => 
					new BadRequestObjectResult(context.ModelState.FirstOrDefault().Value.Errors[0].ErrorMessage);
			});

			services.AddMediatR(typeof(GetCustomerQuery).GetTypeInfo().Assembly);
            services.AddAutoMapper(typeof(DefaultMappingProfile).GetTypeInfo().Assembly);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Customer API", Version = "v1" });

                c.AddSecurityDefinition("ApiKey", new ApiKeyScheme
                {
                    Description = "Authorization header using the ApiKey scheme. Example: \"Authorization: ApiKey {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "ApiKey", new string[] { } }
                });
            });
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

                app.UseExceptionHandler(a => a.Run(context =>
                {
                    context.Response.StatusCode = 400;
                    return Task.CompletedTask;
                }));
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
