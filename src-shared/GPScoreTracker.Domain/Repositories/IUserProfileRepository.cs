using GPScoreTracker.Domain.Entities;

namespace GPScoreTracker.Domain.Repositories;

/// <summary>
/// UserProfile�G���e�B�e�B�̃��|�W�g���C���^�[�t�F�[�X
/// </summary>
public interface IUserProfileRepository
{
    /// <summary>
    /// �w�肳�ꂽID�̃��[�U�[�v���t�@�C�����擾���܂�
    /// </summary>
    /// <param name="userProfileId">���[�U�[�v���t�@�C��ID</param>
    /// <returns>���[�U�[�v���t�@�C�������݂���ꍇ��UserProfile�I�u�W�F�N�g�A���݂��Ȃ��ꍇ��null</returns>
    Task<UserProfile?> GetByIdAsync(Guid userProfileId);

    /// <summary>
    /// �w�肳�ꂽAPI�L�[�n�b�V���ɑΉ����郆�[�U�[�v���t�@�C�����擾���܂�
    /// </summary>
    /// <param name="apiKeyHash">API�L�[�̃n�b�V���l</param>
    /// <returns>�Ή����郆�[�U�[�v���t�@�C���A���݂��Ȃ��ꍇ��null</returns>
    Task<UserProfile?> GetByApiKeyHashAsync(string apiKeyHash);

    /// <summary>
    /// �w�肳�ꂽ�v���t�@�C�����ɑΉ����郆�[�U�[�v���t�@�C�����擾���܂�
    /// </summary>
    /// <param name="profileName">�v���t�@�C����</param>
    /// <returns>�Ή����郆�[�U�[�v���t�@�C���A���݂��Ȃ��ꍇ��null</returns>
    Task<UserProfile?> GetByProfileNameAsync(string profileName);

    /// <summary>
    /// �w�肳�ꂽ���[�U�[�v���t�@�C����ǉ����܂�
    /// </summary>
    /// <param name="userProfile">�ǉ����郆�[�U�[�v���t�@�C��</param>
    Task AddAsync(UserProfile userProfile);

    /// <summary>
    /// �w�肳�ꂽ���[�U�[�v���t�@�C�����X�V���܂�
    /// </summary>
    /// <param name="userProfile">�X�V���郆�[�U�[�v���t�@�C��</param>
    Task UpdateAsync(UserProfile userProfile);
}