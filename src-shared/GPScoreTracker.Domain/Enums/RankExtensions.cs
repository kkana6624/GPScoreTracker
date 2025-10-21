namespace GPScoreTracker.Domain.Enums;

/// <summary>
/// Rank�񋓌^�̊g�����\�b�h
/// </summary>
public static class RankExtensions
{
    /// <summary>
    /// �X�R�A�ƃN���A��Ԃ��烉���N�𔻒肷��
    /// </summary>
    /// <param name="points">�l���X�R�A�i0�`1,000,000�j</param>
    /// <param name="isCleared">�N���A�������ǂ���</param>
    /// <returns>���肳�ꂽ�����N</returns>
    /// <exception cref="ArgumentOutOfRangeException">�X�R�A���͈͊O�̏ꍇ</exception>
    public static Rank DetermineRank(int points, bool isCleared)
    {
        if (points < 0 || points > 1_000_000)
        {
            throw new ArgumentOutOfRangeException(
            nameof(points),
            points,
            "Points must be between 0 and 1,000,000.");
        }

        // �N���A���s�̏ꍇ�͖�������E�����N
        if (!isCleared)
        {
            return Rank.E;
        }

        // �N���A�������̃����N����
        return points switch
        {
            >= 990_000 => Rank.AAA,
            >= 950_000 => Rank.AAPlus,
            >= 900_000 => Rank.AA,
            >= 890_000 => Rank.AAMinus,
            >= 850_000 => Rank.APlus,
            >= 800_000 => Rank.A,
            >= 790_000 => Rank.AMinus,
            >= 750_000 => Rank.BPlus,
            >= 700_000 => Rank.B,
            >= 690_000 => Rank.BMinus,
            >= 650_000 => Rank.CPlus,
            >= 600_000 => Rank.C,
            >= 590_000 => Rank.CMinus,
            _ => Rank.D
        };
    }

    /// <summary>
    /// �����N�̕\�������擾����
    /// </summary>
    /// <param name="rank">�����N</param>
    /// <returns>�\���p�̕�����</returns>
    public static string ToDisplayString(this Rank rank)
    {
        return rank switch
        {
            Rank.E => "E",
            Rank.D => "D",
            Rank.CMinus => "C-",
            Rank.C => "C",
            Rank.CPlus => "C+",
            Rank.BMinus => "B-",
            Rank.B => "B",
            Rank.BPlus => "B+",
            Rank.AMinus => "A-",
            Rank.A => "A",
            Rank.APlus => "A+",
            Rank.AAMinus => "AA-",
            Rank.AA => "AA",
            Rank.AAPlus => "AA+",
            Rank.AAA => "AAA",
            _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, "Unknown rank value.")
        };
    }

    /// <summary>
    /// �����N�̍ŏ��X�R�A���擾����
    /// </summary>
    /// <param name="rank">�����N</param>
    /// <returns>���̃����N��B�����邽�߂̍ŏ��X�R�A</returns>
    public static int GetMinimumPoints(this Rank rank)
    {
        return rank switch
        {
            Rank.E => 0,
            Rank.D => 0,
            Rank.CMinus => 590_000,
            Rank.C => 600_000,
            Rank.CPlus => 650_000,
            Rank.BMinus => 690_000,
            Rank.B => 700_000,
            Rank.BPlus => 750_000,
            Rank.AMinus => 790_000,
            Rank.A => 800_000,
            Rank.APlus => 850_000,
            Rank.AAMinus => 890_000,
            Rank.AA => 900_000,
            Rank.AAPlus => 950_000,
            Rank.AAA => 990_000,
            _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, "Unknown rank value.")
        };
    }

    /// <summary>
    /// �����N�̍ő�X�R�A���擾����
    /// </summary>
    /// <param name="rank">�����N</param>
    /// <returns>���̃����N�̍ő�X�R�A</returns>
    public static int GetMaximumPoints(this Rank rank)
    {
        return rank switch
        {
            Rank.E => 1_000_000, // E�����N�͎��s���Ȃ̂ŃX�R�A�ɏ���͂Ȃ�
            Rank.D => 589_990,
            Rank.CMinus => 599_990,
            Rank.C => 649_990,
            Rank.CPlus => 689_990,
            Rank.BMinus => 699_990,
            Rank.B => 749_990,
            Rank.BPlus => 789_990,
            Rank.AMinus => 799_990,
            Rank.A => 849_990,
            Rank.APlus => 889_990,
            Rank.AAMinus => 899_990,
            Rank.AA => 949_990,
            Rank.AAPlus => 989_990,
            Rank.AAA => 1_000_000,
            _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, "Unknown rank value.")
        };
    }
}
