using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Istomin.Nsudotnet.TaskScheduler
{
    class ConsolePrintJob : IJob
    {
        private string toPrint;

        public ConsolePrintJob(string toPrint)
        {
            this.toPrint = toPrint;
        }

        private ConsolePrintJob() { }

        public void Execute(object argument)
        {
            Console.WriteLine(toPrint);
        }
    }
}
