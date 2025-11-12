using GPScoreTracker.Domain.ValueObjects;
using System.Globalization;

namespace GPScoreTracker.Domain.Entities;

/// <summary>
/// ユーザーごと、譜面ごとの自己ベスト記録を表すエンティティ
/// </summary>
public sealed class PersonalHighScore : IEquatable<PersonalHighScore>
{
    /// <summary>
    /// 自己ベスト記録の一意な識別子
    /// </summary>
    public Guid PersonalHighScoreId { get; private set; }

    /// <summary>
    /// 記録を保持するユーザーのID
    /// </summary>
    public Guid UserProfileId { get; private set; }

    /// <summary>
    /// 対象となる譜面のID（Chartエンティティへの参照）
    /// </summary>
    public Guid ChartId { get; private set; }

    /// <summary>
    /// 自己ベストのスコア詳細
    /// </summary>
    public Score Score { get; private set; }

    /// <summary>
    /// この記録を達成した日時
    /// </summary>
    public DateTime AchievedAt { get; private set; }

    /// <summary>
    /// PersonalHighScore エンティティを作成します
    /// </summary>
    /// <param name="personalHighScoreId">自己ベスト記録の一意な識別子</param>
    /// <param name="userProfileId">記録を保持するユーザーのID</param>
    /// <param name="chartId">対象となる譜面のID</param>
    /// <param name="score">自己ベストのスコア詳細</param>
    /// <param name="achievedAt">この記録を達成した日時</param>
    /// <exception cref="ArgumentNullException">score が null の場合</exception>
    public PersonalHighScore(
        Guid personalHighScoreId,
        Guid userProfileId,
        Guid chartId,
        Score score,
        DateTime achievedAt)
    {
        ArgumentNullException.ThrowIfNull(score);

        PersonalHighScoreId = personalHighScoreId;
        UserProfileId = userProfileId;
        ChartId = chartId;
        Score = score;
        AchievedAt = achievedAt;
    }

    /// <summary>
    /// 新しいスコアで自己ベストの更新を試みます
    /// Pointsのみで判定します
    /// </summary>
    /// <param name="newScore">新しいスコア</param>
    /// <param name="playedAt">プレイ日時</param>
    /// <returns>更新された場合 true、されなかった場合 false（同点の場合は先着優先で更新しない）</returns>
    /// <exception cref="ArgumentNullException">newScore が null の場合</exception>
    public bool TryUpdateWith(Score newScore, DateTime playedAt)
    {
        ArgumentNullException.ThrowIfNull(newScore);

        // Pointsが現在より高い場合のみ更新（同点の場合は更新しない = 先着優先）
        if (newScore.Points > Score.Points)
        {
            Score = newScore;
            AchievedAt = playedAt;
            return true;
        }

        return false;
    }

    /// <summary>
    /// 指定されたPersonalHighScoreオブジェクトと等しいかどうかを判定します
    /// エンティティの一意性は PersonalHighScoreId で判定されます
    /// </summary>
    public bool Equals(PersonalHighScore? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return PersonalHighScoreId == other.PersonalHighScoreId;
    }

    /// <summary>
    /// 指定されたオブジェクトと等しいかどうかを判定します
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as PersonalHighScore);

    /// <summary>
    /// ハッシュコードを取得します
    /// </summary>
    public override int GetHashCode() => PersonalHighScoreId.GetHashCode();

    /// <summary>
    /// 等価演算子
    /// </summary>
    public static bool operator ==(PersonalHighScore? left, PersonalHighScore? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// 非等価演算子
    /// </summary>
    public static bool operator !=(PersonalHighScore? left, PersonalHighScore? right) => !(left == right);

    /// <summary>
    /// 自己ベスト記録を文字列として返します
    /// </summary>
    public override string ToString() =>
      $"PersonalHighScoreId:{PersonalHighScoreId} UserProfileId:{UserProfileId} ChartId:{ChartId} " +
        $"Score:{Score.Points} AchievedAt:{AchievedAt.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)}";
}
