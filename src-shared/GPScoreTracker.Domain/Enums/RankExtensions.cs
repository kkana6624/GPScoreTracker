namespace GPScoreTracker.Domain.Enums;

/// <summary>
/// Rank列挙型の拡張メソッド
/// </summary>
public static class RankExtensions
{
    /// <summary>
    /// スコアとクリア状態からランクを判定する
    /// </summary>
    /// <param name="points">獲得スコア（0～1,000,000）</param>
    /// <param name="isCleared">クリアしたかどうか</param>
    /// <returns>判定されたランク</returns>
    /// <exception cref="ArgumentOutOfRangeException">スコアが範囲外の場合</exception>
    public static Rank DetermineRank(int points, bool isCleared)
    {
        if (points < 0 || points > 1_000_000)
        {
            throw new ArgumentOutOfRangeException(
            nameof(points),
            points,
            "Points must be between 0 and 1,000,000.");
        }

        // クリア失敗の場合は無条件でEランク
        if (!isCleared)
        {
            return Rank.E;
        }

        // クリア成功時のランク判定
        return points switch
        {
            >= 990_000 => Rank.AAA,
            >= 950_000 => Rank.AAPlus,
            >= 900_000 => Rank.AA,
            >= 890_000 => Rank.AAMinus,
            >= 850_000 => Rank.APlus,
            >= 800_000 => Rank.A,
            >= 790_000 => Rank.AMinus,
            >= 750_000 => Rank.BPlus,
            >= 700_000 => Rank.B,
            >= 690_000 => Rank.BMinus,
            >= 650_000 => Rank.CPlus,
            >= 600_000 => Rank.C,
            >= 590_000 => Rank.CMinus,
            _ => Rank.D
        };
    }

    /// <summary>
    /// ランクの表示名を取得する
    /// </summary>
    /// <param name="rank">ランク</param>
    /// <returns>表示用の文字列</returns>
    public static string ToDisplayString(this Rank rank)
    {
        return rank switch
        {
            Rank.E => "E",
            Rank.D => "D",
            Rank.CMinus => "C-",
            Rank.C => "C",
            Rank.CPlus => "C+",
            Rank.BMinus => "B-",
            Rank.B => "B",
            Rank.BPlus => "B+",
            Rank.AMinus => "A-",
            Rank.A => "A",
            Rank.APlus => "A+",
            Rank.AAMinus => "AA-",
            Rank.AA => "AA",
            Rank.AAPlus => "AA+",
            Rank.AAA => "AAA",
            _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, "Unknown rank value.")
        };
    }

    /// <summary>
    /// ランクの最小スコアを取得する
    /// </summary>
    /// <param name="rank">ランク</param>
    /// <returns>そのランクを達成するための最小スコア</returns>
    public static int GetMinimumPoints(this Rank rank)
    {
        return rank switch
        {
            Rank.E => 0,
            Rank.D => 0,
            Rank.CMinus => 590_000,
            Rank.C => 600_000,
            Rank.CPlus => 650_000,
            Rank.BMinus => 690_000,
            Rank.B => 700_000,
            Rank.BPlus => 750_000,
            Rank.AMinus => 790_000,
            Rank.A => 800_000,
            Rank.APlus => 850_000,
            Rank.AAMinus => 890_000,
            Rank.AA => 900_000,
            Rank.AAPlus => 950_000,
            Rank.AAA => 990_000,
            _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, "Unknown rank value.")
        };
    }

    /// <summary>
    /// ランクの最大スコアを取得する
    /// </summary>
    /// <param name="rank">ランク</param>
    /// <returns>そのランクの最大スコア</returns>
    public static int GetMaximumPoints(this Rank rank)
    {
        return rank switch
        {
            Rank.E => 1_000_000, // Eランクは失敗時なのでスコアに上限はない
            Rank.D => 589_990,
            Rank.CMinus => 599_990,
            Rank.C => 649_990,
            Rank.CPlus => 689_990,
            Rank.BMinus => 699_990,
            Rank.B => 749_990,
            Rank.BPlus => 789_990,
            Rank.AMinus => 799_990,
            Rank.A => 849_990,
            Rank.APlus => 889_990,
            Rank.AAMinus => 899_990,
            Rank.AA => 949_990,
            Rank.AAPlus => 989_990,
            Rank.AAA => 1_000_000,
            _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, "Unknown rank value.")
        };
    }
}
