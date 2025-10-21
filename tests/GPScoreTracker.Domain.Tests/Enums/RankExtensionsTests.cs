using GPScoreTracker.Domain.Enums;
using Xunit;

namespace GPScoreTracker.Domain.Tests.Enums;

public class RankExtensionsTests
{
    #region DetermineRank Tests

    [Theory]
    [InlineData(1_000_000, true, Rank.AAA)]
    [InlineData(990_000, true, Rank.AAA)]
    [InlineData(989_990, true, Rank.AAPlus)]
    [InlineData(950_000, true, Rank.AAPlus)]
    [InlineData(949_990, true, Rank.AA)]
    [InlineData(900_000, true, Rank.AA)]
    [InlineData(899_990, true, Rank.AAMinus)]
    [InlineData(890_000, true, Rank.AAMinus)]
    [InlineData(889_990, true, Rank.APlus)]
    [InlineData(850_000, true, Rank.APlus)]
    [InlineData(849_990, true, Rank.A)]
    [InlineData(800_000, true, Rank.A)]
    [InlineData(799_990, true, Rank.AMinus)]
    [InlineData(790_000, true, Rank.AMinus)]
    [InlineData(789_990, true, Rank.BPlus)]
    [InlineData(750_000, true, Rank.BPlus)]
    [InlineData(749_990, true, Rank.B)]
    [InlineData(700_000, true, Rank.B)]
    [InlineData(699_990, true, Rank.BMinus)]
    [InlineData(690_000, true, Rank.BMinus)]
    [InlineData(689_990, true, Rank.CPlus)]
    [InlineData(650_000, true, Rank.CPlus)]
    [InlineData(649_990, true, Rank.C)]
    [InlineData(600_000, true, Rank.C)]
    [InlineData(599_990, true, Rank.CMinus)]
    [InlineData(590_000, true, Rank.CMinus)]
    [InlineData(589_990, true, Rank.D)]
    [InlineData(0, true, Rank.D)]
    public void DetermineRank_ValidScoreAndCleared_ReturnsCorrectRank(
        int points, bool isCleared, Rank expectedRank)
    {
        // Act
        var actualRank = RankExtensions.DetermineRank(points, isCleared);

        // Assert
        Assert.Equal(expectedRank, actualRank);
    }

    [Theory]
    [InlineData(1_000_000, false)]
    [InlineData(700_000, false)]
    [InlineData(0, false)]
    public void DetermineRank_NotCleared_ReturnsERank(int points, bool isCleared)
    {
        // Act
        var actualRank = RankExtensions.DetermineRank(points, isCleared);

        // Assert
        Assert.Equal(Rank.E, actualRank);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(1_000_001)]
    public void DetermineRank_InvalidScore_ThrowsArgumentOutOfRangeException(int points)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => RankExtensions.DetermineRank(points, true));

        Assert.Contains("Points must be between 0 and 1,000,000", exception.Message);
    }

    #endregion

    #region ToDisplayString Tests

    [Theory]
    [InlineData(Rank.AAA, "AAA")]
    [InlineData(Rank.AAPlus, "AA+")]
    [InlineData(Rank.AA, "AA")]
    [InlineData(Rank.AAMinus, "AA-")]
    [InlineData(Rank.APlus, "A+")]
    [InlineData(Rank.A, "A")]
    [InlineData(Rank.AMinus, "A-")]
    [InlineData(Rank.BPlus, "B+")]
    [InlineData(Rank.B, "B")]
    [InlineData(Rank.BMinus, "B-")]
    [InlineData(Rank.CPlus, "C+")]
    [InlineData(Rank.C, "C")]
    [InlineData(Rank.CMinus, "C-")]
    [InlineData(Rank.D, "D")]
    [InlineData(Rank.E, "E")]
    public void ToDisplayString_AllRanks_ReturnsCorrectString(Rank rank, string expected)
    {
        // Act
        var actual = rank.ToDisplayString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToDisplayString_InvalidRank_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var invalidRank = (Rank)999;

        // Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
          () => invalidRank.ToDisplayString());

        Assert.Contains("Unknown rank value", exception.Message);
    }

    #endregion

    #region GetMinimumPoints Tests

    [Theory]
    [InlineData(Rank.E, 0)]
    [InlineData(Rank.D, 0)]
    [InlineData(Rank.CMinus, 590_000)]
    [InlineData(Rank.C, 600_000)]
    [InlineData(Rank.CPlus, 650_000)]
    [InlineData(Rank.BMinus, 690_000)]
    [InlineData(Rank.B, 700_000)]
    [InlineData(Rank.BPlus, 750_000)]
    [InlineData(Rank.AMinus, 790_000)]
    [InlineData(Rank.A, 800_000)]
    [InlineData(Rank.APlus, 850_000)]
    [InlineData(Rank.AAMinus, 890_000)]
    [InlineData(Rank.AA, 900_000)]
    [InlineData(Rank.AAPlus, 950_000)]
    [InlineData(Rank.AAA, 990_000)]
    public void GetMinimumPoints_AllRanks_ReturnsCorrectValue(Rank rank, int expected)
    {
        // Act
        var actual = rank.GetMinimumPoints();

        // Assert
        Assert.Equal(expected, actual);
    }

    #endregion

    #region GetMaximumPoints Tests

    [Theory]
    [InlineData(Rank.E, 1_000_000)]
    [InlineData(Rank.D, 589_990)]
    [InlineData(Rank.CMinus, 599_990)]
    [InlineData(Rank.C, 649_990)]
    [InlineData(Rank.CPlus, 689_990)]
    [InlineData(Rank.BMinus, 699_990)]
    [InlineData(Rank.B, 749_990)]
    [InlineData(Rank.BPlus, 789_990)]
    [InlineData(Rank.AMinus, 799_990)]
    [InlineData(Rank.A, 849_990)]
    [InlineData(Rank.APlus, 889_990)]
    [InlineData(Rank.AAMinus, 899_990)]
    [InlineData(Rank.AA, 949_990)]
    [InlineData(Rank.AAPlus, 989_990)]
    [InlineData(Rank.AAA, 1_000_000)]
    public void GetMaximumPoints_AllRanks_ReturnsCorrectValue(Rank rank, int expected)
    {
        // Act
        var actual = rank.GetMaximumPoints();

        // Assert
        Assert.Equal(expected, actual);
    }

    #endregion

    #region Integration Tests

    [Fact]
    public void RankRanges_DoNotOverlap()
    {
        // すべてのランク（E除く）が重複しないことを確認
        var ranks = new[]
        {
            Rank.D, Rank.CMinus, Rank.C, Rank.CPlus,
            Rank.BMinus, Rank.B, Rank.BPlus,
            Rank.AMinus, Rank.A, Rank.APlus,
            Rank.AAMinus, Rank.AA, Rank.AAPlus, Rank.AAA
        };

        for (int i = 0; i < ranks.Length - 1; i++)
        {
            var currentMax = ranks[i].GetMaximumPoints();
            var nextMin = ranks[i + 1].GetMinimumPoints();

            // 現在のランクの最大値 + 10 = 次のランクの最小値 であることを確認
            Assert.Equal(nextMin, currentMax + 10);
        }
    }

    [Fact]
    public void DetermineRank_CoversAllPointRanges()
    {
        // 0から1,000,000まで10,000点刻みでテスト
        for (int points = 0; points <= 1_000_000; points += 10_000)
        {
            // 例外が発生しないことを確認
            var rank = RankExtensions.DetermineRank(points, true);

            // ランクが妥当な範囲内であることを確認
            Assert.InRange((int)rank, (int)Rank.D, (int)Rank.AAA);
        }
    }
    #endregion
}
