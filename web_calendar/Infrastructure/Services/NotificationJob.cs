using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_calendar.BL.Services
{
    public class NotificationJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;

            JobDataMap dataMap = context.JobDetail.JobDataMap;

            string eventName = dataMap.GetString("eventName");
            string kindOfNotification = dataMap.GetString("kindOfNotification");

            //notify somehow
        }
    }
}
