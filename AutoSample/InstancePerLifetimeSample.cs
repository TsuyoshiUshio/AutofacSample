using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSample
{
    public class InstancePerLifetimeSample
    {
        private static IContainer Container { get; set; }
        public void Exec()
        {
            Console.WriteLine("Instance Per Lifetime Scope ----");
            var builder = new ContainerBuilder();
            builder.RegisterType<Backend>().As<IBackend>().InstancePerLifetimeScope();
            Container = builder.Build();
            GetData();
        }
        private static void GetData()
        {
            using (var scope1 = Container.BeginLifetimeScope())
            {
                var writer = scope1.Resolve<IBackend>();
                var greeting = "hi";
                var serverMessage = writer.getContents(greeting);
                Console.WriteLine($"sent {greeting} response {serverMessage} ");

                var writer2 = scope1.Resolve<IBackend>();
                Console.WriteLine($"write 01 {writer.InstanceID()} write 02 {writer2.InstanceID()}");

            }
            using (var scope2 = Container.BeginLifetimeScope())
            {
                var writer = scope2.Resolve<IBackend>();
                var greeting = "hello";
                var serverMessage = writer.getContents(greeting);
                Console.WriteLine($"sent {greeting} response {serverMessage} ");

                var writer2 = scope2.Resolve<IBackend>();
                Console.WriteLine($"write 03 {writer2.InstanceID()} write 04 {writer2.InstanceID()}");

                using (var scope3 = Container.BeginLifetimeScope())
                {
                    var writer3 = scope3.Resolve<IBackend>();
                    Console.WriteLine($"write 05 {writer3.InstanceID()}");
                }

            }
        }
    }
}
