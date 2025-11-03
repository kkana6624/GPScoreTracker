using GPScoreTracker.Domain.Entities;
using GPScoreTracker.Domain.Enums;
using GPScoreTracker.Domain.ValueObjects;
using Xunit;

namespace GPScoreTracker.Domain.Tests.Entities;

/// <summary>
/// PersonalHighScore エンティティのテスト
/// </summary>
public class PersonalHighScoreTests
{
    #region Test Helpers

    private static ChartIdentifier CreateTestChartIdentifier()
    {
        return new ChartIdentifier(
   songId: Guid.NewGuid(),
     difficulty: Difficulty.Expert,
            level: new Level(15)
     );
    }

  private static Score CreateTestScore(int points = 950000)
    {
        return new Score(
  points: points,
       exScore: 2500,
            rank: Rank.AAPlus,
            judgements: new Judgements(500, 100, 50, 10, 5),
   maxCombo: 650,
            clearType: ClearType.FullCombo
 );
    }

    #endregion

    #region Constructor Tests

    [Fact]
    public void Constructor_ValidParameters_CreatesInstance()
    {
     // Arrange
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();
var chartId = CreateTestChartIdentifier();
        var score = CreateTestScore();
        var achievedAt = DateTime.UtcNow;

  // Act
      var personalHighScore = new PersonalHighScore(id, userId, chartId, score, achievedAt);

     // Assert
     Assert.Equal(id, personalHighScore.PersonalHighScoreId);
     Assert.Equal(userId, personalHighScore.UserProfileId);
        Assert.Equal(chartId, personalHighScore.ChartIdentifier);
   Assert.Equal(score, personalHighScore.Score);
        Assert.Equal(achievedAt, personalHighScore.AchievedAt);
  }

 [Fact]
    public void Constructor_NullChart_ThrowsArgumentNullException()
    {
  // Arrange
        var id = Guid.NewGuid();
    var userId = Guid.NewGuid();
        var score = CreateTestScore();
        var achievedAt = DateTime.UtcNow;

 // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
new PersonalHighScore(id, userId, null!, score, achievedAt));
  }

    [Fact]
    public void Constructor_NullScore_ThrowsArgumentNullException()
    {
   // Arrange
        var id = Guid.NewGuid();
      var userId = Guid.NewGuid();
        var chartId = CreateTestChartIdentifier();
     var achievedAt = DateTime.UtcNow;

 // Act & Assert
  Assert.Throws<ArgumentNullException>(() =>
  new PersonalHighScore(id, userId, chartId, null!, achievedAt));
    }

    #endregion

    #region TryUpdateWith Tests

    [Fact]
    public void TryUpdateWith_HigherScore_UpdatesAndReturnsTrue()
    {
    // Arrange
        var personalHighScore = new PersonalHighScore(
   Guid.NewGuid(),
        Guid.NewGuid(),
            CreateTestChartIdentifier(),
     CreateTestScore(900000), // 初期スコア: 900,000
  DateTime.UtcNow.AddDays(-1)
        );

        var newScore = CreateTestScore(950000); // より高いスコア: 950,000
        var newPlayedAt = DateTime.UtcNow;

        // Act
        var result = personalHighScore.TryUpdateWith(newScore, newPlayedAt);

        // Assert
        Assert.True(result);
      Assert.Equal(newScore, personalHighScore.Score);
        Assert.Equal(newPlayedAt, personalHighScore.AchievedAt);
    }

    [Fact]
  public void TryUpdateWith_LowerScore_DoesNotUpdateAndReturnsFalse()
    {
        // Arrange
   var originalScore = CreateTestScore(950000);
  var originalAchievedAt = DateTime.UtcNow.AddDays(-1);
     var personalHighScore = new PersonalHighScore(
   Guid.NewGuid(),
   Guid.NewGuid(),
      CreateTestChartIdentifier(),
   originalScore,
     originalAchievedAt
  );

   var lowerScore = CreateTestScore(900000); // より低いスコア
  var newPlayedAt = DateTime.UtcNow;

        // Act
        var result = personalHighScore.TryUpdateWith(lowerScore, newPlayedAt);

     // Assert
      Assert.False(result);
     Assert.Equal(originalScore, personalHighScore.Score);
    Assert.Equal(originalAchievedAt, personalHighScore.AchievedAt);
    }

    [Fact]
    public void TryUpdateWith_EqualScore_DoesNotUpdateAndReturnsFalse()
    {
        // Arrange
        var originalScore = CreateTestScore(950000);
     var originalAchievedAt = DateTime.UtcNow.AddDays(-1);
     var personalHighScore = new PersonalHighScore(
Guid.NewGuid(),
            Guid.NewGuid(),
CreateTestChartIdentifier(),
originalScore,
            originalAchievedAt
        );

        var equalScore = CreateTestScore(950000); // 同じスコア
        var newPlayedAt = DateTime.UtcNow;

  // Act
        var result = personalHighScore.TryUpdateWith(equalScore, newPlayedAt);

        // Assert - 先着優先（更新されない）
   Assert.False(result);
      Assert.Equal(originalScore, personalHighScore.Score);
        Assert.Equal(originalAchievedAt, personalHighScore.AchievedAt);
    }

    [Fact]
    public void TryUpdateWith_NullScore_ThrowsArgumentNullException()
    {
 // Arrange
        var personalHighScore = new PersonalHighScore(
Guid.NewGuid(),
   Guid.NewGuid(),
       CreateTestChartIdentifier(),
 CreateTestScore(),
 DateTime.UtcNow
 );

        // Act & Assert
 Assert.Throws<ArgumentNullException>(() =>
            personalHighScore.TryUpdateWith(null!, DateTime.UtcNow));
    }

#endregion

    #region Equality Tests

    [Fact]
    public void Equals_SameId_ReturnsTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        var personalHighScore1 = new PersonalHighScore(
         id, Guid.NewGuid(), CreateTestChartIdentifier(), CreateTestScore(), DateTime.UtcNow);
  var personalHighScore2 = new PersonalHighScore(
  id, Guid.NewGuid(), CreateTestChartIdentifier(), CreateTestScore(), DateTime.UtcNow);

        // Act & Assert
        Assert.Equal(personalHighScore1, personalHighScore2);
        Assert.True(personalHighScore1 == personalHighScore2);
   Assert.False(personalHighScore1 != personalHighScore2);
    }

    [Fact]
    public void Equals_DifferentId_ReturnsFalse()
{
        // Arrange
   var personalHighScore1 = new PersonalHighScore(
   Guid.NewGuid(), Guid.NewGuid(), CreateTestChartIdentifier(), CreateTestScore(), DateTime.UtcNow);
        var personalHighScore2 = new PersonalHighScore(
  Guid.NewGuid(), Guid.NewGuid(), CreateTestChartIdentifier(), CreateTestScore(), DateTime.UtcNow);

 // Act & Assert
        Assert.NotEqual(personalHighScore1, personalHighScore2);
  Assert.False(personalHighScore1 == personalHighScore2);
  Assert.True(personalHighScore1 != personalHighScore2);
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
{
        // Arrange
        var personalHighScore = new PersonalHighScore(
    Guid.NewGuid(), Guid.NewGuid(), CreateTestChartIdentifier(), CreateTestScore(), DateTime.UtcNow);

     // Act & Assert
     Assert.False(personalHighScore.Equals(null));
   Assert.False(personalHighScore == null);
      Assert.True(personalHighScore != null);
    }

    [Fact]
    public void Equals_SameInstance_ReturnsTrue()
    {
// Arrange
 var personalHighScore = new PersonalHighScore(
Guid.NewGuid(), Guid.NewGuid(), CreateTestChartIdentifier(), CreateTestScore(), DateTime.UtcNow);

        // Act & Assert
#pragma warning disable CS1718 // 意図的に同一変数を比較
  Assert.True(personalHighScore.Equals(personalHighScore));
        Assert.True(personalHighScore == personalHighScore);
#pragma warning restore CS1718
    }

 #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameId_ReturnsSameHashCode()
    {
        // Arrange
        var id = Guid.NewGuid();
var personalHighScore1 = new PersonalHighScore(
id, Guid.NewGuid(), CreateTestChartIdentifier(), CreateTestScore(), DateTime.UtcNow);
        var personalHighScore2 = new PersonalHighScore(
            id, Guid.NewGuid(), CreateTestChartIdentifier(), CreateTestScore(), DateTime.UtcNow);

      // Act & Assert
        Assert.Equal(personalHighScore1.GetHashCode(), personalHighScore2.GetHashCode());
  }

    [Fact]
    public void GetHashCode_DifferentId_MayReturnDifferentHashCode()
    {
   // Arrange
  var personalHighScore1 = new PersonalHighScore(
Guid.NewGuid(), Guid.NewGuid(), CreateTestChartIdentifier(), CreateTestScore(), DateTime.UtcNow);
        var personalHighScore2 = new PersonalHighScore(
 Guid.NewGuid(), Guid.NewGuid(), CreateTestChartIdentifier(), CreateTestScore(), DateTime.UtcNow);

   // Act & Assert
        // ハッシュコードは異なる可能性が高いが、衝突もありうるため NotEqual でチェック
        Assert.NotEqual(personalHighScore1.GetHashCode(), personalHighScore2.GetHashCode());
    }

    #endregion

 #region ToString Tests

  [Fact]
    public void ToString_ReturnsFormattedString()
    {
// Arrange
     var id = Guid.Parse("12345678-1234-1234-1234-123456789012");
        var userId = Guid.Parse("87654321-4321-4321-4321-210987654321");
var achievedAt = new DateTime(2025, 11, 3, 10, 30, 0, DateTimeKind.Utc);
        var personalHighScore = new PersonalHighScore(
 id, userId, CreateTestChartIdentifier(), CreateTestScore(980000), achievedAt);

  // Act
        var result = personalHighScore.ToString();

     // Assert
 Assert.Contains(id.ToString(), result);
  Assert.Contains(userId.ToString(), result);
Assert.Contains("980000", result);
    }

  #endregion
}
