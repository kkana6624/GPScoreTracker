using GPScoreTracker.Domain.ValueObjects;
using Xunit;

namespace GPScoreTracker.Domain.Tests.ValueObjects;

/// <summary>
/// Judgements 値オブジェクトのテスト
/// </summary>
public class JudgementsTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_ValidValues_CreatesInstance()
    {
        // Act
        var judgements = new Judgements(
            marvelous: 100,
            perfect: 50,
            great: 30,
            good: 10,
            miss: 5);

        // Assert
        Assert.Equal(100, judgements.Marvelous);
        Assert.Equal(50, judgements.Perfect);
        Assert.Equal(30, judgements.Great);
        Assert.Equal(10, judgements.Good);
        Assert.Equal(5, judgements.Miss);
    }

    [Fact]
    public void Constructor_AllZeroValues_CreatesInstance()
    {
        // Act
        var judgements = new Judgements(0, 0, 0, 0, 0);

        // Assert
        Assert.Equal(0, judgements.Marvelous);
        Assert.Equal(0, judgements.Perfect);
        Assert.Equal(0, judgements.Great);
        Assert.Equal(0, judgements.Good);
        Assert.Equal(0, judgements.Miss);
    }

    [Theory]
    [InlineData(-1, 0, 0, 0, 0)]
    [InlineData(0, -1, 0, 0, 0)]
    [InlineData(0, 0, -1, 0, 0)]
    [InlineData(0, 0, 0, -1, 0)]
    [InlineData(0, 0, 0, 0, -1)]
    public void Constructor_NegativeValue_ThrowsArgumentOutOfRangeException(
        int marvelous, int perfect, int great, int good, int miss)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => new Judgements(marvelous, perfect, great, good, miss));
        Assert.Contains("must be non-negative", exception.Message);
    }

    #endregion

    #region TotalNotes Tests

    [Fact]
    public void TotalNotes_ReturnsCorrectSum()
    {
        // Arrange
        var judgements = new Judgements(
            marvelous: 100,
            perfect: 50,
            great: 30,
            good: 10,
            miss: 5);

        // Act
        var total = judgements.TotalNotes;

        // Assert
        Assert.Equal(195, total);
    }

    [Fact]
    public void TotalNotes_AllZero_ReturnsZero()
    {
        // Arrange
        var judgements = new Judgements(0, 0, 0, 0, 0);

        // Act
        var total = judgements.TotalNotes;

        // Assert
        Assert.Equal(0, total);
    }

    #endregion

    #region Equality Tests

    [Fact]
    public void Equals_SameValues_ReturnsTrue()
    {
        // Arrange
        var judgements1 = new Judgements(100, 50, 30, 10, 5);
        var judgements2 = new Judgements(100, 50, 30, 10, 5);

        // Act & Assert
        Assert.Equal(judgements1, judgements2);
        Assert.True(judgements1 == judgements2);
        Assert.False(judgements1 != judgements2);
    }

    [Fact]
    public void Equals_DifferentMarvelous_ReturnsFalse()
    {
        // Arrange
        var judgements1 = new Judgements(100, 50, 30, 10, 5);
        var judgements2 = new Judgements(99, 50, 30, 10, 5);

        // Act & Assert
        Assert.NotEqual(judgements1, judgements2);
        Assert.False(judgements1 == judgements2);
        Assert.True(judgements1 != judgements2);
    }

    [Fact]
    public void Equals_DifferentPerfect_ReturnsFalse()
    {
        // Arrange
        var judgements1 = new Judgements(100, 50, 30, 10, 5);
        var judgements2 = new Judgements(100, 49, 30, 10, 5);

        // Act & Assert
        Assert.NotEqual(judgements1, judgements2);
    }

    [Fact]
    public void Equals_DifferentGreat_ReturnsFalse()
    {
        // Arrange
        var judgements1 = new Judgements(100, 50, 30, 10, 5);
        var judgements2 = new Judgements(100, 50, 29, 10, 5);

        // Act & Assert
        Assert.NotEqual(judgements1, judgements2);
    }

    [Fact]
    public void Equals_DifferentGood_ReturnsFalse()
    {
        // Arrange
        var judgements1 = new Judgements(100, 50, 30, 10, 5);
        var judgements2 = new Judgements(100, 50, 30, 9, 5);

        // Act & Assert
        Assert.NotEqual(judgements1, judgements2);
    }

    [Fact]
    public void Equals_DifferentMiss_ReturnsFalse()
    {
        // Arrange
        var judgements1 = new Judgements(100, 50, 30, 10, 5);
        var judgements2 = new Judgements(100, 50, 30, 10, 4);

        // Act & Assert
        Assert.NotEqual(judgements1, judgements2);
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var judgements = new Judgements(100, 50, 30, 10, 5);

        // Act & Assert
        Assert.False(judgements.Equals(null));
        Assert.False(judgements == null);
        Assert.True(judgements != null);
    }

    [Fact]
    public void Equals_SameInstance_ReturnsTrue()
    {
        // Arrange
        var judgements = new Judgements(100, 50, 30, 10, 5);

        // Act & Assert
        // 同じインスタンスを比較することで、参照の等価性をテスト
#pragma warning disable CS1718 // 意図的に同じ変数を比較
        Assert.True(judgements.Equals(judgements));
        Assert.True(judgements == judgements);
#pragma warning restore CS1718
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameValues_ReturnsSameHashCode()
    {
        // Arrange
        var judgements1 = new Judgements(100, 50, 30, 10, 5);
        var judgements2 = new Judgements(100, 50, 30, 10, 5);

        // Act & Assert
        Assert.Equal(judgements1.GetHashCode(), judgements2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentValues_ReturnsDifferentHashCode()
    {
        // Arrange
        var judgements1 = new Judgements(100, 50, 30, 10, 5);
        var judgements2 = new Judgements(100, 50, 30, 10, 4);

        // Act & Assert
        Assert.NotEqual(judgements1.GetHashCode(), judgements2.GetHashCode());
    }

    #endregion

    #region Edge Case Tests

    [Fact]
    public void Constructor_MaximumValues_CreatesInstance()
    {
        // Act
        var judgements = new Judgements(
            marvelous: int.MaxValue,
            perfect: int.MaxValue,
            great: int.MaxValue,
            good: int.MaxValue,
            miss: int.MaxValue);

        // Assert
        Assert.Equal(int.MaxValue, judgements.Marvelous);
        Assert.Equal(int.MaxValue, judgements.Perfect);
        Assert.Equal(int.MaxValue, judgements.Great);
        Assert.Equal(int.MaxValue, judgements.Good);
        Assert.Equal(int.MaxValue, judgements.Miss);
    }

    [Fact]
    public void Constructor_OnlyMarvelous_CreatesInstance()
    {
        // Act
        var judgements = new Judgements(marvelous: 500, perfect: 0, great: 0, good: 0, miss: 0);

        // Assert
        Assert.Equal(500, judgements.Marvelous);
        Assert.Equal(500, judgements.TotalNotes);
    }

    [Fact]
    public void Constructor_OnlyMiss_CreatesInstance()
    {
        // Act
        var judgements = new Judgements(marvelous: 0, perfect: 0, great: 0, good: 0, miss: 100);

        // Assert
        Assert.Equal(100, judgements.Miss);
        Assert.Equal(100, judgements.TotalNotes);
    }

    #endregion
}
