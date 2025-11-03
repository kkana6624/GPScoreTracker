using GPScoreTracker.Domain.Enums;
using GPScoreTracker.Domain.ValueObjects;
using Xunit;

namespace GPScoreTracker.Domain.Tests.ValueObjects;

/// <summary>
/// ChartIdentifier 値オブジェクトのテスト
/// </summary>
public class ChartIdentifierTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_ValidValues_CreatesInstance()
    {
        // Arrange
        var songId = Guid.NewGuid();
  var level = new Level(15);

    // Act
        var chartId = new ChartIdentifier(songId, Difficulty.Expert, level);

      // Assert
        Assert.Equal(songId, chartId.SongId);
        Assert.Equal(Difficulty.Expert, chartId.Difficulty);
        Assert.Equal(level, chartId.Level);
    }

    [Fact]
    public void Constructor_AllDifficulties_CreatesInstance()
    {
   // Arrange
        var songId = Guid.NewGuid();
        var level = new Level(10);

        // Act & Assert
        foreach (Difficulty difficulty in Enum.GetValues(typeof(Difficulty)))
      {
   var chartId = new ChartIdentifier(songId, difficulty, level);
    Assert.Equal(songId, chartId.SongId);
        Assert.Equal(difficulty, chartId.Difficulty);
            Assert.Equal(level, chartId.Level);
  }
    }

    [Fact]
    public void Constructor_NullLevel_ThrowsArgumentNullException()
    {
        // Arrange
    var songId = Guid.NewGuid();

      // Act & Assert
   var exception = Assert.Throws<ArgumentNullException>(
            () => new ChartIdentifier(songId, Difficulty.Expert, null!));
    Assert.Equal("level", exception.ParamName);
    }

    #endregion

    #region Equality Tests

    [Fact]
    public void Equals_SameValues_ReturnsTrue()
    {
 // Arrange
var songId = Guid.NewGuid();
        var level = new Level(15);
        var chartId1 = new ChartIdentifier(songId, Difficulty.Expert, level);
   var chartId2 = new ChartIdentifier(songId, Difficulty.Expert, level);

// Act & Assert
      Assert.Equal(chartId1, chartId2);
        Assert.True(chartId1 == chartId2);
        Assert.False(chartId1 != chartId2);
    }

  [Fact]
    public void Equals_DifferentSongId_ReturnsFalse()
    {
        // Arrange
        var songId1 = Guid.NewGuid();
   var songId2 = Guid.NewGuid();
var level = new Level(15);
        var chartId1 = new ChartIdentifier(songId1, Difficulty.Expert, level);
        var chartId2 = new ChartIdentifier(songId2, Difficulty.Expert, level);

        // Act & Assert
        Assert.NotEqual(chartId1, chartId2);
        Assert.False(chartId1 == chartId2);
        Assert.True(chartId1 != chartId2);
    }

    [Fact]
    public void Equals_DifferentDifficulty_ReturnsFalse()
    {
  // Arrange
        var songId = Guid.NewGuid();
        var level = new Level(15);
        var chartId1 = new ChartIdentifier(songId, Difficulty.Expert, level);
     var chartId2 = new ChartIdentifier(songId, Difficulty.Challenge, level);

  // Act & Assert
        Assert.NotEqual(chartId1, chartId2);
    }

    [Fact]
    public void Equals_DifferentLevel_ReturnsFalse()
    {
        // Arrange
        var songId = Guid.NewGuid();
      var level1 = new Level(15);
  var level2 = new Level(16);
        var chartId1 = new ChartIdentifier(songId, Difficulty.Expert, level1);
    var chartId2 = new ChartIdentifier(songId, Difficulty.Expert, level2);

      // Act & Assert
        Assert.NotEqual(chartId1, chartId2);
    }

 [Fact]
    public void Equals_Null_ReturnsFalse()
    {
      // Arrange
        var songId = Guid.NewGuid();
        var level = new Level(15);
        var chartId = new ChartIdentifier(songId, Difficulty.Expert, level);

        // Act & Assert
        Assert.False(chartId.Equals(null));
        Assert.False(chartId == null);
        Assert.True(chartId != null);
    }

    [Fact]
    public void Equals_SameInstance_ReturnsTrue()
    {
    // Arrange
        var songId = Guid.NewGuid();
        var level = new Level(15);
    var chartId = new ChartIdentifier(songId, Difficulty.Expert, level);

     // Act & Assert
#pragma warning disable CS1718 // 意図的に同一変数を比較
        Assert.True(chartId.Equals(chartId));
        Assert.True(chartId == chartId);
#pragma warning restore CS1718
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
public void GetHashCode_SameValues_ReturnsSameHashCode()
    {
        // Arrange
   var songId = Guid.NewGuid();
        var level = new Level(15);
 var chartId1 = new ChartIdentifier(songId, Difficulty.Expert, level);
        var chartId2 = new ChartIdentifier(songId, Difficulty.Expert, level);

        // Act & Assert
        Assert.Equal(chartId1.GetHashCode(), chartId2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentValues_ReturnsDifferentHashCode()
    {
     // Arrange
   var songId1 = Guid.NewGuid();
        var songId2 = Guid.NewGuid();
        var level = new Level(15);
        var chartId1 = new ChartIdentifier(songId1, Difficulty.Expert, level);
   var chartId2 = new ChartIdentifier(songId2, Difficulty.Expert, level);

     // Act & Assert
        Assert.NotEqual(chartId1.GetHashCode(), chartId2.GetHashCode());
    }

    #endregion

    #region ToString Tests

 [Fact]
    public void ToString_ReturnsCorrectFormat()
    {
        // Arrange
        var songId = Guid.Parse("12345678-1234-1234-1234-123456789abc");
     var level = new Level(15);
  var chartId = new ChartIdentifier(songId, Difficulty.Expert, level);

        // Act
        var result = chartId.ToString();

      // Assert
        Assert.Contains("SongId:12345678-1234-1234-1234-123456789abc", result);
        Assert.Contains("Difficulty:Expert", result);
      Assert.Contains("Level:15", result);
    }

    #endregion

    #region Realistic Scenario Tests

    [Fact]
    public void Constructor_TypicalExpertChart_CreatesInstance()
    {
        // Arrange - 典型的なExpert譜面
      var songId = Guid.NewGuid();
        var level = new Level(15);

        // Act
   var chartId = new ChartIdentifier(songId, Difficulty.Expert, level);

        // Assert
Assert.Equal(songId, chartId.SongId);
        Assert.Equal(Difficulty.Expert, chartId.Difficulty);
   Assert.Equal(15, chartId.Level.Value);
 }

    [Fact]
    public void Constructor_BeginnerChart_CreatesInstance()
    {
 // Arrange - Beginner譜面
        var songId = Guid.NewGuid();
        var level = new Level(3);

        // Act
     var chartId = new ChartIdentifier(songId, Difficulty.Beginner, level);

    // Assert
     Assert.Equal(songId, chartId.SongId);
        Assert.Equal(Difficulty.Beginner, chartId.Difficulty);
        Assert.Equal(3, chartId.Level.Value);
    }

    [Fact]
    public void Constructor_ChallengeChart_CreatesInstance()
    {
        // Arrange - Challenge譜面
        var songId = Guid.NewGuid();
        var level = new Level(19);

     // Act
   var chartId = new ChartIdentifier(songId, Difficulty.Challenge, level);

        // Assert
  Assert.Equal(songId, chartId.SongId);
        Assert.Equal(Difficulty.Challenge, chartId.Difficulty);
        Assert.Equal(19, chartId.Level.Value);
    }

    #endregion
}
