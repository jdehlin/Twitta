using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Quartz;

namespace Twitta.Website.Quartz
{
    [DisallowConcurrentExecution]
    public class SearchJob : IJob, IRegisteredObject
    {
        private readonly object _lock = new object();
        private bool _shuttingDown = false;

        public void Execute(IJobExecutionContext context)
        {
            //TODO - implement calling the actua repo/logic methods
        }

        public void Stop(bool immediate)
        {
            lock (_lock)
            {
                _shuttingDown = true;
            }
            HostingEnvironment.UnregisterObject(this);
        }
    }
}