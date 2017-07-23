using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSample
{
    class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
            new SingleSample().Exec();
            new InstancePerLifetimeSample().Exec();
            new InstancePerMatchingLifetimeSample().Exec();
            new InstancePerOwnedSampleSample().Exec();
            new ThreadScopeSample().Exec();
            Console.ReadLine();

        }

    }
}
