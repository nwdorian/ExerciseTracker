using Asp.Versioning;
using ExerciseTracker.Application.Contracts.Categories.Commands;
using ExerciseTracker.Application.Contracts.Categories.Queries;
using ExerciseTracker.Application.Interfaces.Application;
using ExerciseTracker.Contracts.V1.Categories;
using ExerciseTracker.Contracts.V1.Categories.Requests;
using ExerciseTracker.Contracts.V1.Categories.Responses;
using ExerciseTracker.Contracts.V1.Exercises;
using ExerciseTracker.Domain.Models;
using ExerciseTracker.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTracker.WebApi.Controllers;

/// <summary>
/// Defines endpoints related to <see cref="Category"/>
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _categoryService.GetAll();

        var response = new GetAllCategoriesResponse(result.Value.Select(c => new CategoryRecord(c.Id, c.Name)).ToList());

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetCategoryQuery(id);
        var result = await _categoryService.GetById(query);

        if (result.IsFailure)
        {
            return result.Error.ToProblemDetails();
        }

        var response = new GetCategoryByIdResponse(result.Value.Id,
            result.Value.Name,
            result.Value.Exercises
                .Select(e => new ExerciseShallowRecord(
                    e.Id,
                    e.Start,
                    e.End,
                    e.Duration,
                    e.Description))
                .ToList()
                );

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryRequest request)
    {
        var command = new CreateCategoryCommand(request.Name!);
        var result = await _categoryService.Create(command);

        if (result.IsFailure)
        {
            return result.Error.ToProblemDetails();
        }

        var response = new CreateCategoryResponse(result.Value.Id, result.Value.Name);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteCategoryCommand(id);
        var result = await _categoryService.Delete(command);

        if (result.IsFailure)
        {
            return result.Error.ToProblemDetails();
        }

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateCategoryRequest request)
    {
        var command = new UpdateCategoryCommand(id, request.Name!);
        var result = await _categoryService.Update(command);

        if (result.IsFailure)
        {
            return result.Error.ToProblemDetails();
        }

        return NoContent();
    }
}
