using GPScoreTracker.Domain.Enums;

namespace GPScoreTracker.Domain.ValueObjects;

/// <summary>
/// DDRの1回のプレイ結果のスコア詳細を表す値オブジェクト
/// </summary>
public sealed class Score : IEquatable<Score>
{
    /// <summary>
    /// 100万点満点のスコア
    /// </summary>
    public int Points { get; }

    /// <summary>
    /// EXスコア（判定ごとの配点に基づくスコア）
    /// </summary>
    public int EXScore { get; }

    /// <summary>
    /// 評価ランク
    /// </summary>
    public Rank Rank { get; }

    /// <summary>
    /// 判定ごとの回数
    /// </summary>
    public Judgements Judgements { get; }

    /// <summary>
    /// 最大コンボ数
    /// </summary>
    public int MaxCombo { get; }

    /// <summary>
    /// クリアタイプ（達成状況）
    /// </summary>
    public ClearType ClearType { get; }

    /// <summary>
    /// Score値オブジェクトを作成します
    /// </summary>
    /// <param name="points">100万点満点のスコア（0～1,000,000）</param>
    /// <param name="exScore">EXスコア（0以上）</param>
    /// <param name="rank">評価ランク</param>
    /// <param name="judgements">判定ごとの回数</param>
    /// <param name="maxCombo">最大コンボ数（0以上）</param>
    /// <param name="clearType">クリアタイプ</param>
    /// <exception cref="ArgumentOutOfRangeException">数値パラメータが範囲外の場合</exception>
    /// <exception cref="ArgumentNullException">judgementsがnullの場合</exception>
    public Score(
        int points,
        int exScore,
        Rank rank,
        Judgements judgements,
        int maxCombo,
        ClearType clearType)
    {
        if (points < 0 || points > 1_000_000)
        {
            throw new ArgumentOutOfRangeException(
         nameof(points),
                 points,
              "Points must be between 0 and 1,000,000.");
        }

        if (exScore < 0)
        {
            throw new ArgumentOutOfRangeException(
                 nameof(exScore),
              exScore,
                "EXScore must be non-negative.");
        }

        if (maxCombo < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(maxCombo),
         maxCombo,
         "MaxCombo must be non-negative.");
        }

        ArgumentNullException.ThrowIfNull(judgements);

        Points = points;
        EXScore = exScore;
        Rank = rank;
        Judgements = judgements;
        MaxCombo = maxCombo;
        ClearType = clearType;
    }

    /// <summary>
    /// 指定されたScoreオブジェクトと等しいかどうかを判定します
    /// </summary>
    public bool Equals(Score? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Points == other.Points &&
        EXScore == other.EXScore &&
        Rank == other.Rank &&
        Judgements.Equals(other.Judgements) &&
        MaxCombo == other.MaxCombo &&
        ClearType == other.ClearType;
    }

    /// <summary>
    /// 指定されたオブジェクトと等しいかどうかを判定します
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as Score);

    /// <summary>
    /// ハッシュコードを取得します
    /// </summary>
    public override int GetHashCode() =>
        HashCode.Combine(Points, EXScore, Rank, Judgements, MaxCombo, ClearType);

    /// <summary>
    /// 等価演算子
    /// </summary>
    public static bool operator ==(Score? left, Score? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// 非等価演算子
    /// </summary>
    public static bool operator !=(Score? left, Score? right) => !(left == right);

    /// <summary>
    /// スコア情報を文字列として返します
    /// </summary>
    public override string ToString() =>
        $"Points:{Points:N0} Rank:{Rank} ClearType:{ClearType}";
}
