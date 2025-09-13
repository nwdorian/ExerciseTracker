using ExerciseTracker.Domain.Models;
using ExerciseTracker.Domain.Results;

namespace ExerciseTracker.Application.Interfaces.Infrastructure;

public interface ICategoryRepository
{
    Task<Result<List<Category>>> GetAll();
    Task<Result<Category>> GetById(Guid id);
    Task<Result> Create(Category category);
    Task<Result> Delete(Category category);
    Task<Result> Update(Category category);
}
