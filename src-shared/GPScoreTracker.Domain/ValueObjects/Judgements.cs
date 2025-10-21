namespace GPScoreTracker.Domain.ValueObjects;

/// <summary>
/// プレイ中の判定回数を表す値オブジェクト
/// </summary>
public sealed class Judgements : IEquatable<Judgements>
{
    /// <summary>
    /// Marvelous判定の回数
    /// </summary>
    public int Marvelous { get; }

    /// <summary>
    /// Perfect判定の回数
    /// </summary>
    public int Perfect { get; }

    /// <summary>
    /// Great判定の回数
    /// </summary>
    public int Great { get; }

    /// <summary>
    /// Good判定の回数
    /// </summary>
    public int Good { get; }

    /// <summary>
    /// Miss判定の回数
    /// </summary>
    public int Miss { get; }

    /// <summary>
    /// 総ノート数（すべての判定の合計）
    /// </summary>
    public int TotalNotes => Marvelous + Perfect + Great + Good + Miss;

    /// <summary>
    /// Judgements値オブジェクトを作成します
    /// </summary>
    /// <param name="marvelous">Marvelous判定の回数</param>
    /// <param name="perfect">Perfect判定の回数</param>
    /// <param name="great">Great判定の回数</param>
    /// <param name="good">Good判定の回数</param>
    /// <param name="miss">Miss判定の回数</param>
    /// <exception cref="ArgumentOutOfRangeException">いずれかの判定回数が負の値の場合</exception>
    public Judgements(int marvelous, int perfect, int great, int good, int miss)
    {
        if (marvelous < 0 || perfect < 0 || great < 0 || good < 0 || miss < 0)
        {
            throw new ArgumentOutOfRangeException(
        "All judgement counts must be non-negative.");
        }

        Marvelous = marvelous;
        Perfect = perfect;
        Great = great;
        Good = good;
        Miss = miss;
    }

    /// <summary>
    /// 指定されたJudgementsオブジェクトと等しいかどうかを判定します
    /// </summary>
    public bool Equals(Judgements? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Marvelous == other.Marvelous &&
            Perfect == other.Perfect &&
            Great == other.Great &&
            Good == other.Good &&
            Miss == other.Miss;
    }

    /// <summary>
    /// 指定されたオブジェクトと等しいかどうかを判定します
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as Judgements);

    /// <summary>
    /// ハッシュコードを取得します
    /// </summary>
    public override int GetHashCode() =>
        HashCode.Combine(Marvelous, Perfect, Great, Good, Miss);

    /// <summary>
    /// 等価演算子
    /// </summary>
    public static bool operator ==(Judgements? left, Judgements? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// 非等価演算子
    /// </summary>
    public static bool operator !=(Judgements? left, Judgements? right) => !(left == right);

    /// <summary>
    /// 判定情報を文字列として返します
    /// </summary>
    public override string ToString() =>
        $"Marvelous:{Marvelous} Perfect:{Perfect} Great:{Great} Good:{Good} Miss:{Miss}";
}
