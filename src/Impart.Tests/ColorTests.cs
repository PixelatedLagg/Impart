using Xunit;

namespace Impart.Tests
{
    public class ColorTests
    {
        [Fact]
        public void Rgb()
        {
            Assert.Equal((1, 2, 3), new Rgb(1, 2, 3).rgb);
        }
        /*[Fact]
        public void RgbToHsl()
        {
            Assert.Equal((90, 100, 39), ConvertColor.RgbToHsl(new Rgb(100, 200, 0)).hsl);
        }*/ //still fixing
        [Fact]
        public void RgbToHex()
        {
            Assert.Equal("ABC123", new Rgb(171, 193, 35).ToHex().hex);
        }
        [Fact]
        public void Hex()
        {
            Assert.Equal("abc123", new Hex("abc123").hex);
        }
        [Fact]
        public void HexToRgb()
        {
            Assert.Equal((1, 2, 3), new Hex("010203").ToRgb().rgb);
        }
        [Fact]
        public void Hsl()
        {
            Assert.Equal((360, 0, 0), new Hsl(360, 0, 0).hsl);
        }
    }
}