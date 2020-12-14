using System;
using System.Threading.Tasks;
using H.Core.Runners;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Core.UnitTests
{
    [TestClass]
    public class RunnerTests
    {
        private static ICall IsSupportedTest(IRunner runner, string command)
        {
            var call = runner.TryPrepareCall(command);
            
            Assert.IsNotNull(call, $"{nameof(IsSupportedTest)}: {command}");
            call = call ?? throw new Exception();

            return call;
        }

        public static async Task<ICall> CommandTest(ICommand command)
        {
            using var runner = new Runner
            {
                command,
            };

            var call = IsSupportedTest(runner, "print Hello, World!");

            Assert.AreEqual("Hello, World!", call.Arguments, nameof(call.Arguments));
            Assert.AreEqual("print", call.Command.Prefix, nameof(call.Command.Prefix));
            Assert.AreEqual(string.Empty, call.Command.Description, nameof(call.Command.Description));
            Assert.AreEqual(false, call.Command.IsInternal, nameof(call.Command.IsInternal));

            call.Running += (_, _) => Console.WriteLine($"{nameof(call.Running)}");
            call.Ran += (_, _) => Console.WriteLine($"{nameof(call.Ran)}");
            await call.RunAsync();

            return call;
        }
        
        [TestMethod]
        public async Task PrintTest()
        {
            var call = await CommandTest(new Command("print", Console.WriteLine));
            
            Assert.AreEqual(false, call.Command.IsCancellable, nameof(call.Command.IsCancellable));
        }

        [TestMethod]
        public async Task PrintAsyncTest()
        {
            var call = await CommandTest(new AsyncCommand("print", arguments =>
            {
                Console.WriteLine(arguments);

                return Task.CompletedTask;
            }));

            Assert.AreEqual(true, call.Command.IsCancellable, nameof(call.Command.IsCancellable));
        }
    }
}
