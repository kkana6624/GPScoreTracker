using GPScoreTracker.Domain.Entities;

namespace GPScoreTracker.Domain.Repositories;

/// <summary>
/// ScoreRecord�G���e�B�e�B�̃��|�W�g���C���^�[�t�F�[�X
/// </summary>
public interface IScoreRecordRepository
{
    /// <summary>
    /// �w�肳�ꂽID�̃X�R�A�L�^���擾���܂�
    /// </summary>
    /// <param name="scoreRecordId">�X�R�A�L�^ID</param>
    /// <returns>�X�R�A�L�^�����݂���ꍇ��ScoreRecord�I�u�W�F�N�g�A���݂��Ȃ��ꍇ��null</returns>
    Task<ScoreRecord?> GetByIdAsync(Guid scoreRecordId);

    /// <summary>
    /// �w�肳�ꂽ���[�U�[�̂��ׂẴX�R�A�L�^���擾���܂�
    /// </summary>
    /// <param name="userProfileId">���[�U�[�v���t�@�C��ID</param>
    /// <returns>�Y�����[�U�[�̂��ׂẴX�R�A�L�^�̃R���N�V����</returns>
    Task<IEnumerable<ScoreRecord>> GetByUserProfileIdAsync(Guid userProfileId);

    /// <summary>
    /// �w�肳�ꂽ���ʂ̂��ׂẴX�R�A�L�^���擾���܂�
    /// </summary>
    /// <param name="chartId">����ID</param>
    /// <returns>�Y�����ʂ̂��ׂẴX�R�A�L�^�̃R���N�V����</returns>
    Task<IEnumerable<ScoreRecord>> GetByChartIdAsync(Guid chartId);

    /// <summary>
    /// �w�肳�ꂽ���[�U�[�ƕ��ʂ̃X�R�A�L�^���擾���܂�
    /// </summary>
    /// <param name="userProfileId">���[�U�[�v���t�@�C��ID</param>
    /// <param name="chartId">����ID</param>
    /// <returns>�Y���̃X�R�A�L�^�̃R���N�V����</returns>
    Task<IEnumerable<ScoreRecord>> GetByUserProfileIdAndChartIdAsync(Guid userProfileId, Guid chartId);

    /// <summary>
    /// �w�肳�ꂽ���ԓ��̃X�R�A�L�^���擾���܂�
    /// </summary>
    /// <param name="userProfileId">���[�U�[�v���t�@�C��ID</param>
    /// <param name="fromDate">�J�n����</param>
    /// <param name="toDate">�I������</param>
    /// <returns>�Y�����ԓ��̃X�R�A�L�^�̃R���N�V����</returns>
    Task<IEnumerable<ScoreRecord>> GetByUserProfileIdAndDateRangeAsync(Guid userProfileId, DateTime fromDate, DateTime toDate);

    /// <summary>
    /// �w�肳�ꂽ�X�R�A�L�^��ǉ����܂�
    /// </summary>
    /// <param name="scoreRecord">�ǉ�����X�R�A�L�^</param>
    Task AddAsync(ScoreRecord scoreRecord);

    /// <summary>
    /// �w�肳�ꂽ�X�R�A�L�^���X�V���܂�
    /// </summary>
    /// <param name="scoreRecord">�X�V����X�R�A�L�^</param>
    Task UpdateAsync(ScoreRecord scoreRecord);

    /// <summary>
    /// �w�肳�ꂽ�X�R�A�L�^���폜���܂�
    /// </summary>
    /// <param name="scoreRecordId">�폜����X�R�A�L�^ID</param>
    Task DeleteAsync(Guid scoreRecordId);
}