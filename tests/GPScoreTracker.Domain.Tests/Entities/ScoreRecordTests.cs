using GPScoreTracker.Domain.Entities;
using GPScoreTracker.Domain.Enums;
using GPScoreTracker.Domain.ValueObjects;
using Xunit;

namespace GPScoreTracker.Domain.Tests.Entities;

/// <summary>
/// ScoreRecord エンティティのテスト
/// </summary>
public class ScoreRecordTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_ValidValues_CreatesInstance()
    {
        // Arrange
        var scoreRecordId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartId = Guid.NewGuid();
        var score = new Score(980000, 2850, Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, ClearType.Cleared);
        var playedAt = DateTime.UtcNow;

        // Act
        var scoreRecord = new ScoreRecord(scoreRecordId, userProfileId, chartId, score, playedAt);

        // Assert
        Assert.Equal(scoreRecordId, scoreRecord.ScoreRecordId);
        Assert.Equal(userProfileId, scoreRecord.UserProfileId);
        Assert.Equal(chartId, scoreRecord.ChartId);
        Assert.Equal(score, scoreRecord.Score);
        Assert.Equal(playedAt, scoreRecord.PlayedAt);
    }

    [Fact]
    public void Constructor_NullScore_ThrowsArgumentNullException()
    {
        // Arrange
        var scoreRecordId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartId = Guid.NewGuid();
        var playedAt = DateTime.UtcNow;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => new ScoreRecord(scoreRecordId, userProfileId, chartId, null!, playedAt));
        Assert.Equal("score", exception.ParamName);
    }

    #endregion

    #region Equality Tests

    [Fact]
    public void Equals_SameScoreRecordId_ReturnsTrue()
    {
        // Arrange
        var scoreRecordId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartId = Guid.NewGuid();
        var score = new Score(980000, 2850, Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, ClearType.Cleared);
        var playedAt = DateTime.UtcNow;

        var scoreRecord1 = new ScoreRecord(scoreRecordId, userProfileId, chartId, score, playedAt);
        var scoreRecord2 = new ScoreRecord(scoreRecordId, Guid.NewGuid(),
            Guid.NewGuid(), // 異なるChartId
            new Score(950000, 2800, Rank.AA,
                new Judgements(400, 50, 40, 5, 5), 480, ClearType.Cleared),
            DateTime.UtcNow.AddHours(-1)); // 異なるプロパティ

        // Act & Assert
        Assert.Equal(scoreRecord1, scoreRecord2);
        Assert.True(scoreRecord1 == scoreRecord2);
        Assert.False(scoreRecord1 != scoreRecord2);
    }

    [Fact]
    public void Equals_DifferentScoreRecordId_ReturnsFalse()
    {
        // Arrange
        var scoreRecordId1 = Guid.NewGuid();
        var scoreRecordId2 = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartId = Guid.NewGuid();
        var score = new Score(980000, 2850, Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, ClearType.Cleared);
        var playedAt = DateTime.UtcNow;

        var scoreRecord1 = new ScoreRecord(scoreRecordId1, userProfileId, chartId, score, playedAt);
        var scoreRecord2 = new ScoreRecord(scoreRecordId2, userProfileId, chartId, score, playedAt);

        // Act & Assert
        Assert.NotEqual(scoreRecord1, scoreRecord2);
        Assert.False(scoreRecord1 == scoreRecord2);
        Assert.True(scoreRecord1 != scoreRecord2);
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var scoreRecordId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartId = Guid.NewGuid();
        var score = new Score(980000, 2850, Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, ClearType.Cleared);
        var playedAt = DateTime.UtcNow;

        var scoreRecord = new ScoreRecord(scoreRecordId, userProfileId, chartId, score, playedAt);

        // Act & Assert
        Assert.False(scoreRecord.Equals(null));
        Assert.False(scoreRecord == null);
        Assert.True(scoreRecord != null);
    }

    [Fact]
    public void Equals_SameInstance_ReturnsTrue()
    {
        // Arrange
        var scoreRecordId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartId = Guid.NewGuid();
        var score = new Score(980000, 2850, Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, ClearType.Cleared);
        var playedAt = DateTime.UtcNow;

        var scoreRecord = new ScoreRecord(scoreRecordId, userProfileId, chartId, score, playedAt);

        // Act & Assert
#pragma warning disable CS1718 // 意図的に同一変数を比較
        Assert.True(scoreRecord.Equals(scoreRecord));
        Assert.True(scoreRecord == scoreRecord);
#pragma warning restore CS1718
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameScoreRecordId_ReturnsSameHashCode()
    {
        // Arrange
        var scoreRecordId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartId = Guid.NewGuid();
        var score = new Score(980000, 2850, Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, ClearType.Cleared);
        var playedAt = DateTime.UtcNow;

        var scoreRecord1 = new ScoreRecord(scoreRecordId, userProfileId, chartId, score, playedAt);
        var scoreRecord2 = new ScoreRecord(scoreRecordId, Guid.NewGuid(),
            Guid.NewGuid(),
            new Score(950000, 2800, Rank.AA,
                new Judgements(400, 50, 40, 5, 5), 480, ClearType.Cleared),
            DateTime.UtcNow.AddHours(-1));

        // Act & Assert
        Assert.Equal(scoreRecord1.GetHashCode(), scoreRecord2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentScoreRecordId_ReturnsDifferentHashCode()
    {
        // Arrange
        var scoreRecordId1 = Guid.NewGuid();
        var scoreRecordId2 = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartId = Guid.NewGuid();
        var score = new Score(980000, 2850, Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, ClearType.Cleared);
        var playedAt = DateTime.UtcNow;

        var scoreRecord1 = new ScoreRecord(scoreRecordId1, userProfileId, chartId, score, playedAt);
        var scoreRecord2 = new ScoreRecord(scoreRecordId2, userProfileId, chartId, score, playedAt);

        // Act & Assert
        Assert.NotEqual(scoreRecord1.GetHashCode(), scoreRecord2.GetHashCode());
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_ReturnsCorrectFormat()
    {
        // Arrange
        var scoreRecordId = Guid.Parse("12345678-1234-1234-1234-123456789abc");
        var userProfileId = Guid.Parse("87654321-4321-4321-4321-cba987654321");
        var chartId = Guid.Parse("11111111-2222-3333-4444-555555555555");
        var score = new Score(980000, 2850, Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, ClearType.Cleared);
        var playedAt = new DateTime(2025, 1, 15, 12, 30, 45, DateTimeKind.Utc);

        var scoreRecord = new ScoreRecord(scoreRecordId, userProfileId, chartId, score, playedAt);

        // Act
        var result = scoreRecord.ToString();

        // Assert
        Assert.Contains("ScoreRecordId:12345678-1234-1234-1234-123456789abc", result);
        Assert.Contains("UserProfileId:87654321-4321-4321-4321-cba987654321", result);
        Assert.Contains("ChartId:11111111-2222-3333-4444-555555555555", result);
        Assert.Contains("PlayedAt:2025/01/15 12:30:45", result);
    }

    #endregion

    #region Realistic Scenario Tests

    [Fact]
    public void Constructor_TypicalScoreRecord_CreatesInstance()
    {
        // Arrange - 典型的なスコア記録
        var scoreRecordId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartId = Guid.NewGuid();
        var score = new Score(980000, 2850, Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, ClearType.Cleared);
        var playedAt = DateTime.UtcNow;

        // Act
        var scoreRecord = new ScoreRecord(scoreRecordId, userProfileId, chartId, score, playedAt);

        // Assert
        Assert.Equal(scoreRecordId, scoreRecord.ScoreRecordId);
        Assert.Equal(userProfileId, scoreRecord.UserProfileId);
        Assert.Equal(chartId, scoreRecord.ChartId);
        Assert.Equal(980000, scoreRecord.Score.Points);
        Assert.Equal(Rank.AAPlus, scoreRecord.Score.Rank);
    }

    [Fact]
    public void Constructor_FailedScoreRecord_CreatesInstance()
    {
        // Arrange - 失敗したスコア記録
        var scoreRecordId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartId = Guid.NewGuid();
        var score = new Score(650000, 1200, Rank.E,
            new Judgements(200, 100, 80, 50, 70), 200, ClearType.Failed);
        var playedAt = DateTime.UtcNow.AddHours(-2);

        // Act
        var scoreRecord = new ScoreRecord(scoreRecordId, userProfileId, chartId, score, playedAt);

        // Assert
        Assert.Equal(scoreRecordId, scoreRecord.ScoreRecordId);
        Assert.Equal(chartId, scoreRecord.ChartId);
        Assert.Equal(650000, scoreRecord.Score.Points);
        Assert.Equal(Rank.E, scoreRecord.Score.Rank);
        Assert.Equal(ClearType.Failed, scoreRecord.Score.ClearType);
    }

    #endregion
}