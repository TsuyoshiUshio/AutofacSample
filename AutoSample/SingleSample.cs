using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSample
{
    public class SingleSample
    {
        private static IContainer Container { get; set; }
        public void Exec()
        {
            Console.WriteLine("SingleInstance ----");
            var builder = new ContainerBuilder();
            builder.RegisterType<Backend>().As<IBackend>().SingleInstance();
            Container = builder.Build();
            GetData();
        }
        private static void GetData()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IBackend>();
                var greeting = "hi";
                var serverMessage = writer.getContents("hi");
                Console.WriteLine($"sent {greeting} response {serverMessage} ");

                var writer2 = scope.Resolve<IBackend>();
                Console.WriteLine($"write 01 {writer.InstanceID()} write 02 {writer2.InstanceID()}");

            }
        }

    }
}
