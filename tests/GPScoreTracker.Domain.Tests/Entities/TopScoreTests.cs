using GPScoreTracker.Domain.Entities;
using GPScoreTracker.Domain.Enums;
using GPScoreTracker.Domain.ValueObjects;
using Xunit;

namespace GPScoreTracker.Domain.Tests.Entities;

/// <summary>
/// TopScoreエンティティのテストクラス
/// </summary>
public class TopScoreTests
{
    #region Test Data Helpers

    private static ChartIdentifier CreateValidChartIdentifier()
    {
        var songId = Guid.NewGuid();
        var difficulty = Difficulty.Expert;
        var level = new Level(15);
        return new ChartIdentifier(songId, difficulty, level);
    }

    private static Score CreateValidScore(int points = 950000)
    {
        var judgements = new Judgements(
            marvelous: 400,
            perfect: 50,
            great: 10,
            good: 5,
            miss: 0
        );
        return new Score(
            points: points,
            exScore: 1800,
            rank: Rank.AAA,
            judgements: judgements,
            maxCombo: 450,
            clearType: ClearType.FullCombo
        );
    }

    #endregion

    #region Constructor Tests

    [Fact]
    public void Constructor_WithValidParameters_CreatesInstance()
    {
        // Arrange
        var topScoreId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartIdentifier = CreateValidChartIdentifier();
        var score = CreateValidScore();
        var achievedAt = DateTime.UtcNow;

        // Act
        var topScore = new TopScore(
            topScoreId,
            userProfileId,
            chartIdentifier,
            score,
            achievedAt
        );

        // Assert
        Assert.Equal(topScoreId, topScore.TopScoreId);
        Assert.Equal(userProfileId, topScore.UserProfileId);
        Assert.Equal(chartIdentifier, topScore.ChartIdentifier);
        Assert.Equal(score, topScore.Score);
        Assert.Equal(achievedAt, topScore.AchievedAt);
    }

    [Fact]
    public void Constructor_WithNullChartIdentifier_ThrowsArgumentNullException()
    {
        // Arrange
        var topScoreId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        ChartIdentifier? chartIdentifier = null;
        var score = CreateValidScore();
        var achievedAt = DateTime.UtcNow;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new TopScore(topScoreId, userProfileId, chartIdentifier!, score, achievedAt)
        );
        Assert.Equal("chartIdentifier", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithNullScore_ThrowsArgumentNullException()
    {
        // Arrange
        var topScoreId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartIdentifier = CreateValidChartIdentifier();
        Score? score = null;
        var achievedAt = DateTime.UtcNow;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new TopScore(topScoreId, userProfileId, chartIdentifier, score!, achievedAt)
        );
        Assert.Equal("score", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithEmptyTopScoreId_CreatesInstance()
    {
        // Arrange
        var topScoreId = Guid.Empty;
        var userProfileId = Guid.NewGuid();
        var chartIdentifier = CreateValidChartIdentifier();
        var score = CreateValidScore();
        var achievedAt = DateTime.UtcNow;

        // Act
        var topScore = new TopScore(
            topScoreId,
            userProfileId,
            chartIdentifier,
            score,
            achievedAt
        );

        // Assert
        Assert.Equal(Guid.Empty, topScore.TopScoreId);
    }

    [Fact]
    public void Constructor_WithEmptyUserProfileId_CreatesInstance()
    {
        // Arrange
        var topScoreId = Guid.NewGuid();
        var userProfileId = Guid.Empty;
        var chartIdentifier = CreateValidChartIdentifier();
        var score = CreateValidScore();
        var achievedAt = DateTime.UtcNow;

        // Act
        var topScore = new TopScore(
            topScoreId,
            userProfileId,
            chartIdentifier,
            score,
            achievedAt
        );

        // Assert
        Assert.Equal(Guid.Empty, topScore.UserProfileId);
    }

    #endregion

    #region TryUpdateWith Tests

    [Fact]
    public void TryUpdateWith_WithHigherScore_ReturnsTrueAndUpdatesScore()
    {
        // Arrange
        var topScoreId = Guid.NewGuid();
        var originalUserId = Guid.NewGuid();
        var chartIdentifier = CreateValidChartIdentifier();
        var originalScore = CreateValidScore(900000);
        var originalAchievedAt = DateTime.UtcNow.AddDays(-1);

        var topScore = new TopScore(
            topScoreId,
            originalUserId,
            chartIdentifier,
            originalScore,
            originalAchievedAt
        );

        var newUserId = Guid.NewGuid();
        var newScore = CreateValidScore(950000);
        var newAchievedAt = DateTime.UtcNow;

        // Act
        var result = topScore.TryUpdateWith(newScore, newUserId, newAchievedAt);

        // Assert
        Assert.True(result);
        Assert.Equal(newScore, topScore.Score);
        Assert.Equal(newUserId, topScore.UserProfileId);
        Assert.Equal(newAchievedAt, topScore.AchievedAt);
    }

    [Fact]
    public void TryUpdateWith_WithLowerScore_ReturnsFalseAndDoesNotUpdate()
    {
        // Arrange
        var topScoreId = Guid.NewGuid();
        var originalUserId = Guid.NewGuid();
        var chartIdentifier = CreateValidChartIdentifier();
        var originalScore = CreateValidScore(950000);
        var originalAchievedAt = DateTime.UtcNow.AddDays(-1);

        var topScore = new TopScore(
            topScoreId,
            originalUserId,
            chartIdentifier,
            originalScore,
            originalAchievedAt
        );

        var newUserId = Guid.NewGuid();
        var newScore = CreateValidScore(900000);
        var newAchievedAt = DateTime.UtcNow;

        // Act
        var result = topScore.TryUpdateWith(newScore, newUserId, newAchievedAt);

        // Assert
        Assert.False(result);
        Assert.Equal(originalScore, topScore.Score);
        Assert.Equal(originalUserId, topScore.UserProfileId);
        Assert.Equal(originalAchievedAt, topScore.AchievedAt);
    }

    [Fact]
    public void TryUpdateWith_WithEqualScore_ReturnsFalseAndDoesNotUpdate()
    {
        // Arrange
        var topScoreId = Guid.NewGuid();
        var originalUserId = Guid.NewGuid();
        var chartIdentifier = CreateValidChartIdentifier();
        var originalScore = CreateValidScore(950000);
        var originalAchievedAt = DateTime.UtcNow.AddDays(-1);

        var topScore = new TopScore(
            topScoreId,
            originalUserId,
            chartIdentifier,
            originalScore,
            originalAchievedAt
        );

        var newUserId = Guid.NewGuid();
        var newScore = CreateValidScore(950000);
        var newAchievedAt = DateTime.UtcNow;

        // Act
        var result = topScore.TryUpdateWith(newScore, newUserId, newAchievedAt);

        // Assert
        Assert.False(result);
        Assert.Equal(originalScore, topScore.Score);
        Assert.Equal(originalUserId, topScore.UserProfileId);
        Assert.Equal(originalAchievedAt, topScore.AchievedAt);
    }

    [Fact]
    public void TryUpdateWith_WithNullScore_ThrowsArgumentNullException()
    {
        // Arrange
        var topScoreId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartIdentifier = CreateValidChartIdentifier();
        var score = CreateValidScore();
        var achievedAt = DateTime.UtcNow;

        var topScore = new TopScore(
            topScoreId,
            userProfileId,
            chartIdentifier,
            score,
            achievedAt
        );

        var newUserId = Guid.NewGuid();
        Score? newScore = null;
        var newAchievedAt = DateTime.UtcNow;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() =>
            topScore.TryUpdateWith(newScore!, newUserId, newAchievedAt)
        );
        Assert.Equal("newScore", exception.ParamName);
    }

    [Fact]
    public void TryUpdateWith_WithSameUserButHigherScore_UpdatesScoreAndKeepsUser()
    {
        // Arrange
        var topScoreId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var chartIdentifier = CreateValidChartIdentifier();
        var originalScore = CreateValidScore(900000);
        var originalAchievedAt = DateTime.UtcNow.AddDays(-1);

        var topScore = new TopScore(
            topScoreId,
            userId,
            chartIdentifier,
            originalScore,
            originalAchievedAt
        );

        var newScore = CreateValidScore(950000);
        var newAchievedAt = DateTime.UtcNow;

        // Act
        var result = topScore.TryUpdateWith(newScore, userId, newAchievedAt);

        // Assert
        Assert.True(result);
        Assert.Equal(newScore, topScore.Score);
        Assert.Equal(userId, topScore.UserProfileId);
        Assert.Equal(newAchievedAt, topScore.AchievedAt);
    }

    #endregion

    #region Equals and GetHashCode Tests

    [Fact]
    public void Equals_WithSameTopScoreId_ReturnsTrue()
    {
        // Arrange
        var topScoreId = Guid.NewGuid();
        var userProfileId1 = Guid.NewGuid();
        var userProfileId2 = Guid.NewGuid();
        var chartIdentifier1 = CreateValidChartIdentifier();
        var chartIdentifier2 = CreateValidChartIdentifier();
        var score1 = CreateValidScore(900000);
        var score2 = CreateValidScore(950000);

        var topScore1 = new TopScore(topScoreId, userProfileId1, chartIdentifier1, score1, DateTime.UtcNow);
        var topScore2 = new TopScore(topScoreId, userProfileId2, chartIdentifier2, score2, DateTime.UtcNow);

        // Act & Assert
        Assert.True(topScore1.Equals(topScore2));
        Assert.True(topScore1 == topScore2);
        Assert.False(topScore1 != topScore2);
    }

    [Fact]
    public void Equals_WithDifferentTopScoreId_ReturnsFalse()
    {
        // Arrange
        var topScoreId1 = Guid.NewGuid();
        var topScoreId2 = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartIdentifier = CreateValidChartIdentifier();
        var score = CreateValidScore();

        var topScore1 = new TopScore(topScoreId1, userProfileId, chartIdentifier, score, DateTime.UtcNow);
        var topScore2 = new TopScore(topScoreId2, userProfileId, chartIdentifier, score, DateTime.UtcNow);

        // Act & Assert
        Assert.False(topScore1.Equals(topScore2));
        Assert.False(topScore1 == topScore2);
        Assert.True(topScore1 != topScore2);
    }

    [Fact]
    public void Equals_WithNull_ReturnsFalse()
    {
        // Arrange
        var topScoreId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartIdentifier = CreateValidChartIdentifier();
        var score = CreateValidScore();

        var topScore = new TopScore(topScoreId, userProfileId, chartIdentifier, score, DateTime.UtcNow);

        // Act & Assert
        Assert.False(topScore.Equals(null));
        Assert.False(topScore == null);
        Assert.True(topScore != null);
    }

    [Fact]
    public void Equals_WithSameReference_ReturnsTrue()
    {
        // Arrange
        var topScoreId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartIdentifier = CreateValidChartIdentifier();
        var score = CreateValidScore();

        var topScore = new TopScore(topScoreId, userProfileId, chartIdentifier, score, DateTime.UtcNow);

        // Act & Assert
        Assert.True(topScore.Equals(topScore));
        Assert.True(topScore == topScore);
    }

    [Fact]
    public void GetHashCode_WithSameTopScoreId_ReturnsSameHashCode()
    {
        // Arrange
        var topScoreId = Guid.NewGuid();
        var userProfileId1 = Guid.NewGuid();
        var userProfileId2 = Guid.NewGuid();
        var chartIdentifier1 = CreateValidChartIdentifier();
        var chartIdentifier2 = CreateValidChartIdentifier();
        var score1 = CreateValidScore(900000);
        var score2 = CreateValidScore(950000);

        var topScore1 = new TopScore(topScoreId, userProfileId1, chartIdentifier1, score1, DateTime.UtcNow);
        var topScore2 = new TopScore(topScoreId, userProfileId2, chartIdentifier2, score2, DateTime.UtcNow);

        // Act & Assert
        Assert.Equal(topScore1.GetHashCode(), topScore2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_WithDifferentTopScoreId_ReturnsDifferentHashCode()
    {
        // Arrange
        var topScoreId1 = Guid.NewGuid();
        var topScoreId2 = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartIdentifier = CreateValidChartIdentifier();
        var score = CreateValidScore();

        var topScore1 = new TopScore(topScoreId1, userProfileId, chartIdentifier, score, DateTime.UtcNow);
        var topScore2 = new TopScore(topScoreId2, userProfileId, chartIdentifier, score, DateTime.UtcNow);

        // Act & Assert
        Assert.NotEqual(topScore1.GetHashCode(), topScore2.GetHashCode());
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_ReturnsFormattedString()
    {
        // Arrange
        var topScoreId = Guid.NewGuid();
        var userProfileId = Guid.NewGuid();
        var chartIdentifier = CreateValidChartIdentifier();
        var score = CreateValidScore(987654);
        var achievedAt = new DateTime(2025, 1, 15, 10, 30, 45, DateTimeKind.Utc);

        var topScore = new TopScore(topScoreId, userProfileId, chartIdentifier, score, achievedAt);

        // Act
        var result = topScore.ToString();

        // Assert
        Assert.Contains($"TopScoreId:{topScoreId}", result);
        Assert.Contains($"UserProfileId:{userProfileId}", result);
        Assert.Contains("Score:987654", result);
        Assert.Contains("AchievedAt:2025/01/15 10:30:45", result);
    }

    #endregion
}
