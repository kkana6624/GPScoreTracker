using System.Collections.Concurrent;
using GPScoreTracker.Domain.Entities;
using GPScoreTracker.Domain.Enums;
using GPScoreTracker.Domain.Repositories;
using GPScoreTracker.Domain.ValueObjects;

namespace GPScoreTracker.Server.WebApi.Repositories;

public class InMemoryChartRepository : IChartRepository
{
    private readonly ConcurrentDictionary<Guid, Chart> _charts = new();

    public InMemoryChartRepository()
    {
        // Seed data (matching songs in InMemorySongRepository)
        var songId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Song 1, Basic, Level 5
        var chartId1 = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var chart1 = new Chart(chartId1, songId1, Difficulty.Basic, new Level(5));
        _charts.TryAdd(chartId1, chart1);

        // Song 1, Difficult, Level 9
        var chartId2 = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var chart2 = new Chart(chartId2, songId1, Difficulty.Difficult, new Level(9));
        _charts.TryAdd(chartId2, chart2);
    }

    public Task<Chart?> GetByIdAsync(Guid chartId)
    {
        _charts.TryGetValue(chartId, out var chart);
        return Task.FromResult(chart);
    }

    public Task<Chart?> GetBySongIdAndDifficultyAsync(Guid songId, Difficulty difficulty)
    {
        var chart = _charts.Values.FirstOrDefault(c => c.SongId == songId && c.Difficulty == difficulty);
        return Task.FromResult(chart);
    }

    public Task<IEnumerable<Chart>> GetBySongIdAsync(Guid songId)
    {
        var charts = _charts.Values.Where(c => c.SongId == songId).ToList();
        return Task.FromResult((IEnumerable<Chart>)charts);
    }

    public Task<IEnumerable<Chart>> GetByDifficultyAsync(Difficulty difficulty)
    {
        var charts = _charts.Values.Where(c => c.Difficulty == difficulty).ToList();
        return Task.FromResult((IEnumerable<Chart>)charts);
    }

    public Task AddAsync(Chart chart)
    {
        _charts.TryAdd(chart.ChartId, chart);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Chart chart)
    {
        _charts.AddOrUpdate(chart.ChartId, chart, (key, oldValue) => chart);
        return Task.CompletedTask;
    }
}
