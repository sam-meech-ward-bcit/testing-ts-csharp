using FluentAssertions;
namespace Week5.Web.Tests;

public class TestAddNumbers
{
    [Fact]
    public void Test_EmptyArray_Returns_0()
    {
        // Arrange 
        var addNumbers = new AddNumbers(new int[] { });
        var expected = 0;

        // Act
        var actual = addNumbers.Sum();

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Test_SingleNumber_Returns_ThatNumber()
    {
        // Arrange 
        var addNumbers = new AddNumbers(new int[] { 1 });
        var expected = 1;

        // Act
        var actual = addNumbers.Sum();

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Test_TwoNumbers_Returns_Sum()
    {
        // Arrange 
        var addNumbers = new AddNumbers(new int[] { 1, 2 });
        var expected = 3;

        // Act
        var actual = addNumbers.Sum();

        // Assert
        actual.Should().Be(expected);
    }
}