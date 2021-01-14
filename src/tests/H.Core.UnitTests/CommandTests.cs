using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Core.UnitTests
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void ParseTest()
        {
            var first = new Command("вставь", "123");
            var second = Command.Parse("вставь 123");

            first.Should().BeEquivalentTo(second);
        }
    }
}
