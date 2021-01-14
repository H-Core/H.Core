using System;
using System.Threading.Tasks;
using FluentAssertions;
using H.Core.Runners;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Core.UnitTests
{
    [TestClass]
    public class RunnerTests
    {
        public static async Task<ICall> ActionBaseTest(IAction action)
        {
            using var runner = new Runner
            {
                action,
            };
            var call = runner.TryPrepareCall(new Command("print", "Hello, World!")) ??
                       throw new InvalidOperationException("Call is null.");
            call.Running += (_, _) => Console.WriteLine($"{nameof(call.Running)}");
            call.Ran += (_, _) => Console.WriteLine($"{nameof(call.Ran)}");

            call.Should().BeEquivalentTo(new Call(action, new Command("print", "Hello, World!")));

            await call.RunAsync();

            return call;
        }
        
        [TestMethod]
        public async Task PrintTest()
        {
            await ActionBaseTest(SyncAction.WithSingleArgument("print", Console.WriteLine, "value"));
        }

        [TestMethod]
        public async Task PrintAsyncTest()
        {
            await ActionBaseTest(AsyncAction.WithSingleArgument("print", argument =>
            {
                Console.WriteLine(argument);

                return Task.CompletedTask;
            }, "value"));
        }
    }
}
