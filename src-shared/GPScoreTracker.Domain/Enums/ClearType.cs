namespace GPScoreTracker.Domain.Enums;

/// <summary>
/// プレイの達成状況を表す列挙型
/// </summary>
public enum ClearType
{
    /// <summary>
    /// 失敗（ゲームオーバー）
    /// </summary>
    Failed = 0,

    /// <summary>
    /// クリア
    /// </summary>
    Cleared = 1,

    /// <summary>
    /// ライフ4クリア
    /// </summary>
    LifeFourClear = 2,

    /// <summary>
    /// フルコンボ達成
    /// </summary>
    FullCombo = 3,

    /// <summary>
    /// Great以上のフルコンボ
    /// </summary>
    GreatFullCombo = 4,

    /// <summary>
    /// Perfect以上のフルコンボ
    /// </summary>
    PerfectFullCombo = 5,

    /// <summary>
    /// Marvelousのみのフルコンボ（最高評価）
    /// </summary>
    MarvelousFullCombo = 6
}
