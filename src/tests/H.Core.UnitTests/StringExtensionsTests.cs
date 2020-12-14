using System;
using H.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Core.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        private static void BaseTwoStringTupleTest((string, string) expected, string[] actual)
        {
            Assert.AreEqual(2, actual.Length);
            Assert.AreEqual(expected.Item1, actual[0]);
            Assert.AreEqual(expected.Item2, actual[1]);
        }
        
        private static void BaseTest(string expected, string[] actual)
        {
            Assert.AreEqual(1, actual.Length);
            Assert.AreEqual(expected, actual[0]);
        }

        [TestMethod]
        public void SplitOnlyFirstTest()
        {
            BaseTwoStringTupleTest(("test", "test test"), "test test test".SplitOnlyFirst(' '));
            BaseTwoStringTupleTest(("test", "test"), "test test".SplitOnlyFirst(' '));
            BaseTwoStringTupleTest(("test", ""), "test ".SplitOnlyFirst(' '));
            BaseTwoStringTupleTest(("", ""), " ".SplitOnlyFirst(' '));

            BaseTest("test", "test".SplitOnlyFirst(' '));
            BaseTest("", "".SplitOnlyFirst(' '));
            
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                string? test = null;
                test.SplitOnlyFirst(' ');
            });
        }
    }
}
