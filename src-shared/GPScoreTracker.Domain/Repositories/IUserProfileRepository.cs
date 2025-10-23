using GPScoreTracker.Domain.Entities;

namespace GPScoreTracker.Domain.Repositories;

/// <summary>
/// UserProfileエンティティのリポジトリインターフェース
/// </summary>
public interface IUserProfileRepository
{
    /// <summary>
    /// 指定されたIDのユーザープロファイルを取得します
    /// </summary>
    /// <param name="userProfileId">ユーザープロファイルID</param>
    /// <returns>ユーザープロファイルが存在する場合はUserProfileオブジェクト、存在しない場合はnull</returns>
    Task<UserProfile?> GetByIdAsync(Guid userProfileId);

    /// <summary>
    /// 指定されたAPIキーハッシュに対応するユーザープロファイルを取得します
    /// </summary>
    /// <param name="apiKeyHash">APIキーのハッシュ値</param>
    /// <returns>対応するユーザープロファイル、存在しない場合はnull</returns>
    Task<UserProfile?> GetByApiKeyHashAsync(string apiKeyHash);

    /// <summary>
    /// 指定されたプロファイル名に対応するユーザープロファイルを取得します
    /// </summary>
    /// <param name="profileName">プロファイル名</param>
    /// <returns>対応するユーザープロファイル、存在しない場合はnull</returns>
    Task<UserProfile?> GetByProfileNameAsync(string profileName);

    /// <summary>
    /// 指定されたユーザープロファイルを追加します
    /// </summary>
    /// <param name="userProfile">追加するユーザープロファイル</param>
    Task AddAsync(UserProfile userProfile);

    /// <summary>
    /// 指定されたユーザープロファイルを更新します
    /// </summary>
    /// <param name="userProfile">更新するユーザープロファイル</param>
    Task UpdateAsync(UserProfile userProfile);
}