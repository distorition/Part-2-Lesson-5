using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part_2_Lesson_5.DAL.DTO
{
    public class JobScheldue//тут храним расписание запуска когда будет собирать метрики 
    {
        public JobScheldue(Type jobType,string cronExpression)
        {
            JodbType = jobType;
            CronExpression = cronExpression;
        }

        public Type JodbType { get; set; }
        public string CronExpression { get; set; }
    }
}
