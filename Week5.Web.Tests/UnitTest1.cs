using FluentAssertions;
namespace Week5.Web.Tests;


public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var x = 1;
        x.Should().Be(1);
    }
}