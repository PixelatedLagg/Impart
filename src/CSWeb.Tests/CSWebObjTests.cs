using Xunit;

namespace CSWeb.Tests
{
    public class CSWebObjTests
    {
        private static cswebobj test = new cswebobj("test.html", "test.css");
        [Fact]
        public void NamesTest()
        {
            Assert.Equal(("test.html", "test.css"), (test.path, test.cssPath));
        }
    }
}