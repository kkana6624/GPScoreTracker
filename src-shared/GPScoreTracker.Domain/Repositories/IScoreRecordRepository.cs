using GPScoreTracker.Domain.Entities;

namespace GPScoreTracker.Domain.Repositories;

/// <summary>
/// ScoreRecordエンティティのリポジトリインターフェース
/// </summary>
public interface IScoreRecordRepository
{
    /// <summary>
    /// 指定されたIDのスコア記録を取得します
    /// </summary>
    /// <param name="scoreRecordId">スコア記録ID</param>
    /// <returns>スコア記録が存在する場合はScoreRecordオブジェクト、存在しない場合はnull</returns>
    Task<ScoreRecord?> GetByIdAsync(Guid scoreRecordId);

    /// <summary>
    /// 指定されたユーザーのすべてのスコア記録を取得します
    /// </summary>
    /// <param name="userProfileId">ユーザープロファイルID</param>
    /// <returns>該当ユーザーのすべてのスコア記録のコレクション</returns>
    Task<IEnumerable<ScoreRecord>> GetByUserProfileIdAsync(Guid userProfileId);

    /// <summary>
    /// 指定された譜面のすべてのスコア記録を取得します
    /// </summary>
    /// <param name="chartId">譜面ID</param>
    /// <returns>該当譜面のすべてのスコア記録のコレクション</returns>
    Task<IEnumerable<ScoreRecord>> GetByChartIdAsync(Guid chartId);

    /// <summary>
    /// 指定されたユーザーと譜面のスコア記録を取得します
    /// </summary>
    /// <param name="userProfileId">ユーザープロファイルID</param>
    /// <param name="chartId">譜面ID</param>
    /// <returns>該当のスコア記録のコレクション</returns>
    Task<IEnumerable<ScoreRecord>> GetByUserProfileIdAndChartIdAsync(Guid userProfileId, Guid chartId);

    /// <summary>
    /// 指定された期間内のスコア記録を取得します
    /// </summary>
    /// <param name="userProfileId">ユーザープロファイルID</param>
    /// <param name="fromDate">開始日時</param>
    /// <param name="toDate">終了日時</param>
    /// <returns>該当期間内のスコア記録のコレクション</returns>
    Task<IEnumerable<ScoreRecord>> GetByUserProfileIdAndDateRangeAsync(Guid userProfileId, DateTime fromDate, DateTime toDate);

    /// <summary>
    /// 指定されたスコア記録を追加します
    /// </summary>
    /// <param name="scoreRecord">追加するスコア記録</param>
    Task AddAsync(ScoreRecord scoreRecord);

    /// <summary>
    /// 指定されたスコア記録を更新します
    /// </summary>
    /// <param name="scoreRecord">更新するスコア記録</param>
    Task UpdateAsync(ScoreRecord scoreRecord);

    /// <summary>
    /// 指定されたスコア記録を削除します
    /// </summary>
    /// <param name="scoreRecordId">削除するスコア記録ID</param>
    Task DeleteAsync(Guid scoreRecordId);
}