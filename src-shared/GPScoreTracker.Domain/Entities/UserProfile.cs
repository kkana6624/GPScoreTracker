namespace GPScoreTracker.Domain.Entities;

/// <summary>
/// �V�X�e���ɃX�R�A���L�^���郆�[�U�[��\���G���e�B�e�B
/// </summary>
public sealed class UserProfile : IEquatable<UserProfile>
{
    /// <summary>
    /// ���[�U�[�̈�ӂȎ��ʎq
    /// </summary>
    public Guid UserProfileId { get; private set; }

    /// <summary>
    /// �v���t�@�C����
    /// </summary>
    public string ProfileName { get; private set; }

    /// <summary>
    /// API�L�[�̃n�b�V���l
    /// </summary>
    public string ApiKeyHash { get; private set; }

    /// <summary>
    /// UserProfile�G���e�B�e�B���쐬���܂�
    /// </summary>
    /// <param name="userProfileId">���[�U�[�̈�ӂȎ��ʎq</param>
    /// <param name="profileName">�v���t�@�C����</param>
    /// <param name="apiKeyHash">API�L�[�̃n�b�V���l</param>
    /// <exception cref="ArgumentNullException">profileName�܂���apiKeyHash��null�̏ꍇ</exception>
    /// <exception cref="ArgumentException">profileName�܂���apiKeyHash���󕶎���̏ꍇ</exception>
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
    /// �w�肳�ꂽUserProfile�I�u�W�F�N�g�Ɠ��������ǂ����𔻒肵�܂�
    /// </summary>
    public bool Equals(UserProfile? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return UserProfileId == other.UserProfileId;
    }

    /// <summary>
    /// �w�肳�ꂽ�I�u�W�F�N�g�Ɠ��������ǂ����𔻒肵�܂�
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as UserProfile);

    /// <summary>
    /// �n�b�V���R�[�h���擾���܂�
    /// </summary>
    public override int GetHashCode() => UserProfileId.GetHashCode();

    /// <summary>
    /// �������Z�q
    /// </summary>
    public static bool operator ==(UserProfile? left, UserProfile? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// �񓙉����Z�q
    /// </summary>
    public static bool operator !=(UserProfile? left, UserProfile? right) => !(left == right);

    /// <summary>
    /// ���[�U�[�v���t�@�C�����𕶎���Ƃ��ĕԂ��܂�
    /// </summary>
    public override string ToString() =>
        $"UserProfileId:{UserProfileId} ProfileName:{ProfileName} ApiKeyHash:{ApiKeyHash}";
}