using ExerciseTracker.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ExerciseTracker.Infrastructure.Interceptors;

public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            SoftDeleteEntities(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void SoftDeleteEntities(DbContext context)
    {
        var entries =
            context
                .ChangeTracker
                .Entries<ISoftDeletable>()
                .Where(e => e.State == EntityState.Deleted)
                .ToList();

        foreach (var softDeletable in entries)
        {
            softDeletable.State = EntityState.Modified;
            softDeletable.Entity.SoftDelete();
        }
    }
}
