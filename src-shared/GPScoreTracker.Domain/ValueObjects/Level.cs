namespace GPScoreTracker.Domain.ValueObjects;

/// <summary>
/// DDR���ʂ̃��x����\���l�I�u�W�F�N�g�i1-19�j
/// </summary>
public sealed class Level : IEquatable<Level>
{
    /// <summary>
    /// ���x���̒l�i1�`19�j
    /// </summary>
    public int Value { get; }

    /// <summary>
    /// Level�l�I�u�W�F�N�g���쐬���܂�
    /// </summary>
    /// <param name="value">���x���̒l�i1�`19�͈͓̔��j</param>
    /// <exception cref="ArgumentOutOfRangeException">�l��1�`19�͈̔͊O�̏ꍇ</exception>
    public Level(int value)
    {
        if (value < 1 || value > 19)
        {
            throw new ArgumentOutOfRangeException(
             nameof(value),
                      value,
                "Level must be between 1 and 19.");
        }

        Value = value;
    }

    /// <summary>
    /// �w�肳�ꂽLevel�I�u�W�F�N�g�Ɠ��������ǂ����𔻒肵�܂�
    /// </summary>
    public bool Equals(Level? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    /// <summary>
    /// �w�肳�ꂽ�I�u�W�F�N�g�Ɠ��������ǂ����𔻒肵�܂�
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as Level);

    /// <summary>
    /// �n�b�V���R�[�h���擾���܂�
    /// </summary>
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>
    /// �������Z�q
    /// </summary>
    public static bool operator ==(Level? left, Level? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// �񓙉����Z�q
    /// </summary>
    public static bool operator !=(Level? left, Level? right) => !(left == right);

    /// <summary>
    /// ���x���̒l�𕶎���Ƃ��ĕԂ��܂�
    /// </summary>
    public override string ToString() => Value.ToString();
}
