using GPScoreTracker.Domain.Entities;
using GPScoreTracker.Domain.ValueObjects;
using System.Globalization;

namespace GPScoreTracker.Domain.Entities;

/// <summary>
/// �v���C���[�ɂ��1�񂲂Ƃ̃v���C������\���G���e�B�e�B
/// </summary>
public sealed class ScoreRecord : IEquatable<ScoreRecord>
{
    /// <summary>
    /// �X�R�A�L�^�̈�ӂȎ��ʎq
    /// </summary>
    public Guid ScoreRecordId { get; private set; }

    /// <summary>
    /// �v���C�������[�U�[��ID
    /// </summary>
    public Guid UserProfileId { get; private set; }

    /// <summary>
    /// �v���C��������
    /// </summary>
    public Chart Chart { get; private set; }

    /// <summary>
    /// �v���C���ʂ̃X�R�A�ڍ�
    /// </summary>
    public Score Score { get; private set; }

    /// <summary>
    /// �v���C��������
    /// </summary>
    public DateTime PlayedAt { get; private set; }

    /// <summary>
    /// ScoreRecord�G���e�B�e�B���쐬���܂�
    /// </summary>
    /// <param name="scoreRecordId">�X�R�A�L�^�̈�ӂȎ��ʎq</param>
    /// <param name="userProfileId">�v���C�������[�U�[��ID</param>
    /// <param name="chart">�v���C��������</param>
    /// <param name="score">�v���C���ʂ̃X�R�A�ڍ�</param>
    /// <param name="playedAt">�v���C��������</param>
    /// <exception cref="ArgumentNullException">chart�܂���score��null�̏ꍇ</exception>
    public ScoreRecord(Guid scoreRecordId, Guid userProfileId, Chart chart, Score score, DateTime playedAt)
    {
        ArgumentNullException.ThrowIfNull(chart);
        ArgumentNullException.ThrowIfNull(score);

        ScoreRecordId = scoreRecordId;
        UserProfileId = userProfileId;
        Chart = chart;
        Score = score;
        PlayedAt = playedAt;
    }

    /// <summary>
    /// �w�肳�ꂽScoreRecord�I�u�W�F�N�g�Ɠ��������ǂ����𔻒肵�܂�
    /// </summary>
    public bool Equals(ScoreRecord? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return ScoreRecordId == other.ScoreRecordId;
    }

    /// <summary>
    /// �w�肳�ꂽ�I�u�W�F�N�g�Ɠ��������ǂ����𔻒肵�܂�
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as ScoreRecord);

    /// <summary>
    /// �n�b�V���R�[�h���擾���܂�
    /// </summary>
    public override int GetHashCode() => ScoreRecordId.GetHashCode();

    /// <summary>
    /// �������Z�q
    /// </summary>
    public static bool operator ==(ScoreRecord? left, ScoreRecord? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// �񓙉����Z�q
    /// </summary>
    public static bool operator !=(ScoreRecord? left, ScoreRecord? right) => !(left == right);

    /// <summary>
    /// �X�R�A�L�^���𕶎���Ƃ��ĕԂ��܂�
    /// </summary>
    public override string ToString() =>
        $"ScoreRecordId:{ScoreRecordId} UserProfileId:{UserProfileId} PlayedAt:{PlayedAt.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)}";
}