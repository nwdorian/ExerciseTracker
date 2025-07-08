using ExerciseTracker.Application.Contracts.Exercises;
using ExerciseTracker.Application.Errors;
using ExerciseTracker.Application.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTracker.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExercisesController : ControllerBase
{
    private readonly IExerciseService _exerciseService;

    public ExercisesController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _exerciseService.GetAll();

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _exerciseService.GetById(id);
        return result.IsFailure
            ? NotFound(result.Error)
            : Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ExerciseRequest request)
    {
        var result = await _exerciseService.Create(request);
        return result.IsFailure
            ? BadRequest(result.Error)
            : CreatedAtAction(nameof(GetById), new { result.Value.Id }, result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _exerciseService.Delete(id);
        if (result.Error == ExerciseErrors.NotFound)
        {
            return NotFound(result.Error);
        }

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ExerciseRequest request)
    {
        var result = await _exerciseService.Update(id, request);
        if (result.Error == ExerciseErrors.NotFound)
        {
            return NotFound(result.Error);
        }

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }
}
