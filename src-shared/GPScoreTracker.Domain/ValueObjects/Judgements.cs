namespace GPScoreTracker.Domain.ValueObjects;

/// <summary>
/// �v���C���̔���񐔂�\���l�I�u�W�F�N�g
/// </summary>
public sealed class Judgements : IEquatable<Judgements>
{
    /// <summary>
    /// Marvelous����̉�
    /// </summary>
    public int Marvelous { get; }

    /// <summary>
    /// Perfect����̉�
    /// </summary>
    public int Perfect { get; }

    /// <summary>
    /// Great����̉�
    /// </summary>
    public int Great { get; }

    /// <summary>
    /// Good����̉�
    /// </summary>
    public int Good { get; }

    /// <summary>
    /// Miss����̉�
    /// </summary>
    public int Miss { get; }

    /// <summary>
    /// ���m�[�g���i���ׂĂ̔���̍��v�j
    /// </summary>
    public int TotalNotes => Marvelous + Perfect + Great + Good + Miss;

    /// <summary>
    /// Judgements�l�I�u�W�F�N�g���쐬���܂�
    /// </summary>
    /// <param name="marvelous">Marvelous����̉�</param>
    /// <param name="perfect">Perfect����̉�</param>
    /// <param name="great">Great����̉�</param>
    /// <param name="good">Good����̉�</param>
    /// <param name="miss">Miss����̉�</param>
    /// <exception cref="ArgumentOutOfRangeException">�����ꂩ�̔���񐔂����̒l�̏ꍇ</exception>
    public Judgements(int marvelous, int perfect, int great, int good, int miss)
    {
        if (marvelous < 0 || perfect < 0 || great < 0 || good < 0 || miss < 0)
        {
            throw new ArgumentOutOfRangeException(
        "All judgement counts must be non-negative.");
        }

        Marvelous = marvelous;
        Perfect = perfect;
        Great = great;
        Good = good;
        Miss = miss;
    }

    /// <summary>
    /// �w�肳�ꂽJudgements�I�u�W�F�N�g�Ɠ��������ǂ����𔻒肵�܂�
    /// </summary>
    public bool Equals(Judgements? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Marvelous == other.Marvelous &&
            Perfect == other.Perfect &&
            Great == other.Great &&
            Good == other.Good &&
            Miss == other.Miss;
    }

    /// <summary>
    /// �w�肳�ꂽ�I�u�W�F�N�g�Ɠ��������ǂ����𔻒肵�܂�
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as Judgements);

    /// <summary>
    /// �n�b�V���R�[�h���擾���܂�
    /// </summary>
    public override int GetHashCode() =>
        HashCode.Combine(Marvelous, Perfect, Great, Good, Miss);

    /// <summary>
    /// �������Z�q
    /// </summary>
    public static bool operator ==(Judgements? left, Judgements? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// �񓙉����Z�q
    /// </summary>
    public static bool operator !=(Judgements? left, Judgements? right) => !(left == right);

    /// <summary>
    /// ������𕶎���Ƃ��ĕԂ��܂�
    /// </summary>
    public override string ToString() =>
        $"Marvelous:{Marvelous} Perfect:{Perfect} Great:{Great} Good:{Good} Miss:{Miss}";
}
