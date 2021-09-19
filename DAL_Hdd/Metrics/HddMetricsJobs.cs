using Part_2_Lesson_4.Hdd.Dto;
using Part_2_Lesson_4.Hdd.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_5.DAL_Hdd.Metrics
{
    public class HddMetricsJobs : IJob
    {
        private IHddInterfaces _repository;
        private PerformanceCounter _hddcount;
        public HddMetricsJobs(IHddInterfaces hddInterfaces)
        {
            _repository = hddInterfaces;
            _hddcount = new PerformanceCounter("Hdd", "Available MBytes");
        }
        public Task Execute(IJobExecutionContext context)
        {
            var Hddcount = Convert.ToInt32(_hddcount.NextValue());
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _repository.Create(new HddMetrics{ Time=time,value=Hddcount});
            return Task.CompletedTask;
        }
    }
}
