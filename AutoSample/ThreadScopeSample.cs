using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoSample
{
    public class ThreadScopeSample
    {
        private static IContainer Container { get; set; }
        public void Exec()
        {
            Console.WriteLine("Thread Scope ----");
            var builder = new ContainerBuilder();
            builder.RegisterType<ThreadCreator>();
            builder.RegisterType<Backend>().As<IBackend>().InstancePerLifetimeScope();
            Container = builder.Build();
            using (var scope = Container.BeginLifetimeScope())
            {
                var tc = scope.Resolve<ThreadCreator>();
                Thread aThread = new Thread(new ThreadStart(tc.ThreadExec));
                Thread bThread = new Thread(new ThreadStart(tc.ThreadExec));
                aThread.Start();
                bThread.Start();
                aThread.Join();
                bThread.Join();
            }
        }
    }

    public class ThreadCreator
    {
        private ILifetimeScope parentScope;

        public ThreadCreator(ILifetimeScope scope)
        {
            parentScope = scope;
        }
        public void ThreadExec()
        {
            using (var scope = parentScope.BeginLifetimeScope())
            {
                var backend1 = scope.Resolve<IBackend>();
                var backend2 = scope.Resolve<IBackend>();
                Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId} backend1 : {backend1.InstanceID() } backend2 : {backend2.InstanceID() }");
            }
        }
    }


}
