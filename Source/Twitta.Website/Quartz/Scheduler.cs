using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Quartz.Impl;
using Quartz.Impl.Triggers;

namespace Twitta.Website.Quartz
{
    public static class Scheduler
    {
        public static void Init()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            var job = new JobDetailImpl("SearchJob", "jobs", typeof(SearchJob));
            var trigger = new CronTriggerImpl("SearchTrigger", "jobs", ConfigurationManager.AppSettings["CronTrigger"]);
            scheduler.ScheduleJob(job, trigger);
        }
    }
}