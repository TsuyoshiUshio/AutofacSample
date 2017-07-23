using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSample
{
    public class InstancePerMatchingLifetimeSample
    {
        private static IContainer Container { get; set; }
        public void Exec()
        {
            Console.WriteLine("Instance Per Matching Lifetime Scope ----");
            var builder = new ContainerBuilder();
            builder.RegisterType<Backend>().As<IBackend>().InstancePerMatchingLifetimeScope("aRequest");
   //         builder.RegisterType<Backend>().As<IBackend>().InstancePerMatchingLifetimeScope("bRequest");

            Container = builder.Build();
            GetData();
        }
        private static void GetData()
        {
            using (var scope1 = Container.BeginLifetimeScope("aRequest"))
            {
                var writer = scope1.Resolve<IBackend>();
                var greeting = "hi";
                var serverMessage = writer.getContents(greeting);
                Console.WriteLine($"sent {greeting} response {serverMessage} ");

                var writer2 = scope1.Resolve<IBackend>();
                Console.WriteLine($"write 01 {writer.InstanceID()} write 02 {writer2.InstanceID()}");

            }
            using (var scope2 = Container.BeginLifetimeScope("aRequest"))
            {
                var writer = scope2.Resolve<IBackend>();
                var greeting = "hello";
                var serverMessage = writer.getContents(greeting);
                Console.WriteLine($"sent {greeting} response {serverMessage} ");
                using (var scope3 = scope2.BeginLifetimeScope())
                {

                    var writer2 = scope3.Resolve<IBackend>();
                    Console.WriteLine($"write 03 {writer2.InstanceID()} write 04 {writer2.InstanceID()}");
                }
            }

  //          using (var scope4 = Container.BeginLifetimeScope("bRequest"))
  //          {
  //              var writer = scope4.Resolve<IBackend>();
  //              Console.WriteLine($"write 05 {writer.InstanceID()}");
  //          }


        }
    }
}
