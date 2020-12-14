using System;
using H.Core.Runners;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Core.UnitTests
{
    [TestClass]
    public class RunnerTests
    {
        private static void IsSupportedTest(IRunner runner, string command)
        {
            Assert.IsTrue(runner.IsSupported(command), $"{nameof(IsSupportedTest)}: {command}");
        }

        [TestMethod]
        public void IsSupportedTest()
        {
            using var runner = new Runner
            {
                Actions =
                {
                    {
                        "print",
                        new RunInformation
                        {
                            Action = Console.WriteLine,
                        }
                    }
                }
            };
            
            IsSupportedTest(runner, "print Hello, World!");
        }
    }
}
