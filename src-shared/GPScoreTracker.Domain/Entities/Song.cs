using GPScoreTracker.Domain.Enums;

namespace GPScoreTracker.Domain.Entities;

/// <summary>
/// DDR�Ɏ��^����Ă���y�Ȃ�\���G���e�B�e�B
/// </summary>
public sealed class Song : IEquatable<Song>
{
    /// <summary>
    /// �y�Ȃ̈�ӂȎ��ʎq
    /// </summary>
    public Guid SongId { get; private set; }

    /// <summary>
    /// �y�Ȃ̃^�C�g��
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// �A�[�e�B�X�g��
    /// </summary>
    public string Artist { get; private set; }

    /// <summary>
    /// �y�Ȃ̏��
    /// </summary>
    public SongStatus Status { get; private set; }

    /// <summary>
    /// Song�G���e�B�e�B���쐬���܂�
    /// </summary>
    /// <param name="songId">�y�Ȃ̈�ӂȎ��ʎq</param>
    /// <param name="title">�y�Ȃ̃^�C�g��</param>
    /// <param name="artist">�A�[�e�B�X�g��</param>
    /// <exception cref="ArgumentNullException">title�܂���artist��null�̏ꍇ</exception>
    /// <exception cref="ArgumentException">title�܂���artist���󕶎���̏ꍇ</exception>
    public Song(Guid songId, string title, string artist)
    {
        ArgumentNullException.ThrowIfNull(title);
        ArgumentNullException.ThrowIfNull(artist);

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        }

        if (string.IsNullOrWhiteSpace(artist))
        {
            throw new ArgumentException("Artist cannot be empty.", nameof(artist));
        }

        SongId = songId;
        Title = title;
        Artist = artist;
        Status = SongStatus.Active; // �f�t�H���g��Active
    }

    /// <summary>
    /// �w�肳�ꂽSong�I�u�W�F�N�g�Ɠ��������ǂ����𔻒肵�܂�
    /// </summary>
    public bool Equals(Song? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return SongId == other.SongId;
    }

    /// <summary>
    /// �w�肳�ꂽ�I�u�W�F�N�g�Ɠ��������ǂ����𔻒肵�܂�
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as Song);

    /// <summary>
    /// �n�b�V���R�[�h���擾���܂�
    /// </summary>
    public override int GetHashCode() => SongId.GetHashCode();

    /// <summary>
    /// �������Z�q
    /// </summary>
    public static bool operator ==(Song? left, Song? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// �񓙉����Z�q
    /// </summary>
    public static bool operator !=(Song? left, Song? right) => !(left == right);

    /// <summary>
    /// ���̊y�Ȃ��폜�ς݂Ƀ}�[�N���܂�
    /// </summary>
    public void MarkAsDeleted()
    {
        Status = SongStatus.Deleted;
    }

    /// <summary>
    /// �y�ȏ��𕶎���Ƃ��ĕԂ��܂�
    /// </summary>
    public override string ToString() =>
        $"SongId:{SongId} Title:{Title} Artist:{Artist} Status:{Status}";
}