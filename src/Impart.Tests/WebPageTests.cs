using Xunit;

namespace Impart.Tests
{
    public class CSWebObjTests
    {
        private static WebPage test = new WebPage("test.html", "test.css");
        [Fact]
        public void NamesTest()
        {
            Assert.Equal(("test.html", "test.css"), (test.path, test.cssPath));
        }
    }
}