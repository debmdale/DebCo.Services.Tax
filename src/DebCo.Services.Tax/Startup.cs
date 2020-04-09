using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using AutoMapper;
using DebCo.Services.Tax.Options;
using DebCo.Services.Tax.Providers.TaxJar;
using DebCo.Services.Tax.Providers.TaxJar.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;

namespace DebCo.Services.Tax
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
            services.AddControllers();

            services.Configure<ApiBehaviorOptions>(s => s.SuppressMapClientErrors = true);

            services.AddAutoMapper(typeof(DebCoMappingProfile));
            
            services.AddApiVersioning(o =>
                {
                    o.DefaultApiVersion = ApiVersion.Default; // 1.0
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.ReportApiVersions = true;
                })
                .AddVersionedApiExplorer(options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    options.GroupNameFormat = "'v'VVV"; // "'v'major[.minor][-status]"

                    // note: this option is only necessary when versioning by url segment. 
                    options.SubstituteApiVersionInUrl = true;
                })
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DebCo Tax Service", Version = "v1" });
                });

            ConfigureServiceIntegrations(services, Configuration);

            services.AddScoped<ITaxJarService, TaxJarService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [JetBrains.Annotations.UsedImplicitly]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHeaderPropagation();

            app.UseRouting();

            app.UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.DocumentTitle = "DebCo.Services.Tax API Documentation";
                    options.RoutePrefix = "swagger";
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"../{options.RoutePrefix}/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureServiceIntegrations(IServiceCollection services, IConfiguration configuration)
        {
            TaxJarOptions options = new TaxJarOptions();
            configuration.GetSection("TaxJar").Bind(options);

            static HttpMessageHandler CreateSocketsHandler() => new SocketsHttpHandler
            {
                SslOptions =
                {
                    EnabledSslProtocols =
                        SslProtocols.Tls13 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12,
                    RemoteCertificateValidationCallback = delegate { return true; }
                }
            };

            var endpoint = new UriBuilder($"{options.ServiceUrl}{options.ServiceVersion}/");
            services.AddHttpClient("TaxJar", c =>
                {
                    c.BaseAddress = endpoint.Uri;
                    c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.Token);
                })
                .AddHeaderPropagation()
                .ConfigurePrimaryHttpMessageHandler(CreateSocketsHandler);
        }

        // TODO: implement retry policy
        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                    retryAttempt)));
        }
    }
}
