using AutoMapper;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Part_2_Lesson_4;
using Part_2_Lesson_4.Hdd.Interfaces;
using Part_2_Lesson_4.Hdd.Mapper;
using Part_2_Lesson_4.Hdd.Repository;
using Part_2_Lesson_4.Interfaces;
using Part_2_Lesson_5.DAL.DTO;
using Part_2_Lesson_5.DAL.Host;
using Part_2_Lesson_5.DAL.Jobs;
using Part_2_Lesson_5.DAL_Hdd.JobsHdd;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string ConnectionString = @"Data Source=metrics.db; Version=3;";


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddSingleton<IHddInterfaces, HddRepository>();
            var mapperConfigHdd = new MapperConfiguration(mph => mph.AddProfile(new MapperHdd()));
            var mapperHdd = mapperConfigHdd.CreateMapper();
            services.AddSingleton(mapperConfigHdd);


            services.AddSingleton<AgainRepositry, CpuMetricRepository > ();
            var mapperCongif = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperCongif.CreateMapper();
            services.AddSingleton(mapper);

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb=>rb.AddSQLite().WithGlobalConnectionString(ConnectionString).ScanIn(typeof(Startup).Assembly).For.Migrations()).AddLogging(ld=>ld.AddFluentMigratorConsole());
            services.AddSingleton<IJobFactory, SingeltonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<CpuMetricsJobs>();
            services.AddSingleton(new JobScheldue(jobType: typeof(CpuMetricsJobs), cronExpression: "0/5 * * * * ?"));

            services.AddSingleton<HddJobs>();
            services.AddSingleton(new JobScheldue(jobType: typeof(HddJobs), cronExpression: "0/5 * * * * ?"));

            services.AddHostedService<QuartzHostedService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Part_2_Lesson_5", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Part_2_Lesson_5 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
