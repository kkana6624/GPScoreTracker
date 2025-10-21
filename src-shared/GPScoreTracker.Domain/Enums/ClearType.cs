namespace GPScoreTracker.Domain.Enums;

/// <summary>
/// �v���C�̒B���󋵂�\���񋓌^
/// </summary>
public enum ClearType
{
    /// <summary>
    /// ���s�i�Q�[���I�[�o�[�j
    /// </summary>
    Failed = 0,

    /// <summary>
    /// �N���A
    /// </summary>
    Cleared = 1,

    /// <summary>
    /// ���C�t4�N���A
    /// </summary>
    LifeFourClear = 2,

    /// <summary>
    /// �t���R���{�B��
    /// </summary>
    FullCombo = 3,

    /// <summary>
    /// Great�ȏ�̃t���R���{
    /// </summary>
    GreatFullCombo = 4,

    /// <summary>
    /// Perfect�ȏ�̃t���R���{
    /// </summary>
    PerfectFullCombo = 5,

    /// <summary>
    /// Marvelous�݂̂̃t���R���{�i�ō��]���j
    /// </summary>
    MarvelousFullCombo = 6
}
