using GPScoreTracker.Domain.Enums;

namespace GPScoreTracker.Domain.ValueObjects;

/// <summary>
/// DDR�̕��ʁi�y�ȂƓ�Փx�̑g�ݍ��킹�j��\���l�I�u�W�F�N�g
/// </summary>
public sealed class Chart : IEquatable<Chart>
{
    /// <summary>
    /// �y�Ȃ�ID
    /// </summary>
    public Guid SongId { get; }

    /// <summary>
    /// ��Փx
    /// </summary>
    public Difficulty Difficulty { get; }

    /// <summary>
    /// ���x��
    /// </summary>
    public Level Level { get; }

    /// <summary>
    /// Chart�l�I�u�W�F�N�g���쐬���܂�
    /// </summary>
    /// <param name="songId">�y�Ȃ�ID</param>
    /// <param name="difficulty">��Փx</param>
    /// <param name="level">���x��</param>
    /// <exception cref="ArgumentNullException">level��null�̏ꍇ</exception>
    public Chart(Guid songId, Difficulty difficulty, Level level)
    {
        ArgumentNullException.ThrowIfNull(level);

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
        return SongId == other.SongId &&
               Difficulty == other.Difficulty &&
               Level.Equals(other.Level);
    }

    /// <summary>
    /// �w�肳�ꂽ�I�u�W�F�N�g�Ɠ��������ǂ����𔻒肵�܂�
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as Chart);

    /// <summary>
    /// �n�b�V���R�[�h���擾���܂�
    /// </summary>
    public override int GetHashCode() =>
        HashCode.Combine(SongId, Difficulty, Level);

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
        $"SongId:{SongId} Difficulty:{Difficulty} Level:{Level}";
}