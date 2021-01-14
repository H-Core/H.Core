using FluentAssertions;
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
            "command \"argument1\" \"argument2\" argument3 \"argument4\"".CmdSplit()
                .Should().BeEquivalentTo("command", "argument1", "argument2", "argument3", "argument4");
        }
    }
}
