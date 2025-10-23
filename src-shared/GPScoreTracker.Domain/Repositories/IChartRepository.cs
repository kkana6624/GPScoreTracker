using GPScoreTracker.Domain.Entities;
using GPScoreTracker.Domain.Enums;

namespace GPScoreTracker.Domain.Repositories;

/// <summary>
/// Chartエンティティのリポジトリインターフェース
/// </summary>
public interface IChartRepository
{
    /// <summary>
    /// 指定されたIDの譜面を取得します
    /// </summary>
    /// <param name="chartId">譜面ID</param>
    /// <returns>譜面が存在する場合はChartオブジェクト、存在しない場合はnull</returns>
    Task<Chart?> GetByIdAsync(Guid chartId);

    /// <summary>
    /// 指定された楽曲IDと難易度に対応する譜面を取得します
    /// </summary>
    /// <param name="songId">楽曲ID</param>
    /// <param name="difficulty">難易度</param>
    /// <returns>対応する譜面、存在しない場合はnull</returns>
    Task<Chart?> GetBySongIdAndDifficultyAsync(Guid songId, Difficulty difficulty);

    /// <summary>
    /// 指定された楽曲IDのすべての譜面を取得します
    /// </summary>
    /// <param name="songId">楽曲ID</param>
    /// <returns>該当楽曲のすべての譜面のコレクション</returns>
    Task<IEnumerable<Chart>> GetBySongIdAsync(Guid songId);

    /// <summary>
    /// 指定された難易度のすべての譜面を取得します
    /// </summary>
    /// <param name="difficulty">難易度</param>
    /// <returns>該当難易度のすべての譜面のコレクション</returns>
    Task<IEnumerable<Chart>> GetByDifficultyAsync(Difficulty difficulty);

    /// <summary>
    /// 指定された譜面を追加します
    /// </summary>
    /// <param name="chart">追加する譜面</param>
    Task AddAsync(Chart chart);

    /// <summary>
    /// 指定された譜面を更新します
    /// </summary>
    /// <param name="chart">更新する譜面</param>
    Task UpdateAsync(Chart chart);
}