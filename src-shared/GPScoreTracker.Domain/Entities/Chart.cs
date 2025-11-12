using GPScoreTracker.Domain.Enums;
using GPScoreTracker.Domain.ValueObjects;

namespace GPScoreTracker.Domain.Entities;

/// <summary>
/// DDRの譜面（楽曲と難易度の組み合わせ）を表すエンティティ
/// </summary>
public sealed class Chart : IEquatable<Chart>
{
    /// <summary>
    /// 譜面の一意な識別子
    /// </summary>
    public Guid ChartId { get; private set; }

    /// <summary>
    /// 楽曲のID
    /// </summary>
    public Guid SongId { get; private set; }

    /// <summary>
    /// 難易度
    /// </summary>
    public Difficulty Difficulty { get; private set; }

    /// <summary>
    /// レベル値
    /// </summary>
    public Level Level { get; private set; }

    /// <summary>
    /// 論理削除フラグ（版権の都合で削除された譜面など）
    /// </summary>
    public bool IsDeleted { get; private set; }

    /// <summary>
    /// Chart エンティティを作成します
    /// </summary>
    /// <param name="chartId">譜面の一意な識別子</param>
    /// <param name="songId">楽曲のID</param>
    /// <param name="difficulty">難易度</param>
    /// <param name="level">レベル値</param>
    /// <exception cref="ArgumentNullException">level が null の場合</exception>
    public Chart(Guid chartId, Guid songId, Difficulty difficulty, Level level)
    {
        ArgumentNullException.ThrowIfNull(level);

        ChartId = chartId;
        SongId = songId;
        Difficulty = difficulty;
        Level = level;
        IsDeleted = false;
    }

    /// <summary>
    /// レベルを更新します（難易度調整時）
    /// </summary>
    /// <param name="newLevel">新しいレベル値</param>
    /// <exception cref="ArgumentNullException">newLevel が null の場合</exception>
    public void UpdateLevel(Level newLevel)
    {
        ArgumentNullException.ThrowIfNull(newLevel);
        Level = newLevel;
    }

    /// <summary>
    /// 譜面を論理削除します（版権の都合で削除される場合など）
    /// </summary>
    public void MarkAsDeleted()
    {
        IsDeleted = true;
    }

    /// <summary>
    /// 指定されたChartオブジェクトと等しいかどうかを判定します
    /// </summary>
    public bool Equals(Chart? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return ChartId == other.ChartId;
    }

    /// <summary>
    /// 指定されたオブジェクトと等しいかどうかを判定します
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as Chart);

    /// <summary>
    /// ハッシュコードを取得します
    /// </summary>
    public override int GetHashCode() => ChartId.GetHashCode();

    /// <summary>
    /// 等価演算子
    /// </summary>
    public static bool operator ==(Chart? left, Chart? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// 非等価演算子
    /// </summary>
    public static bool operator !=(Chart? left, Chart? right) => !(left == right);

    /// <summary>
    /// 譜面情報を文字列として返します
    /// </summary>
    public override string ToString() =>
        $"ChartId:{ChartId} SongId:{SongId} Difficulty:{Difficulty} Level:{Level.Value}" +
        (IsDeleted ? " [Deleted]" : "");
}