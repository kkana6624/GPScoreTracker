using GPScoreTracker.Domain.Entities;
using GPScoreTracker.Domain.Enums;
using Xunit;

namespace GPScoreTracker.Domain.Tests.Entities;

/// <summary>
/// Song エンティティのテスト
/// </summary>
public class SongTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_ValidValues_CreatesInstance()
    {
        // Arrange
        var songId = Guid.NewGuid();

        // Act
        var song = new Song(songId, "Test Song", "Test Artist");

        // Assert
        Assert.Equal(songId, song.SongId);
        Assert.Equal("Test Song", song.Title);
        Assert.Equal("Test Artist", song.Artist);
        Assert.Equal(SongStatus.Active, song.Status); // デフォルトはActive
    }

    [Fact]
    public void Constructor_NullTitle_ThrowsArgumentNullException()
    {
        // Arrange
        var songId = Guid.NewGuid();

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => new Song(songId, null!, "Test Artist"));
        Assert.Equal("title", exception.ParamName);
    }

    [Fact]
    public void Constructor_NullArtist_ThrowsArgumentNullException()
    {
        // Arrange
        var songId = Guid.NewGuid();

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(
            () => new Song(songId, "Test Song", null!));
        Assert.Equal("artist", exception.ParamName);
    }

    [Fact]
    public void Constructor_EmptyTitle_ThrowsArgumentException()
    {
        // Arrange
        var songId = Guid.NewGuid();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(
            () => new Song(songId, "", "Test Artist"));
        Assert.Contains("Title cannot be empty", exception.Message);
    }

    [Fact]
    public void Constructor_EmptyArtist_ThrowsArgumentException()
    {
        // Arrange
        var songId = Guid.NewGuid();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(
            () => new Song(songId, "Test Song", ""));
        Assert.Contains("Artist cannot be empty", exception.Message);
    }

    #endregion

    #region Equality Tests

    [Fact]
    public void Equals_SameSongId_ReturnsTrue()
    {
        // Arrange
        var songId = Guid.NewGuid();
        var song1 = new Song(songId, "Song A", "Artist A");
        var song2 = new Song(songId, "Song B", "Artist B"); // 異なるプロパティ

        // Act & Assert
        Assert.Equal(song1, song2);
        Assert.True(song1 == song2);
        Assert.False(song1 != song2);
    }

    [Fact]
    public void Equals_DifferentSongId_ReturnsFalse()
    {
        // Arrange
        var songId1 = Guid.NewGuid();
        var songId2 = Guid.NewGuid();
        var song1 = new Song(songId1, "Song A", "Artist A");
        var song2 = new Song(songId2, "Song A", "Artist A");

        // Act & Assert
        Assert.NotEqual(song1, song2);
        Assert.False(song1 == song2);
        Assert.True(song1 != song2);
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var songId = Guid.NewGuid();
        var song = new Song(songId, "Test Song", "Test Artist");

        // Act & Assert
        Assert.False(song.Equals(null));
        Assert.False(song == null);
        Assert.True(song != null);
    }

    [Fact]
    public void Equals_SameInstance_ReturnsTrue()
    {
        // Arrange
        var songId = Guid.NewGuid();
        var song = new Song(songId, "Test Song", "Test Artist");

        // Act & Assert
#pragma warning disable CS1718 // 意図的に同じ変数を比較
        Assert.True(song.Equals(song));
        Assert.True(song == song);
#pragma warning restore CS1718
    }

    #endregion

    #region GetHashCode Tests

    [Fact]
    public void GetHashCode_SameSongId_ReturnsSameHashCode()
    {
        // Arrange
        var songId = Guid.NewGuid();
        var song1 = new Song(songId, "Song A", "Artist A");
        var song2 = new Song(songId, "Song B", "Artist B");

        // Act & Assert
        Assert.Equal(song1.GetHashCode(), song2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentSongId_ReturnsDifferentHashCode()
    {
        // Arrange
        var songId1 = Guid.NewGuid();
        var songId2 = Guid.NewGuid();
        var song1 = new Song(songId1, "Song A", "Artist A");
        var song2 = new Song(songId2, "Song A", "Artist A");

        // Act & Assert
        Assert.NotEqual(song1.GetHashCode(), song2.GetHashCode());
    }

    #endregion

    #region Business Method Tests

    [Fact]
    public void MarkAsDeleted_ChangesStatusToDeleted()
    {
        // Arrange
        var songId = Guid.NewGuid();
        var song = new Song(songId, "Test Song", "Test Artist");

        // Act
        song.MarkAsDeleted();

        // Assert
        Assert.Equal(SongStatus.Deleted, song.Status);
    }

    [Fact]
    public void MarkAsDeleted_AlreadyDeleted_RemainsDeleted()
    {
        // Arrange
        var songId = Guid.NewGuid();
        var song = new Song(songId, "Test Song", "Test Artist");
        song.MarkAsDeleted();

        // Act
        song.MarkAsDeleted(); // 再度呼び出し

        // Assert
        Assert.Equal(SongStatus.Deleted, song.Status);
    }

    #endregion

    #region ToString Tests

    [Fact]
    public void ToString_ReturnsCorrectFormat()
    {
        // Arrange
        var songId = Guid.Parse("12345678-1234-1234-1234-123456789abc");
        var song = new Song(songId, "Test Song", "Test Artist");

        // Act
        var result = song.ToString();

        // Assert
        Assert.Contains("SongId:12345678-1234-1234-1234-123456789abc", result);
        Assert.Contains("Title:Test Song", result);
        Assert.Contains("Artist:Test Artist", result);
        Assert.Contains("Status:Active", result);
    }

    #endregion
}