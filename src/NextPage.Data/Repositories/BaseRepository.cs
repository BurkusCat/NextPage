using Microsoft.EntityFrameworkCore;

namespace NextPage.Data;

public abstract class BaseRepository
{
    protected readonly NextPageDbContext dbContext;

    public BaseRepository(NextPageDbContext dbContext)
    {
        try
        {
            dbContext.Database.Migrate();
        }
        catch (Exception)
        {
        }

        this.dbContext = dbContext;
    }

    public void AddOrUpdate<T>(T entity) where T : class
    {
        var dbSet = dbContext.Set<T>();
        var entry = dbContext.Entry(entity);

        if (entry.State == EntityState.Detached)
        {
            // Entity is not tracked, so check if it already exists in the database
            var keyProperties = entry.Metadata.FindPrimaryKey().Properties;
            var keyValues = keyProperties.Select(p => entry.OriginalValues[p]).ToArray();

            var existingEntity = dbSet.Find(keyValues);

            if (existingEntity != null)
            {
                // Entity exists, update its properties
                dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                // Entity doesn't exist, add it to the context
                dbSet.Add(entity);
            }
        }
        else if (entry.State == EntityState.Modified)
        {
            // Entity is tracked and marked as modified, update it
            dbSet.Update(entity);
        }
        else if (entry.State == EntityState.Added)
        {
            // Entity is tracked and marked as added, no action needed
        }

        dbContext.SaveChanges();
    }
}
