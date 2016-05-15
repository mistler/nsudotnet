using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Istomin.Nsudotnet.TaskScheduler
{
    class SimpleTaskScheduler
    {
        public void ScheduleDelayedJob(IJob job, object arg, TimeSpan delay)
        {
            Exec(job, arg, delay, true);
        }

        public void SchedulePeriodicJob(IJob job, object arg, TimeSpan period)
        {
            Exec(job, arg, period, false);   
        }

        private void Callback(object o, bool b)
        {
            object[] args = o as object[];
            IJob job = args[0] as IJob;
            object arg = args[1];
            job.Execute(arg);
        }

        private void Exec(IJob job, object arg, TimeSpan time, bool executeOnlyOnce)
        {
            if (job == null) throw new ArgumentNullException("Job cannot be null!");
            EventWaitHandle eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            object state = new[] { job, arg };
            WaitOrTimerCallback waitOrTimerCallback = delegate(object o, bool b) { Callback(o, b); };

            ThreadPool.RegisterWaitForSingleObject(eventWaitHandle, waitOrTimerCallback, state, time, executeOnlyOnce);

        }
    }
}
