using GPScoreTracker.Domain.Entities;
using GPScoreTracker.Domain.Enums;
using GPScoreTracker.Domain.ValueObjects;
using Xunit;

namespace GPScoreTracker.Domain.Tests.Entities;

/// <summary>
/// Chart エンティティのテスト
/// </summary>
public class ChartTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_ValidValues_CreatesInstance()
    {
        // Arrange
        var chartId = Guid.NewGuid();
        var songId = Guid.NewGuid();
        var level = new Level(15);

        // Act
        var chart = new Domain.Entities.Chart(chartId, songId, Difficulty.Expert, level);

        // Assert
        Assert.Equal(chartId, chart.ChartId);
        Assert.Equal(songId, chart.SongId);
        Assert.Equal(Difficulty.Expert, chart.Difficulty);
        Assert.Equal(level, chart.Level);
    }

    [Fact]
    public void Constructor_NullLevel_ThrowsArgumentNullException()
    {
        // Arrange
        var chartId = Guid.NewGuid();
        var songId = Guid.NewGuid();

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => new GPScoreTracker.Domain.Entities.Chart(chartId, songId, Difficulty.Expert, null!));
        Assert.Equal("level", exception.ParamName);
    }

    #endregion

    #region Equality Tests

    [Fact]
    public void Equals_SameChartId_ReturnsTrue()
    {
        // Arrange
        var chartId = Guid.NewGuid();
        var songId = Guid.NewGuid();
        var level = new Level(15);
        var chart1 = new GPScoreTracker.Domain.Entities.Chart(chartId, songId, Difficulty.Expert, level);
        var chart2 = new GPScoreTracker.Domain.Entities.Chart(chartId, Guid.NewGuid(), Difficulty.Basic, new Level(10)); // 異なるプロパティ

        // Act & Assert
        Assert.Equal(chart1, chart2);
        Assert.True(chart1 == chart2);
        Assert.False(chart1 != chart2);
    }

    [Fact]
    public void Equals_DifferentChartId_ReturnsFalse()
    {
        // Arrange
        var chartId1 = Guid.NewGuid();
        var chartId2 = Guid.NewGuid();
        var songId = Guid.NewGuid();
        var level = new Level(15);
        var chart1 = new GPScoreTracker.Domain.Entities.Chart(chartId1, songId, Difficulty.Expert, level);
        var chart2 = new GPScoreTracker.Domain.Entities.Chart(chartId2, songId, Difficulty.Expert, level);

        // Act & Assert
        Assert.NotEqual(chart1, chart2);
        Assert.False(chart1 == chart2);
        Assert.True(chart1 != chart2);
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var chartId = Guid.NewGuid();
        var songId = Guid.NewGuid();
        var level = new Level(15);
        var chart = new GPScoreTracker.Domain.Entities.Chart(chartId, songId, Difficulty.Expert, level);

        // Act & Assert
        Assert.False(chart.Equals(null));
        Assert.False(chart == null);
        Assert.True(chart != null);
    }

    [Fact]
    public void Equals_SameInstance_ReturnsTrue()
    {
        // Arrange
        var chartId = Guid.NewGuid();
        var songId = Guid.NewGuid();
        var level = new Level(15);
        var chart = new GPScoreTracker.Domain.Entities.Chart(chartId, songId, Difficulty.Expert, level);

        // Act & Assert
#pragma warning disable CS1718 // 意図的に同じ変数を比較
        Assert.True(chart.Equals(chart));
        Assert.True(chart == chart);
#pragma warning restore CS1718
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameChartId_ReturnsSameHashCode()
    {
        // Arrange
        var chartId = Guid.NewGuid();
        var songId = Guid.NewGuid();
        var level = new Level(15);
        var chart1 = new GPScoreTracker.Domain.Entities.Chart(chartId, songId, Difficulty.Expert, level);
        var chart2 = new GPScoreTracker.Domain.Entities.Chart(chartId, Guid.NewGuid(), Difficulty.Basic, new Level(10));

        // Act & Assert
        Assert.Equal(chart1.GetHashCode(), chart2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentChartId_ReturnsDifferentHashCode()
    {
        // Arrange
        var chartId1 = Guid.NewGuid();
        var chartId2 = Guid.NewGuid();
        var songId = Guid.NewGuid();
        var level = new Level(15);
        var chart1 = new GPScoreTracker.Domain.Entities.Chart(chartId1, songId, Difficulty.Expert, level);
        var chart2 = new GPScoreTracker.Domain.Entities.Chart(chartId2, songId, Difficulty.Expert, level);

        // Act & Assert
        Assert.NotEqual(chart1.GetHashCode(), chart2.GetHashCode());
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_ReturnsCorrectFormat()
    {
        // Arrange
        var chartId = Guid.Parse("12345678-1234-1234-1234-123456789abc");
        var songId = Guid.Parse("87654321-4321-4321-4321-cba987654321");
        var level = new Level(15);
        var chart = new GPScoreTracker.Domain.Entities.Chart(chartId, songId, Difficulty.Expert, level);

        // Act
        var result = chart.ToString();

        // Assert
        Assert.Contains("ChartId:12345678-1234-1234-1234-123456789abc", result);
        Assert.Contains("SongId:87654321-4321-4321-4321-cba987654321", result);
        Assert.Contains("Difficulty:Expert", result);
        Assert.Contains("Level:15", result);
    }

    #endregion

    #region UpdateLevel Tests

    [Fact]
    public void UpdateLevel_WithValidLevel_UpdatesLevel()
    {
        // Arrange
        var chartId = Guid.NewGuid();
        var songId = Guid.NewGuid();
        var originalLevel = new Level(18);
        var chart = new GPScoreTracker.Domain.Entities.Chart(chartId, songId, Difficulty.Expert, originalLevel);

        var newLevel = new Level(19);

        // Act
        chart.UpdateLevel(newLevel);

        // Assert
        Assert.Equal(19, chart.Level.Value);
        Assert.Equal(newLevel, chart.Level);
    }

    [Fact]
    public void UpdateLevel_WithNullLevel_ThrowsArgumentNullException()
    {
        // Arrange
        var chartId = Guid.NewGuid();
        var songId = Guid.NewGuid();
        var level = new Level(18);
        var chart = new GPScoreTracker.Domain.Entities.Chart(chartId, songId, Difficulty.Expert, level);

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => chart.UpdateLevel(null!));
        Assert.Equal("newLevel", exception.ParamName);
    }

    [Fact]
    public void UpdateLevel_LowerLevel_UpdatesLevel()
    {
        // Arrange - レベルが下がる場合もある
        var chartId = Guid.NewGuid();
        var songId = Guid.NewGuid();
        var originalLevel = new Level(19);
        var chart = new GPScoreTracker.Domain.Entities.Chart(chartId, songId, Difficulty.Expert, originalLevel);

        var newLevel = new Level(18);

        // Act
        chart.UpdateLevel(newLevel);

        // Assert
        Assert.Equal(18, chart.Level.Value);
    }

    #endregion

    #region MarkAsDeleted Tests

    [Fact]
    public void MarkAsDeleted_SetsIsDeletedToTrue()
    {
        // Arrange
        var chartId = Guid.NewGuid();
        var songId = Guid.NewGuid();
        var level = new Level(15);
        var chart = new GPScoreTracker.Domain.Entities.Chart(chartId, songId, Difficulty.Expert, level);

        // Assert - 初期状態
        Assert.False(chart.IsDeleted);

        // Act
        chart.MarkAsDeleted();

        // Assert
        Assert.True(chart.IsDeleted);
    }

    [Fact]
    public void MarkAsDeleted_CalledTwice_RemainsDeleted()
    {
        // Arrange
        var chartId = Guid.NewGuid();
        var songId = Guid.NewGuid();
        var level = new Level(15);
        var chart = new GPScoreTracker.Domain.Entities.Chart(chartId, songId, Difficulty.Expert, level);

        // Act
        chart.MarkAsDeleted();
        chart.MarkAsDeleted(); // 2回目の呼び出し

        // Assert
        Assert.True(chart.IsDeleted);
    }

    [Fact]
    public void ToString_DeletedChart_ContainsDeletedFlag()
    {
        // Arrange
        var chartId = Guid.NewGuid();
        var songId = Guid.NewGuid();
        var level = new Level(15);
        var chart = new GPScoreTracker.Domain.Entities.Chart(chartId, songId, Difficulty.Expert, level);

        // Act
        chart.MarkAsDeleted();
        var result = chart.ToString();

        // Assert
        Assert.Contains("[Deleted]", result);
    }

    [Fact]
    public void Constructor_NewChart_IsNotDeleted()
    {
        // Arrange & Act
        var chartId = Guid.NewGuid();
        var songId = Guid.NewGuid();
        var level = new Level(15);
        var chart = new GPScoreTracker.Domain.Entities.Chart(chartId, songId, Difficulty.Expert, level);

        // Assert
        Assert.False(chart.IsDeleted);
    }

    #endregion

    #region Realistic Scenario Tests

    [Fact]
    public void Constructor_TypicalExpertChart_CreatesInstance()
    {
        // Arrange - 典型的なExpert譜面
        var chartId = Guid.NewGuid();
        var songId = Guid.NewGuid();
        var level = new Level(15);

        // Act
        var chart = new GPScoreTracker.Domain.Entities.Chart(chartId, songId, Difficulty.Expert, level);

        // Assert
        Assert.Equal(chartId, chart.ChartId);
        Assert.Equal(songId, chart.SongId);
        Assert.Equal(Difficulty.Expert, chart.Difficulty);
        Assert.Equal(15, chart.Level.Value);
    }

    [Fact]
    public void Constructor_BeginnerChart_CreatesInstance()
    {
        // Arrange - Beginner譜面
        var chartId = Guid.NewGuid();
        var songId = Guid.NewGuid();
        var level = new Level(3);

        // Act
        var chart = new GPScoreTracker.Domain.Entities.Chart(chartId, songId, Difficulty.Beginner, level);

        // Assert
        Assert.Equal(chartId, chart.ChartId);
        Assert.Equal(songId, chart.SongId);
        Assert.Equal(Difficulty.Beginner, chart.Difficulty);
        Assert.Equal(3, chart.Level.Value);
    }

    [Fact]
    public void Constructor_ChallengeChart_CreatesInstance()
    {
        // Arrange - Challenge譜面
        var chartId = Guid.NewGuid();
        var songId = Guid.NewGuid();
        var level = new Level(19);

        // Act
        var chart = new GPScoreTracker.Domain.Entities.Chart(chartId, songId, Difficulty.Challenge, level);

        // Assert
        Assert.Equal(chartId, chart.ChartId);
        Assert.Equal(songId, chart.SongId);
        Assert.Equal(Difficulty.Challenge, chart.Difficulty);
        Assert.Equal(19, chart.Level.Value);
    }

    #endregion
}