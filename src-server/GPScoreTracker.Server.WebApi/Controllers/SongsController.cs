using GPScoreTracker.Domain.Entities;
using GPScoreTracker.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GPScoreTracker.Server.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SongsController : ControllerBase
{
    private readonly ISongRepository _songRepository;
    private readonly IChartRepository _chartRepository;

    public SongsController(ISongRepository songRepository, IChartRepository chartRepository)
    {
        _songRepository = songRepository;
        _chartRepository = chartRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
    {
        var songs = await _songRepository.GetActiveSongsAsync();
        return Ok(songs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Song>> GetSong(Guid id)
    {
        var song = await _songRepository.GetByIdAsync(id);

        if (song == null)
        {
            return NotFound();
        }

        return Ok(song);
    }

    [HttpGet("{id}/charts")]
    public async Task<ActionResult<IEnumerable<Chart>>> GetChartsBySongId(Guid id)
    {
        // 楽曲の存在チェックは要件によるが、ここではシンプルに譜面一覧を返す
        var charts = await _chartRepository.GetBySongIdAsync(id);
        return Ok(charts);
    }
}
