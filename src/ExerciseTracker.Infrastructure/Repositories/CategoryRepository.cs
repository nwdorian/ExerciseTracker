using ExerciseTracker.Application.Errors;
using ExerciseTracker.Application.Interfaces.Infrastructure;
using ExerciseTracker.Domain.Models;
using ExerciseTracker.Domain.Results;
using ExerciseTracker.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ExerciseTrackerContext _context;
    public CategoryRepository(ExerciseTrackerContext context)
    {
        _context = context;
    }

    public async Task<Result<List<Category>>> GetAll()
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .ToListAsync();

        return Result.Success(categories);
    }

    public async Task<Result<Category>> GetById(Guid id)
    {
        var category = await _context.Categories
            .Include(c => c.Exercises)
            .FirstOrDefaultAsync(c => c.Id == id);
        return category is not null
            ? Result.Success(category)
            : CategoryErrors.NotFound(id);
    }

    public async Task<Result> Create(Category category)
    {
        _context.Categories.Add(category);
        var saved = await _context.SaveChangesAsync();
        return saved > 0
            ? Result.Success(category)
            : CategoryErrors.NotCreated;
    }

    public async Task<Result> Delete(Category category)
    {
        _context.Categories.Remove(category);
        var deleted = await _context.SaveChangesAsync();
        return deleted > 0
            ? Result.Success()
            : CategoryErrors.NotDeleted(category.Id);
    }

    public async Task<Result> Update(Category category)
    {
        _context.Categories.Update(category);
        var updated = await _context.SaveChangesAsync();
        return updated > 0
            ? Result.Success()
            : CategoryErrors.NotUpdated(category.Id);
    }
}
