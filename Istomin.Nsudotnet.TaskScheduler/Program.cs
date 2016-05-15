using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Istomin.Nsudotnet.TaskScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleTaskScheduler scheduler = new SimpleTaskScheduler();   

            IJob printHello = new ConsolePrintJob("Hello");
            IJob printWorld = new ConsolePrintJob("world");

            scheduler.ScheduleDelayedJob(printHello, null, new TimeSpan(0, 0, 0, 1));
            scheduler.SchedulePeriodicJob(printWorld, null, new TimeSpan(0, 0, 0, 0, 500));

            Thread.Sleep(10000);
        }
    }
}
