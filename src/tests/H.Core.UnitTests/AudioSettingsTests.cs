﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace H.Core.UnitTests
{
    [TestClass]
    public class AudioSettingsTests
    {
        [TestMethod]
        public void ParseTest()
        {
            AudioSettings.Parse("mp3")
                .Should().BeEquivalentTo(new AudioSettings(AudioFormat.Mp3));
        }
    }
}
