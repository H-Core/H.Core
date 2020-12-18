using System;
using System.Threading.Tasks;
using H.Core.Runners;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Core.UnitTests
{
    [TestClass]
    public class RunnerTests
    {
        private static ICall IsSupportedTest(IRunner runner, string name, params string[] arguments)
        {
            var call = runner.TryPrepareCall(new Command(name, arguments));
            
            Assert.IsNotNull(call, $"{nameof(IsSupportedTest)}: {name}");
            call = call ?? throw new Exception();

            return call;
        }

        public static async Task<ICall> ActionTest(IAction action)
        {
            using var runner = new Runner
            {
                action,
            };
            var call = IsSupportedTest(runner, "print", "Hello, World!");

            Assert.AreEqual("print", call.Command.Name, nameof(call.Command.Name));
            CollectionAssert.AreEqual(new [] { "Hello, World!" }, call.Command.Value.Arguments, nameof(call.Command.Value.Arguments));
            CollectionAssert.AreEqual(new byte[0], call.Command.Value.Data, nameof(call.Command.Value.Data));
            Assert.AreEqual("Hello, World!", call.Command.Value.Argument, nameof(call.Command.Value.Argument));
            Assert.AreEqual("print", call.Action.Name, nameof(call.Action.Name));
            Assert.AreEqual("value", call.Action.Description, nameof(call.Action.Description));
            Assert.AreEqual(false, call.Action.IsInternal, nameof(call.Action.IsInternal));
            Assert.AreEqual(false, call.Action.IsCancellable, nameof(call.Action.IsCancellable));

            call.Running += (_, _) => Console.WriteLine($"{nameof(call.Running)}");
            call.Ran += (_, _) => Console.WriteLine($"{nameof(call.Ran)}");
            await call.RunAsync();

            return call;
        }
        
        [TestMethod]
        public async Task PrintTest()
        {
            await ActionTest(SyncAction.WithSingleArgument("print", Console.WriteLine, "value"));
        }

        [TestMethod]
        public async Task PrintAsyncTest()
        {
            await ActionTest(AsyncAction.WithSingleArgumentAndWithoutToken("print", argument =>
            {
                Console.WriteLine(argument);

                return Task.CompletedTask;
            }, "value"));
        }
    }
}
