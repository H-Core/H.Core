using H.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Core.Tests
{
    [TestClass]
    public class InvariantStringDictionaryTests
    {
        private static void BaseContainsTest<T>(InvariantStringDictionary<T> dictionary, string key, T expected)
        {
            Assert.IsTrue(dictionary.ContainsKey(key));
            Assert.IsTrue(dictionary.TryGetValue(key, out _));
            Assert.AreEqual(expected, dictionary[key]);
        }

        [TestMethod]
        public void InvariantStringDictionaryStringTest()
        {
            var dictionary = new InvariantStringDictionary<string>
            {
                ["key1"] = "value1",
                ["KEY2"] = "value2",
                ["Key3"] = "value3",
                ["KeY4"] = "value4"
            };

            BaseContainsTest(dictionary, "key1", "value1");
            BaseContainsTest(dictionary, "KEY1", "value1");
            BaseContainsTest(dictionary, "KeY1", "value1");
            BaseContainsTest(dictionary, "Key1", "value1");

            BaseContainsTest(dictionary, "key2", "value2");
            BaseContainsTest(dictionary, "KEY2", "value2");
            BaseContainsTest(dictionary, "KeY2", "value2");
            BaseContainsTest(dictionary, "Key2", "value2");

            BaseContainsTest(dictionary, "key3", "value3");
            BaseContainsTest(dictionary, "KEY3", "value3");
            BaseContainsTest(dictionary, "KeY3", "value3");
            BaseContainsTest(dictionary, "Key3", "value3");

            BaseContainsTest(dictionary, "key4", "value4");
            BaseContainsTest(dictionary, "KEY4", "value4");
            BaseContainsTest(dictionary, "KeY4", "value4");
            BaseContainsTest(dictionary, "Key4", "value4");
        }
    }
}
