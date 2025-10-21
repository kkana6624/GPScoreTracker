namespace GPScoreTracker.Domain.Enums;

/// <summary>
/// スコアの評価ランクを表す列挙型
/// DDRの実際のランク判定システムに基づく
/// </summary>
public enum Rank
{
    /// <summary>
    /// Eランク（クリア失敗）
    /// 途中でライフが尽きた場合。スコアは失敗時点のものが記録される
    /// </summary>
    E = 0,

    /// <summary>
    /// Dランク
    /// スコア: 0～589,990点
    /// </summary>
    D = 1,

    /// <summary>
    /// C-ランク
    /// スコア: 590,000～599,990点
    /// </summary>
    CMinus = 2,

    /// <summary>
    /// Cランク
    /// スコア: 600,000～649,990点
    /// </summary>
    C = 3,

    /// <summary>
    /// C+ランク
    /// スコア: 650,000～689,990点
    /// </summary>
    CPlus = 4,

    /// <summary>
    /// B-ランク
    /// スコア: 690,000～699,990点
    /// </summary>
    BMinus = 5,

    /// <summary>
    /// Bランク
    /// スコア: 700,000～749,990点
    /// </summary>
    B = 6,

    /// <summary>
    /// B+ランク
    /// スコア: 750,000～789,990点
    /// </summary>
    BPlus = 7,

    /// <summary>
    /// A-ランク
    /// スコア: 790,000～799,990点
    /// </summary>
    AMinus = 8,

    /// <summary>
    /// Aランク
    /// スコア: 800,000～849,990点
    /// </summary>
    A = 9,

    /// <summary>
    /// A+ランク
    /// スコア: 850,000～889,990点
    /// </summary>
    APlus = 10,

    /// <summary>
    /// AA-ランク
    /// スコア: 890,000～899,990点
    /// </summary>
    AAMinus = 11,

    /// <summary>
    /// AAランク
    /// スコア: 900,000～949,990点
    /// </summary>
    AA = 12,

    /// <summary>
    /// AA+ランク
    /// スコア: 950,000～989,990点
    /// </summary>
    AAPlus = 13,

    /// <summary>
    /// AAAランク（最高ランク）
    /// スコア: 990,000～1,000,000点
    /// </summary>
    AAA = 14
}
