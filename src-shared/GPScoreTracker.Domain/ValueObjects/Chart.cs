using GPScoreTracker.Domain.Enums;

namespace GPScoreTracker.Domain.ValueObjects;

/// <summary>
/// DDRの譜面（楽曲と難易度の組み合わせ）を表す値オブジェクト
/// </summary>
public sealed class Chart : IEquatable<Chart>
{
    /// <summary>
    /// 楽曲のID
    /// </summary>
    public Guid SongId { get; }

    /// <summary>
    /// 難易度
    /// </summary>
    public Difficulty Difficulty { get; }

    /// <summary>
    /// レベル
    /// </summary>
    public Level Level { get; }

    /// <summary>
    /// Chart値オブジェクトを作成します
    /// </summary>
    /// <param name="songId">楽曲のID</param>
    /// <param name="difficulty">難易度</param>
    /// <param name="level">レベル</param>
    /// <exception cref="ArgumentNullException">levelがnullの場合</exception>
    public Chart(Guid songId, Difficulty difficulty, Level level)
    {
        ArgumentNullException.ThrowIfNull(level);

        SongId = songId;
        Difficulty = difficulty;
        Level = level;
    }

    /// <summary>
    /// 指定されたChartオブジェクトと等しいかどうかを判定します
    /// </summary>
    public bool Equals(Chart? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return SongId == other.SongId &&
               Difficulty == other.Difficulty &&
               Level.Equals(other.Level);
    }

    /// <summary>
    /// 指定されたオブジェクトと等しいかどうかを判定します
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as Chart);

    /// <summary>
    /// ハッシュコードを取得します
    /// </summary>
    public override int GetHashCode() =>
        HashCode.Combine(SongId, Difficulty, Level);

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
        $"SongId:{SongId} Difficulty:{Difficulty} Level:{Level}";
}