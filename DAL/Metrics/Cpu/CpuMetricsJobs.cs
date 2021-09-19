using Part_2_Lesson_4;
using Part_2_Lesson_4.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_5.Metrics.Cpu
{
    public class CpuMetricsJobs : IJob//сбор метрики 
    {
        private AgainRepositry _repositry;
        private PerformanceCounter _cpuCount;
        
        public CpuMetricsJobs(AgainRepositry repositry)
        {
            _repositry = repositry;
            _cpuCount = new PerformanceCounter("Processor", "%Processor Time", "_Total");

        }

        public Task Execute(IJobExecutionContext context)
        {
            var cpuCountPErscent = Convert.ToInt32(_cpuCount.NextValue());
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _repositry.Create(new CpuMetrics { Time = time, Value = cpuCountPErscent });
            return Task.CompletedTask;
        }
    }
}
