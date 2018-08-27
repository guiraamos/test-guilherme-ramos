using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using web.Bus;
using web.Repository;
using Web.Models;
using Web.Repository;

namespace Web
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

            services.AddTransient<IRepositoryBase<DisplayTypePrice>, RepositoryBase<DisplayTypePrice>>();
            services.AddTransient<IDisplayTypePriceBus, DisplayTypePriceBus>();

            services.AddDbContextPool<Context>(options =>
            {
                options.UseInMemoryDatabase("tests").ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                options.EnableSensitiveDataLogging();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            SeedDatabase(app, env);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private void SeedDatabase(IApplicationBuilder app, IHostingEnvironment env)
        {
            var serviceFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
            using (var scope = serviceFactory.CreateScope())
            {
                Task.WhenAll(
                  PrepareDatabaseAsync(scope.ServiceProvider, env)
                ).Wait();
            }
        }

        private async Task PrepareDatabaseAsync(IServiceProvider serviceProvider, IHostingEnvironment env)
        {
            var dbContext = serviceProvider.GetService<Context>();

            if (await dbContext.Database.EnsureCreatedAsync())
            {
                await dbContext.SeedData();
            }
        }
    }
}
