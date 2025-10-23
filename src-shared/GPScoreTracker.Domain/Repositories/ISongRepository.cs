using GPScoreTracker.Domain.Entities;

namespace GPScoreTracker.Domain.Repositories;

/// <summary>
/// Song�G���e�B�e�B�̃��|�W�g���C���^�[�t�F�[�X
/// </summary>
public interface ISongRepository
{
    /// <summary>
    /// �w�肳�ꂽID�̊y�Ȃ��擾���܂�
    /// </summary>
    /// <param name="songId">�y��ID</param>
    /// <returns>�y�Ȃ����݂���ꍇ��Song�I�u�W�F�N�g�A���݂��Ȃ��ꍇ��null</returns>
    Task<Song?> GetByIdAsync(Guid songId);

    /// <summary>
    /// ���ׂẴA�N�e�B�u�Ȋy�Ȃ��擾���܂�
    /// </summary>
    /// <returns>�A�N�e�B�u�Ȋy�Ȃ̃R���N�V����</returns>
    Task<IEnumerable<Song>> GetActiveSongsAsync();

    /// <summary>
    /// �w�肳�ꂽ�y�Ȃ�ǉ����܂�
    /// </summary>
    /// <param name="song">�ǉ�����y��</param>
    Task AddAsync(Song song);

    /// <summary>
    /// �w�肳�ꂽ�y�Ȃ��X�V���܂�
    /// </summary>
    /// <param name="song">�X�V����y��</param>
    Task UpdateAsync(Song song);
}