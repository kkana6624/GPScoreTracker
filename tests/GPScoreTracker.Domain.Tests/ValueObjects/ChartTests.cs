using GPScoreTracker.Domain.Enums;
using GPScoreTracker.Domain.ValueObjects;
using Xunit;

namespace GPScoreTracker.Domain.Tests.ValueObjects;

/// <summary>
/// Chart 値オブジェクトのテスト
/// </summary>
public class ChartTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_ValidValues_CreatesInstance()
    {
        // Arrange
        var songId = Guid.NewGuid();
        var level = new Level(15);

        // Act
        var chart = new Chart(songId, Difficulty.Expert, level);

        // Assert
        Assert.Equal(songId, chart.SongId);
        Assert.Equal(Difficulty.Expert, chart.Difficulty);
        Assert.Equal(level, chart.Level);
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
            var chart = new Chart(songId, difficulty, level);
            Assert.Equal(songId, chart.SongId);
            Assert.Equal(difficulty, chart.Difficulty);
            Assert.Equal(level, chart.Level);
        }
    }

    [Fact]
    public void Constructor_NullLevel_ThrowsArgumentNullException()
    {
        // Arrange
        var songId = Guid.NewGuid();

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => new Chart(songId, Difficulty.Expert, null!));
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
        var chart1 = new Chart(songId, Difficulty.Expert, level);
        var chart2 = new Chart(songId, Difficulty.Expert, level);

        // Act & Assert
        Assert.Equal(chart1, chart2);
        Assert.True(chart1 == chart2);
        Assert.False(chart1 != chart2);
    }

    [Fact]
    public void Equals_DifferentSongId_ReturnsFalse()
    {
        // Arrange
        var songId1 = Guid.NewGuid();
        var songId2 = Guid.NewGuid();
        var level = new Level(15);
        var chart1 = new Chart(songId1, Difficulty.Expert, level);
        var chart2 = new Chart(songId2, Difficulty.Expert, level);

        // Act & Assert
        Assert.NotEqual(chart1, chart2);
        Assert.False(chart1 == chart2);
        Assert.True(chart1 != chart2);
    }

    [Fact]
    public void Equals_DifferentDifficulty_ReturnsFalse()
    {
        // Arrange
        var songId = Guid.NewGuid();
        var level = new Level(15);
        var chart1 = new Chart(songId, Difficulty.Expert, level);
        var chart2 = new Chart(songId, Difficulty.Challenge, level);

        // Act & Assert
        Assert.NotEqual(chart1, chart2);
    }

    [Fact]
    public void Equals_DifferentLevel_ReturnsFalse()
    {
        // Arrange
        var songId = Guid.NewGuid();
        var level1 = new Level(15);
        var level2 = new Level(16);
        var chart1 = new Chart(songId, Difficulty.Expert, level1);
        var chart2 = new Chart(songId, Difficulty.Expert, level2);

        // Act & Assert
        Assert.NotEqual(chart1, chart2);
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var songId = Guid.NewGuid();
        var level = new Level(15);
        var chart = new Chart(songId, Difficulty.Expert, level);

        // Act & Assert
        Assert.False(chart.Equals(null));
        Assert.False(chart == null);
        Assert.True(chart != null);
    }

    [Fact]
    public void Equals_SameInstance_ReturnsTrue()
    {
        // Arrange
        var songId = Guid.NewGuid();
        var level = new Level(15);
        var chart = new Chart(songId, Difficulty.Expert, level);

        // Act & Assert
#pragma warning disable CS1718 // 意図的に同じ変数を比較
        Assert.True(chart.Equals(chart));
        Assert.True(chart == chart);
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
        var chart1 = new Chart(songId, Difficulty.Expert, level);
        var chart2 = new Chart(songId, Difficulty.Expert, level);

        // Act & Assert
        Assert.Equal(chart1.GetHashCode(), chart2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentValues_ReturnsDifferentHashCode()
    {
        // Arrange
        var songId1 = Guid.NewGuid();
        var songId2 = Guid.NewGuid();
        var level = new Level(15);
        var chart1 = new Chart(songId1, Difficulty.Expert, level);
        var chart2 = new Chart(songId2, Difficulty.Expert, level);

        // Act & Assert
        Assert.NotEqual(chart1.GetHashCode(), chart2.GetHashCode());
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_ReturnsCorrectFormat()
    {
        // Arrange
        var songId = Guid.Parse("12345678-1234-1234-1234-123456789abc");
        var level = new Level(15);
        var chart = new Chart(songId, Difficulty.Expert, level);

        // Act
        var result = chart.ToString();

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
        var chart = new Chart(songId, Difficulty.Expert, level);

        // Assert
        Assert.Equal(songId, chart.SongId);
        Assert.Equal(Difficulty.Expert, chart.Difficulty);
        Assert.Equal(15, chart.Level.Value);
    }

    [Fact]
    public void Constructor_BeginnerChart_CreatesInstance()
    {
        // Arrange - Beginner譜面
        var songId = Guid.NewGuid();
        var level = new Level(3);

        // Act
        var chart = new Chart(songId, Difficulty.Beginner, level);

        // Assert
        Assert.Equal(songId, chart.SongId);
        Assert.Equal(Difficulty.Beginner, chart.Difficulty);
        Assert.Equal(3, chart.Level.Value);
    }

    [Fact]
    public void Constructor_ChallengeChart_CreatesInstance()
    {
        // Arrange - Challenge譜面
        var songId = Guid.NewGuid();
        var level = new Level(19);

        // Act
        var chart = new Chart(songId, Difficulty.Challenge, level);

        // Assert
        Assert.Equal(songId, chart.SongId);
        Assert.Equal(Difficulty.Challenge, chart.Difficulty);
        Assert.Equal(19, chart.Level.Value);
    }

    #endregion
}