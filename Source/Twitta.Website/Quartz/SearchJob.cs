using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Quartz;
using StructureMap;
using Twitta.Website.Logic;

namespace Twitta.Website.Quartz
{
    [DisallowConcurrentExecution]
    public class SearchJob : IJob, IRegisteredObject
    {
        private readonly object _lock = new object();
        private bool _shuttingDown = false;
        private readonly ISearchJobLogic _searchJobLogic = ObjectFactory.GetInstance<ISearchJobLogic>();

        public void Execute(IJobExecutionContext context)
        {
            _searchJobLogic.RunAllSearches();
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