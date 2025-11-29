using GPScoreTracker.Domain.Entities;
using GPScoreTracker.Domain.Enums;
using GPScoreTracker.Domain.Repositories;
using GPScoreTracker.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace GPScoreTracker.Server.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScoresController : ControllerBase
{
    private readonly IScoreRecordRepository _scoreRecordRepository;
    private readonly ILogger<ScoresController> _logger;

    public ScoresController(IScoreRecordRepository scoreRecordRepository, ILogger<ScoresController> logger)
    {
        _scoreRecordRepository = scoreRecordRepository;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<ScoreRecord>> AddScoreRecord(CreateScoreRecordDto dto)
    {
        try
        {
            var score = new Score(
                dto.Points,
                dto.ExScore,
                Enum.Parse<Rank>(dto.Rank),
                new Judgements(
                    dto.Marvelous,
                    dto.Perfect,
                    dto.Great,
                    dto.Good,
                    dto.Miss
                ),
                dto.MaxCombo,
                Enum.Parse<ClearType>(dto.ClearType)
            );

            var scoreRecord = new ScoreRecord(
                Guid.NewGuid(),
                dto.UserProfileId,
                dto.ChartId,
                score,
                DateTime.UtcNow
            );

            await _scoreRecordRepository.AddAsync(scoreRecord);

            return CreatedAtAction(nameof(GetScoreRecords), new { userId = dto.UserProfileId }, scoreRecord);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating score record");
            return StatusCode(500, "Internal server error");
        }
    }

    // Note: This route is slightly inconsistent with REST resource hierarchy but useful for querying.
    // Ideally might be /api/users/{userId}/scores or similar.
    // For simplicity in this controller, we'll use a route parameter here.
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<ScoreRecord>>> GetScoreRecords(Guid userId)
    {
        var scores = await _scoreRecordRepository.GetByUserProfileIdAsync(userId);
        return Ok(scores);
    }
}

public class CreateScoreRecordDto
{
    public Guid UserProfileId { get; set; }
    public Guid ChartId { get; set; }
    public int Points { get; set; }
    public int ExScore { get; set; }
    public string Rank { get; set; } = string.Empty;
    public int MaxCombo { get; set; }
    public string ClearType { get; set; } = string.Empty;

    // Judgements
    public int Marvelous { get; set; }
    public int Perfect { get; set; }
    public int Great { get; set; }
    public int Good { get; set; }
    public int Miss { get; set; }
    public int Ok { get; set; }
}
