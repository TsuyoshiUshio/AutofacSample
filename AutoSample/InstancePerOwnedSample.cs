using Autofac;
using Autofac.Features.OwnedInstances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSample
{
    public class InstancePerOwnedSampleSample
    {
        private static IContainer Container { get; set; }
        public void Exec()
        {
            Console.WriteLine("Instance Per Owned Scope ----");
            var builder = new ContainerBuilder();
            builder.RegisterType<Command>();
            builder.RegisterType<Backend>().As<IBackend>().InstancePerOwned<Command>();
            Container = builder.Build();
            Console.WriteLine("command 01 ---");
            using (var scope1 = Container.BeginLifetimeScope())
            {
                var command1 = scope1.Resolve<Owned<Command>>();
                command1.Value.Exec();
                command1.Value.Exec();
                command1.Dispose();
                var command2 = scope1.Resolve<Owned<Command>>();
                command2.Value.Exec();
                command2.Value.Exec();
                command2.Dispose();
            }

            Console.WriteLine("command 02 ---");
            using (var scope2 = Container.BeginLifetimeScope())
            {
                var command2 = scope2.Resolve<Owned<Command>>();
                command2.Value.Exec();
                command2.Value.Exec();
            }

        }


        public class Command
        {
            private IBackend backend;

            public Command(IBackend backend)
            {
                this.backend = backend;
            }
           public void Exec()
            { 
                Console.WriteLine($"Command: Backend is executed InstanceID: {backend.InstanceID()}");
            }
        }

    }
}
