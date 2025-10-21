using GPScoreTracker.Domain.ValueObjects;
using Xunit;

namespace GPScoreTracker.Domain.Tests.ValueObjects;

/// <summary>
/// Level 値オブジェクトのテスト
/// </summary>
public class LevelTests
{
    #region Constructor Tests

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(19)]
    public void Constructor_ValidValue_CreatesInstance(int value)
    {
        // Act
        var level = new Level(value);

        // Assert
        Assert.Equal(value, level.Value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(20)]
    [InlineData(100)]
    public void Constructor_InvalidValue_ThrowsArgumentOutOfRangeException(int value)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Level(value));
        Assert.Contains("Level must be between 1 and 19", exception.Message);
    }

    #endregion

    #region Equality Tests

    [Fact]
    public void Equals_SameValue_ReturnsTrue()
    {
        // Arrange
        var level1 = new Level(10);
        var level2 = new Level(10);

        // Act & Assert
        Assert.Equal(level1, level2);
        Assert.True(level1 == level2);
        Assert.False(level1 != level2);
    }

    [Fact]
    public void Equals_DifferentValue_ReturnsFalse()
    {
        // Arrange
        var level1 = new Level(10);
        var level2 = new Level(11);

        // Act & Assert
        Assert.NotEqual(level1, level2);
        Assert.False(level1 == level2);
        Assert.True(level1 != level2);
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var level = new Level(10);

        // Act & Assert
        Assert.False(level.Equals(null));
        Assert.False(level == null);
        Assert.True(level != null);
    }

    [Fact]
    public void Equals_SameInstance_ReturnsTrue()
    {
        // Arrange
        var level = new Level(10);

        // Act & Assert
        // 同じインスタンスを比較することで、参照の等価性をテスト
#pragma warning disable CS1718 // 意図的に同じ変数を比較
        Assert.True(level.Equals(level));
        Assert.True(level == level);
#pragma warning restore CS1718
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameValue_ReturnsSameHashCode()
    {
        // Arrange
        var level1 = new Level(10);
        var level2 = new Level(10);

        // Act & Assert
        Assert.Equal(level1.GetHashCode(), level2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentValue_ReturnsDifferentHashCode()
    {
        // Arrange
        var level1 = new Level(10);
        var level2 = new Level(11);

        // Act & Assert
        Assert.NotEqual(level1.GetHashCode(), level2.GetHashCode());
    }

    #endregion

    #region ToString Tests

    [Theory]
    [InlineData(1, "1")]
    [InlineData(10, "10")]
    [InlineData(19, "19")]
    public void ToString_ReturnsValueAsString(int value, string expected)
    {
        // Arrange
        var level = new Level(value);

        // Act
        var result = level.ToString();

        // Assert
        Assert.Equal(expected, result);
    }

    #endregion

    #region Boundary Tests

    [Fact]
    public void Constructor_MinimumValue_CreatesInstance()
    {
        // Act
        var level = new Level(1);

        // Assert
        Assert.Equal(1, level.Value);
    }

    [Fact]
    public void Constructor_MaximumValue_CreatesInstance()
    {
        // Act
        var level = new Level(19);

        // Assert
        Assert.Equal(19, level.Value);
    }

    [Fact]
    public void Constructor_BelowMinimum_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Level(0));
    }

    [Fact]
    public void Constructor_AboveMaximum_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Level(20));
    }

    #endregion
}
