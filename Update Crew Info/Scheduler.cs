using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Update_Crew_Info.Services;

namespace Update_Crew_Info
{
       
    public static class Scheduler
    {
        public static void IntervalInSeconds(int hour, int sec, double interval,bool startIntermediate, Action task)
        {
            interval = interval / 3600;
            SchedulerService.Instance.ScheduleTask(hour, sec, interval, startIntermediate, task);
        }

        public static void IntervalInMinutes(int hour, int min, double interval, bool startIntermediate, Action task)
        {
            interval = interval / 60;
            SchedulerService.Instance.ScheduleTask(hour, min, interval, startIntermediate, task);
        }

        public static void IntervalInHours(int hour, int min, double interval, bool startIntermediate, Action task)
        {
            SchedulerService.Instance.ScheduleTask(hour, min, interval, startIntermediate, task);
        }

        public static void IntervalInDays(int hour, int min, double interval, bool startIntermediate, Action task)
        {
            interval = interval * 24;
            SchedulerService.Instance.ScheduleTask(hour, min, interval, startIntermediate, task);
        }
    }
}
