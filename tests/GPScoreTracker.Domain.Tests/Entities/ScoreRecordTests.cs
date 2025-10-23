using GPScoreTracker.Domain.Entities;
using GPScoreTracker.Domain.ValueObjects;
using Xunit;

namespace GPScoreTracker.Domain.Tests.Entities;

/// <summary>
/// ScoreRecord �G���e�B�e�B�̃e�X�g
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
        var chart = new GPScoreTracker.Domain.Entities.Chart(
            Guid.NewGuid(), Guid.NewGuid(), GPScoreTracker.Domain.Enums.Difficulty.Expert, new Level(15));
        var score = new Score(980000, 2850, GPScoreTracker.Domain.Enums.Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, GPScoreTracker.Domain.Enums.ClearType.Cleared);
        var playedAt = DateTime.UtcNow;

        // Act
        var scoreRecord = new ScoreRecord(scoreRecordId, userProfileId, chart, score, playedAt);

        // Assert
        Assert.Equal(scoreRecordId, scoreRecord.ScoreRecordId);
        Assert.Equal(userProfileId, scoreRecord.UserProfileId);
        Assert.Equal(chart, scoreRecord.Chart);
        Assert.Equal(score, scoreRecord.Score);
        Assert.Equal(playedAt, scoreRecord.PlayedAt);
    }

    [Fact]
    public void Constructor_NullChart_ThrowsArgumentNullException()
    {
        // Arrange
        var scoreRecordId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var score = new Score(980000, 2850, GPScoreTracker.Domain.Enums.Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, GPScoreTracker.Domain.Enums.ClearType.Cleared);
        var playedAt = DateTime.UtcNow;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => new ScoreRecord(scoreRecordId, userProfileId, null!, score, playedAt));
        Assert.Equal("chart", exception.ParamName);
    }

    [Fact]
    public void Constructor_NullScore_ThrowsArgumentNullException()
    {
        // Arrange
        var scoreRecordId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chart = new GPScoreTracker.Domain.Entities.Chart(
            Guid.NewGuid(), Guid.NewGuid(), GPScoreTracker.Domain.Enums.Difficulty.Expert, new Level(15));
        var playedAt = DateTime.UtcNow;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => new ScoreRecord(scoreRecordId, userProfileId, chart, null!, playedAt));
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
        var chart = new GPScoreTracker.Domain.Entities.Chart(
            Guid.NewGuid(), Guid.NewGuid(), GPScoreTracker.Domain.Enums.Difficulty.Expert, new Level(15));
        var score = new Score(980000, 2850, GPScoreTracker.Domain.Enums.Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, GPScoreTracker.Domain.Enums.ClearType.Cleared);
        var playedAt = DateTime.UtcNow;

        var scoreRecord1 = new ScoreRecord(scoreRecordId, userProfileId, chart, score, playedAt);
        var scoreRecord2 = new ScoreRecord(scoreRecordId, Guid.NewGuid(),
            new GPScoreTracker.Domain.Entities.Chart(Guid.NewGuid(), Guid.NewGuid(),
                GPScoreTracker.Domain.Enums.Difficulty.Basic, new Level(10)),
            new Score(950000, 2800, GPScoreTracker.Domain.Enums.Rank.AA,
                new Judgements(400, 50, 40, 5, 5), 480, GPScoreTracker.Domain.Enums.ClearType.Cleared),
            DateTime.UtcNow.AddHours(-1)); // �قȂ�v���p�e�B

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
        var chart = new GPScoreTracker.Domain.Entities.Chart(
            Guid.NewGuid(), Guid.NewGuid(), GPScoreTracker.Domain.Enums.Difficulty.Expert, new Level(15));
        var score = new Score(980000, 2850, GPScoreTracker.Domain.Enums.Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, GPScoreTracker.Domain.Enums.ClearType.Cleared);
        var playedAt = DateTime.UtcNow;

        var scoreRecord1 = new ScoreRecord(scoreRecordId1, userProfileId, chart, score, playedAt);
        var scoreRecord2 = new ScoreRecord(scoreRecordId2, userProfileId, chart, score, playedAt);

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
        var chart = new GPScoreTracker.Domain.Entities.Chart(
            Guid.NewGuid(), Guid.NewGuid(), GPScoreTracker.Domain.Enums.Difficulty.Expert, new Level(15));
        var score = new Score(980000, 2850, GPScoreTracker.Domain.Enums.Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, GPScoreTracker.Domain.Enums.ClearType.Cleared);
        var playedAt = DateTime.UtcNow;

        var scoreRecord = new ScoreRecord(scoreRecordId, userProfileId, chart, score, playedAt);

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
        var chart = new GPScoreTracker.Domain.Entities.Chart(
            Guid.NewGuid(), Guid.NewGuid(), GPScoreTracker.Domain.Enums.Difficulty.Expert, new Level(15));
        var score = new Score(980000, 2850, GPScoreTracker.Domain.Enums.Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, GPScoreTracker.Domain.Enums.ClearType.Cleared);
        var playedAt = DateTime.UtcNow;

        var scoreRecord = new ScoreRecord(scoreRecordId, userProfileId, chart, score, playedAt);

        // Act & Assert
#pragma warning disable CS1718 // �Ӑ}�I�ɓ����ϐ����r
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
        var chart = new GPScoreTracker.Domain.Entities.Chart(
            Guid.NewGuid(), Guid.NewGuid(), GPScoreTracker.Domain.Enums.Difficulty.Expert, new Level(15));
        var score = new Score(980000, 2850, GPScoreTracker.Domain.Enums.Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, GPScoreTracker.Domain.Enums.ClearType.Cleared);
        var playedAt = DateTime.UtcNow;

        var scoreRecord1 = new ScoreRecord(scoreRecordId, userProfileId, chart, score, playedAt);
        var scoreRecord2 = new ScoreRecord(scoreRecordId, Guid.NewGuid(),
            new GPScoreTracker.Domain.Entities.Chart(Guid.NewGuid(), Guid.NewGuid(),
                GPScoreTracker.Domain.Enums.Difficulty.Basic, new Level(10)),
            new Score(950000, 2800, GPScoreTracker.Domain.Enums.Rank.AA,
                new Judgements(400, 50, 40, 5, 5), 480, GPScoreTracker.Domain.Enums.ClearType.Cleared),
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
        var chart = new GPScoreTracker.Domain.Entities.Chart(
            Guid.NewGuid(), Guid.NewGuid(), GPScoreTracker.Domain.Enums.Difficulty.Expert, new Level(15));
        var score = new Score(980000, 2850, GPScoreTracker.Domain.Enums.Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, GPScoreTracker.Domain.Enums.ClearType.Cleared);
        var playedAt = DateTime.UtcNow;

        var scoreRecord1 = new ScoreRecord(scoreRecordId1, userProfileId, chart, score, playedAt);
        var scoreRecord2 = new ScoreRecord(scoreRecordId2, userProfileId, chart, score, playedAt);

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
        var chart = new GPScoreTracker.Domain.Entities.Chart(
            Guid.NewGuid(), Guid.NewGuid(), GPScoreTracker.Domain.Enums.Difficulty.Expert, new Level(15));
        var score = new Score(980000, 2850, GPScoreTracker.Domain.Enums.Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, GPScoreTracker.Domain.Enums.ClearType.Cleared);
        var playedAt = new DateTime(2025, 1, 15, 12, 30, 45, DateTimeKind.Utc);

        var scoreRecord = new ScoreRecord(scoreRecordId, userProfileId, chart, score, playedAt);

        // Act
        var result = scoreRecord.ToString();

        // Assert - �܂���{�I�Ȋm�F
        Assert.Contains("ScoreRecordId:12345678-1234-1234-1234-123456789abc", result);
        Assert.Contains("UserProfileId:87654321-4321-4321-4321-cba987654321", result);
        Assert.Contains("PlayedAt:2025/01/15 12:30:45", result);
    }

    #endregion

    #region Realistic Scenario Tests

    [Fact]
    public void Constructor_TypicalScoreRecord_CreatesInstance()
    {
        // Arrange - �T�^�I�ȃX�R�A�L�^
        var scoreRecordId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chart = new GPScoreTracker.Domain.Entities.Chart(
            Guid.NewGuid(), Guid.NewGuid(), GPScoreTracker.Domain.Enums.Difficulty.Expert, new Level(15));
        var score = new Score(980000, 2850, GPScoreTracker.Domain.Enums.Rank.AAPlus,
            new Judgements(450, 30, 15, 3, 2), 495, GPScoreTracker.Domain.Enums.ClearType.Cleared);
        var playedAt = DateTime.UtcNow;

        // Act
        var scoreRecord = new ScoreRecord(scoreRecordId, userProfileId, chart, score, playedAt);

        // Assert
        Assert.Equal(scoreRecordId, scoreRecord.ScoreRecordId);
        Assert.Equal(userProfileId, scoreRecord.UserProfileId);
        Assert.Equal(GPScoreTracker.Domain.Enums.Difficulty.Expert, scoreRecord.Chart.Difficulty);
        Assert.Equal(15, scoreRecord.Chart.Level.Value);
        Assert.Equal(980000, scoreRecord.Score.Points);
        Assert.Equal(GPScoreTracker.Domain.Enums.Rank.AAPlus, scoreRecord.Score.Rank);
    }

    [Fact]
    public void Constructor_FailedScoreRecord_CreatesInstance()
    {
        // Arrange - ���s�����X�R�A�L�^
        var scoreRecordId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chart = new GPScoreTracker.Domain.Entities.Chart(
            Guid.NewGuid(), Guid.NewGuid(), GPScoreTracker.Domain.Enums.Difficulty.Challenge, new Level(18));
        var score = new Score(650000, 1200, GPScoreTracker.Domain.Enums.Rank.E,
            new Judgements(200, 100, 80, 50, 70), 200, GPScoreTracker.Domain.Enums.ClearType.Failed);
        var playedAt = DateTime.UtcNow.AddHours(-2);

        // Act
        var scoreRecord = new ScoreRecord(scoreRecordId, userProfileId, chart, score, playedAt);

        // Assert
        Assert.Equal(scoreRecordId, scoreRecord.ScoreRecordId);
        Assert.Equal(GPScoreTracker.Domain.Enums.Difficulty.Challenge, scoreRecord.Chart.Difficulty);
        Assert.Equal(18, scoreRecord.Chart.Level.Value);
        Assert.Equal(650000, scoreRecord.Score.Points);
        Assert.Equal(GPScoreTracker.Domain.Enums.Rank.E, scoreRecord.Score.Rank);
        Assert.Equal(GPScoreTracker.Domain.Enums.ClearType.Failed, scoreRecord.Score.ClearType);
    }

    #endregion
}