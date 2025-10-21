namespace GPScoreTracker.Domain.Enums;

/// <summary>
/// �X�R�A�̕]�������N��\���񋓌^
/// DDR�̎��ۂ̃����N����V�X�e���Ɋ�Â�
/// </summary>
public enum Rank
{
    /// <summary>
    /// E�����N�i�N���A���s�j
    /// �r���Ń��C�t���s�����ꍇ�B�X�R�A�͎��s���_�̂��̂��L�^�����
    /// </summary>
    E = 0,

    /// <summary>
    /// D�����N
    /// �X�R�A: 0�`589,990�_
    /// </summary>
    D = 1,

    /// <summary>
    /// C-�����N
    /// �X�R�A: 590,000�`599,990�_
    /// </summary>
    CMinus = 2,

    /// <summary>
    /// C�����N
    /// �X�R�A: 600,000�`649,990�_
    /// </summary>
    C = 3,

    /// <summary>
    /// C+�����N
    /// �X�R�A: 650,000�`689,990�_
    /// </summary>
    CPlus = 4,

    /// <summary>
    /// B-�����N
    /// �X�R�A: 690,000�`699,990�_
    /// </summary>
    BMinus = 5,

    /// <summary>
    /// B�����N
    /// �X�R�A: 700,000�`749,990�_
    /// </summary>
    B = 6,

    /// <summary>
    /// B+�����N
    /// �X�R�A: 750,000�`789,990�_
    /// </summary>
    BPlus = 7,

    /// <summary>
    /// A-�����N
    /// �X�R�A: 790,000�`799,990�_
    /// </summary>
    AMinus = 8,

    /// <summary>
    /// A�����N
    /// �X�R�A: 800,000�`849,990�_
    /// </summary>
    A = 9,

    /// <summary>
    /// A+�����N
    /// �X�R�A: 850,000�`889,990�_
    /// </summary>
    APlus = 10,

    /// <summary>
    /// AA-�����N
    /// �X�R�A: 890,000�`899,990�_
    /// </summary>
    AAMinus = 11,

    /// <summary>
    /// AA�����N
    /// �X�R�A: 900,000�`949,990�_
    /// </summary>
    AA = 12,

    /// <summary>
    /// AA+�����N
    /// �X�R�A: 950,000�`989,990�_
    /// </summary>
    AAPlus = 13,

    /// <summary>
    /// AAA�����N�i�ō������N�j
    /// �X�R�A: 990,000�`1,000,000�_
    /// </summary>
    AAA = 14
}
