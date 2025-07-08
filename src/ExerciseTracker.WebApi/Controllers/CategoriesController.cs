using ExerciseTracker.Application.Contracts.Categories;
using ExerciseTracker.Application.Errors;
using ExerciseTracker.Application.Interfaces.Application;
using ExerciseTracker.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTracker.WebApi.Controllers;

/// <summary>
/// Defines endpoints related to <see cref="Category"/>
/// </summary>
[Route("api/[controller]")]
[ApiController]
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
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _categoryService.GetById(id);
        return result.IsFailure
            ? NotFound(result.Error)
            : Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryRequest request)
    {
        var result = await _categoryService.Create(request);
        return result.IsFailure
            ? BadRequest(result.Error)
            : CreatedAtAction(nameof(GetById), new { result.Value.Id }, result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _categoryService.Delete(id);

        if (result.Error == CategoryErrors.NotFound)
        {
            return NotFound(result.Error);
        }

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CategoryRequest request)
    {
        var result = await _categoryService.Update(id, request);
        if (result.Error == CategoryErrors.NotFound)
        {
            return NotFound(result.Error);
        }

        return result.IsFailure
            ? BadRequest(result.Error)
            : NoContent();
    }
}
