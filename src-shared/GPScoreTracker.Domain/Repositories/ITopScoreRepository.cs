using GPScoreTracker.Domain.Entities;
using GPScoreTracker.Domain.ValueObjects;

namespace GPScoreTracker.Domain.Repositories;

/// <summary>
/// TopScoreエンティティの永続化を抽象化するリポジトリインターフェース
/// </summary>
public interface ITopScoreRepository
{
    /// <summary>
    /// 特定の譜面のトップスコアを取得します
    /// </summary>
    /// <param name="chartIdentifier">譜面識別子</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>トップスコア。存在しない場合はnull</returns>
    Task<TopScore?> GetByChartAsync(
        ChartIdentifier chartIdentifier,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// トップスコアを追加します
    /// </summary>
    /// <param name="topScore">追加するトップスコア</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    Task AddAsync(
        TopScore topScore,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// トップスコアを更新します
    /// </summary>
    /// <param name="topScore">更新するトップスコア</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    Task UpdateAsync(
        TopScore topScore,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 全トップスコアを取得します
    /// </summary>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>トップスコアのリスト</returns>
    Task<IReadOnlyList<TopScore>> GetAllAsync(
        CancellationToken cancellationToken = default);
}
