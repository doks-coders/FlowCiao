using FlowCiao.Studio.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FlowCiao.Studio.Persistence.Providers.Rdbms;

internal class FlowCiaoDbContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<DataContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseSqlServer(
            "Password=Abc1234;TrustServerCertificate=True;Persist Security Info=True;User ID=sa;Initial Catalog=FlowCiao;Data Source=.");
        dbContextOptionsBuilder.ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning));

        return new DataContext(dbContextOptionsBuilder.Options);
    }
}