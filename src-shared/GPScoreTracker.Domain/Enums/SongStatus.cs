namespace GPScoreTracker.Domain.Enums;

/// <summary>
/// 楽曲の状態を表す列挙型
/// </summary>
public enum SongStatus
{
    /// <summary>
    /// 有効な楽曲
    /// </summary>
    Active = 0,

    /// <summary>
    /// 削除された楽曲
    /// </summary>
    Deleted = 1
}
