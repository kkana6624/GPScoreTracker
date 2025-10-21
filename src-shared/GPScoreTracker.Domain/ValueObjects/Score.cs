using GPScoreTracker.Domain.Enums;

namespace GPScoreTracker.Domain.ValueObjects;

/// <summary>
/// DDR��1��̃v���C���ʂ̃X�R�A�ڍׂ�\���l�I�u�W�F�N�g
/// </summary>
public sealed class Score : IEquatable<Score>
{
    /// <summary>
    /// 100���_���_�̃X�R�A
    /// </summary>
    public int Points { get; }

    /// <summary>
    /// EX�X�R�A�i���育�Ƃ̔z�_�Ɋ�Â��X�R�A�j
    /// </summary>
    public int EXScore { get; }

    /// <summary>
    /// �]�������N
    /// </summary>
    public Rank Rank { get; }

    /// <summary>
    /// ���育�Ƃ̉�
    /// </summary>
    public Judgements Judgements { get; }

    /// <summary>
    /// �ő�R���{��
    /// </summary>
    public int MaxCombo { get; }

    /// <summary>
    /// �N���A�^�C�v�i�B���󋵁j
    /// </summary>
    public ClearType ClearType { get; }

    /// <summary>
    /// Score�l�I�u�W�F�N�g���쐬���܂�
    /// </summary>
    /// <param name="points">100���_���_�̃X�R�A�i0�`1,000,000�j</param>
    /// <param name="exScore">EX�X�R�A�i0�ȏ�j</param>
    /// <param name="rank">�]�������N</param>
    /// <param name="judgements">���育�Ƃ̉�</param>
    /// <param name="maxCombo">�ő�R���{���i0�ȏ�j</param>
    /// <param name="clearType">�N���A�^�C�v</param>
    /// <exception cref="ArgumentOutOfRangeException">���l�p�����[�^���͈͊O�̏ꍇ</exception>
    /// <exception cref="ArgumentNullException">judgements��null�̏ꍇ</exception>
    public Score(
        int points,
        int exScore,
        Rank rank,
        Judgements judgements,
        int maxCombo,
        ClearType clearType)
    {
        if (points < 0 || points > 1_000_000)
        {
            throw new ArgumentOutOfRangeException(
         nameof(points),
                 points,
              "Points must be between 0 and 1,000,000.");
        }

        if (exScore < 0)
        {
            throw new ArgumentOutOfRangeException(
                 nameof(exScore),
              exScore,
                "EXScore must be non-negative.");
        }

        if (maxCombo < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(maxCombo),
         maxCombo,
         "MaxCombo must be non-negative.");
        }

        ArgumentNullException.ThrowIfNull(judgements);

        Points = points;
        EXScore = exScore;
        Rank = rank;
        Judgements = judgements;
        MaxCombo = maxCombo;
        ClearType = clearType;
    }

    /// <summary>
    /// �w�肳�ꂽScore�I�u�W�F�N�g�Ɠ��������ǂ����𔻒肵�܂�
    /// </summary>
    public bool Equals(Score? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Points == other.Points &&
        EXScore == other.EXScore &&
        Rank == other.Rank &&
        Judgements.Equals(other.Judgements) &&
        MaxCombo == other.MaxCombo &&
        ClearType == other.ClearType;
    }

    /// <summary>
    /// �w�肳�ꂽ�I�u�W�F�N�g�Ɠ��������ǂ����𔻒肵�܂�
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as Score);

    /// <summary>
    /// �n�b�V���R�[�h���擾���܂�
    /// </summary>
    public override int GetHashCode() =>
        HashCode.Combine(Points, EXScore, Rank, Judgements, MaxCombo, ClearType);

    /// <summary>
    /// �������Z�q
    /// </summary>
    public static bool operator ==(Score? left, Score? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// �񓙉����Z�q
    /// </summary>
    public static bool operator !=(Score? left, Score? right) => !(left == right);

    /// <summary>
    /// �X�R�A���𕶎���Ƃ��ĕԂ��܂�
    /// </summary>
    public override string ToString() =>
        $"Points:{Points:N0} Rank:{Rank} ClearType:{ClearType}";
}
