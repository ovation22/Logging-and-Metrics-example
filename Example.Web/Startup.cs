using App.Metrics;
using Example.Models;
using Example.Repositories;
using Example.Repositories.Interfaces;
using Example.Services;
using Example.Services.Interfaces;
using Example.Web.Interfaces;
using Example.Web.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Example.Web.Filters;

namespace Example.Web
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

            services.AddDbContextPool<ExampleContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .WriteTo.MSSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), "Logs")
                .WriteTo.Console()
                .WriteTo.Debug()
                .CreateLogger();

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add(new MetricsResourceFilter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IMapper<Dto.Horse, Models.HorseSummary>), typeof(HorseToHorseSummaryMapper));
            services.AddScoped(typeof(IMapper<Dto.Horse, Models.HorseDetail>), typeof(HorseToHorseDetailMapper));
            services.AddTransient<IHorseService, HorseService>();

            var metrics = AppMetrics.CreateDefaultBuilder()
                .Report
                .ToInfluxDb("http://127.0.0.1:8086", "Example")
                .Build();

            services.AddMetrics(metrics);
            services.AddMetricsReportingHostedService();
            services.AddMetricsTrackingMiddleware();
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMetricsAllMiddleware();
            app.UseMetricsRequestTrackingMiddleware();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
