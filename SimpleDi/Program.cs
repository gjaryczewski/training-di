using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace SimpleDi
{
    public interface IFooService
    {
        void DoThing(int number);
    }

    public class FooService : IFooService
    {
        public FooService()
        {
            Console.WriteLine("Creating FooService object");
        }

        public void DoThing(int number)
        {
            Console.WriteLine($"Doing the thing {number}");
        }
    }

    public interface IBarService
    {
        void DoSomeRealWork();
    }

    public class BarService : IBarService
    {
        private readonly IFooService _fooService;
        public BarService(IFooService fooService)
        {
            Console.WriteLine("Creating BarService object");
            _fooService = fooService;
        }

        public void DoSomeRealWork()
        {
            for (int i = 0; i < 10; i++)
            {
                _fooService.DoThing(i);
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IFooService, FooService>()
                .AddSingleton<IBarService, BarService>()
                .BuildServiceProvider();

            //do the actual work here
            var bar = serviceProvider.GetService<IBarService>();
            bar.DoSomeRealWork();

            Console.WriteLine("All done!");
        }
    }
}
