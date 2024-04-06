using Microsoft.EntityFrameworkCore;

namespace NextPage.Data;

public class NextPageDbContext : DbContext
{
    public DbSet<BookModel> Books { get; set; }

    #region Constructors

    public NextPageDbContext()
    {
        SQLitePCL.Batteries_V2.Init();
    }
    public NextPageDbContext(DbContextOptions<NextPageDbContext> options)
        : base(options)
    {
    }

    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }

        var folder = Environment.SpecialFolder.LocalApplicationData;
        var folderPath = Environment.GetFolderPath(folder);
        string dbPath = Path.Combine(folderPath, "nextpage.db3");

        optionsBuilder.UseSqlite($"Filename={dbPath}");
    }
}