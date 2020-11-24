using System;
using H.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Core.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        private static void BaseTwoStringTupleTest((string?, string?) expected, string?[] actual)
        {
            Assert.AreEqual(expected.Item1, actual[0]);
            Assert.AreEqual(expected.Item2, actual[1]);
        }

        [TestMethod]
        public void SplitOnlyFirstTest()
        {
            BaseTwoStringTupleTest(("test", "test test"), "test test test".SplitOnlyFirst(' '));
            BaseTwoStringTupleTest(("test", "test"), "test test".SplitOnlyFirst(' '));
            BaseTwoStringTupleTest(("test", ""), "test ".SplitOnlyFirst(' '));
            BaseTwoStringTupleTest(("test", null), "test".SplitOnlyFirst(' '));
            BaseTwoStringTupleTest(("", ""), " ".SplitOnlyFirst(' '));
            BaseTwoStringTupleTest(("", null), "".SplitOnlyFirst(' '));

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                string? test = null;
                test.SplitOnlyFirst(' ');
            });
        }
    }
}
