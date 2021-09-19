using Part_2_Lesson_4.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_5.DAL.Jobs
{
    public class CpuMetricsJobs : IJob//тут мы записываем что то при помощи репозитория (добавляем в репозиторий)
    {
        private AgainRepositry _repository;
        public CpuMetricsJobs(AgainRepositry againRepositry)
        {
            _repository = againRepositry;
        }
        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
