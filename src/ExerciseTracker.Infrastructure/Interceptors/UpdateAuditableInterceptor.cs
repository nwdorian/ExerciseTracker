using ExerciseTracker.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ExerciseTracker.Infrastructure.Interceptors;

public sealed class UpdateAuditableInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            UpdateAuditableEntities(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateAuditableEntities(DbContext context)
    {
        var entries = context.ChangeTracker.Entries<IAuditable>().ToList();

        foreach (var auditable in entries)
        {
            if (auditable.State == EntityState.Added)
            {
                auditable.Entity.AuditCreation();
            }

            if (auditable.State == EntityState.Modified)
            {
                auditable.Entity.AuditModification();
            }
        }
    }
}
