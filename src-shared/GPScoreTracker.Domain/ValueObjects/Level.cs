namespace GPScoreTracker.Domain.ValueObjects;

/// <summary>
/// DDR譜面のレベルを表す値オブジェクト（1-19）
/// </summary>
public sealed class Level : IEquatable<Level>
{
    /// <summary>
    /// レベルの値（1～19）
    /// </summary>
    public int Value { get; }

    /// <summary>
    /// Level値オブジェクトを作成します
    /// </summary>
    /// <param name="value">レベルの値（1～19の範囲内）</param>
    /// <exception cref="ArgumentOutOfRangeException">値が1～19の範囲外の場合</exception>
    public Level(int value)
    {
        if (value < 1 || value > 19)
        {
            throw new ArgumentOutOfRangeException(
             nameof(value),
                      value,
                "Level must be between 1 and 19.");
        }

        Value = value;
    }

    /// <summary>
    /// 指定されたLevelオブジェクトと等しいかどうかを判定します
    /// </summary>
    public bool Equals(Level? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    /// <summary>
    /// 指定されたオブジェクトと等しいかどうかを判定します
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as Level);

    /// <summary>
    /// ハッシュコードを取得します
    /// </summary>
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>
    /// 等価演算子
    /// </summary>
    public static bool operator ==(Level? left, Level? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// 非等価演算子
    /// </summary>
    public static bool operator !=(Level? left, Level? right) => !(left == right);

    /// <summary>
    /// レベルの値を文字列として返します
    /// </summary>
    public override string ToString() => Value.ToString();
}
