namespace GPScoreTracker.Domain.Entities;

/// <summary>
/// システムにスコアを記録するユーザーを表すエンティティ
/// </summary>
public sealed class UserProfile : IEquatable<UserProfile>
{
    /// <summary>
    /// ユーザーの一意な識別子
    /// </summary>
    public Guid UserProfileId { get; private set; }

    /// <summary>
    /// プロファイル名
    /// </summary>
    public string ProfileName { get; private set; }

    /// <summary>
    /// APIキーのハッシュ値
    /// </summary>
    public string ApiKeyHash { get; private set; }

    /// <summary>
    /// UserProfileエンティティを作成します
    /// </summary>
    /// <param name="userProfileId">ユーザーの一意な識別子</param>
    /// <param name="profileName">プロファイル名</param>
    /// <param name="apiKeyHash">APIキーのハッシュ値</param>
    /// <exception cref="ArgumentNullException">profileNameまたはapiKeyHashがnullの場合</exception>
    /// <exception cref="ArgumentException">profileNameまたはapiKeyHashが空文字列の場合</exception>
    public UserProfile(Guid userProfileId, string profileName, string apiKeyHash)
    {
        ArgumentNullException.ThrowIfNull(profileName);
        ArgumentNullException.ThrowIfNull(apiKeyHash);

        if (string.IsNullOrWhiteSpace(profileName))
        {
            throw new ArgumentException("ProfileName cannot be empty.", nameof(profileName));
        }

        if (string.IsNullOrWhiteSpace(apiKeyHash))
        {
            throw new ArgumentException("ApiKeyHash cannot be empty.", nameof(apiKeyHash));
        }

        UserProfileId = userProfileId;
        ProfileName = profileName;
        ApiKeyHash = apiKeyHash;
    }

    /// <summary>
    /// 指定されたUserProfileオブジェクトと等しいかどうかを判定します
    /// </summary>
    public bool Equals(UserProfile? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return UserProfileId == other.UserProfileId;
    }

    /// <summary>
    /// 指定されたオブジェクトと等しいかどうかを判定します
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as UserProfile);

    /// <summary>
    /// ハッシュコードを取得します
    /// </summary>
    public override int GetHashCode() => UserProfileId.GetHashCode();

    /// <summary>
    /// 等価演算子
    /// </summary>
    public static bool operator ==(UserProfile? left, UserProfile? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// 非等価演算子
    /// </summary>
    public static bool operator !=(UserProfile? left, UserProfile? right) => !(left == right);

    /// <summary>
    /// ユーザープロファイル情報を文字列として返します
    /// </summary>
    public override string ToString() =>
        $"UserProfileId:{UserProfileId} ProfileName:{ProfileName} ApiKeyHash:{ApiKeyHash}";
}