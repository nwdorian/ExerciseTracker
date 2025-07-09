using ExerciseTracker.Application.Contracts.Categories;
using ExerciseTracker.Application.Interfaces.Application;
using ExerciseTracker.Domain.Models;
using ExerciseTracker.WebApi.Mappings;
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
            ? result.Error.ToActionResult()
            : Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryRequest request)
    {
        var result = await _categoryService.Create(request);
        return result.IsFailure
            ? result.Error.ToActionResult()
            : CreatedAtAction(nameof(GetById), new { result.Value.Id }, result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _categoryService.Delete(id);

        return result.IsFailure
            ? result.Error.ToActionResult()
            : NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CategoryRequest request)
    {
        var result = await _categoryService.Update(id, request);

        return result.IsFailure
            ? result.Error.ToActionResult()
            : NoContent();
    }
}
