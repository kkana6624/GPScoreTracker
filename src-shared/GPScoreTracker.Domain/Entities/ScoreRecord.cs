using GPScoreTracker.Domain.ValueObjects;
using System.Globalization;

namespace GPScoreTracker.Domain.Entities;

/// <summary>
/// プレイヤーによる1回ごとのプレイ履歴を表すエンティティ
/// </summary>
public sealed class ScoreRecord : IEquatable<ScoreRecord>
{
    /// <summary>
    /// スコア記録の一意な識別子
    /// </summary>
    public Guid ScoreRecordId { get; private set; }

    /// <summary>
    /// プレイしたユーザーのID
    /// </summary>
    public Guid UserProfileId { get; private set; }

    /// <summary>
    /// プレイした譜面の識別情報
    /// </summary>
    public ChartIdentifier ChartIdentifier { get; private set; }

    /// <summary>
    /// プレイ結果のスコア詳細
    /// </summary>
    public Score Score { get; private set; }

    /// <summary>
    /// プレイした日時
    /// </summary>
    public DateTime PlayedAt { get; private set; }

    /// <summary>
    /// ScoreRecordエンティティを作成します
    /// </summary>
    /// <param name="scoreRecordId">スコア記録の一意な識別子</param>
    /// <param name="userProfileId">プレイしたユーザーのID</param>
    /// <param name="chartIdentifier">プレイした譜面の識別情報</param>
    /// <param name="score">プレイ結果のスコア詳細</param>
    /// <param name="playedAt">プレイした日時</param>
    /// <exception cref="ArgumentNullException">chartIdentifier または score が null の場合</exception>
    public ScoreRecord(Guid scoreRecordId, Guid userProfileId, ChartIdentifier chartIdentifier, Score score, DateTime playedAt)
    {
        ArgumentNullException.ThrowIfNull(chartIdentifier);
        ArgumentNullException.ThrowIfNull(score);

        ScoreRecordId = scoreRecordId;
        UserProfileId = userProfileId;
        ChartIdentifier = chartIdentifier;
        Score = score;
        PlayedAt = playedAt;
    }

    /// <summary>
    /// 指定されたScoreRecordオブジェクトと等しいかどうかを判定します
    /// </summary>
    public bool Equals(ScoreRecord? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return ScoreRecordId == other.ScoreRecordId;
    }

    /// <summary>
    /// 指定されたオブジェクトと等しいかどうかを判定します
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as ScoreRecord);

    /// <summary>
    /// ハッシュコードを取得します
    /// </summary>
    public override int GetHashCode() => ScoreRecordId.GetHashCode();

    /// <summary>
    /// 等価演算子
    /// </summary>
    public static bool operator ==(ScoreRecord? left, ScoreRecord? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// 非等価演算子
    /// </summary>
    public static bool operator !=(ScoreRecord? left, ScoreRecord? right) => !(left == right);

    /// <summary>
    /// スコア記録情報を文字列として返します
    /// </summary>
    public override string ToString() =>
        $"ScoreRecordId:{ScoreRecordId} UserProfileId:{UserProfileId} PlayedAt:{PlayedAt.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)}";
}