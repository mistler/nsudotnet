using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istomin.Nsudotnet.TaskScheduler
{
    interface IJob
    {
        void Execute(object argument);
    }
}
