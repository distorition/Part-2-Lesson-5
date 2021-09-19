using Microsoft.Extensions.Hosting;
using Part_2_Lesson_4.Interfaces;
using Part_2_Lesson_5.DAL.DTO;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Part_2_Lesson_5.DAL.Host
{
    public class QuartzHostedService : IHostedService//запуск задач по расписанию 
    {
        private readonly ISchedulerFactory _shelduerFactory;
        private readonly IJobFactory _jodFactoory;
        private readonly IEnumerable<JobScheldue> _jobScheldues;
        
        public QuartzHostedService(AgainRepositry repository,ISchedulerFactory factory,IJobFactory job,IEnumerable<JobScheldue> scheldues)
        {
            _shelduerFactory = factory;
            _jodFactoory = job;
            _jobScheldues = scheldues;
        }
        private IScheduler Scheduler { get; set; }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _shelduerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jodFactoory;
            foreach(var jobSheulder in _jobScheldues)
            {
                var jod = CreatJodDetail(jobSheulder);
                var triger = CreateTriger(jobSheulder);
                await Scheduler.ScheduleJob(jod, triger, cancellationToken);
            }
            await Scheduler.Start(cancellationToken);
           
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        private static IJobDetail CreatJodDetail(JobScheldue scheldue)
        {
            var jobtype = scheldue.JodbType;
            return JobBuilder.Create(jobtype).WithIdentity(jobtype.FullName).WithDescription(jobtype.Name).Build();
        }
        
        private static ITrigger CreateTriger(JobScheldue scheldue)
        {
            return TriggerBuilder.Create().WithIdentity($"{scheldue.JodbType.FullName}.trigger").WithCronSchedule(scheldue.CronExpression).WithDescription(scheldue.CronExpression).Build();


        }
    }
}
