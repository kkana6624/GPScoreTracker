using GPScoreTracker.Domain.Enums;
using GPScoreTracker.Domain.ValueObjects;
using Xunit;

namespace GPScoreTracker.Domain.Tests.ValueObjects;

/// <summary>
/// Score 値オブジェクトのテスト
/// </summary>
public class ScoreTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_ValidValues_CreatesInstance()
    {
        // Arrange
        var judgements = new Judgements(450, 30, 15, 3, 2);

        // Act
        var score = new Score(
            points: 980000,
            exScore: 2850,
            rank: Rank.AAPlus,
            judgements: judgements,
            maxCombo: 495,
            clearType: ClearType.Cleared);

        // Assert
        Assert.Equal(980000, score.Points);
        Assert.Equal(2850, score.EXScore);
        Assert.Equal(Rank.AAPlus, score.Rank);
        Assert.Equal(judgements, score.Judgements);
        Assert.Equal(495, score.MaxCombo);
        Assert.Equal(ClearType.Cleared, score.ClearType);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000_001)]
    public void Constructor_InvalidPoints_ThrowsArgumentOutOfRangeException(int points)
    {
        // Arrange
        var judgements = new Judgements(100, 0, 0, 0, 0);

        // Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => new Score(points, 300, Rank.AAA, judgements, 100, ClearType.FullCombo));
        Assert.Contains("Points must be between 0 and 1,000,000", exception.Message);
    }

    [Fact]
    public void Constructor_NegativeEXScore_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var judgements = new Judgements(100, 0, 0, 0, 0);

        // Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => new Score(1000000, -1, Rank.AAA, judgements, 100, ClearType.FullCombo));
        Assert.Contains("EXScore must be non-negative", exception.Message);
    }

    [Fact]
    public void Constructor_NegativeMaxCombo_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var judgements = new Judgements(100, 0, 0, 0, 0);

        // Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => new Score(1000000, 300, Rank.AAA, judgements, -1, ClearType.FullCombo));
        Assert.Contains("MaxCombo must be non-negative", exception.Message);
    }

    [Fact]
    public void Constructor_NullJudgements_ThrowsArgumentNullException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => new Score(1000000, 300, Rank.AAA, null!, 100, ClearType.FullCombo));
        Assert.Equal("judgements", exception.ParamName);
    }

    #endregion

    #region Boundary Tests

    [Fact]
    public void Constructor_MinimumPoints_CreatesInstance()
    {
        // Arrange
        var judgements = new Judgements(0, 0, 0, 0, 100);

        // Act
        var score = new Score(0, 0, Rank.E, judgements, 0, ClearType.Failed);

        // Assert
        Assert.Equal(0, score.Points);
    }

    [Fact]
    public void Constructor_MaximumPoints_CreatesInstance()
    {
        // Arrange
        var judgements = new Judgements(500, 0, 0, 0, 0);

        // Act
        var score = new Score(1_000_000, 1500, Rank.AAA, judgements, 500, ClearType.MarvelousFullCombo);

        // Assert
        Assert.Equal(1_000_000, score.Points);
    }

    [Fact]
    public void Constructor_PerfectScore_CreatesInstance()
    {
        // Arrange
        var judgements = new Judgements(500, 0, 0, 0, 0);

        // Act
        var score = new Score(
            points: 1_000_000,
            exScore: 1500,
            rank: Rank.AAA,
            judgements: judgements,
            maxCombo: 500,
            clearType: ClearType.MarvelousFullCombo);

        // Assert
        Assert.Equal(1_000_000, score.Points);
        Assert.Equal(Rank.AAA, score.Rank);
        Assert.Equal(ClearType.MarvelousFullCombo, score.ClearType);
    }

    [Fact]
    public void Constructor_FailedScore_CreatesInstance()
    {
        // Arrange
        var judgements = new Judgements(50, 30, 20, 10, 90);

        // Act
        var score = new Score(
            points: 650000,
            exScore: 230,
            rank: Rank.E,
            judgements: judgements,
            maxCombo: 50,
            clearType: ClearType.Failed);

        // Assert
        Assert.Equal(650000, score.Points);
        Assert.Equal(Rank.E, score.Rank);
        Assert.Equal(ClearType.Failed, score.ClearType);
    }

    #endregion

    #region Equality Tests

    [Fact]
    public void Equals_SameValues_ReturnsTrue()
    {
        // Arrange
        var judgements = new Judgements(100, 50, 30, 10, 5);
        var score1 = new Score(980000, 585, Rank.AAPlus, judgements, 190, ClearType.Cleared);
        var score2 = new Score(980000, 585, Rank.AAPlus, judgements, 190, ClearType.Cleared);

        // Act & Assert
        Assert.Equal(score1, score2);
        Assert.True(score1 == score2);
        Assert.False(score1 != score2);
    }

    [Fact]
    public void Equals_DifferentPoints_ReturnsFalse()
    {
        // Arrange
        var judgements = new Judgements(100, 50, 30, 10, 5);
        var score1 = new Score(980000, 585, Rank.AAPlus, judgements, 190, ClearType.Cleared);
        var score2 = new Score(970000, 585, Rank.AAPlus, judgements, 190, ClearType.Cleared);

        // Act & Assert
        Assert.NotEqual(score1, score2);
        Assert.False(score1 == score2);
        Assert.True(score1 != score2);
    }

    [Fact]
    public void Equals_DifferentRank_ReturnsFalse()
    {
        // Arrange
        var judgements = new Judgements(100, 50, 30, 10, 5);
        var score1 = new Score(980000, 585, Rank.AAPlus, judgements, 190, ClearType.Cleared);
        var score2 = new Score(980000, 585, Rank.AA, judgements, 190, ClearType.Cleared);

        // Act & Assert
        Assert.NotEqual(score1, score2);
    }

    [Fact]
    public void Equals_DifferentJudgements_ReturnsFalse()
    {
        // Arrange
        var judgements1 = new Judgements(100, 50, 30, 10, 5);
        var judgements2 = new Judgements(100, 50, 30, 10, 6);
        var score1 = new Score(980000, 585, Rank.AAPlus, judgements1, 190, ClearType.Cleared);
        var score2 = new Score(980000, 585, Rank.AAPlus, judgements2, 190, ClearType.Cleared);

        // Act & Assert
        Assert.NotEqual(score1, score2);
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var judgements = new Judgements(100, 50, 30, 10, 5);
        var score = new Score(980000, 585, Rank.AAPlus, judgements, 190, ClearType.Cleared);

        // Act & Assert
        Assert.False(score.Equals(null));
        Assert.False(score == null);
        Assert.True(score != null);
    }

    [Fact]
    public void Equals_SameInstance_ReturnsTrue()
    {
        // Arrange
        var judgements = new Judgements(100, 50, 30, 10, 5);
        var score = new Score(980000, 585, Rank.AAPlus, judgements, 190, ClearType.Cleared);

        // Act & Assert
#pragma warning disable CS1718 // 意図的に同じ変数を比較
        Assert.True(score.Equals(score));
        Assert.True(score == score);
#pragma warning restore CS1718
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameValues_ReturnsSameHashCode()
    {
        // Arrange
        var judgements = new Judgements(100, 50, 30, 10, 5);
        var score1 = new Score(980000, 585, Rank.AAPlus, judgements, 190, ClearType.Cleared);
        var score2 = new Score(980000, 585, Rank.AAPlus, judgements, 190, ClearType.Cleared);

        // Act & Assert
        Assert.Equal(score1.GetHashCode(), score2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentValues_ReturnsDifferentHashCode()
    {
        // Arrange
        var judgements = new Judgements(100, 50, 30, 10, 5);
        var score1 = new Score(980000, 585, Rank.AAPlus, judgements, 190, ClearType.Cleared);
        var score2 = new Score(970000, 585, Rank.AAPlus, judgements, 190, ClearType.Cleared);

        // Act & Assert
        Assert.NotEqual(score1.GetHashCode(), score2.GetHashCode());
    }

    #endregion

    #region Realistic Scenario Tests

    [Fact]
    public void Constructor_TypicalAAAScore_CreatesInstance()
    {
        // Arrange - 典型的なAAAスコア
        var judgements = new Judgements(
            marvelous: 480,
            perfect: 15,
            great: 5,
            good: 0,
            miss: 0);

        // Act
        var score = new Score(
            points: 998000,
            exScore: 1475,
            rank: Rank.AAA,
            judgements: judgements,
            maxCombo: 500,
            clearType: ClearType.GreatFullCombo);

        // Assert
        Assert.Equal(998000, score.Points);
        Assert.Equal(Rank.AAA, score.Rank);
        Assert.Equal(ClearType.GreatFullCombo, score.ClearType);
        Assert.Equal(500, score.Judgements.TotalNotes);
    }

    [Fact]
    public void Constructor_TypicalBScore_CreatesInstance()
    {
        // Arrange - 典型的なBランクスコア
        var judgements = new Judgements(
            marvelous: 200,
            perfect: 150,
            great: 100,
            good: 30,
            miss: 20);

        // Act
        var score = new Score(
            points: 720000,
            exScore: 1030,
            rank: Rank.B,
            judgements: judgements,
            maxCombo: 250,
            clearType: ClearType.Cleared);

        // Assert
        Assert.Equal(720000, score.Points);
        Assert.Equal(Rank.B, score.Rank);
        Assert.Equal(ClearType.Cleared, score.ClearType);
    }

    #endregion
}
