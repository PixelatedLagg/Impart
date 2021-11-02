using System;
using Csweb;
using Xunit;

namespace Csweb.Tests
{
    public class ColorTests
    {
        [Fact]
        public void Rgb()
        {
            Assert.Equal((1, 2, 3), new Rgb(1, 2, 3).rgb);
        }
        [Fact]
        public void Hex()
        {
            Assert.Equal("abc123", new Hex("abc123").hex);
        }
        [Fact]
        public void Hsl()
        {
            Assert.Equal((360, 0, 0), new Hsl(360, 0, 0).hsl);
        }
        [Fact]
        public void HslToRgb()
        {
            Assert.Equal((1, 2, 3), ConvertColor.HslToRgb(new Hsl(210, 0, 0)).rgb);
        }
        [Fact]
        public void HexToRgb()
        {
            Assert.Equal((1, 2, 3), ConvertColor.HexToRgb(new Hex("010203")).rgb);
        }
    }
}