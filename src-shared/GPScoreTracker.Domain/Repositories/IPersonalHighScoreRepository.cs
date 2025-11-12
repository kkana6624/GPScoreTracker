using GPScoreTracker.Domain.Entities;
using GPScoreTracker.Domain.ValueObjects;

namespace GPScoreTracker.Domain.Repositories;

/// <summary>
/// PersonalHighScoreエンティティの永続化を抽象化するリポジトリインターフェース
/// </summary>
public interface IPersonalHighScoreRepository
{
    /// <summary>
    /// 特定のユーザーと譜面の自己ベストを取得します
    /// </summary>
    /// <param name="userProfileId">ユーザープロファイルID</param>
    /// <param name="chartIdentifier">譜面識別子</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>自己ベスト記録。存在しない場合はnull</returns>
    Task<PersonalHighScore?> GetByUserAndChartAsync(
        Guid userProfileId,
        ChartIdentifier chartIdentifier,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 自己ベストを追加します
    /// </summary>
    /// <param name="personalHighScore">追加する自己ベスト</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    Task AddAsync(
        PersonalHighScore personalHighScore,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 自己ベストを更新します
    /// </summary>
    /// <param name="personalHighScore">更新する自己ベスト</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    Task UpdateAsync(
        PersonalHighScore personalHighScore,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 特定ユーザーの全自己ベストを取得します
    /// </summary>
    /// <param name="userProfileId">ユーザープロファイルID</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>自己ベストのリスト</returns>
    Task<IReadOnlyList<PersonalHighScore>> GetAllByUserAsync(
        Guid userProfileId,
        CancellationToken cancellationToken = default);
}
