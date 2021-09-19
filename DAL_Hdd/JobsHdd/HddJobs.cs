using Part_2_Lesson_4.Hdd.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_5.DAL_Hdd.JobsHdd
{
    public class HddJobs : IJob
    {
        private IHddInterfaces _repistrory;
        public HddJobs(IHddInterfaces hddInterfaces)
        {
            _repistrory = hddInterfaces;
        }
        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
