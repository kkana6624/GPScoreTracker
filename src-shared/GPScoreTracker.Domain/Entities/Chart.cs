using GPScoreTracker.Domain.Enums;
using GPScoreTracker.Domain.ValueObjects;

namespace GPScoreTracker.Domain.Entities;

/// <summary>
/// DDR�̕��ʁi�y�ȂƓ�Փx�̑g�ݍ��킹�j��\���G���e�B�e�B
/// </summary>
public sealed class Chart : IEquatable<Chart>
{
    /// <summary>
    /// ���ʂ̈�ӂȎ��ʎq
    /// </summary>
    public Guid ChartId { get; private set; }

    /// <summary>
    /// �y�Ȃ�ID
    /// </summary>
    public Guid SongId { get; private set; }

    /// <summary>
    /// ��Փx
    /// </summary>
    public Difficulty Difficulty { get; private set; }

    /// <summary>
    /// ���x��
    /// </summary>
    public Level Level { get; private set; }

    /// <summary>
    /// Chart�G���e�B�e�B���쐬���܂�
    /// </summary>
    /// <param name="chartId">���ʂ̈�ӂȎ��ʎq</param>
    /// <param name="songId">�y�Ȃ�ID</param>
    /// <param name="difficulty">��Փx</param>
    /// <param name="level">���x��</param>
    /// <exception cref="ArgumentNullException">level��null�̏ꍇ</exception>
    public Chart(Guid chartId, Guid songId, Difficulty difficulty, Level level)
    {
        ArgumentNullException.ThrowIfNull(level);

        ChartId = chartId;
        SongId = songId;
        Difficulty = difficulty;
        Level = level;
    }

    /// <summary>
    /// �w�肳�ꂽChart�I�u�W�F�N�g�Ɠ��������ǂ����𔻒肵�܂�
    /// </summary>
    public bool Equals(Chart? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return ChartId == other.ChartId;
    }

    /// <summary>
    /// �w�肳�ꂽ�I�u�W�F�N�g�Ɠ��������ǂ����𔻒肵�܂�
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as Chart);

    /// <summary>
    /// �n�b�V���R�[�h���擾���܂�
    /// </summary>
    public override int GetHashCode() => ChartId.GetHashCode();

    /// <summary>
    /// �������Z�q
    /// </summary>
    public static bool operator ==(Chart? left, Chart? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// �񓙉����Z�q
    /// </summary>
    public static bool operator !=(Chart? left, Chart? right) => !(left == right);

    /// <summary>
    /// ���ʏ��𕶎���Ƃ��ĕԂ��܂�
    /// </summary>
    public override string ToString() =>
        $"ChartId:{ChartId} SongId:{SongId} Difficulty:{Difficulty} Level:{Level}";
}