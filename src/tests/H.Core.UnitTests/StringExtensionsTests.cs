using System;
using H.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Core.UnitTests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void CmdSplitTest()
        {
            var actual = "command \"argument1\" \"argument2\" argument3 \"argument4\"".CmdSplit();
            CollectionAssert.AreEquivalent(
                new [] { "command", "argument1", "argument2", "argument3", "argument4" },
                actual,
                string.Join(Environment.NewLine, actual));
        }
    }
}
