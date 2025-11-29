using System.Collections.Concurrent;
using GPScoreTracker.Domain.Entities;
using GPScoreTracker.Domain.Repositories;

namespace GPScoreTracker.Server.WebApi.Repositories;

public class InMemoryScoreRecordRepository : IScoreRecordRepository
{
    private readonly ConcurrentDictionary<Guid, ScoreRecord> _scoreRecords = new();

    public Task<ScoreRecord?> GetByIdAsync(Guid scoreRecordId)
    {
        _scoreRecords.TryGetValue(scoreRecordId, out var scoreRecord);
        return Task.FromResult(scoreRecord);
    }

    public Task<IEnumerable<ScoreRecord>> GetByUserProfileIdAsync(Guid userProfileId)
    {
        var records = _scoreRecords.Values
            .Where(sr => sr.UserProfileId == userProfileId)
            .ToList();
        return Task.FromResult((IEnumerable<ScoreRecord>)records);
    }

    public Task<IEnumerable<ScoreRecord>> GetByChartIdAsync(Guid chartId)
    {
        var records = _scoreRecords.Values
            .Where(sr => sr.ChartId == chartId)
            .ToList();
        return Task.FromResult((IEnumerable<ScoreRecord>)records);
    }

    public Task<IEnumerable<ScoreRecord>> GetByUserProfileIdAndChartIdAsync(Guid userProfileId, Guid chartId)
    {
        var records = _scoreRecords.Values
            .Where(sr => sr.UserProfileId == userProfileId && sr.ChartId == chartId)
            .ToList();
        return Task.FromResult((IEnumerable<ScoreRecord>)records);
    }

    public Task<IEnumerable<ScoreRecord>> GetByUserProfileIdAndDateRangeAsync(Guid userProfileId, DateTime fromDate, DateTime toDate)
    {
        var records = _scoreRecords.Values
            .Where(sr => sr.UserProfileId == userProfileId && sr.PlayedAt >= fromDate && sr.PlayedAt <= toDate)
            .ToList();
        return Task.FromResult((IEnumerable<ScoreRecord>)records);
    }

    public Task AddAsync(ScoreRecord scoreRecord)
    {
        _scoreRecords.TryAdd(scoreRecord.ScoreRecordId, scoreRecord);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(ScoreRecord scoreRecord)
    {
        _scoreRecords.AddOrUpdate(scoreRecord.ScoreRecordId, scoreRecord, (key, oldValue) => scoreRecord);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid scoreRecordId)
    {
        _scoreRecords.TryRemove(scoreRecordId, out _);
        return Task.CompletedTask;
    }
}
