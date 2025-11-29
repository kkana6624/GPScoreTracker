using System.Collections.Concurrent;
using GPScoreTracker.Domain.Entities;
using GPScoreTracker.Domain.Enums;
using GPScoreTracker.Domain.Repositories;

namespace GPScoreTracker.Server.WebApi.Repositories;

public class InMemorySongRepository : ISongRepository
{
    private readonly ConcurrentDictionary<Guid, Song> _songs = new();

    public InMemorySongRepository()
    {
        // Seed data
        var songId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var song1 = new Song(songId1, "Test Song 1", "Artist 1");
        _songs.TryAdd(songId1, song1);

        var songId2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var song2 = new Song(songId2, "Test Song 2", "Artist 2");
        _songs.TryAdd(songId2, song2);
    }

    public Task<Song?> GetByIdAsync(Guid songId)
    {
        _songs.TryGetValue(songId, out var song);
        return Task.FromResult(song);
    }

    public Task<IEnumerable<Song>> GetActiveSongsAsync()
    {
        var songs = _songs.Values
            .Where(s => s.Status == SongStatus.Active)
            .ToList();
        return Task.FromResult((IEnumerable<Song>)songs);
    }

    public Task AddAsync(Song song)
    {
        _songs.TryAdd(song.SongId, song);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Song song)
    {
        _songs.AddOrUpdate(song.SongId, song, (key, oldValue) => song);
        return Task.CompletedTask;
    }
}
