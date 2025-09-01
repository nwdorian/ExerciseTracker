using Asp.Versioning;
using ExerciseTracker.Application.Contracts.Exercises.Commands;
using ExerciseTracker.Application.Contracts.Exercises.Queries;
using ExerciseTracker.Application.Interfaces.Application;
using ExerciseTracker.Contracts.V1.Categories;
using ExerciseTracker.Contracts.V1.Exercises;
using ExerciseTracker.Contracts.V1.Exercises.Requests;
using ExerciseTracker.Contracts.V1.Exercises.Responses;
using ExerciseTracker.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTracker.WebApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
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

        var response = new GetAllExercisesResponse(result.Value
            .Select(e => new ExerciseRecord(
                e.Id,
                e.Start,
                e.End,
                e.Duration,
                e.Description,
                new CategoryRecord(e.Category.Id, e.Category.Name)
                )
            ).ToList()
        );

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetExerciseQuery(id);
        var result = await _exerciseService.GetById(query);

        if (result.IsFailure)
        {
            return result.Error.ToProblemDetails();
        }

        var response = new GetExerciseByIdResponse(
            new ExerciseRecord(result.Value.Id,
                result.Value.Start,
                result.Value.End,
                result.Value.Duration,
                result.Value.Description,
                new CategoryRecord(result.Value.Category.Id, result.Value.Category.Name))
        );

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateExerciseRequest request)
    {
        var command = new CreateExerciseCommand(request.CategoryId,
            request.Start,
            request.End,
            request.Description!);

        var result = await _exerciseService.Create(command);

        if (result.IsFailure)
        {
            return result.Error.ToProblemDetails();
        }

        var response = new CreateExerciseResponse(
                result.Value.Id,
                result.Value.Start,
                result.Value.End,
                result.Value.Duration,
                result.Value.Description,
                new CategoryRecord(result.Value.Category.Id, result.Value.Category.Name)
        );

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteExerciseCommand(id);
        var result = await _exerciseService.Delete(command);

        if (result.IsFailure)
        {
            return result.Error.ToProblemDetails();
        }

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateExerciseRequest request)
    {
        var command = new UpdateExerciseCommand(id, request.CategoryId, request.Start, request.End, request.Description!);
        var result = await _exerciseService.Update(command);

        if (result.IsFailure)
        {
            return result.Error.ToProblemDetails();
        }

        return NoContent();
    }
}
