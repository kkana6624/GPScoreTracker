using GPScoreTracker.Domain.Entities;
using GPScoreTracker.Domain.Enums;

namespace GPScoreTracker.Domain.Repositories;

/// <summary>
/// Chart�G���e�B�e�B�̃��|�W�g���C���^�[�t�F�[�X
/// </summary>
public interface IChartRepository
{
    /// <summary>
    /// �w�肳�ꂽID�̕��ʂ��擾���܂�
    /// </summary>
    /// <param name="chartId">����ID</param>
    /// <returns>���ʂ����݂���ꍇ��Chart�I�u�W�F�N�g�A���݂��Ȃ��ꍇ��null</returns>
    Task<Chart?> GetByIdAsync(Guid chartId);

    /// <summary>
    /// �w�肳�ꂽ�y��ID�Ɠ�Փx�ɑΉ����镈�ʂ��擾���܂�
    /// </summary>
    /// <param name="songId">�y��ID</param>
    /// <param name="difficulty">��Փx</param>
    /// <returns>�Ή����镈�ʁA���݂��Ȃ��ꍇ��null</returns>
    Task<Chart?> GetBySongIdAndDifficultyAsync(Guid songId, Difficulty difficulty);

    /// <summary>
    /// �w�肳�ꂽ�y��ID�̂��ׂĂ̕��ʂ��擾���܂�
    /// </summary>
    /// <param name="songId">�y��ID</param>
    /// <returns>�Y���y�Ȃ̂��ׂĂ̕��ʂ̃R���N�V����</returns>
    Task<IEnumerable<Chart>> GetBySongIdAsync(Guid songId);

    /// <summary>
    /// �w�肳�ꂽ��Փx�̂��ׂĂ̕��ʂ��擾���܂�
    /// </summary>
    /// <param name="difficulty">��Փx</param>
    /// <returns>�Y����Փx�̂��ׂĂ̕��ʂ̃R���N�V����</returns>
    Task<IEnumerable<Chart>> GetByDifficultyAsync(Difficulty difficulty);

    /// <summary>
    /// �w�肳�ꂽ���ʂ�ǉ����܂�
    /// </summary>
    /// <param name="chart">�ǉ����镈��</param>
    Task AddAsync(Chart chart);

    /// <summary>
    /// �w�肳�ꂽ���ʂ��X�V���܂�
    /// </summary>
    /// <param name="chart">�X�V���镈��</param>
    Task UpdateAsync(Chart chart);
}