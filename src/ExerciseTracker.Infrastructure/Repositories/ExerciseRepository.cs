using ExerciseTracker.Application.Errors;
using ExerciseTracker.Application.Interfaces.Infrastructure;
using ExerciseTracker.Domain.Models;
using ExerciseTracker.Domain.Results;
using ExerciseTracker.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker.Infrastructure.Repositories;

public class ExerciseRepository : IExerciseRepository
{
    private readonly ExerciseTrackerContext _context;

    public ExerciseRepository(ExerciseTrackerContext context)
    {
        _context = context;
    }

    public async Task<Result<List<Exercise>>> GetAll()
    {
        var categories = await _context.Exercises
            .Include(e => e.Category)
            .AsNoTracking()
            .ToListAsync();

        return Result.Success(categories);
    }

    public async Task<Result<Exercise>> GetById(Guid id)
    {
        var exercise = await _context.Exercises
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id == id);

        return exercise is not null
            ? Result.Success(exercise)
            : ExerciseErrors.NotFound(id);
    }

    public async Task<Result> Create(Exercise exercise)
    {
        _context.Exercises.Add(exercise);
        var saved = await _context.SaveChangesAsync();
        return saved > 0
            ? Result.Success()
            : ExerciseErrors.NotCreated;
    }

    public async Task<Result> Delete(Exercise exercise)
    {
        _context.Exercises.Remove(exercise);
        var deleted = await _context.SaveChangesAsync();
        return deleted > 0
            ? Result.Success()
            : ExerciseErrors.NotDeleted(exercise.Id);
    }

    public async Task<Result> Update(Exercise exercise)
    {
        _context.Exercises.Update(exercise);
        var updated = await _context.SaveChangesAsync();
        return updated > 0
            ? Result.Success()
            : ExerciseErrors.NotUpdated(exercise.Id);
    }
}
