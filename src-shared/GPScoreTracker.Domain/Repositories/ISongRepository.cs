using GPScoreTracker.Domain.Entities;

namespace GPScoreTracker.Domain.Repositories;

/// <summary>
/// Songエンティティのリポジトリインターフェース
/// </summary>
public interface ISongRepository
{
    /// <summary>
    /// 指定されたIDの楽曲を取得します
    /// </summary>
    /// <param name="songId">楽曲ID</param>
    /// <returns>楽曲が存在する場合はSongオブジェクト、存在しない場合はnull</returns>
    Task<Song?> GetByIdAsync(Guid songId);

    /// <summary>
    /// すべてのアクティブな楽曲を取得します
    /// </summary>
    /// <returns>アクティブな楽曲のコレクション</returns>
    Task<IEnumerable<Song>> GetActiveSongsAsync();

    /// <summary>
    /// 指定された楽曲を追加します
    /// </summary>
    /// <param name="song">追加する楽曲</param>
    Task AddAsync(Song song);

    /// <summary>
    /// 指定された楽曲を更新します
    /// </summary>
    /// <param name="song">更新する楽曲</param>
    Task UpdateAsync(Song song);
}