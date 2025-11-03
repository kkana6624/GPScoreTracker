using GPScoreTracker.Domain.Enums;

namespace GPScoreTracker.Domain.Entities;

/// <summary>
/// DDRに収録されている楽曲を表すエンティティ
/// </summary>
public sealed class Song : IEquatable<Song>
{
    /// <summary>
    /// 楽曲の一意な識別子
    /// </summary>
    public Guid SongId { get; private set; }

    /// <summary>
    /// 楽曲のタイトル
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// アーティスト名
    /// </summary>
    public string Artist { get; private set; }

    /// <summary>
    /// 楽曲の状態
    /// </summary>
    public SongStatus Status { get; private set; }

    /// <summary>
    /// Songエンティティを作成します
    /// </summary>
    /// <param name="songId">楽曲の一意な識別子</param>
    /// <param name="title">楽曲のタイトル</param>
    /// <param name="artist">アーティスト名</param>
    /// <exception cref="ArgumentNullException">titleまたはartistがnullの場合</exception>
    /// <exception cref="ArgumentException">titleまたはartistが空文字列の場合</exception>
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
        Status = SongStatus.Active; // デフォルトはActive
    }

    /// <summary>
    /// 指定されたSongオブジェクトと等しいかどうかを判定します
    /// </summary>
    public bool Equals(Song? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return SongId == other.SongId;
    }

    /// <summary>
    /// 指定されたオブジェクトと等しいかどうかを判定します
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as Song);

    /// <summary>
    /// ハッシュコードを取得します
    /// </summary>
    public override int GetHashCode() => SongId.GetHashCode();

    /// <summary>
    /// 等価演算子
    /// </summary>
    public static bool operator ==(Song? left, Song? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// 非等価演算子
    /// </summary>
    public static bool operator !=(Song? left, Song? right) => !(left == right);

    /// <summary>
    /// この楽曲を削除済みにマークします
    /// </summary>
    public void MarkAsDeleted()
    {
        Status = SongStatus.Deleted;
    }

    /// <summary>
    /// 楽曲情報を文字列として返します
    /// </summary>
    public override string ToString() =>
        $"SongId:{SongId} Title:{Title} Artist:{Artist} Status:{Status}";
}