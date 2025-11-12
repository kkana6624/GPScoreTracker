using GPScoreTracker.Domain.ValueObjects;
using System.Globalization;

namespace GPScoreTracker.Domain.Entities;

/// <summary>
/// 譜面ごとの、全ユーザー中での最高記録を表すエンティティ
/// </summary>
public sealed class TopScore : IEquatable<TopScore>
{
    /// <summary>
    /// トップスコア記録の一意な識別子
    /// </summary>
    public Guid TopScoreId { get; private set; }

    /// <summary>
    /// この記録を達成したユーザーのID
    /// </summary>
    public Guid UserProfileId { get; private set; }

    /// <summary>
    /// 対象となる譜面の識別情報（値オブジェクト）
    /// </summary>
    public ChartIdentifier ChartIdentifier { get; private set; }

    /// <summary>
    /// トップスコアのスコア詳細
    /// </summary>
    public Score Score { get; private set; }

    /// <summary>
    /// この記録を達成した日時
    /// </summary>
    public DateTime AchievedAt { get; private set; }

    /// <summary>
    /// TopScoreエンティティを作成します
    /// </summary>
    /// <param name="topScoreId">トップスコア記録の一意な識別子</param>
    /// <param name="userProfileId">この記録を達成したユーザーのID</param>
    /// <param name="chartIdentifier">対象となる譜面の識別情報（値オブジェクト）</param>
    /// <param name="score">トップスコアのスコア詳細</param>
    /// <param name="achievedAt">この記録を達成した日時</param>
    /// <exception cref="ArgumentNullException">chartIdentifier または score が null の場合</exception>
    public TopScore(
        Guid topScoreId,
        Guid userProfileId,
        ChartIdentifier chartIdentifier,
        Score score,
        DateTime achievedAt)
    {
        ArgumentNullException.ThrowIfNull(chartIdentifier);
        ArgumentNullException.ThrowIfNull(score);

        TopScoreId = topScoreId;
        UserProfileId = userProfileId;
        ChartIdentifier = chartIdentifier;
        Score = score;
        AchievedAt = achievedAt;
    }

    /// <summary>
    /// 新しいスコアでトップスコアの更新を試みます
    /// Points（100万点満点のスコア）のみで判定します
    /// </summary>
    /// <param name="newScore">新しいスコア</param>
    /// <param name="achievedByUserId">新しい記録を達成したユーザーID</param>
    /// <param name="playedAt">プレイ日時</param>
    /// <returns>更新された場合 true、されなかった場合 false（同点の場合は先着優先で更新しない）</returns>
    /// <exception cref="ArgumentNullException">newScore が null の場合</exception>
    public bool TryUpdateWith(Score newScore, Guid achievedByUserId, DateTime playedAt)
    {
        ArgumentNullException.ThrowIfNull(newScore);

        // Pointsが現在より高い場合のみ更新（同点の場合は更新しない = 先着優先）
        if (newScore.Points > Score.Points)
        {
            Score = newScore;
            UserProfileId = achievedByUserId; // 達成者が変わる可能性がある
            AchievedAt = playedAt;
            return true;
        }

        return false;
    }

    /// <summary>
    /// 指定されたTopScoreオブジェクトと等しいかどうかを判定します
    /// エンティティの一意性は TopScoreId で判定されます
    /// </summary>
    public bool Equals(TopScore? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return TopScoreId == other.TopScoreId;
    }

    /// <summary>
    /// 指定されたオブジェクトと等しいかどうかを判定します
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as TopScore);

    /// <summary>
    /// ハッシュコードを取得します
    /// </summary>
    public override int GetHashCode() => TopScoreId.GetHashCode();

    /// <summary>
    /// 等価演算子
    /// </summary>
    public static bool operator ==(TopScore? left, TopScore? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// 非等価演算子
    /// </summary>
    public static bool operator !=(TopScore? left, TopScore? right) => !(left == right);

    /// <summary>
    /// トップスコア記録を文字列として返します
    /// </summary>
    public override string ToString() =>
        $"TopScoreId:{TopScoreId} UserProfileId:{UserProfileId} " +
        $"Score:{Score.Points} AchievedAt:{AchievedAt.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)}";
}
